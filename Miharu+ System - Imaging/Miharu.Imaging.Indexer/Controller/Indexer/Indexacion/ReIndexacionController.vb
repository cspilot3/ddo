Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Imaging.Indexer.Generic
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports System.Text

Namespace Controller.Indexer.Indexacion

    Public Class ReIndexacionController
        Inherits IndexerController

#Region " Declaraciones "

        Private Class FolioReIndex
            Public Property EsExterno() As Boolean
            Public Property Expediente() As Long
            Public Property Folder() As Short
            Public Property File() As Short
            Public Property Version() As Short
            Public Property Folio() As Short
            Public Property Filename() As String
        End Class

        Private _foliosTable As New List(Of FolioReIndex)

        Private _bloqueotable As DBImaging.SchemaProcess.TBL_Dashboard_CapturasDataTable

        Private _ot As Integer

        Private _firstFile As FolioReIndex

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

            IndexerView.ShowSaveButton(False)
            IndexerView.ShowAddFolioButton(True)
            IndexerView.ShowDeleteFolioButton(True)
            IndexerView.ShowNewFolderButton(Not Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Folder_Unico)
            IndexerView.ShowNewFileButton(Not Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_File_Unico)
            IndexerView.ShowNextButton(True)
            IndexerView.ShowReprocesoButton(False)

            IndexerView.ShowInformationPanel(False)
            IndexerView.ShowDataPanel(False)
            IndexerView.ShowValidationsPanel(False)
            IndexerView.ShowValidationsListasPanel(False)
            IndexerView.ShowToolTipData(False)
            IndexerView.ShowAutoIndexar(False)

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
                        Dim newCarpeta = New Folder(Me)
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
            Try
                'Se seleccionan las nuevas imágenes
                Dim imagenOpenFileDialog = New OpenFileDialog() With {
                    .Multiselect = False,
                    .Title = "Seleccionar la nueva imagen",
                    .Filter = "Imágenes (*" & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada & ")|*" & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Entrada & ""
                }

                Dim respuesta = imagenOpenFileDialog.ShowDialog()
                If respuesta = DialogResult.OK Then
                    Dim formato = Utilities.getEnumFormat(Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                    Dim compresion = Utilities.getEnumCompression(CType(Me.IndexerImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                    'Se crean los folios del file.
                    Dim totalFolios = CShort(ImageManager.GetFolios(imagenOpenFileDialog.FileName))
                    Dim fileName = _TempPath & Guid.NewGuid().ToString()

                    Dim lastFolio = CurrentDocumentFile.Folio(CurrentDocumentFile.Count - 1)

                    For folio = 1 To totalFolios
                        Dim imageBinary = ImageManager.GetFolioData(imagenOpenFileDialog.FileName, folio, formato, compresion)
                        Dim folioFilename = fileName & folio.ToString("000") & ".image"
                        Using imageFileStream = New FileStream(folioFilename, FileMode.Create)
                            imageFileStream.Write(imageBinary, 0, imageBinary.Length)
                            imageFileStream.Close()
                        End Using

                        _foliosTable.Insert(lastFolio.GlobalIndex + folio, New FolioReIndex() With {.EsExterno = True, .Filename = folioFilename})

                        Dim newFolio = CurrentDocumentFile.NewFolio(True)
                        CurrentDocumentFile.Add(newFolio)
                        newFolio.setIndex(lastFolio.GlobalIndex + folio)

                        InicializarFolio(newFolio)
                    Next

                    Renumerar()

                    _ImageCount += totalFolios
                End If

                Return True
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("AddFolio", ex)
                Return False
            End Try
        End Function

        Public Overrides Function DeleteFolio() As Boolean
            Try
                If (Me.Folios = 1) Then
                    MessageBox.Show("No se puede eliminar el último folio en pantalla", "Indexer", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If

                ' Apuntadores al folio actual
                Dim folderDelete = CurrentFolder
                Dim documentFileDelete = CurrentDocumentFile
                Dim folioDelete = CurrentFolio
                Dim index = CurrentFolio.GlobalIndex

                ' Seleccionar otro folio
                If (Not View.PreviousFolio()) Then
                    _CurrentFolderIndex = 0
                    _CurrentDocumentFileIndex = 0
                    _CurrentFolioIndex = 0
                End If

                ' Remover el folio
                _foliosTable.RemoveAt(index)
                documentFileDelete.Remove(folioDelete)
                If (documentFileDelete.Count = 0) Then folderDelete.Remove(documentFileDelete)
                If (folderDelete.Count = 0) Then
                    View.ThumbnailPanel.Controls.Remove(folderDelete.Panel)
                    Folders.Remove(folderDelete)
                End If

                Renumerar()

                _ImageCount -= 1

                Return True
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("DeleteFolio", ex)
                Return False
            End Try
        End Function

        Public Overrides Function Save() As Boolean
            SaveFolio()
            SaveDocument()

            If (Validar()) Then
                Dim progressForm As New FormProgress
                Dim manager As FileProviderManager = Nothing
                Dim formato = Utilities.getEnumFormat(Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                Dim compresion = Utilities.getEnumCompression(CType(Me.IndexerImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))
                Dim contador = 0

                Try
#If Not Debug Then
                    ProgressForm.Show()
#End If
                    progressForm.SetProceso("Indexar")
                    progressForm.CanCancel = False
                    progressForm.SetAccion("Procesar imágenes")
                    progressForm.SetProgreso(0)
                    progressForm.SetMaxValue(_foliosTable.Count)
                    Application.DoEvents()

                    Dim dbmCore As DBCore.DBCoreDataBaseManager
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Dim paqueteRow As DBImaging.SchemaProcess.TBL_Cargue_PaqueteRow

                    Try
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                        dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                        'dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)
                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Me.IndexerDesktopGlobal.ServidorImagenRow.fk_Entidad, Me.IndexerDesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType
                        Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede, Me.IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                        manager = New FileProviderManager(servidor, centro, dbmImaging, Me.IndexerSesion.Usuario.id)
                        manager.Connect()

                        ' Iniciar transacción
                        dbmImaging.Transaction_Begin()
                        manager.TransactionBegin()

                        'dbmCore.LinkDataBaseManager(dbmImaging)

                        ' Leer el paquete del primer File

                        Dim firstFolderRow = dbmCore.SchemaImaging.TBL_Folder.DBGet(_firstFile.Expediente, _firstFile.Folder)(0)
                        paqueteRow = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBGet(firstFolderRow.fk_Cargue, firstFolderRow.fk_Cargue_Paquete)(0)

                        ' Calcular documentos
                        Const idFolder As Short = 1
                        For Each FolderItem In Folders
                            ' Crear el expediente
                            Dim idExpediente = dbmCore.SchemaProcess.PA_Insertar_Expediente.DBExecute(IndexerImagingGlobal.Entidad, IndexerImagingGlobal.Proyecto, FolderItem.Esquema, CShort(1), CShort(1), CShort(1), Nothing)

                            ' Crear el folder
                            Dim folderProType As New DBCore.SchemaProcess.TBL_FolderType()
                            folderProType.fk_Expediente = idExpediente
                            folderProType.id_Folder = idFolder
                            folderProType.CBarras_Folder = ""
                            folderProType.Fecha_Inicial = SlygNullable.SysDate
                            folderProType.Fecha_Final = SlygNullable.SysDate
                            dbmCore.SchemaProcess.TBL_Folder.DBInsert(folderProType)

                            Dim folderImgType As New DBCore.SchemaImaging.TBL_FolderType()
                            folderImgType.fk_Expediente = idExpediente
                            folderImgType.fk_Folder = idFolder
                            folderImgType.fk_Entidad_Servidor = IndexerDesktopGlobal.ServidorImagenRow.fk_Entidad
                            folderImgType.fk_Servidor = IndexerDesktopGlobal.ServidorImagenRow.id_Servidor
                            folderImgType.Fecha_Creacion = SlygNullable.SysDate
                            folderImgType.Fecha_Transferencia = Nothing
                            folderImgType.En_Transferencia = False
                            folderImgType.fk_Entidad_Servidor_Transferencia = Nothing
                            folderImgType.fk_Servidor_Transferencia = Nothing
                            folderImgType.fk_Cargue = paqueteRow.fk_Cargue
                            folderImgType.fk_Cargue_Paquete = paqueteRow.id_Cargue_Paquete
                            dbmCore.SchemaImaging.TBL_Folder.DBInsert(folderImgType)

                            'Crea las llaves a partir del nombre del paquete
                            If (Me.IndexerImagingGlobal.ProyectoImagingRow.Captura_Llaves_Paquete) Then
                                Dim paqueteNombre = paqueteRow.Data_Path

                                For Each Llave In Me.IndexerImagingGlobal.ProyectoImagingLlaveDataTable
                                    If (Llave.Posicion_Inicial + Llave.Posicion_Longitud > paqueteNombre.Length) Then
                                        Dim mensaje As New StringBuilder()
                                        mensaje.AppendLine("El paquete que se está indexando tiene un nombre de carpeta que no cumple con la estructura de las llaves")
                                        mensaje.AppendLine("")
                                        mensaje.AppendLine("Cargue: " & paqueteRow.fk_Cargue)
                                        mensaje.AppendLine("Paquete: " & paqueteRow.id_Cargue_Paquete)
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

                                        Dim llaveType = New DBCore.SchemaProcess.TBL_Expediente_LlaveType()
                                        llaveType.fk_Expediente = idExpediente
                                        llaveType.fk_proyecto_Llave = Llave.Id_Proyecto_Llave_Paquete
                                        llaveType.fk_campo_tipo = Llave.fk_Campo_Tipo
                                        llaveType.Valor_Llave = valorLlave
                                        dbmCore.SchemaProcess.TBL_Expediente_Llave.DBInsert(llaveType)
                                    End If
                                Next
                            End If

                            ' Crear los file
                            Dim idFile As Short = 0
                            Const idVersion As Short = 1
                            For Each documentItem As DocumentFile In FolderItem
                                idFile += CShort(1)

                                Dim fileProType As New DBCore.SchemaProcess.TBL_FileType()
                                fileProType.fk_Expediente = idExpediente
                                fileProType.fk_Folder = idFolder
                                fileProType.id_File = idFile
                                fileProType.File_Unique_Identifier = Guid.NewGuid
                                fileProType.fk_Documento = documentItem.TipoDocumento
                                fileProType.Folios_File = CShort(documentItem.Folios)
                                fileProType.Monto_File = 0
                                fileProType.CBarras_File = idFile.ToString()
                                dbmCore.SchemaProcess.TBL_File.DBInsert(fileProType)

                                Dim fileImgType As New DBCore.SchemaImaging.TBL_FileType()
                                fileImgType.fk_Expediente = idExpediente
                                fileImgType.fk_Folder = idFolder
                                fileImgType.fk_File = idFile
                                fileImgType.id_Version = idVersion
                                fileImgType.File_Unique_Identifier = fileProType.File_Unique_Identifier
                                fileImgType.Folios_Documento_File = fileProType.Folios_File
                                fileImgType.Tamaño_Imagen_File = 0
                                fileImgType.Nombre_Imagen_File = ""
                                fileImgType.Key_Cargue_Item = ""
                                fileImgType.Save_FileName = ""
                                fileImgType.fk_Content_Type = IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                                fileImgType.fk_Usuario_Log = IndexerSesion.Usuario.id
                                fileImgType.Validaciones_Opcionales = False
                                fileImgType.Es_Anexo = False
                                fileImgType.fk_Anexo = Nothing
                                fileImgType.fk_Entidad_Servidor = IndexerDesktopGlobal.ServidorImagenRow.fk_Entidad
                                fileImgType.fk_Servidor = IndexerDesktopGlobal.ServidorImagenRow.id_Servidor
                                fileImgType.Fecha_Creacion = SlygNullable.SysDate
                                fileImgType.Fecha_Transferencia = Nothing
                                fileImgType.En_Transferencia = False
                                fileImgType.fk_Entidad_Servidor_Transferencia = Nothing
                                fileImgType.fk_Servidor_Transferencia = Nothing
                                dbmCore.SchemaImaging.TBL_File.DBInsert(fileImgType)

                                ' Calcular el siguiente Estado
                                Dim nextEstado = dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(documentItem.TipoDocumento, DBCore.EstadoEnum.Indexacion)

                                ' Crear el Estado
                                Dim fileEstadoType As New DBCore.SchemaProcess.TBL_File_EstadoType()
                                fileEstadoType.fk_Expediente = fileImgType.fk_Expediente
                                fileEstadoType.fk_Folder = fileImgType.fk_Folder
                                fileEstadoType.fk_File = fileImgType.fk_File
                                fileEstadoType.Modulo = DesktopConfig.Modulo.Imaging
                                fileEstadoType.fk_Estado = nextEstado
                                fileEstadoType.fk_Usuario = IndexerSesion.Usuario.id
                                fileEstadoType.Fecha_Log = SlygNullable.SysDate
                                dbmCore.SchemaProcess.TBL_File_Estado.DBInsert(fileEstadoType)

                                If (nextEstado < DBCore.EstadoEnum.Indexado) Then
                                    ' Crear el File en Imaging
                                    Dim fileMcType As New DBImaging.SchemaProcess.TBL_FileType()
                                    fileMcType.fk_Expediente = fileImgType.fk_Expediente
                                    fileMcType.fk_Folder = fileImgType.fk_Folder
                                    fileMcType.fk_File = fileImgType.fk_File
                                    fileMcType.id_Version = fileImgType.id_Version
                                    fileMcType.fk_Reproceso = Nothing
                                    fileMcType.fk_Reproceso_Motivo = Nothing
                                    fileMcType.Actualizado = False
                                    fileMcType.Reprocesado = False
                                    fileMcType.Fecha_Reproceso = Nothing
                                    fileMcType.fk_Documento = documentItem.TipoDocumento
                                    fileMcType.Usuario_Primera_Captura = Nothing
                                    fileMcType.Fecha_Primera_Captura = Nothing
                                    fileMcType.Usuario_Segunda_Captura = Nothing
                                    fileMcType.Fecha_Segunda_Captura = Nothing
                                    fileMcType.Usuario_Tercera_Captura = Nothing
                                    fileMcType.Fecha_Tercera_Captura = Nothing
                                    fileMcType.Usuario_Calidad = Nothing
                                    fileMcType.Fecha_Calidad = Nothing
                                    dbmImaging.SchemaProcess.TBL_File.DBInsert(fileMcType)

                                    ' Crear el acceso en DashBoard
                                    ' Captura
                                    If (nextEstado < DBCore.EstadoEnum.Indexado) Then
                                        Dim dasBoardCapturaType As New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                                        dasBoardCapturaType.fk_Expediente = fileImgType.fk_Expediente
                                        dasBoardCapturaType.fk_Folder = fileImgType.fk_Folder
                                        dasBoardCapturaType.fk_File = fileImgType.fk_File
                                        dasBoardCapturaType.fk_Documento = documentItem.TipoDocumento
                                        dasBoardCapturaType.fk_Cargue = paqueteRow.fk_Cargue
                                        dasBoardCapturaType.fk_Cargue_Paquete = paqueteRow.id_Cargue_Paquete
                                        dasBoardCapturaType.fk_Entidad_Procesamiento = IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad
                                        dasBoardCapturaType.fk_Sede_Procesamiento = IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede
                                        dasBoardCapturaType.fk_Centro_Procesamiento = IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                                        dasBoardCapturaType.fk_Entidad = IndexerImagingGlobal.Entidad
                                        dasBoardCapturaType.fk_Proyecto = IndexerImagingGlobal.Proyecto
                                        dasBoardCapturaType.fk_Estado = nextEstado
                                        dasBoardCapturaType.fk_Usuario_log = Nothing
                                        dasBoardCapturaType.Sesion = Nothing
                                        dasBoardCapturaType.PCName = Nothing
                                        dasBoardCapturaType.fk_OT = Me._ot

                                        dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBInsert(dasBoardCapturaType)
                                    End If

                                    ' Validaciones
                                    Dim validacionesDataTable = dbmImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(documentItem.TipoDocumento, DBImaging.EnumEtapaCaptura.Opcional, 1, New DBImaging.SchemaConfig.CTA_ValidacionEnumList(DBImaging.SchemaConfig.CTA_ValidacionEnum.fk_Documento, True))

                                    If (validacionesDataTable.Count > 1) Then
                                        Dim dasBoardValidacionesType As New DBImaging.SchemaProcess.TBL_Dashboard_ValidacionesType()
                                        dasBoardValidacionesType.fk_Expediente = fileImgType.fk_Expediente
                                        dasBoardValidacionesType.fk_Folder = fileImgType.fk_Folder
                                        dasBoardValidacionesType.fk_File = fileImgType.fk_File
                                        dasBoardValidacionesType.fk_Documento = documentItem.TipoDocumento
                                        dasBoardValidacionesType.fk_Cargue = paqueteRow.fk_Cargue
                                        dasBoardValidacionesType.fk_Cargue_Paquete = paqueteRow.id_Cargue_Paquete
                                        dasBoardValidacionesType.fk_Entidad_Procesamiento = IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad
                                        dasBoardValidacionesType.fk_Sede_Procesamiento = IndexerDesktopGlobal.CentroProcesamientoRow.fk_Sede
                                        dasBoardValidacionesType.fk_Centro_Procesamiento = IndexerDesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento
                                        dasBoardValidacionesType.fk_Entidad = IndexerImagingGlobal.Entidad
                                        dasBoardValidacionesType.fk_Proyecto = IndexerImagingGlobal.Proyecto
                                        dasBoardValidacionesType.Procesado = False
                                        dasBoardValidacionesType.fk_Usuario_log = Nothing
                                        dasBoardValidacionesType.Sesion = Nothing
                                        dasBoardValidacionesType.PCName = Nothing
                                        dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBInsert(dasBoardValidacionesType)
                                    End If
                                End If

                                '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
                                ''20210225
                                'manager.EvaluateVolumen(idExpediente, GetSize(documentItem, formato, compresion), CType(documentItem.Count, Short))
                                '********************************************************************

                                manager.CreateItem(idExpediente, idFolder, idFile, idVersion, IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, fileProType.File_Unique_Identifier)

                                Dim idFolio As Short = 0
                                For Each folioItem As Folio In documentItem
                                    idFolio += CShort(1)

                                    Dim dataImage = ImageManager.GetFolioData(folioItem.FileName, 1, formato, compresion)
                                    Dim dataImageThumbnail = ImageManager.GetThumbnailData(folioItem.FileName, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)

                                    manager.CreateFolio(idExpediente, idFolder, idFile, idVersion, idFolio, dataImage, dataImageThumbnail(0), False)

                                    contador += 1
                                    progressForm.SetProgreso(contador)
                                    Application.DoEvents()
                                Next
                            Next
                        Next

                        ' Eliminar los Files iniciales
                        For Each dashboardRow In _bloqueotable
                            'Se llama el evento Para marcar imagenes como eliminadas
                            EventManager.EliminarImagen(dashboardRow.fk_Expediente, dashboardRow.fk_Folder, dashboardRow.fk_File)

                            ' Se elimina de  las tablas TBL_Dashboard_Capturas -TBL_Dashboard_Validaciones
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(dashboardRow.fk_Expediente, dashboardRow.fk_Folder, dashboardRow.fk_File)
                            dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBDelete(dashboardRow.fk_Expediente, dashboardRow.fk_Folder, dashboardRow.fk_File)

                            ' Se actualiza en la tabla de Core El estado del file en el modulo de Imagenes
                            Dim newFilesEstadoType As New DBCore.SchemaProcess.TBL_File_EstadoType()
                            newFilesEstadoType.fk_Estado = DBCore.EstadoEnum.Eliminado
                            newFilesEstadoType.fk_Usuario = IndexerSesion.Usuario.id
                            newFilesEstadoType.Fecha_Log = SlygNullable.SysDate

                            dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(newFilesEstadoType, dashboardRow.fk_Expediente, dashboardRow.fk_Folder, dashboardRow.fk_File, DesktopConfig.Modulo.Imaging)

                            'Marcar Folder como eliminado si todos los files estan eliminados
                            Dim FilesEliminadosDataTable = dbmCore.SchemaProcess.TBL_File_Estado.DBFindByfk_Expedientefk_FolderModulofk_Estado(dashboardRow.fk_Expediente, dashboardRow.fk_Folder, DesktopConfig.Modulo.Imaging, DBCore.EstadoEnum.Eliminado)
                            Dim FilesDataTable = dbmCore.SchemaProcess.TBL_File_Estado.DBFindByfk_Expedientefk_FolderModulo(dashboardRow.fk_Expediente, dashboardRow.fk_Folder, DesktopConfig.Modulo.Imaging)

                            If FilesEliminadosDataTable.Count = FilesDataTable.Count Then
                                ' Se actualiza en la tabla de Core El estado del folder en el modulo de Imagenes
                                Dim newFolderEstadoType As New DBCore.SchemaProcess.TBL_Folder_estadoType()
                                newFolderEstadoType.fk_Expediente = dashboardRow.fk_Expediente
                                newFolderEstadoType.fk_Folder = dashboardRow.fk_Folder
                                newFolderEstadoType.Modulo = DesktopConfig.Modulo.Imaging
                                newFolderEstadoType.fk_Estado = DBCore.EstadoEnum.Eliminado
                                newFolderEstadoType.fk_Usuario = IndexerSesion.Usuario.id
                                newFolderEstadoType.Fecha_Log = SlygNullable.SysDate

                                Dim FolderEstadoDataTable = dbmCore.SchemaProcess.TBL_Folder_estado.DBFindByfk_Expedientefk_FolderModulo(dashboardRow.fk_Expediente, dashboardRow.fk_Folder, DesktopConfig.Modulo.Imaging)

                                If FolderEstadoDataTable.Count = 0 Then
                                    dbmCore.SchemaProcess.TBL_Folder_estado.DBInsert(newFolderEstadoType)
                                ElseIf FolderEstadoDataTable.Count > 0 Then
                                    dbmCore.SchemaProcess.TBL_Folder_estado.DBUpdate(newFolderEstadoType, dashboardRow.fk_Expediente, dashboardRow.fk_Folder, DesktopConfig.Modulo.Imaging)
                                End If

                            End If
                        Next

                        Application.DoEvents()

                        dbmImaging.Transaction_Commit()
                        manager.TransactionCommit()

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

                    EventManager.FinalizarReIndexacion(paqueteRow.fk_Cargue, paqueteRow.id_Cargue_Paquete)


                    Return True
                Catch ex As Exception
                    progressForm.Hide()
                    Application.DoEvents()

                    DesktopMessageBoxControl.DesktopMessageShow("Save", ex)

                    Return False
                Finally
                    progressForm.Close()
                End Try
            End If

            Return False
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Private Function GetSize(ByRef documentItem As Generic.DocumentFile, ByVal formato As Slyg.Tools.Imaging.ImageManager.EnumFormat, ByVal compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression) As Long

        '    Dim Peso As Long = 0

        '    For Each folioItem As Folio In documentItem
        '        Dim dataImage = ImageManager.GetFolioData(folioItem.FileName, 1, formato, compresion)
        '        Peso += dataImage.Length
        '    Next

        '    Return Peso

        'End Function

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

            If (Validar()) Then
                SaveDocument()

                If (CurrentFolio IsNot Nothing) Then CurrentFolio.Unselect()

                If (_CurrentImageIndex > 0) Then
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
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                Dim bloqueoDatatable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_get.DBExecute(Me.IndexerDesktopGlobal.SesionID)
                If bloqueoDatatable.Count > 0 Then
                    For Each BloqueoRow In bloqueoDatatable
                        dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Capturas.DBExecute(BloqueoRow.fk_Expediente, BloqueoRow.fk_Folder, BloqueoRow.fk_File)
                    Next
                End If
            Catch ex As Exception
                Throw

            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
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

            Me._ot = ot

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)


                ' Leer los Files a reindexar                
                _bloqueotable = dbmImaging.SchemaProcess.PA_Dashboard_Capturas_get.DBExecute(Me.IndexerDesktopGlobal.SesionID)
                If (_bloqueotable.Count > 0) Then
                    ' Leer el listado de imagenes
                    Dim manager As FileProviderManager = Nothing
                    Dim idEntidadServidor As Short
                    Dim idServidor As Short = -1
                    Try
                        For Each dashboardRow In _bloqueotable
                            Dim fileImagingTable = dbmCore.SchemaImaging.TBL_File.DBGet(dashboardRow.fk_Expediente, dashboardRow.fk_Folder, dashboardRow.fk_File, Nothing)
                            If (fileImagingTable.Count = 0) Then
                                Throw New Exception("No se encontró data asociada al Ex: " & dashboardRow.fk_Expediente & " Fo: " & dashboardRow.fk_Folder & " Fi: " & dashboardRow.fk_File & " del Dashboard")
                            End If
                            Dim fileImagingRow = fileImagingTable(fileImagingTable.Count - 1)

                            Dim folderTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(dashboardRow.fk_Expediente, dashboardRow.fk_Folder)

                            If (idServidor = -1 OrElse (idEntidadServidor <> folderTable(0).fk_Entidad_Servidor And idServidor <> folderTable(0).fk_Servidor)) Then
                                If (manager IsNot Nothing) Then manager.Disconnect()

                                idEntidadServidor = folderTable(0).fk_Entidad_Servidor
                                idServidor = folderTable(0).fk_Servidor

                                manager = New FileProviderManager(dashboardRow.fk_Expediente, dashboardRow.fk_Folder, dbmImaging, Me.IndexerSesion.Usuario.id)
                                manager.Connect()
                            End If

                            Dim foliosFile = manager.GetFolios(fileImagingRow.fk_Expediente, fileImagingRow.fk_Folder, fileImagingRow.fk_File, fileImagingRow.id_Version)

                            For i As Short = 1 To foliosFile
                                _foliosTable.Add(New FolioReIndex() With {.EsExterno = False, .Expediente = fileImagingRow.fk_Expediente, .Folder = fileImagingRow.fk_Folder, .File = fileImagingRow.fk_File, .Version = fileImagingRow.id_Version, .Folio = i})
                            Next

                            _firstFile = _foliosTable(0)
                        Next
                    Catch
                        Throw
                    Finally
                        If (manager IsNot Nothing) Then manager.Disconnect()
                    End Try

                    _ImageCount = _foliosTable.Count

                    IndexerView.Information = ""
                    LoadConfig(dbmCore, dbmImaging, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto)
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

            _ImageCount = _foliosTable.Count

            If (_ImageCount = 0) Then
                Const mensaje = "No se encontraron folios asociados a los Files seleccionados, por favor comuniquese con el administrador del sistema." & vbCrLf

                Throw New Exception(mensaje)
            End If

            ' Crear data inicial
            Dim newFolder = New Folder(Me)
            Folders.Add(newFolder)

            Dim newDocumento = newFolder.NewDocumentFile()
            newFolder.Add(newDocumento)

            Dim newFolio = newDocumento.NewFolio(True)

            newDocumento.Add(newFolio)
            InicializarFolio(newFolio)

            View.ThumbnailPanel.Controls.Add(newFolder.Panel)

            'View.SetTitle("Indexación [Cargue: " & CargueRow.id_Cargue & " - Paquete: " & paqueteRow.id_Cargue_Paquete & " - CP: " & CargueRow.Observaciones & "]")

            '-------------------------------------------------------------------------------------------------------------
            ' LOGGING - PERFORMANCE
            '-------------------------------------------------------------------------------------------------------------
            Dim fechaFinProceso = Now

            Dim traceMessage As String = ""
            traceMessage &= "Duración:" & vbTab & (fechaInicioProceso - fechaFinProceso).TotalMilliseconds & vbTab
            'TraceMessage &= "Cargue:" & vbTab & CargueRow.id_Cargue & vbTab
            'TraceMessage &= "Paquete:" & vbTab & paqueteRow.id_Cargue_Paquete & vbTab
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
            Else
                Return True
            End If

            Return False
        End Function

        Public Overrides Function SetCurrentFolio(ByVal value As Folio, ByRef nUpdateInfo As Boolean) As Boolean
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

        Public Overrides Sub Move(ByVal nSourceFolio As Folio, ByVal nTargetFolio As Folio, ByVal nTargetIndex As Integer)
            Dim sourceDocument = nSourceFolio.Parent
            Dim sourceFolder = sourceDocument.Parent

            ' Mover los indices
            Dim actual = Me._foliosTable(nSourceFolio.GlobalIndex)
            Me._foliosTable.RemoveAt(nSourceFolio.GlobalIndex)

            nSourceFolio.Move(nTargetFolio.Parent, nTargetIndex)

            Me._foliosTable.Insert(nTargetIndex, actual)

            If (sourceDocument.Count = 0) Then sourceFolder.Remove(sourceDocument)
            If (sourceFolder.Count = 0) Then
                Me.View.ThumbnailPanel.Controls.Remove(sourceFolder.Panel)
                Me.Folders.Remove(sourceFolder)
            End If

            Renumerar()
        End Sub

        Public Overrides Sub AutoIndexar(ByVal nDocumento As Integer, ByVal nFolios As Integer, ByVal nModo As ModoAutoIndexarEnum)
            Throw New NotImplementedException()
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

            IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
            'IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)
            IndexerView.TipoDocumental_DataSource = New DataView(DocumentoIndexacionDataTable)
        End Sub

        Private Sub InicializarFolio(ByRef nFolio As Folio)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Try
                If (_foliosTable.Count > 0) Then
                    Dim folioActual = _foliosTable(nFolio.GlobalIndex)

                    If (folioActual.EsExterno) Then
                        Dim thumbnail = ImageManager.GetThumbnailData(folioActual.Filename, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                        nFolio.FileName = folioActual.Filename
                        nFolio.ThumbnailImage = New FreeImageAPI.FreeImageBitmap(New MemoryStream(thumbnail(0)))
                    Else
                        Dim imagen() As Byte = Nothing
                        Dim thumbnail() As Byte = Nothing

                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        manager = New FileProviderManager(folioActual.Expediente, folioActual.Folder, dbmImaging, Me.IndexerSesion.Usuario.id)
                        manager.Connect()

                        manager.GetFolio(folioActual.Expediente, folioActual.Folder, folioActual.File, folioActual.Version, folioActual.Folio, imagen, thumbnail)

                        nFolio.FileName = _TempPath & Guid.NewGuid().ToString() & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                        nFolio.ThumbnailImage = New FreeImageAPI.FreeImageBitmap(New MemoryStream(thumbnail))

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


        Public Sub SaveDocument()
            ' Actulizar esquema del folder
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
            'Try
            '    Dim Hora = Now.Hour
            '    Dim EsNocturno = Not (Hora >= 6 And Hora < 22)
            '    dbmImaging.SchemaProcess.PA_Insertar_Productividad.DBExecute(Me.IndexerDesktopGlobal.CentroProcesamientoRow.fk_Entidad, CargueRow.fk_Entidad, CargueRow.fk_Proyecto, _
            '                                                                  CShort(0), nProductividad.Usuario, nProductividad.Documentos, nProductividad.Campos, _
            '                                                                 nProductividad.Caracteres, nProductividad.Validaciones, nEtapa, EsNocturno, nCargue, nPaquete, Nothing, Nothing, Nothing, Nothing)
            'Catch ex As Exception
            '    DesktopMessageBoxControl.DesktopMessageShow("Insertar Productividad", ex)
            'End Try
        End Sub

        Private Sub Renumerar()
            Dim globalIndex = 0
            For Each folder In Folders
                For Each document As DocumentFile In folder
                    For Each folio As Folio In document
                        folio.setIndex(globalIndex)
                        globalIndex += 1
                    Next
                Next
            Next
        End Sub

#End Region

#Region " Funciones "

        ' ReSharper disable UnusedParameter.Local
        Private Function ValidarFolder(ByVal nFolder As Folder) As Boolean
            Return True
        End Function

#End Region

    End Class

End Namespace