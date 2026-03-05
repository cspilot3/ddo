Imports System
Imports System.IO
Imports System.Threading
Imports Slyg.Tools
Imports Miharu.FileProvider.Library
Imports Miharu.Security.Library.WebService
Imports Miharu.Desktop.Library.Config

Namespace Servicio

    Public Class MigracionService

#Region " Declaraciones "

        Private _detener As Boolean
        Private Const fk_Entidad_Procesamiento As Short = 11 'P&C
        Private Const fk_Sede_Procesamiento As Short = 3 'P&C - Bogota
        Private Const id_Centro_Procesamiento As Short = 1 'P&C - Bogota - Scanner Bogota
        Private id_Calendario As Short = 0
        Public idEntidad As Short = 0
        Public idProyecto As Short = 0
        Public idEsquema As Short = 0
        Public idMigracionLog As Long = 0

#End Region

#Region " Metodos reemplazados "
        Protected Overrides Sub OnStart(ByVal args() As String)
            IniciarServicio()
        End Sub

        Protected Overrides Sub OnStop()
            DetenerServicio()
        End Sub
#End Region

#Region " Metodos "
        Private Sub LoadConfig()
            ' Leer la configuración
            If (File.Exists(Program.AppDataPath + MigracionConfig.ConfigFileName)) Then
                Program.Config = MigracionConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub

        Public Sub IniciarServicio()
            Try
                Dim RwebService As SecurityWebService
                Dim CwebService As SecurityWebService

                LoadConfig()

                RwebService = New SecurityWebService(Program.Config.RSecurityWebServiceURL, "")
                CwebService = New SecurityWebService(Program.Config.CSecurityWebServiceURL, "")

                '#If Not Debug Then
                '                ' Validar que la versión corresponda
                '                Dim versionApp As String = webService.getAssemblyVersion(Program.AssemblyName)

                '                If Not versionApp = Program.AssemblyVersion Then
                '                    WriteErrorLog("La versión del aplicativo no corresponde a la registrada en la base de datos," & vbCrLf & vbCrLf & _
                '                                    "Versión registrada: [" & versionApp & "]" & vbCrLf & _
                '                                    "Versión ejecutable: [" & Program.AssemblyVersion & "]")

                '                    Me.Stop()

                '                    Return
                '                End If
                '#End If

                RwebService.CrearCanalSeguro()
                RwebService.setUser(Program.Config.User, MigracionConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStringsR = MigracionConfig.getCadenasConexionR(RwebService)

                CwebService.CrearCanalSeguro()
                CwebService.setUser(Program.Config.User, MigracionConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStringsC = MigracionConfig.getCadenasConexionC(CwebService)

                If Program.ConnectionStringsR.RCore = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Core de Risk")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStringsR.RSecurity = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Security de Risk")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStringsR.RImaging = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Imaging de Risk")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStringsR.RArchiving = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Archiving de Risk")
                    Me.Stop()

                    Return
                End If


                If Program.ConnectionStringsC.CCore = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Core de Convenios")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStringsC.CSecurity = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Security de Convenios")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStringsC.CImaging = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Imaging de Convenios")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStringsC.CArchiving = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Archiving de Convenios")
                    Me.Stop()

                    Return
                End If

                Dim newThread As New Thread(AddressOf Proceso)

                _detener = False
                newThread.Start()

            Catch ex As Exception
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
                Me.Stop()
            End Try
        End Sub

        Public Sub DetenerServicio()
            _detener = True
        End Sub

        Private Sub WriteErrorLog(ByVal nMessage As String)
            Try
                Dim sw As New StreamWriter(Program.AppDataPath & "log_" & Now.ToString("yyyyMMdd") & ".txt", True)

                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                sw.WriteLine("Mensaje: " & nMessage)
                sw.WriteLine("--------------------------------------------------------------")
                sw.WriteLine("")

                sw.Flush()
                sw.Close()
            Catch ex As Exception
                Try : EventLog.WriteEntry(ex.Message, EventLogEntryType.Error) : Catch : End Try
            End Try

            Windows.Forms.Application.DoEvents()
        End Sub

        Private Sub Proceso()
            Dim dbmCoreR As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImagingR As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmSecurityR As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmArchivingR As DBArchiving.DBArchivingDataBaseManager = Nothing
            Dim dbmCoreC As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImagingC As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmSecurityC As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmArchivingC As DBArchiving.DBArchivingDataBaseManager = Nothing

            Try
                dbmCoreR = New DBCore.DBCoreDataBaseManager(Program.ConnectionStringsR.RCore)
                dbmImagingR = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStringsR.RImaging)
                dbmSecurityR = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStringsR.RSecurity)
                dbmArchivingR = New DBArchiving.DBArchivingDataBaseManager(Program.ConnectionStringsR.RArchiving)
                dbmCoreC = New DBCore.DBCoreDataBaseManager(Program.ConnectionStringsC.CCore)
                dbmImagingC = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStringsC.CImaging)
                dbmSecurityC = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStringsC.CSecurity)
                dbmArchivingC = New DBArchiving.DBArchivingDataBaseManager(Program.ConnectionStringsC.CArchiving)

                'Abrir cadenas de conexión
                dbmSecurityR.Connection_Open(2)
                dbmCoreR.Connection_Open(2)
                dbmImagingR.Connection_Open(2)
                dbmSecurityC.Connection_Open(2)
                dbmCoreC.Connection_Open(2)
                dbmImagingC.Connection_Open(2)


                Dim CalendarioDataTable = dbmSecurityC.SchemaConfig.TBL_Calendario.DBFindByfk_EntidadNombre_Calendario(fk_Entidad_Procesamiento, "Migracion")

                If CalendarioDataTable.Count > 0 Then
                    id_Calendario = CalendarioDataTable(0).id_Calendario
                Else
                    WriteErrorLog("No hay calendario programado para el servicio")
                End If

                Dim habil = dbmSecurityC.SchemaConfig.PA_Es_Hora_Habil.DBExecute(fk_Entidad_Procesamiento, id_Calendario)

                If (habil) Then
                    Dim MigracionDataTable = dbmCoreC.SchemaProcess.TBL_Migracion.DBFindBymigrado(False)

                    If Not MigracionDataTable Is Nothing Then

                        For Each registro In MigracionDataTable
                            'Actualizar fecha de inicio
                            idMigracionLog = dbmCoreC.SchemaAudit.PA_TBL_Migracion.DBExecute(registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)

                            Dim MigracionType As New DBCore.SchemaProcess.TBL_MigracionType()

                            'Crear entidad sino existe
                            If registro.id_Entidad_Imaging = 0 Then

                                Dim EntidadDataTableRisk = dbmSecurityR.SchemaConfig.TBL_Entidad.DBFindByid_Entidad(registro.id_Entidad_Risk)

                                idEntidad = dbmSecurityC.SchemaConfig.TBL_Entidad.DBNextId()
                                Dim EntidadType = New DBSecurity.SchemaConfig.TBL_EntidadType() With {
                                .fk_Grupo_Empresarial = 1,
                                .id_Entidad = idEntidad,
                                .Nombre_Entidad = registro.nombre_Entidad,
                                .Codigo_Entidad = EntidadDataTableRisk(0).Codigo_Entidad,
                                .NIT_Entidad = EntidadDataTableRisk(0).NIT_Entidad,
                                .Contacto_Entidad = EntidadDataTableRisk(0).Contacto_Entidad,
                                .Telefono_Entidad = EntidadDataTableRisk(0).Telefono_Entidad,
                                .Activo = True,
                                .Eliminado = False,
                                .fk_Usuario_Log = 1,
                                .Fecha_log = SlygNullable.SysDate
                                }

                                dbmSecurityC.SchemaConfig.TBL_Entidad.DBInsert(EntidadType)

                                'Actualizar TBL_Migracion para campo Entidad
                                dbmCoreC.SchemaProcess.PA_Migracion_Entidad_Proyecto.DBExecute(1, registro.id_Entidad_Risk, idEntidad, 0, 0)
                            Else
                                idEntidad = CShort(registro.id_Entidad_Imaging)
                            End If

                            'Crear Proyecto
                            If registro.id_Proyecto_Imaging = 0 Then
                                Dim ProyectoDataTableRisk = dbmCoreR.SchemaConfig.TBL_Proyecto.DBFindByfk_Entidadid_Proyecto(registro.id_Entidad_Risk, registro.id_Proyecto_Risk)

                                idProyecto = dbmCoreC.SchemaConfig.TBL_Proyecto.DBNextId_for_id_Proyecto(idEntidad)
                                Dim ProyectoType = New DBCore.SchemaConfig.TBL_ProyectoType() With {
                                    .fk_Entidad = idEntidad,
                                    .id_Proyecto = idProyecto,
                                    .Nombre_Proyecto = registro.nombre_Proyecto,
                                    .Vencimiento_Proyecto = SlygNullable.SysDate,
                                    .Responsable_Proyecto = ProyectoDataTableRisk(0).Responsable_Proyecto,
                                    .Telefono_Responsable_Proyecto = ProyectoDataTableRisk(0).Telefono_Responsable_Proyecto,
                                    .Email_Responsable_Proyecto = ProyectoDataTableRisk(0).Email_Responsable_Proyecto,
                                    .fk_Folder_Tipo = ProyectoDataTableRisk(0).fk_Folder_Tipo,
                                    .fk_Caja_Defecto = ProyectoDataTableRisk(0).fk_Caja_Defecto,
                                    .Aplica_Fisico = registro.aplica_Fisico,
                                    .Aplica_Imagen = registro.aplica_Imagen
                                }
                                dbmCoreC.SchemaConfig.TBL_Proyecto.DBInsert(ProyectoType)

                                Dim ProyectoImagingRisk = dbmImagingR.SchemaConfig.TBL_Proyecto.DBFindByfk_Entidadfk_Proyecto(registro.id_Entidad_Risk, registro.id_Proyecto_Risk)

                                Dim ProyectoImagingType = New DBImaging.SchemaConfig.TBL_ProyectoType() With {
                                    .fk_Entidad = idEntidad,
                                    .fk_Proyecto = idProyecto,
                                    .Input_Folder = "C:\",
                                    .Usa_Rango_Paquetes = False,
                                    .Inicio_Nombre_Paquete = "",
                                    .Formato_Fecha_Paquete = "",
                                    .Usa_Archivo_Indices = False,
                                    .Nombre_Archivo_Indices = "",
                                    .fk_Separador = Nothing,
                                    .Columnas_Archivo = False,
                                    .Usa_Encabezado_Columnas = False,
                                    .fk_Identificador_Texto = Nothing,
                                    .Columna_Imagen = 0,
                                    .Caracteres_Omitir = 0,
                                    .fk_Formato_Entrada = ProyectoImagingRisk(0).fk_Formato_Entrada,
                                    .Columna_Key = 0,
                                    .Usa_Indexacion = True,
                                    .Usa_Paquete_x_Imagen = False,
                                    .fk_Formato_Salida = ProyectoImagingRisk(0).fk_Formato_Salida,
                                    .Usa_Folder_Unico = False,
                                    .Usa_File_Unico = False,
                                    .Usa_Calidad = False,
                                    .fk_Entidad_Servidor = 11,
                                    .fk_Servidor = 5,
                                    .Captura_Llaves_Paquete = False,
                                    .Show_Information = False,
                                    .Usa_Indexacion_Parcial = False,
                                    .Seguimiento_Show_Col_Observaciones = False,
                                    .Seguimiento_Show_Col_Key01 = False,
                                    .Seguimiento_Show_Col_Key02 = False,
                                    .Seguimiento_Show_Col_Key03 = False,
                                    .Seguimiento_Show_Col_Paquetes = False,
                                    .Seguimiento_Show_Col_fk_Paquete = False,
                                    .Seguimiento_Show_Col_Items = False,
                                    .Seguimiento_Show_Col_Indexacion = False,
                                    .Seguimiento_Show_Col_Retenido = False,
                                    .Seguimiento_Show_Col_PreCaptura = False,
                                    .Seguimiento_Show_Col_PrimeraCaptura = False,
                                    .Seguimiento_Show_Col_SegundaCaptura = False,
                                    .Seguimiento_Show_Col_TerceraCaptura = False,
                                    .Seguimiento_Show_Col_Calidad = False,
                                    .Seguimiento_Show_Col_Indexado = False,
                                    .Seguimiento_Show_Col_Reproceso = False,
                                    .Seguimiento_Show_Col_Validaciones_Totales = False,
                                    .Seguimiento_Show_Col_Validaciones_Pendientes = False,
                                    .Seguimiento_Show_Col_Recortes = False,
                                    .Usa_Columna_Esquema = False,
                                    .Columna_Esquema = 0,
                                    .Default_Esquema = 0,
                                    .Usa_Columna_Documento = False,
                                    .Columna_Documento = 0,
                                    .Default_Documento = 0,
                                    .Usa_Destape_Contenedor = True,
                                    .Usa_Codigo_Contenedor = True,
                                    .Empaque_Min = 0,
                                    .Empaque_Max = 250,
                                    .Requiere_Exportacion = False,
                                    .fk_Usuario_Log = 1,
                                    .Fecha_Log = SlygNullable.SysDate,
                                    .Usa_Cantidades_Enviadas_Recibidas = False,
                                    .Usa_Recortes = False,
                                    .Muestra_Cantidad_Recibida = False,
                                    .Seguimiento_Show_Col_Calidad_Recortes = False,
                                    .Cantidad_Maxima_OTxTipo = 0,
                                    .Correspondencia_Destape_Vs_Imagenes = False,
                                    .Usa_Dominio_Externo = False,
                                    .Seguimiento_Show_Col_Validacion_Listas = False,
                                    .Notificacion_Cierre_OT = False,
                                    .Notificacion_Cierre_FechaProceso = False,
                                    .Usa_Exportacion_Validos = False,
                                    .Usa_Fecha_Proceso_Cerrada_Captura_Adicional = False,
                                    .Usa_Cruce_Linea = False,
                                    .Usa_Cargue_Masivo = False,
                                    .Exportar_Unico_Archivo_TIFF = False,
                                    .Usa_Fecha_Proceso_Cerrada_F11 = False,
                                    .Usa_Reconocimiento_CBarras = False,
                                    .Usa_Carga_Datos_Captura = False,
                                    .Usa_Cargue_PDF = False,
                                    .Usa_Exportacion_PDF = False,
                                    .Usa_Renombramiento_Imagen_Exportacion = False,
                                    .Usa_Cruce_Generico = False,
                                    .Usa_Cruce_Precaptura = False,
                                    .Usa_Cargue_Log_Generico = False,
                                    .Usa_Correccion_Captura_Maquina = False,
                                    .Usa_Creacion_Automatica_Destape = False,
                                    .Usa_Transmision_Data_Maquina = False,
                                    .Seguimiento_Show_Col_CorreccionCaptura = False,
                                    .Usa_Guardar_Nombre_Imagen = False,
                                    .Cruce_Fecha_Anterior = False,
                                    .Actualiza_Tabla_Log = False,
                                    .Usa_Borrar_File_Mayor_1 = False,
                                    .Usa_Proceso_Detalle = False,
                                    .Usa_Destape_Masivo = False,
                                    .Usa_Validacion_File_1 = False,
                                    .Usa_Campo_Llave = False,
                                    .Usa_Campo_Empaque = False}

                                dbmImagingC.SchemaConfig.TBL_Proyecto.DBInsert(ProyectoImagingType)

                                'Actualizar TBL_Migracion para campo proyecto
                                dbmCoreC.SchemaProcess.PA_Migracion_Entidad_Proyecto.DBExecute(2, registro.id_Entidad_Risk, idEntidad, registro.id_Proyecto_Risk, idProyecto)
                            Else
                                idProyecto = CShort(registro.id_Proyecto_Imaging)
                            End If

                            'Crear esquema
                            If registro.id_Esquema_Imaging = 0 Then
                                Dim EsquemaDataTableRisk = dbmCoreR.SchemaConfig.TBL_Esquema.DBFindByfk_Entidadfk_Proyectoid_Esquema(registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                'IIf(EsquemaDataTableRisk(0).IsValor_Restriccion_MontoNull(), Nothing, EsquemaDataTableRisk(0).Valor_Restriccion_Monto),
                                idEsquema = dbmCoreC.SchemaConfig.TBL_Esquema.DBNextId(idEntidad, idProyecto)
                                Dim EsquemaType = New DBCore.SchemaConfig.TBL_EsquemaType() With {
                                    .fk_Entidad = idEntidad,
                                    .fk_Proyecto = idProyecto,
                                    .id_Esquema = idEsquema,
                                    .Nombre_Esquema = registro.nombre_Esquema,
                                    .fk_Entidad_Servidor = 11,
                                    .fk_Servidor = 5,
                                    .fk_Restriccion_Monto = EsquemaDataTableRisk(0).fk_Restriccion_Monto,
                                    .Valor_Restriccion_Monto = Nothing
                                }
                                dbmCoreC.SchemaConfig.TBL_Esquema.DBInsert(EsquemaType)

                                'Actualizar TBL_Migracion para campo Esquema
                                MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                    .id_Esquema_Imaging = idEsquema}
                                dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                            Else
                                idEsquema = CShort(registro.id_Proyecto_Imaging)
                            End If

                            'Crear campos lista --update TBL_Migración en el sp
                            If registro.camposLista = False Then
                                dbmCoreC.SchemaProcess.PA_Migracion_Campos_Lista.DBExecute(registro.id_Entidad_Risk, idEntidad)
                            End If

                            'insertar y traer Documentos a homologar
                            Dim DocumentosTipologiasDatatable = dbmCoreC.SchemaProcess.PA_Migracion_Set_Documentos.DBExecute(registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)

                            If Not DocumentosTipologiasDatatable Is Nothing Then
                                'crear tipologías
                                If registro.tipologias = False Then
                                    Dim fkTipologiasgrupo = From tipologias In DocumentosTipologiasDatatable
                                                            Where tipologias.fk_Tipologia_Imaging = 0
                                                        Group tipologias By grpTipologias = tipologias.fk_Tipologia & "_" &
                                                                                            tipologias.Nombre_Tipologia
                                                        Into FilaAgrupada = Group
                                                        Select grpTipologias, FilaAgrupada

                                    For Each tipologia In fkTipologiasgrupo
                                        Dim NextidTipologia = dbmCoreC.SchemaConfig.TBL_Tipologia.DBNextId()
                                        Dim TipologiaType = New DBCore.SchemaConfig.TBL_TipologiaType() With {
                                            .id_Tipologia = NextidTipologia,
                                            .Nombre_Tipologia = tipologia.FilaAgrupada(0).Nombre_Tipologia,
                                            .Tipologia_Eliminado = False,
                                            .fk_Usuario_Log = 1,
                                            .Fecha_Log = SlygNullable.SysDate}
                                        dbmCoreC.SchemaConfig.TBL_Tipologia.DBInsert(TipologiaType)

                                        'update DocumentosTipologiasDatatable
                                        For Each dtdt In DocumentosTipologiasDatatable
                                            If dtdt.fk_Tipologia = tipologia.FilaAgrupada(0).fk_Tipologia Then
                                                dtdt.fk_Tipologia_Imaging = NextidTipologia
                                            End If
                                        Next
                                        DocumentosTipologiasDatatable.AcceptChanges()

                                        'update TBL_Documento_Migracion - tipologias
                                        dbmCoreC.SchemaProcess.PA_Migracion_Actualizacion_TipologiaDocumentos.DBExecute(1, tipologia.FilaAgrupada(0).fk_Tipologia, NextidTipologia, Nothing, Nothing)
                                    Next
                                    'Actualizar TBL_Migracion para campo tipologias
                                    MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                        .tipologias = True}
                                    dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                End If

                                'Crear documentos
                                If registro.documentos = False Then
                                    For Each documento In DocumentosTipologiasDatatable
                                        If documento.id_Documento_Imaging = 0 Then
                                            Dim NextidDocumento = dbmCoreC.SchemaConfig.TBL_Documento.DBNextId()
                                            Dim DocumentoType = New DBCore.SchemaConfig.TBL_DocumentoType() With {
                                                .fk_Entidad = idEntidad,
                                                .fk_Proyecto = idProyecto,
                                                .fk_Esquema = idEsquema,
                                                .id_Documento = NextidDocumento,
                                                .Nombre_Documento = documento.Nombre_Documento,
                                                .fk_Tipologia = documento.fk_Tipologia_Imaging,
                                                .fk_Documento_Grupo = Nothing,
                                                .Eliminado = False,
                                                .fk_Usuario_Log = 1,
                                                .Fecha_Log = SlygNullable.SysDate,
                                                .Es_Indexable = True,
                                                .Usa_Recorte = False}
                                            dbmCoreC.SchemaConfig.TBL_Documento.DBInsert(DocumentoType)

                                            'update DocumentosTipologiasDatatable
                                            documento.id_Documento_Imaging = NextidDocumento
                                            DocumentosTipologiasDatatable.AcceptChanges()

                                            'update TBL_Documento_Migracion - documentos
                                            dbmCoreC.SchemaProcess.PA_Migracion_Actualizacion_TipologiaDocumentos.DBExecute(2, Nothing, Nothing, documento.id_Documento, NextidDocumento)
                                        End If
                                    Next

                                    'Actualizar TBL_Migracion para campo documentos
                                    MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                        .documentos = True}
                                    dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                End If
                            End If

                            'Crear Campos
                            'buscar campos por documentos risk, hacer cruce con homologación de documentos creados en imaging, crear campos (crear tabla de homologación campos)
                            If registro.campos = False Then
                                dbmCoreC.SchemaProcess.PA_Migracion_Campos.DBExecute(registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk, idEntidad, idProyecto, idEsquema)
                            End If

                            'Crear validaciones
                            'buscar validaciones por documentos risk, hacer cruce con homologación de documentos creados en imaging, crear validaciones (crear tabla de homologación validaciones)
                            If registro.validaciones = False Then
                                dbmCoreC.SchemaProcess.PA_Migracion_Validaciones.DBExecute(registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk, idEntidad, idProyecto, idEsquema)
                            End If

                            '***** migración proceso como tal *****'
                            If registro.aplica_Fisico Then
                                Dim MigracionProcesoDataTable = dbmCoreC.SchemaProcess.PA_Migracion_Set_Proceso.DBExecute(registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk, idEntidad, idProyecto, idEsquema)
                                If Not MigracionProcesoDataTable Is Nothing Then
                                    'Fecha proceso
                                    If registro.FechasProceso = False Then
                                        dbmCoreC.SchemaProcess.PA_Migracion_Proceso_Fecha_Proceso.DBExecute(registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk, idEntidad, idProyecto, idEsquema)

                                        'Actualizar TBL_Migracion para fechas proceso
                                        MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                            .FechasProceso = True}
                                        dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                    End If


                                    'Ot
                                    If registro.OTs = False Then
                                        Dim fkOTGrupo = From OTs In MigracionProcesoDataTable
                                                           Where OTs.fk_OT_Imaging = 0
                                                       Group OTs By grpOTs = OTs.fk_OT_Risk
                                                       Into FilaAgrupadaOT = Group
                                                       Select grpOTs, FilaAgrupadaOT

                                        Dim OTTipoDataTable = dbmImagingC.SchemaProcess.TBL_OT_Tipo.DBFindByfk_Entidadfk_Proyecto(idEntidad, idProyecto)
                                        Dim fkOTTipo As Integer

                                        If Not OTTipoDataTable Is Nothing Then
                                            fkOTTipo = OTTipoDataTable(0).id_OT_Tipo
                                        Else
                                            Dim NextidOTTipo = dbmImagingC.SchemaProcess.TBL_OT_Tipo.DBNextId_for_id_OT_Tipo(idEntidad, idProyecto)
                                            Dim OTTipoType = New DBImaging.SchemaProcess.TBL_OT_TipoType() With {
                                                .fk_Entidad = idEntidad,
                                                .fk_Proyecto = idProyecto,
                                                .id_OT_Tipo = NextidOTTipo,
                                                .Nombre_OT_Tipo = "Normal",
                                                .Descripcion_OT_Tipo = "Normal",
                                                .Eliminado = 0,
                                                .fk_usuario_log = 1,
                                                .fecha_log = SlygNullable.SysDate
                                                }
                                            dbmImagingC.SchemaProcess.TBL_OT_Tipo.DBInsert(OTTipoType)

                                            fkOTTipo = NextidOTTipo
                                        End If

                                        For Each ot In fkOTGrupo
                                            Dim NextidOT = dbmImagingC.SchemaProcess.TBL_OT.DBNextId()
                                            Dim OTType = New DBImaging.SchemaProcess.TBL_OTType() With {
                                                .fk_Entidad_Procesamiento = 11,
                                                .fk_Entidad = idEntidad,
                                                .fk_Proyecto = idProyecto,
                                                .fk_fecha_proceso = ot.FilaAgrupadaOT(0).Fecha_OT,
                                                .id_OT = NextidOT,
                                                .fk_OT_Tipo = fkOTTipo,
                                                .fk_Sede_Procesamiento = 3,
                                                .fk_Centro_Procesamiento = 1,
                                                .fk_Usuario_Apertura = 1,
                                                .Fecha_Apertura = SlygNullable.SysDate,
                                                .Cerrado = 1,
                                                .fk_Usuario_Cierre = 1,
                                                .Fecha_Cierre = SlygNullable.SysDate,
                                                .Exportado = 0,
                                                .fk_Exportacion = Nothing,
                                                .fk_Linea_Proceso = Nothing}
                                            dbmImagingC.SchemaProcess.TBL_OT.DBInsert(OTType)

                                            'update DocumentosTipologiasDatatable
                                            For Each dtdt In MigracionProcesoDataTable
                                                If dtdt.fk_OT_Risk = ot.FilaAgrupadaOT(0).fk_OT_Risk Then
                                                    dtdt.fk_OT_Imaging = NextidOT
                                                End If
                                            Next
                                            MigracionProcesoDataTable.AcceptChanges()

                                            'Actualizar TBL_Migracion_Proceso OT
                                            dbmCoreC.SchemaProcess.PA_Migracion_Actualizacion_TBL_Proceso.DBExecute(1, ot.FilaAgrupadaOT(0).fk_OT_Risk, NextidOT, Nothing, Nothing, Nothing)
                                        Next

                                        'actualizar tbl_migracion ot
                                        MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                            .OTs = True}
                                        dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                    End If


                                    'Precinto
                                    If registro.Precintos = False Then

                                        Dim fkOTPrecintoGrupo = From OTPrecintos In MigracionProcesoDataTable
                                                                Where OTPrecintos.fk_OT_Imaging > 0
                                                                Group OTPrecintos By grpOTPrecintos = OTPrecintos.fk_OT_Risk & "_" &
                                                                                                        OTPrecintos.Precinto_Risk & "_" &
                                                                                                        OTPrecintos.fk_OT_Imaging
                                                                Into FilaAgrupadaOTPrecintos = Group
                                                                Select grpOTPrecintos, FilaAgrupadaOTPrecintos


                                        For Each otprecinto In fkOTPrecintoGrupo
                                            Dim NextidOTPrecinto = dbmImagingC.SchemaProcess.TBL_Precinto.DBNextId(otprecinto.FilaAgrupadaOTPrecintos(0).fk_OT_Imaging)

                                            Dim OTPrecintoType = New DBImaging.SchemaProcess.TBL_PrecintoType() With {
                                                .fk_OT = otprecinto.FilaAgrupadaOTPrecintos(0).fk_OT_Imaging,
                                                .id_Precinto = NextidOTPrecinto,
                                                .Precinto = otprecinto.FilaAgrupadaOTPrecintos(0).Precinto_Risk,
                                                .fk_Puesto_Trabajo = 1,
                                                .fk_Usuario_Apertura = 1,
                                                .Fecha_Apertura = SlygNullable.SysDate,
                                                .Cerrado = 1,
                                                .fk_Usuario_Cierre = 1,
                                                .Fecha_Cierre = SlygNullable.SysDate}


                                            dbmImagingC.SchemaProcess.TBL_Precinto.DBInsert(OTPrecintoType)

                                            Dim OTPrecintoContenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType() With {
                                                .fk_OT = otprecinto.FilaAgrupadaOTPrecintos(0).fk_OT_Imaging,
                                                .fk_Precinto = NextidOTPrecinto,
                                                .id_Contenedor = NextidOTPrecinto,
                                                .fk_Esquema = otprecinto.FilaAgrupadaOTPrecintos(0).fk_Esquema_Imaging,
                                                .Token = otprecinto.FilaAgrupadaOTPrecintos(0).Precinto_Risk,
                                                .fk_Usuario_Apertura = 1,
                                                .Fecha_Apertura = SlygNullable.SysDate,
                                                .Cerrado = 1,
                                                .fk_Usuario_Cierre = 1,
                                                .Fecha_Cierre = SlygNullable.SysDate,
                                                .Cargado = 0,
                                                .Empacado = 0}

                                            dbmImagingC.SchemaProcess.TBL_Contenedor.DBInsert(OTPrecintoContenedorType)

                                            'update DocumentosTipologiasDatatable
                                            For Each dtdt In MigracionProcesoDataTable
                                                If ((dtdt.fk_OT_Risk = otprecinto.FilaAgrupadaOTPrecintos(0).fk_OT_Risk) And (dtdt.Precinto_Risk = otprecinto.FilaAgrupadaOTPrecintos(0).Precinto_Risk) And (dtdt.fk_OT_Imaging = otprecinto.FilaAgrupadaOTPrecintos(0).fk_OT_Imaging)) Then
                                                    dtdt.fk_Precinto_Imaging = NextidOTPrecinto
                                                End If
                                            Next
                                            MigracionProcesoDataTable.AcceptChanges()

                                            'Actualizar TBL_Migracion_Proceso OT
                                            dbmCoreC.SchemaProcess.PA_Migracion_Actualizacion_TBL_Proceso.DBExecute(2, otprecinto.FilaAgrupadaOTPrecintos(0).fk_OT_Risk, otprecinto.FilaAgrupadaOTPrecintos(0).fk_OT_Imaging, otprecinto.FilaAgrupadaOTPrecintos(0).Precinto_Risk, NextidOTPrecinto, Nothing)
                                        Next

                                        'actualizar tbl_migracion ot
                                        MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                            .Precintos = True}
                                        dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                    End If

                                    ''Contenedor
                                    'If registro.Contenedores = False Then


                                    '    'actualizar tbl_migracion ot
                                    '    MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                    '        .Contenedores = True}
                                    '    dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                    'End If

                                    'Cargue-paquete
                                    If registro.Cargues = False Then
                                        Dim fkOTPrecintoCargueGrupo = From OTPrecintosCargue In MigracionProcesoDataTable
                                                              Where (OTPrecintosCargue.fk_OT_Imaging > 0 And OTPrecintosCargue.fk_Precinto_Imaging > 0 And OTPrecintosCargue.fk_Cargue_Imaging = 0)
                                                              Group OTPrecintosCargue By grpOTPrecintosCargue = OTPrecintosCargue.fk_OT_Risk & "_" &
                                                                                                      OTPrecintosCargue.Precinto_Risk & "_" &
                                                                                                      OTPrecintosCargue.fk_OT_Imaging
                                                              Into FilaAgrupadaOTPrecintosCargue = Group
                                                              Select grpOTPrecintosCargue, FilaAgrupadaOTPrecintosCargue

                                        

                                        For Each otprecintocargue In fkOTPrecintoCargueGrupo
                                            Dim cargueType As New DBImaging.SchemaProcess.TBL_CargueType()
                                            Dim managerR As FileProviderManager = Nothing
                                            Dim managerC As FileProviderManager = Nothing
                                            Dim expedientes As New List(Of Long)

                                            Try
                                                Dim servidorC = dbmImagingC.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(11, 5)(0).ToCTA_ServidorSimpleType
                                                Dim centroC = dbmImagingC.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(11, 3, 1)(0).ToCTA_Centro_ProcesamientoSimpleType()
                                                managerC = New FileProviderManager(servidorC, centroC, dbmImagingC, 1)
                                                managerC.Connect()

                                                ' Obtener el nuevo id para el cargue
                                                cargueType.fk_Entidad = idEntidad
                                                cargueType.fk_Proyecto = idProyecto
                                                cargueType.fk_Estado = DBCore.EstadoEnum.Creado
                                                cargueType.fk_Entidad_Procesamiento = 11
                                                cargueType.fk_Sede_Procesamiento_Cargue = 3
                                                cargueType.fk_Centro_Procesamiento_Cargue = 1
                                                cargueType.fk_Entidad_Servidor = 11
                                                cargueType.fk_Servidor = 5
                                                cargueType.fk_OT = otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_OT_Imaging
                                                cargueType.Fecha_Proceso = otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).Fecha_OT_Risk
                                                cargueType.Observaciones = "CP-" & "Migración"
                                                cargueType.fk_Usuario_Log = 1

                                                cargueType.id_Cargue = dbmImagingC.SchemaProcess.PA_Guardar_TBL_Cargue.DBExecute(
                                                    cargueType.fk_Entidad,
                                                    cargueType.fk_Proyecto,
                                                    cargueType.fk_Estado,
                                                    cargueType.fk_Entidad_Procesamiento,
                                                    cargueType.fk_Sede_Procesamiento_Cargue,
                                                    cargueType.fk_Centro_Procesamiento_Cargue,
                                                    cargueType.fk_Entidad_Servidor,
                                                    cargueType.fk_Servidor,
                                                    cargueType.fk_OT,
                                                    cargueType.Fecha_Proceso,
                                                    cargueType.Observaciones,
                                                    cargueType.fk_Usuario_Log
                                                    )

                                                Dim dataCargue = dbmImagingC.SchemaProcess.TBL_Cargue.DBGet(cargueType.id_Cargue)
                                                cargueType.Fecha_Cargue = dataCargue(0).Fecha_Cargue

                                                'Dim formato = Utilities.GetEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                                                'Dim compresion = Utilities.GetEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                                                Dim paqueteType As New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()

                                                paqueteType.fk_Cargue = cargueType.id_Cargue
                                                paqueteType.id_Cargue_Paquete = 1
                                                paqueteType.fk_Estado = DBCore.EstadoEnum.Indexacion
                                                paqueteType.fk_Usuario_Log = 1
                                                paqueteType.Fecha_Proceso = cargueType.Fecha_Cargue
                                                paqueteType.Path_Cargue_Paquete = ""
                                                paqueteType.Bloqueado = False
                                                paqueteType.Data_Path = ""
                                                paqueteType.fk_Sede_Procesamiento_Asignada = 3
                                                paqueteType.fk_Centro_Procesamiento_Asignado = 1

                                                dbmImagingC.SchemaProcess.TBL_Cargue_Paquete.DBInsert(paqueteType)

                                                ' Actualizar el contenedor
                                                Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                                                contenedorType.Cargado = True
                                                contenedorType.fk_Cargue = cargueType.id_Cargue
                                                contenedorType.fk_Paquete = 1
                                                dbmImagingC.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_OT_Imaging, otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_Precinto_Imaging, otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_Precinto_Imaging)

                                                Dim idExpediente As Long = -1
                                                Dim idExpedienteAnterior As Long = -1
                                                Dim newExpediente As Boolean = False
                                                Const idFolder As Short = 1
                                                Const idVersion As Short = 1
                                                Dim idFile As Short

                                                Dim selectExpedientesFolderFile = From ExpedienteFolderFile In MigracionProcesoDataTable
                                                                     Where (ExpedienteFolderFile.fk_OT_Imaging = otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_OT_Imaging And ExpedienteFolderFile.fk_Precinto_Imaging = otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_Precinto_Imaging)
                                                                     Order By (otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_OT_Imaging And otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_Precinto_Imaging And otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_Expediente_Risk And otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_Folder_Risk And otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_File_Risk)


                                                For Each expedientefolderfile In selectExpedientesFolderFile
                                                    If idExpedienteAnterior <> expedientefolderfile.fk_Expediente_Risk Then
                                                        If expedientefolderfile.fk_Expediente = 0 Then
                                                            idExpediente = -1
                                                        Else
                                                            idExpediente = expedientefolderfile.fk_Expediente
                                                        End If

                                                    End If
                                                    ' Crear un nuevo Expediente
                                                    If (idExpediente = -1) Then
                                                        newExpediente = True
                                                        idExpediente = dbmCoreC.SchemaProcess.PA_Insertar_Expediente.DBExecute(idEntidad, idProyecto, idEsquema, CShort(1), CShort(1), CShort(1), Nothing)
                                                        'idFile = 1
                                                        idFile = expedientefolderfile.fk_File_Risk

                                                        'update MigracionProcesoDataTable
                                                        For Each dtdt In MigracionProcesoDataTable
                                                            If (dtdt.fk_Expediente_Risk = expedientefolderfile.fk_Expediente_Risk) Then
                                                                dtdt.fk_Expediente = idExpediente
                                                            End If
                                                        Next
                                                        MigracionProcesoDataTable.AcceptChanges()

                                                        'Actualizar la tabla real
                                                        dbmCoreC.SchemaProcess.PA_Migracion_Set_Proceso_Expediente.DBExecute(expedientefolderfile.fk_Expediente_Risk, idExpediente, CLng(cargueType.id_Cargue))
                                                    Else
                                                        newExpediente = False
                                                        'idFile += CShort(1)
                                                        idFile = expedientefolderfile.fk_File_Risk
                                                    End If

                                                    ' Crear lista de expedientes
                                                    If (Not expedientes.Contains(idExpediente)) Then
                                                        expedientes.Add(idExpediente)
                                                    End If

                                                    If (newExpediente) Then
                                                        ' Crear Folder Process
                                                        Dim folderProType As New DBCore.SchemaProcess.TBL_FolderType()
                                                        folderProType.fk_Expediente = idExpediente
                                                        folderProType.id_Folder = idFolder
                                                        folderProType.CBarras_Folder = idExpediente 'rowPaquete.CBarrasContenedor
                                                        folderProType.Fecha_Inicial = SlygNullable.SysDate
                                                        folderProType.Fecha_Final = SlygNullable.SysDate
                                                        dbmCoreC.SchemaProcess.TBL_Folder.DBInsert(folderProType)

                                                        ' Crear Folder Imaging
                                                        Dim folderImgType As New DBCore.SchemaImaging.TBL_FolderType()
                                                        folderImgType.fk_Expediente = idExpediente
                                                        folderImgType.fk_Folder = idFolder
                                                        folderImgType.fk_Entidad_Servidor = 11
                                                        folderImgType.fk_Servidor = 5
                                                        folderImgType.Fecha_Creacion = SlygNullable.SysDate
                                                        folderImgType.Fecha_Transferencia = Nothing
                                                        folderImgType.En_Transferencia = False
                                                        folderImgType.fk_Cargue = cargueType.id_Cargue
                                                        folderImgType.fk_Cargue_Paquete = 1

                                                        dbmCoreC.SchemaImaging.TBL_Folder.DBInsert(folderImgType)
                                                    End If

                                                    ' Crear el File
                                                    Dim fileProType As New DBCore.SchemaProcess.TBL_FileType()
                                                    fileProType.fk_Expediente = idExpediente
                                                    fileProType.fk_Folder = idFolder
                                                    fileProType.id_File = idFile
                                                    fileProType.File_Unique_Identifier = Guid.NewGuid
                                                    fileProType.fk_Documento = expedientefolderfile.id_Documento_Imaging
                                                    fileProType.Folios_File = expedientefolderfile.Folios_File_Risk
                                                    fileProType.Monto_File = 0
                                                    fileProType.CBarras_File = expedientefolderfile.CBarras_File_Risk
                                                    dbmCoreC.SchemaProcess.TBL_File.DBInsert(fileProType)

                                                    Dim FileImagingCoreR = dbmCoreR.SchemaImaging.TBL_File.DBFindByfk_Expedientefk_Folderfk_File(expedientefolderfile.fk_Expediente_Risk, expedientefolderfile.fk_Folder_Risk, expedientefolderfile.fk_File_Risk)

                                                    If Not FileImagingCoreR Is Nothing Then
                                                        If FileImagingCoreR.Rows.Count > 0 Then
                                                            Dim fileImgType As New DBCore.SchemaImaging.TBL_FileType()
                                                            fileImgType.fk_Expediente = idExpediente
                                                            fileImgType.fk_Folder = idFolder
                                                            fileImgType.fk_File = idFile
                                                            fileImgType.id_Version = idVersion
                                                            fileImgType.File_Unique_Identifier = fileProType.File_Unique_Identifier
                                                            fileImgType.Folios_Documento_File = FileImagingCoreR(0).Folios_Documento_File
                                                            fileImgType.Tamaño_Imagen_File = FileImagingCoreR(0).Tamaño_Imagen_File
                                                            fileImgType.Nombre_Imagen_File = FileImagingCoreR(0).Nombre_Imagen_File
                                                            fileImgType.Key_Cargue_Item = FileImagingCoreR(0).Key_Cargue_Item
                                                            fileImgType.Save_FileName = FileImagingCoreR(0).Save_FileName
                                                            fileImgType.fk_Content_Type = FileImagingCoreR(0).fk_Content_Type
                                                            fileImgType.fk_Usuario_Log = 1
                                                            fileImgType.Validaciones_Opcionales = False
                                                            fileImgType.Es_Anexo = False
                                                            fileImgType.fk_Anexo = Nothing

                                                            dbmCoreC.SchemaImaging.TBL_File.DBInsert(fileImgType)

                                                            managerC.CreateItem(idExpediente, idFolder, idFile, idVersion, FileImagingCoreR(0).fk_Content_Type, fileProType.File_Unique_Identifier)

                                                            Dim ServidoresR = dbmImagingR.SchemaCore.CTA_Servidor.DBGet(Nothing, Nothing)

                                                            For Each servidor In ServidoresR

                                                                Dim centroR = dbmImagingR.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(1, 1, 1)(0).ToCTA_Centro_ProcesamientoSimpleType()
                                                                managerR = New FileProviderManager(servidor.ToCTA_ServidorSimpleType, centroR, dbmImagingR, 1)
                                                                managerR.Connect()

                                                                Dim Folios = managerR.GetFolios(expedientefolderfile.fk_Expediente_Risk, expedientefolderfile.fk_Folder_Risk, expedientefolderfile.fk_File_Risk, 1)

                                                                '20210305
                                                                'managerC.EvaluateVolumen(idExpediente, GetSize(expedientefolderfile, managerR, Folios), Folios)

                                                                For folio As Short = 1 To Folios
                                                                    Dim Imagen() As Byte = Nothing
                                                                    Dim Thumbnail() As Byte = Nothing

                                                                    'Busca folio en risk
                                                                    managerR.GetFolio(expedientefolderfile.fk_Expediente_Risk, expedientefolderfile.fk_Folder_Risk, expedientefolderfile.fk_File_Risk, 1, folio, Imagen, Thumbnail)

                                                                    'Inserta folio en convenios
                                                                    managerC.CreateFolio(idExpediente, idFolder, idFile, idVersion, CShort(1), Imagen, Thumbnail, False)
                                                                Next
                                                                If (managerR IsNot Nothing) Then managerR.Disconnect()
                                                            Next
                                                        End If
                                                    End If

                                                    idExpedienteAnterior = expedientefolderfile.fk_Expediente_Risk
                                                Next
                                            Catch ex As Exception
                                                'Elimina el cargue realizado
                                                If (Not cargueType.id_Cargue Is Nothing AndAlso (dbmImagingC IsNot Nothing) AndAlso (managerC IsNot Nothing)) Then
                                                    ' Actualizar el cargue y expediente de migración proceso  por idCargue                                               
                                                    dbmCoreC.SchemaProcess.PA_Migracion_Actualizacion_TBL_Proceso.DBExecute(3, Nothing, Nothing, Nothing, Nothing, CLng(cargueType.id_Cargue))

                                                    dbmCoreC.SchemaProcess.PA_Borrar_Expedientes.DBExecute(cargueType.id_Cargue)
                                                    dbmImagingC.SchemaProcess.PA_Borrar_Expedientes.DBExecute(cargueType.id_Cargue)
                                                    dbmImagingC.SchemaProcess.TBL_Cargue.DBDelete(cargueType.id_Cargue)

                                                    ' Actualizar el contenedor
                                                    Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                                                    contenedorType.Cargado = False
                                                    contenedorType.fk_Cargue = DBNull.Value
                                                    contenedorType.fk_Paquete = DBNull.Value
                                                    dbmImagingC.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_OT_Imaging, otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_Precinto_Imaging, otprecintocargue.FilaAgrupadaOTPrecintosCargue(0).fk_Precinto_Imaging)


                                                    For Each Expediente In expedientes
                                                        managerC.DeleteExpediente(Expediente)
                                                    Next
                                                End If
                                            Finally
                                                If (managerC IsNot Nothing) Then managerC.Disconnect()
                                                If (managerR IsNot Nothing) Then managerR.Disconnect()
                                            End Try
                                        Next

                                        'actualizar tbl_migracion ot
                                        MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                            .Cargues = True}
                                        dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                    End If


                                    ''Estados
                                    If registro.Estados = False Then
                                        dbmCoreC.SchemaProcess.PA_Migracion_Insercion_Estados.DBExecute(idEntidad, idProyecto, idEsquema)

                                        'actualizar tbl_migracion
                                        MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                            .Estados = True}
                                        dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                    End If

                                    'file data
                                    If registro.Data = False Then
                                        dbmCoreC.SchemaProcess.PA_Migracion_Insercion_File_Data.DBExecute(idEntidad, idProyecto, idEsquema)

                                        'actualizar tbl_migracion
                                        MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                            .Data = True}
                                        dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                    End If


                                    'file validaciones
                                    If registro.DataValidaciones = False Then
                                        dbmCoreC.SchemaProcess.PA_Migracion_Insercion_File_Validacion.DBExecute(idEntidad, idProyecto, idEsquema)

                                        'actualizar tbl_migracion ot
                                        MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                            .DataValidaciones = True}
                                        dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                                    End If
                                End If
                            ElseIf registro.aplica_Imagen Then
                                'sacar la data de proceso de otro lado

                            End If

                            '***** fin por esquema *****'
                            'Actualizar TBL_Migracion para campo migrado
                            MigracionType = New DBCore.SchemaProcess.TBL_MigracionType() With {
                                .migrado = True}
                            dbmCoreC.SchemaProcess.TBL_Migracion.DBUpdate(MigracionType, registro.id_Entidad_Risk, registro.id_Proyecto_Risk, registro.id_Esquema_Risk)
                            'Actualización Log
                            dbmCoreC.SchemaAudit.PA_TBL_Migracion_Fin.DBExecute(idMigracionLog, Nothing)
                        Next
                    End If
                End If

            Catch ex As Exception
                WriteErrorLog("Error en método proceso: " + ex.ToString())
                'update por si hay error
                dbmCoreC.SchemaAudit.PA_TBL_Migracion_Fin.DBExecute(idMigracionLog, ex.ToString())
            Finally
                If (dbmSecurityR IsNot Nothing) Then dbmSecurityR.Connection_Close()
                If (dbmCoreR IsNot Nothing) Then dbmCoreR.Connection_Close()
                If (dbmImagingR IsNot Nothing) Then dbmImagingR.Connection_Close()
                If (dbmSecurityC IsNot Nothing) Then dbmSecurityC.Connection_Close()
                If (dbmCoreC IsNot Nothing) Then dbmCoreC.Connection_Close()
                If (dbmImagingC IsNot Nothing) Then dbmImagingC.Connection_Close()
            End Try

            Me.Stop()

        End Sub
#End Region

#Region "Funciones "
        Private Function GetSize(ByRef expedientefolderfile As DBCore.SchemaProcess.CTA_Migracion_ProcesoRow, ByRef managerR As FileProviderManager, ByVal Folios As Short) As Long
            Dim Peso As Long = 0

            For folio As Short = 1 To Folios
                Dim Imagen() As Byte = Nothing
                Dim Thumbnail() As Byte = Nothing

                'Busca folio en risk
                managerR.GetFolio(expedientefolderfile.fk_Expediente_Risk, expedientefolderfile.fk_Folder_Risk, expedientefolderfile.fk_File_Risk, 1, folio, Imagen, Thumbnail)

                Peso += Imagen.Length
            Next

            Return Peso
        End Function
#End Region

    End Class
End Namespace
