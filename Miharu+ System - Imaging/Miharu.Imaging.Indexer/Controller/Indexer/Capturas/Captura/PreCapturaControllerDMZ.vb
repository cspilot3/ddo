Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports Slyg.Tools
Imports QueryResponse = Miharu.Desktop.Library.MiharuDMZ.QueryResponse
Imports ClientUtil = Miharu.Desktop.Library.MiharuDMZ.ClientUtil
Imports QueryParameter = Miharu.Desktop.Library.MiharuDMZ.QueryParameter
Imports QueryResponseType = Miharu.Desktop.Library.MiharuDMZ.QueryResponseType
Imports QueryRequestType = Miharu.Desktop.Library.MiharuDMZ.QueryRequestType

Namespace Controller.Indexer.Capturas.Captura

    Public Class PreCapturaControllerDMZ
        Inherits CapturasControllerDMZ

#Region " Propiedades "

        Protected Overrides ReadOnly Property ProcessName As String
            Get
                Return "Pre Captura"
            End Get
        End Property

#End Region

#Region " Declaraciones "

        Dim fk_Esquema As Integer

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
            IndexerView.ShowValidationsPanel(False)
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
                    Dim FechaInicioProceso = Now

                    Dim Productividad As New DesktopConfig.ProductividadType
                    Productividad.Usuario = Me.IndexerSesion.Usuario.id
                    Productividad.Campos = _Campos.Count
                    Productividad.CamposLlave = _CamposLlave.Count
                    Productividad.Validaciones = _Validaciones.Count

                    Try
                        Dim ValidarSave As Boolean = True
                        'Valida label Primera Captura
                        Try
                            EventManager.ValidarSaveLabelCaptura(_Campos, FileCoreRow.fk_Documento, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, "1", ValidarSave)

                            'Validación Campo Duplicado 
                            Dim id_Campo As Int16
                            Dim Nombre_Campo As String
                            Dim Label As String = ""

                            'Dim drowCampoFiltrado = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_DocumentoCampo_Control_Duplicado(Me.IndexerImagingGlobal.Entidad, FileCoreRow.fk_Documento, True)
                            Dim drowCampoFiltrado As DBCore.SchemaConfig.TBL_CampoDataTable = Nothing
                            Dim queryResponseDrowCampoFiltrado As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Config.TBL_Campo", New List(Of QueryParameter) From {
                                                                                            New QueryParameter With {.name = "fk_Entidad", .value = Me.IndexerImagingGlobal.Entidad.ToString()},
                                                                                            New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                                            New QueryParameter With {.name = "Campo_Control_Duplicado", .value = True.ToString()}
                                                                                        }, QueryRequestType.Table, QueryResponseType.Table)
                            drowCampoFiltrado = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaConfig.TBL_CampoDataTable(), queryResponseDrowCampoFiltrado.dataTable), DBCore.SchemaConfig.TBL_CampoDataTable)

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
                                    'Dim dtResult = dbmImaging.SchemaProcess.PA_Validar_Campo_Duplicado.DBExecute(CInt(FileImagingRow.fk_Expediente), FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileCoreRow.fk_Documento, id_Campo, Nombre_Campo, Trim(Label), CStr(1))
                                    Dim dtResult As DataTable = Nothing
                                    Dim queryResponseDtResult As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Validar_Campo_Duplicado", New List(Of QueryParameter) From {
                                                                                                    New QueryParameter With {.name = "fk_Expediente", .value = CInt(FileImagingRow.fk_Expediente).ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Folder", .value = FileImagingRow.fk_Folder.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_File", .value = FileImagingRow.fk_File.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Campo", .value = id_Campo.ToString()},
                                                                                                    New QueryParameter With {.name = "Nombre_Campo", .value = Nombre_Campo.ToString()},
                                                                                                    New QueryParameter With {.name = "Valor_Campo_Control", .value = Trim(Label).ToString()},
                                                                                                    New QueryParameter With {.name = "TipoCaptura", .value = CStr(1).ToString()}
                                                                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)
                                    dtResult = queryResponseDtResult.dataTable

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
                            'Dim FileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                            Dim FileBloqueadoDataTable As DBImaging.SchemaProcess.TBL_Dashboard_CapturasDataTable = Nothing
                            Dim queryResponseFileBloqueado As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.TBL_Dashboard_Capturas", New List(Of QueryParameter) From {
                                                                                            New QueryParameter With {.name = "fk_Expediente", .value = FileImagingRow.fk_Expediente.ToString()},
                                                                                            New QueryParameter With {.name = "fk_Folder", .value = FileImagingRow.fk_Folder.ToString()},
                                                                                            New QueryParameter With {.name = "fk_File", .value = FileImagingRow.fk_File.ToString()}
                                                                                        }, QueryRequestType.Table, QueryResponseType.Table)
                            FileBloqueadoDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.TBL_Dashboard_CapturasDataTable(), queryResponseFileBloqueado.dataTable), DBImaging.SchemaProcess.TBL_Dashboard_CapturasDataTable)

                            If (FileBloqueadoDataTable(0).fk_Usuario_log <> Me.IndexerSesion.Usuario.id) Then
                                'dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Pre_Captura, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PcName)
                                Dim queryResponseInsertaSeguimientoImagenes As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Audit.PA_Inserta_Seguimiento_Imagenes", New List(Of QueryParameter) From {
                                                                                                    New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_file", .value = FileCoreRow.id_File.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Estado", .value = CInt(DBCore.EstadoEnum.Pre_Captura).ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Usuario", .value = Me.IndexerSesion.Usuario.id.ToString()},
                                                                                                    New QueryParameter With {.name = "PC_Name", .value = Me.IndexerDesktopGlobal.PcName.ToString()}
                                                                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)

                            End If

                        Catch : End Try
                        '--------------------------------------------------------------------------------------

                        ''dbmCore.LinkDataBaseManager(dbmImaging)
                        ''dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                        'dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                        ''dbmCore.Transaction_Begin()

                        Dim CamposQuery As New List(Of CampoCaptura)
                        For Each Campo In _Campos
                            If Campo.Control.IsVisible Then
                                If (Campo.Control.Tipo <> DesktopConfig.CampoTipo.TablaAsociada) Then
                                    ' Eliminar todos los datos de doble captura
                                    'dbmImaging.SchemaProcess.TBL_File_Data_MC.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, FileImagingRow.id_Version)
                                    Dim queryResponseDeleteFileDataMC As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Delete_TBL_File_Data_MC", New List(Of QueryParameter) From {
                                                                        New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                        New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                        New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                        New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                        New QueryParameter With {.name = "fk_Campo", .value = Campo.id.ToString()},
                                                                        New QueryParameter With {.name = "fk_Version", .value = FileImagingRow.id_Version.ToString()}
                                                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)

                                    If Campo.Control.Tipo <> DesktopConfig.CampoTipo.Query Then
                                        ' Leer la data almacenada
                                        'Dim FileDataCampo = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)
                                        Dim FileDataCampo As DBCore.SchemaProcess.TBL_File_DataDataTable = Nothing
                                        Dim queryResponseFileDataCampo As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.TBL_File_Data", New List(Of QueryParameter) From {
                                                                                                        New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                        New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                                        New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                                        New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                                                        New QueryParameter With {.name = "fk_Campo", .value = Campo.id.ToString()}
                                                                                                    }, QueryRequestType.Table, QueryResponseType.Table)
                                        FileDataCampo = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.TBL_File_DataDataTable(), queryResponseFileDataCampo.dataTable), DBCore.SchemaProcess.TBL_File_DataDataTable)

                                        Dim FileDataType As New DBCore.SchemaProcess.TBL_File_DataType()

                                        FileDataType.Valor_File_Data = Campo.Control.Value
                                        FileDataType.Conteo_File_Data = Campo.Control.Value.ToString().Length
                                        Productividad.Caracteres += Campo.Control.Value.ToString().Length

                                        If (FileDataCampo.Count = 0) Then
                                            FileDataType.fk_Expediente = FileCoreRow.fk_Expediente
                                            FileDataType.fk_Folder = FileCoreRow.fk_Folder
                                            FileDataType.fk_File = FileCoreRow.id_File
                                            FileDataType.fk_Documento = FileCoreRow.fk_Documento
                                            FileDataType.fk_Campo = Campo.id

                                            'dbmCore.SchemaProcess.TBL_File_Data.DBInsert(FileDataType)
                                            Dim queryResponseInsertFileData As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Insert_TBL_File_Data", New List(Of QueryParameter) From {
                                                                                                 New QueryParameter With {.name = "fk_Expediente", .value = FileDataType.fk_Expediente.ToString()},
                                                                                                 New QueryParameter With {.name = "fk_Folder", .value = FileDataType.fk_Folder.ToString()},
                                                                                                 New QueryParameter With {.name = "fk_File", .value = FileDataType.fk_File.ToString()},
                                                                                                 New QueryParameter With {.name = "fk_Documento", .value = FileDataType.fk_Documento.ToString()},
                                                                                                 New QueryParameter With {.name = "fk_Campo", .value = FileDataType.fk_Campo.ToString()},
                                                                                                 New QueryParameter With {.name = "Valor_File_Data", .value = FileDataType.Valor_File_Data.ToString()},
                                                                                                 New QueryParameter With {.name = "Conteo_File_Data", .value = FileDataType.Conteo_File_Data.ToString()}
                                                                                             }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                                        Else
                                            'dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(FileDataType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)
                                            Dim queryResponseInsertFileData As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Update_TBL_File_Data", New List(Of QueryParameter) From {
                                                                                                New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                                New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                                New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                                                New QueryParameter With {.name = "fk_Campo", .value = Campo.id.ToString()},
                                                                                                New QueryParameter With {.name = "Valor_File_Data", .value = FileDataType.Valor_File_Data.ToString()},
                                                                                                New QueryParameter With {.name = "Conteo_File_Data", .value = FileDataType.Conteo_File_Data.ToString()}
                                                                                            }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
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
                                    Dim TablaControl = CType(Campo.Control, TableInputControl)

                                    Dim Col As Integer = 0
                                    For Each TablaAsociadaRow As DBImaging.SchemaConfig.CTA_Tabla_AsociadaRow In TablaAsociadaDataTable.Select("fk_Campo = " & Campo.id)
                                        Dim Fil As Integer = 1
                                        For Each Registro As DataRow In ValueTable.Rows
                                            Dim TableValue As String

                                            TableValue = Registro.Item(Col).ToString()

                                            'dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil))
                                            Dim FileDataAsociada As DBCore.SchemaProcess.TBL_File_Data_AsociadaDataTable = Nothing
                                            Dim queryResponseFileDataAsociada As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.TBL_File_Data_Asociada", New List(Of QueryParameter) From {
                                                                                                            New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                            New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                                            New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                                            New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                                                            New QueryParameter With {.name = "fk_Campo", .value = Campo.id.ToString()},
                                                                                                            New QueryParameter With {.name = "fk_Campo_Tabla", .value = CShort(TablaAsociadaRow.id_Campo_Tabla).ToString()},
                                                                                                            New QueryParameter With {.name = "id_File_Data_Asociada", .value = CShort(Fil).ToString()}
                                                                                                        }, QueryRequestType.Table, QueryResponseType.Table)
                                            FileDataAsociada = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.TBL_File_Data_AsociadaDataTable(), queryResponseFileDataAsociada.dataTable), DBCore.SchemaProcess.TBL_File_Data_AsociadaDataTable)

                                            If (FileDataAsociada.Count = 0) Then
                                                'dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBInsert(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil), TableValue, TableValue.Length)
                                                Dim queryResponseInsertFileDataAsociada As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Insert_TBL_File_Data_Asociada", New List(Of QueryParameter) From {
                                                                                                                New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                                New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                                                New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                                                New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                                                                New QueryParameter With {.name = "fk_Campo", .value = Campo.id.ToString()},
                                                                                                                New QueryParameter With {.name = "fk_Campo_Tabla", .value = CShort(TablaAsociadaRow.id_Campo_Tabla).ToString()},
                                                                                                                New QueryParameter With {.name = "id_File_Data_Asociada", .value = CShort(Fil).ToString()},
                                                                                                                New QueryParameter With {.name = "Valor_File_Data", .value = TableValue.ToString()},
                                                                                                                New QueryParameter With {.name = "Conteo_File_Data", .value = TableValue.Length.ToString()}
                                                                                                            }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                                            Else
                                                'dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBUpdate(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, TableValue, TableValue.Length, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil))
                                                Dim queryResponseUpdateFileDataAsociada As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Update_TBL_File_Data_Asociada", New List(Of QueryParameter) From {
                                                                                                                New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                                New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                                                New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                                                New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                                                                New QueryParameter With {.name = "fk_Campo", .value = Campo.id.ToString()},
                                                                                                                New QueryParameter With {.name = "fk_Campo_Tabla", .value = CShort(TablaAsociadaRow.id_Campo_Tabla).ToString()},
                                                                                                                New QueryParameter With {.name = "id_File_Data_Asociada", .value = CShort(Fil).ToString()},
                                                                                                                New QueryParameter With {.name = "Valor_File_Data", .value = TableValue.ToString()},
                                                                                                                New QueryParameter With {.name = "Conteo_File_Data", .value = TableValue.Length.ToString()}
                                                                                                            }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
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
                                'Dim Value As SlygNullable(Of String) = Nothing

                                Dim Parametros As New List(Of Slyg.Data.Schemas.Parameter)
                                Dim Par1 As New Slyg.Data.Schemas.Parameter("@Expediente", FileCoreRow.fk_Expediente)
                                Dim Par2 As New Slyg.Data.Schemas.Parameter("@Folder", FileCoreRow.fk_Folder)
                                Dim Par3 As New Slyg.Data.Schemas.Parameter("@File", FileCoreRow.id_File)
                                Parametros.Add(Par1) : Parametros.Add(Par2) : Parametros.Add(Par3)
                                'Dim QueryRecuperacion = dbmCore.DataBase.ExecuteStoredProcedureGet("DBO", "PA_CAMPO_QUERY_" & CStr(Me.ExpedienteRow.fk_Entidad) & "_" & CStr(FileCoreRow.fk_Documento) & "_" & CStr(Campo.id), Parametros)
                                'If QueryRecuperacion.Rows.Count > 0 Then Value = Utilities.DStr(QueryRecuperacion.Rows(0)(0).ToString())

                                'Dim FileDataCampo = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)
                                Dim FileDataCampo As DBCore.SchemaProcess.TBL_File_DataDataTable = Nothing
                                Dim queryResponseFileDataCampo As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.TBL_File_Data", New List(Of QueryParameter) From {
                                                                                                New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                                New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                                New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                                                New QueryParameter With {.name = "fk_Campo", .value = Campo.id.ToString()}
                                                                                            }, QueryRequestType.Table, QueryResponseType.Table)
                                FileDataCampo = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.TBL_File_DataDataTable(), queryResponseFileDataCampo.dataTable), DBCore.SchemaProcess.TBL_File_DataDataTable)


                                Dim FileDataType As New DBCore.SchemaProcess.TBL_File_DataType()
                                FileDataType.Valor_File_Data = Campo.Control.Value
                                FileDataType.Conteo_File_Data = Campo.Control.Value.ToString().Length

                                If (FileDataCampo.Count = 0) Then
                                    FileDataType.fk_Expediente = FileCoreRow.fk_Expediente
                                    FileDataType.fk_Folder = FileCoreRow.fk_Folder
                                    FileDataType.fk_File = FileCoreRow.id_File
                                    FileDataType.fk_Documento = FileCoreRow.fk_Documento
                                    FileDataType.fk_Campo = Campo.id

                                    'dbmCore.SchemaProcess.TBL_File_Data.DBInsert(FileDataType)
                                    Dim queryResponseInsertFileData As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Insert_TBL_File_Data", New List(Of QueryParameter) From {
                                                                                        New QueryParameter With {.name = "fk_Expediente", .value = FileDataType.fk_Expediente.ToString()},
                                                                                        New QueryParameter With {.name = "fk_Folder", .value = FileDataType.fk_Folder.ToString()},
                                                                                        New QueryParameter With {.name = "fk_File", .value = FileDataType.fk_File.ToString()},
                                                                                        New QueryParameter With {.name = "fk_Documento", .value = FileDataType.fk_Documento.ToString()},
                                                                                        New QueryParameter With {.name = "fk_Campo", .value = FileDataType.fk_Campo.ToString()},
                                                                                        New QueryParameter With {.name = "Valor_File_Data", .value = FileDataType.Valor_File_Data.ToString()},
                                                                                        New QueryParameter With {.name = "Conteo_File_Data", .value = FileDataType.Conteo_File_Data.ToString()}
                                                                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                                Else
                                    'dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(FileDataType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)
                                    Dim queryResponseInsertFileData As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Update_TBL_File_Data", New List(Of QueryParameter) From {
                                                                                        New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                        New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                        New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                        New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                                        New QueryParameter With {.name = "fk_Campo", .value = Campo.id.ToString()},
                                                                                        New QueryParameter With {.name = "Valor_File_Data", .value = FileDataType.Valor_File_Data.ToString()},
                                                                                        New QueryParameter With {.name = "Conteo_File_Data", .value = FileDataType.Conteo_File_Data.ToString()}
                                                                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
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

                                        ' Eliminar todos los datos de doble captura
                                        'dbmImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MC.DBDelete(FileCoreRow.fk_Expediente, CampoLlave.Id_Campo)
                                        Dim queryResponseDeleteExpedienteLlaveLineaMC As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Delete_TBL_Expediente_Llave_Linea_MC", New List(Of QueryParameter) From {
                                                                                                            New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                            New QueryParameter With {.name = "fk_Campo_LLave", .value = CampoLlave.Id_Campo.ToString()}
                                                                                                        }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                                    End If
                                Next

                                'dbmCore.SchemaProcess.PA_Guarda_Expediente_Llave_En_Linea.DBExecute(FileCoreRow.fk_Expediente, CShort(_CamposLlave(0).fk_Entidad), CShort(_CamposLlave(0).fk_Proyecto), CShort(_CamposLlave(0).fk_Esquema), Llaves, CampoEmpaque)
                                Dim queryResponsePAGuardaExpedienteLlaveEnLinea As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Guarda_Expediente_Llave_En_Linea", New List(Of QueryParameter) From {
                                                                                                        New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                        New QueryParameter With {.name = "fk_Entidad", .value = CShort(_CamposLlave(0).fk_Entidad).ToString()},
                                                                                                        New QueryParameter With {.name = "fk_Proyecto", .value = CShort(_CamposLlave(0).fk_Proyecto).ToString()},
                                                                                                        New QueryParameter With {.name = "fk_Esquema", .value = CShort(_CamposLlave(0).fk_Esquema).ToString()},
                                                                                                        New QueryParameter With {.name = "Llaves", .value = Llaves.ToString()},
                                                                                                        New QueryParameter With {.name = "Campo_Empaque", .value = CampoEmpaque.ToString()}
                                                                                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                            End If
                        End If

                        'Almacenar validaciones
                        For Each item In _Validaciones
                            'dbmCore.SchemaProcess.TBL_File_Validacion.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, FileCoreRow.fk_Documento)
                            Dim FileDataValidacion As DBCore.SchemaProcess.TBL_File_ValidacionDataTable = Nothing
                            Dim queryResponseFileDataValidacion As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.TBL_File_Validacion", New List(Of QueryParameter) From {
                                                                                            New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                            New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                            New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                            New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                                            New QueryParameter With {.name = "fk_Validacion", .value = item.id.ToString()}
                                                                                        }, QueryRequestType.Table, QueryResponseType.Table)
                            FileDataValidacion = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.TBL_File_ValidacionDataTable(), queryResponseFileDataValidacion.dataTable), DBCore.SchemaProcess.TBL_File_ValidacionDataTable)


                            If (FileDataValidacion.Count > 0) Then
                                'dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(Nothing, Nothing, Nothing, Nothing, item.Control.ValueControl, item.Control.Motivo, FileCoreRow.fk_Documento, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, FileCoreRow.fk_Documento)
                                Dim queryResponseUpdateFileValidacion As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Update_TBL_File_Validacion", New List(Of QueryParameter) From {
                                                                                                    New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Validacion", .value = item.id.ToString()},
                                                                                                    New QueryParameter With {.name = "Respuesta", .value = item.Control.ValueControl.ToString()},
                                                                                                    New QueryParameter With {.name = "Motivo", .value = item.Control.Motivo.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()}
                                                                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                            Else
                                'dbmCore.SchemaProcess.TBL_File_Validacion.DBInsert(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, item.id, item.Control.ValueControl, item.Control.Motivo, FileCoreRow.fk_Documento)
                                Dim queryResponseInsertFileValidacion As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Insert_TBL_File_Validacion", New List(Of QueryParameter) From {
                                                                                                    New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Validacion", .value = item.id.ToString()},
                                                                                                    New QueryParameter With {.name = "Respuesta", .value = item.Control.ValueControl.ToString()},
                                                                                                    New QueryParameter With {.name = "Motivo", .value = item.Control.Motivo.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()}
                                                                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                            End If
                        Next

                        ' Insertar productividad
                        InsertarProductividad(Nothing, Productividad, DesktopConfig.Etapa_Productividad.PreCaptura, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                        ' Actualizar llaves
                        'dbmCore.SchemaProcess.PA_Calcular_Llaves.DBExecute(FileImagingRow.fk_Expediente)
                        Dim queryResponsePACalcularLlaves As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Calcular_Llaves", New List(Of QueryParameter) From {
                                                                        New QueryParameter With {.name = "id_Expediente", .value = FileImagingRow.fk_Expediente.ToString()}
                                                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)

                        ' Aplicar validaciones automáticas
                        'dbmImaging.SchemaProcess.PA_Aplicar_Validaciones_Automaticas.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento)
                        Dim queryResponsePaAplicarValidacionesAutomaticas As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Aplicar_Validaciones_Automaticas", New List(Of QueryParameter) From {
                                                New QueryParameter With {.name = "id_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                New QueryParameter With {.name = "id_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                New QueryParameter With {.name = "id_File", .value = FileCoreRow.id_File.ToString()},
                                                New QueryParameter With {.name = "id_Documento", .value = FileCoreRow.fk_Documento.ToString()}
                                            }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)

                        ' Actualizar estados
                        'Dim NextEstado = dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(CurrentDocumentFile.TipoDocumento, DBCore.EstadoEnum.Pre_Captura)
                        Dim NextEstado As Short = 0
                        Dim queryResponseNextEstado As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Next_Estado", New List(Of QueryParameter) From {
                                                        New QueryParameter With {.name = "id_Documento", .value = CurrentDocumentFile.TipoDocumento.ToString()},
                                                        New QueryParameter With {.name = "Estado_Actual", .value = CInt(DBCore.EstadoEnum.Pre_Captura).ToString()}
                                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                        NextEstado = CShort(queryResponseNextEstado.scalar)

                        ' Preparar los recortes
                        If (NextEstado = DBCore.EstadoEnum.Recorte) Then
                            'dbmImaging.SchemaProcess.PA_Preparar_Recortes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)
                            Dim PA_Preparar_Recortes As Boolean = False
                            Dim queryResponsePAPrepararRecortes As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Preparar_Recortes", New List(Of QueryParameter) From {
                                                                                    New QueryParameter With {.name = "id_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                    New QueryParameter With {.name = "id_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                    New QueryParameter With {.name = "id_File", .value = FileCoreRow.id_File.ToString()},
                                                                                    New QueryParameter With {.name = "id_Version", .value = FileImagingRow.id_Version.ToString()}
                                                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                            PA_Preparar_Recortes = CBool(queryResponsePAPrepararRecortes.scalar)

                            If Not (Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Recortes AndAlso PA_Preparar_Recortes) Then
                                NextEstado = DBCore.EstadoEnum.Indexado
                            End If
                        End If

                        ' Establece estado heredado del doccumento de captura campos llaves, cuando este no esta en el expediente.
                        If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                            If (NextEstado = DBCore.EstadoEnum.Indexado) Then
                                'NextEstado = dbmImaging.SchemaProcess.PA_Next_Estado_Documento_Captura_Campos_Llave.DBExecute(Me.IndexerImagingGlobal.Entidad, Me.IndexerImagingGlobal.Proyecto, Me.fk_Esquema, CType(FileCoreRow.fk_Expediente, Global.Slyg.Tools.SlygNullable(Of Integer)), FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Pre_Captura)
                                Dim queryResponseNextEstadoDocumentoCapturaCamposLlave As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Next_Estado_Documento_Captura_Campos_Llave", New List(Of QueryParameter) From {
                                                                                                            New QueryParameter With {.name = "fk_Entidad", .value = Me.IndexerImagingGlobal.Entidad.ToString()},
                                                                                                            New QueryParameter With {.name = "fk_Proyecto", .value = Me.IndexerImagingGlobal.Proyecto.ToString()},
                                                                                                            New QueryParameter With {.name = "fk_Esquema", .value = Me.fk_Esquema.ToString()},
                                                                                                            New QueryParameter With {.name = "fk_Expediente", .value = CType(FileCoreRow.fk_Expediente, Global.Slyg.Tools.SlygNullable(Of Integer)).ToString()},
                                                                                                            New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                                            New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                                            New QueryParameter With {.name = "Estado_Actual", .value = CInt(DBCore.EstadoEnum.Pre_Captura).ToString()}
                                                                                                        }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                                NextEstado = CShort(queryResponseNextEstadoDocumentoCapturaCamposLlave.scalar)

                            End If
                        End If

                        Dim UpdateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                        UpdateEstado.Fecha_Log = DateTime.Now
                        UpdateEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                        UpdateEstado.fk_Estado = NextEstado

                        'dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(UpdateEstado, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, DesktopConfig.Modulo.Imaging)
                        Dim queryResponseUpdateFileEstado As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.PA_Update_TBL_File_Estado", New List(Of QueryParameter) From {
                                                                                        New QueryParameter With {.name = "fk_Expediente", .value = FileImagingRow.fk_Expediente.ToString()},
                                                                                        New QueryParameter With {.name = "fk_Folder", .value = FileImagingRow.fk_Folder.ToString()},
                                                                                        New QueryParameter With {.name = "fk_File", .value = FileImagingRow.fk_File.ToString()},
                                                                                        New QueryParameter With {.name = "Modulo", .value = CInt(DesktopConfig.Modulo.Imaging).ToString()},
                                                                                        New QueryParameter With {.name = "fk_Estado", .value = UpdateEstado.fk_Estado.ToString()},
                                                                                        New QueryParameter With {.name = "fk_Usuario", .value = UpdateEstado.fk_Usuario.ToString()},
                                                                                        New QueryParameter With {.name = "Fecha_Log", .value = UpdateEstado.Fecha_Log.ToString()}
                                                                                    }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)

                        '---------------------------------------------------------------------------
                        ' Actualizar Dashboard
                        '---------------------------------------------------------------------------
                        If (NextEstado = DBCore.EstadoEnum.Indexado) Then
                            'dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                            Dim queryResponseDeleteDashboardCapturas As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Delete_TBL_Dashboard_Capturas", New List(Of QueryParameter) From {
                                                                                    New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                    New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                    New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()}
                                                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                        Else
                            Dim CapDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                            CapDashboardType.fk_Usuario_log = DBNull.Value
                            CapDashboardType.Sesion = DBNull.Value
                            CapDashboardType.fk_Estado = NextEstado
                            'dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(CapDashboardType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                            Dim queryResponseUpdateDashboardCapturas As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Update_TBL_Dashboard_Capturas", New List(Of QueryParameter) From {
                                                                                            New QueryParameter With {.name = "fk_Expediente", .value = FileCoreRow.fk_Expediente.ToString()},
                                                                                            New QueryParameter With {.name = "fk_Folder", .value = FileCoreRow.fk_Folder.ToString()},
                                                                                            New QueryParameter With {.name = "fk_File", .value = FileCoreRow.id_File.ToString()},
                                                                                            New QueryParameter With {.name = "fk_Estado", .value = CapDashboardType.fk_Estado.ToString()}
                                                                                        }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                        End If
                        '---------------------------------------------------------------------------

                        ''dbmCore.Transaction_Commit()
                        'dbmImaging.Transaction_Commit()

                        ' Actualizar el estado de los cargues que ya fueron procesados
                        Try
                            'dbmImaging.Transaction_Begin(IsolationLevel.ReadCommitted)

                            'Dim CarguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(_idCargue, _idCarguePaquete)
                            Dim CarguePaqueteFileDataTable As DBImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_FileDataTable = Nothing
                            Dim queryResponseCarguePaqueteFileData As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.CTA_Cargue_Paquete_Estado_File", New List(Of QueryParameter) From {
                                                                                            New QueryParameter With {.name = "id_Cargue", .value = _idCargue.ToString()},
                                                                                            New QueryParameter With {.name = "id_Cargue_Paquete", .value = _idCarguePaquete.ToString()}
                                                                                        }, QueryRequestType.Table, QueryResponseType.Table)
                            CarguePaqueteFileDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_FileDataTable(), queryResponseCarguePaqueteFileData.dataTable), DBImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_FileDataTable)

                            If (CarguePaqueteFileDataTable.Count > 0) Then
                                Dim UpdatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                                UpdatePaquete.fk_Estado = CarguePaqueteFileDataTable(0).Estado_File
                                'dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(UpdatePaquete, _idCargue, _idCarguePaquete)
                                Dim queryResponseUpdateCarguePaquete As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Update_TBL_Cargue_Paquete", New List(Of QueryParameter) From {
                                                                                                New QueryParameter With {.name = "fk_Cargue", .value = _idCargue.ToString()},
                                                                                                New QueryParameter With {.name = "id_Cargue_Paquete", .value = _idCarguePaquete.ToString()},
                                                                                                New QueryParameter With {.name = "fk_Estado", .value = UpdatePaquete.fk_Estado.ToString()}
                                                                                            }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)

                                'Dim CargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(_idCargue)
                                Dim CargueFileDataTable As DBImaging.SchemaProcess.CTA_Cargue_EstadoDataTable = Nothing
                                Dim queryResponseCargueFileData As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.CTA_Cargue_Estado", New List(Of QueryParameter) From {
                                                                                                New QueryParameter With {.name = "id_Cargue", .value = _idCargue.ToString()}
                                                                                            }, QueryRequestType.Table, QueryResponseType.Table)
                                CargueFileDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaProcess.CTA_Cargue_EstadoDataTable(), queryResponseCargueFileData.dataTable), DBImaging.SchemaProcess.CTA_Cargue_EstadoDataTable)

                                If (CargueFileDataTable.Count > 0) Then
                                    Dim UpdateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                                    UpdateCargue.fk_Estado = CargueFileDataTable(0).Estado_File
                                    'dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(UpdateCargue, _idCargue)
                                    Dim queryResponseUpdateCargue As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Update_TBL_Cargue", New List(Of QueryParameter) From {
                                                                                                    New QueryParameter With {.name = "id_Cargue", .value = _idCargue.ToString()},
                                                                                                    New QueryParameter With {.name = "fk_Estado", .value = UpdateCargue.fk_Estado.ToString()}
                                                                                                }, QueryRequestType.StoredProcedure, QueryResponseType.Scalar)
                                End If
                            End If

                            'dbmImaging.Transaction_Commit()
                        Catch ex As Exception
                            'If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                            Throw
                        End Try

                    Catch ex As Exception
                        'If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                        'If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                        DesktopMessageBoxControl.DesktopMessageShow("Publicar", ex)

                        Return False
                        'Finally
                        '    'If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        '    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try

                    EventManager.FinalizarPreCaptura(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                    '-------------------------------------------------------------------------------------------------------------
                    ' LOGGING - PERFORMANCE
                    '-------------------------------------------------------------------------------------------------------------
                    Dim FechaFinProceso = Now
                    Dim TraceMessage As String = ""
                    TraceMessage &= "Duración:" & vbTab & (FechaInicioProceso - FechaFinProceso).TotalMilliseconds & vbTab
                    TraceMessage &= "Expediente:" & vbTab & FileCoreRow.fk_Expediente & vbTab
                    TraceMessage &= "Folder:" & vbTab & FileCoreRow.fk_Folder & vbTab
                    TraceMessage &= "File:" & vbTab & FileCoreRow.id_File & vbTab
                    DesktopTrace.Trace(TraceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, "[Precaptura][Publicar]")
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

        Public Overrides Function Campos(ByVal idDocumento As Integer) As List(Of CampoCaptura)
            Me._Campos.Clear()

            For Each CampoRow As DBImaging.SchemaConfig.CTA_CampoRow In CamposDataTable.Select("fk_Documento = " & idDocumento)

                Dim ControlCaptura As View.IInputControl
                Dim CampoCaptura As New CampoCaptura()
                Dim ListDefinicionCaptura As New List(Of DefinicionCaptura)

                Dim CampoOCR As DataRow()
                Dim CampoOCRRow As DBOCR.SchemaProcess.TBL_File_Data_CamposRow = Nothing

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

                            Dim ListControl = CType(ControlCaptura, ListInputControl)
                            ListControl.ValueDesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                            ListControl.ValueDesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                            ListControl.ValueDesktopComboBox.DataSource = dtvLista
                            ListControl.ValueDesktopComboBox.Refresh()
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

                        DefinicionCaptura.Type = DesktopConfig.CampoTipo.Query
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
                    For Each TriggerRow As DBImaging.SchemaConfig.TBL_Campo_TriggerRow In TriggersDataTable.Select("fk_Campo_Trigger = " & CampoRow.id_Campo)
                        ControlCaptura.TriggerValues.Add(New View.KeyValueItem(TriggerRow.Valor, TriggerRow.fk_Campo_Ocultar, TriggerRow.fk_Campo_Tabla_Ocultar))
                    Next
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
                    For Each CampoRow As DBImaging.SchemaConfig.CTA_Campo_LlaveRow In CamposLlaveDataTable.Select("fk_Documento = " & idDocumento & " AND Es_Campo_Indexacion = 1")

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

        Public Overrides Function GetValueFileData(idCampo As Integer) As String
            Dim ValueFileData As String = Nothing

            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Carga_Datos_Captura Then
                Try

                    'Dim CBarrasCampo = dbmImaging.SchemaConfig.TBL_CBarras_Campo.DBFindByfk_Documentofk_Campo(FileCoreRow.fk_Documento, CShort(idCampo))
                    Dim CBarrasCampo As DBImaging.SchemaConfig.TBL_CBarras_CampoDataTable = Nothing
                    Dim queryResponseCBarrasCampo As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.TBL_CBarras_Campo", New List(Of QueryParameter) From {
                                                                    New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                    New QueryParameter With {.name = "fk_Campo", .value = CShort(idCampo).ToString()}
                                                                }, QueryRequestType.Table, QueryResponseType.Table)
                    CBarrasCampo = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.TBL_CBarras_CampoDataTable(), queryResponseCBarrasCampo.dataTable), DBImaging.SchemaConfig.TBL_CBarras_CampoDataTable)


                    If CBarrasCampo.Rows.Count > 0 Then
                        'Dim FileDataValues = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_File(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                        Dim FileDataValues As DBCore.SchemaProcess.TBL_File_DataDataTable = Nothing
                        Dim queryResponseFileDataValues As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.TBL_File_Data", New List(Of QueryParameter) From {
                                                                        New QueryParameter With {.name = "fk_Expediente", .value = FileImagingRow.fk_Expediente.ToString()},
                                                                        New QueryParameter With {.name = "fk_Folder", .value = FileImagingRow.fk_Folder.ToString()},
                                                                        New QueryParameter With {.name = "fk_File", .value = FileImagingRow.fk_File.ToString()}
                                                                    }, QueryRequestType.Table, QueryResponseType.Table)
                        FileDataValues = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.TBL_File_DataDataTable(), queryResponseFileDataValues.dataTable), DBCore.SchemaProcess.TBL_File_DataDataTable)

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
                    DesktopMessageBoxControl.DesktopMessageShow("Se ha presentado un error al obtener valor de captura", ex)
                    Return ""
                End Try

            End If

            Return ValueFileData
        End Function

        Public Overrides Function GetValueLlaveData() As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow

            Dim ValueFileData As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow = Nothing
            Dim ValueFileDataTable As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaDataTable = Nothing

            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Carga_Datos_Captura Then
                Try

                    If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                        'ValueFileDataTable = dbmCore.SchemaProcess.TBL_Expediente_Llave_Linea.DBFindByfk_Expediente(FileImagingRow.fk_Expediente)
                        Dim queryResponseValueFile As QueryResponse = ClientUtil.resolver("[DB_OCR].Process.TBL_Expediente_Llave_Linea", New List(Of QueryParameter) From {
                             New QueryParameter With {.name = "fk_Expediente", .value = FileImagingRow.fk_Expediente.ToString()}
                         }, QueryRequestType.Table, QueryResponseType.Table)
                        ValueFileDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaProcess.TBL_Expediente_Llave_LineaDataTable(), queryResponseValueFile.dataTable), DBCore.SchemaProcess.TBL_Expediente_Llave_LineaDataTable)

                        If ValueFileDataTable.Rows.Count > 0 Then
                            ValueFileData = ValueFileDataTable(0)
                        End If
                    End If

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Se ha presentado un error al obtener valor de captura de las llaves", ex)
                    Return Nothing
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

        'Public Overrides Function DocumentosCaptura(ByVal nEsquema As Short) As DBImaging.SchemaProcess.CTA_Documentos_ValidacionesDataTable
        '    Return New DBImaging.SchemaProcess.CTA_Documentos_ValidacionesDataTable()
        'End Function

        'Public Overrides Function ShowDocumentosCaptura() As DialogResult
        '    Return DialogResult.OK
        'End Function

#End Region

#Region " Metodos "

        Protected Overrides Sub LoadConfig(ByRef nDBMCore As DBCore.DBCoreDataBaseManager, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short)

            'Dim dbmOCR As DBOCR.DBOCRDataBaseManager = Nothing
            'dbmOCR = New DBOCR.DBOCRDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.OCR)

            'Me.EsquemaDataTable = nDBMCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
            Dim queryResponseEsquema As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Config.TBL_Esquema", New List(Of QueryParameter) From {
                             New QueryParameter With {.name = "fk_Entidad", .value = nEntidad.ToString()},
                             New QueryParameter With {.name = "fk_Proyecto", .value = nProyecto.ToString()},
                             New QueryParameter With {.name = "id_Esquema", .value = nEsquema.ToString()}
                         }, QueryRequestType.Table, QueryResponseType.Table)
            Me.EsquemaDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaConfig.TBL_EsquemaDataTable(), queryResponseEsquema.dataTable), DBCore.SchemaConfig.TBL_EsquemaDataTable)

            'Me.DocumentoDataTable = nDBMImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(nEntidad, nProyecto, nEsquema, FileCoreRow.fk_Documento, False)
            Dim queryResponseDocumento As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.CTA_Documento", New List(Of QueryParameter) From {
                                                              New QueryParameter With {.name = "fk_Entidad", .value = nEntidad.ToString()},
                                                              New QueryParameter With {.name = "fk_Proyecto", .value = nProyecto.ToString()},
                                                              New QueryParameter With {.name = "fk_Esquema", .value = nEsquema.ToString()},
                                                              New QueryParameter With {.name = "id_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                              New QueryParameter With {.name = "Eliminado", .value = False.ToString()}
                                                        }, QueryRequestType.Table, QueryResponseType.Table)
            Me.DocumentoDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.CTA_DocumentoDataTable(), queryResponseDocumento.dataTable), DBImaging.SchemaConfig.CTA_DocumentoDataTable)

            Me.fk_Esquema = nEsquema

            'Dim Orden = New DBImaging.SchemaConfig.CTA_CampoEnumList()
            'Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.fk_Documento, True)
            'Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.Orden_Campo, True)
            'Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.id_Campo, True)

            'Me.CamposDataTable = nDBMImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_Documentofk_Proyectoid_EsquemaUsa_CapturaEs_Campo_IndexacionEliminado_Campo(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, Nothing, True, False, 0, Orden)
            Dim queryResponseCampos As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.CTA_Campo", New List(Of QueryParameter) From {
                                                  New QueryParameter With {.name = "fk_Entidad", .value = Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad.ToString()},
                                                  New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                  New QueryParameter With {.name = "fk_Proyecto", .value = Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                  New QueryParameter With {.name = "id_Esquema", .value = nEsquema.ToString()},
                                                  New QueryParameter With {.name = "Es_Campo_Indexacion", .value = True.ToString()},
                                                  New QueryParameter With {.name = "Eliminado_Campo", .value = False.ToString()}
                                            }, QueryRequestType.Table, QueryResponseType.Table)
            Dim queryResponseCamposOrdenados As IEnumerable(Of DataRow) = queryResponseCampos.dataTable.AsEnumerable().OrderBy(Function(row) row("fk_Documento")) _
                                                                             .ThenBy(Function(row) row("Orden_Campo")) _
                                                                             .ThenBy(Function(row) row("id_Campo"))
            Dim dataTableCamposOrdenado As DataTable = queryResponseCamposOrdenados.CopyToDataTable()
            Me.CamposDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.CTA_CampoDataTable(), dataTableCamposOrdenado), DBImaging.SchemaConfig.CTA_CampoDataTable)

            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                'Me.CamposLlaveDataTable = nDBMImaging.SchemaConfig.CTA_Campo_Llave.DBFindByfk_Entidadfk_DocumentoEliminado_Campofk_Proyectoid_EsquemaUsa_CapturaUsa_Doble_CapturaEs_Campo_Indexacion(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, False, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, Nothing, Nothing, True)
                Dim queryResponseCamposLlave As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.CTA_Campo_Llave", New List(Of QueryParameter) From {
                                                  New QueryParameter With {.name = "fk_Entidad", .value = Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad.ToString()},
                                                  New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                  New QueryParameter With {.name = "Eliminado_Campo", .value = False.ToString()},
                                                  New QueryParameter With {.name = "fk_Proyecto", .value = Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                  New QueryParameter With {.name = "id_Esquema", .value = nEsquema.ToString()},
                                                  New QueryParameter With {.name = "Es_Campo_Indexacion", .value = True.ToString()}
                                            }, QueryRequestType.Table, QueryResponseType.Table)
                Me.CamposLlaveDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.CTA_Campo_LlaveDataTable(), queryResponseCamposLlave.dataTable), DBImaging.SchemaConfig.CTA_Campo_LlaveDataTable)

                'Busca campos llaves asignados al DocPpal para capturarselos al File1 del Expediente
                If CamposLlaveDataTable.Count = 0 Then
                    'Me.CamposLlaveDataTable = nDBMImaging.SchemaProcess.PA_Busca_Campos_Llave_DocPpal.DBExecute(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, FileCoreRow.fk_Documento, CInt(FileImagingRow.fk_Expediente), FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                    Dim queryResponseCamposLlavePpal As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Process.PA_Busca_Campos_Llave_DocPpal", New List(Of QueryParameter) From {
                                                  New QueryParameter With {.name = "fk_Entidad", .value = Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad.ToString()},
                                                  New QueryParameter With {.name = "fk_Proyecto", .value = Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto.ToString()},
                                                  New QueryParameter With {.name = "fk_Esquema", .value = nEsquema.ToString()},
                                                  New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                  New QueryParameter With {.name = "fk_Expediente", .value = CInt(FileImagingRow.fk_Expediente).ToString()},
                                                  New QueryParameter With {.name = "fk_Folder", .value = FileImagingRow.fk_Folder.ToString()},
                                                  New QueryParameter With {.name = "fk_File", .value = FileImagingRow.fk_File.ToString()}
                                            }, QueryRequestType.StoredProcedure, QueryResponseType.Table)
                    Me.CamposLlaveDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.CTA_Campo_LlaveDataTable(), queryResponseCamposLlavePpal.dataTable), DBImaging.SchemaConfig.CTA_Campo_LlaveDataTable)

                End If
            End If

            'Me.TriggersDataTable = nDBMImaging.SchemaConfig.TBL_Campo_Trigger.DBGet(DBImaging.EnumEtapaCaptura.Precaptura, FileCoreRow.fk_Documento, Nothing, Nothing, Nothing, Nothing)
            Dim queryResponseTriggers As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.TBL_Campo_Trigger", New List(Of QueryParameter) From {
                                                  New QueryParameter With {.name = "fk_Etapa_Captura", .value = CInt(DBImaging.EnumEtapaCaptura.Precaptura).ToString()},
                                                  New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()}
                                            }, QueryRequestType.Table, QueryResponseType.Table)
            Me.TriggersDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.TBL_Campo_TriggerDataTable(), queryResponseTriggers.dataTable), DBImaging.SchemaConfig.TBL_Campo_TriggerDataTable)

            ' Lista Item
            'Me.ListaItemsDataTable = nDBMCore.SchemaConfig.PA_Campo_Lista_Item_getDocumento.DBExecute(FileCoreRow.fk_Documento)
            Dim queryResponseListaItems As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Config.PA_Campo_Lista_Item_getDocumento", New List(Of QueryParameter) From {
                                      New QueryParameter With {.name = "id_Documento", .value = CInt(FileCoreRow.fk_Documento).ToString()}
                                }, QueryRequestType.StoredProcedure, QueryResponseType.Table)
            Me.ListaItemsDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable(), queryResponseListaItems.dataTable), DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable)


            'Dim Orden_Validacion = New DBImaging.SchemaConfig.CTA_ValidacionEnumList()
            'Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.fk_Documento, True)
            'Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.Orden_Validacion, True)
            'Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.id_Validacion, True)
            'Me.ValidacionesDataTable = nDBMImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(FileCoreRow.fk_Documento, DBImaging.EnumEtapaCaptura.Precaptura, 0, Orden_Validacion)
            Dim queryResponseValidaciones As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.CTA_Validacion", New List(Of QueryParameter) From {
                                                              New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                              New QueryParameter With {.name = "fk_Etapa_Captura", .value = CInt(DBImaging.EnumEtapaCaptura.Precaptura).ToString()}
                                                        }, QueryRequestType.Table, QueryResponseType.Table)
            Dim queryResponseValidacionesOrdenados As IEnumerable(Of DataRow) = queryResponseValidaciones.dataTable.AsEnumerable().OrderBy(Function(row) row("fk_Documento")) _
                                                                             .ThenBy(Function(row) row("Orden_Validacion")) _
                                                                             .ThenBy(Function(row) row("id_Validacion"))
            ' Crear un DataTable ordenado o vacío según el contenido de queryResponseValidacionesOrdenados
            Dim dataTableValidacionesOrdenado As DataTable = If(queryResponseValidacionesOrdenados.Any(),
                                                                queryResponseValidacionesOrdenados.CopyToDataTable(),
                                                                queryResponseValidaciones.dataTable.Clone())
            Me.ValidacionesDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.CTA_ValidacionDataTable(), dataTableValidacionesOrdenado), DBImaging.SchemaConfig.CTA_ValidacionDataTable)

            'Me.TablaAsociadaDataTable = nDBMImaging.SchemaConfig.CTA_Tabla_Asociada.DBFindByfk_DocumentoEliminado_Campo(FileCoreRow.fk_Documento, False)
            Dim queryResponseTablaAsociada As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Config.CTA_Tabla_Asociada", New List(Of QueryParameter) From {
                                                                 New QueryParameter With {.name = "fk_Documento", .value = FileCoreRow.fk_Documento.ToString()},
                                                                 New QueryParameter With {.name = "Eliminado_Campo", .value = False.ToString()}
                                                             }, QueryRequestType.Table, QueryResponseType.Table)
            Me.TablaAsociadaDataTable = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaConfig.CTA_Tabla_AsociadaDataTable(), queryResponseTablaAsociada.dataTable), DBImaging.SchemaConfig.CTA_Tabla_AsociadaDataTable)

            ' Anexos del documento
            'Me.AnexosDataTable = nDBMCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)
            Dim queryResponseAnexosDataTable As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Imaging.CTA_Documentos", New List(Of QueryParameter) From {
                                                     New QueryParameter With {.name = "fk_Expediente", .value = FileImagingRow.fk_Expediente.ToString()},
                                                     New QueryParameter With {.name = "fk_Folder", .value = FileImagingRow.fk_Folder.ToString()}
                                                 }, QueryRequestType.Table, QueryResponseType.Table)
            Me.AnexosDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaImaging.CTA_DocumentosDataTable(), queryResponseAnexosDataTable.dataTable), DBCore.SchemaImaging.CTA_DocumentosDataTable)

            'Llena los campos lista para la seleccion de motivos
            'Me.MotivosDataTable = nDBMCore.SchemaConfig.TBL_Campo_Lista_Item.DBGet(nEntidad, Nothing, Nothing)
            Dim queryResponseMotivos As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Config.TBL_Campo_Lista_Item", New List(Of QueryParameter) From {
                                         New QueryParameter With {.name = "fk_Entidad", .value = nEntidad.ToString()}
                                     }, QueryRequestType.Table, QueryResponseType.Table)
            Me.MotivosDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable(), queryResponseMotivos.dataTable), DBCore.SchemaConfig.TBL_Campo_Lista_ItemDataTable)

            'Llena el dato si Usa OCR Captura
            'Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_OCR_Captura()
            'Dim proyectoDataTable = nDBMImaging.SchemaConfig.TBL_Proyecto.DBFindByfk_Entidadfk_Proyecto(nEntidad, nProyecto)

            'Me._Usa_OCR_Captura = Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_OCR_Captura

            Me.IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
            Me.IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)
        End Sub

#End Region

    End Class

End Namespace