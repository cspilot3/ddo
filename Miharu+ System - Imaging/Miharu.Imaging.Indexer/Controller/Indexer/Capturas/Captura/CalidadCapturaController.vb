Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports Slyg.Tools

Namespace Controller.Indexer.Capturas.Captura

    Public Class CalidadCapturaController
        Inherits CapturasController

#Region " Declaraciones "

        Protected MesaControlDataTable As New DBImaging.SchemaProcess.CTA_File_Data_MC_OrdenDataTable()
        Protected MesaControlCamposLlaveDataTable As New DBImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MCDataTable()

#End Region

#Region " Propiedades "

        Protected Overrides ReadOnly Property ProcessName As String
            Get
                Return "Calidad"
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
            IndexerView.ShowToolTipData(False)
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

                Dim productividad As New DesktopConfig.ProductividadType
                Dim errores(2) As DesktopConfig.ProductividadType

                errores(0) = New DesktopConfig.ProductividadType()
                errores(1) = New DesktopConfig.ProductividadType()
                errores(2) = New DesktopConfig.ProductividadType()

                productividad.Usuario = Me.IndexerSesion.Usuario.id

                Try

                    Dim ValidarSave As Boolean = True

                    EventManager.ValidarSaveCalidad(_Campos, FileCoreRow.fk_Documento, ValidarSave)

                    If Not ValidarSave Then
                        Return False
                    End If



                    If (Me.IndexerView.RequiereAutorizacion) Then
                        'If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Algunos de los campos capturados no coinciden con ninguna de las anteriores capturas, se requiere autorización de un usuario con Perfil Nacional para continuar el proceso", Me.IndexerSesion.Usuario, Me.IndexerDesktopGlobal.SecurityServiceURL, Me.IndexerDesktopGlobal.ClientIPAddress)) Then
                        If (Not Utilities.ValidarPermiso(Permisos.Imaging.Proceso.Control.Autorizaciones, Program.AccesoDesktopAssembly, "Algunos de los campos capturados no coinciden con ninguna de las anteriores capturas, se requiere autorización para continuar el proceso", Me.IndexerSesion.Usuario, Me.IndexerDesktopGlobal.SecurityServiceUrl, Me.IndexerDesktopGlobal.ClientIpAddress)) Then
                            Throw New Exception("No se permitió el almacenamiento de la data capturada")
                        End If
                    End If

                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Dim fechaInicioProceso = Now

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
                                dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Tercera_Captura, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PcName)
                            End If

                        Catch : End Try
                        '--------------------------------------------------------------------------------------




                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)


                        ' Reportar usuario captura
                        Dim fileType = New DBImaging.SchemaProcess.TBL_FileType()
                        fileType.Usuario_Calidad = Me.IndexerSesion.Usuario.id
                        fileType.Fecha_Calidad = SlygNullable.SysDate
                        dbmImaging.SchemaProcess.TBL_File.DBUpdate(fileType, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                        Dim fileTable = dbmImaging.SchemaProcess.TBL_File.DBGet(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)
                        fileType = fileTable(0).ToTBL_FileType()

                        errores(0).Usuario = fileType.Usuario_Primera_Captura
                        errores(1).Usuario = fileType.Usuario_Segunda_Captura
                        errores(2).Usuario = fileType.Usuario_Tercera_Captura

                        errores(0).Fecha = fileType.Fecha_Primera_Captura
                        errores(1).Fecha = fileType.Fecha_Segunda_Captura
                        errores(2).Fecha = fileType.Fecha_Tercera_Captura

                        ' Actualizar datos capturados                        
                        Dim camposQuery As New List(Of CampoCaptura)
                        For Each Campo In _Campos
                            If Campo.Control.IsVisible Then
                                ' Borrar reporte de errores de este campo
                                dbmImaging.SchemaProcess.PA_Borrar_Productividad_Error.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)

                                If (Campo.Control.Tipo <> DesktopConfig.CampoTipo.TablaAsociada) Then
                                    If Campo.Control.Tipo <> DesktopConfig.CampoTipo.Query Then
                                        Dim campoType = New DBCore.SchemaProcess.TBL_File_DataType()

                                        campoType.Valor_File_Data = Campo.Control.Value
                                        campoType.Conteo_File_Data = Campo.Control.Value.ToString().Length

                                        ' Actualizar mesa de control
                                        Dim mcType = New DBImaging.SchemaProcess.TBL_File_Data_MCType()
                                        mcType.Valor_Calidad = Campo.Control.Value
                                        dbmImaging.SchemaProcess.TBL_File_Data_MC.DBUpdate(mcType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, FileImagingRow.id_Version)

                                        If Campo.Control.IsVisible Then
                                            productividad.Campos += 1
                                            productividad.Caracteres += Campo.Control.Value.ToString().Length

                                            ' Conteo de Errores
                                            If Campo.Control.Value.ToString() <> Campo.Control.ValueOld1.ToString() Then
                                                errores(0).Campos += 1
                                                errores(0).Caracteres += Campo.Control.ValueOld1.ToString().Length
                                                InsertarProductividad_Error_Detallado(dbmImaging, Campo, Nothing, Nothing, errores(0), Campo.Control.ValueOld1, Me.IndexerSesion.Usuario.id, Campo.Control.Value, DesktopConfig.Etapa_Productividad.Primera_Captura)
                                            End If

                                            If Campo.Control.Value.ToString() <> Campo.Control.ValueOld2.ToString() Then
                                                errores(1).Campos += 1
                                                errores(1).Caracteres += Campo.Control.ValueOld2.ToString().Length
                                                InsertarProductividad_Error_Detallado(dbmImaging, Campo, Nothing, Nothing, errores(1), Campo.Control.ValueOld2, Me.IndexerSesion.Usuario.id, Campo.Control.Value, DesktopConfig.Etapa_Productividad.Segunda_Captura)
                                            End If

                                            If Campo.Control.Value.ToString() <> Campo.Control.ValueOld3.ToString() Then
                                                errores(2).Campos += 1
                                                errores(2).Caracteres += Campo.Control.ValueOld3.ToString().Length
                                                InsertarProductividad_Error_Detallado(dbmImaging, Campo, Nothing, Nothing, errores(2), Campo.Control.ValueOld3, Me.IndexerSesion.Usuario.id, Campo.Control.Value, DesktopConfig.Etapa_Productividad.Tercera_Captura)
                                            End If
                                        End If

                                        dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(campoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)
                                    Else
                                        camposQuery.Add(Campo)
                                    End If
                                Else
                                    Dim valueTable As DataTable = CType(Campo.Control.Value, DataTable)
                                    Dim valueOld1Table As DataTable = CType(Campo.Control.ValueOld1, DataTable)
                                    Dim valueOld2Table As DataTable = CType(Campo.Control.ValueOld2, DataTable)
                                    Dim valueOld3Table As DataTable = CType(Campo.Control.ValueOld3, DataTable)

                                    Dim col As Integer = 0
                                    Dim tablaControl = CType(Campo.Control, TableInputControl)

                                    For Each tablaAsociadaRow As DBImaging.SchemaConfig.CTA_Tabla_AsociadaRow In TablaAsociadaDataTable.Select("fk_Campo = " & Campo.id)
                                        Dim fil As Integer = 1

                                        For Each registro As DataRow In valueTable.Rows
                                            If (Campo.Control.IsVisible And Not tablaControl.DefinicionCaptura(col).IsReadOnly) Then
                                                Dim campoType = New DBCore.SchemaProcess.TBL_File_Data_AsociadaType()
                                                Dim tableValue As String

                                                tableValue = registro.Item(col).ToString()

                                                campoType.Valor_File_Data = tableValue
                                                campoType.Conteo_File_Data = tableValue.Length

                                                Dim mcAsociadaType = New DBImaging.SchemaProcess.TBL_File_Data_MC_AsociadaType()
                                                mcAsociadaType.Valor_Tercera_Captura = tableValue
                                                dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBUpdate(mcAsociadaType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version, FileCoreRow.fk_Documento, Campo.id, CShort(tablaAsociadaRow.id_Campo_Tabla), CShort(fil))

                                                productividad.Caracteres += tableValue.Length

                                                Dim valueOld1 As String = ""
                                                If (valueOld1Table.Rows.Count > fil - 1) Then
                                                    valueOld1 = valueOld1Table.Rows(fil - 1).Item(col).ToString()
                                                End If

                                                Dim valueOld2 As String = ""
                                                If (valueOld2Table.Rows.Count > fil - 1) Then
                                                    valueOld2 = valueOld2Table.Rows(fil - 1).Item(col).ToString()
                                                End If

                                                Dim valueOld3 As String = ""
                                                If (valueOld3Table.Rows.Count > fil - 1) Then
                                                    valueOld3 = valueOld3Table.Rows(fil - 1).Item(col).ToString()
                                                End If

                                                'Conteo de Errores
                                                If tableValue <> valueOld1 Then
                                                    productividad.Campos += 1
                                                    errores(0).Campos += 1
                                                    errores(0).Caracteres += valueOld1.Length
                                                    InsertarProductividad_Error_Detallado(dbmImaging, Campo, CShort(tablaAsociadaRow.id_Campo_Tabla), CShort(fil), errores(0), valueOld1, Me.IndexerSesion.Usuario.id, tableValue, DesktopConfig.Etapa_Productividad.Primera_Captura)
                                                End If

                                                If tableValue <> valueOld2 Then
                                                    productividad.Campos += 1
                                                    errores(1).Campos += 1
                                                    errores(1).Caracteres += valueOld2.Length
                                                    InsertarProductividad_Error_Detallado(dbmImaging, Campo, CShort(tablaAsociadaRow.id_Campo_Tabla), CShort(fil), errores(1), valueOld2, Me.IndexerSesion.Usuario.id, tableValue, DesktopConfig.Etapa_Productividad.Segunda_Captura)
                                                End If

                                                If tableValue <> valueOld3 Then
                                                    productividad.Campos += 1
                                                    errores(1).Campos += 1
                                                    errores(1).Caracteres += valueOld2.Length
                                                    InsertarProductividad_Error_Detallado(dbmImaging, Campo, CShort(tablaAsociadaRow.id_Campo_Tabla), CShort(fil), errores(2), valueOld2, Me.IndexerSesion.Usuario.id, tableValue, DesktopConfig.Etapa_Productividad.Tercera_Captura)
                                                End If

                                                dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBUpdate(campoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, CShort(tablaAsociadaRow.id_Campo_Tabla), CShort(fil))
                                            End If

                                            fil += 1
                                        Next

                                        col += 1
                                    Next
                                End If
                            End If
                        Next

                        'Almacena los valores de los campos tipo Query // Se almacenan de ultimo porque puden depender de los valores antes insertados
                        For Each Campo In camposQuery
                            Try
                                Dim campoType = New DBCore.SchemaProcess.TBL_File_DataType()
                                campoType.Conteo_File_Data = 0
                                dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(campoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)

                                Dim parametros As New List(Of Slyg.Data.Schemas.Parameter)
                                Dim par1 As New Slyg.Data.Schemas.Parameter("@Expediente", FileCoreRow.fk_Expediente)
                                Dim par2 As New Slyg.Data.Schemas.Parameter("@Folder", FileCoreRow.fk_Folder)
                                Dim par3 As New Slyg.Data.Schemas.Parameter("@File", FileCoreRow.id_File)
                                parametros.Add(par1) : parametros.Add(par2) : parametros.Add(par3)
                                Dim queryRecuperacion = dbmCore.DataBase.ExecuteStoredProcedureGet("DBO", "PA_CAMPO_QUERY_" & CStr(Me.ExpedienteRow.fk_Entidad) & "_" & CStr(FileCoreRow.fk_Documento) & "_" & CStr(Campo.id), parametros)
                                If queryRecuperacion.Rows.Count > 0 Then campoType.Valor_File_Data = Utilities.DStr(queryRecuperacion.Rows(0)(0).ToString())

                                dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(campoType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id)

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
                                        productividad.CamposLlave += 1
                                        productividad.Caracteres += CampoLlave.Control.Value.ToString().Length

                                         ' Actualizar mesa de control
                                        Dim mcType = New DBImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MCType()
                                        mcType.Valor_Calidad = CampoLlave.Control.Value.ToString()
                                        dbmImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MC.DBUpdate(mcType, FileCoreRow.fk_Expediente, CShort(CampoLlave.Id_Campo))
                                    End If
                                Next

                                dbmCore.SchemaProcess.PA_Guarda_Expediente_Llave_En_Linea.DBExecute(FileCoreRow.fk_Expediente, CShort(_CamposLlave(0).fk_Entidad), CShort(_CamposLlave(0).fk_Proyecto), CShort(_CamposLlave(0).fk_Esquema), Llaves, CampoEmpaque)
                            End If
                        End If

                        'Crea el estado del Folder en Core
                        Dim folderEstadoDataTable = dbmCore.SchemaProcess.TBL_Folder_estado.DBFindByfk_Expedientefk_FolderModulo(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, DesktopConfig.Modulo.Imaging)

                        If folderEstadoDataTable.Count = 0 Then
                            Dim insertEstado As New DBCore.SchemaProcess.TBL_Folder_estadoType()
                            insertEstado.fk_Expediente = FileImagingRow.fk_Expediente
                            insertEstado.fk_Folder = FileImagingRow.fk_Folder
                            insertEstado.Modulo = DesktopConfig.Modulo.Imaging
                            insertEstado.fk_Estado = DBCore.EstadoEnum.Indexado
                            insertEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                            insertEstado.Fecha_Log = SlygNullable.SysDate
                            dbmCore.SchemaProcess.TBL_Folder_estado.DBInsert(insertEstado)
                        End If

                        ' Insertar productividad
                        InsertarProductividad(dbmImaging, productividad, DesktopConfig.Etapa_Productividad.CalidadCaptura, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                        InsertarProductividad_Error(dbmImaging, errores, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)

                        ' Actualizar llaves
                        dbmCore.SchemaProcess.PA_Calcular_Llaves.DBExecute(FileImagingRow.fk_Expediente)

                        ' Aplicar validaciones automáticas
                        dbmImaging.SchemaProcess.PA_Aplicar_Validaciones_Automaticas.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento)

                        Dim nextEstado As DBCore.EstadoEnum

                        ' Preparar los recortes
                        If (Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Recortes AndAlso dbmImaging.SchemaProcess.PA_Preparar_Recortes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)) Then
                            nextEstado = DBCore.EstadoEnum.Recorte
                        Else
                            nextEstado = DBCore.EstadoEnum.Indexado
                        End If

                        ' Actualizar estado
                        Dim updateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                        updateEstado.Fecha_Log = SlygNullable.SysDate
                        updateEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                        updateEstado.fk_Estado = nextEstado
                        dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(updateEstado, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, DesktopConfig.Modulo.Imaging)

                        '---------------------------------------------------------------------------
                        ' Actualizar Dashboard
                        '---------------------------------------------------------------------------
                        dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        '---------------------------------------------------------------------------

                        dbmImaging.Transaction_Commit()

                        Try
                            dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                            Dim carguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(_idCargue, _idCarguePaquete)
                            If (carguePaqueteFileDataTable.Count > 0) Then
                                Dim updatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                                updatePaquete.fk_Estado = carguePaqueteFileDataTable(0).Estado_File
                                dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(updatePaquete, _idCargue, _idCarguePaquete)

                                Dim cargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(_idCargue)
                                If (cargueFileDataTable.Count > 0) Then
                                    Dim updateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                                    updateCargue.fk_Estado = cargueFileDataTable(0).Estado_File
                                    dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(updateCargue, _idCargue)
                                End If
                            End If

                            dbmImaging.Transaction_Commit()
                        Catch ex As Exception
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                            Throw
                        End Try


                    Catch ex As Exception
                        ' If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()

                        DesktopMessageBoxControl.DesktopMessageShow("Publicar", ex)

                        Return False
                    Finally
                        ' If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try

                    EventManager.FinalizarCalidad(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                    '-------------------------------------------------------------------------------------------------------------
                    ' LOGGING - PERFORMANCE
                    '-------------------------------------------------------------------------------------------------------------
                    Dim fechaFinProceso = Now
                    Dim traceMessage As String = "[Calidad][Publicar] - Inicio: " & fechaInicioProceso.ToString() & " Fin: " & fechaFinProceso.ToString()
                    DesktopTrace.Trace(traceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, Program.AssemblyTitle)
                    '-------------------------------------------------------------------------------------------------------------

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Publicar", ex)

                    Return False
                End Try

                Return True
            End If

            Return False
        End Function

        Public Overrides Function Campos(idDocumento As Integer) As List(Of CampoCaptura)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Me._Campos.Clear()

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                For Each RowData In MesaControlDataTable
                    If (Not RowData.IsValor_Primera_CapturaNull And Not RowData.IsValor_Segunda_CapturaNull And Not RowData.IsValor_Tercera_CapturaNull) _
                        AndAlso (RowData.Valor_Primera_Captura.ToString() <> RowData.Valor_Segunda_Captura.ToString() _
                                 And RowData.Valor_Primera_Captura.ToString() <> RowData.Valor_Tercera_Captura.ToString() _
                                 And RowData.Valor_Segunda_Captura.ToString() <> RowData.Valor_Tercera_Captura.ToString()) Then

                        Dim campoRow As DBImaging.SchemaConfig.CTA_CampoRow = CType(CamposDataTable.Select("fk_Documento = " & idDocumento & " AND id_Campo = " & RowData.fk_Campo)(0), DBImaging.SchemaConfig.CTA_CampoRow)
                        Dim controlCaptura As View.IInputControl
                        Dim campoCaptura As New CampoCaptura()
                        Dim listDefinicionCaptura As New List(Of DefinicionCaptura)

                        ' Control
                        Select Case CType(campoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                            Case DesktopConfig.CampoTipo.Texto
                                Dim definicionCaptura = New DefinicionCaptura()

                                If RowData.Valor_Primera_Captura.ToString().Contains(" ") Then
                                    RowData.Valor_Primera_Captura = RowData.Valor_Primera_Captura.ToString().Replace(" ", "-")
                                End If

                                If RowData.Valor_Segunda_Captura.ToString().Contains(" ") Then
                                    RowData.Valor_Segunda_Captura = RowData.Valor_Segunda_Captura.ToString().Replace(" ", "-")
                                End If

                                If RowData.Valor_Tercera_Captura.ToString().Contains(" ") Then
                                    RowData.Valor_Tercera_Captura = RowData.Valor_Tercera_Captura.ToString().Replace(" ", "-")
                                End If

                                controlCaptura = New TextInputControl()
                                controlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                controlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura
                                controlCaptura.ValueOld3 = RowData.Valor_Tercera_Captura

                                definicionCaptura.Type = DesktopConfig.CampoTipo.Texto
                                definicionCaptura.Mascara = campoRow.Mascara
                                definicionCaptura.FormatoFecha = campoRow.Formato
                                definicionCaptura.Es_Obligatorio_Campo = campoRow.Es_Obligatorio_Campo
                                definicionCaptura.MaximumLength = campoRow.Length_Campo
                                definicionCaptura.MinimumLength = campoRow.Length_Min_Campo
                                definicionCaptura.Caption = campoRow.Nombre_Campo

                                listDefinicionCaptura.Add(definicionCaptura)
                                controlCaptura.LoadDefinition(listDefinicionCaptura)

                                If (campoRow.Valor_por_Defecto <> "") Then
                                    controlCaptura.Value = campoRow.Valor_por_Defecto
                                End If

                            Case DesktopConfig.CampoTipo.Numerico
                                Dim definicionCaptura = New DefinicionCaptura()

                                controlCaptura = New TextInputNumericControl()
                                controlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                controlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura
                                controlCaptura.ValueOld3 = RowData.Valor_Tercera_Captura

                                definicionCaptura.Type = DesktopConfig.CampoTipo.Numerico
                                definicionCaptura.Es_Obligatorio_Campo = campoRow.Es_Obligatorio_Campo
                                definicionCaptura.MaximumLength = campoRow.Length_Campo
                                definicionCaptura.MinimumLength = campoRow.Length_Min_Campo
                                definicionCaptura.Usa_Decimales = campoRow.Usa_Decimales
                                definicionCaptura.Caption = campoRow.Nombre_Campo

                                If campoRow.Usa_Decimales Then
                                    definicionCaptura.Caracter_Decimal = CChar(campoRow.Caracter_Decimal)
                                    definicionCaptura.Cantidad_Decimales = campoRow.Cantidad_Decimales
                                End If

                                listDefinicionCaptura.Add(definicionCaptura)
                                controlCaptura.LoadDefinition(listDefinicionCaptura)

                                If (campoRow.Valor_por_Defecto <> "") Then
                                    controlCaptura.Value = campoRow.Valor_por_Defecto
                                End If

                            Case DesktopConfig.CampoTipo.Fecha
                                Dim definicionCaptura = New DefinicionCaptura()

                                controlCaptura = New TextInputDateTimeControl()
                                controlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                controlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura
                                controlCaptura.ValueOld3 = RowData.Valor_Tercera_Captura

                                definicionCaptura.Type = DesktopConfig.CampoTipo.Fecha
                                definicionCaptura.Mascara = campoRow.Mascara
                                definicionCaptura.FormatoFecha = campoRow.Formato
                                definicionCaptura.Es_Obligatorio_Campo = campoRow.Es_Obligatorio_Campo
                                definicionCaptura.Caption = campoRow.Nombre_Campo

                                listDefinicionCaptura.Add(definicionCaptura)
                                controlCaptura.LoadDefinition(listDefinicionCaptura)

                                If (campoRow.Valor_por_Defecto <> "") Then
                                    controlCaptura.Value = campoRow.Valor_por_Defecto
                                End If

                            Case DesktopConfig.CampoTipo.Lista
                                Dim definicionCaptura = New DefinicionCaptura()
                                controlCaptura = New ListInputControl()

                                controlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                controlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura
                                controlCaptura.ValueOld3 = RowData.Valor_Tercera_Captura

                                definicionCaptura.Type = DesktopConfig.CampoTipo.Lista
                                definicionCaptura.Es_Obligatorio_Campo = campoRow.Es_Obligatorio_Campo
                                definicionCaptura.Caption = campoRow.Nombre_Campo

                                ' Datos de la lista
                                If (Not campoRow.Isfk_Campo_ListaNull()) Then
                                    Dim dtvLista As New DataView(ListaItemsDataTable)

                                    dtvLista.RowFilter = "fk_Campo_Lista = " & campoRow.fk_Campo_Lista

                                    Dim listControl = CType(controlCaptura, ListInputControl)
                                    listControl.ValueDesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                                    listControl.ValueDesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                    listControl.ValueDesktopComboBox.DataSource = dtvLista
                                    listControl.ValueDesktopComboBox.SelectedValue = -1
                                    listControl.ValueDesktopComboBox.Refresh()
                                End If

                                listDefinicionCaptura.Add(definicionCaptura)
                                controlCaptura.LoadDefinition(listDefinicionCaptura)

                            Case DesktopConfig.CampoTipo.SiNo
                                Dim definicionCaptura = New DefinicionCaptura()

                                controlCaptura = New TextInputControl()
                                controlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                controlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura
                                controlCaptura.ValueOld3 = RowData.Valor_Tercera_Captura

                                definicionCaptura.Type = DesktopConfig.CampoTipo.SiNo
                                definicionCaptura.Es_Obligatorio_Campo = campoRow.Es_Obligatorio_Campo
                                definicionCaptura.Caption = campoRow.Nombre_Campo

                                Dim listControl = CType(controlCaptura, ListInputControl)
                                listControl.ValueDesktopComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                                listControl.ValueDesktopComboBox.Items.Add("Si")
                                listControl.ValueDesktopComboBox.Items.Add("No")
                                listControl.ValueDesktopComboBox.SelectedIndex = 0

                                listDefinicionCaptura.Add(definicionCaptura)
                                controlCaptura.LoadDefinition(listDefinicionCaptura)

                            Case DesktopConfig.CampoTipo.Query
                                Dim definicionCaptura = New DefinicionCaptura()
                                controlCaptura = New TextInputControl()

                                definicionCaptura.Type = DesktopConfig.CampoTipo.Texto
                                definicionCaptura.Es_Obligatorio_Campo = campoRow.Es_Obligatorio_Campo
                                definicionCaptura.Caption = campoRow.Nombre_Campo

                                Dim textControl = CType(controlCaptura, TextInputControl)
                                textControl.ValueDesktopTextBox.MaxLength = campoRow.Length_Campo
                                textControl.ValueDesktopTextBox.Text = "Automatico"
                                textControl.ValueDesktopTextBox.Enabled = False

                                listDefinicionCaptura.Add(definicionCaptura)
                                controlCaptura.LoadDefinition(listDefinicionCaptura)

                            Case DesktopConfig.CampoTipo.TablaAsociada
                                Dim valueTable = New DataTable()
                                Dim value1Table = New DataTable()
                                Dim value2Table = New DataTable()
                                Dim value3Table = New DataTable()
                                Dim columna As DataColumn

                                controlCaptura = New TableInputControl()

                                For Each tablaAsociadaRow As DBImaging.SchemaConfig.CTA_Tabla_AsociadaRow In TablaAsociadaDataTable.Select("fk_Campo = " & campoRow.id_Campo)
                                    Dim definicionCaptura = New DefinicionCaptura()

                                    definicionCaptura.Caption = tablaAsociadaRow.Nombre_Campo
                                    definicionCaptura.Es_Obligatorio_Campo = tablaAsociadaRow.Es_Obligatorio_Campo
                                    definicionCaptura.FormatoFecha = tablaAsociadaRow.Formato

                                    columna = New DataColumn()
                                    columna.ColumnName = "col_" & tablaAsociadaRow.id_Campo_Tabla
                                    columna.Caption = tablaAsociadaRow.Nombre_Campo

                                    Select Case CType(tablaAsociadaRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                                        Case DesktopConfig.CampoTipo.Texto
                                            columna.DataType = GetType(String)
                                            columna.MaxLength = tablaAsociadaRow.Length_Campo

                                            definicionCaptura.Type = DesktopConfig.CampoTipo.Texto
                                            definicionCaptura.MaximumLength = tablaAsociadaRow.Length_Campo
                                            definicionCaptura.Mascara = tablaAsociadaRow.Mascara

                                        Case DesktopConfig.CampoTipo.Numerico
                                            columna.DataType = GetType(String)

                                            definicionCaptura.Type = DesktopConfig.CampoTipo.Numerico
                                            definicionCaptura.MaximumLength = tablaAsociadaRow.Length_Campo
                                            definicionCaptura.MinimumLength = 0
                                            definicionCaptura.Usa_Decimales = tablaAsociadaRow.Usa_Decimales

                                            If tablaAsociadaRow.Usa_Decimales Then
                                                definicionCaptura.Caracter_Decimal = CChar(tablaAsociadaRow.Caracter_Decimal)
                                                definicionCaptura.Cantidad_Decimales = tablaAsociadaRow.Cantidad_Decimales
                                            End If

                                        Case DesktopConfig.CampoTipo.Fecha
                                            columna.DataType = GetType(DateTime)

                                            definicionCaptura.Type = DesktopConfig.CampoTipo.Fecha
                                            definicionCaptura.Mascara = tablaAsociadaRow.Mascara
                                            definicionCaptura.FormatoFecha = tablaAsociadaRow.Formato

                                        Case DesktopConfig.CampoTipo.Lista
                                            columna.DataType = GetType(String)

                                            definicionCaptura.Type = DesktopConfig.CampoTipo.Lista

                                            ' Datos de la lista
                                            If (Not tablaAsociadaRow.Isfk_Campo_ListaNull()) Then
                                                definicionCaptura.Items = New DataView(ListaItemsDataTable)
                                                definicionCaptura.Items.RowFilter = "fk_Campo_Lista = " & tablaAsociadaRow.fk_Campo_Lista
                                                definicionCaptura.ValueMember = "Valor_Campo_Lista_Item"
                                                definicionCaptura.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                            End If

                                        Case DesktopConfig.CampoTipo.SiNo
                                            definicionCaptura.Type = DesktopConfig.CampoTipo.SiNo

                                        Case Else
                                            Throw New Exception("Tipo de campo no valido para una tabla asociada: " & tablaAsociadaRow.fk_Campo_Tipo)

                                    End Select

                                    listDefinicionCaptura.Add(definicionCaptura)

                                    valueTable.Columns.Add(columna)
                                    value1Table.Columns.Add(ClonarColumna(columna))
                                    value2Table.Columns.Add(ClonarColumna(columna))
                                    value3Table.Columns.Add(ClonarColumna(columna))
                                Next

                                Dim fileDataAsociadaDataTable = dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBGet(RowData.fk_Expediente, RowData.fk_Folder, RowData.fk_File, FileImagingRow.id_Version, RowData.fk_Documento, campoRow.id_Campo, Nothing, Nothing)
                                Dim fileDataAsociadaView = New DataView(fileDataAsociadaDataTable)
                                Dim registrosAsociada = fileDataAsociadaView.ToTable(True, "fk_Expediente", "fk_Folder", "fk_File", "fk_Version", "fk_Documento", "fk_Campo", "fk_File_Data_Asociada")

                                For Each itemRow As DataRow In registrosAsociada.Rows
                                    Dim newRow1 = value1Table.NewRow()
                                    Dim newRow2 = value2Table.NewRow()
                                    Dim newRow3 = value3Table.NewRow()

                                    fileDataAsociadaView.RowFilter = "fk_File_Data_Asociada = " & itemRow.Item("fk_File_Data_Asociada").ToString()
                                    fileDataAsociadaView.Sort = "fk_Campo_Tabla"

                                    For col As Integer = 0 To fileDataAsociadaView.Count - 1
                                        If (col < valueTable.Columns.Count) Then
                                            newRow1.Item(col) = fileDataAsociadaView.Item(col).Item("Valor_Primera_Captura")
                                            newRow2.Item(col) = fileDataAsociadaView.Item(col).Item("Valor_Segunda_Captura")
                                            newRow3.Item(col) = fileDataAsociadaView.Item(col).Item("Valor_Tercera_Captura")
                                        End If
                                    Next

                                    value1Table.Rows.Add(newRow1)
                                    value2Table.Rows.Add(newRow2)
                                    value3Table.Rows.Add(newRow3)
                                Next

                                controlCaptura.Value = valueTable
                                controlCaptura.ValueOld1 = value1Table
                                controlCaptura.ValueOld2 = value2Table
                                controlCaptura.ValueOld3 = value3Table

                                controlCaptura.LoadDefinition(listDefinicionCaptura)

                            Case Else
                                Throw New Exception("Tipo de campo no válido para segunda captura: " & campoRow.fk_Campo_Tipo)

                        End Select

                        controlCaptura.IsCalidad = True

                        controlCaptura.UsaTrigger = campoRow.Usa_Trigger
                        controlCaptura.TriggerValues.Clear()
                        If (campoRow.Usa_Trigger) Then
                            For Each triggerRow As DBImaging.SchemaConfig.TBL_Campo_TriggerRow In TriggersDataTable.Select("fk_Campo_Trigger = " & campoRow.id_Campo)
                                Dim newKeyValue = New View.KeyValueItem(triggerRow.Valor, triggerRow.fk_Campo_Ocultar, triggerRow.fk_Campo_Tabla_Ocultar)
                                controlCaptura.TriggerValues.Add(newKeyValue)
                            Next
                        End If

                        controlCaptura.ÏsOCRCapture = Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_OCR_Captura  ' data obtenida TBL_Proyecto
                        controlCaptura.ShowSecondControls = True
                        controlCaptura.Tipo = CType(campoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                        controlCaptura.Etiqueta = campoRow.Nombre_Campo
                        controlCaptura.CampoCaptura = campoCaptura
                        controlCaptura.ShowSecondControls = False
                        controlCaptura.ShowPrimaryControls = True
                        controlCaptura.ShowValidacionListasControls = False

                        campoCaptura.id = campoRow.id_Campo
                        campoCaptura.Control = controlCaptura
                        campoCaptura.Marca_Height_Campo = campoRow.Marca_Height_Campo
                        campoCaptura.Marca_Width_Campo = campoRow.Marca_Width_Campo
                        campoCaptura.Marca_X_Campo = campoRow.Marca_X_Campo
                        campoCaptura.Marca_Y_Campo = campoRow.Marca_Y_Campo
                        campoCaptura.Usa_Marca = campoRow.Usa_Marca

                        _Campos.Add(campoCaptura)
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

            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                For Each RowData In MesaControlCamposLlaveDataTable

                    If (Not RowData.IsValor_Primera_CapturaNull And Not RowData.IsValor_Segunda_CapturaNull And Not RowData.IsValor_Tercera_CapturaNull) _
                        AndAlso (RowData.Valor_Primera_Captura.ToString() <> RowData.Valor_Segunda_Captura.ToString() _
                                 And RowData.Valor_Primera_Captura.ToString() <> RowData.Valor_Tercera_Captura.ToString() _
                                 And RowData.Valor_Segunda_Captura.ToString() <> RowData.Valor_Tercera_Captura.ToString()) Then

                        Dim CampoRow As DBImaging.SchemaConfig.CTA_Campo_LlaveRow = CType(CamposLlaveDataTable.Select("fk_Documento = " & idDocumento & " AND id_Campo = " & RowData.fk_Campo_LLave & " AND Usa_Doble_Captura = 1")(0), DBImaging.SchemaConfig.CTA_Campo_LlaveRow)
                        Dim ControlCaptura As View.IInputControl
                        Dim CampoLlaveCaptura As New CampoLlaveCaptura()
                        Dim ListDefinicionCaptura As New List(Of DefinicionCaptura)

                        ' Control
                        Select Case CType(CampoRow.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                            Case DesktopConfig.CampoTipo.Texto
                                Dim definicionCaptura = New DefinicionCaptura()

                                If RowData.Valor_Primera_Captura.ToString().Contains(" ") Then
                                    RowData.Valor_Primera_Captura = RowData.Valor_Primera_Captura.ToString().Replace(" ", "-")
                                End If

                                If RowData.Valor_Segunda_Captura.ToString().Contains(" ") Then
                                    RowData.Valor_Segunda_Captura = RowData.Valor_Segunda_Captura.ToString().Replace(" ", "-")
                                End If

                                If RowData.Valor_Tercera_Captura.ToString().Contains(" ") Then
                                    RowData.Valor_Tercera_Captura = RowData.Valor_Tercera_Captura.ToString().Replace(" ", "-")
                                End If

                                ControlCaptura = New TextInputControl()
                                ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura
                                ControlCaptura.ValueOld3 = RowData.Valor_Tercera_Captura
                                definicionCaptura.MaximumLength = CampoRow.Length_Campo
                                definicionCaptura.MinimumLength = CampoRow.Length_Min_Campo

                                ListDefinicionCaptura.Add(definicionCaptura)
                                ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                            Case DesktopConfig.CampoTipo.Numerico
                                Dim definicionCaptura = New DefinicionCaptura()

                                ControlCaptura = New TextInputNumericControl()
                                ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura
                                ControlCaptura.ValueOld3 = RowData.Valor_Tercera_Captura

                                definicionCaptura.Type = DesktopConfig.CampoTipo.Numerico
                                definicionCaptura.MaximumLength = CampoRow.Length_Campo
                                definicionCaptura.MinimumLength = CampoRow.Length_Min_Campo

                                ListDefinicionCaptura.Add(definicionCaptura)
                                ControlCaptura.LoadDefinition(ListDefinicionCaptura)

                            Case DesktopConfig.CampoTipo.Fecha
                                Dim definicionCaptura = New DefinicionCaptura()

                                ControlCaptura = New TextInputDateTimeControl()
                                ControlCaptura.ValueOld1 = RowData.Valor_Primera_Captura
                                ControlCaptura.ValueOld2 = RowData.Valor_Segunda_Captura
                                ControlCaptura.ValueOld3 = RowData.Valor_Tercera_Captura

                                definicionCaptura.Type = DesktopConfig.CampoTipo.Fecha

                                ListDefinicionCaptura.Add(definicionCaptura)
                                ControlCaptura.LoadDefinition(ListDefinicionCaptura)
                            Case Else
                                Throw New Exception("Tipo de campo no válido para segunda captura: " & CampoRow.fk_Campo_Tipo)

                        End Select

                        ControlCaptura.IsCalidad = True

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
                Next

            End If

            Return _CamposLlave
        End Function

        Public Overrides Function GetValueFileData(idCampo As Integer) As String
            Return ""
        End Function

        Public Overrides Function GetValueLlaveData() As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow
            Return Nothing
        End Function

        Public Overrides Function Validaciones(ByVal idDocumento As Integer) As List(Of ValidacionCaptura)
            Return _Validaciones
        End Function

        Public Overrides Function ValidacionListas() As Boolean
            Dim _ValidacionListas As Boolean

            _ValidacionListas = False

            Return _ValidacionListas
        End Function

        'Public Overrides Function DocumentosCaptura(ByVal nEsquema As Short) As DBImaging.SchemaProcess.CTA_Documentos_ValidacionesDataTable
        '    Return New DBImaging.SchemaProcess.CTA_Documentos_ValidacionesDataTable()
        'End Function

        'Public Overrides Function ShowDocumentosCaptura() As DialogResult
        '    Return DialogResult.OK
        'End Function

#End Region

#Region " Metodos "

        Protected Overrides Sub LoadConfig(ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short)
            Me.EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
            Me.DocumentoDataTable = dbmImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(nEntidad, nProyecto, nEsquema, FileCoreRow.fk_Documento, False)

            Me.CamposDataTable = dbmImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_Documentofk_Proyectoid_EsquemaUsa_CapturaEs_Campo_IndexacionEliminado_Campo(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, Nothing, False)

            If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campo_Llave Then
                Me.CamposLlaveDataTable = dbmImaging.SchemaConfig.CTA_Campo_Llave.DBFindByfk_Entidadfk_DocumentoEliminado_Campofk_Proyectoid_EsquemaUsa_CapturaUsa_Doble_CapturaEs_Campo_Indexacion(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, False, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, Nothing, True, Nothing)
            End If

            Me.TriggersDataTable = dbmImaging.SchemaConfig.TBL_Campo_Trigger.DBGet(DBImaging.EnumEtapaCaptura.Captura, FileCoreRow.fk_Documento, Nothing, Nothing, Nothing, Nothing)

            ' Lista Item
            Me.ListaItemsDataTable = dbmCore.SchemaConfig.PA_Campo_Lista_Item_getDocumento.DBExecute(FileCoreRow.fk_Documento)

            Me.TablaAsociadaDataTable = dbmImaging.SchemaConfig.CTA_Tabla_Asociada.DBFindByfk_DocumentoEliminado_Campo(FileCoreRow.fk_Documento, False)

            ' Leer datos mesa de control
            Dim orden = New DBImaging.SchemaProcess.CTA_File_Data_MC_OrdenEnumList()
            orden.Add(DBImaging.SchemaProcess.CTA_File_Data_MC_OrdenEnum.fk_Documento, True)
            orden.Add(DBImaging.SchemaProcess.CTA_File_Data_MC_OrdenEnum.Orden_Campo, True)
            orden.Add(DBImaging.SchemaProcess.CTA_File_Data_MC_OrdenEnum.fk_Campo, True)
            Me.MesaControlDataTable = dbmImaging.SchemaProcess.CTA_File_Data_MC_Orden.DBFindByfk_Expedientefk_Folderfk_Filefk_Version(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, 0, orden)
            Me.MesaControlCamposLlaveDataTable = dbmImaging.SchemaProcess.TBL_Expediente_Llave_Linea_MC.DBFindByfk_Expediente(FileImagingRow.fk_Expediente)

            ' Anexos del documento
            Me.AnexosDataTable = dbmCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

            Me.IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
            Me.IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)
        End Sub

#End Region

    End Class

End Namespace