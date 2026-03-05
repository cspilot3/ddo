Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Indexer.View.Indexacion
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports System.Windows.Forms
Imports Slyg.Tools

Namespace Controller.Indexer.Capturas.Captura

    Public Class CorreccionCapturaMaquinaController
        Inherits CapturasController

#Region " Declaraciones "

        Protected MesaControlDataTable As New DBImaging.SchemaProcess.TBL_File_Data_MCDataTable()

#End Region

#Region " Propiedades "

        Protected Overrides ReadOnly Property ProcessName As String
            Get
                Return "Corrección Captura Máquina"
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
            IndexerView.ShowCamposLlavePanel(False)
            IndexerView.ShowValidationsPanel(False)
            IndexerView.ShowValidationsListasPanel(False)
            IndexerView.ShowToolTipData(True)
            IndexerView.ShowAutoIndexar(False)

            IndexerView.Esquema_Enabled = False
            IndexerView.TipoDocumental_Enabled = False
        End Sub

        Public Overrides Function AddFolio() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function Campos(idDocumento As Integer) As System.Collections.Generic.List(Of View.Indexacion.CampoCaptura)
            Me._Campos.Clear()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me.IndexerDesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Me.IndexerSesion.Usuario.id)

                Dim CamposMostrar = dbmImaging.SchemaConfig.PA_Campo_Por_Expediente_File_Data.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, idDocumento)

                For Each CampoRow As DBImaging.SchemaConfig.CTA_CampoRow In CamposMostrar
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

                                Dim ListControl = CType(ControlCaptura, ListInputControl)
                                ListControl.ValueDesktopComboBox.ValueMember = "Valor_Campo_Lista_Item"
                                ListControl.ValueDesktopComboBox.DisplayMember = "Etiqueta_Campo_Lista_Item"
                                ListControl.ValueDesktopComboBox.DataSource = dtvLista
                                ListControl.ValueDesktopComboBox.Refresh()
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
            Catch ex As Exception
                Throw
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
           
            Return _Campos
        End Function

        Public Overrides Function DeleteFolio() As Boolean
            Throw New NotImplementedException()
        End Function

        Public Overrides Function GetValueFileData(idCampo As Integer) As String

            Dim ValueFileData As String

            ValueFileData = ""
            
            Return ValueFileData
        End Function

        Public Overrides Function GetValueLlaveData() As DBCore.SchemaProcess.TBL_Expediente_Llave_LineaRow
            Return Nothing
        End Function

        Protected Overrides Sub LoadConfig(ByRef dbmCore As DBCore.DBCoreDataBaseManager, ByRef dbmImaging As DBImaging.DBImagingDataBaseManager, nEntidad As Short, nProyecto As Short, nEsquema As Short)
            Me.EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(nEntidad, nProyecto, nEsquema)
            Me.DocumentoDataTable = dbmImaging.SchemaConfig.CTA_Documento.DBFindByfk_Entidadfk_Proyectofk_Esquemaid_DocumentoEliminado(nEntidad, nProyecto, nEsquema, FileCoreRow.fk_Documento, False)

            Dim Orden = New DBImaging.SchemaConfig.CTA_CampoEnumList()
            Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.fk_Documento, True)
            Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.Orden_Campo, True)
            Orden.Add(DBImaging.SchemaConfig.CTA_CampoEnum.id_Campo, True)
            Me.CamposDataTable = dbmImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_DocumentoEliminado_Campofk_Proyectoid_EsquemaUsa_Captura_Correccion_Maquina(Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Entidad, FileCoreRow.fk_Documento, False, Me.IndexerImagingGlobal.ProyectoImagingRow.fk_Proyecto, nEsquema, True, 0, Orden)

            Me.TriggersDataTable = dbmImaging.SchemaConfig.TBL_Campo_Trigger.DBGet(DBImaging.EnumEtapaCaptura.Captura, FileCoreRow.fk_Documento, Nothing, Nothing, Nothing, Nothing)

            ' Lista Item
            Me.ListaItemsDataTable = dbmCore.SchemaConfig.PA_Campo_Lista_Item_getDocumento.DBExecute(FileCoreRow.fk_Documento)

            Dim Orden_Validacion = New DBImaging.SchemaConfig.CTA_ValidacionEnumList()
            Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.fk_Documento, True)
            Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.Orden_Validacion, True)
            Orden_Validacion.Add(DBImaging.SchemaConfig.CTA_ValidacionEnum.id_Validacion, True)
            Me.ValidacionesDataTable = dbmImaging.SchemaConfig.CTA_Validacion.DBFindByfk_Documentofk_Etapa_Captura(FileCoreRow.fk_Documento, DBImaging.EnumEtapaCaptura.Precaptura, 0, Orden_Validacion)

            Me.TablaAsociadaDataTable = dbmImaging.SchemaConfig.CTA_Tabla_Asociada.DBFindByfk_DocumentoEliminado_Campo(FileCoreRow.fk_Documento, False)

            ' Anexos del documento
            Me.AnexosDataTable = dbmCore.SchemaImaging.CTA_Documentos.DBFindByfk_Expedientefk_Folder(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder)

            'Llena los campos lista para la seleccion de motivos
            Me.MotivosDataTable = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBGet(nEntidad, Nothing, Nothing)

            Me.IndexerView.Esquema_DataSource = New DataView(EsquemaDataTable)
            Me.IndexerView.TipoDocumental_DataSource = New DataView(DocumentoDataTable)
        End Sub

        Public Overrides Function Save() As Boolean
            Dim Productividad As New DesktopConfig.ProductividadType

            Productividad.Usuario = Me.IndexerSesion.Usuario.id


            If (Validar()) Then
                Try
                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Dim FechaInicioProceso = Now

                    Try
                        Dim ValidarSave As Boolean = True

                        EventManager.ValidarSavePrimeraCaptura(_Campos, FileCoreRow.fk_Documento, ValidarSave)

                        If Not ValidarSave Then
                            Return False
                        End If

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
                                dbmImaging.SchemaAudit.PA_Inserta_Seguimiento_Imagenes.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, DBCore.EstadoEnum.Correccion_Maquina, Me.IndexerSesion.Usuario.id, Me.IndexerDesktopGlobal.PcName)
                            End If

                        Catch : End Try

                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                        ' Actualizar estados
                        Dim NextEstado = dbmImaging.SchemaProcess.PA_Next_Estado.DBExecute(CurrentDocumentFile.TipoDocumento, DBCore.EstadoEnum.Correccion_Maquina)

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
                        FileType.Usuario_Correccion_Maquina = Me.IndexerSesion.Usuario.id
                        FileType.Fecha_Correccion_Maquina = SlygNullable.SysDate
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


                                        'Insertar registro de data capturada
                                        Dim File_Data_MCDataTable = dbmImaging.SchemaProcess.TBL_File_Data_MC.DBFindByfk_Expedientefk_Folderfk_Filefk_Versionfk_Documentofk_Campo(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version, FileCoreRow.fk_Documento, Campo.id)

                                        Dim FileDataMCType As New DBImaging.SchemaProcess.TBL_File_Data_MCType()
                                        FileDataMCType.Valor_Correccion_Captura_Maquina = Campo.Control.Value

                                        If File_Data_MCDataTable.Count = 0 Then
                                            FileDataMCType.fk_Expediente = FileCoreRow.fk_Expediente
                                            FileDataMCType.fk_Folder = FileCoreRow.fk_Folder
                                            FileDataMCType.fk_File = FileCoreRow.id_File
                                            FileDataMCType.fk_Version = FileImagingRow.id_Version
                                            FileDataMCType.fk_Documento = FileCoreRow.fk_Documento
                                            FileDataMCType.fk_Campo = Campo.id

                                            dbmImaging.SchemaProcess.TBL_File_Data_MC.DBInsert(FileDataMCType)
                                        Else
                                            dbmImaging.SchemaProcess.TBL_File_Data_MC.DBUpdate(FileDataMCType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento, Campo.id, FileImagingRow.id_Version)
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
                        InsertarProductividad(dbmImaging, Productividad, DesktopConfig.Etapa_Productividad.Primera_Captura, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)

                        ' Aplicar validaciones automáticas
                        dbmImaging.SchemaProcess.PA_Aplicar_Validaciones_Automaticas.DBExecute(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileCoreRow.fk_Documento)

                        ' Actualizar estado
                        Dim UpdateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                        UpdateEstado.Fecha_Log = SlygNullable.SysDate
                        UpdateEstado.fk_Usuario = Me.IndexerSesion.Usuario.id
                        UpdateEstado.fk_Estado = 34 'NextEstado
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
                            CapDashboardType.fk_Estado = 34 'NextEstado
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(CapDashboardType, FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File)
                        End If
                        '---------------------------------------------------------------------------

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

                        DesktopMessageBoxControl.DesktopMessageShow("Publicar_Correcion_Maquina", ex)

                        Return False
                    Finally
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    End Try

                    EventManager.FinalizarPrimeraCaptura(FileCoreRow.fk_Expediente, FileCoreRow.fk_Folder, FileCoreRow.id_File, FileImagingRow.id_Version)

                    '-------------------------------------------------------------------------------------------------------------
                    ' LOGGING - PERFORMANCE
                    '-------------------------------------------------------------------------------------------------------------
                    Dim FechaFinProceso = Now
                    Dim TraceMessage As String = "[Corrección Captura Máquina][Publicar] - Inicio: " & FechaInicioProceso.ToString() & " Fin: " & FechaFinProceso.ToString()
                    DesktopTrace.Trace(TraceMessage, DesktopTrace.CategoryEnum.Performance, 1, 0, TraceEventType.Information, Program.AssemblyTitle)
                    '-------------------------------------------------------------------------------------------------------------

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Publicar_Correcion_Maquina", ex)

                    Return False
                End Try

                Return True
            End If

            Return False
        End Function

        Public Overrides ReadOnly Property ShowSecondControls As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Function CamposLlave(ByVal idDocumento As Integer) As List(Of CampoLlaveCaptura)
            Me._CamposLlave.Clear()
            Return _CamposLlave
        End Function

        Public Overrides Function Validaciones(idDocumento As Integer) As System.Collections.Generic.List(Of View.Indexacion.ValidacionCaptura)
            Return _Validaciones
        End Function

        Public Overrides Function ValidacionListas() As Boolean
            Dim _ValidacionListas As Boolean

            _ValidacionListas = False

            Return _ValidacionListas
        End Function

#End Region
       
    End Class

End Namespace
