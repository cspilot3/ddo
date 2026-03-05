Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports Slyg.Tools
Imports Miharu.FileProvider.Library
Imports Slyg.Tools.Imaging
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Imports BarcodeLib
Imports System.Drawing
Imports Microsoft.Reporting.WinForms
Imports BarcodeLib.BarcodeReader


Namespace Controller.Indexer.Capturas.Captura

    Public Class ProcesoAdicionalCapturaController
        Inherits CapturasAdicionalController

#Region " Declaraciones "

        Protected MesaControlDataTable As New DBImaging.SchemaProcess.TBL_File_Data_MCDataTable()

#End Region

#Region " Propiedades "

        Protected Overrides ReadOnly Property ProcessName As String
            Get
                Return "Proceso Especial Captura"
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
            IndexerView.ShowDataPanel(True)
            IndexerView.ShowCamposLlavePanel(False)
            IndexerView.ShowValidationsPanel(False)
            IndexerView.ShowValidationsListasPanel(False)
            IndexerView.ShowToolTipData(True)
            IndexerView.ShowAutoIndexar(False)

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
            Dim Productividad As New DesktopConfig.ProductividadType

            Productividad.Usuario = Me.IndexerSesion.Usuario.id

            If (Validar()) Then
                Try
                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                    dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                    dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                    Dim FechaInicioProceso = Now

                    Try
                        Dim ValidarSave As Boolean = True

                        'Valida label Captura Adicional
                        Try
                            EventManager.ValidarSaveLabelCaptura(_Campos, FileCoreRow.fk_Documento, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, "A", ValidarSave)
                            'Validación Campo Duplicado 
                            Dim id_Campo As Int16
                            Dim Nombre_Campo As String
                            Dim Label As String = ""

                            Dim drowCampoFiltrado = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_DocumentoCampo_Control_Duplicado(Me.IndexerImagingGlobal.Entidad, FileCoreRow.fk_Documento, True)

                            If (drowCampoFiltrado.Count > 0) Then
                                id_Campo = CShort(drowCampoFiltrado(0).Item("id_Campo").ToString())
                                Nombre_Campo = drowCampoFiltrado(0).Item("Nombre_Campo").ToString()

                                'recorre cada campo para encontrar valor de label
                                For Each Item In _Campos
                                    If Item.id = id_Campo Then
                                        Label = Item.Control.Value.ToString
                                    End If
                                Next
                                If Trim(Label) <> "" Then
                                    Dim dtResult = dbmImaging.SchemaProcess.PA_Validar_Campo_Duplicado.DBExecute(CInt(FileImagingRow.fk_Expediente), FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileCoreRow.fk_Documento, id_Campo, Nombre_Campo, Trim(Label), "A")
                                    If dtResult.Rows.Count > 0 Then
                                        If dtResult.Rows(0).Item("TipoResultado").ToString = "ERROR" Then
                                            DesktopMessageBoxControl.DesktopMessageShow(dtResult.Rows(0).Item("Mensaje").ToString(), "Guardar Captura", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                            ValidarSave = False
                                        End If
                                    End If
                                End If
                            End If

                            If Not ValidarSave Then
                                Return False
                            End If
                        Catch
                        End Try

                        '--------------------------------------------------------------------------------------
                        ' REPORTAR DUPLICADOS
                        '--------------------------------------------------------------------------------------
                        Try
                            Dim FileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Validacion_Listas.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                            If (FileBloqueadoDataTable(0).fk_Usuario_Log <> Me.IndexerSesion.Usuario.id) Then
                                dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Proceso_Adicional, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PcName)
                            End If

                        Catch : End Try
                        '--------------------------------------------------------------------------------------


                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)


                        ' Actualizar estados
                        Dim NextEstado = DBCore.EstadoEnum.Indexado

                        'Crea el estado del Folder en Core
                        If (CType(NextEstado, DBCore.EstadoEnum) = DBCore.EstadoEnum.Indexado) Then
                            Dim FolderEstadoDataTable = dbmCore.SchemaProcess.TBL_Folder_estado.DBFindByfk_Expedientefk_FolderModulo(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, DesktopConfig.Modulo.Imaging)

                            If FolderEstadoDataTable.Count = 0 Then
                                Dim InsertEstado As New DBCore.SchemaProcess.TBL_Folder_estadoType()
                                InsertEstado.fk_Expediente = FileImagingRow.fk_Expediente
                                InsertEstado.fk_Folder = FileImagingRow.fk_Folder
                                InsertEstado.Modulo = DesktopConfig.Modulo.Imaging
                                InsertEstado.fk_Estado = DBCore.EstadoEnum.Indexado
                                InsertEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                                InsertEstado.Fecha_Log = SlygNullable.SysDate
                                dbmCore.SchemaProcess.TBL_Folder_estado.DBInsert(InsertEstado)
                            Else
                                Dim UpdateEstadoFolder As New DBCore.SchemaProcess.TBL_Folder_estadoType()
                                UpdateEstadoFolder.fk_Estado = DBCore.EstadoEnum.Indexado
                                UpdateEstadoFolder.fk_Usuario = Me.IndexerSesion.Usuario.id
                                UpdateEstadoFolder.Fecha_Log = SlygNullable.SysDate
                                dbmCore.SchemaProcess.TBL_Folder_estado.DBUpdate(UpdateEstadoFolder, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, DesktopConfig.Modulo.Imaging)
                            End If
                        End If

                        ' Reportar usuario captura
                        Dim FileType = New DBImaging.SchemaProcess.TBL_FileType()
                        FileType.Usuario_Proceso_Adicional = Me.IndexerSesion.Usuario.id
                        FileType.Fecha_Proceso_Adicional = SlygNullable.SysDate
                        dbmImaging.SchemaProcess.TBL_File.DBUpdate(FileType, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                        ' Crear datos de primera captura a partir de captura
                        Dim CamposQuery As New List(Of CampoCaptura)
                        For Each Campo In _Campos
                            If Campo.Control.IsVisible Then
                                ' Eliminar todos los datos de doble captura
                                If (Campo.Control.Tipo <> DesktopConfig.CampoTipo.TablaAsociada) Then
                                    ' Leer la data almacenada
                                    If Campo.Control.Tipo <> DesktopConfig.CampoTipo.Query Then
                                        Dim FileDataCampo = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)
                                        Dim FileDataType As New DBCore.SchemaProcess.TBL_File_DataType()

                                        FileDataType.Valor_File_Data = Campo.Control.Value
                                        FileDataType.Conteo_File_Data = Campo.Control.Value.ToString().Length

                                        If (FileDataCampo.Count = 0) Then
                                            FileDataType.fk_Expediente = FileCoreRow.fk_Expediente
                                            FileDataType.fk_Folder = FileCoreRow.fk_Folder
                                            FileDataType.fk_File = FileCoreRow.id_File
                                            FileDataType.fk_Documento = FileCoreRow.fk_Documento
                                            FileDataType.fk_Campo = Campo.id
                                            dbmCore.SchemaProcess.TBL_File_Data.DBInsert(FileDataType)
                                        Else
                                            dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(FileDataType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)
                                        End If

                                        If Campo.Control.IsVisible Then
                                            Productividad.Campos += 1
                                            Productividad.Caracteres += Campo.Control.Value.ToString().Length
                                        End If
                                    Else
                                        CamposQuery.Add(Campo)
                                    End If
                                Else
                                    Dim ValueTable As DataTable = CType(Campo.Control.Value, DataTable)
                                    Dim Col As Integer = 0


                                    For Each TablaAsociadaRow As DBImaging.SchemaConfig.CTA_Tabla_AsociadaRow In TablaAsociadaDataTable.Select("fk_Campo = " & Campo.id)
                                        Dim Fil As Integer = 1
                                        Dim TablaControl = CType(Campo.Control, TableInputControl)

                                        For Each Registro As DataRow In ValueTable.Rows
                                            Dim TableValue As String

                                            TableValue = Registro.Item(Col).ToString()

                                            If (dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil)).Count = 0) Then
                                                dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBInsert(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil), TableValue, TableValue.Length)
                                            Else
                                                dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBUpdate(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, TableValue, TableValue.Length, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil))
                                            End If


                                            If (Campo.Control.IsVisible And Not TablaControl.DefinicionCaptura(Col).IsReadOnly) Then
                                                Productividad.Campos += 1
                                                Productividad.Caracteres += Registro.Item(Col).ToString().Length
                                            End If

                                            Fil += 1
                                        Next

                                        Col += 1
                                    Next
                                End If
                            End If
                        Next

                        'Almacena los valores de los campos tipo Query // Se almacenan de ultimo porque puden depender de los valores antes insertados
                        For Each Campo In CamposQuery
                            Try
                                Dim Parametros As New List(Of Slyg.Data.Schemas.Parameter)
                                Dim Par1 As New Slyg.Data.Schemas.Parameter("@Expediente", FileCoreRow.fk_Expediente)
                                Dim Par2 As New Slyg.Data.Schemas.Parameter("@Folder", FileCoreRow.fk_Folder)
                                Dim Par3 As New Slyg.Data.Schemas.Parameter("@File", FileCoreRow.id_File)
                                Parametros.Add(Par1) : Parametros.Add(Par2) : Parametros.Add(Par3)

                                Dim FileDataCampo = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)

                                Dim FileDataType As New DBCore.SchemaProcess.TBL_File_DataType()
                                FileDataType.Valor_File_Data = Campo.Control.Value
                                FileDataType.Conteo_File_Data = Campo.Control.Value.ToString().Length

                                If (FileDataCampo.Count = 0) Then
                                    FileDataType.fk_Expediente = FileCoreRow.fk_Expediente
                                    FileDataType.fk_Folder = FileCoreRow.fk_Folder
                                    FileDataType.fk_File = FileCoreRow.id_File
                                    FileDataType.fk_Documento = FileCoreRow.fk_Documento
                                    FileDataType.fk_Campo = Campo.id

                                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(FileDataType)
                                Else
                                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(FileDataType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)
                                End If
                            Catch ex As Exception
                                Throw New Exception("Ha ocurrido un error en el campo [" & Campo.id & "] de tipo [QUERY], es posible que contenga errores o no exista una definicion para este. --[" & ex.Message & "]--")
                            End Try
                        Next


                        ' Insertar productividad
                        InsertarProductividad(dbmImaging, Productividad, DesktopConfig.Etapa_Productividad.ProcesoAdicional_Captura, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                        ' Actualizar llaves
                        dbmCore.SchemaProcess.PA_Calcular_Llaves.DBExecute(FileImagingRow.fk_Expediente)


                        ' Actualizar estado
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

                        '--==================== INICIO REQUERIMIENTO RITM0364368 =====================================
                        IndexarFolioSticker(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento)
                        '--==================== FIN REQUERIMIENTO RITM0364368 =====================================

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

                    Catch ex As Exception
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                        DesktopMessageBoxControl.DesktopMessageShow("Publicar_Proceso_Adicional", ex)

                        Return False
                    Finally
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try

                    EventManager.FinalizarProcesoAdicionalCaptura(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                    '-------------------------------------------------------------------------------------------------------------
                    ' LOGGING - PERFORMANCE
                    '-------------------------------------------------------------------------------------------------------------
                    Dim FechaFinProceso = Now
                    Dim TraceMessage As String = "[Proceso Adicional Captura][Publicar] - Inicio: " & FechaInicioProceso.ToString() & " Fin: " & FechaFinProceso.ToString()
                    DesktopTrace.Trace(TraceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, Program.AssemblyTitle)
                    '-------------------------------------------------------------------------------------------------------------

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Publicar_Proceso_Adicional", ex)

                    Return False
                End Try

                Return True


            End If

            Return False
        End Function
#Region "RITM0364368"
        Private Sub IndexarFolioSticker(ByVal nExpediente As Long,
                                        ByVal nFolder As Short,
                                        ByVal nFile As Short,
                                        ByVal nDocumento As Long)
            Dim manager As FileProviderManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim idFolder, idFile, idFolio, id_Version, idVersion, idDocumento As Short
            Dim idExpediente As Long
            Dim backImage, topImage As Image
            Dim imagenFinal As Byte()
            Dim AumentaFolio As Short = 1
            Dim AumentaVersion As Short = 1

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                dbmCore = New DBCore.DBCoreDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Core)
                dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                manager = New FileProviderManager(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, dbmImaging, Me.IndexerSesion.Usuario.id)
                manager.Connect()

                ' CONSULTA TABLA PARAMETRICA PARA GENERAR STICKER
                Dim _documentosStickerDataTable As DBImaging.SchemaConfig.TBL_Documento_StickerDataTable
                _documentosStickerDataTable = dbmImaging.SchemaConfig.TBL_Documento_Sticker.DBFindByfk_Entidadfk_Proyectogenera_Sticker_Fisicofk_Documento(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, False, FileCoreRow.fk_Documento)

                If _documentosStickerDataTable.Rows.Count > 0 Then

                    'CONSULTA PARAMETROS EN FORMATO XML
                    Dim docXML As XmlDocument = New XmlDocument()
                    docXML.LoadXml(_documentosStickerDataTable(0).parametro_Reporte)
                    Dim nodes As XmlNodeList = docXML.SelectNodes("/Configuracion/Sticker")

                    Dim dt As New DataTable
                    With dt
                        .Columns.Add(New DataColumn("PosicionX", GetType(String)))
                        .Columns.Add(New DataColumn("PosicionY", GetType(String)))
                        .Columns.Add(New DataColumn("CodigoBarras", GetType(String)))
                        .Columns.Add(New DataColumn("NombreParametrosPrincipal", GetType(String)))
                        .Columns.Add(New DataColumn("ValorParametrosPrincipal", GetType(String)))
                        .Columns.Add(New DataColumn("NombreParametrosSticker", GetType(String)))
                        .Columns.Add(New DataColumn("ValorParametrosSticker", GetType(String)))
                    End With

                    For Each n1 As XmlNode In docXML.DocumentElement.ChildNodes
                        If n1.HasChildNodes Then
                            Dim row As DataRow
                            row = dt.NewRow
                            For Each n2 As XmlNode In n1.ChildNodes
                                For Each n3 As XmlNode In n2.ChildNodes
                                    row(n2.Name) = n3.InnerText
                                Next
                            Next
                            dt.Rows.Add(row)
                        End If
                    Next

                    'CONSULTA DATOS PRINCIPALES 
                    Dim resultadoDataTable = DatosParametros(dbmImaging, nExpediente, nFolder, nFile, nDocumento,
                        _documentosStickerDataTable(0).sqlPrincipal, dt.Rows(0).Item("NombreParametrosPrincipal").ToString(),
                                       dt.Rows(0).Item("ValorParametrosPrincipal").ToString())
                    If resultadoDataTable.Rows.Count > 0 Then
                        Try
                            For Each ItemCrear As DataRow In resultadoDataTable.Rows
                                
                                Dim FileName = _TempPath & Guid.NewGuid().ToString() & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                                Dim FileNameNuevo = _TempPath & Guid.NewGuid().ToString() & Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida

                                Try
                                    If ItemCrear.Item("fk_Expediente").ToString() <> "" Then
                                        idExpediente = CLng(ItemCrear.Item("fk_Expediente"))
                                    Else
                                        idExpediente = FileCoreRow.fk_Expediente
                                    End If
                                Catch ex As Exception
                                    idExpediente = FileCoreRow.fk_Expediente
                                End Try
                                Try
                                    If ItemCrear.Item("fk_Folder").ToString() <> "" Then
                                        idFolder = CShort(ItemCrear.Item("fk_Folder"))
                                    Else
                                        idFolder = FileCoreRow.fk_Folder
                                    End If
                                Catch ex As Exception
                                    idFolder = FileCoreRow.fk_Folder
                                End Try
                                Try
                                    If ItemCrear.Item("fk_File").ToString() <> "" Then
                                        idFile = CShort(ItemCrear.Item("fk_File"))
                                    Else
                                        idFile = FileCoreRow.id_File
                                    End If
                                Catch ex As Exception
                                    idFile = FileCoreRow.id_File
                                End Try
                                Try
                                    If ItemCrear.Item("id_File_Record_Folio").ToString() <> "" Then
                                        idFolio = CShort(CInt(ItemCrear.Item("id_File_Record_Folio")))
                                    Else
                                        idFolio = FileImagingRow.Folios_Documento_File
                                    End If
                                Catch ex As Exception
                                    idFolio = FileImagingRow.Folios_Documento_File
                                End Try
                                Try
                                    If ItemCrear.Item("fk_Version").ToString() <> "" Then
                                        id_Version = CShort(ItemCrear.Item("fk_Version"))
                                    Else
                                        id_Version = FileImagingRow.id_Version
                                    End If
                                Catch ex As Exception
                                    id_Version = FileImagingRow.id_Version
                                End Try
                                Try
                                    If ItemCrear.Item("id_Documento").ToString() <> "" Then
                                        idDocumento = CShort(ItemCrear.Item("id_Documento"))
                                    Else
                                        idDocumento = CShort(FileCoreRow.fk_Documento)
                                    End If
                                Catch ex As Exception
                                    idDocumento = CShort(FileCoreRow.fk_Documento)
                                End Try

                                idVersion = id_Version + AumentaVersion

                                topImage = GenerarSticker(dbmImaging,
                                               _documentosStickerDataTable,
                                               dt,
                                               Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad,
                                               Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto,
                                               idDocumento,
                                               idExpediente,
                                               idFolder,
                                               idFile)

                                Dim Folios = manager.GetFolios(idExpediente, idFolder, idFile, id_Version)

                                If Folios > 0 Then
                                    'crear registro en [DB_Miharu.Core].Imaging.TBL_File
                                    Dim identificador = Guid.NewGuid()
                                    Dim fileImgType As New DBCore.SchemaImaging.TBL_FileType()
                                    fileImgType.fk_Expediente = idExpediente
                                    fileImgType.fk_Folder = idFolder
                                    fileImgType.fk_File = idFile
                                    fileImgType.id_Version = idVersion
                                    fileImgType.File_Unique_Identifier = identificador
                                    fileImgType.Folios_Documento_File = Folios
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

                                    'crear registro en [DB_Miharu.Imagign_Storage].Imaging.TBL_File
                                    manager.CreateItem(idExpediente, idFolder, idFile, idVersion, Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, identificador)

                                    For folio As Short = 1 To Folios
                                        Dim imagen() As Byte = Nothing
                                        Dim thumbnail() As Byte = Nothing

                                        manager.GetFolio(idExpediente, idFolder, idFile, id_Version, folio, imagen, thumbnail)

                                        Using fileImage = New FileStream(FileName, FileMode.Create)
                                            fileImage.Write(imagen, 0, imagen.Length)
                                            fileImage.Close()
                                        End Using

                                        If folio = idFolio Then
                                            backImage = ByteArrayToImage(imagen)
                                            imagenFinal = ImageToByteArray(SobrePonerImagen(backImage, topImage, FileNameNuevo, Me.IndexerImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, Integer.Parse(dt.Rows(0).Item("PosicionX").ToString()), Integer.Parse(dt.Rows(0).Item("PosicionY").ToString())))
                                            manager.CreateFolio(idExpediente, idFolder, idFile, idVersion, idFolio, imagenFinal, thumbnail, True)
                                        Else
                                            manager.CreateFolio(idExpediente, idFolder, idFile, idVersion, idFolio, imagen, thumbnail, True)
                                        End If
                                    Next
                                End If
                            Next
                        Catch ex As Exception
                        End Try
                    End If
                End If
            Catch ex As Exception
                If (Not View.ViewClosing) Then ShowMessage("IndexarFolioSticker", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub
        Private Function GenerarSticker(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager,
                                   ByVal _documentosStickerDataTable As DBImaging.SchemaConfig.TBL_Documento_StickerDataTable,
                                   ByVal dt As DataTable,
                                   ByVal nEntidad As Short,
                                   ByVal nProyecto As Short,
                                   ByVal nDocumento As Long,
                                   ByVal nExpediente As Long,
                                   ByVal nFolder As Short,
                                   ByVal nFile As Short) As Image
            Dim Extension_Formato_Imagen_Salida As String = String.Empty
            Dim ImagenSticker As Image = Nothing
            Dim NombreArchivo As String

            Dim Datos = DatosParametros(dbmImaging,
                                   nExpediente,
                                   nFolder,
                                   nFile,
                                   nDocumento,
                                   _documentosStickerDataTable(0).sqlSticker,
                                   dt.Rows(0).Item("NombreParametrosSticker").ToString(),
                                   dt.Rows(0).Item("ValorParametrosSticker").ToString())
            Datos.Columns.Add("IMAGEN", GetType(Byte()))
            Dim barcodeImage As BarcodeLib.Barcode = New BarcodeLib.Barcode()
            barcodeImage.IncludeLabel = False
            For Each item As DataRow In Datos.Rows
                Try
                    Dim img As Byte() = Nothing
                    Select Case dt.Rows(0).Item("CodigoBarras").ToString()
                        Case "CODE39Extended"
                            img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE39Extended, item("CodigoBarras").ToString(), Color.Black, Color.White, 250, 25))
                        Case "CODE39"
                            img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE39, item("CodigoBarras").ToString(), Color.Black, Color.White, 250, 25))
                        Case "CODE128"
                            img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE128, item("CodigoBarras").ToString(), Color.Black, Color.White, 250, 25))
                        Case "EAN13"
                            img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.EAN13, item("CodigoBarras").ToString(), Color.Black, Color.White, 250, 25))
                    End Select
                    item("IMAGEN") = img
                Catch ex As Exception
                End Try
            Next
            Datos.AcceptChanges()
            If Datos.Rows.Count > 0 Then
                Dim viewer As ReportViewer = New ReportViewer()
                Dim rds As ReportDataSource = New ReportDataSource("DataSet1", Datos)
                viewer.Reset()
                viewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Indexer." & _documentosStickerDataTable(0).nombre_RDLC & ".rdlc"
                viewer.LocalReport.DataSources.Clear()
                viewer.LocalReport.DataSources.Add(rds)
                Try
                    Dim docXMLDevive As XmlDocument = New XmlDocument()
                    docXMLDevive.LoadXml(_documentosStickerDataTable(0).parametro_Dispositivo)
                    Dim nodo As XmlNodeList = docXMLDevive.GetElementsByTagName("OutputFormat")
                    Extension_Formato_Imagen_Salida = nodo(0).InnerText
                    NombreArchivo = _TempPath & Guid.NewGuid().ToString() & "." & Extension_Formato_Imagen_Salida
                    ImagenSticker = SaveSticker(viewer, NombreArchivo, _documentosStickerDataTable(0).parametro_Dispositivo)
                Catch ex As Exception
                    Return Nothing
                End Try
            End If
            Return ImagenSticker
        End Function
        Private Function DatosParametros(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager,
                                      ByVal nExpediente As Long,
                                      ByVal nFolder As Short,
                                      ByVal nFile As Short,
                                      ByVal nDocumento As Long,
                                      ByVal sqlquery As String,
                                      ByVal nombreParametros As String,
                                      ByVal valorParametros As String) As DataTable
            Dim dt As DataTable = Nothing
            Try
                Dim parametros As String()
                Dim valores As String()
                Dim SqlParameter() As SqlParameter
                parametros = nombreParametros.Split(","c)
                valores = valorParametros.Split(","c)
                ReDim SqlParameter(parametros.Length - 1)
                For i As Integer = 0 To UBound(parametros, 1)
                    Select Case parametros(i)
                        Case "fkExpediente"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nExpediente)
                        Case "fkFolder"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nFolder)
                        Case "fkFile"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nFile)
                        Case "fkDocumento"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nDocumento)
                        Case Else
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", valores(i))
                    End Select
                Next i
                dt = ExecuteQuery(sqlquery, dbmImaging, SqlParameter)
                Return dt
            Catch ex As Exception
            End Try
            Return dt
        End Function
        Private Function ExecuteQuery(ByVal s As String,
                                      ByVal dbmImaging As DBImaging.DBImagingDataBaseManager,
                                      ByVal ParamArray params() As SqlParameter) As DataTable
            Dim dt As DataTable = Nothing
            Using da As New System.Data.SqlClient.SqlDataAdapter(s, dbmImaging.DataBase.ConnectionString)
                Try
                    dt = New DataTable
                    If params.Length > 0 Then
                        da.SelectCommand.Parameters.AddRange(params)
                    End If
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(dt)
                    Return dt
                Catch ex As SqlException
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Return dt
            End Using
        End Function
        Public Shared Function ImageToByteArray(ByVal Image As System.Drawing.Image) As Byte()
            Using Ms = New MemoryStream()
                Image.Save(Ms, System.Drawing.Imaging.ImageFormat.Png)
                Return Ms.ToArray()
            End Using
        End Function
        Public Shared Function ByteArrayToImage(ByVal Bit As Byte()) As System.Drawing.Image
            Using Ms = New MemoryStream(Bit)
                Return Image.FromStream(Ms)
            End Using
        End Function
        Public Function SaveSticker(ByVal viewer As ReportViewer,
                                    ByVal savePath As String,
                                    ByVal DeviceInfo As String) As System.Drawing.Image
            Dim warnings As Warning() = Nothing
            Dim streamIds As String() = Nothing
            Dim mimeType As String = String.Empty
            Dim encoding As String = String.Empty
            Dim extension As String = String.Empty
            Dim filetype As String = String.Empty
            Dim bytes As Byte() = viewer.LocalReport.Render("Image", DeviceInfo, mimeType, encoding, extension, streamIds, warnings)
            Dim fs As FileStream = New FileStream(savePath, FileMode.OpenOrCreate)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
            Return ByteArrayToImage(bytes)
        End Function
        Public Shared Function SobrePonerImagen(ByVal backImage As Image,
                                                ByVal topImage As Image,
                                                ByVal FileName As String,
                                                ByVal FormatoSalida As String,
                                                Optional ByVal topPosX As Integer = 0,
                                                Optional ByVal topPosY As Integer = 0) As Image
            Dim Imagen As Image
            If backImage Is Nothing Then
                Throw New ArgumentNullException(paramName:="backImage")
            ElseIf topImage Is Nothing Then
                Throw New ArgumentNullException(paramName:="topImage")
            ElseIf (topImage.Width > backImage.Width) OrElse (topImage.Height > backImage.Height) Then
                Throw New ArgumentException("Los límites de la imagen son mayores que la imagen de fondo.", "topImage")
            Else
                topPosX += Convert.ToInt32((backImage.Width / 2) - (topImage.Width / 2))
                topPosY += Convert.ToInt32((backImage.Height / 2) - (topImage.Height / 2))
                Dim bmp As Bitmap = New Bitmap(backImage.Width, backImage.Height)

                Using canvas As Graphics = Graphics.FromImage(bmp)
                    canvas.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    canvas.DrawImage(image:=backImage, destRect:=New Rectangle(0, 0, bmp.Width, bmp.Height), srcRect:=New Rectangle(0, 0, bmp.Width, bmp.Height), srcUnit:=GraphicsUnit.Pixel)
                    canvas.DrawImage(image:=topImage, destRect:=New Rectangle(topPosX, topPosY, topImage.Width, topImage.Height), srcRect:=New Rectangle(0, 0, topImage.Width, topImage.Height), srcUnit:=GraphicsUnit.Pixel)
                    canvas.Save()
                End Using

                Select Case FormatoSalida
                    Case ".tif"
                        bmp.Save(FileName, System.Drawing.Imaging.ImageFormat.Tiff)
                    Case ".png"
                        bmp.Save(FileName, System.Drawing.Imaging.ImageFormat.Png)
                    Case Else
                        bmp.Save(FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
                End Select
                Imagen = Image.FromFile(FileName)
                'Process.Start(FileName)
                Return Imagen
            End If
        End Function
#End Region
        Public Overrides Function ValidacionListas() As Boolean
            Dim _ValidacionListas As Boolean

            _ValidacionListas = False

            Return _ValidacionListas
        End Function

        Public Overrides Function Campos(ByVal idDocumento As Integer) As List(Of CampoCaptura)
            Me._Campos.Clear()

            For Each CampoRow As DBImaging.SchemaConfig.CTA_CampoRow In CamposDataTable.Select("fk_Documento = " & idDocumento & " AND Usa_Captura_Proceso_Adicional = 1")
                Dim ControlCaptura As View.IInputControl
                Dim CampoCaptura As New CampoCaptura()
                Dim ListDefinicionCaptura As New List(Of DefinicionCaptura)

                ' Control
                Select Case CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                    Case DesktopConfig.CampoTipo.Texto
                        Dim DefinicionCaptura = New DefinicionCaptura()
                        ControlCaptura = New TextInputControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.Texto
                        DefinicionCaptura.Mascara = CampoRow.Mascara
                        DefinicionCaptura.FormatoFecha = CampoRow.Formato
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                        DefinicionCaptura.MaximumLength = CampoRow.Length_Campo
                        DefinicionCaptura.MinimumLength = CampoRow.Length_Min_Campo
                        DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                        ListDefinicionCaptura.Add(DefinicionCaptura)
                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                        If (CampoRow.Valor_por_Defecto <> "") Then
                            ControlCaptura.Value = CampoRow.Valor_por_Defecto
                        End If

                    Case DesktopConfig.CampoTipo.Numerico
                        Dim DefinicionCaptura = New DefinicionCaptura()
                        ControlCaptura = New TextInputNumericControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.Numerico
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                        DefinicionCaptura.MaximumLength = CampoRow.Length_Campo
                        DefinicionCaptura.MinimumLength = CampoRow.Length_Min_Campo
                        DefinicionCaptura.Usa_Decimales = CampoRow.Usa_Decimales
                        DefinicionCaptura.Caption = CampoRow.Nombre_Campo

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
                        ControlCaptura = New TextInputDateTimeControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.Fecha
                        DefinicionCaptura.Mascara = CampoRow.Mascara
                        DefinicionCaptura.FormatoFecha = CampoRow.Formato
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                        DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                        ListDefinicionCaptura.Add(DefinicionCaptura)
                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                        If (CampoRow.Valor_por_Defecto <> "") Then
                            ControlCaptura.Value = CampoRow.Valor_por_Defecto
                        End If

                    Case DesktopConfig.CampoTipo.Lista
                        Dim DefinicionCaptura = New DefinicionCaptura()
                        ControlCaptura = New ListInputControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.Lista
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                        DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                        ' Datos de la lista
                        If (Not CampoRow.Isfk_Campo_ListaNull()) Then
                            Dim dtvLista As New DataView(ListaItemsDataTable)

                            dtvLista.RowFilter = "fk_Campo_Lista = " & CampoRow.fk_Campo_Lista

                            DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo

                            Dim ListControl = CType(ControlCaptura, ListInputControl)
                            ListControl.ValueDesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                            ListControl.ValueDesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                            ListControl.ValueDesktopComboBox.DataSource = dtvLista
                            ListControl.ValueDesktopComboBox.Refresh()
                            ListControl.ValueDesktopComboBox.SelectedValue = -1
                        End If

                        ListDefinicionCaptura.Add(DefinicionCaptura)
                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                    Case DesktopConfig.CampoTipo.SiNo
                        Dim DefinicionCaptura = New DefinicionCaptura()
                        ControlCaptura = New ListInputControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.SiNo
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                        DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                        Dim ListControl = CType(ControlCaptura, ListInputControl)
                        ListControl.ValueDesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                        ListControl.ValueDesktopComboBox.Items.Add("Si")
                        ListControl.ValueDesktopComboBox.Items.Add("No")
                        ListControl.ValueDesktopComboBox.SelectedIndex = 0

                        ListDefinicionCaptura.Add(DefinicionCaptura)
                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                    Case DesktopConfig.CampoTipo.Query
                        Dim DefinicionCaptura = New DefinicionCaptura()
                        ControlCaptura = New TextInputControl()

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.Texto
                        DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                        DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                        Dim TextControl = CType(ControlCaptura, TextInputControl)
                        TextControl.ValueDesktopTextBox.MaxLength = CampoRow.Length_Campo
                        TextControl.ValueDesktopTextBox.Text = "Automatico"
                        TextControl.ValueDesktopTextBox.Enabled = False

                        ListDefinicionCaptura.Add(DefinicionCaptura)
                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                    Case DesktopConfig.CampoTipo.TablaAsociada
                        Dim ValueTable = New DataTable()
                        Dim Columna As DataColumn
                        Dim ColumnasTotales As New List(Of Integer)

                        ControlCaptura = New TableInputControl()

                        For Each TablaAsociadaRow As DBImaging.SchemaConfig.CTA_Tabla_AsociadaRow In TablaAsociadaDataTable.Select("fk_Campo = " & CampoRow.id_Campo)
                            Dim DefinicionCaptura = New DefinicionCaptura()

                            DefinicionCaptura.id = TablaAsociadaRow.id_Campo_Tabla
                            DefinicionCaptura.DefaultValue = TablaAsociadaRow.Valor_por_Defecto
                            DefinicionCaptura.Caption = TablaAsociadaRow.Nombre_Campo
                            DefinicionCaptura.Es_Obligatorio_Campo = TablaAsociadaRow.Es_Obligatorio_Campo
                            DefinicionCaptura.FormatoFecha = TablaAsociadaRow.Formato

                            Columna = New DataColumn()
                            Columna.ColumnName = "col_" & TablaAsociadaRow.id_Campo_Tabla
                            Columna.Caption = TablaAsociadaRow.Nombre_Campo

                            Select Case CType(TablaAsociadaRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                                Case DesktopConfig.CampoTipo.Texto
                                    Columna.DataType = GetType(String)
                                    Columna.MaxLength = TablaAsociadaRow.Length_Campo

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.Texto
                                    DefinicionCaptura.MaximumLength = TablaAsociadaRow.Length_Campo
                                    DefinicionCaptura.Mascara = TablaAsociadaRow.Mascara

                                Case DesktopConfig.CampoTipo.Numerico
                                    Columna.DataType = GetType(String)

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.Numerico
                                    DefinicionCaptura.MaximumLength = TablaAsociadaRow.Length_Campo
                                    DefinicionCaptura.MinimumLength = 0
                                    DefinicionCaptura.Usa_Decimales = TablaAsociadaRow.Usa_Decimales

                                    If TablaAsociadaRow.Usa_Decimales Then
                                        DefinicionCaptura.Caracter_Decimal = CChar(TablaAsociadaRow.Caracter_Decimal)
                                        DefinicionCaptura.Cantidad_Decimales = TablaAsociadaRow.Cantidad_Decimales
                                    End If

                                Case DesktopConfig.CampoTipo.Fecha
                                    Columna.DataType = GetType(DateTime)

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.Fecha
                                    DefinicionCaptura.Mascara = TablaAsociadaRow.Mascara
                                    DefinicionCaptura.FormatoFecha = TablaAsociadaRow.Formato

                                Case DesktopConfig.CampoTipo.Lista
                                    Columna.DataType = GetType(String)

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.Lista

                                    ' Datos de la lista
                                    If (Not TablaAsociadaRow.Isfk_Campo_ListaNull()) Then
                                        DefinicionCaptura.Items = New DataView(ListaItemsDataTable)
                                        DefinicionCaptura.Items.RowFilter = "fk_Campo_Lista = " & TablaAsociadaRow.fk_Campo_Lista
                                        DefinicionCaptura.ValueMember = "Valor_Campo_Lista_Item"
                                        DefinicionCaptura.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                    End If

                                Case DesktopConfig.CampoTipo.SiNo
                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.SiNo

                                Case Else
                                    Throw New Exception("Tipo de campo no valido para una tabla asociada: " & TablaAsociadaRow.fk_Campo_Tipo)

                            End Select

                            ListDefinicionCaptura.Add(DefinicionCaptura)
                            ValueTable.Columns.Add(Columna)

                            If (TablaAsociadaRow.Es_Columna_Valor) Then
                                ColumnasTotales.Add(TablaAsociadaRow.id_Campo_Tabla)
                            End If
                        Next

                        ControlCaptura.Value = ValueTable

                        ' Control de totales
                        Dim ControlTabla = CType(ControlCaptura, TableInputControl)

                        ControlTabla.MinRegistros = CampoRow.Tabla_Min_Registros
                        ControlTabla.MaxRegistros = CampoRow.Tabla_Max_Registros
                        ControlTabla.ShowControlRegistros = CampoRow.Validar_Registros
                        ControlTabla.ShowControlValor = CampoRow.Validar_Totales And ColumnasTotales.Count > 0
                        ControlTabla.ColumnasTotales = ColumnasTotales

                        ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                    Case Else
                        Throw New Exception("Tipo de campo no reconocido: " & CampoRow.fk_Campo_Tipo)

                End Select

                ControlCaptura.UsaTrigger = CampoRow.Usa_Trigger
                ControlCaptura.TriggerValues.Clear()

                If (CampoRow.Usa_Trigger) Then
                    If (Not CampoRow.Isfk_Campo_ListaNull()) Then
                        For Each TriggerRow As DBImaging.SchemaConfig.TBL_Campo_TriggerRow In TriggersDataTable.Select("fk_Campo_Trigger = " & CampoRow.id_Campo)
                            Dim NewKeyValue = New View.KeyValueItem(TriggerRow.Valor, TriggerRow.fk_Campo_Ocultar, TriggerRow.fk_Campo_Tabla_Ocultar)
                            ControlCaptura.TriggerValues.Add(NewKeyValue)
                        Next
                    Else
                        For Each TriggerValidacionRow As DBImaging.SchemaConfig.TBL_Campo_Trigger_ValidacionRow In TriggersValidacionesDataTable
                            Dim NewValValue = New View.TriggersValidations_Items(TriggerValidacionRow.fk_Validacion_Ocultar, TriggerValidacionRow.Valor, TriggerValidacionRow.fk_Campo_Trigger, TriggerValidacionRow.Operador_Validacion)
                            ControlCaptura.TriggerValidaciones.Add(NewValValue)
                        Next
                    End If

                End If

                ControlCaptura.ÏsOCRCapture = Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_OCR_Captura  ' data obtenida TBL_Proyecto
                ControlCaptura.ShowSecondControls = False
                ControlCaptura.Tipo = CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                ControlCaptura.Etiqueta = CampoRow.Nombre_Campo
                ControlCaptura.CampoCaptura = CampoCaptura
                ControlCaptura.ShowSecondControls = False
                ControlCaptura.ShowPrimaryControls = True
                ControlCaptura.ShowValidacionListasControls = False

                CampoCaptura.id = CampoRow.id_Campo
                CampoCaptura.Control = ControlCaptura
                CampoCaptura.Marca_Height_Campo = CampoRow.Marca_Height_Campo
                CampoCaptura.Marca_Width_Campo = CampoRow.Marca_Width_Campo
                CampoCaptura.Marca_X_Campo = CampoRow.Marca_X_Campo
                CampoCaptura.Marca_Y_Campo = CampoRow.Marca_Y_Campo
                CampoCaptura.Usa_Marca = CampoRow.Usa_Marca

                _Campos.Add(CampoCaptura)
            Next

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

        Protected Overrides Sub LoadConfig(ByRef nDBMCore As DBCore.DBCoreDataBaseManager, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short)
            Me.EsquemaDataTable = nDBMCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
            Me.DocumentoDataTable = nDBMImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(nEntidad, nProyecto, nEsquema, FileCoreRow.fk_Documento, False)

            Dim Orden = New DBImaging.SchemaConfig.CTA_CampoEnumList()
            Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.fk_Documento, True)
            Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.Orden_Campo, True)
            Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.id_Campo, True)
            Me.CamposDataTable = nDBMImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_DocumentoEliminado_Campofk_Proyectoid_EsquemaUsa_Captura_Proceso_Adicional(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, False, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, 0, Orden)

            ' Para los campos trigger se toma la configuración de primera captura
            Me.TriggersDataTable = nDBMImaging.SchemaConfig.TBL_Campo_Trigger.DBGet(DBImaging.EnumEtapaCaptura.Captura, FileCoreRow.fk_Documento, Nothing, Nothing, Nothing, Nothing)
            Me.TriggersValidacionesDataTable = nDBMImaging.SchemaConfig.TBL_Campo_Trigger_Validacion.DBGet(DBImaging.EnumEtapaCaptura.Captura, FileCoreRow.fk_Documento, Nothing, Nothing, Nothing)


            ' Lista Item
            Me.ListaItemsDataTable = nDBMCore.SchemaConfig.PA_Campo_Lista_Item_getDocumento.DBExecute(FileCoreRow.fk_Documento)

            Dim Orden_Validacion = New DBImaging.SchemaConfig.CTA_ValidacionEnumList()
            Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.fk_Documento, True)
            Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.Orden_Validacion, True)
            Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.id_Validacion, True)
            Me.ValidacionesDataTable = nDBMImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(FileCoreRow.fk_Documento, DBImaging.EnumEtapaCaptura.Captura, 0, Orden_Validacion)

            Me.TablaAsociadaDataTable = nDBMImaging.SchemaConfig.CTA_Tabla_Asociada.DBFindByfk_DocumentoEliminado_Campo(FileCoreRow.fk_Documento, False)

            ' Leer datos mesa de control
            Me.MesaControlDataTable = nDBMImaging.SchemaProcess.TBL_File_Data_MC.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, Nothing, Nothing)

            ' Anexos del documento
            Me.AnexosDataTable = nDBMCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

            'Llena los campos lista para la seleccion de motivos
            Me.MotivosDataTable = nDBMCore.SchemaConfig.TBL_Campo_Lista_Item.DBGet(nEntidad, Nothing, Nothing)

            Me.IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
            Me.IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)
        End Sub

#End Region

    End Class

End Namespace