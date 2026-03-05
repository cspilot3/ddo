Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports Slyg.Tools

Namespace Controller.Indexer.Capturas.Captura

    Public Class TerceraCapturaController
        Inherits CapturasController

#Region " Declaraciones "

        Protected MesaControlDataTable As New DBImaging.SchemaProcess.CTA_File_Data_MC_OrdenDataTable()
        Protected MesaControlCamposLlaveDataTable As New DBImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MCDataTable()
        Dim fk_Esquema As Integer
        Protected MesaControlDataTableAnexo As New DBImaging.SchemaProcess.CTA_Anexo_Data_MC_OrdenDataTable()

#End Region

#Region " Propiedades "

        Protected Overrides ReadOnly Property ProcessName As String
            Get
                Return "Tercera Captura"
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
            IndexerView.ShowValidationsPanel(False)
            IndexerView.ShowValidationsListasPanel(False)
            IndexerView.ShowToolTipData(True)
            IndexerView.ShowAutoIndexar(False)
        End Sub

        Public Overrides ReadOnly Property ShowSecondControls As Boolean
            Get
                Return True
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
                If (Validar()) Then
                    Dim Productividad As New DesktopConfig.ProductividadType
                    Dim Errores(1) As DesktopConfig.ProductividadType

                    Errores(0) = New DesktopConfig.ProductividadType
                    Errores(1) = New DesktopConfig.ProductividadType

                    Productividad.Usuario = Me.IndexerSesion.Usuario.id

                    Try

                        Dim ValidarSave As Boolean = True

                        EventManager.ValidarSaveTerceraCaptura(_Campos, FileCoreRow.fk_Documento, ValidarSave)

                        If Not ValidarSave Then
                            Return False
                        End If

                        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                        dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        'Valida label Tercera Captura
                        Try
                            EventManager.ValidarSaveLabelCaptura(_Campos, FileCoreRow.fk_Documento, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, "3", ValidarSave)

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
                                    Dim dtResult = dbmImaging.SchemaProcess.PA_Validar_Campo_Duplicado.DBExecute(CInt(FileImagingRow.fk_Expediente), FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileCoreRow.fk_Documento, id_Campo, Nombre_Campo, Trim(Label), CStr(3))
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



                        Dim FechaInicioProceso = Now

                        Try
                            '--------------------------------------------------------------------------------------
                            ' REPORTAR DUPLICADOS
                            '--------------------------------------------------------------------------------------
                            Try
                                Dim FileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                                If (FileBloqueadoDataTable(0).fk_Usuario_log <> Me.IndexerSesion.Usuario.id) Then
                                    dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Tercera_Captura, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PcName)
                                End If

                            Catch : End Try
                            '--------------------------------------------------------------------------------------

                            'dbmCore.LinkDataBaseManager(dbmImaging)
                            'dbmCore.Connection_Open(Me.IndexerSesion.Usuario.id)

                            dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                            'dbmCore.Transaction_Begin()

                            ' Reportar usuario captura
                            Dim FileType = New DBImaging.SchemaProcess.TBL_FileType()
                            FileType.Usuario_Tercera_Captura = Me.IndexerSesion.Usuario.id
                            FileType.Fecha_Tercera_Captura = SlygNullable.SysDate
                            dbmImaging.SchemaProcess.TBL_File.DBUpdate(FileType, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                            Dim FileTable = dbmImaging.SchemaProcess.TBL_File.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)
                            FileType = FileTable(0).ToTBL_FileType()

                            Errores(1).Usuario = FileType.Usuario_Segunda_Captura
                            Errores(1).Fecha = FileType.Fecha_Segunda_Captura

                            ' Actualizar datos capturados
                            Dim PasaCalidad As Boolean = False
                            Dim CamposQuery As New List(Of CampoCaptura)
                            For Each Campo In _Campos
                                If Campo.Control.IsVisible Then
                                    If (Campo.Control.Tipo <> DesktopConfig.CampoTipo.TablaAsociada) Then

                                        If Campo.Control.Tipo <> DesktopConfig.CampoTipo.Query Then
                                            Dim CampoType = New DBCore.SchemaProcess.TBL_File_DataType()

                                            CampoType.Valor_File_Data = Campo.Control.Value
                                            CampoType.Conteo_File_Data = Campo.Control.Value.ToString().Length

                                            ' Actualizar mesa de control
                                            Dim MCType = New DBImaging.SchemaProcess.TBL_File_Data_MCType()
                                            MCType.Valor_Tercera_Captura = Campo.Control.Value
                                            dbmImaging.SchemaProcess.TBL_File_Data_MC.DBUpdate(MCType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, FileImagingRow.id_Version)

                                            'aca
                                            Dim DocumentoUsaCapturaCorreccionMaquina = dbmImaging.SchemaConfig.TBL_Campo.DBFindByfk_Documentoid_CampoUsa_Captura_Correccion_Maquina(FileCoreRow.fk_Documento, Campo.id, True)

                                            If DocumentoUsaCapturaCorreccionMaquina.Rows.Count > 0 Then
                                                If DocumentoUsaCapturaCorreccionMaquina(0).Usa_Captura_Correccion_Maquina Then
                                                    Errores(0).Usuario = FileType.Usuario_Correccion_Maquina
                                                    Errores(0).Fecha = FileType.Fecha_Correccion_Maquina
                                                Else
                                                    Errores(0).Usuario = FileType.Usuario_Primera_Captura
                                                    Errores(0).Fecha = FileType.Fecha_Primera_Captura
                                                End If
                                            Else
                                                Errores(0).Usuario = FileType.Usuario_Primera_Captura
                                                Errores(0).Fecha = FileType.Fecha_Primera_Captura
                                            End If


                                            If Campo.Control.IsVisible Then
                                                Productividad.Campos += 1
                                                Productividad.Caracteres += Campo.Control.Value.ToString().Length

                                                'Conteo de Errores
                                                If Campo.Control.Value.ToString() <> Campo.Control.ValueOld1.ToString() Then
                                                    Errores(0).Campos += 1
                                                    Errores(0).Caracteres += Campo.Control.ValueOld1.ToString().Length
                                                    InsertarProductividad_Error_Detallado(dbmImaging, Campo, Nothing, Nothing, Errores(0), Campo.Control.ValueOld1, Me.IndexerSesion.Usuario.id, Campo.Control.Value, DesktopConfig.Etapa_Productividad.Primera_Captura)
                                                End If

                                                If Campo.Control.Value.ToString() <> Campo.Control.ValueOld2.ToString() Then
                                                    Errores(1).Campos += 1
                                                    Errores(1).Caracteres += Campo.Control.ValueOld2.ToString().Length
                                                    InsertarProductividad_Error_Detallado(dbmImaging, Campo, Nothing, Nothing, Errores(1), Campo.Control.ValueOld2, Me.IndexerSesion.Usuario.id, Campo.Control.Value, DesktopConfig.Etapa_Productividad.Segunda_Captura)
                                                End If
                                            End If

                                            dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(CampoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)

                                            If (Not PasaCalidad) Then
                                                Dim DataMCDataTable = dbmImaging.SchemaProcess.TBL_File_Data_MC.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, FileImagingRow.id_Version)
                                                If (DataMCDataTable.Count > 0) _
                                                     AndAlso DataMCDataTable(0).Valor_Primera_Captura.ToString() <> DataMCDataTable(0).Valor_Segunda_Captura.ToString() _
                                                     AndAlso DataMCDataTable(0).Valor_Primera_Captura.ToString() <> DataMCDataTable(0).Valor_Tercera_Captura.ToString() _
                                                     AndAlso DataMCDataTable(0).Valor_Segunda_Captura.ToString() <> DataMCDataTable(0).Valor_Tercera_Captura.ToString() Then
                                                    PasaCalidad = True
                                                End If
                                            End If
                                        Else
                                            CamposQuery.Add(Campo)
                                        End If
                                    Else
                                        Dim ValueTable As DataTable = CType(Campo.Control.Value, DataTable)
                                        Dim ValueOld1Table As DataTable = CType(Campo.Control.ValueOld1, DataTable)
                                        Dim ValueOld2Table As DataTable = CType(Campo.Control.ValueOld2, DataTable)

                                        Dim Col As Integer = 0
                                        Dim ErrorTabla As Boolean = False
                                        Dim TablaControl = CType(Campo.Control, TableInputControl)

                                        For Each TablaAsociadaRow As DBImaging.SchemaConfig.CTA_Tabla_AsociadaRow In TablaAsociadaDataTable.Select("fk_Campo = " & Campo.id)
                                            Dim Fil As Integer = 1

                                            For Each Registro As DataRow In ValueTable.Rows
                                                If (Campo.Control.IsVisible And Not TablaControl.DefinicionCaptura(Col).IsReadOnly) Then
                                                    Dim CampoType = New DBCore.SchemaProcess.TBL_File_Data_AsociadaType()
                                                    Dim TableValue As String

                                                    TableValue = Registro.Item(Col).ToString()

                                                    CampoType.Valor_File_Data = TableValue
                                                    CampoType.Conteo_File_Data = TableValue.Length

                                                    Dim MCAsociadaType = New DBImaging.SchemaProcess.TBL_File_Data_MC_AsociadaType()
                                                    MCAsociadaType.Valor_Tercera_Captura = TableValue
                                                    dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBUpdate(MCAsociadaType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version, FileCoreRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil))

                                                    'aca
                                                    Dim DocumentoUsaCapturaCorreccionMaquina = dbmImaging.SchemaConfig.TBL_Campo.DBFindByfk_Documentoid_CampoUsa_Captura_Correccion_Maquina(FileCoreRow.fk_Documento, Campo.id, True)

                                                    If DocumentoUsaCapturaCorreccionMaquina.Rows.Count > 0 Then
                                                        If DocumentoUsaCapturaCorreccionMaquina(0).Usa_Captura_Correccion_Maquina Then
                                                            Errores(0).Usuario = FileType.Usuario_Correccion_Maquina
                                                            Errores(0).Fecha = FileType.Fecha_Correccion_Maquina
                                                        Else
                                                            Errores(0).Usuario = FileType.Usuario_Primera_Captura
                                                            Errores(0).Fecha = FileType.Fecha_Primera_Captura
                                                        End If
                                                    Else
                                                        Errores(0).Usuario = FileType.Usuario_Primera_Captura
                                                        Errores(0).Fecha = FileType.Fecha_Primera_Captura
                                                    End If

                                                    Productividad.Caracteres += TableValue.Length

                                                    Dim ValueOld1 As String = ""
                                                    If (ValueOld1Table.Rows.Count > Fil - 1) Then
                                                        ValueOld1 = ValueOld1Table.Rows(Fil - 1).Item(Col).ToString()
                                                    End If

                                                    Dim ValueOld2 As String = ""
                                                    If (ValueOld2Table.Rows.Count > Fil - 1) Then
                                                        ValueOld2 = ValueOld2Table.Rows(Fil - 1).Item(Col).ToString()
                                                    End If

                                                    'Conteo de Errores
                                                    If TableValue <> ValueOld1 Then
                                                        Productividad.Campos += 1
                                                        Errores(0).Campos += 1
                                                        Productividad.Caracteres += ValueOld1.Length
                                                        InsertarProductividad_Error_Detallado(dbmImaging, Campo, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil), Errores(0), ValueOld1, Me.IndexerSesion.Usuario.id, TableValue, DesktopConfig.Etapa_Productividad.Primera_Captura)
                                                    End If

                                                    If TableValue <> ValueOld2 Then
                                                        Productividad.Campos += 1
                                                        Errores(1).Campos += 1
                                                        Errores(1).Caracteres += ValueOld2.Length
                                                        InsertarProductividad_Error_Detallado(dbmImaging, Campo, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil), Errores(1), ValueOld2, Me.IndexerSesion.Usuario.id, TableValue, DesktopConfig.Etapa_Productividad.Segunda_Captura)
                                                    End If

                                                    dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBUpdate(CampoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil))

                                                    ' Buscar registro primnera captura
                                                    Dim DataMCAsociadaDataTable = dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version, FileCoreRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil))
                                                    If (DataMCAsociadaDataTable.Count > 0) _
                                                         AndAlso DataMCAsociadaDataTable(0).Valor_Primera_Captura.ToString() <> DataMCAsociadaDataTable(0).Valor_Segunda_Captura.ToString() _
                                                         AndAlso DataMCAsociadaDataTable(0).Valor_Primera_Captura.ToString() <> DataMCAsociadaDataTable(0).Valor_Tercera_Captura.ToString() _
                                                         AndAlso DataMCAsociadaDataTable(0).Valor_Segunda_Captura.ToString() <> DataMCAsociadaDataTable(0).Valor_Tercera_Captura.ToString() Then

                                                        ' Pasar a calidad
                                                        ErrorTabla = True
                                                        PasaCalidad = True
                                                    End If
                                                End If

                                                Fil += 1
                                            Next

                                            Col += 1
                                        Next

                                        Dim MCType = New DBImaging.SchemaProcess.TBL_File_Data_MCType()
                                        MCType.Valor_Tercera_Captura = CInt(IIf(ErrorTabla, 3, 1))
                                        dbmImaging.SchemaProcess.TBL_File_Data_MC.DBUpdate(MCType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, FileImagingRow.id_Version)
                                    End If
                                End If
                            Next

                            'Almacena los valores de los campos tipo Query // Se almacenan de ultimo porque puden depender de los valores antes insertados
                            For Each Campo In CamposQuery
                                Try
                                    Dim CampoType = New DBCore.SchemaProcess.TBL_File_DataType()
                                    CampoType.Conteo_File_Data = 0
                                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(CampoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)

                                    Dim Parametros As New List(Of Slyg.Data.Schemas.Parameter)
                                    Dim Par1 As New Slyg.Data.Schemas.Parameter("@Expediente", FileCoreRow.fk_Expediente)
                                    Dim Par2 As New Slyg.Data.Schemas.Parameter("@Folder", FileCoreRow.fk_Folder)
                                    Dim Par3 As New Slyg.Data.Schemas.Parameter("@File", FileCoreRow.id_File)
                                    Parametros.Add(Par1) : Parametros.Add(Par2) : Parametros.Add(Par3)
                                    Dim QueryRecuperacion = dbmCore.DataBase.ExecuteStoredProcedureGet("DBO", "PA_CAMPO_QUERY_" & CStr(Me.ExpedienteRow.fk_Entidad) & "_" & CStr(FileCoreRow.fk_Documento) & "_" & CStr(Campo.id), Parametros)
                                    If QueryRecuperacion.Rows.Count > 0 Then CampoType.Valor_File_Data = Utilities.DStr(QueryRecuperacion.Rows(0)(0).ToString())

                                    dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(CampoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)

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

                                            ' Actualizar mesa de control
                                            Dim MCType = New DBImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MCType()
                                            MCType.Valor_Tercera_Captura = CampoLlave.Control.Value.ToString()
                                            dbmImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MC.DBUpdate(MCType, FileCoreRow.fk_Expediente, CampoLlave.Id_Campo)

                                            If (Not PasaCalidad) Then
                                                Dim DataMCDataTable = dbmImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MC.DBGet(FileCoreRow.fk_Expediente, CampoLlave.Id_Campo)
                                                If (DataMCDataTable.Count > 0) _
                                                     AndAlso DataMCDataTable(0).Valor_Primera_Captura.ToString() <> DataMCDataTable(0).Valor_Segunda_Captura.ToString() _
                                                     AndAlso DataMCDataTable(0).Valor_Primera_Captura.ToString() <> DataMCDataTable(0).Valor_Tercera_Captura.ToString() _
                                                     AndAlso DataMCDataTable(0).Valor_Segunda_Captura.ToString() <> DataMCDataTable(0).Valor_Tercera_Captura.ToString() Then
                                                    PasaCalidad = True
                                                End If
                                            End If
                                        End If
                                    Next

                                    dbmCore.SchemaProcess.PA_Guarda_Expediente_Llave_En_Linea.DBExecute(FileCoreRow.fk_Expediente, CShort(_CamposLlave(0).fk_Entidad), CShort(_CamposLlave(0).fk_Proyecto), CShort(_CamposLlave(0).fk_Esquema), Llaves, CampoEmpaque)
                                End If
                            End If

                            'Crea el estado del Folder en Core
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

                            ' Insertar productividad
                            InsertarProductividad(dbmImaging, Productividad, DesktopConfig.Etapa_Productividad.Tercera_Captura, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                            InsertarProductividad_Error(dbmImaging, Errores, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                            ' Actualizar llaves
                            dbmCore.SchemaProcess.PA_Calcular_Llaves.DBExecute(FileImagingRow.fk_Expediente)

                            ' Aplicar validaciones automáticas
                            dbmImaging.SchemaProcess.PA_Aplicar_Validaciones_Automaticas.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento)

                            ' Actualizar estado
                            Dim NextEstado As Short = CShort(IIf(PasaCalidad And Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Calidad, DBCore.EstadoEnum.Calidad_Captura, DBCore.EstadoEnum.Recorte))

                            ' Preparar los recortes
                            If (NextEstado = DBCore.EstadoEnum.Recorte) Then
                                If Not (Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Recortes AndAlso dbmImaging.SchemaProcess.PA_Preparar_Recortes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)) Then
                                    NextEstado = DBCore.EstadoEnum.Indexado
                                End If
                            End If

                            ' Establece estado heredado del doccumento de captura campos llaves, cuando este no esta en el expediente.
                            'If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                            '    If (NextEstado = DBCore.EstadoEnum.Indexado) Then
                            '        NextEstado = dbmImaging.SchemaProcess.PA_Next_Estado_Documento_Captura_Campos_Llave.DBExecute(Me.IndexerImagingGlobal.Entidad, Me.IndexerImagingGlobal.Proyecto, Me.fk_Esquema, CType(FileCoreRow.fk_Expediente, Global.Slyg.Tools.SlygNullable(Of Integer)), DBCore.EstadoEnum.Tercera_Captura)
                            '    End If
                            'End If

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
                                dbmImaging.SchemaProcess.PA_Cruce_En_Linea_Cola.DBExecute(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, "3")
                            End If

                            'dbmCore.Transaction_Commit()
                            dbmImaging.Transaction_Commit()

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

                            DesktopMessageBoxControl.DesktopMessageShow("Publicar_Tercera", ex)

                            Return False
                        Finally
                            'If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        End Try

                        EventManager.FinalizarTerceraCaptura(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                        '-------------------------------------------------------------------------------------------------------------
                        ' LOGGING - PERFORMANCE
                        '-------------------------------------------------------------------------------------------------------------
                        Dim FechaFinProceso = Now
                        Dim TraceMessage As String = "[Tercera Captura][Publicar] - Inicio: " & FechaInicioProceso.ToString() & " Fin: " & FechaFinProceso.ToString()
                        DesktopTrace.Trace(TraceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, Program.AssemblyTitle)
                        '-------------------------------------------------------------------------------------------------------------

                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("Publicar_Tercera", ex)

                        Return False
                    End Try

                    Return True
                End If
            ElseIf AnexoImagingRow IsNot Nothing Then
                If (Validar()) Then
                    Dim Productividad As New DesktopConfig.ProductividadType
                    Dim Errores(1) As DesktopConfig.ProductividadType

                    Errores(0) = New DesktopConfig.ProductividadType
                    Errores(1) = New DesktopConfig.ProductividadType

                    Productividad.Usuario = Me.IndexerSesion.Usuario.id

                    Try


                        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)
                        dbmCore = New DBCore.DBCoreDataBaseManager(dbmImaging, Me.IndexerDesktopGlobal.ConnectionStrings.Core)

                        dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                        Dim FechaInicioProceso = Now

                        Try
                            ''--------------------------------------------------------------------------------------
                            '' REPORTAR DUPLICADOS
                            ''--------------------------------------------------------------------------------------
                            'Try
                            '    Dim FileBloqueadoDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                            '    If (FileBloqueadoDataTable(0).fk_Usuario_log <> Me.IndexerSesion.Usuario.id) Then
                            '        dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Tercera_Captura, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PcName)
                            '    End If

                            'Catch : End Try
                            ''--------------------------------------------------------------------------------------


                            dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                            ' Reportar usuario captura
                            Dim AnexoType = New DBImaging.SchemaProcess.TBL_AnexoType()
                            AnexoType.Usuario_Tercera_Captura = Me.IndexerSesion.Usuario.id
                            AnexoType.Fecha_Tercera_Captura = SlygNullable.SysDate
                            dbmImaging.SchemaProcess.TBL_Anexo.DBUpdate(AnexoType, AnexoImagingRow.id_Anexo)

                            Dim AnexoTable = dbmImaging.SchemaProcess.TBL_Anexo.DBGet(AnexoImagingRow.id_Anexo)
                            AnexoType = AnexoTable(0).ToTBL_AnexoType()

                            Errores(1).Usuario = AnexoType.Usuario_Segunda_Captura
                            Errores(1).Fecha = AnexoType.Fecha_Segunda_Captura

                            ' Actualizar datos capturados
                            Dim PasaCalidad As Boolean = False
                            Dim CamposQuery As New List(Of CampoCaptura)
                            For Each Campo In _Campos
                                If Campo.Control.IsVisible Then
                                    If (Campo.Control.Tipo <> DesktopConfig.CampoTipo.TablaAsociada) Then

                                        If Campo.Control.Tipo <> DesktopConfig.CampoTipo.Query Then
                                            Dim CampoType = New DBCore.SchemaProcess.TBL_Anexo_DataType()

                                            CampoType.Valor_File_Data = Campo.Control.Value
                                            CampoType.Conteo_File_Data = Campo.Control.Value.ToString().Length

                                            ' Actualizar mesa de control
                                            Dim MCType = New DBImaging.SchemaProcess.TBL_Anexo_Data_MCType()
                                            MCType.Valor_Tercera_Captura = Campo.Control.Value
                                            dbmImaging.SchemaProcess.TBL_Anexo_Data_MC.DBUpdate(MCType, AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id)


                                            Errores(0).Usuario = AnexoType.Usuario_Primera_Captura
                                            Errores(0).Fecha = AnexoType.Fecha_Primera_Captura


                                            If Campo.Control.IsVisible Then
                                                Productividad.Campos += 1
                                                Productividad.Caracteres += Campo.Control.Value.ToString().Length

                                                'Conteo de Errores
                                                If Campo.Control.Value.ToString() <> Campo.Control.ValueOld1.ToString() Then
                                                    Errores(0).Campos += 1
                                                    Errores(0).Caracteres += Campo.Control.ValueOld1.ToString().Length
                                                    'InsertarProductividad_Error_Detallado(dbmImaging, Campo, Nothing, Nothing, Errores(0), Campo.Control.ValueOld1, Me.IndexerSesion.Usuario.id, Campo.Control.Value, DesktopConfig.Etapa_Productividad.Primera_Captura)
                                                End If

                                                If Campo.Control.Value.ToString() <> Campo.Control.ValueOld2.ToString() Then
                                                    Errores(1).Campos += 1
                                                    Errores(1).Caracteres += Campo.Control.ValueOld2.ToString().Length
                                                    'InsertarProductividad_Error_Detallado(dbmImaging, Campo, Nothing, Nothing, Errores(1), Campo.Control.ValueOld2, Me.IndexerSesion.Usuario.id, Campo.Control.Value, DesktopConfig.Etapa_Productividad.Segunda_Captura)
                                                End If
                                            End If

                                            dbmCore.SchemaProcess.TBL_Anexo_Data.DBUpdate(CampoType, AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id)

                                            'If (Not PasaCalidad) Then
                                            '    Dim DataMCDataTable = dbmImaging.SchemaProcess.TBL_File_Data_MC.DBGet(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, FileImagingRow.id_Version)
                                            '    If (DataMCDataTable.Count > 0) _
                                            '         AndAlso DataMCDataTable(0).Valor_Primera_Captura.ToString() <> DataMCDataTable(0).Valor_Segunda_Captura.ToString() _
                                            '         AndAlso DataMCDataTable(0).Valor_Primera_Captura.ToString() <> DataMCDataTable(0).Valor_Tercera_Captura.ToString() _
                                            '         AndAlso DataMCDataTable(0).Valor_Segunda_Captura.ToString() <> DataMCDataTable(0).Valor_Tercera_Captura.ToString() Then
                                            '        PasaCalidad = True
                                            '    End If
                                            'End If
                                        Else
                                            CamposQuery.Add(Campo)
                                        End If
                                    Else
                                        Dim ValueTable As DataTable = CType(Campo.Control.Value, DataTable)
                                        Dim ValueOld1Table As DataTable = CType(Campo.Control.ValueOld1, DataTable)
                                        Dim ValueOld2Table As DataTable = CType(Campo.Control.ValueOld2, DataTable)

                                        Dim Col As Integer = 0
                                        Dim ErrorTabla As Boolean = False
                                        Dim TablaControl = CType(Campo.Control, TableInputControl)

                                        For Each TablaAsociadaRow As DBImaging.SchemaConfig.CTA_Tabla_AsociadaRow In TablaAsociadaDataTable.Select("fk_Campo = " & Campo.id)
                                            Dim Fil As Integer = 1

                                            For Each Registro As DataRow In ValueTable.Rows
                                                If (Campo.Control.IsVisible And Not TablaControl.DefinicionCaptura(Col).IsReadOnly) Then
                                                    Dim CampoType = New DBCore.SchemaProcess.TBL_Anexo_Data_AsociadaType()
                                                    Dim TableValue As String

                                                    TableValue = Registro.Item(Col).ToString()

                                                    CampoType.Valor_File_Data = TableValue
                                                    CampoType.Conteo_File_Data = TableValue.Length

                                                    Dim MCAsociadaType = New DBImaging.SchemaProcess.TBL_Anexo_Data_MC_AsociadaType()
                                                    MCAsociadaType.Valor_Tercera_Captura = TableValue
                                                    dbmImaging.SchemaProcess.TBL_Anexo_Data_MC_Asociada.DBUpdate(MCAsociadaType, AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil))

                                                  
                                                    Errores(0).Usuario = AnexoType.Usuario_Primera_Captura
                                                    Errores(0).Fecha = AnexoType.Fecha_Primera_Captura


                                                    Productividad.Caracteres += TableValue.Length

                                                    Dim ValueOld1 As String = ""
                                                    If (ValueOld1Table.Rows.Count > Fil - 1) Then
                                                        ValueOld1 = ValueOld1Table.Rows(Fil - 1).Item(Col).ToString()
                                                    End If

                                                    Dim ValueOld2 As String = ""
                                                    If (ValueOld2Table.Rows.Count > Fil - 1) Then
                                                        ValueOld2 = ValueOld2Table.Rows(Fil - 1).Item(Col).ToString()
                                                    End If

                                                    'Conteo de Errores
                                                    If TableValue <> ValueOld1 Then
                                                        Productividad.Campos += 1
                                                        Errores(0).Campos += 1
                                                        Productividad.Caracteres += ValueOld1.Length
                                                        'InsertarProductividad_Error_Detallado(dbmImaging, Campo, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil), Errores(0), ValueOld1, Me.IndexerSesion.Usuario.id, TableValue, DesktopConfig.Etapa_Productividad.Primera_Captura)
                                                    End If

                                                    If TableValue <> ValueOld2 Then
                                                        Productividad.Campos += 1
                                                        Errores(1).Campos += 1
                                                        Errores(1).Caracteres += ValueOld2.Length
                                                        'InsertarProductividad_Error_Detallado(dbmImaging, Campo, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil), Errores(1), ValueOld2, Me.IndexerSesion.Usuario.id, TableValue, DesktopConfig.Etapa_Productividad.Segunda_Captura)
                                                    End If

                                                    dbmCore.SchemaProcess.TBL_Anexo_Data_Asociada.DBUpdate(CampoType, AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil))

                                                    ' Buscar registro primnera captura
                                                    Dim DataMCAsociadaDataTable = dbmImaging.SchemaProcess.TBL_Anexo_Data_MC_Asociada.DBGet(AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id, CShort(TablaAsociadaRow.id_Campo_Tabla), CShort(Fil))
                                                    If (DataMCAsociadaDataTable.Count > 0) _
                                                         AndAlso DataMCAsociadaDataTable(0).Valor_Primera_Captura.ToString() <> DataMCAsociadaDataTable(0).Valor_Segunda_Captura.ToString() _
                                                         AndAlso DataMCAsociadaDataTable(0).Valor_Primera_Captura.ToString() <> DataMCAsociadaDataTable(0).Valor_Tercera_Captura.ToString() _
                                                         AndAlso DataMCAsociadaDataTable(0).Valor_Segunda_Captura.ToString() <> DataMCAsociadaDataTable(0).Valor_Tercera_Captura.ToString() Then

                                                        ' Pasar a calidad
                                                        ErrorTabla = True
                                                        PasaCalidad = True
                                                    End If
                                                End If

                                                Fil += 1
                                            Next

                                            Col += 1
                                        Next

                                        Dim MCType = New DBImaging.SchemaProcess.TBL_Anexo_Data_MCType()
                                        MCType.Valor_Tercera_Captura = CInt(IIf(ErrorTabla, 3, 1))
                                        dbmImaging.SchemaProcess.TBL_Anexo_Data_MC.DBUpdate(MCType, AnexoImagingRow.id_Anexo, AnexoImagingRow.fk_Documento, Campo.id)
                                    End If
                                End If
                            Next

                            ''Almacena los valores de los campos tipo Query // Se almacenan de ultimo porque puden depender de los valores antes insertados
                            'For Each Campo In CamposQuery
                            '    Try
                            '        Dim CampoType = New DBCore.SchemaProcess.TBL_File_DataType()
                            '        CampoType.Conteo_File_Data = 0
                            '        dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(CampoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)

                            '        Dim Parametros As New List(Of Slyg.Data.Schemas.Parameter)
                            '        Dim Par1 As New Slyg.Data.Schemas.Parameter("@Expediente", FileCoreRow.fk_Expediente)
                            '        Dim Par2 As New Slyg.Data.Schemas.Parameter("@Folder", FileCoreRow.fk_Folder)
                            '        Dim Par3 As New Slyg.Data.Schemas.Parameter("@File", FileCoreRow.id_File)
                            '        Parametros.Add(Par1) : Parametros.Add(Par2) : Parametros.Add(Par3)
                            '        Dim QueryRecuperacion = dbmCore.DataBase.ExecuteStoredProcedureGet("DBO", "PA_CAMPO_QUERY_" & CStr(Me.ExpedienteRow.fk_Entidad) & "_" & CStr(FileCoreRow.fk_Documento) & "_" & CStr(Campo.id), Parametros)
                            '        If QueryRecuperacion.Rows.Count > 0 Then CampoType.Valor_File_Data = Utilities.DStr(QueryRecuperacion.Rows(0)(0).ToString())

                            '        dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(CampoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)

                            '    Catch ex As Exception
                            '        Throw New Exception("Ha ocurrido un error en el campo [" & Campo.id & "] de tipo [QUERY], es posible que contenga errores o no exista una definicion para este. --[" & ex.Message & "]--")
                            '    End Try
                            'Next

                            ''Crea el estado del Folder en Core
                            'Dim FolderEstadoDataTable = dbmCore.SchemaProcess.TBL_Folder_estado.DBFindByfk_Expedientefk_FolderModulo(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, DesktopConfig.Modulo.Imaging)

                            'If FolderEstadoDataTable.Count = 0 Then
                            '    Dim InsertEstado As New DBCore.SchemaProcess.TBL_Folder_estadoType()
                            '    InsertEstado.fk_Expediente = FileImagingRow.fk_Expediente
                            '    InsertEstado.fk_Folder = FileImagingRow.fk_Folder
                            '    InsertEstado.Modulo = DesktopConfig.Modulo.Imaging
                            '    InsertEstado.fk_Estado = DBCore.EstadoEnum.Indexado
                            '    InsertEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                            '    InsertEstado.Fecha_Log = SlygNullable.SysDate
                            '    dbmCore.SchemaProcess.TBL_Folder_estado.DBInsert(InsertEstado)
                            'End If

                            ' Insertar productividad
                            'InsertarProductividad(dbmImaging, Productividad, DesktopConfig.Etapa_Productividad.Tercera_Captura, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                            'InsertarProductividad_Error(dbmImaging, Errores, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                            ' Actualizar llaves
                            'dbmCore.SchemaProcess.PA_Calcular_Llaves.DBExecute(FileImagingRow.fk_Expediente)

                            ' Aplicar validaciones automáticas
                            'dbmImaging.SchemaProcess.PA_Aplicar_Validaciones_Automaticas.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento)

                            ' Actualizar estado
                            Dim NextEstado As Short = DBCore.EstadoEnum.Indexado

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


                            dbmImaging.Transaction_Commit()

                            '    Try
                            '        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                            '        Dim CarguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(_idCargue, _idCarguePaquete)
                            '        If (CarguePaqueteFileDataTable.Count > 0) Then
                            '            Dim UpdatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                            '            UpdatePaquete.fk_Estado = CarguePaqueteFileDataTable(0).Estado_File
                            '            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(UpdatePaquete, _idCargue, _idCarguePaquete)

                            '            Dim CargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(_idCargue)
                            '            If (CargueFileDataTable.Count > 0) Then
                            '                Dim UpdateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                            '                UpdateCargue.fk_Estado = CargueFileDataTable(0).Estado_File
                            '                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(UpdateCargue, _idCargue)
                            '            End If
                            '        End If

                            '        dbmImaging.Transaction_Commit()
                            '    Catch ex As Exception
                            '        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                            '        Throw
                            '    End Try


                        Catch ex As Exception
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                            DesktopMessageBoxControl.DesktopMessageShow("Publicar_Tercera", ex)

                            Return False
                        Finally
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        End Try

                        'EventManager.FinalizarTerceraCaptura(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                        EventManager.FinalizarTerceraCapturaAnexo(AnexoImagingRow.id_Anexo)
                        '-------------------------------------------------------------------------------------------------------------
                        ' LOGGING - PERFORMANCE
                        '-------------------------------------------------------------------------------------------------------------
                        Dim FechaFinProceso = Now
                        Dim TraceMessage As String = "[Tercera Captura][Publicar] - Inicio: " & FechaInicioProceso.ToString() & " Fin: " & FechaFinProceso.ToString()
                        DesktopTrace.Trace(TraceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, Program.AssemblyTitle)
                        '-------------------------------------------------------------------------------------------------------------

                    Catch ex As Exception
                        DesktopMessageBoxControl.DesktopMessageShow("Publicar_Tercera", ex)

                        Return False
                    End Try

                    Return True
                End If
            End If
            Return False
        End Function

        Public Overrides Function ValidacionListas() As Boolean
            Dim _ValidacionListas As Boolean

            _ValidacionListas = False

            Return _ValidacionListas
        End Function

        Public Overrides Function Campos(ByVal idDocumento As Integer) As List(Of CampoCaptura)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Me._Campos.Clear()

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)
                If AnexoImagingRow IsNot Nothing Then
                    For Each RowData In MesaControlDataTableAnexo
                        If (Not RowData.IsValor_Primera_CapturaNull And Not RowData.IsValor_Segunda_CapturaNull) AndAlso (RowData.Valor_Primera_Captura.ToString() <> RowData.Valor_Segunda_Captura.ToString()) Then

                            Dim CampoRow As DBImaging.SchemaConfig.CTA_CampoRow = CType(CamposDataTable.Select("fk_Documento = " & idDocumento & " AND id_Campo = " & RowData.fk_Campo)(0), DBImaging.SchemaConfig.CTA_CampoRow)
                            Dim ControlCaptura As View.IInputControl
                            Dim CampoCaptura As New CampoCaptura()
                            Dim ListDefinicionCaptura As New List(Of DefinicionCaptura)


                            ' Control
                            Select Case CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)

                                Case DesktopConfig.CampoTipo.Texto
                                    Dim DefinicionCaptura = New DefinicionCaptura()


                                    If RowData.Valor_Primera_Captura.ToString().Contains(" ") Then
                                        RowData.Valor_Primera_Captura = RowData.Valor_Primera_Captura.ToString().Replace(" ", "-")
                                    End If

                                    If RowData.Valor_Segunda_Captura.ToString().Contains(" ") Then
                                        RowData.Valor_Segunda_Captura = RowData.Valor_Segunda_Captura.ToString().Replace(" ", "-")
                                    End If

                                    ControlCaptura = New TextInputControl()

                                    ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura

                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

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

                                    ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura

                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

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

                                    ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura

                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

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

                                    ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura

                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.Lista
                                    DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                                    DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                                    ' Datos de la lista
                                    If (Not CampoRow.Isfk_Campo_ListaNull()) Then
                                        Dim dtvLista1 As New DataView(ListaItemsDataTable)
                                        Dim dtvLista2 As New DataView(ListaItemsDataTable)
                                        Dim dtvLista As New DataView(ListaItemsDataTable)

                                        dtvLista1.RowFilter = "fk_Campo_Lista = " & CampoRow.fk_Campo_Lista
                                        dtvLista2.RowFilter = "fk_Campo_Lista = " & CampoRow.fk_Campo_Lista
                                        dtvLista.RowFilter = "fk_Campo_Lista = " & CampoRow.fk_Campo_Lista

                                        Dim ListControl = CType(ControlCaptura, ListInputControl)
                                        ListControl.ValueOld1DesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                                        ListControl.ValueOld1DesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                        ListControl.ValueOld1DesktopComboBox.DataSource = dtvLista1

                                        ListControl.ValueOld1DesktopComboBox.SelectedValue = RowData.Valor_Primera_Captura

                                        ListControl.ValueDesktopComboBox.Refresh()

                                        ListControl.ValueOld2DesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                                        ListControl.ValueOld2DesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                        ListControl.ValueOld2DesktopComboBox.DataSource = dtvLista2
                                        ListControl.ValueOld2DesktopComboBox.SelectedValue = RowData.Valor_Segunda_Captura
                                        ListControl.ValueDesktopComboBox.Refresh()

                                        ListControl.ValueDesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                                        ListControl.ValueDesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                        ListControl.ValueDesktopComboBox.DataSource = dtvLista
                                        ListControl.ValueDesktopComboBox.SelectedValue = -1
                                        ListControl.ValueDesktopComboBox.Refresh()
                                    End If

                                    ListDefinicionCaptura.Add(DefinicionCaptura)
                                    ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                                Case DesktopConfig.CampoTipo.SiNo
                                    Dim DefinicionCaptura = New DefinicionCaptura()

                                    ControlCaptura = New ListInputControl()

                                    If (RowData.Valor_Primera_Captura.ToString() = "1") Then
                                        RowData.Valor_Primera_Captura = "Si"
                                    Else
                                        RowData.Valor_Primera_Captura = "No"
                                    End If


                                    If (RowData.Valor_Segunda_Captura.ToString() = "1") Then
                                        RowData.Valor_Segunda_Captura = "Si"
                                    Else
                                        RowData.Valor_Segunda_Captura = "No"
                                    End If


                                    ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura


                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.SiNo
                                    DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                                    DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                                    Dim ListControl = CType(ControlCaptura, ListInputControl)
                                    ListControl.ValueOld1DesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                                    ListControl.ValueOld1DesktopComboBox.Items.Add("Si")
                                    ListControl.ValueOld1DesktopComboBox.Items.Add("No")
                                    ListControl.ValueOld1DesktopComboBox.SelectedValue = RowData.Valor_Primera_Captura


                                    ListControl.ValueOld2DesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                                    ListControl.ValueOld2DesktopComboBox.Items.Add("Si")
                                    ListControl.ValueOld2DesktopComboBox.Items.Add("No")
                                    ListControl.ValueOld2DesktopComboBox.SelectedValue = RowData.Valor_Segunda_Captura

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
                                    Dim Value1Table = New DataTable()
                                    Dim Value2Table = New DataTable()
                                    Dim Columna As DataColumn

                                    ControlCaptura = New TableInputControl()

                                    For Each TablaAsociadaRow As DBImaging.SchemaConfig.CTA_Tabla_AsociadaRow In TablaAsociadaDataTable.Select("fk_Campo = " & CampoRow.id_Campo)
                                        Dim DefinicionCaptura = New DefinicionCaptura()

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
                                                Throw New Exception("Tipo de campo no valido para una tabla asociada: " & CampoRow.fk_Campo_Tipo)

                                        End Select

                                        ListDefinicionCaptura.Add(DefinicionCaptura)

                                        ValueTable.Columns.Add(Columna)
                                        Value1Table.Columns.Add(ClonarColumna(Columna))
                                        Value2Table.Columns.Add(ClonarColumna(Columna))
                                    Next

                                    Dim AnexoDataAsociadaDataTable = dbmImaging.SchemaProcess.TBL_Anexo_Data_MC_Asociada.DBGet(RowData.fk_Anexo, RowData.fk_Documento, CampoRow.id_Campo, Nothing, Nothing)
                                    Dim AnexoDataAsociadaView = New DataView(AnexoDataAsociadaDataTable)
                                    Dim RegistrosAsociada = AnexoDataAsociadaView.ToTable(True, "fk_Anexo", "fk_Documento", "fk_Campo", "fk_File_Data_Asociada")

                                    For Each ItemRow As DataRow In RegistrosAsociada.Rows
                                        Dim NewRow1 = Value1Table.NewRow()
                                        Dim NewRow2 = Value2Table.NewRow()

                                        AnexoDataAsociadaView.RowFilter = "fk_File_Data_Asociada = " & ItemRow.Item("fk_File_Data_Asociada").ToString()
                                        AnexoDataAsociadaView.Sort = "fk_Campo_Tabla"

                                        For Col As Integer = 0 To AnexoDataAsociadaView.Count - 1
                                            If (Col < ValueTable.Columns.Count) Then
                                                NewRow1.Item(Col) = AnexoDataAsociadaView.Item(Col).Item("Valor_Primera_Captura")
                                                NewRow2.Item(Col) = AnexoDataAsociadaView.Item(Col).Item("Valor_Segunda_Captura")
                                            End If
                                        Next

                                        Value1Table.Rows.Add(NewRow1)
                                        Value2Table.Rows.Add(NewRow2)
                                    Next

                                    ControlCaptura.Value = ValueTable
                                    ControlCaptura.ValueOld1 = Value1Table
                                    ControlCaptura.ValueOld2 = Value2Table

                                    ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                                Case Else
                                    Throw New Exception("Tipo de campo no válido para segunda captura: " & CampoRow.fk_Campo_Tipo)

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
                            ControlCaptura.ShowSecondControls = True
                            ControlCaptura.Tipo = CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                            ControlCaptura.Etiqueta = CampoRow.Nombre_Campo
                            ControlCaptura.CampoCaptura = CampoCaptura
                            ControlCaptura.ShowSecondControls = True
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
                        End If
                    Next
                ElseIf FileCoreRow IsNot Nothing Then
                    For Each RowData In MesaControlDataTable
                        If ((Not RowData.IsValor_Primera_CapturaNull And Not RowData.IsValor_Segunda_CapturaNull) AndAlso (RowData.Valor_Primera_Captura.ToString().ToUpper() <> RowData.Valor_Segunda_Captura.ToString().ToUpper()) Or (Not RowData.IsValor_Correccion_Captura_MaquinaNull And Not RowData.IsValor_Segunda_CapturaNull) AndAlso (RowData.Valor_Correccion_Captura_Maquina.ToString().ToUpper() <> RowData.Valor_Segunda_Captura.ToString().ToUpper())) Then

                            Dim CampoRow As DBImaging.SchemaConfig.CTA_CampoRow = CType(CamposDataTable.Select("fk_Documento = " & idDocumento & " AND id_Campo = " & RowData.fk_Campo)(0), DBImaging.SchemaConfig.CTA_CampoRow)
                            Dim ControlCaptura As View.IInputControl
                            Dim CampoCaptura As New CampoCaptura()
                            Dim ListDefinicionCaptura As New List(Of DefinicionCaptura)

                            Dim Es_Campo_Correccion = dbmImaging.SchemaConfig.TBL_Campo.DBFindByfk_Documentoid_CampoUsa_Captura_Correccion_Maquina(idDocumento, CampoRow.id_Campo, True)

                            ' Control
                            Select Case CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)

                                Case DesktopConfig.CampoTipo.Texto
                                    Dim DefinicionCaptura = New DefinicionCaptura()


                                    If RowData.Valor_Primera_Captura.ToString().Contains(" ") Then
                                        RowData.Valor_Primera_Captura = RowData.Valor_Primera_Captura.ToString().Replace(" ", "-")
                                    End If

                                    If RowData.Valor_Correccion_Captura_Maquina.ToString().Contains(" ") Then
                                        RowData.Valor_Correccion_Captura_Maquina = RowData.Valor_Correccion_Captura_Maquina.ToString().Replace(" ", "-")
                                    End If

                                    If RowData.Valor_Segunda_Captura.ToString().Contains(" ") Then
                                        RowData.Valor_Segunda_Captura = RowData.Valor_Segunda_Captura.ToString().Replace(" ", "-")
                                    End If

                                    ControlCaptura = New TextInputControl()
                                    If Es_Campo_Correccion.Count = 0 Then
                                        ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                    Else
                                        ControlCaptura.ValueOld1 = RowData.Valor_Correccion_Captura_Maquina
                                    End If
                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

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
                                    If Es_Campo_Correccion.Count = 0 Then
                                        ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                    Else
                                        ControlCaptura.ValueOld1 = RowData.Valor_Correccion_Captura_Maquina
                                    End If
                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

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
                                    If Es_Campo_Correccion.Count = 0 Then
                                        ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                    Else
                                        ControlCaptura.ValueOld1 = RowData.Valor_Correccion_Captura_Maquina
                                    End If
                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

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

                                    If Es_Campo_Correccion.Count = 0 Then
                                        ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                    Else
                                        ControlCaptura.ValueOld1 = RowData.Valor_Correccion_Captura_Maquina
                                    End If
                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.Lista
                                    DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                                    DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                                    ' Datos de la lista
                                    If (Not CampoRow.Isfk_Campo_ListaNull()) Then
                                        Dim dtvLista1 As New DataView(ListaItemsDataTable)
                                        Dim dtvLista2 As New DataView(ListaItemsDataTable)
                                        Dim dtvLista As New DataView(ListaItemsDataTable)

                                        dtvLista1.RowFilter = "fk_Campo_Lista = " & CampoRow.fk_Campo_Lista
                                        dtvLista2.RowFilter = "fk_Campo_Lista = " & CampoRow.fk_Campo_Lista
                                        dtvLista.RowFilter = "fk_Campo_Lista = " & CampoRow.fk_Campo_Lista

                                        Dim ListControl = CType(ControlCaptura, ListInputControl)
                                        ListControl.ValueOld1DesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                                        ListControl.ValueOld1DesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                        ListControl.ValueOld1DesktopComboBox.DataSource = dtvLista1
                                        If Es_Campo_Correccion.Count = 0 Then
                                            ListControl.ValueOld1DesktopComboBox.SelectedValue = RowData.Valor_Primera_Captura
                                        Else
                                            ListControl.ValueOld1DesktopComboBox.SelectedValue = RowData.Valor_Correccion_Captura_Maquina
                                        End If
                                        ListControl.ValueDesktopComboBox.Refresh()

                                        ListControl.ValueOld2DesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                                        ListControl.ValueOld2DesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                        ListControl.ValueOld2DesktopComboBox.DataSource = dtvLista2
                                        ListControl.ValueOld2DesktopComboBox.SelectedValue = RowData.Valor_Segunda_Captura
                                        ListControl.ValueDesktopComboBox.Refresh()

                                        ListControl.ValueDesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                                        ListControl.ValueDesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                        ListControl.ValueDesktopComboBox.DataSource = dtvLista
                                        ListControl.ValueDesktopComboBox.SelectedValue = -1
                                        ListControl.ValueDesktopComboBox.Refresh()
                                    End If

                                    ListDefinicionCaptura.Add(DefinicionCaptura)
                                    ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                                Case DesktopConfig.CampoTipo.SiNo
                                    Dim DefinicionCaptura = New DefinicionCaptura()

                                    ControlCaptura = New ListInputControl()
                                    If Es_Campo_Correccion.Count = 0 Then
                                        If (RowData.Valor_Primera_Captura.ToString() = "1") Then
                                            RowData.Valor_Primera_Captura = "Si"
                                        Else
                                            RowData.Valor_Primera_Captura = "No"
                                        End If
                                    Else
                                        If (RowData.Valor_Correccion_Captura_Maquina.ToString() = "1") Then
                                            RowData.Valor_Correccion_Captura_Maquina = "Si"
                                        Else
                                            RowData.Valor_Correccion_Captura_Maquina = "No"
                                        End If
                                    End If

                                    If (RowData.Valor_Segunda_Captura.ToString() = "1") Then
                                        RowData.Valor_Segunda_Captura = "Si"
                                    Else
                                        RowData.Valor_Segunda_Captura = "No"
                                    End If

                                    If Es_Campo_Correccion.Count = 0 Then
                                        ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                    Else
                                        ControlCaptura.ValueOld1 = RowData.Valor_Correccion_Captura_Maquina
                                    End If

                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.SiNo
                                    DefinicionCaptura.Es_Obligatorio_Campo = CampoRow.Es_Obligatorio_Campo
                                    DefinicionCaptura.Caption = CampoRow.Nombre_Campo

                                    Dim ListControl = CType(ControlCaptura, ListInputControl)
                                    ListControl.ValueOld1DesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                                    ListControl.ValueOld1DesktopComboBox.Items.Add("Si")
                                    ListControl.ValueOld1DesktopComboBox.Items.Add("No")
                                    If Es_Campo_Correccion.Count = 0 Then
                                        ListControl.ValueOld1DesktopComboBox.SelectedValue = RowData.Valor_Primera_Captura
                                    Else
                                        ListControl.ValueOld1DesktopComboBox.SelectedValue = RowData.Valor_Correccion_Captura_Maquina
                                    End If


                                    ListControl.ValueOld2DesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                                    ListControl.ValueOld2DesktopComboBox.Items.Add("Si")
                                    ListControl.ValueOld2DesktopComboBox.Items.Add("No")
                                    ListControl.ValueOld2DesktopComboBox.SelectedValue = RowData.Valor_Segunda_Captura

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
                                    Dim Value1Table = New DataTable()
                                    Dim Value2Table = New DataTable()
                                    Dim Columna As DataColumn

                                    ControlCaptura = New TableInputControl()

                                    For Each TablaAsociadaRow As DBImaging.SchemaConfig.CTA_Tabla_AsociadaRow In TablaAsociadaDataTable.Select("fk_Campo = " & CampoRow.id_Campo)
                                        Dim DefinicionCaptura = New DefinicionCaptura()

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
                                                Throw New Exception("Tipo de campo no valido para una tabla asociada: " & CampoRow.fk_Campo_Tipo)

                                        End Select

                                        ListDefinicionCaptura.Add(DefinicionCaptura)

                                        ValueTable.Columns.Add(Columna)
                                        Value1Table.Columns.Add(ClonarColumna(Columna))
                                        Value2Table.Columns.Add(ClonarColumna(Columna))
                                    Next

                                    Dim FileDataAsociadaDataTable = dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBGet(RowData.fk_Expediente, RowData.fk_Folder, RowData.fk_File, FileImagingRow.id_Version, RowData.fk_Documento, CampoRow.id_Campo, Nothing, Nothing)
                                    Dim FileDataAsociadaView = New DataView(FileDataAsociadaDataTable)
                                    Dim RegistrosAsociada = FileDataAsociadaView.ToTable(True, "fk_Expediente", "fk_Folder", "fk_File", "fk_Version", "fk_Documento", "fk_Campo", "fk_File_Data_Asociada")

                                    For Each ItemRow As DataRow In RegistrosAsociada.Rows
                                        Dim NewRow1 = Value1Table.NewRow()
                                        Dim NewRow2 = Value2Table.NewRow()

                                        FileDataAsociadaView.RowFilter = "fk_File_Data_Asociada = " & ItemRow.Item("fk_File_Data_Asociada").ToString()
                                        FileDataAsociadaView.Sort = "fk_Campo_Tabla"

                                        For Col As Integer = 0 To FileDataAsociadaView.Count - 1
                                            If (Col < ValueTable.Columns.Count) Then
                                                NewRow1.Item(Col) = FileDataAsociadaView.Item(Col).Item("Valor_Primera_Captura")
                                                NewRow2.Item(Col) = FileDataAsociadaView.Item(Col).Item("Valor_Segunda_Captura")
                                            End If
                                        Next

                                        Value1Table.Rows.Add(NewRow1)
                                        Value2Table.Rows.Add(NewRow2)
                                    Next

                                    ControlCaptura.Value = ValueTable
                                    ControlCaptura.ValueOld1 = Value1Table
                                    ControlCaptura.ValueOld2 = Value2Table

                                    ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                                Case Else
                                    Throw New Exception("Tipo de campo no válido para segunda captura: " & CampoRow.fk_Campo_Tipo)

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

                            ControlCaptura.ShowSecondControls = True
                            ControlCaptura.Tipo = CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                            ControlCaptura.Etiqueta = CampoRow.Nombre_Campo
                            ControlCaptura.CampoCaptura = CampoCaptura
                            ControlCaptura.ShowSecondControls = True
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
                        End If
                    Next
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("getCampos", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return _Campos
        End Function

        Public Overrides Function CamposLlave(ByVal idDocumento As Integer) As List(Of CampoLlaveCaptura)
            Me._CamposLlave.Clear()

            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                For Each RowData In MesaControlCamposLlaveDataTable
                    If ((Not RowData.IsValor_Primera_CapturaNull And Not RowData.IsValor_Segunda_CapturaNull) AndAlso (RowData.Valor_Primera_Captura.ToString() <> RowData.Valor_Segunda_Captura.ToString()) Or (Not RowData.IsValor_Correccion_Captura_MaquinaNull And Not RowData.IsValor_Segunda_CapturaNull) AndAlso (RowData.Valor_Correccion_Captura_Maquina.ToString() <> RowData.Valor_Segunda_Captura.ToString())) Then
                        If CamposLlaveDataTable.Rows.Count > 0 Then
                            Dim CampoRow As DBImaging.SchemaConfig.CTA_Campo_LlaveRow = CType(CamposLlaveDataTable.Select("fk_Documento = " & idDocumento & " AND id_Campo = " & RowData.fk_Campo_LLave & " AND Usa_Doble_Captura = 1")(0), DBImaging.SchemaConfig.CTA_Campo_LlaveRow)
                            Dim ControlCaptura As View.IInputControl
                            Dim CampoLlaveCaptura As New CampoLlaveCaptura()
                            Dim ListDefinicionCaptura As New List(Of DefinicionCaptura)



                            ' Control
                            Select Case CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                                Case DesktopConfig.CampoTipo.Texto
                                    Dim DefinicionCaptura = New DefinicionCaptura()
                                    ControlCaptura = New TextInputControl()

                                    If RowData.Valor_Primera_Captura.ToString().Contains(" ") Then
                                        RowData.Valor_Primera_Captura = RowData.Valor_Primera_Captura.ToString().Replace(" ", "-")
                                    End If

                                    If RowData.Valor_Correccion_Captura_Maquina.ToString().Contains(" ") Then
                                        RowData.Valor_Correccion_Captura_Maquina = RowData.Valor_Correccion_Captura_Maquina.ToString().Replace(" ", "-")
                                    End If

                                    If RowData.Valor_Segunda_Captura.ToString().Contains(" ") Then
                                        RowData.Valor_Segunda_Captura = RowData.Valor_Segunda_Captura.ToString().Replace(" ", "-")
                                    End If

                                    ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.Texto

                                    DefinicionCaptura.MaximumLength = CampoRow.Length_Campo
                                    DefinicionCaptura.MinimumLength = CampoRow.Length_Min_Campo

                                    ListDefinicionCaptura.Add(DefinicionCaptura)
                                    ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                                Case DesktopConfig.CampoTipo.Numerico
                                    Dim DefinicionCaptura = New DefinicionCaptura()
                                    ControlCaptura = New TextInputNumericControl()

                                    ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.Numerico

                                    DefinicionCaptura.MaximumLength = CampoRow.Length_Campo
                                    DefinicionCaptura.MinimumLength = CampoRow.Length_Min_Campo

                                    ListDefinicionCaptura.Add(DefinicionCaptura)
                                    ControlCaptura.LoadDefinition(ListDefinicionCaptura)


                                Case DesktopConfig.CampoTipo.Fecha
                                    Dim DefinicionCaptura = New DefinicionCaptura()
                                    ControlCaptura = New TextInputDateTimeControl()

                                    ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                    ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura

                                    DefinicionCaptura.Type = DesktopConfig.CampoTipo.Fecha

                                    ListDefinicionCaptura.Add(DefinicionCaptura)
                                    ControlCaptura.LoadDefinition(ListDefinicionCaptura)
                                Case Else
                                    Throw New Exception("Tipo de campo no reconocido: " & CampoRow.fk_Campo_Tipo)

                            End Select

                            ControlCaptura.ShowSecondControls = True
                            ControlCaptura.Tipo = CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                            ControlCaptura.Etiqueta = CampoRow.Nombre_Campo
                            ControlCaptura.CampoLlaveCaptura = CampoLlaveCaptura
                            ControlCaptura.ShowSecondControls = True
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
                        End If
                    End If
                Next

            End If

            Return _CamposLlave
        End Function

        Public Overrides Function GetValueFileData(idCampo As Integer) As String
            Return Nothing
        End Function

        Public Overrides Function GetValueLlaveData() As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow
            Return Nothing
        End Function

        Public Overrides Function Validaciones(ByVal idDocumento As Integer) As List(Of ValidacionCaptura)
            Return _Validaciones
        End Function

#End Region

#Region " Metodos "

        Protected Overrides Sub LoadConfig(ByRef nDBMCore As DBCore.DBCoreDataBaseManager, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short)
            If AnexoImagingRow IsNot Nothing Then
                Me.EsquemaDataTable = nDBMCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
                Me.DocumentoDataTable = nDBMImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(nEntidad, nProyecto, nEsquema, AnexoImagingRow.fk_Documento, False)
                Me.fk_Esquema = nEsquema

                Dim OrdenC = New DBImaging.SchemaConfig.CTA_CampoEnumList()
                OrdenC.Add(DBImaging.SchemaConfig.CTA_CampoEnum.fk_Documento, True)
                OrdenC.Add(DBImaging.SchemaConfig.CTA_CampoEnum.Orden_Campo, True)
                OrdenC.Add(DBImaging.SchemaConfig.CTA_CampoEnum.id_Campo, True)

                Me.CamposDataTable = nDBMImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_Documentofk_Proyectoid_EsquemaUsa_CapturaEs_Campo_IndexacionEliminado_Campo(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, AnexoImagingRow.fk_Documento, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, Nothing, False)

                ' Para los campos trigger se toma la configuración de primera captura
                Me.TriggersDataTable = nDBMImaging.SchemaConfig.TBL_Campo_Trigger.DBGet(DBImaging.EnumEtapaCaptura.Captura, AnexoImagingRow.fk_Documento, Nothing, Nothing, Nothing, Nothing)
                Me.TriggersValidacionesDataTable = nDBMImaging.SchemaConfig.TBL_Campo_Trigger_Validacion.DBGet(DBImaging.EnumEtapaCaptura.Captura, AnexoImagingRow.fk_Documento, Nothing, Nothing, Nothing)

                ' Lista Item
                Me.ListaItemsDataTable = nDBMCore.SchemaConfig.PA_Campo_Lista_Item_getDocumento.DBExecute(AnexoImagingRow.fk_Documento)

                Me.TablaAsociadaDataTable = nDBMImaging.SchemaConfig.CTA_Tabla_Asociada.DBFindByfk_DocumentoEliminado_Campo(AnexoImagingRow.fk_Documento, False)

                ' Leer datos mesa de control
                Dim Orden = New DBImaging.SchemaProcess.CTA_Anexo_Data_MC_OrdenEnumList()
                Orden.Add(DBImaging.SchemaProcess.CTA_Anexo_Data_MC_OrdenEnum.fk_Documento, True)
                Orden.Add(DBImaging.SchemaProcess.CTA_Anexo_Data_MC_OrdenEnum.Orden_Campo, True)
                Orden.Add(DBImaging.SchemaProcess.CTA_Anexo_Data_MC_OrdenEnum.fk_Campo, True)
                Me.MesaControlDataTableAnexo = nDBMImaging.SchemaProcess.CTA_Anexo_Data_MC_Orden.DBFindByfk_Anexo(AnexoImagingRow.id_Anexo, 0, Orden)


                Me.IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
                Me.IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)

                EventManager.FinalizarLoadConfig(IndexerView, FileCoreRow)
            Else
                Me.EsquemaDataTable = nDBMCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
                Me.DocumentoDataTable = nDBMImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(nEntidad, nProyecto, nEsquema, FileCoreRow.fk_Documento, False)
                Me.fk_Esquema = nEsquema

                Dim OrdenC = New DBImaging.SchemaConfig.CTA_CampoEnumList()
                OrdenC.Add(DBImaging.SchemaConfig.CTA_CampoEnum.fk_Documento, True)
                OrdenC.Add(DBImaging.SchemaConfig.CTA_CampoEnum.Orden_Campo, True)
                OrdenC.Add(DBImaging.SchemaConfig.CTA_CampoEnum.id_Campo, True)

                Me.CamposDataTable = nDBMImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_Documentofk_Proyectoid_EsquemaUsa_CapturaEs_Campo_IndexacionEliminado_Campo(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, Nothing, False)

                If Me.CamposDataTable.Rows.Count = 0 Then
                    Me.CamposDataTable = nDBMImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_DocumentoEliminado_Campofk_Proyectoid_EsquemaUsa_Captura_Correccion_Maquina(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, False, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, 0, OrdenC)
                End If

                If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                    Me.CamposLlaveDataTable = nDBMImaging.SchemaConfig.CTA_Campo_Llave.DBFindByfk_Entidadfk_DocumentoEliminado_Campofk_Proyectoid_EsquemaUsa_CapturaUsa_Doble_CapturaEs_Campo_Indexacion(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, False, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, Nothing, True, Nothing)

                    'Busca campos llaves asignados al DocPpal para capturarselos al File1 del Expediente
                    If CamposLlaveDataTable.Count = 0 Then
                        Me.CamposLlaveDataTable = nDBMImaging.SchemaProcess.PA_Busca_Campos_Llave_DocPpal.DBExecute(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, FileCoreRow.fk_Documento, CInt(FileImagingRow.fk_Expediente), FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                    End If
                End If

                ' Para los campos trigger se toma la configuración de primera captura
                Me.TriggersDataTable = nDBMImaging.SchemaConfig.TBL_Campo_Trigger.DBGet(DBImaging.EnumEtapaCaptura.Captura, FileCoreRow.fk_Documento, Nothing, Nothing, Nothing, Nothing)
                Me.TriggersValidacionesDataTable = nDBMImaging.SchemaConfig.TBL_Campo_Trigger_Validacion.DBGet(DBImaging.EnumEtapaCaptura.Captura, FileCoreRow.fk_Documento, Nothing, Nothing, Nothing)

                ' Lista Item
                Me.ListaItemsDataTable = nDBMCore.SchemaConfig.PA_Campo_Lista_Item_getDocumento.DBExecute(FileCoreRow.fk_Documento)

                Me.TablaAsociadaDataTable = nDBMImaging.SchemaConfig.CTA_Tabla_Asociada.DBFindByfk_DocumentoEliminado_Campo(FileCoreRow.fk_Documento, False)

                ' Leer datos mesa de control
                Dim Orden = New DBImaging.SchemaProcess.CTA_File_Data_MC_OrdenEnumList()
                Orden.Add(DBImaging.SchemaProcess.CTA_File_Data_MC_OrdenEnum.fk_Documento, True)
                Orden.Add(DBImaging.SchemaProcess.CTA_File_Data_MC_OrdenEnum.Orden_Campo, True)
                Orden.Add(DBImaging.SchemaProcess.CTA_File_Data_MC_OrdenEnum.fk_Campo, True)
                Me.MesaControlDataTable = nDBMImaging.SchemaProcess.CTA_File_Data_MC_Orden.DBFindByfk_Expedientefk_Folderfk_Filefk_Version(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, 0, Orden)
                Me.MesaControlCamposLlaveDataTable = nDBMImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MC.DBFindByfk_Expediente(FileImagingRow.fk_Expediente)

                ' Anexos del documento
                Me.AnexosDataTable = nDBMCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

                Me.IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
                Me.IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)

                EventManager.FinalizarLoadConfig(IndexerView, FileCoreRow)
            End If
        End Sub

#End Region

    End Class

End Namespace