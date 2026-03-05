Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports Slyg.Tools

Namespace Controller.Indexer.Capturas.Captura

    Public Class PrimeraCapturaController
        Inherits CapturasController

#Region " Declaraciones "

        Protected MesaControlDataTable As New DBImaging.SchemaProcess.TBL_File_Data_MCDataTable()
        Protected MesaControlCamposLlaveDataTable As New DBImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MCDataTable()
        Dim fk_Esquema As Integer
        Protected MesaControlDataTableAnexo As New DBImaging.SchemaProcess.TBL_Anexo_Data_MCDataTable()

#End Region

#Region " Propiedades "

        Protected Overrides ReadOnly Property ProcessName As String
            Get
                Return "Primera Captura"
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
            IndexerView.ShowDataPanel(True)
            IndexerView.ShowCamposLlavePanel(True)
            IndexerView.ShowValidationsPanel(True)
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

            If FileCoreRow IsNot Nothing Then
                Dim Productividad As New DesktopConfig.ProductividadType

                Productividad.Usuario = Me.IndexerSesion.Usuario.id

                If (Validar()) Then
                    Try
                        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                        Dim FechaInicioProceso = Now

                        Try

                            Dim ValidarSave As Boolean = True

                            dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                            dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                            dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                            'Valida label Primera Captura
                            Try
                                EventManager.ValidarSaveLabelCaptura(_Campos, FileCoreRow.fk_Documento, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, "1", ValidarSave)

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
                                        Dim dtResult = dbmImaging.SchemaProcess.PA_Validar_Campo_Duplicado.DBExecute(CInt(FileImagingRow.fk_Expediente), FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileCoreRow.fk_Documento, id_Campo, Nombre_Campo, Trim(Label), CStr(1))
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
                                Dim FileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                                If (FileBloqueadoDataTable(0).fk_Usuario_log <> Me.IndexerSesion.Usuario.id) Then
                                    dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Captura, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PcName)
                                End If

                            Catch : End Try
                            '--------------------------------------------------------------------------------------

                            'dbmCore.LinkDataBaseManager(dbmImaging)
                            'dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                            dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                            'dbmCore.Transaction_Begin()

                            ' Actualizar estados
                            Dim NextEstado = dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(CurrentDocumentFile.TipoDocumento, DBCore.EstadoEnum.Captura)

                            ' Establece estado heredado del doccumento de captura campos llaves, cuando este no esta en el expediente.
                            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                                If (NextEstado = DBCore.EstadoEnum.Indexado) Then
                                    NextEstado = dbmImaging.SchemaProcess.PA_Next_Estado_Documento_Captura_Campos_Llave.DBExecute(Me.IndexerImagingGlobal.Entidad, Me.IndexerImagingGlobal.Proyecto, Me.fk_Esquema, CType(FileCoreRow.fk_Expediente, Global.Slyg.Tools.SlygNullable(Of Integer)), FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Captura)
                                End If
                            End If

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
                                End If
                            End If

                            ' Reportar usuario captura
                            Dim FileType = New DBImaging.SchemaProcess.TBL_FileType()
                            FileType.Usuario_Primera_Captura = Me.IndexerSesion.Usuario.id
                            FileType.Fecha_Primera_Captura = SlygNullable.SysDate
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

                                            ' Data para segunda captura
                                            If (CType(NextEstado, DBCore.EstadoEnum) = DBCore.EstadoEnum.Segunda_Captura) Then
                                                dbmImaging.SchemaProcess.TBL_File_Data_MC.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, FileImagingRow.id_Version)

                                                Dim FileDataMCType As New DBImaging.SchemaProcess.TBL_File_Data_MCType()
                                                FileDataMCType.fk_Expediente = FileCoreRow.fk_Expediente
                                                FileDataMCType.fk_Folder = FileCoreRow.fk_Folder
                                                FileDataMCType.fk_File = FileCoreRow.id_File
                                                FileDataMCType.fk_Version = FileImagingRow.id_Version
                                                FileDataMCType.fk_Documento = FileCoreRow.fk_Documento
                                                FileDataMCType.fk_Campo = Campo.id
                                                FileDataMCType.Valor_Primera_Captura = Campo.Control.Value
                                                dbmImaging.SchemaProcess.TBL_File_Data_MC.DBInsert(FileDataMCType)
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

                                        ' Data para segunda captura
                                        If (CType(NextEstado, DBCore.EstadoEnum) = DBCore.EstadoEnum.Segunda_Captura) Then
                                            dbmImaging.SchemaProcess.TBL_File_Data_MC.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, FileImagingRow.id_Version)
                                            dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version, FileCoreRow.fk_Documento, Campo.id, Nothing, Nothing)

                                            Dim FileDataMCType As New DBImaging.SchemaProcess.TBL_File_Data_MCType()
                                            FileDataMCType.fk_Expediente = FileCoreRow.fk_Expediente
                                            FileDataMCType.fk_Folder = FileCoreRow.fk_Folder
                                            FileDataMCType.fk_File = FileCoreRow.id_File
                                            FileDataMCType.fk_Version = FileImagingRow.id_Version
                                            FileDataMCType.fk_Documento = FileCoreRow.fk_Documento
                                            FileDataMCType.fk_Campo = Campo.id
                                            FileDataMCType.Valor_Primera_Captura = 1
                                            dbmImaging.SchemaProcess.TBL_File_Data_MC.DBInsert(FileDataMCType)
                                        End If

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

                                                ' Data para segunda captura
                                                If (CType(NextEstado, DBCore.EstadoEnum) = DBCore.EstadoEnum.Segunda_Captura) Then

                                                    Dim FileDataMCAsociadaType As New DBImaging.SchemaProcess.TBL_File_Data_MC_AsociadaType()
                                                    FileDataMCAsociadaType.fk_Expediente = FileCoreRow.fk_Expediente
                                                    FileDataMCAsociadaType.fk_Folder = FileCoreRow.fk_Folder
                                                    FileDataMCAsociadaType.fk_File = FileCoreRow.id_File
                                                    FileDataMCAsociadaType.fk_Version = FileImagingRow.id_Version
                                                    FileDataMCAsociadaType.fk_Documento = FileCoreRow.fk_Documento
                                                    FileDataMCAsociadaType.fk_Campo = Campo.id
                                                    FileDataMCAsociadaType.fk_Campo_Tabla = CShort(TablaAsociadaRow.id_Campo_Tabla)
                                                    FileDataMCAsociadaType.fk_File_Data_Asociada = CShort(Fil)
                                                    FileDataMCAsociadaType.Valor_Primera_Captura = TableValue
                                                    dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBInsert(FileDataMCAsociadaType)
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
                                    'Dim QueryRecuperacion = dbmCore.DataBase.ExecuteStoredProcedureGet("DBO", "PA_CAMPO_QUERY_" & CStr(Me.ExpedienteRow.fk_Entidad) & "_" & CStr(FileCoreRow.fk_Documento) & "_" & CStr(Campo.id), Parametros)
                                    'If QueryRecuperacion.Rows.Count > 0 Then Value = Utilities.DStr(QueryRecuperacion.Rows(0)(0).ToString())
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

                            'Almacena campos llaves 
                            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                                If _CamposLlave.Count > 0 Then
                                    Dim Llaves As String = ""
                                    Dim CampoEmpaque As String = ""

                                    For Each CampoLlave In _CamposLlave
                                        If CampoLlave.Control.IsVisible Then
                                            If CInt(CampoLlave.fk_Tipo_Llave) = 1 Then
                                                Llaves = Llaves + CampoLlave.Numero_Llave.ToString() + ":" + CampoLlave.Control.Value.ToString() + "|"
                                            ElseIf CInt(CampoLlave.fk_Tipo_Llave) = 2 Then
                                                CampoEmpaque = CampoLlave.Control.Value.ToString()
                                            End If
                                            Productividad.CamposLlave += 1
                                            Productividad.Caracteres += CampoLlave.Control.Value.ToString().Length

                                            ' Data para segunda captura
                                            If (CType(NextEstado, DBCore.EstadoEnum) = DBCore.EstadoEnum.Segunda_Captura) Then
                                                dbmImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MC.DBDelete(FileCoreRow.fk_Expediente, CampoLlave.Id_Campo)

                                                Dim FileDataMCType As New DBImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MCType()
                                                FileDataMCType.fk_Expediente = FileCoreRow.fk_Expediente
                                                FileDataMCType.fk_Campo_LLave = CShort(CampoLlave.Id_Campo)
                                                FileDataMCType.Valor_Primera_Captura = CampoLlave.Control.Value.ToString()
                                                dbmImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MC.DBInsert(FileDataMCType)
                                            End If
                                        End If
                                    Next

                                    dbmCore.SchemaProcess.PA_Guarda_Expediente_Llave_En_Linea.DBExecute(FileCoreRow.fk_Expediente, CShort(_CamposLlave(0).fk_Entidad), CShort(_CamposLlave(0).fk_Proyecto), CShort(_CamposLlave(0).fk_Esquema), Llaves, CampoEmpaque)
                                End If
                            End If

                            'Almacenar validaciones
                            For Each item In _Validaciones
                                If item.Control.Visible = True Then
                                    If (dbmCore.SchemaProcess.TBL_File_Validacion.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, FileCoreRow.fk_Documento).Count > 0) Then
                                        'dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(Nothing, Nothing, Nothing, Nothing, item.Control.Value, item.Control.Motivo, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, FileCoreRow.fk_Documento)
                                        dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(Nothing, Nothing, Nothing, Nothing, item.Control.ValueControl, item.Control.Motivo, FileCoreRow.fk_Documento, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, FileCoreRow.fk_Documento)
                                    Else
                                        dbmCore.SchemaProcess.TBL_File_Validacion.DBInsert(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, item.Control.ValueControl, item.Control.Motivo, FileCoreRow.fk_Documento)
                                    End If
                                End If
                            Next

                            ' Insertar productividad
                            InsertarProductividad(dbmImaging, Productividad, DesktopConfig.Etapa_Productividad.Primera_Captura, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                            ' Actualizar llaves
                            dbmCore.SchemaProcess.PA_Calcular_Llaves.DBExecute(FileImagingRow.fk_Expediente)

                            ' Aplicar validaciones automáticas
                            dbmImaging.SchemaProcess.PA_Aplicar_Validaciones_Automaticas.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento)

                            ' Preparar los recortes
                            If (NextEstado = DBCore.EstadoEnum.Recorte) Then
                                If Not (Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Recortes AndAlso dbmImaging.SchemaProcess.PA_Preparar_Recortes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)) Then
                                    NextEstado = DBCore.EstadoEnum.Indexado
                                End If
                            End If

                            ' Establece estado heredado del doccumento de captura campos llaves, cuando este no esta en el expediente.
                            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                                If (NextEstado = DBCore.EstadoEnum.Indexado) Then
                                    NextEstado = dbmImaging.SchemaProcess.PA_Next_Estado_Documento_Captura_Campos_Llave.DBExecute(Me.IndexerImagingGlobal.Entidad, Me.IndexerImagingGlobal.Proyecto, Me.fk_Esquema, CType(FileCoreRow.fk_Expediente, Global.Slyg.Tools.SlygNullable(Of Integer)), FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Captura)
                                End If
                            End If

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
                                dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                            Else
                                Dim CapDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                                CapDashboardType.fk_Usuario_log = DBNull.Value
                                CapDashboardType.Sesion = DBNull.Value
                                CapDashboardType.fk_Estado = NextEstado
                                dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(CapDashboardType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                            End If
                            '---------------------------------------------------------------------------

                            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Cruce_Linea And Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Cruce_Generico Then
                                dbmImaging.SchemaProcess.PA_Cruce_En_Linea_Cola.DBExecute(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, "1")
                            End If

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
                            'If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                            DesktopMessageBoxControl.DesktopMessageShow("Publicar_Primera", ex)

                            Return False
                        Finally
                            'If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        End Try


                        EventManager.FinalizarPrimeraCaptura(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                        '-------------------------------------------------------------------------------------------------------------
                        ' LOGGING - PERFORMANCE
                        '-------------------------------------------------------------------------------------------------------------
                        Dim FechaFinProceso = Now
                        Dim TraceMessage As String = "[Primera Captura][Publicar] - Inicio: " & FechaInicioProceso.ToString() & " Fin: " & FechaFinProceso.ToString()
                        DesktopTrace.Trace(TraceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, Program.AssemblyTitle)
                        '-------------------------------------------------------------------------------------------------------------

                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("Publicar_Primera", ex)

                        Return False
                    End Try

                    Return True


                End If

                Return False

            ElseIf AnexoImagingRow IsNot Nothing Then
                Dim Productividad As New DesktopConfig.ProductividadType

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

                            ''--------------------------------------------------------------------------------------
                            '' REPORTAR DUPLICADOS
                            ''--------------------------------------------------------------------------------------
                            'Try
                            '    Dim FileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                            '    If (FileBloqueadoDataTable(0).fk_Usuario_log <> Me.IndexerSesion.Usuario.id) Then
                            '        dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Captura, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PcName)
                            '    End If

                            'Catch : End Try
                            '--------------------------------------------------------------------------------------


                            dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)


                            ' Actualizar estados
                            Dim NextEstado = dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(CurrentDocumentFile.TipoDocumento, DBCore.EstadoEnum.Captura)


                            ''Crea el estado del Folder en Core
                            'If (CType(NextEstado, DBCore.EstadoEnum) = DBCore.EstadoEnum.Indexado) Then
                            '    Dim FolderEstadoDataTable = dbmCore.SchemaProcess.TBL_Folder_estado.DBFindByfk_Expedientefk_FolderModulo(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, DesktopConfig.Modulo.Imaging)

                            '    If FolderEstadoDataTable.Count = 0 Then
                            '        Dim InsertEstado As New DBCore.SchemaProcess.TBL_Folder_estadoType()
                            '        InsertEstado.fk_Expediente = FileImagingRow.fk_Expediente
                            '        InsertEstado.fk_Folder = FileImagingRow.fk_Folder
                            '        InsertEstado.Modulo = DesktopConfig.Modulo.Imaging
                            '        InsertEstado.fk_Estado = DBCore.EstadoEnum.Indexado
                            '        InsertEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                            '        InsertEstado.Fecha_Log = SlygNullable.SysDate
                            '        dbmCore.SchemaProcess.TBL_Folder_estado.DBInsert(InsertEstado)
                            '    End If
                            'End If


                            ' Reportar usuario captura
                            Dim AnexoType = New DBImaging.SchemaProcess.TBL_AnexoType()
                            AnexoType.Usuario_Primera_Captura = Me.IndexerSesion.Usuario.id
                            AnexoType.Fecha_Primera_Captura = SlygNullable.SysDate
                            dbmImaging.SchemaProcess.TBL_Anexo.DBUpdate(AnexoType, AnexoImagingRow.id_Anexo)

                            ' Crear datos de primera captura a partir de captura
                            Dim CamposQuery As New List(Of CampoCaptura)
                            For Each Campo In _Campos
                                If Campo.Control.IsVisible Then
                                    ' Eliminar todos los datos de doble captura
                                    If (Campo.Control.Tipo <> DesktopConfig.CampoTipo.TablaAsociada) Then
                                        ' Leer la data almacenada
                                        If Campo.Control.Tipo <> DesktopConfig.CampoTipo.Query Then
                                            Dim AnexoDataCampo = dbmCore.SchemaProcess.TBL_Anexo_Data.DBFindByfk_Anexofk_Documentofk_Campo(AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id)

                                            Dim AnexoDataType As New DBCore.SchemaProcess.TBL_Anexo_DataType()

                                            AnexoDataType.Valor_File_Data = Campo.Control.Value
                                            AnexoDataType.Conteo_File_Data = Campo.Control.Value.ToString().Length

                                            If (AnexoDataCampo.Count = 0) Then
                                                AnexoDataType.fk_Anexo = AnexoImagingRow.id_Anexo
                                                AnexoDataType.fk_Documento = AnexoImagingRow.fk_Documento
                                                AnexoDataType.fk_Campo = Campo.id
                                                dbmCore.SchemaProcess.TBL_Anexo_Data.DBInsert(AnexoDataType)
                                            Else
                                                dbmCore.SchemaProcess.TBL_Anexo_Data.DBUpdate(AnexoDataType, AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id)
                                            End If

                                            ' Data para segunda captura
                                            If (CType(NextEstado, DBCore.EstadoEnum) = DBCore.EstadoEnum.Segunda_Captura) Then
                                                dbmImaging.SchemaProcess.TBL_Anexo_Data_MC.DBDelete(AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id)

                                                Dim AnexoDataMCType As New DBImaging.SchemaProcess.TBL_Anexo_Data_MCType()
                                                AnexoDataMCType.fk_Anexo = AnexoImagingRow.id_Anexo
                                                AnexoDataMCType.fk_Documento = AnexoImagingRow.fk_Documento
                                                AnexoDataMCType.fk_Campo = Campo.id
                                                AnexoDataMCType.Valor_Primera_Captura = Campo.Control.Value
                                                dbmImaging.SchemaProcess.TBL_Anexo_Data_MC.DBInsert(AnexoDataMCType)
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

                                        ' Data para segunda captura
                                        If (CType(NextEstado, DBCore.EstadoEnum) = DBCore.EstadoEnum.Segunda_Captura) Then
                                            dbmImaging.SchemaProcess.TBL_Anexo_Data_MC.DBDelete(AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id)
                                            dbmImaging.SchemaProcess.TBL_Anexo_Data_MC_Asociada.DBDelete(AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id, Nothing, Nothing)

                                            Dim AnexoDataMCType As New DBImaging.SchemaProcess.TBL_Anexo_Data_MCType()
                                            AnexoDataMCType.fk_Anexo = AnexoImagingRow.id_Anexo
                                            AnexoDataMCType.fk_Documento = AnexoImagingRow.fk_Documento
                                            AnexoDataMCType.fk_Campo = Campo.id
                                            AnexoDataMCType.Valor_Primera_Captura = 1
                                            dbmImaging.SchemaProcess.TBL_Anexo_Data_MC.DBInsert(AnexoDataMCType)
                                        End If

                                        For Each TablaAsociadaRow As DBImaging.SchemaConfig.CTA_Tabla_AsociadaRow In TablaAsociadaDataTable.Select("fk_Campo = " & Campo.id)
                                            Dim Fil As Integer = 1
                                            Dim TablaControl = CType(Campo.Control, TableInputControl)

                                            For Each Registro As DataRow In ValueTable.Rows
                                                Dim TableValue As String

                                                TableValue = Registro.Item(Col).ToString()

                                                If (dbmCore.SchemaProcess.TBL_Anexo_Data_Asociada.DBGet(AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil)).Count = 0) Then
                                                    dbmCore.SchemaProcess.TBL_Anexo_Data_Asociada.DBInsert(AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil), TableValue, TableValue.Length)
                                                Else
                                                    dbmCore.SchemaProcess.TBL_Anexo_Data_Asociada.DBUpdate(Nothing, Nothing, Nothing, Nothing, Nothing, TableValue, TableValue.Length, AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil))
                                                End If

                                                ' Data para segunda captura
                                                If (CType(NextEstado, DBCore.EstadoEnum) = DBCore.EstadoEnum.Segunda_Captura) Then

                                                    Dim AnexoDataMCAsociadaType As New DBImaging.SchemaProcess.TBL_Anexo_Data_MC_AsociadaType()
                                                    AnexoDataMCAsociadaType.fk_Anexo = AnexoImagingRow.id_Anexo
                                                    AnexoDataMCAsociadaType.fk_Documento = AnexoImagingRow.fk_Documento
                                                    AnexoDataMCAsociadaType.fk_Campo = Campo.id
                                                    AnexoDataMCAsociadaType.fk_Campo_Tabla = CShort(TablaAsociadaRow.id_Campo_Tabla)
                                                    AnexoDataMCAsociadaType.fk_File_Data_Asociada = CShort(Fil)
                                                    AnexoDataMCAsociadaType.Valor_Primera_Captura = TableValue
                                                    dbmImaging.SchemaProcess.TBL_Anexo_Data_MC_Asociada.DBInsert(AnexoDataMCAsociadaType)
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
                            'For Each Campo In CamposQuery
                            '    Try
                            '        Dim Parametros As New List(Of Slyg.Data.Schemas.Parameter)
                            '        Dim Par1 As New Slyg.Data.Schemas.Parameter("@Expediente", FileCoreRow.fk_Expediente)
                            '        Dim Par2 As New Slyg.Data.Schemas.Parameter("@Folder", FileCoreRow.fk_Folder)
                            '        Dim Par3 As New Slyg.Data.Schemas.Parameter("@File", FileCoreRow.id_File)
                            '        Parametros.Add(Par1) : Parametros.Add(Par2) : Parametros.Add(Par3)
                            '        'Dim QueryRecuperacion = dbmCore.DataBase.ExecuteStoredProcedureGet("DBO", "PA_CAMPO_QUERY_" & CStr(Me.ExpedienteRow.fk_Entidad) & "_" & CStr(FileCoreRow.fk_Documento) & "_" & CStr(Campo.id), Parametros)
                            '        'If QueryRecuperacion.Rows.Count > 0 Then Value = Utilities.DStr(QueryRecuperacion.Rows(0)(0).ToString())
                            '        Dim FileDataCampo = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)

                            '        Dim FileDataType As New DBCore.SchemaProcess.TBL_File_DataType()
                            '        FileDataType.Valor_File_Data = Campo.Control.Value
                            '        FileDataType.Conteo_File_Data = Campo.Control.Value.ToString().Length

                            '        If (FileDataCampo.Count = 0) Then
                            '            FileDataType.fk_Expediente = FileCoreRow.fk_Expediente
                            '            FileDataType.fk_Folder = FileCoreRow.fk_Folder
                            '            FileDataType.fk_File = FileCoreRow.id_File
                            '            FileDataType.fk_Documento = FileCoreRow.fk_Documento
                            '            FileDataType.fk_Campo = Campo.id

                            '            dbmCore.SchemaProcess.TBL_File_Data.DBInsert(FileDataType)
                            '        Else
                            '            dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(FileDataType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)
                            '        End If
                            '    Catch ex As Exception
                            '        Throw New Exception("Ha ocurrido un error en el campo [" & Campo.id & "] de tipo [QUERY], es posible que contenga errores o no exista una definicion para este. --[" & ex.Message & "]--")
                            '    End Try
                            'Next


                            ''Almacenar validaciones
                            'For Each item In _Validaciones
                            '    If item.Control.Visible = True Then
                            '        If (dbmCore.SchemaProcess.TBL_File_Validacion.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, FileCoreRow.fk_Documento).Count > 0) Then
                            '            'dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(Nothing, Nothing, Nothing, Nothing, item.Control.Value, item.Control.Motivo, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, FileCoreRow.fk_Documento)
                            '            dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(Nothing, Nothing, Nothing, Nothing, item.Control.ValueControl, item.Control.Motivo, FileCoreRow.fk_Documento, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, FileCoreRow.fk_Documento)
                            '        Else
                            '            dbmCore.SchemaProcess.TBL_File_Validacion.DBInsert(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, item.Control.ValueControl, item.Control.Motivo, FileCoreRow.fk_Documento)
                            '        End If
                            '    End If
                            'Next

                            '' Insertar productividad
                            'InsertarProductividad(dbmImaging, Productividad, DesktopConfig.Etapa_Productividad.Primera_Captura, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                            '' Actualizar llaves
                            'dbmCore.SchemaProcess.PA_Calcular_Llaves.DBExecute(FileImagingRow.fk_Expediente)

                            '' Aplicar validaciones automáticas
                            'dbmImaging.SchemaProcess.PA_Aplicar_Validaciones_Automaticas.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento)

                            ' Actualizar estado
                            Dim UpdateEstado As New DBCore.SchemaProcess.TBL_Anexo_EstadoType()
                            UpdateEstado.Fecha_Log = SlygNullable.SysDate
                            UpdateEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                            UpdateEstado.fk_Estado = NextEstado
                            dbmCore.SchemaProcess.TBL_Anexo_Estado.DBUpdate(UpdateEstado, AnexoImagingRow.id_Anexo, DesktopConfig.Modulo.Imaging)

                            '---------------------------------------------------------------------------
                            ' Actualizar Dashboard
                            '---------------------------------------------------------------------------
                            If (NextEstado = DBCore.EstadoEnum.Indexado) Then
                                dbmImaging.SchemaProcess.TBL_Dashboard_Capturas_Anexo.DBDelete(AnexoImagingRow.id_Anexo)
                            Else
                                Dim CapDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_Capturas_AnexoType()
                                CapDashboardType.fk_Usuario_log = DBNull.Value
                                CapDashboardType.Sesion = DBNull.Value
                                CapDashboardType.fk_Estado = NextEstado
                                dbmImaging.SchemaProcess.TBL_Dashboard_Capturas_Anexo.DBUpdate(CapDashboardType, AnexoImagingRow.id_Anexo)
                            End If
                            '---------------------------------------------------------------------------

                            'dbmCore.Transaction_Commit()
                            dbmImaging.Transaction_Commit()

                            '' Actualizar el estado de los cargues que ya fueron procesados
                            'Try
                            '    dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                            '    Dim CarguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(_idCargue, _idCarguePaquete)
                            '    If (CarguePaqueteFileDataTable.Count > 0) Then
                            '        Dim UpdatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                            '        UpdatePaquete.fk_Estado = CarguePaqueteFileDataTable(0).Estado_File
                            '        dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(UpdatePaquete, _idCargue, _idCarguePaquete)

                            '        Dim CargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(_idCargue)
                            '        If (CargueFileDataTable.Count > 0) Then
                            '            Dim UpdateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                            '            UpdateCargue.fk_Estado = CargueFileDataTable(0).Estado_File
                            '            dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(UpdateCargue, _idCargue)
                            '        End If
                            '    End If

                            '    dbmImaging.Transaction_Commit()
                            'Catch ex As Exception
                            '    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                            '    Throw
                            'End Try

                        Catch ex As Exception
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                            DesktopMessageBoxControl.DesktopMessageShow("Publicar_Primera", ex)

                            Return False
                        Finally
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        End Try


                        'EventManager.FinalizarPrimeraCaptura(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)
                        EventManager.FinalizarPrimeraCapturaAnexo(AnexoImagingRow.id_Anexo)

                        '-------------------------------------------------------------------------------------------------------------
                        ' LOGGING - PERFORMANCE
                        '-------------------------------------------------------------------------------------------------------------
                        Dim FechaFinProceso = Now
                        Dim TraceMessage As String = "[Primera Captura][Publicar] - Inicio: " & FechaInicioProceso.ToString() & " Fin: " & FechaFinProceso.ToString()
                        DesktopTrace.Trace(TraceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, Program.AssemblyTitle)
                        '-------------------------------------------------------------------------------------------------------------

                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("Publicar_Primera", ex)

                        Return False
                    End Try

                    Return True

                End If

                Return False
            End If
            Return False
        End Function

        Public Overrides Function ValidacionListas() As Boolean
            Dim _ValidacionListas As Boolean

            _ValidacionListas = False

            Return _ValidacionListas
        End Function

        Public Overrides Function Campos(ByVal idDocumento As Integer) As List(Of CampoCaptura)
            Me._Campos.Clear()

            For Each CampoRow As DBImaging.SchemaConfig.CTA_CampoRow In CamposDataTable.Select("fk_Documento = " & idDocumento & " AND Usa_Captura = 1")
                Dim ControlCaptura As View.IInputControl
                Dim CampoCaptura As New CampoCaptura()
                Dim ListDefinicionCaptura As New List(Of DefinicionCaptura)

                Dim CampoOCR As DataRow()
                Dim CampoOCRRow As DBOCR.SchemaProcess.TBL_File_Data_CamposRow

                If _Usa_OCR_Captura Then
                    ' Busca un campo de extracción OCR relacionado con el campo actual en la tabla CamposOCRDataTable.
                    If CamposOCRTataTable IsNot Nothing Then
                        CampoOCR = CamposOCRTataTable.Select("fk_Campo = " & CampoRow.id_Campo.ToString())
                        CampoOCRRow = Nothing
                        If CampoOCR.Length > 0 Then
                            ' Si se encontró, asigna el primer resultado a CampoOCRRow.
                            CampoOCRRow = DirectCast(CampoOCR(0), DBOCR.SchemaProcess.TBL_File_Data_CamposRow)
                        End If
                    End If
                End If

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

                        ' Verifica si existe un Campo de la extracción OCR por su IDCampo.
                        If CampoOCRRow IsNot Nothing Then
                            ControlCaptura.Value = CampoOCRRow.Valor_Captura_OCR
                        ElseIf CampoRow.Valor_por_Defecto <> "" Then
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

                        ' Verifica si existe un Campo de la extracción OCR por su IDCampo.
                        If CampoOCRRow IsNot Nothing Then
                            Dim valor As Long
                            ' Intentar convertir el valor de cadena a entero
                            If Long.TryParse(CampoOCRRow.Valor_Captura_OCR, valor) Then
                                ControlCaptura.Value = valor
                            End If
                        ElseIf CampoRow.Valor_por_Defecto <> "" Then
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

                        ' Verifica si existe un Campo de la extracción OCR por su IDCampo.
                        If CampoOCRRow IsNot Nothing Then
                            ControlCaptura.Value = CampoOCRRow.Valor_Captura_OCR
                        ElseIf CampoRow.Valor_por_Defecto <> "" Then
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

                        ' Verifica si existe un Campo de la extracción OCR por su IDCampo.
                        If CampoOCRRow IsNot Nothing Then
                            ControlCaptura.Value = CampoOCRRow.Valor_Captura_OCR
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

                ' Establece las coordenadas obtenidas por el sistema de OCR en Bash
                If CampoOCRRow IsNot Nothing Then
                    CampoCaptura.Marca_Height_Campo = CampoOCRRow.Rectangle_Height
                    CampoCaptura.Marca_Width_Campo = CampoOCRRow.Rectangle_Width
                    CampoCaptura.Marca_X_Campo = CampoOCRRow.Rectangle_X
                    CampoCaptura.Marca_Y_Campo = CampoOCRRow.Rectangle_Y
                    CampoCaptura.Usa_Marca = True
                Else
                    CampoCaptura.Marca_Height_Campo = CampoRow.Marca_Height_Campo
                    CampoCaptura.Marca_Width_Campo = CampoRow.Marca_Width_Campo
                    CampoCaptura.Marca_X_Campo = CampoRow.Marca_X_Campo
                    CampoCaptura.Marca_Y_Campo = CampoRow.Marca_Y_Campo
                    CampoCaptura.Usa_Marca = CampoRow.Usa_Marca
                End If

                _Campos.Add(CampoCaptura)
            Next

            Return _Campos
        End Function

        Public Overrides Function CamposLlave(ByVal idDocumento As Integer) As List(Of CampoLlaveCaptura)
            Me._CamposLlave.Clear()

            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                If CamposLlaveDataTable.Count > 0 Then
                    For Each CampoRow As DBImaging.SchemaConfig.CTA_Campo_LlaveRow In CamposLlaveDataTable.Select("fk_Documento = " & idDocumento & " AND Usa_Captura = 1")

                        Dim ControlCaptura As View.IInputControl
                        Dim CampoLlaveCaptura As New CampoLlaveCaptura()
                        Dim ListDefinicionCaptura As New List(Of DefinicionCaptura)

                        ' Control
                        Select Case CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                            Case DesktopConfig.CampoTipo.Texto
                                Dim DefinicionCaptura = New DefinicionCaptura()
                                ControlCaptura = New TextInputControl()

                                DefinicionCaptura.Type = DesktopConfig.CampoTipo.Texto
                                DefinicionCaptura.MaximumLength = CampoRow.Length_Campo
                                DefinicionCaptura.MinimumLength = CampoRow.Length_Min_Campo
                                DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                                ListDefinicionCaptura.Add(DefinicionCaptura)
                                ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                            Case DesktopConfig.CampoTipo.Numerico
                                Dim DefinicionCaptura = New DefinicionCaptura()
                                ControlCaptura = New TextInputNumericControl()

                                DefinicionCaptura.Type = DesktopConfig.CampoTipo.Numerico
                                DefinicionCaptura.MaximumLength = CampoRow.Length_Campo
                                DefinicionCaptura.MinimumLength = CampoRow.Length_Min_Campo
                                DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                                ListDefinicionCaptura.Add(DefinicionCaptura)
                                ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                            Case DesktopConfig.CampoTipo.Fecha
                                Dim DefinicionCaptura = New DefinicionCaptura()
                                ControlCaptura = New TextInputDateTimeControl()

                                DefinicionCaptura.Type = DesktopConfig.CampoTipo.Fecha
                                DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                                ListDefinicionCaptura.Add(DefinicionCaptura)
                                ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                            Case Else
                                Throw New Exception("Tipo de campo no reconocido: " & CampoRow.fk_Campo_Tipo)

                        End Select

                        ControlCaptura.ShowSecondControls = False
                        ControlCaptura.Tipo = CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                        ControlCaptura.Etiqueta = CampoRow.Nombre_Campo
                        ControlCaptura.CampoLlaveCaptura = CampoLlaveCaptura
                        ControlCaptura.ShowSecondControls = False
                        ControlCaptura.ShowPrimaryControls = True
                        ControlCaptura.ShowValidacionListasControls = False

                        CampoLlaveCaptura.id = CampoRow.id_Campo
                        CampoLlaveCaptura.Control = ControlCaptura
                        CampoLlaveCaptura.Id_Campo = CampoRow.id_Campo
                        CampoLlaveCaptura.Numero_Llave = CampoRow.Numero_Llave
                        CampoLlaveCaptura.fk_Entidad = CampoRow.fk_Entidad
                        CampoLlaveCaptura.fk_Proyecto = CampoRow.fk_Proyecto
                        CampoLlaveCaptura.fk_Esquema = CampoRow.id_Esquema
                        CampoLlaveCaptura.fk_Tipo_Llave = CampoRow.fk_Tipo_Llave
                        CampoLlaveCaptura.Marca_Height_Campo = CampoRow.Marca_Height_Campo
                        CampoLlaveCaptura.Marca_Width_Campo = CampoRow.Marca_Width_Campo
                        CampoLlaveCaptura.Marca_X_Campo = CampoRow.Marca_X_Campo
                        CampoLlaveCaptura.Marca_Y_Campo = CampoRow.Marca_Y_Campo
                        CampoLlaveCaptura.Usa_Marca = CampoRow.Usa_Marca

                        _CamposLlave.Add(CampoLlaveCaptura)
                    Next

                End If
            End If

            Return _CamposLlave
        End Function

        Public Overrides Function GetValueFileData(ByVal idCampo As Integer) As String

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ValueFileData As String = Nothing

            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Carga_Datos_Captura Then
                Try

                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                    dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                    dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)


                    Dim CBarrasCampo = dbmImaging.SchemaConfig.TBL_CBarras_Campo.DBFindByfk_Documentofk_Campo(FileCoreRow.fk_Documento, CShort(idCampo))

                    If CBarrasCampo.Rows.Count > 0 Then
                        Dim FileDataValues = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_File(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                        If FileDataValues.Rows.Count > 0 Then
                            For Each campo In IndexerView.Campos
                                If campo.id = idCampo Then

                                    Dim ValueRow As DataRow()
                                    ValueRow = FileDataValues.Select("fk_Campo = " & campo.id)

                                    If ValueRow.Count > 0 Then
                                        'Asigna Valor File Data
                                        ValueFileData = ValueRow(0).Item("Valor_File_Data").ToString
                                        campo.Control.Value = ValueFileData
                                    End If

                                    Exit For
                                End If
                            Next
                        End If

                    End If

                Catch ex As Exception
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                    DesktopMessageBoxControl.DesktopMessageShow("Se ha presentado un error al obtener valor de captura", ex)
                    Return ""

                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try

            End If

            Return ValueFileData

        End Function

        Public Overrides Function GetValueLlaveData() As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow

            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ValueFileData As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow = Nothing
            Dim ValueFileDataTable As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaDataTable = Nothing

            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Carga_Datos_Captura Then
                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                    dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                    dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                    If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                        ValueFileDataTable = dbmCore.SchemaProcess.TBL_Expediente_Llave_Linea.DBFindByfk_Expediente(FileImagingRow.fk_Expediente)

                        If ValueFileDataTable.Rows.Count > 0 Then
                            ValueFileData = ValueFileDataTable(0)
                        End If
                    End If

                Catch ex As Exception
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                    DesktopMessageBoxControl.DesktopMessageShow("Se ha presentado un error al obtener valor de captura de las llaves", ex)
                    Return Nothing

                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If

            Return ValueFileData

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

            Try

                'Llena el dato si Usa OCR Captura
                'Me._Usa_OCR_Captura = Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_OCR_Captura

                If AnexoImagingRow IsNot Nothing Then
                    Me.EsquemaDataTable = nDBMCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
                    Me.DocumentoDataTable = nDBMImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(nEntidad, nProyecto, nEsquema, AnexoImagingRow.fk_Documento, False)
                    Me.fk_Esquema = nEsquema


                    Dim Orden = New DBImaging.SchemaConfig.CTA_CampoEnumList()
                    Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.fk_Documento, True)
                    Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.Orden_Campo, True)
                    Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.id_Campo, True)
                    Me.CamposDataTable = nDBMImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_Documentofk_Proyectoid_EsquemaUsa_CapturaEs_Campo_IndexacionEliminado_Campo(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, AnexoImagingRow.fk_Documento, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, Nothing, False, 0, Orden)


                    ' Para los campos trigger se toma la configuración de primera captura
                    Me.TriggersDataTable = nDBMImaging.SchemaConfig.TBL_Campo_Trigger.DBGet(DBImaging.EnumEtapaCaptura.Captura, AnexoImagingRow.fk_Documento, Nothing, Nothing, Nothing, Nothing)
                    Me.TriggersValidacionesDataTable = nDBMImaging.SchemaConfig.TBL_Campo_Trigger_Validacion.DBGet(DBImaging.EnumEtapaCaptura.Captura, AnexoImagingRow.fk_Documento, Nothing, Nothing, Nothing)


                    ' Lista Item
                    Me.ListaItemsDataTable = nDBMCore.SchemaConfig.PA_Campo_Lista_Item_getDocumento.DBExecute(AnexoImagingRow.fk_Documento)

                    Dim Orden_Validacion = New DBImaging.SchemaConfig.CTA_ValidacionEnumList()
                    Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.fk_Documento, True)
                    Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.Orden_Validacion, True)
                    Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.id_Validacion, True)
                    Me.ValidacionesDataTable = nDBMImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(AnexoImagingRow.fk_Documento, DBImaging.EnumEtapaCaptura.Captura, 0, Orden_Validacion)

                    Me.TablaAsociadaDataTable = nDBMImaging.SchemaConfig.CTA_Tabla_Asociada.DBFindByfk_DocumentoEliminado_Campo(AnexoImagingRow.fk_Documento, False)

                    ' Leer datos mesa de control
                    Me.MesaControlDataTableAnexo = nDBMImaging.SchemaProcess.TBL_Anexo_Data_MC.DBGet(AnexoImagingRow.id_Anexo, Nothing, Nothing)

                    'Llena los campos lista para la seleccion de motivos
                    Me.MotivosDataTable = nDBMCore.SchemaConfig.TBL_Campo_Lista_Item.DBGet(nEntidad, Nothing, Nothing)

                    Me.IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
                    Me.IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)

                    EventManager.FinalizarLoadConfig(IndexerView, FileCoreRow)
                Else
                    Me.EsquemaDataTable = nDBMCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
                    Me.DocumentoDataTable = nDBMImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(nEntidad, nProyecto, nEsquema, FileCoreRow.fk_Documento, False)
                    Me.fk_Esquema = nEsquema

                    Dim Orden = New DBImaging.SchemaConfig.CTA_CampoEnumList()
                    Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.fk_Documento, True)
                    Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.Orden_Campo, True)
                    Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.id_Campo, True)
                    Me.CamposDataTable = nDBMImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_Documentofk_Proyectoid_EsquemaUsa_CapturaEs_Campo_IndexacionEliminado_Campo(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, Nothing, False, 0, Orden)

                    If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                        Me.CamposLlaveDataTable = nDBMImaging.SchemaConfig.CTA_Campo_Llave.DBFindByfk_Entidadfk_DocumentoEliminado_Campofk_Proyectoid_EsquemaUsa_CapturaUsa_Doble_CapturaEs_Campo_Indexacion(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, False, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, Nothing, Nothing)

                        'Busca campos llaves asignados al DocPpal para capturarselos al File1 del Expediente
                        If CamposLlaveDataTable.Count = 0 Then
                            Me.CamposLlaveDataTable = nDBMImaging.SchemaProcess.PA_Busca_Campos_Llave_DocPpal.DBExecute(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, FileCoreRow.fk_Documento, CInt(FileImagingRow.fk_Expediente), CInt(FileImagingRow.fk_Folder), CInt(FileImagingRow.fk_File))
                        End If
                    End If

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
                    If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                        Me.MesaControlCamposLlaveDataTable = nDBMImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MC.DBFindByfk_Expediente(FileImagingRow.fk_Expediente)
                    End If

                    ' Anexos del documento
                    Me.AnexosDataTable = nDBMCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                    'Llena los campos lista para la seleccion de motivos
                    Me.MotivosDataTable = nDBMCore.SchemaConfig.TBL_Campo_Lista_Item.DBGet(nEntidad, Nothing, Nothing)

                    Me.IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
                    Me.IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)

                    EventManager.FinalizarLoadConfig(IndexerView, FileCoreRow)
                End If

            Catch ex As Exception

                DesktopMessageBoxControl.DesktopMessageShow("Se ha presentado un error al obtener valor de captura", ex)

            Finally

            End Try


        End Sub

#End Region

    End Class

End Namespace