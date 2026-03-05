Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging
Imports System.Text
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports BarcodeLib.BarcodeReader
Imports BarcodeLib


Namespace Controller.Indexer.Indexacion

    Public Class IndexacionController
        Inherits IndexerController

#Region " Declaraciones "

        'Private _foliosDataTable As DBImaging.SchemaProcess.TBL_Cargue_FolioDataTable
        Private _foliosDataTable As DBImaging.SchemaProcess.CTA_Get_Cargue_Folio_IndexadoDataTable

        Private _cargueRow As DBImaging.SchemaProcess.TBL_CargueRow
        Private _paqueteRow As DBImaging.SchemaProcess.TBL_Cargue_PaqueteRow

        Private _dataItemIndex As List(Of Integer)
        Private _centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType

#End Region

#Region " Implementación IIndexerController "

        Public Overrides Sub Inicializar(ByVal nTempPath As String, ByVal nIndexerSesion As Security.Library.Session.Sesion, ByVal nIndexerDesktopGlobal As DesktopGlobal, ByVal nIndexerImagingGlobal As ImagingGlobal)
            Me.IndexerSesion = nIndexerSesion
            Me.IndexerDesktopGlobal = nIndexerDesktopGlobal
            Me.IndexerImagingGlobal = nIndexerImagingGlobal

            DBImaging.DBImagingDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat
            DBCore.DBCoreDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat
            DBSecurity.DBSecurityDataBaseManager.IdentifierDateFormat = nIndexerDesktopGlobal.IdentifierDateFormat

            _IndexerView = New FormIndexerView
            View.SetController(Me)

            IndexerView.ShowSaveButton(True)
            IndexerView.ShowAddFolioButton(False)
            IndexerView.ShowDeleteFolioButton(False)
            IndexerView.ShowNewFolderButton(Not Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Folder_Unico)
            IndexerView.ShowNewFileButton(Not Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_File_Unico)
            IndexerView.ShowNextButton(True)
            IndexerView.ShowReprocesoButton(False)

            IndexerView.ShowInformationPanel(False)
            IndexerView.ShowDataPanel(False)
            IndexerView.ShowCamposLlavePanel(False)
            IndexerView.ShowValidationsPanel(False)
            IndexerView.ShowValidationsListasPanel(False)
            IndexerView.ShowToolTipData(True)
            IndexerView.ShowAutoIndexar(Not Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Folder_Unico)

            IndexerView.Esquema_Enabled = True

            IndexerView.TipoDocumental_Enabled = True

            _TempPath = nTempPath.TrimEnd("\"c) & "\"
            If (Not Directory.Exists(nTempPath)) Then
                Directory.CreateDirectory(nTempPath)
            End If
        End Sub

        Public Overrides ReadOnly Property Cargado As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides ReadOnly Property AllowNewFile As Boolean
            Get
                Return Not Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_File_Unico
            End Get
        End Property

        Public Overrides ReadOnly Property AllowNewFolder As Boolean
            Get
                Return Not Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Folder_Unico
            End Get
        End Property

        Public Overrides ReadOnly Property ShowSecondControls As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Function NewDocumentFile() As Boolean
            If Validar() Then
                SaveDocument()

                If CurrentDocumentFile.Count > 1 Then
                    Dim newDocument = CurrentFolder.NewDocumentFile()

                    CurrentFolder.Add(newDocument)
                    CurrentFolio.Move(newDocument)

                    _CurrentFolioIndex = 0
                    _CurrentDocumentFileIndex += 1

                    Me.View.ScrollThumbnail()

                    Return True
                End If
            End If

            Return False
        End Function

        Public Overrides Function NewFolder() As Boolean
            If Validar() Then
                SaveDocument()

                If (ValidarFolder(CurrentFolder)) Then
                    If CurrentDocumentFile.Count > 1 Then
                        Dim newCarpeta = New Generic.Folder(Me)
                        Dim newDocument = newCarpeta.NewDocumentFile()

                        Folders.Add(newCarpeta)
                        newCarpeta.Add(newDocument)
                        CurrentFolio.Move(newDocument)

                        _CurrentFolioIndex = 0
                        _CurrentDocumentFileIndex = 0
                        _CurrentFolderIndex += 1

                        View.ThumbnailPanel.Controls.Add(CurrentFolder.Panel)

                        CurrentFolder.Esquema = IndexerView.Esquema_Value

                        Me.View.ScrollThumbnail()
                        Return True
                    End If
                End If
            End If

            Return False
        End Function

        Public Overrides Function AddFolio() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function DeleteFolio() As Boolean
            Throw New NotImplementedException
        End Function

        Public Overrides Function Save() As Boolean
            SaveFolio()
            SaveDocument()

            If (Validar()) Then
                Dim progressForm As New FormProgress
                Dim manager As FileProviderManager = Nothing
                Dim paqueteNombre As String = ""
                Dim items As Integer = 0
                Dim documentos = 0

                Dim formato = Utilities.GetEnumFormat(Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                Dim compresion = Utilities.GetEnumCompression(CType(Me.IndexerImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                Dim fechaInicioProceso = Now

                Try
#If Not DEBUG Then
                    ProgressForm.Show()
#End If
                    progressForm.SetProceso("Indexar")
                    progressForm.CanCancel = False
                    progressForm.SetAccion("Procesar imágenes")
                    progressForm.SetProgreso(0)
                    progressForm.SetMaxValue(3)
                    Application.DoEvents()

                    Dim dbmCore As DBCore.DBCoreDataBaseManager
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Try
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                        dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                        'dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)
                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        'dbmCore.LinkDataBaseManager(dbmImaging)

                        Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Me.IndexerDesktopGlobal.ServidorImagenRow.fk_Entidad, Me.IndexerDesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType

                        manager = New FileProviderManager(servidor, _centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                        manager.Connect()

                        Dim enTransferencia = (Me.IndexerDesktopGlobal.ServidorImagenRow.fk_Entidad <> Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad_Servidor Or Me.IndexerDesktopGlobal.ServidorImagenRow.id_Servidor <> Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Servidor)
                        Dim entidadServidorTransferencia As SlygNullable(Of Short) = Nothing
                        Dim servidorTransferencia As SlygNullable(Of Short) = Nothing

                        If (enTransferencia) Then
                            entidadServidorTransferencia = Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad_Servidor
                            servidorTransferencia = Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Servidor
                        End If

                        '--------------------------------------------------------------------------------------
                        ' REPORTAR DUPLICADOS
                        '--------------------------------------------------------------------------------------
                        Try
                            Dim fileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBGet(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)

                            If (fileBloqueadoDataTable(0).fk_Usuario_log <> Me.IndexerSesion.Usuario.id) Then
                                dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Cargues.DBExecute(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PcName, 0)
                                Return True
                            End If

                        Catch : End Try
                        '--------------------------------------------------------------------------------------

                        Dim sqlAnexoItem As New StringBuilder()
                        Dim sqlAnexoFolioItem As New StringBuilder()
                        Dim sqlFolderItem As New StringBuilder()
                        Dim sqlLlaveItem As New StringBuilder()
                        Dim sqlDocumentoItem As New StringBuilder()
                        Dim sqlFolioItem As New StringBuilder()
                        Dim sql As String

                        Dim folioType = New DBImaging.SchemaProcess.TBL_Cargue_FolioType()
                        folioType.Indexado = True

                        ' Calcular los anexos
                        Const idAnexoItem As Integer = 1
                        Dim foliosAnexo As Integer = 0
                        Dim idAnexoFolioItem As Integer = 0

                        For Each FolderItem In Folders
                            If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Anexo Or Me.IndexerView.AnexoCompleto) Then

                                For Each documentItem As Generic.DocumentFile In FolderItem
                                    foliosAnexo += documentItem.Folios
                                    For Each folioItem As Generic.Folio In documentItem
                                        idAnexoFolioItem += 1

                                        sql = "INSERT INTO #AnexoFolioItem (fk_AnexoItem, id_AnexoFolioItem, fk_Cargue_Item, fk_Item_Folio) VALUES (" & idAnexoItem & ", " & idAnexoFolioItem & ", " & folioItem.Cargue_Item & ", " & folioItem.Id_Item_Folio & ");"
                                        sqlAnexoFolioItem.Append(sql)
                                    Next
                                Next
                                If (foliosAnexo > 0) Then
                                    sql = "INSERT INTO #AnexoItem (id_AnexoItem, Folios, fk_Anexo, fk_Documento_Anexo) VALUES (" & idAnexoItem & ", " & foliosAnexo & ", NULL, " & FolderItem.IdAnexo & ");"
                                    sqlAnexoItem.Append(sql)
                                End If
                            End If
                        Next

                        'If (foliosAnexo > 0) Then
                        '    sql = "INSERT INTO #AnexoItem (id_AnexoItem, Folios, fk_Anexo, fk_Documento) VALUES (" & idAnexoItem & ", " & foliosAnexo & ", NULL);"
                        '    sqlAnexoItem.Append(sql)
                        'End If

                        ' Calcular documentos
                        Dim idFolderItem As Integer = 0
                        For Each FolderItem In Folders
                            If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then
                                ' Crear FolderItem
                                idFolderItem += 1
                                sql = "INSERT INTO #FolderItem (id_FolderItem, fk_Esquema, fk_Documento_Anexo) VALUES (" & idFolderItem & ", " & FolderItem.Esquema & ", " & FolderItem.IdAnexo & ");"
                                sqlFolderItem.Append(sql)

                                'Crea las llaves a partir del nombre del paquete
                                If (Me.IndexerImagingGlobal.ProyectoImagingRow.Captura_Llaves_Paquete) Then
                                    paqueteNombre = _paqueteRow.Data_Path

                                    For Each Llave In Me.IndexerImagingGlobal.ProyectoImagingLlaveDataTable
                                        If (Llave.Posicion_Inicial + Llave.Posicion_Longitud > paqueteNombre.Length) Then
                                            Dim mensaje As New StringBuilder()
                                            mensaje.AppendLine("El paquete que se está indexando tiene un nombre de carpeta que no cumple con la estructura de las llaves")
                                            mensaje.AppendLine("")
                                            mensaje.AppendLine("Cargue: " & _paqueteRow.fk_Cargue)
                                            mensaje.AppendLine("Paquete: " & _paqueteRow.id_Cargue_Paquete)
                                            mensaje.AppendLine("DataPath: " & paqueteNombre)
                                            mensaje.AppendLine("Llave: " & Llave.Id_Proyecto_Llave_Paquete)
                                            mensaje.AppendLine("Inicio: " & Llave.Posicion_Inicial)
                                            mensaje.AppendLine("Longitud: " & Llave.Posicion_Longitud)

                                            Throw New Exception(mensaje.ToString())
                                        Else
                                            Dim valorLlave As String = ""

                                            Select Case Llave.fk_Campo_Tipo
                                                Case DesktopConfig.CampoTipo.Texto
                                                    valorLlave = paqueteNombre.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)

                                                Case DesktopConfig.CampoTipo.Numerico
                                                    valorLlave = CLng(paqueteNombre.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)).ToString()

                                                Case DesktopConfig.CampoTipo.Fecha
                                                    Dim fechaPaquete = paqueteNombre.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)
                                                    valorLlave = CDate(fechaPaquete.Substring(0, 4) & "/" & fechaPaquete.Substring(4, 2) & "/" & fechaPaquete.Substring(6, 2)).ToString("yyyy/MM/dd")

                                                Case DesktopConfig.CampoTipo.Lista
                                                    valorLlave = CLng(paqueteNombre.Substring(Llave.Posicion_Inicial, Llave.Posicion_Longitud)).ToString()
                                            End Select

                                            sql = "INSERT INTO #LlaveItem (fk_FolderItem, fk_Proyecto_Llave, fk_Campo_Tipo, Valor_Llave) VALUES (" & idFolderItem & ", " & Llave.Id_Proyecto_Llave_Paquete & ", " & Llave.fk_Campo_Tipo & ", '" & valorLlave & "');"
                                            sqlLlaveItem.Append(sql)
                                        End If
                                    Next
                                End If

                                Dim idDocumentoItem As Integer = 0
                                For Each documentItem As Generic.DocumentFile In FolderItem
                                    idDocumentoItem += 1
                                    sql = "INSERT INTO #DocumentoItem (fk_FolderItem, id_DocumentoItem, fk_Documento, Folios) VALUES (" & idFolderItem & ", " & idDocumentoItem & ", " & documentItem.TipoDocumento & ", " & documentItem.Folios & ");"
                                    sqlDocumentoItem.Append(sql)

                                    documentos += 1

                                    Dim idFolioItem As Integer = 0
                                    For Each folioItem As Generic.Folio In documentItem
                                        idFolioItem += 1
                                        items += 1

                                        sql = "INSERT INTO #FolioItem (fk_FolderItem, fk_DocumentoItem, id_FolioItem, fk_Cargue_Item, fk_Item_Folio) VALUES (" & idFolderItem & ", " & idDocumentoItem & ", " & idFolioItem & ", " & folioItem.Cargue_Item & ", " & folioItem.Id_Item_Folio & ");"
                                        sqlFolioItem.Append(sql)

                                        If (folioItem.Updated Or Me.IndexerDesktopGlobal.CentroProcesamientoRow.Usa_Cache_Local) Then
                                            ImageManager.Save(folioItem.ThumbnailImage, Program.AppPath & Program.TempPath & "temp" & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", formato, compresion, False, Program.AppPath & Program.TempPath)

                                            Dim dataImage = ImageManager.GetData(folioItem.FileName)
                                            Dim dataImageThumbnail = ImageManager.GetThumbnailData(Program.AppPath & Program.TempPath & "temp" & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)

                                            If folioItem.Updated Then
                                                manager.UpdateFolio(folioItem.Cargue, folioItem.Cargue_Paquete, folioItem.Cargue_Item, folioItem.Id_Item_Folio, dataImage, dataImageThumbnail(0))
                                            End If
                                        End If

                                        Application.DoEvents()
                                    Next
                                Next
                            End If
                        Next

                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                        manager.TransactionBegin()

                        progressForm.SetProgreso(1)
                        Application.DoEvents()

                        '-------------------------------------------------------------------------------------------------------------
                        ' Procesar información
                        '-------------------------------------------------------------------------------------------------------------
                        Dim IsSofTrac As Boolean = False
                        If _cargueRow.Observaciones = "SOFTRAC" Then
                            IsSofTrac = True
                        End If

                        Dim resultadoDataTable = dbmImaging.SchemaProcess.PA_Publicar_Indexacion.DBExecute(Me._cargueRow.fk_OT,
                                                                                                        _paqueteRow.fk_Cargue,
                                                                                                        _paqueteRow.id_Cargue_Paquete,
                                                                                                        _cargueRow.fk_Entidad_Procesamiento,
                                                                                                        _paqueteRow.fk_Sede_Procesamiento_Asignada,
                                                                                                        _paqueteRow.fk_Centro_Procesamiento_Asignado,
                                                                                                        _cargueRow.fk_Entidad,
                                                                                                        _cargueRow.fk_Proyecto,
                                                                                                        Me.IndexerSesion.Usuario.id,
                                                                                                        Me.IndexerDesktopGlobal.ServidorImagenRow.fk_Entidad,
                                                                                                        Me.IndexerDesktopGlobal.ServidorImagenRow.id_Servidor,
                                                                                                        enTransferencia,
                                                                                                        entidadServidorTransferencia,
                                                                                                        servidorTransferencia,
                                                                                                        Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida,
                                                                                                        sqlAnexoItem.ToString(),
                                                                                                        sqlAnexoFolioItem.ToString(),
                                                                                                        sqlFolderItem.ToString(),
                                                                                                        sqlLlaveItem.ToString(),
                                                                                                        sqlDocumentoItem.ToString(),
                                                                                                        sqlFolioItem.ToString(),
                                                                                                        IsSofTrac,
                                                                                                        Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Guardar_Nombre_Imagen)


                        dbmImaging.Transaction_Commit()

                        '******Pasar data de la máquina******
                        If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Transmision_Data_Maquina Then
                            Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing
                            dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Softrac)
                            dbmSofTrac.Connection_Open(Me.IndexerSesion.Usuario.id)

                            Try
                                dbmSofTrac.SchemaProcess.PA_Transmision_Data_Miharu.DBExecute(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete, Me._cargueRow.fk_OT, _cargueRow.fk_Entidad_Procesamiento, _paqueteRow.fk_Sede_Procesamiento_Asignada, _paqueteRow.fk_Centro_Procesamiento_Asignado, _cargueRow.fk_Entidad, _cargueRow.fk_Proyecto, Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Correccion_Captura_Maquina)
                            Catch ex As Exception
                                MessageBox.Show("Error finalizando indexación transmitiendo data SofTrac,  cargue: " + _paqueteRow.fk_Cargue.ToString + " , paquete" + _paqueteRow.id_Cargue_Paquete.ToString() + ex.Message, "Error transmisión Data: máquina a Miharu", MessageBoxButtons.OK)
                            Finally
                                If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
                            End Try
                        End If

                        '*******Pasar datos destape a captura*****
                        If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campos_Captura_Destape Then
                            dbmImaging.SchemaProcess.PA_Insertar_Datos_Captura_Destape.DBExecute(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)
                        End If


                        Try
                            'dbmImaging.DataBase.BeginBullkQuery()

                            ' Mover las imágenes
                            idFolderItem = 0
                            For Each FolderItem In Folders
                                ' Crear FolderItem
                                If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then idFolderItem += 1

                                Dim idDocumentoItem As Integer = 0
                                For Each documentItem As Generic.DocumentFile In FolderItem
                                    If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then idDocumentoItem += 1

                                    ''20210225, se debe obtener el idExpediente y el peso de las imágenes para evaluar el volumen
                                    'Dim Peso As Long = 0
                                    'Dim idExpediente As Long = GetidExpediente(FolderItem, resultadoDataTable, documentItem, idFolderItem, idDocumentoItem, Peso)

                                    'manager.EvaluateVolumen(idExpediente, Peso, CType(documentItem.Count, Short))

                                    Dim idFolioItem As Integer = 0
                                    For Each folioItem As Generic.Folio In documentItem
                                        If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then idFolioItem += 1

                                        ' Preparar data de la imagen
                                        ImageManager.Save(folioItem.ThumbnailImage, Program.AppPath & Program.TempPath & "temp" & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", formato, compresion, False, Program.AppPath & Program.TempPath)

                                        Dim dataImage = ImageManager.GetData(folioItem.FileName)
                                        Dim dataImageThumbnail = ImageManager.GetThumbnailData(Program.AppPath & Program.TempPath & "temp" & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)

                                        ' Actualizar estado folio cargue
                                        'dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBUpdate(FolioType, PaqueteRow.fk_Cargue, PaqueteRow.id_Cargue_Paquete, FolioItem.Cargue_Item, FolioItem.Id_Item_Folio)
                                        dbmImaging.SchemaProcess.PA_Set_Cargue_Folio_Indexado.DBExecute(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete, folioItem.Cargue_Item, folioItem.Id_Item_Folio)

                                        Dim filaFiltro As DBImaging.SchemaProcess.TBL_Publicar_IndexacionRow

                                        ' Si es un File
                                        If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then
                                            filaFiltro = resultadoDataTable.FindByEs_Anexoid_Folder_Itemid_Documento_Itemid_Folio_Item(False, idFolderItem, CShort(idDocumentoItem), CShort(idFolioItem))
                                            manager.CreateItem(filaFiltro.fk_Expediente, filaFiltro.fk_Folder, filaFiltro.fk_File, filaFiltro.fk_Version, Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, Guid.NewGuid())
                                            manager.CreateFolio(filaFiltro.fk_Expediente, filaFiltro.fk_Folder, filaFiltro.fk_File, filaFiltro.fk_Version, filaFiltro.id_File_Record_Folio, dataImage, dataImageThumbnail(0), False)
                                        End If

                                        ' Si es un Anexo
                                        filaFiltro = resultadoDataTable.FindByEs_Anexoid_Folder_Itemid_Documento_Itemid_Folio_Item(True, 0, CShort(folioItem.Cargue_Item), folioItem.Id_Item_Folio)
                                        If (filaFiltro IsNot Nothing) Then
                                            manager.CreateItem(filaFiltro.fk_Anexo, Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                                            manager.CreateFolio(filaFiltro.fk_Anexo, filaFiltro.id_File_Record_Folio, dataImage, dataImageThumbnail(0), False)
                                        End If
                                    Next
                                Next
                            Next
                            '-------------------------------------------------------------------------------------------------------------

                            'While dbmImaging.DataBase.BullkQueryLines > 0
                            '    dbmImaging.DataBase.SendBullkQuery(1000)
                            'End While

                            'dbmImaging.DataBase.EndBullkQuery()

                            progressForm.SetProgreso(2)
                            Application.DoEvents()

                            ' Actualizar Paquete
                            Dim paqueteType = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                            paqueteType.fk_Usuario_Log = Me.IndexerSesion.Usuario.id
                            paqueteType.Fecha_Proceso = SlygNullable.SysDate
                            paqueteType.Data_Path = paqueteNombre
                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(paqueteType, _paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)

                            ' Productividad
                            Dim productividad As New DesktopConfig.ProductividadType
                            productividad.Usuario = Me.IndexerSesion.Usuario.id
                            productividad.Documentos = documentos
                            InsertarProductividad(dbmImaging, productividad, DesktopConfig.Etapa_Productividad.Indexacion, _paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)

                            'dbmImaging.Transaction_Commit()
                            manager.TransactionCommit()

                            progressForm.SetProgreso(3)
                            Application.DoEvents()

                        Catch ex As Exception
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                            If (manager IsNot Nothing) Then manager.TransactionRollback()

                            progressForm.Hide()
                            Application.DoEvents()

                            DesktopMessageBoxControl.DesktopMessageShow("Publicar [1]", ex)

                            Return False
                        End Try

                        Try
                            dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                            Dim carguePaqueteDataTable = dbmImaging.SchemaProcess.PA_Get_Cargue_Folio_Indexado.DBExecute(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete, False)
                            'dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBFindByfk_Carguefk_Cargue_PaqueteIndexado(PaqueteRow.fk_Cargue, PaqueteRow.id_Cargue_Paquete, False)

                            '************SE COMENTA PORQUE SE CAMBIA LÓGICA****************************
                            ''20210225  Borrar los files
                            'For Each FolderItem In Folders
                            '    For Each documentItem As Generic.DocumentFile In FolderItem
                            '        For Each folioItem As Generic.Folio In documentItem
                            '            manager.DeleteItem(folioItem.Cargue, folioItem.Cargue_Paquete, folioItem.Cargue_Item)
                            '        Next
                            '    Next
                            'Next
                            '***************************************************************************

                            If (carguePaqueteDataTable.Count = 0) Then
                                '---------------------------------------------------------------------------D:\Proyectos\SLYG\Fuentes\PyC\Miharu\Miharu+ System - Imaging\Miharu.Imaging.Indexer\Controller\Recorte\
                                ' Actualizar Dashboard
                                '---------------------------------------------------------------------------
                                dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)
                                '---------------------------------------------------------------------------

                                Dim updatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                                updatePaquete.fk_Estado = DBCore.EstadoEnum.Indexado
                                dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(updatePaquete, _paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)
                                dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBDelete(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete, Nothing, Nothing)

                                Dim cargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBFindByfk_Carguefk_Estado(_paqueteRow.fk_Cargue, DBCore.EstadoEnum.Indexacion)
                                If (cargueDataTable.Count = 0) Then
                                    Dim updateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                                    updateCargue.fk_Estado = DBCore.EstadoEnum.Indexado
                                    dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(updateCargue, _paqueteRow.fk_Cargue)
                                End If
                            Else
                                '---------------------------------------------------------------------------
                                ' Actualizar Dashboard
                                '---------------------------------------------------------------------------
                                dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Paquetes.DBExecute(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)
                                '---------------------------------------------------------------------------
                            End If

                            ' Borrar los files, se mueve unas lienas arriba para que primero se elimine el registro en la BD y luego si en Físico
                            ' Borrar los files
                            For Each FolderItem In Folders
                                For Each documentItem As Generic.DocumentFile In FolderItem
                                    For Each folioItem As Generic.Folio In documentItem
                                        manager.DeleteItem(folioItem.Cargue, folioItem.Cargue_Paquete, folioItem.Cargue_Item)
                                    Next
                                Next
                            Next

                            dbmImaging.Transaction_Commit()

                        Catch ex As Exception
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                        End Try

                        '-------------------------------------------------------------------------------------------------------------
                        ' LOGGING - PERFORMANCE
                        '-------------------------------------------------------------------------------------------------------------
                        Dim fechaFinProceso = Now
                        Dim traceMessage As String = ""
                        traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
                        traceMessage &= "Cargue:" & vbTab & _cargueRow.id_Cargue & vbTab
                        traceMessage &= "Paquete:" & vbTab & _paqueteRow.id_Cargue_Paquete & vbTab
                        traceMessage &= "Items:" & vbTab & items & vbTab
                        DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Indexacion][Publicar]")
                        '-------------------------------------------------------------------------------------------------------------

                    Catch ex As Exception
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                        If (manager IsNot Nothing) Then manager.TransactionRollback()

                        progressForm.Hide()
                        Application.DoEvents()

                        DesktopMessageBoxControl.DesktopMessageShow("Publicar [2]", ex)

                        Return False
                    Finally
                        'If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        If (manager IsNot Nothing) Then manager.Disconnect()
                    End Try

                    'Validacion reconocimiento codigo de barras
                    If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Reconocimiento_CBarras Then
                        ReconocimientoBarCode()
                    End If

                    progressForm.Hide()

                    EventManager.FinalizarIndexacion(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)

                    Return True
                Catch ex As Exception
                    progressForm.Hide()
                    Application.DoEvents()

                    DesktopMessageBoxControl.DesktopMessageShow("Publicar [3]", ex)

                    Return False
                Finally
                    progressForm.Close()
                End Try
            End If

            Return False
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Private Function GetidExpediente(ByRef FolderItem As Generic.Folder, ByRef resultadoDataTable As DBImaging.SchemaProcess.TBL_Publicar_IndexacionDataTable, ByRef documentItem As Generic.DocumentFile, ByVal idFolderItem As Integer, idDocumentoItem As Integer, ByRef Peso As Long) As Long

        '    Dim idExpediente As Long = 0
        '    Peso = 0
        '    Try
        '        For Each folioItem As Generic.Folio In documentItem

        '            Dim dataImage = ImageManager.GetData(folioItem.FileName)

        '            Dim filaFiltro As DBImaging.SchemaProcess.TBL_Publicar_IndexacionRow

        '            ' Si es un File
        '            If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal And idExpediente = 0) Then
        '                filaFiltro = resultadoDataTable.FindByEs_Anexoid_Folder_Itemid_Documento_Itemid_Folio_Item(False, idFolderItem, CShort(idDocumentoItem), 1)
        '                idExpediente = filaFiltro.fk_Expediente
        '            End If

        '            Peso += dataImage.Length

        '        Next

        '    Catch ex As Exception
        '        Throw New Exception(ex.ToString)
        '    End Try

        '    Return idExpediente

        'End Function

        Public Sub ReconocimientoBarCode()

            Dim progressFormEsp As New Progress.FormProgress
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Try

                If (manager IsNot Nothing) Then manager.Disconnect()

                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Me.IndexerDesktopGlobal.ServidorImagenRow.fk_Entidad, Me.IndexerDesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType

                manager = New FileProviderManager(servidor, centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                manager.Connect()

                Dim DashBoardCapturasDt = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBFindByfk_Carguefk_Cargue_Paquete(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)
                Dim folio As Short
                Dim Expedientes As Integer = 0
                Dim TotalExpedientes As Integer = DashBoardCapturasDt.Count()
                Dim Folios As Short

                '-----------------------------------------------------------------------------------------------------------------

                progressFormEsp.Process = ""
                progressFormEsp.Action = ""
                progressFormEsp.ValueProcess = 0
                progressFormEsp.ValueAction = 0

                progressFormEsp.Show()

                Application.DoEvents()
                '-----------------------------------------------------------------------------------------------------------------

                Dim actualizarFolder As Boolean = True

                For Each CarguePaqueteFileRow As DBImaging.SchemaProcess.TBL_Dashboard_CapturasRow In DashBoardCapturasDt

                    '-----------------------------------------------------------------------------------------------------------------
                    Expedientes += 1
                    progressFormEsp.MaxValueProcess = TotalExpedientes
                    progressFormEsp.Process = "Procesar Expediente " & Expedientes & " de " & TotalExpedientes
                    progressFormEsp.ValueProcess = Expedientes

                    Dim fileImagingDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(CarguePaqueteFileRow.fk_Expediente, CarguePaqueteFileRow.fk_Folder, CarguePaqueteFileRow.fk_File, Nothing)

                    Dim FileImagingRow = fileImagingDataTable(fileImagingDataTable.Count - 1)

                    Dim Filefk_Documento = CarguePaqueteFileRow.fk_Documento

                    Dim CBarrasCampoDocumento = dbmImaging.SchemaConfig.TBL_CBarras_Campo.DBFindByfk_Documento(Filefk_Documento)
                    Dim TipoCBarrasDocumento = dbmImaging.SchemaConfig.TBL_TipoCodigoBarras_Documento.DBFindByfk_Documento(Filefk_Documento)

                    Dim Campos_Documento = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documentoid_Campo(Me.IndexerImagingGlobal.Entidad, Filefk_Documento, Nothing)

                    Dim CamposLLaveDocumento = dbmCore.SchemaConfig.TBL_Campo_Llave.DBFindByfk_DocumentoEliminado_Campo(Filefk_Documento, False)

                    If ((Campos_Documento.Count > 0) Or (Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave = True And CamposLLaveDocumento.Count > 0)) Then

                        If CBarrasCampoDocumento.Rows.Count > 0 And TipoCBarrasDocumento.Rows.Count > 0 Then

                            Folios = manager.GetFolios(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                            '-----------------------------------------------------------------------------------------------------------------
                            progressFormEsp.ValueAction = 0
                            progressFormEsp.MaxValueAction = Folios
                            progressFormEsp.BringToFront()
                            Application.DoEvents()

                            Dim Codigos() As String = Nothing

                            For folio = CShort(1) To Folios

                                Dim imagen() As Byte = Nothing
                                Dim thumbnail() As Byte = Nothing

                                ' Leer folio actual
                                manager.GetFolio(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, folio, imagen, thumbnail)

                                Dim ms = New MemoryStream(imagen)
                                Dim Img = New System.Drawing.Bitmap(ms)

                                'Reconocimiento
                                Dim codigoBarras As String = TipoCBarrasDocumento.Rows(0).Item("TipoCBarras").ToString

                                Select Case codigoBarras
                                    Case "CODE128"
                                        Codigos = BarcodeReader.read(Img, BarcodeReader.CODE128)
                                    Case "CODE39"
                                        Codigos = BarcodeReader.read(Img, BarcodeReader.CODE39)
                                    Case "CODE39EX"
                                        Codigos = BarcodeReader.read(Img, BarcodeReader.CODE39EX)
                                    Case "EAN8"
                                        Codigos = BarcodeReader.read(Img, BarcodeReader.EAN8)
                                    Case "EAN13"
                                        Codigos = BarcodeReader.read(Img, BarcodeReader.EAN13)
                                    Case "QRCODE"
                                        Codigos = BarcodeReader.read(Img, BarcodeReader.QRCODE)
                                    Case "CODABAR"
                                        Codigos = BarcodeReader.read(Img, BarcodeReader.CODABAR)
                                End Select

                                If Codigos IsNot Nothing Then

                                    For Each codigo In Codigos
                                        If codigo.Length = TipoCBarrasDocumento(0).LongitudIdentificador Then
                                            'Validacion sobre BarcodeReader
                                            Dim strCodigos() As String = Nothing

                                            strCodigos = Codigos(0).Split(CChar(TipoCBarrasDocumento.Rows(0).Item("Separador").ToString))
                                            For Each campo As DataRow In Campos_Documento
                                                Try
                                                    Dim ValueRow As DataRow()
                                                    ValueRow = CBarrasCampoDocumento.Select("fk_Campo = " & CShort(campo.Item("id_Campo")))
                                                    'Dim ConfigCamposDataTable As DBCore.SchemaConfig.TBL_CampoRow
                                                    Dim ConfigCamposDataTable = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documentoid_Campo(_cargueRow.fk_Entidad, Filefk_Documento, CShort(campo.Item("id_Campo")))(0) '.Rows(0), DBCore.SchemaConfig.TBL_CampoRow)

                                                    If ValueRow.Count > 0 Then
                                                        Dim Value As String
                                                        Value = strCodigos(CInt(ValueRow(0).Item("PosicionCBarras")))
                                                        If Value <> "" And Value IsNot Nothing Then
                                                            Dim PosIni = CInt(ValueRow(0).Item("PosicionInicia"))
                                                            Dim Longitud = CInt(ValueRow(0).Item("Longitud"))
                                                            Dim TipoCampo = CStr(ValueRow(0).Item("TipoCampo"))
                                                            Dim Formato = ValueRow(0).Item("Formato").ToString()
                                                            Dim CBActivo = CBool(ValueRow(0).Item("Activo"))
                                                            Dim CaracterDecimal As String = CStr(IIf(ConfigCamposDataTable.IsCaracter_DecimalNull(), "", ConfigCamposDataTable.Caracter_Decimal.ToString()))
                                                            Dim CantidadDecimales As Integer = CType(ConfigCamposDataTable.Cantidad_Decimales, Integer)

                                                            Value = SetValueFileData(Value.Substring(PosIni, Longitud), TipoCampo, Formato, CaracterDecimal, CantidadDecimales)

                                                            If CBActivo Then
                                                                Dim FileDataType As New DBCore.SchemaProcess.TBL_File_DataType()

                                                                If dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, Filefk_Documento, CShort(campo.Item("id_Campo"))).Count = 0 Then
                                                                    FileDataType.fk_Expediente = FileImagingRow.fk_Expediente
                                                                    FileDataType.fk_Folder = FileImagingRow.fk_Folder
                                                                    FileDataType.fk_File = FileImagingRow.fk_File
                                                                    FileDataType.fk_Documento = Filefk_Documento
                                                                    FileDataType.fk_Campo = CShort(campo.Item("id_Campo"))
                                                                    FileDataType.Valor_File_Data = Value
                                                                    FileDataType.Conteo_File_Data = Value.Length
                                                                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(FileDataType)
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                Catch
                                                End Try
                                            Next

                                            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                                                For Each campo As DataRow In CamposLLaveDocumento
                                                    Try
                                                        Dim ValueRow As DataRow()
                                                        ValueRow = CBarrasCampoDocumento.Select("fk_Campo = " & CShort(campo.Item("Numero_Llave")))
                                                        Dim ConfigCamposDataTable As DBCore.SchemaConfig.TBL_CampoRow
                                                        ConfigCamposDataTable = CType(dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documentoid_Campo(_cargueRow.fk_Entidad, Filefk_Documento, CShort(campo.Item("id_Campo"))).Rows(0), DBCore.SchemaConfig.TBL_CampoRow)

                                                        If ValueRow.Count > 0 Then
                                                            Dim Value As String
                                                            Value = strCodigos(CInt(ValueRow(0).Item("PosicionCBarras")))
                                                            If Value <> "" And Value IsNot Nothing Then
                                                                Dim PosIni = CInt(ValueRow(0).Item("PosicionInicia"))
                                                                Dim Longitud = CInt(ValueRow(0).Item("Longitud"))
                                                                Dim TipoCampo = CStr(ValueRow(0).Item("TipoCampo"))
                                                                Dim Formato = ValueRow(0).Item("Formato").ToString()
                                                                Dim CBActivo = CBool(ValueRow(0).Item("Activo"))


                                                                Value = SetValueFileData(Value.Substring(PosIni, Longitud), TipoCampo, Formato, "", 0)

                                                                If CBActivo Then
                                                                    Dim ExpedienteLlaveLinea As New DBCore.SchemaProcess.TBL_Expediente_Llave_LineaType()

                                                                    Dim LlavesDataTableExpediente = dbmCore.SchemaProcess.TBL_Expediente_Llave_Linea.DBFindByfk_Expediente(FileImagingRow.fk_Expediente)
                                                                    If LlavesDataTableExpediente.Count = 0 Then
                                                                        ExpedienteLlaveLinea.fk_Expediente = FileImagingRow.fk_Expediente
                                                                        ExpedienteLlaveLinea.fk_Entidad = CamposLLaveDocumento(0).fk_Entidad
                                                                        ExpedienteLlaveLinea.fk_Proyecto = CamposLLaveDocumento(0).fk_Proyecto
                                                                        ExpedienteLlaveLinea.fk_Esquema = CamposLLaveDocumento(0).fk_Esquema

                                                                        If CShort(campo.Item("Numero_Llave")) = 3 Then
                                                                            ExpedienteLlaveLinea.Campo_Empaque = Value
                                                                        End If
                                                                    Else
                                                                        If CShort(campo.Item("Numero_Llave")) = 3 Then
                                                                            ExpedienteLlaveLinea.Campo_Empaque = Value
                                                                        End If

                                                                        dbmCore.SchemaProcess.TBL_Expediente_Llave_Linea.DBUpdate(ExpedienteLlaveLinea, FileImagingRow.fk_Expediente)
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    Catch
                                                    End Try
                                                Next
                                            End If
                                        End If
                                    Next
                                End If
                                progressFormEsp.Action = "Leer Codigo de Barras en Imagen " & folio & " de " & Folios
                                progressFormEsp.ValueAction = folio
                                Application.DoEvents()
                            Next
                        End If
                    End If
                    'TODO Calcular el siguiente estado
                    ' Actualizar estados
                    Dim NextEstado = dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(CarguePaqueteFileRow.fk_Documento, DBCore.EstadoEnum.Captura)
                Next

            Catch ex As Exception
                progressFormEsp.Hide()
                Application.DoEvents()

                DesktopMessageBoxControl.DesktopMessageShow("Se ha presentado un error al procesar la lectura de codigo de barras!. ", ex)
            Finally
                progressFormEsp.Close()
            End Try

        End Sub

        Public Function SetValueFileData(ByVal Value As String, ByVal TipoCampo As String, Optional ByVal Formato As String = "", Optional ByVal CaracterSepDecimal As String = "", Optional ByVal CantidadDecimales As Integer = 0) As String
            Try
                If Value <> Nothing And Value <> "" Then
                    Select Case TipoCampo
                        Case "DEC" 'Decimal
                            If IsNumeric(Value) Then
                                Value = CStr(Convert.ToDecimal(Value))
                                If CaracterSepDecimal <> "" And CantidadDecimales > 0 Then
                                    Value = Value.Substring(0, Len(Value) - CantidadDecimales) & CaracterSepDecimal & Value.Substring(Len(Value) - CantidadDecimales, CantidadDecimales)
                                End If
                            End If
                        Case "NUM" 'Numeric
                            If IsNumeric(Value) Then
                                Value = CLng(Value).ToString
                            End If
                            Return Value
                        Case Else
                            Return Value
                    End Select
                End If
            Catch ex As Exception
                Return Value
            End Try
            Return Value
        End Function

        Public Overrides Function ValidacionListas() As Boolean
            Dim _ValidacionListas As Boolean

            _ValidacionListas = False

            Return _ValidacionListas
        End Function

        Public Overrides Function DesindexarFolio() As Boolean
            If (_CurrentImageIndex >= 0) Then
                _CurrentImageIndex -= 1
            End If

            Dim oldFolio = Me.CurrentFolio

            If (_CurrentFolioIndex > 0) Then
                Me.CurrentDocumentFile.Remove(Me.CurrentFolio)

                _CurrentFolioIndex -= 1

            ElseIf (_CurrentDocumentFileIndex > 0) Then
                Me.CurrentFolder.Remove(Me.CurrentDocumentFile)

                _CurrentDocumentFileIndex -= 1
                _CurrentFolioIndex = Me.CurrentDocumentFile.Count - 1

            ElseIf (_CurrentFolderIndex > 0) Then
                View.ThumbnailPanel.Controls.Remove(CurrentFolder.Panel)
                Me.Folders.Remove(Me.CurrentFolder)

                _CurrentFolderIndex = Me._Folders.Count - 1
                _CurrentDocumentFileIndex = Me.CurrentFolder.Count - 1
                _CurrentFolioIndex = Me.CurrentDocumentFile.Count - 1
            End If

            oldFolio.Dispose()

            Return True
        End Function

        Public Overrides Function NextFolio(ByRef nUpdateInfo As Boolean) As Boolean
            Try
                If (Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Indexacion_Parcial And Me.Folios Mod 100 = 0) Then
                    MessageBox.Show("Se ha completado el número de imagenes recomendado para hacer indexación parcial. Recuerde que puede hacerlo en este momento", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                nUpdateInfo = False

                If (CurrentFolio IsNot Nothing) Then CurrentFolio.Unselect()

                If _CurrentImageIndex + 1 < Me.Folios Then
                    SaveFolio()

                    _CurrentImageIndex += 1

                    If (_CurrentFolioIndex + 1 < CurrentDocumentFile.Count) Then
                        _CurrentFolioIndex += 1
                    ElseIf (_CurrentDocumentFileIndex + 1 < CurrentFolder.Count) Then
                        _CurrentDocumentFileIndex += 1
                        _CurrentFolioIndex = 0
                        nUpdateInfo = True
                    Else
                        _CurrentFolderIndex += 1
                        _CurrentDocumentFileIndex = 0
                        _CurrentFolioIndex = 0
                        nUpdateInfo = True
                    End If

                    Me.View.ScrollThumbnail()

                    CurrentFolio.Select()

                    Return True
                ElseIf _CurrentImageIndex + 1 < Me.ImageCount Then
                    SaveFolio()

                    _CurrentImageIndex += 1

                    Dim newFolio = CurrentDocumentFile.NewFolio(True)
                    CurrentDocumentFile.Add(newFolio)
                    InicializarFolio(newFolio)
                    _CurrentFolioIndex += 1

                    Me.View.ScrollThumbnail()

                    CurrentFolio.Select()

                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Function

        Public Overrides Function PreviousFolio(ByRef nUpdateInfo As Boolean) As Boolean
            nUpdateInfo = False

            If Validar() Then
                SaveDocument()

                If (CurrentFolio IsNot Nothing) Then CurrentFolio.Unselect()

                If (_CurrentImageIndex >= 0) Then
                    _CurrentImageIndex -= 1

                    If (_CurrentFolioIndex > 0) Then
                        _CurrentFolioIndex -= 1
                    ElseIf (_CurrentDocumentFileIndex > 0) Then
                        _CurrentDocumentFileIndex -= 1
                        _CurrentFolioIndex = CurrentDocumentFile.Count - 1
                        nUpdateInfo = True
                    Else
                        _CurrentFolderIndex -= 1
                        _CurrentDocumentFileIndex = CurrentFolder.Count - 1
                        _CurrentFolioIndex = CurrentDocumentFile.Count - 1
                        nUpdateInfo = True
                    End If

                    CurrentFolio.Select()

                    Return True
                Else
                    CurrentFolio.Select()
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        Public Overrides Sub Unlock()
            If (Not _paqueteRow Is Nothing) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                    dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Paquetes.DBExecute(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)
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

            Return _Validaciones
        End Function

        Public Overrides Function NextIndexingElement(ByVal ot As Integer, ByVal nEstado As DBCore.EstadoEnum, ByVal nIdDocumento As SlygNullable(Of Integer)) As Boolean
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim fechaInicioProceso = Now

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                ' Mirar si existe un registro previamente asignado
                Dim bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Paquetes_get.DBExecute(Me.IndexerDesktopGlobal.SesionId)
                If bloqueoDatatable.Count > 0 Then
                    For Each BloqueoRow In bloqueoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Paquetes.DBExecute(BloqueoRow.fk_Cargue, BloqueoRow.fk_Cargue_Paquete)
                    Next
                End If

                ' Calcular siguiente imagen disponible
                Dim bloqueados = dbmImaging.SchemaProcess.PA_Bloqueo_Paquete_Next.DBExecute(ot, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.SesionId, Me.IndexerDesktopGlobal.PcName, DBCore.EstadoEnum.Indexacion)
                If (Not bloqueados) Then Return False

                bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Paquetes_get.DBExecute(Me.IndexerDesktopGlobal.SesionId)
                If bloqueoDatatable.Count > 0 Then
                    Dim paqueteDataTable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBGet(bloqueoDatatable(0).fk_Cargue, bloqueoDatatable(0).fk_Cargue_Paquete)
                    _paqueteRow = paqueteDataTable(0)

                    Dim cargeDataTable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(_paqueteRow.fk_Cargue)
                    _cargueRow = cargeDataTable(0)

                    Me._centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                    ' Leer el listado de imagenes
                    Me._foliosDataTable = dbmImaging.SchemaProcess.PA_Get_Cargue_Folio_Indexado.DBExecute(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete, False)

                    _dataItemIndex = New List(Of Integer)

                    For i As Integer = 0 To _foliosDataTable.Count - 1
                        _dataItemIndex.Add(i)
                    Next

                    IndexerView.Information = ""
                    LoadConfig(dbmCore, dbmImaging, _cargueRow.fk_Entidad, _cargueRow.fk_Proyecto)
                Else
                    Return False
                End If

            Catch ex As Exception
                Throw
            Finally
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

            _ImageCount = _foliosDataTable.Count

            If (_ImageCount = 0) Then
                Dim mensaje = "No se encontraron folios asociados al Paquete, por favor comuniquese con el administrador del sistema." & vbCrLf
                mensaje &= "[Cargue]: " & _paqueteRow.fk_Cargue & vbCrLf
                mensaje &= "[Paquete]: " & _paqueteRow.id_Cargue_Paquete & vbCrLf

                Throw New Exception(mensaje)
            End If

            ' Crear data inicial
            Dim newFolder = New Generic.Folder(Me)
            Folders.Add(newFolder)

            Dim newDocumento = newFolder.NewDocumentFile()
            newFolder.Add(newDocumento)

            Dim newFolio = newDocumento.NewFolio(True)

            newDocumento.Add(newFolio)
            InicializarFolio(newFolio)

            View.ThumbnailPanel.Controls.Add(newFolder.Panel)

            View.SetTitle("Indexación [Cargue: " & _cargueRow.id_Cargue & " - Paquete: " & _paqueteRow.id_Cargue_Paquete & " - CP: " & _cargueRow.Observaciones & "]")

            '-------------------------------------------------------------------------------------------------------------
            ' LOGGING - PERFORMANCE
            '-------------------------------------------------------------------------------------------------------------
            Dim fechaFinProceso = Now

            Dim traceMessage As String = ""
            traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
            traceMessage &= "Cargue:" & vbTab & _cargueRow.id_Cargue & vbTab
            traceMessage &= "Paquete:" & vbTab & _paqueteRow.id_Cargue_Paquete & vbTab
            DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Indexar][NextIndexingElement]")
            '-------------------------------------------------------------------------------------------------------------

            Return True
        End Function

        Public Overrides Function Indexar() As DialogResult
            View.Unlock = True

            _Ciclo += 1

            View.ActivarControles(True)
            Return View.ShowDialog()
        End Function

        Public Overrides Function Validar() As Boolean
            If (IndexerView.TipoDocumental_Value Is Nothing) Then
                MessageBox.Show("Se debe seleccionar la tipología documental para todos los documentos. Folder: " & CurrentFolder.Index + 1 & ", Documento: " & CurrentDocumentFile.Index + 1, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                'MessageBox.Show("Se debe seleccionar una tipología documental", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                ' Validar que exista almenos un folder que no sea anexo
                Dim existeNoAnexo As Boolean = False
                Dim existeAnexo As Boolean = False

                For Each FolderItem In Folders
                    If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then
                        existeNoAnexo = True
                    Else
                        existeAnexo = True
                    End If
                Next

                If (existeAnexo Or Me.IndexerView.AnexoCompleto) Then
                    For Each FolderItem In Folders
                        'If (FolderItem.Modo = Generic.Folder.FolderModoEnum.Normal) Then
                        Dim anexoDataTable = DocumentoIndexacionDataTable.Select("fk_Esquema = " & FolderItem.Esquema & " AND Es_Anexo = 1")

                        If (anexoDataTable Is Nothing OrElse anexoDataTable.Length = 0) Then
                            MessageBox.Show("El esquema: " & FolderItem.Esquema & " no tiene definido un documento como anexo", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

                            Return False
                        End If

                        FolderItem.IdAnexo = CType(anexoDataTable(0), DBImaging.SchemaConfig.CTA_Documento_IndexacionRow).id_Documento
                        'Else
                        'FolderItem.IdAnexo = 0
                        'End If
                    Next
                End If

                If (Not existeNoAnexo) Then
                    MessageBox.Show("No se pueden definir todos los folder como anexos", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ElseIf (ValidarFolder(CurrentFolder)) Then
                    Return True
                End If
            End If

            Return False
        End Function

        Public Overrides Function SetCurrentFolio(ByVal value As Generic.Folio, ByRef nUpdateInfo As Boolean) As Boolean
            nUpdateInfo = False

            If Validar() Then
                SaveDocument()

                If (CurrentFolio IsNot Nothing) Then CurrentFolio.Unselect()

                _CurrentFolderIndex = value.Parent.Parent.Index
                _CurrentDocumentFileIndex = value.Parent.Index
                _CurrentFolioIndex = value.Index
                _CurrentImageIndex = value.GlobalIndex

                CurrentFolio.Select()

                nUpdateInfo = True
                Return True
            Else
                Return False
            End If
        End Function

        Public Overrides Sub Move(ByVal nSourceFolio As Generic.Folio, ByVal nTargetFolio As Generic.Folio, ByVal nTargetIndex As Integer)
            Dim sourceDocument = nSourceFolio.Parent
            Dim sourceFolder = sourceDocument.Parent

            ' Mover los indices
            Dim actual = Me._dataItemIndex(nSourceFolio.GlobalIndex)
            Me._dataItemIndex.RemoveAt(nSourceFolio.GlobalIndex)

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

            ' Finalizar movimiento de indices
            Me._dataItemIndex.Insert(nSourceFolio.GlobalIndex, actual)
        End Sub

        Public Overrides Sub AutoIndexar(ByVal nDocumento As Integer, ByVal nFolios As Integer, ByVal nModo As ModoAutoIndexarEnum)
            Dim crearFolder As Boolean = True

            Do
                If (nModo = ModoAutoIndexarEnum.Folder Or crearFolder) Then
                    If (Not NewFolder()) Then Return

                    crearFolder = False
                Else
                    If (Not NewDocumentFile()) Then Return
                End If

                If (Me.IndexerView.CancelProcess) Then Return

                ' Completar los folios del documento actual
                For i = 2 To nFolios
                    If (_CurrentImageIndex + 1 < Me.ImageCount) Then
                        NextFolio(False)

                        Me.View.UpdateAvance()
                        Me.View.UpdateNombreImagen()
                    Else
                        Exit For
                    End If

                    If (Me.IndexerView.CancelProcess) Then Return
                Next

                ' Pedir el siguiente folio
                If (_CurrentImageIndex + 1 < Me.ImageCount) Then
                    NextFolio(False)

                    Me.View.UpdateAvance()
                    Me.View.UpdateNombreImagen()
                End If
            Loop While (Me.CurrentDocumentFile.Folios > nFolios)
        End Sub

        Public Overrides Function MotivosReproceso() As List(Of Item)
            Return New List(Of Item)
        End Function

#End Region

#Region " Metodos "

        Private Sub LoadConfig(ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short)
            EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, Nothing)
            'DocumentoDataTable = dbmImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(nEntidad, nProyecto, Nothing, False)
            DocumentoIndexacionDataTable = dbmImaging.SchemaConfig.CTA_Documento_Indexacion.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(nEntidad, nProyecto, Nothing, False)

            'Carga campos que son llaves.
            '_LlavesEsquema = ndbmCore.SchemaConfig.TBL_Esquema_Llave.DBFindByfk_Entidadfk_Proyectofk_Esquemafk_Proyecto_Llave(nEntidad, nProyecto, Nothing, Nothing)

            IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
            IndexerView.TipoDocumental_DataSource = New DataView(DocumentoIndexacionDataTable)
            'IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)
        End Sub

        Private Sub InicializarFolio(ByRef nFolio As Generic.Folio)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Try
                If (_foliosDataTable.Count > 0) Then
                    Dim folioActual = _foliosDataTable(Me._dataItemIndex(nFolio.GlobalIndex))
                    Dim imagen() As Byte = Nothing
                    Dim thumbnail() As Byte = Nothing

                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                    manager = New FileProviderManager(_cargueRow.id_Cargue, _centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                    manager.Connect()

                    manager.GetFolio(folioActual.fk_Cargue, folioActual.fk_Cargue_Paquete, folioActual.fk_Cargue_Item, folioActual.id_Folio, imagen, thumbnail)

                    nFolio.FileName = _TempPath & Guid.NewGuid().ToString() & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    nFolio.ThumbnailImage = New FreeImageAPI.FreeImageBitmap(New MemoryStream(thumbnail))

                    nFolio.Cargue = folioActual.fk_Cargue
                    nFolio.Cargue_Paquete = folioActual.fk_Cargue_Paquete
                    nFolio.Cargue_Item = folioActual.fk_Cargue_Item
                    nFolio.Id_Item_Folio = folioActual.id_Folio
                    nFolio.NombreImagen = folioActual.NombreImagen

                    Using fileImage = New FileStream(nFolio.FileName, FileMode.Create)
                        fileImage.Write(imagen, 0, imagen.Length)
                        fileImage.Close()
                    End Using
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


        Public Sub SaveDocument()
            ' Actualizar esquema del folder
            If (CurrentFolder IsNot Nothing) Then CurrentFolder.Esquema = IndexerView.Esquema_Value

            ' Almacenar Documento
            If (CurrentDocumentFile IsNot Nothing AndAlso IndexerView.TipoDocumental_Value.HasValue) Then
                CurrentDocumentFile.TipoDocumento = IndexerView.TipoDocumental_Value.Value
                CurrentDocumentFile.NombreTipoDocumento = IndexerView.TipoDocumental_Text

                ' Salvar campos
                CurrentDocumentFile.Campos.Clear()

                ' Salvar validaciones
                CurrentDocumentFile.Validaciones.Clear()
            End If
        End Sub

        Public Sub InsertarProductividad(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nProductividad As DesktopConfig.ProductividadType, ByVal nEtapa As DesktopConfig.Etapa_Productividad, ByVal nCargue As Integer, ByVal nPaquete As Short)
            Try
                Dim hora = Now.Hour
                Dim esNocturno = Not (hora >= 6 And hora < 22)
                dbmImaging.SchemaProcess.PA_Insertar_Productividad.DBExecute(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, _cargueRow.fk_Entidad, _cargueRow.fk_Proyecto, _
                                                                              CShort(0), nProductividad.Usuario, nProductividad.Documentos, nProductividad.Campos, _
                                                                             nProductividad.Caracteres, nProductividad.Validaciones, nEtapa, esNocturno, nCargue, nPaquete, Nothing, Nothing, Nothing, Nothing)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Insertar Productividad", ex)
            End Try
        End Sub

#End Region

#Region " Funciones "

        ' ReSharper disable UnusedParameter.Local
        Private Function ValidarFolder(ByVal nFolder As Generic.Folder) As Boolean
            Return True
        End Function

#End Region

    End Class

End Namespace