Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.IO
Imports System.Globalization
Imports Slyg.Tools
Imports Slyg.Tools.Progress
Imports System.Windows.Forms
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library.Config
Imports Miharu.FileProvider.Library
Imports Miharu.Imaging.Library.Eventos
Imports Slyg.Tools.Imaging.ImageManager
Imports System.Dynamic
Imports System.Threading
Imports Ionic.Zip
Imports System.Text


Namespace Imaging.Carpeta_Unica.Forms.Reportes
    Public Class FormReportes

#Region " Declaraciones "
        Private _Plugin As CarpetaUnicaPlugin
        Private _Reportes As DataTable
        Private _Reportes_Delta As DataTable
        Private _Procesos As DataTable
        Private _Rotulos As DataTable
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private servidor As DBImaging.SchemaCore.CTA_ServidorSimpleType
        Private centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType
        Private CarpetaTempImagenes As String
        Private DateTimeNombreArchivo As String
        Private ConfigReporteDataTableDelta As DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteDataTable
        Private ConfigReporteDataTableDeltaEmpresarial As DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteDataTable
        Private DTResultReportReporte As DataTable
        Private DTResultReport As DataTable
        Private DTResultReportPC As DataTable
        Private pathDelta As String = ""
        Private pathDeltaImg As String = ""
        Private PathDoeTapas As String = ""
        Private ProgressForm As New FormProgress()
        Private ContadorDeltaImagen As Integer = 0
        Private ContadorZip As Integer
        Private GenerarDelta As Boolean
        Private GenerarDeltaEmpresarial As Boolean
        Dim _EventManager As EventManager
        Dim DateTimeGeneracionNombreArchivo As String
        Private ArrayNotificacion As ArrayList
        Private ArrayNotificacionTapas As ArrayList
        Private BloqueoConcurrencia As Object
        Private BloqueoConcurrenciaTapas As Object = New Object
        Dim _ltErroresReporte As New List(Of String)
        Dim dtPeriodicidad As DataTable
        Dim _StrArchivoLog As String
        Dim _rutaGenerar As String
        Dim _rutaStartProcess As String
        Dim _horaDeltaReport As Integer = 0
        Dim _minutosDeltaReport As Integer = 0
        Dim _segundosDeltaReport As Integer = 0
        Dim Rta_ValidaRotulo As Respuesta_ValidaRotulo
        Public Shared FileNamesCons As New List(Of String)
        Private ImageSizeLimit As Long = 0
        Private ImageLimit As Boolean = False


        Friend Structure FolderDeltaImagen
            Dim Directorio As String
            Dim Peso As Decimal
        End Structure


        Friend Structure Respuesta_ValidaRotulo
            Dim msj As String
            Dim Result As Boolean
        End Structure

#End Region

#Region " Propiedades "
        Property SelectedPath As String
            Get
                Return RutaTextBox.Text.TrimEnd("\"c) & "\"
            End Get
            Set(ByVal value As String)
                RutaTextBox.Text = value
            End Set
        End Property

        Public Property EventManager As EventManager
            Get
                Return Me._EventManager
            End Get
            Set(value As EventManager)
                _EventManager = value
            End Set
        End Property
#End Region

#Region " Contructores "

        Public Sub New(ByVal nCarpetaUnicaDesktopPlugin As CarpetaUnicaPlugin)
            InitializeComponent()

            _Plugin = nCarpetaUnicaDesktopPlugin
            CargaTablas()
        End Sub

#End Region

#Region " Eventos "
        Private Sub FRM_Reporte_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Cargar_Reportes()
            Habilitar_Controles()
        End Sub

        Private Sub SelectFolderButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectFolderButton.Click
            SelectFolderPath()
        End Sub

        Private Sub GenerarButton_Click(sender As System.Object, e As System.EventArgs) Handles GenerarButton.Click
            If ArchivoRotulosCheckBox.Checked Then
                _Rotulos = Nothing

                'validar archivo Rotulo Seleccionado
                If ArchivoRotulosTextBox Is String.Empty Then
                    DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar un archivo de Rotulos", "Generacion de Reportes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                    Return
                End If

                'Cargar log de Rotulos
                If Not Cargar_ArchivoRotulos() Then
                    Return
                End If
            End If

            If Validar() Then
                If DesktopMessageBoxControl.DesktopMessageShow(IIf(Not ArchivoRotulosCheckBox.Checked, "Desea generar el reporte: " & ReporteDesktopComboBox.SelectedText & ", de la fecha: " & dtpFechaProceso.Text & "?", "Desea Generar el Reporte del archivo de Rotulos cargado?"), "Generar Reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = Windows.Forms.DialogResult.OK Then
                    ContadorZip = 1
                    GenerarDelta = False
                    GenerarDeltaEmpresarial = False
                    ConfigReporteDataTableDelta = New DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteDataTable
                    ConfigReporteDataTableDeltaEmpresarial = New DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteDataTable

                    CarpetaTempImagenes = Program.AppPath & Program.TempPath & "Delta_" & Guid.NewGuid().ToString()
                    If Not Directory.Exists(CarpetaTempImagenes) Then
                        CrearDirectorio(CarpetaTempImagenes)
                    End If

                    Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                    Try
                        dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                        dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                        dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                        Dim ImageSizeLimitDataTable = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "ImageSizeLimit")
                        If (ImageSizeLimitDataTable.Rows.Count > 0) Then
                            ImageSizeLimit = CInt(ImageSizeLimitDataTable(0).Valor_Parametro_Sistema.ToString())
                            ImageLimit = True
                        End If

                        dtPeriodicidad = dbmIntegration.SchemaBCSCarpetaUnica.PA_Config_Get_Periodicidad.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), CType(ReporteDesktopComboBox.SelectedValue, Integer))

                        If dtPeriodicidad.Rows.Count() > 0 Or ArchivoRotulosCheckBox.Checked Then
                            Me._rutaGenerar = Me.RutaTextBox.Text
                            Me._rutaStartProcess = Me._rutaGenerar + "\SALIDA"
                            Me._StrArchivoLog = Me._rutaGenerar + "\Log_REPORTE_BCS_" + Date.Now.ToString("yyyyMMdd") + "_" + Date.Now.Hour.ToString("00") + Date.Now.Minute.ToString("00") + Date.Now.Second.ToString("00") + ".dat"

                            EscribeLog(_StrArchivoLog, "LOG DE REPORTE PARA FECHA DE PROCESO: " + Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), True, False)

                            'CREAR DIRECTORIO DE SALIDA
                            Try
                                Dim strA() As String = Directory.GetFiles(_rutaStartProcess, "*", SearchOption.AllDirectories)
                                If (strA.Count >= 1) Then
                                    If (DesktopMessageBoxControl.DesktopMessageShow("La Carpeta " + _rutaStartProcess + " no esta vacia, ¿Desea eliminar su contenido?", "Contenido Carpeta", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False, False) = Windows.Forms.DialogResult.OK) Then
                                        CrearDirectorio(_rutaStartProcess, True)
                                    Else
                                        CrearDirectorio(_rutaStartProcess, False)
                                    End If
                                End If
                            Catch ex As Exception
                                CrearDirectorio(_rutaStartProcess, False)
                            End Try


                            Me.Enabled = False
                            Me.Cursor = Cursors.WaitCursor

                            Dim Compresion As ImageManager.EnumCompression
                            If (_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                Compresion = ImageManager.EnumCompression.Ccitt4
                            Else
                                Compresion = ImageManager.EnumCompression.Lzw
                            End If

                            Dim ReportesGenerar As DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteDataTable
                            Dim ReportesDictionary As New Dictionary(Of Integer, String)

                            'Trae los reportes dependiendo de lo seleccionado.
                            ReportesGenerar = dbmIntegration.SchemaBCSCarpetaUnica.PA_Get_Reporte.DBExecute(CInt(Me.ReporteDesktopComboBox.SelectedValue), True)

                            'Se adicionan los reportes
                            For Each reporte In ReportesGenerar
                                ReportesDictionary.Add(reporte.id_Reporte, reporte.Nombre_Reporte)
                            Next

                            Dim ProcesosGenerar As DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoDataTable
                            Dim ProcesosDictionary As New Dictionary(Of Integer, String)

                            'Buscar todos los procesos
                            ProcesosGenerar = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByActivoAplica_Tipo_Proceso(True, True)

                            'Se adicionan los procesos
                            For Each proceso In ProcesosGenerar
                                If proceso.Nombre_Tipo_Proceso <> "CONSTRUCTOR" Then
                                    ProcesosDictionary.Add(proceso.id_Tipo_Proceso, proceso.Nombre_Tipo_Proceso)
                                End If
                            Next

                            'Iniciar proceso                
                            ProgressForm.Process = ""
                            ProgressForm.Action = ""
                            ProgressForm.ValueProcess = 0
                            ProgressForm.ValueAction = 0
                            ProgressForm.MaxValueAction = ReportesDictionary.Count + 1

                            ProgressForm.Show()

                            Dim Count_Action As Integer = 0
                            For Each Item In ReportesDictionary
                                ProgressForm.Action = Item.Value
                                Application.DoEvents()

                                Dim ConfigReporteDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBFindByid_ReporteVigente(Item.Key, True)
                                DateTimeNombreArchivo = Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + "_" + CStr(Now.Hour).PadLeft(2, "0"c)

                                DTResultReportReporte = Nothing

                                If ((Item.Value.ToUpper() = "DELTA") Or ((Item.Value.ToUpper() = "DELTA_EMPRESARIAL"))) Then
                                    Try
                                        DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Get_Registros_Delta.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), Item.Key, CInt(ProcesoDesktopComboBox.SelectedValue))
                                        pathDeltaImg = Me._rutaStartProcess + "\DELTAIMAGEN\" + Item.Value.ToUpper()

                                        If (Not Directory.Exists(pathDeltaImg)) Then
                                            CrearDirectorio(pathDeltaImg)
                                        End If

                                        If DTResultReport.Rows.Count > 0 Then
                                            'Borrar Delta Fracción
                                            dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_Borrar_DELTA_Fraccion.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), CInt(ProcesoDesktopComboBox.SelectedValue))

                                            Dim Procesos As List(Of Object) = Nothing
                                            Procesos = (From a In DTResultReport Group a By groupDt = a.Field(Of Integer)("fk_Tipo_Proceso") Into Group Select Group.Select(Function(x) x("fk_Tipo_Proceso")).First()).ToList()

                                            Dim id_Proceso_Productos_Cruzados As Integer = 0
                                            id_Proceso_Productos_Cruzados = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByNombre_Tipo_ProcesoActivo("PRODUCTOS CRUZADOS", True).Rows(0)("id_Tipo_Proceso")

                                            ArrayNotificacion = New ArrayList
                                            BloqueoConcurrencia = New Object

                                            ContadorDeltaImagen = 0

                                            Dim Count_Process_Delta As Integer = 0
                                            ProgressForm.ValueProcess = Count_Process_Delta
                                            ProgressForm.MaxValueProcess = Procesos.Count
                                            For h = 0 To Procesos.Count - 1
                                                If Procesos(h).ToString() <> id_Proceso_Productos_Cruzados Then
                                                    For Each Item2 In ProcesosDictionary
                                                        If Item2.Key = Procesos(h) Then
                                                            ProgressForm.Process = Item2.Value
                                                            Application.DoEvents()

                                                            ArmarProceso(Item2.Key, Item2.Value, ConfigReporteDataTable, Compresion)

                                                            If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                                            Count_Process_Delta += 1
                                                            ProgressForm.ValueProcess = Count_Process_Delta
                                                        End If
                                                    Next
                                                End If
                                            Next

                                            Dim Salir As Boolean = False
                                            While Salir = False
                                                Salir = True
                                                SyncLock BloqueoConcurrencia
                                                    For Each HiloTerminado As Boolean In ArrayNotificacion
                                                        If HiloTerminado = False Then
                                                            Salir = False
                                                        End If
                                                    Next
                                                End SyncLock
                                                Thread.Sleep(1000)
                                            End While

                                            'Productos cruzados
                                            Dim ProcesoEncontrado As Boolean = False
                                            For Each itemProceso In Procesos
                                                If itemProceso = id_Proceso_Productos_Cruzados Then
                                                    ProcesoEncontrado = True
                                                End If
                                            Next

                                            If (ProcesoEncontrado) Then
                                                Dim RutaPC = Item.Value.ToUpper() + "_PRODUCTOS_CRUZADOS"
                                                pathDeltaImg = Me._rutaStartProcess + "\DELTAIMAGEN\" + RutaPC

                                                If (Not Directory.Exists(pathDeltaImg)) Then
                                                    CrearDirectorio(pathDeltaImg)
                                                End If

                                                ArrayNotificacion = New ArrayList
                                                BloqueoConcurrencia = New Object

                                                ProgressForm.Process = "PRODUCTOS CRUZADOS"
                                                Application.DoEvents()

                                                ArmarProceso(id_Proceso_Productos_Cruzados, "PRODUCTOS CRUZADOS", ConfigReporteDataTable, Compresion)

                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                                Count_Process_Delta += 1
                                                ProgressForm.ValueProcess = Count_Process_Delta

                                                Dim SalirPC As Boolean = False
                                                While SalirPC = False
                                                    SalirPC = True
                                                    SyncLock BloqueoConcurrencia
                                                        For Each HiloTerminado As Boolean In ArrayNotificacion
                                                            If HiloTerminado = False Then
                                                                SalirPC = False
                                                            End If
                                                        Next
                                                    End SyncLock
                                                    Thread.Sleep(1000)
                                                End While
                                            End If
                                        End If

                                        If (ConfigReporteDataTable IsNot Nothing) Then ConfigReporteDataTable.Dispose()
                                        If (DTResultReportReporte IsNot Nothing) Then DTResultReportReporte.Dispose()
                                    Catch ex As Exception
                                        EscribeLog(Me._StrArchivoLog, "Error Generacion Reportes " + ex.Message, False, True)
                                    End Try

                                    EscribeLog(_StrArchivoLog, "Reporte " + ConfigReporteDataTable(0).Nombre_Reporte.ToString() + " finalizado.", False, True)
                                    InsertarExportacion(Item.Key, Date.Now, pathDelta.Replace("\\", "\"))
                                Else 'OTROS REPORTES
                                    If (Item.Value.ToUpper() <> "DELTA_CONSTRUCTOR") Then
                                        DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_SeleccionaReporte_2.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), Item.Key, Item.Value, CInt(ProcesoDesktopComboBox.SelectedValue), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)
                                        If (Item.Value.ToUpper() = "DOE_TAPAS") Then
                                            If (DTResultReport.Rows.Count > 0) Then
                                                ArrayNotificacionTapas = New ArrayList

                                                PathDoeTapas = Me._rutaStartProcess + "\" + ConfigReporteDataTable(0).Carpeta_Generacion

                                                Dim camposTapaContenedor = dbmImaging.SchemaConfig.TBL_Contenedor_Campo.DBGet(Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, Nothing)
                                                Dim JornadasBD = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Jornada.DBGet(Nothing).ToList()
                                                Dim CampoOficina As String = ""
                                                Dim CampoProceso As String = ""
                                                Dim CampoFechaApertura As String = ""
                                                Dim CampoJornada As String = ""

                                                If (camposTapaContenedor.Rows.Count > 0) Then
                                                    CampoOficina = "Campo_" + camposTapaContenedor.ToList().Where(Function(x) x.Nombre_Campo.ToUpper() = "OFICINA").Select(Function(X) X.id_Campo).ToList().FirstOrDefault().ToString()
                                                    CampoProceso = "Campo_" + camposTapaContenedor.ToList().Where(Function(x) x.Nombre_Campo.ToUpper() = "PROCESO").Select(Function(X) X.id_Campo).ToList().FirstOrDefault().ToString()
                                                    CampoFechaApertura = "Campo_" + camposTapaContenedor.ToList().Where(Function(x) x.Nombre_Campo.ToUpper() = "FECHA_APERTURA").Select(Function(X) X.id_Campo).ToList().FirstOrDefault().ToString()
                                                    CampoJornada = "Campo_" + camposTapaContenedor.ToList().Where(Function(x) x.Nombre_Campo.ToUpper() = "JORNADA").Select(Function(X) X.id_Campo).ToList().FirstOrDefault().ToString()

                                                    Dim Procesos As List(Of Object) = Nothing
                                                    Procesos = (From a In DTResultReport Group a By groupDt = a.Field(Of Integer)("fk_Tipo_Proceso") Into Group Select Group.Select(Function(x) x("fk_Tipo_Proceso")).First()).ToList()

                                                    Dim Count_Process_Tapa As Integer = 0
                                                    ProgressForm.ValueProcess = Count_Process_Tapa
                                                    ProgressForm.MaxValueProcess = Procesos.Count

                                                    For k = 0 To Procesos.Count - 1
                                                        For Each Item2 In ProcesosDictionary
                                                            If Item2.Key = Procesos(k) Then
                                                                ProgressForm.Process = Item2.Value
                                                                Application.DoEvents()

                                                                Dim DTResultReportProceso = DTResultReport.Select("fk_Tipo_Proceso = " + Procesos(k).ToString()).CopyToDataTable
                                                                If DTResultReportProceso.Rows.Count > 0 Then
                                                                    Dim PathDoeTapasProceso As String
                                                                    PathDoeTapasProceso = PathDoeTapas + "\" + Item2.Value.Replace(" ", "_")
                                                                    If (Not Directory.Exists(PathDoeTapasProceso)) Then
                                                                        CrearDirectorio(PathDoeTapasProceso)
                                                                    End If

                                                                    Dim Jornadas As List(Of Object) = Nothing
                                                                    Jornadas = (From a In DTResultReportProceso Group a By groupDt = a.Field(Of String)(CampoJornada) Into Group Select Group.Select(Function(x) x(CampoJornada)).First()).ToList()
                                                                    If (Jornadas.Count > 0) Then
                                                                        For Each jornadaitem In Jornadas
                                                                            Dim jornada = jornadaitem.ToString()
                                                                            Dim encontradaJornada = JornadasBD.Where(Function(x) x.Nombre_Tipo_Jornada.ToUpper() = jornada.ToUpper()).ToList()
                                                                            Dim PathTapas As String
                                                                            If (encontradaJornada.Count > 0) Then
                                                                                PathTapas = PathDoeTapasProceso + "\" + jornada

                                                                                If (Not Directory.Exists(PathTapas)) Then
                                                                                    CrearDirectorio(PathTapas)
                                                                                End If
                                                                            Else
                                                                                PathTapas = PathDoeTapasProceso
                                                                            End If

                                                                            Dim filtro = CampoJornada + " = '" + jornada + "'"
                                                                            Dim DTResultReportProcesoJornada = DTResultReportProceso.Select(filtro).CopyToDataTable()

                                                                            Dim Ots As List(Of Object) = Nothing
                                                                            Ots = (From a In DTResultReportProcesoJornada Group a By groupDt = a.Field(Of Long)("fk_OT") Into Group Select Group.Select(Function(x) x("fk_OT")).First()).ToList()
                                                                            If (Ots.Count > 0) Then
                                                                                For Each itemOt In Ots
                                                                                    servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Convert.ToInt32(itemOt))(0).ToCTA_ServidorSimpleType()
                                                                                    centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                                                                    Dim dtResulFiltradaProceso_OT = DTResultReportProceso.Select("fk_OT =" + itemOt.ToString()).CopyToDataTable
                                                                                    Dim ArraListParametersTapas As ArrayList = New ArrayList

                                                                                    ArraListParametersTapas.Add(servidor)
                                                                                    ArraListParametersTapas.Add(centro)
                                                                                    ArraListParametersTapas.Add(dtResulFiltradaProceso_OT)
                                                                                    ArraListParametersTapas.Add(Compresion)
                                                                                    ArraListParametersTapas.Add(PathTapas)
                                                                                    ArraListParametersTapas.Add(ArrayNotificacionTapas.Count)
                                                                                    ArraListParametersTapas.Add(CampoOficina)
                                                                                    ArraListParametersTapas.Add(CampoFechaApertura)

                                                                                    SyncLock BloqueoConcurrenciaTapas
                                                                                        ArrayNotificacionTapas.Add(False)
                                                                                    End SyncLock

                                                                                    Dim NewThread As New Thread(AddressOf TapasHilos)
                                                                                    NewThread.Start(ArraListParametersTapas)
                                                                                Next
                                                                            End If
                                                                        Next
                                                                    End If
                                                                End If

                                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                                                Count_Process_Tapa += 1
                                                                ProgressForm.ValueProcess = Count_Process_Tapa
                                                            End If
                                                        Next
                                                    Next
                                                Else
                                                    EscribeLog(Me._StrArchivoLog, "Error Generacion Reportes: No hay información para el nombre de las tapas ", False, True)
                                                End If

                                                Dim SalirTapas As Boolean = False
                                                While SalirTapas = False
                                                    SalirTapas = True
                                                    SyncLock BloqueoConcurrenciaTapas
                                                        For Each HiloTerminado As Boolean In ArrayNotificacionTapas
                                                            If HiloTerminado = False Then
                                                                SalirTapas = False
                                                            End If
                                                        Next
                                                    End SyncLock
                                                    Thread.Sleep(10000)
                                                End While
                                            Else
                                                EscribeLog(Me._StrArchivoLog, "Error Generacion Reportes: No hay registros para el reporte " + Item.Value, False, True)
                                            End If
                                        Else
                                            GenerarReporte(ConfigReporteDataTable)
                                        End If
                                    End If
                                End If

                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                Count_Action += 1
                                ProgressForm.ValueAction = Count_Action
                            Next

                            'Comprimir
                            Dim BorrarZip As Boolean = False

                            BorrarZip = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "BorrarZip")(0).Valor_Parametro_Sistema

                            ComprimirArchivos(Count_Action, BorrarZip)

                            'Eliminar imagenes de carpeta temporal 2.0
                            If Directory.Exists(CarpetaTempImagenes) Then
                                EscribeLog(_StrArchivoLog, "Eliminación de imágenes carpeta temporal: " + CarpetaTempImagenes + " iniciada.", False, True)
                                Directory.Delete(CarpetaTempImagenes, True)
                                EscribeLog(_StrArchivoLog, "Eliminación de imágenes carpeta temporal: " + CarpetaTempImagenes + " terminada.", False, True)
                            End If


                            'Fin Reporte
                            EscribeLog(_StrArchivoLog, "FIN LOG DE REPORTE PARA FECHA DE PROCESO: " + Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), False, True)
                            MessageBox.Show("Proceso Finalizado, favor revisar log", "Generar Reportes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        End If
                    Catch ex As Exception
                        EscribeLog(Me._StrArchivoLog, "Error Generacion Reportes " + ex.Message, False, True)
                        MessageBox.Show(ex.Message.ToString(), "Error Generación Reportes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Finally
                        ProgressForm.Visible = False
                        ProgressForm.Hide()

                        Me.Enabled = True
                        Me.Cursor = Cursors.Default

                        If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        If (DTResultReport IsNot Nothing) Then DTResultReport.Dispose()
                        If (dtPeriodicidad IsNot Nothing) Then dtPeriodicidad.Dispose()

                        If Directory.Exists(CarpetaTempImagenes) Then
                            Directory.Delete(CarpetaTempImagenes, True)
                        End If
                    End Try
                End If
            End If
        End Sub
#End Region

#Region " Métodos "
        Private Sub ArmarProceso(ByVal nid_Proceso As Integer, ByVal nNombre_Proceso As String, ByVal nConfigReporteDataTable As DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteDataTable, ByVal nCompresion As ImageManager.EnumCompression)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim DTResultReportProceso = DTResultReport.Select("fk_Tipo_Proceso = " + nid_Proceso.ToString()).CopyToDataTable
                If DTResultReportProceso.Rows.Count > 0 Then

                    Dim Cajas As List(Of Object) = Nothing
                    Cajas = (From a In DTResultReportProceso Group a By groupDt = a.Field(Of String)("Caja") Into Group Select Group.Select(Function(x) x("Caja")).First()).ToList()

                    For i = 0 To Cajas.Count - 1
                        Dim nameProceso = nNombre_Proceso.Replace(" ", "_")
                        Dim pathDeltaImgxCaja As String = ""
                        Dim pathFinalImg_0 As String = ""

                        ContadorDeltaImagen += 1
                        DateTimeGeneracionNombreArchivo = DateTimeNombreArchivo + CStr(ContadorDeltaImagen).PadLeft(4, "0"c)
                        pathDeltaImgxCaja = pathDeltaImg + "\DELTAIMAGEN_" + DateTimeGeneracionNombreArchivo + "_" + Cajas(i)
                        pathFinalImg_0 = pathDeltaImgxCaja + "\" + nameProceso + "\" + Cajas(i)
                        If (Not Directory.Exists(pathFinalImg_0)) Then
                            CrearDirectorio(pathFinalImg_0)
                        End If

                        Dim dtResulFiltradaCaja = DTResultReportProceso.Select("Caja =" + Cajas(i).ToString()).CopyToDataTable
                        Dim Ots As List(Of Object) = Nothing
                        Ots = (From a In dtResulFiltradaCaja Group a By groupDt = a.Field(Of Integer)("Imaging_fk_OT") Into Group Select Group.Select(Function(x) x("Imaging_fk_OT")).First()).ToList()
                        If (Ots.Count > 0) Then
                            For Each itemOt In Ots
                                servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Convert.ToInt32(itemOt))(0).ToCTA_ServidorSimpleType()
                                centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                Dim dtResulFiltradaCajaOT = dtResulFiltradaCaja.Select("Imaging_fk_OT =" + itemOt.ToString()).CopyToDataTable

                                Dim ArraListParameters As ArrayList = New ArrayList

                                ArraListParameters.Add(servidor)
                                ArraListParameters.Add(centro)
                                ArraListParameters.Add(dtResulFiltradaCajaOT)
                                ArraListParameters.Add(nCompresion)
                                ArraListParameters.Add(ArrayNotificacion.Count)
                                ArraListParameters.Add(pathFinalImg_0)
                                ArraListParameters.Add(pathDeltaImgxCaja)


                                SyncLock BloqueoConcurrencia
                                    ArrayNotificacion.Add(False)
                                End SyncLock

                                Dim NewThread As New Thread(AddressOf ProcesoHilos)
                                NewThread.Start(ArraListParameters)
                            Next
                        End If
                    Next
                End If
            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error Generacion Reportes " + ex.Message, False, True)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub TapasHilos(ByVal objectArray As Object)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing
            Dim ArraListParametersTapas As ArrayList = objectArray

            Dim nservidor As DBImaging.SchemaCore.CTA_ServidorSimpleType = ArraListParametersTapas(0)
            Dim ncentro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = ArraListParametersTapas(1)
            Dim ndtResulFiltradaProcesoJornadaOT As DataTable = ArraListParametersTapas(2)
            Dim nCompresion As Slyg.Tools.Imaging.ImageManager.EnumCompression = ArraListParametersTapas(3)
            Dim nPathTapas As String = ArraListParametersTapas(4)
            Dim Hilo_Indice_Tapas As Integer = ArraListParametersTapas(5)
            Dim nCampoOficina As String = ArraListParametersTapas(6)
            Dim nCampoFechaApertura As String = ArraListParametersTapas(7)

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                manager = New FileProviderManager(nservidor, ncentro, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                manager.Connect()

                For i = 0 To ndtResulFiltradaProcesoJornadaOT.Rows.Count - 1
                    Dim FileNameTapa = ndtResulFiltradaProcesoJornadaOT(i)(nCampoFechaApertura).ToString().Replace("/", "") + "_" + ndtResulFiltradaProcesoJornadaOT(i)(nCampoOficina).ToString() + "_" + ndtResulFiltradaProcesoJornadaOT(i)("fk_Cargue").ToString() + "_" + ndtResulFiltradaProcesoJornadaOT(i)("fk_Cargue_Paquete").ToString()
                    ExportarImagenTapa(manager, FileNameTapa, nCompresion, nPathTapas, ndtResulFiltradaProcesoJornadaOT.Rows(i)("fk_Anexo").ToString())
                Next

                EscribeLog(_StrArchivoLog, "Exportacion de imagenes del hilo de tapas con ruta: " + nPathTapas + " terminada.", False, True)

            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error Proceso Tapas Hilos: " + nPathTapas + " " + ex.Message, False, True)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()

                SyncLock BloqueoConcurrenciaTapas
                    ArrayNotificacionTapas(Hilo_Indice_Tapas) = True
                End SyncLock
            End Try
        End Sub

        Private Sub ProcesoHilos(ByVal objectArray As Object)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Dim ArraListParameters As ArrayList = objectArray

            Dim nservidor As DBImaging.SchemaCore.CTA_ServidorSimpleType = ArraListParameters(0)
            Dim ncentro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = ArraListParameters(1)
            Dim ndtResulFiltradaCajaOT As DataTable = ArraListParameters(2)
            Dim nCompresion As Slyg.Tools.Imaging.ImageManager.EnumCompression = ArraListParameters(3)
            Dim Hilo_Indice As Integer = ArraListParameters(4)
            Dim npathFinalImg_0 As String = ArraListParameters(5)
            Dim npathFinalImgxCaja As String = ArraListParameters(6)

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                For i = 0 To ndtResulFiltradaCajaOT.Rows.Count - 1

                    manager = New FileProviderManager(nservidor, ncentro, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                    manager.Connect()

                    Dim fk_Anexo As Long = 0
                    If (ndtResulFiltradaCajaOT.Rows(i)("Es_Anexo")) Then
                        fk_Anexo = CType(ndtResulFiltradaCajaOT.Rows(i)("fk_Anexo"), Long)
                    End If

                    ExportarImagen(manager, CType(ndtResulFiltradaCajaOT.Rows(i)("fk_Expediente"), Long), CType(ndtResulFiltradaCajaOT.Rows(i)("fk_Folder"), Short), CType(ndtResulFiltradaCajaOT.Rows(i)("fk_File"), Short), CType(ndtResulFiltradaCajaOT.Rows(i)("Rotulo"), String), nCompresion, npathFinalImg_0, fk_Anexo)

                    If (manager IsNot Nothing) Then manager.Disconnect()
                Next

                EscribeLog(_StrArchivoLog, "Exportacion de imagenes del hilo con ruta: " + npathFinalImg_0 + " terminada.", False, True)

            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error Proceso Hilos: " + npathFinalImg_0 + " " + ex.Message, False, True)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()

                SyncLock BloqueoConcurrencia
                    ArrayNotificacion(Hilo_Indice) = True
                End SyncLock
            End Try
        End Sub

        Private Sub CierraConexionIntegration(dbmIntegration As DBIntegration.DBIntegrationDataBaseManager)
            If (dbmIntegration.DataBase.Connection.State = ConnectionState.Open) Then
                dbmIntegration.Connection_Close()
            End If
        End Sub

        Private Sub AbreConexionIntegration(dbmIntegration As DBIntegration.DBIntegrationDataBaseManager, Usuario As Int32)
            If (dbmIntegration.DataBase.Connection.State = ConnectionState.Closed) Then
                dbmIntegration.Connection_Open(Usuario)
            End If
        End Sub

        Private Sub CargaTablas()
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try
                AbreConexionIntegration(dbmIntegration, _Plugin.Manager.Sesion.Usuario.id)

                _Reportes = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBFindByid_ReporteVigente(Nothing, True).OrderBy(Function(x) x("id_Reporte")).CopyToDataTable()

                'Crear nueva variable de Reportes con solo Reportes delta.
                _Reportes_Delta = (From Row In _Reportes.AsEnumerable() Where Row("Nombre_Reporte").ToString().ToUpper.Contains("DELTA") Select Row).CopyToDataTable()

                _Procesos = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByActivoAplica_Tipo_Proceso(True, True)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargaTablas", ex)
                Me._ltErroresReporte.Add("CargaTablas" + ex.Message)
            Finally
                CierraConexionIntegration(dbmIntegration)
            End Try
        End Sub

        Private Sub Cargar_Reportes()
            Try
                Cargar_Combo_Reportes()

                Utilities.LlenarCombo(ProcesoDesktopComboBox, _Procesos, DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoEnum.id_Tipo_Proceso.ColumnName, DBIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_ProcesoEnum.Nombre_Tipo_Proceso.ColumnName, True, "-1", "--TODOS--")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargar_Reportes", ex)
                Me._ltErroresReporte.Add("Cargar_Reportes" + ex.Message)
            End Try
        End Sub

        Private Sub Cargar_Combo_Reportes()
            'Si se Selecciona Check Archivo Rotulos, Al seleccionar Poner unicamente Reportes Delta, al deseleccionar poner Todos los reportes nuevamente. 
            If Not ArchivoRotulosCheckBox.Checked Then
                Utilities.LlenarCombo(ReporteDesktopComboBox, _Reportes, DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteEnum.id_Reporte.ColumnName, DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteEnum.Nombre_Reporte.ColumnName, True, "-1", "--TODOS--")
            Else
                Utilities.LlenarCombo(ReporteDesktopComboBox, _Reportes_Delta, DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteEnum.id_Reporte.ColumnName, DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteEnum.Nombre_Reporte.ColumnName, False, "", "")
            End If

        End Sub

        Private Sub EscribeLog(pathStrFile As String, StrLine As String, Optional CrearFile As Boolean = False, Optional LeerFile As Boolean = False)
            Dim modeFile As FileMode = Nothing
            Dim modeFile_2 As FileMode = Nothing
            Dim strMessageComplete As String = StrLine + Environment.NewLine
            Try
                If (CrearFile) Then
                    modeFile = FileMode.CreateNew
                    modeFile_2 = FileAccess.Write
                    Using fs As New FileStream(pathStrFile, modeFile)
                        Using w As New BinaryWriter(fs)
                            w.Write("Date : " + Date.Now.ToString() + " " + strMessageComplete)
                        End Using
                    End Using
                ElseIf (LeerFile) Then
                    modeFile = FileMode.Append
                    Using fs As New FileStream(pathStrFile, modeFile)
                        Using w As New BinaryWriter(fs)
                            w.Write(" Date : " + Date.Now.ToString() + " " + strMessageComplete)
                        End Using
                    End Using
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub CrearDirectorio(path As String, Optional Elimina As Boolean = True)
            Try
                If (Not Directory.Exists(path)) Then
                    Directory.CreateDirectory(path)
                Else
                    If (Elimina) Then
                        Directory.Delete(path, Elimina)
                    End If
                    Directory.CreateDirectory(path)
                End If
            Catch ex As Exception
                Dim st = New StackTrace(ex, True)
                Dim frame = st.GetFrame(0)
                Dim line = frame.GetFileLineNumber()
                EscribeLog(Me._StrArchivoLog, "Error, CrearDirectorio " + ex.Message + " - " + DateTime.Now + " Linea Error: " + line.ToString(), False, True)
            End Try
        End Sub

        Private Sub DeshabilitaControles(ByVal opcion As Boolean)
            Me.dtpFechaProceso.Enabled = opcion
            Me.ReporteDesktopComboBox.Enabled = opcion
            Me.ProcesoDesktopComboBox.Enabled = opcion
            Me.RutaTextBox.Enabled = opcion
            Me.SelectFolderButton.Enabled = opcion
            Me.GenerarButton.Enabled = opcion
            Me._horaDeltaReport = 0
            Me._minutosDeltaReport = 0
            Me._segundosDeltaReport = 0
        End Sub

        Private Sub Habilitar_Controles()
            'Habilitar/Deshabilitar textbox y boton seleccionar rotulo
            ArchivoRotulosTextBox.Enabled = ArchivoRotulosCheckBox.Checked
            ArchivoRotulosSelectButton.Enabled = ArchivoRotulosCheckBox.Checked
        End Sub

        Private Sub SelectFolderPath()
            Dim LectorFolderBrowserDialog = New FolderBrowserDialog()
            Dim Respuesta As DialogResult

            LectorFolderBrowserDialog.SelectedPath = RutaTextBox.Text
            LectorFolderBrowserDialog.ShowNewFolderButton = False
            LectorFolderBrowserDialog.Description = "Seleccione la carpeta"

            Respuesta = LectorFolderBrowserDialog.ShowDialog()

            If (Respuesta = DialogResult.OK) Then
                RutaTextBox.Text = LectorFolderBrowserDialog.SelectedPath
            End If
        End Sub

        Private Sub ExportarImagen(nManager As FileProviderManager, ByVal nfk_Expediente As Long, ByVal nfk_Folder As Short, ByVal nfk_File As Short, ByVal nFileName As String, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, ByVal nfk_Anexo As Long)
            Dim FileNames As New List(Of String)
            Dim FileNamePending As String = Nothing
            Dim FileNameWrite As String = Nothing
            Dim Folios As Short
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                If nfk_Anexo = 0 Then
                    Folios = nManager.GetFolios(nfk_Expediente, nfk_Folder, nfk_File, 1)
                Else
                    Folios = nManager.GetFolios(nfk_Anexo)
                End If

                If Folios > 0 Then
                    Dim ImageSize As Long = 0
                    Dim Contador As Integer = 1
                    Dim FileNamesLimit As New List(Of String)
                    For folio As Short = 1 To Folios

                        Dim Imagen() As Byte = Nothing
                        Dim Thumbnail() As Byte = Nothing

                        If nfk_Anexo = 0 Then
                            nManager.GetFolio(nfk_Expediente, nfk_Folder, nfk_File, 1, folio, Imagen, Thumbnail)
                        Else
                            nManager.GetFolio(nfk_Anexo, folio, Imagen, Thumbnail)
                        End If

                        FileNamePending = CarpetaTempImagenes & "\" & Guid.NewGuid().ToString() & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                        FileNames.Add(FileNamePending)


                        FileNamesCons.Add(FileNamePending)

                        Using fs = New FileStream(FileNamePending, FileMode.Create)
                            fs.Write(Imagen, 0, Imagen.Length)
                            fs.Close()
                        End Using

                        Dim objDeltaFraccionFileImagen As New DBIntegration.SchemaBCSCarpetaUnica.TBL_Report_Delta_Fraccion_File_ImagenType

                        If ImageLimit Then
                            If folio = Folios And Contador = 1 And ImageSize = 0 Then
                                FileNameWrite = nFileFolderName & "\" & nFileName.ToString() & "_" & CStr(nfk_File).PadLeft(5, "0"c) & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida

                                '-------------------------------------------------------------------------
                                ImageManager.Save(FileNames, FileNameWrite, ".tiff", EnumFormat.Tiff, nCompresion, False, nFileFolderName, True)
                                '-------------------------------------------------------------------------
                            Else
                                If ImageSize = 0 Then
                                    If Imagen.Length >= ImageSizeLimit Then
                                        FileNamesLimit.Add(FileNamePending)
                                        FileNameWrite = nFileFolderName & "\" & nFileName.ToString() & "_" & CStr(nfk_File).PadLeft(5, "0"c) & "_" & CStr(Contador).PadLeft(5, "0"c) & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida

                                        '-------------------------------------------------------------------------
                                        ImageManager.Save(FileNamesLimit, FileNameWrite, ".tiff", EnumFormat.Tiff, nCompresion, False, nFileFolderName, True)
                                        '-------------------------------------------------------------------------

                                        objDeltaFraccionFileImagen.fk_Expediente = nfk_Expediente
                                        objDeltaFraccionFileImagen.fk_Folder = nfk_Folder
                                        objDeltaFraccionFileImagen.fk_File = nfk_File
                                        objDeltaFraccionFileImagen.id_Fraccion = Contador
                                        objDeltaFraccionFileImagen.Folios_Fraccion = folio

                                        dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Delta_Fraccion_File_Imagen.DBInsert(objDeltaFraccionFileImagen)

                                        FileNamesLimit = New List(Of String)
                                        Contador = Contador + 1
                                    Else
                                        ImageSize = ImageSize + Imagen.Length
                                        FileNamesLimit.Add(FileNamePending)
                                    End If
                                Else
                                    If ImageSize + Imagen.Length >= ImageSizeLimit Then
                                        FileNameWrite = nFileFolderName & "\" & nFileName.ToString() & "_" & CStr(nfk_File).PadLeft(5, "0"c) & "_" & CStr(Contador).PadLeft(5, "0"c) & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida

                                        '-------------------------------------------------------------------------
                                        ImageManager.Save(FileNamesLimit, FileNameWrite, ".tiff", EnumFormat.Tiff, nCompresion, False, nFileFolderName, True)
                                        '-------------------------------------------------------------------------

                                        objDeltaFraccionFileImagen.fk_Expediente = nfk_Expediente
                                        objDeltaFraccionFileImagen.fk_Folder = nfk_Folder
                                        objDeltaFraccionFileImagen.fk_File = nfk_File
                                        objDeltaFraccionFileImagen.id_Fraccion = Contador
                                        objDeltaFraccionFileImagen.Folios_Fraccion = folio

                                        dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Delta_Fraccion_File_Imagen.DBInsert(objDeltaFraccionFileImagen)

                                        FileNamesLimit = New List(Of String)
                                        FileNamesLimit.Add(FileNamePending)
                                        Contador = Contador + 1
                                        ImageSize = Imagen.Length
                                    Else
                                        FileNamesLimit.Add(FileNamePending)
                                        ImageSize = ImageSize + Imagen.Length
                                    End If
                                End If
                            End If
                        End If

                        If folio = Folios And FileNamesLimit.Count > 0 Then
                            If Contador = 1 Then
                                FileNameWrite = nFileFolderName & "\" & nFileName.ToString() & "_" & CStr(nfk_File).PadLeft(5, "0"c) & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                            Else
                                FileNameWrite = nFileFolderName & "\" & nFileName.ToString() & "_" & CStr(nfk_File).PadLeft(5, "0"c) & "_" & CStr(Contador).PadLeft(5, "0"c) & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida

                                objDeltaFraccionFileImagen.fk_Expediente = nfk_Expediente
                                objDeltaFraccionFileImagen.fk_Folder = nfk_Folder
                                objDeltaFraccionFileImagen.fk_File = nfk_File
                                objDeltaFraccionFileImagen.id_Fraccion = Contador
                                objDeltaFraccionFileImagen.Folios_Fraccion = folio

                                dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Delta_Fraccion_File_Imagen.DBInsert(objDeltaFraccionFileImagen)
                            End If


                            '-------------------------------------------------------------------------
                            ImageManager.Save(FileNamesLimit, FileNameWrite, ".tiff", EnumFormat.Tiff, nCompresion, False, nFileFolderName, True)
                            '-------------------------------------------------------------------------
                        End If
                    Next

                    If Not ImageLimit Then
                        FileNameWrite = nFileFolderName & "\" & nFileName.ToString() & "_" & CStr(nfk_File).PadLeft(5, "0"c) & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida

                        '-------------------------------------------------------------------------
                        ImageManager.Save(FileNames, FileNameWrite, ".tiff", EnumFormat.Tiff, nCompresion, False, nFileFolderName, True)
                        '-------------------------------------------------------------------------
                    End If
                Else
                    EscribeLog(Me._StrArchivoLog, "La imagen del expediente, folder, file: " + nfk_Expediente.ToString() + ", " + nfk_Folder.ToString() + ", " + nfk_File.ToString() + " no existe.", False, True)
                End If
            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Exportar imagen " + nFileFolderName + " - " + nfk_Expediente.ToString() + ", " + nfk_Folder.ToString() + ", " + nfk_File.ToString() + " mensaje: " + ex.ToString(), False, True)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub ExportarImagenTapa(nManager As FileProviderManager, ByVal nFileName As String, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, ByVal nfk_Anexo As Long)
            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim Folios As Short

            Try
                Folios = nManager.GetFolios(nfk_Anexo)
                If Folios > 0 Then
                    For folio As Short = 1 To Folios
                        Dim Imagen() As Byte = Nothing
                        Dim Thumbnail() As Byte = Nothing

                        nManager.GetFolio(nfk_Anexo, folio, Imagen, Thumbnail)

                        FileName = CarpetaTempImagenes & "\" & Guid.NewGuid().ToString() & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                        FileNames.Add(FileName)

                        FileNamesCons.Add(FileName)

                        Using fs = New FileStream(FileName, FileMode.Create)
                            fs.Write(Imagen, 0, Imagen.Length)
                            fs.Close()
                        End Using
                    Next

                    FileName = nFileFolderName & "\" & nFileName.ToString() & "_" & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida

                    '-------------------------------------------------------------------------
                    ImageManager.Save(FileNames, FileName, ".tiff", EnumFormat.Tiff, nCompresion, False, nFileFolderName, True)
                    '-------------------------------------------------------------------------
                Else
                    EscribeLog(Me._StrArchivoLog, "La imagen de la tapa con fk_Anexo " + nfk_Anexo.ToString() + " no existe.", False, True)
                End If
            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Exportar imagen Tapa " + ex.ToString(), False, True)
            End Try
        End Sub

        Private Sub SeleccionExtensionGenerar(ByVal DTExtensionGenerar As DataTable, ByVal RutaGenerar As String, ByVal NombreReporteArchivo As String, ByVal DTResultReport As DataTable, nid_Reporte As Integer, nReporte As String)
            Dim eliminaFolders As Boolean = True
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                If (DTExtensionGenerar.Rows.Count > 0) Then

                    Dim extensiones As New List(Of String)
                    Dim generaConTAB As Boolean = True
                    If NombreReporteArchivo.Contains("DOE18") Or NombreReporteArchivo.Contains("FATCA") Then
                        generaConTAB = False
                    End If

                    For Each itemExtension As DataRow In DTExtensionGenerar.Rows
                        Dim fk_Reporte = itemExtension("id_Reporte").ToString()
                        If (Convert.ToBoolean(itemExtension("Aplica_TXT"))) Then
                            extensiones.Add(".txt")

                            If (nReporte = "FATCA") Then
                                Dim DTFatca = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_FATCA.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), nid_Reporte, "TXT", nReporte + ".txt", CType(Me.ProcesoDesktopComboBox.SelectedValue.ToString(), Integer))
                                eliminaFolders = False
                                If (Genera_ReporteArchivoPlano(RutaGenerar, ".txt", NombreReporteArchivo, DTFatca, eliminaFolders, generaConTAB)) Then
                                    EscribeLog(_StrArchivoLog, "Reporte " + NombreReporteArchivo + " TXT generado con exito en la ruta " + RutaGenerar, False, True)
                                End If
                                GoTo Segunda_Validacion
                            End If


                            If (nReporte = "SIPLA") Then
                                extensiones.Add(".dat")
                                If (Genera_ReporteArchivoPlano(RutaGenerar, ".dat", NombreReporteArchivo, DTResultReport, eliminaFolders, generaConTAB)) Then
                                    EscribeLog(_StrArchivoLog, "Reporte " + NombreReporteArchivo + " TXT generado con exito en la ruta " + RutaGenerar, False, True)
                                End If
                            ElseIf (nReporte = "GORO") Then
                                Dim AUXDT_Acu = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_GORO_ACUSE_RECIBIDO.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), nid_Reporte)
                                AUXDT_Acu.Columns.Remove("FechaProceso")
                                Dim NombreReporte_ACU = "7089-N9V9P9E3-" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + Date.Now.Hour.ToString("00") + "0000"
                                Dim RutaGenerar_ACU = Me._rutaStartProcess + "\GORO\"
                                If Not Directory.Exists(RutaGenerar_ACU) Then
                                    CrearDirectorio(RutaGenerar_ACU)
                                End If

                                'Acuses
                                If (Genera_ReporteArchivoPlano(RutaGenerar_ACU, ".txt", NombreReporte_ACU, AUXDT_Acu, eliminaFolders)) Then
                                    EscribeLog(_StrArchivoLog, "Reporte " + NombreReporte_ACU + " TXT generado con exito en la ruta " + RutaGenerar, False, True)
                                End If

                                'General
                                Dim auxDt_General = DTResultReport.AsEnumerable().Where(Function(x) CBool(x("Aplica_Goro")) = True)
                                If auxDt_General.Count > 0 Then
                                    Dim FinalDt = auxDt_General.CopyToDataTable()
                                    NombreReporteArchivo = NombreReporteArchivo + Date.Now.Hour.ToString("00") + "0000"
                                    If (Genera_ReporteArchivoPlano(RutaGenerar, ".txt", NombreReporteArchivo, FinalDt, eliminaFolders)) Then
                                        EscribeLog(_StrArchivoLog, "Reporte " + NombreReporteArchivo + " TXT generado con exito en la ruta " + RutaGenerar, False, True)
                                    End If
                                End If
                            ElseIf ((nReporte = "DELTA") Or (nReporte = "DELTA_EMPRESARIAL")) Then 'DELTA Y DELTA EMPRESARIAL
                                If (nReporte = "DELTA") Then
                                    eliminaFolders = False
                                End If
                                If (Genera_ReporteArchivoPlano(RutaGenerar, ".txt", NombreReporteArchivo, DTResultReport, eliminaFolders, generaConTAB)) Then
                                    EscribeLog(_StrArchivoLog, "Reporte " + NombreReporteArchivo + " TXT generado con exito en la ruta " + RutaGenerar, False, True)
                                End If
                            Else
                                If nReporte = "GORO_VOLUMEN" Then
                                    NombreReporteArchivo = NombreReporteArchivo + Date.Now.Hour.ToString("00") + "0000"
                                End If
                                If (Genera_ReporteArchivoPlano(RutaGenerar, ".txt", NombreReporteArchivo, DTResultReport, eliminaFolders, generaConTAB)) Then
                                    EscribeLog(_StrArchivoLog, "Reporte " + NombreReporteArchivo + " TXT generado con exito en la ruta " + RutaGenerar, False, True)
                                    InsertarExportacion(fk_Reporte, Date.Now, RutaGenerar)
                                End If
                            End If
                        End If

Segunda_Validacion:

                        If (Convert.ToBoolean(itemExtension("Aplica_Excel"))) Then

                            If (nReporte = "FATCA") Then
                                Dim DTFatca = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_FATCA.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), nid_Reporte, "XLS", NombreReporteArchivo + ".xls", CType(Me.ProcesoDesktopComboBox.SelectedValue.ToString(), Integer))
                                eliminaFolders = False
                                If (Genera_ReporteExcel(RutaGenerar, ".xlsx", NombreReporteArchivo, DTFatca, "FATCA")) Then
                                    EscribeLog(_StrArchivoLog, "Reporte " + NombreReporteArchivo + " XLS generado con exito en la ruta " + RutaGenerar, False, True)
                                End If
                                Return
                            End If

                            extensiones.Add(".xlsx")

                            If Genera_ReporteExcel(RutaGenerar, ".xlsx", NombreReporteArchivo, DTResultReport, itemExtension("Nombre_Reporte").ToString()) Then
                                EscribeLog(_StrArchivoLog, "Reporte " + NombreReporteArchivo + " generado con exito en la ruta " + RutaGenerar, False, True)
                                InsertarExportacion(fk_Reporte, Date.Now, RutaGenerar)
                            End If

                            If (nReporte = "INCONSISTENCIAS") Then
                                Dim fechaInicial = (CDate(Me.dtpFechaProceso.Value).AddDays(-30)).ToString("yyyyMMdd")
                                Dim fechaFinal = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")
                                Dim dtHis_inc = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_HISTORICOS_INC_NOV.DBExecute(fechaInicial, fechaFinal, nid_Reporte, CType(Me.ProcesoDesktopComboBox.SelectedValue.ToString(), Integer))

                                Dim NombreReporte_Inconsistencias_Historico = "INCONSISTENCIAS_H_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd")

                                If (Genera_ReporteExcel(RutaGenerar, ".xlsx", NombreReporte_Inconsistencias_Historico, dtHis_inc, itemExtension("Nombre_Reporte").ToString())) Then
                                    EscribeLog(_StrArchivoLog, "Reporte " + NombreReporte_Inconsistencias_Historico + " generado con exito en la ruta " + RutaGenerar, False, True)
                                End If
                            End If

                            If (nReporte = "NOVEDADES") Then
                                Dim fechaInicial = (CDate(Me.dtpFechaProceso.Value).AddDays(-30)).ToString("yyyyMMdd")
                                Dim fechaFinal = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")
                                Dim dtHis_nov = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_HISTORICOS_INC_NOV.DBExecute(fechaInicial, fechaFinal, nid_Reporte, CType(Me.ProcesoDesktopComboBox.SelectedValue.ToString(), Integer))

                                Dim NombreReporte_Novedades_Historico = "NOVEDADES_H_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd")

                                If (Genera_ReporteExcel(RutaGenerar, ".xlsx", NombreReporte_Novedades_Historico, dtHis_nov, itemExtension("Nombre_Reporte").ToString())) Then
                                    EscribeLog(_StrArchivoLog, "Reporte " + NombreReporte_Novedades_Historico + " generado con exito en la ruta " + RutaGenerar, False, True)
                                End If
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                EscribeLog(_StrArchivoLog, "Error, SeleccionExtensionGenerar " + ex.Message, False, True)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

        End Sub

        Private Sub InsertarExportacion(fk_Reporte As Integer, FechaExportacion As Date, Ruta As String)
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try
                AbreConexionIntegration(dbmIntegration, _Plugin.Manager.Sesion.Usuario.id)
                Dim ObjInsertarExportacion As New DBIntegration.SchemaBCSCarpetaUnica.TBL_Report_Log_ExportacionType
                ObjInsertarExportacion.id_reporte_Exportacion = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Log_Exportacion.DBNextId()
                ObjInsertarExportacion.fk_fecha_proceso = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")
                ObjInsertarExportacion.fk_Reporte = CInt(fk_Reporte)
                ObjInsertarExportacion.Fecha_Exportacion = FechaExportacion
                ObjInsertarExportacion.fk_Usuario = Me._Plugin.Manager.Sesion.Usuario.id
                ObjInsertarExportacion.IP_Exportacion = Me._Plugin.Manager.DesktopGlobal.ClientIpAddress
                ObjInsertarExportacion.Ruta_Exportacion = Ruta
                dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Log_Exportacion.DBInsert(ObjInsertarExportacion)
            Catch ex As Exception
                CierraConexionIntegration(dbmIntegration)
            End Try
        End Sub

        Public Function DevuelveNombreReporte(ByVal idTipoReporte As String, ByVal nDateTimeGeneracionNombreArchivo As String, Optional DTResultReport As DataTable = Nothing, Optional ByVal NombreZip As Boolean = False) As String
            Dim NombreReporte As String = ""
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

            Try
                AbreConexionIntegration(dbmIntegration, _Plugin.Manager.Sesion.Usuario.id)
                Dim extensiones = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBGet(CShort(idTipoReporte))
                Dim exts As String = ""
                If (extensiones.Rows.Count > 0) Then
                    If (extensiones.Rows(0)("Aplica_TXT")) Then
                        exts = ".txt"
                    Else
                        exts = ".xlsx"
                    End If
                End If
                If (NombreZip) Then
                    NombreReporte = String.Format(dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Formatos_Salida.DBFindByid_Formato_Salidafk_Reporte(Nothing, idTipoReporte).ToList().FirstOrDefault().Formato_Salida_ZIP, nDateTimeGeneracionNombreArchivo)
                Else
                    NombreReporte = String.Format(dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Formatos_Salida.DBFindByid_Formato_Salidafk_Reporte(Nothing, idTipoReporte).ToList().FirstOrDefault().Formato_Salida_Plano, nDateTimeGeneracionNombreArchivo)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("DevuelveNombreReporte", ex)
            Finally
                CierraConexionIntegration(dbmIntegration)
            End Try
            Return NombreReporte

        End Function

        Private Sub GenerarReporte(ByVal nReporteDatatable As DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteDataTable)
            Dim viewPeriodicidad As DataView
            Dim RutaReporte As String

            Try
                viewPeriodicidad = dtPeriodicidad.DefaultView

                viewPeriodicidad.RowFilter = "id_reporte = " & nReporteDatatable(0).id_Reporte &
                                            " And (Fecha_Dia = '" & Me.dtpFechaProceso.Value.ToString("yyyyMMdd") & "'" &
                                            " Or Fecha_Dia_Habil = '" & Me.dtpFechaProceso.Value.ToString("yyyyMMdd") & "'" &
                                            " Or Fecha_Semana = '" & Me.dtpFechaProceso.Value.ToString("yyyyMMdd") & "')"

                If viewPeriodicidad.Count > 0 Or ArchivoRotulosCheckBox.Checked Then
                    EscribeLog(Me._StrArchivoLog, "Exportando Reporte " + nReporteDatatable(0).Nombre_Reporte.ToString(), False, True)
                    RutaReporte = Me._rutaGenerar + "\SALIDA\" + nReporteDatatable(0).Carpeta_Generacion.ToString()
                    If Not Directory.Exists(RutaReporte) Then
                        CrearDirectorio(RutaReporte)
                    End If

                    If (nReporteDatatable(0).Nombre_Reporte.ToString() = "GORO") Then
                        If (DTResultReport.Columns.Count > 0 And DTResultReport.Columns.Contains("FechaProceso")) Then
                            DTResultReport.Columns.Remove("FechaProceso")
                        End If
                    End If

                    SeleccionExtensionGenerar(nReporteDatatable, RutaReporte & "\", DevuelveNombreReporte(nReporteDatatable(0).id_Reporte, Me.dtpFechaProceso.Value.ToString("yyyMMdd"), nReporteDatatable), DTResultReport, CInt(nReporteDatatable(0).id_Reporte.ToString()), nReporteDatatable(0).Nombre_Reporte.ToString())
                End If
            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error Generacion Reportes " + ex.Message, False, True)
            End Try
        End Sub

        Private Sub ComprimirArchivos(ByVal contador_action As Integer, ByVal nBorrarZip As Boolean)
            Dim DirectoryNames As String()
            DirectoryNames = Directory.GetDirectories(Me._rutaStartProcess)

            Dim ContadorProcess As Integer = 0
            Dim NombreDirectorio As String

            ProgressForm.Action = "COMPRIMIENDO..."
            ProgressForm.ValueProcess = ContadorProcess
            ProgressForm.MaxValueProcess = DirectoryNames.Count

            For Each Directorio In DirectoryNames
                NombreDirectorio = Directorio.Substring(Directorio.LastIndexOf("\"c) + 1)
                If NombreDirectorio = "DELTAIMAGEN" Then
                    ProgressForm.Process = "Carpeta DELTAIMAGEN"
                    Dim DirectoryDeltaImagen = Directory.GetDirectories(Directorio)
                    For Each DirectorioDeltaImagenitem In DirectoryDeltaImagen
                        ComprimrDeltaImagen(DirectorioDeltaImagenitem)
                    Next

                    Dim FilespgpDeltaImagen As String() = Directory.GetFiles(Directorio, "*.zip")

                    If FilespgpDeltaImagen.Length > 0 Then
                        EscribeLog(_StrArchivoLog, "Inicio de cifrado de la carpeta: " + Directorio, False, True)
                        Encriptar(FilespgpDeltaImagen, Directorio)
                        EscribeLog(_StrArchivoLog, "Cifrado de carpeta: " + Directorio + " finalizada.", False, True)
                    End If

                    If (nBorrarZip) Then
                        For Each ItemFilespgpDeltaImagen In FilespgpDeltaImagen
                            File.Delete(ItemFilespgpDeltaImagen)
                        Next
                    End If

                    If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                    ContadorProcess += 1
                    ProgressForm.ValueProcess = ContadorProcess

                    'Comprimir carpeta DELTA, ya que se creó al finalizar los DELTAIMAGEN
                    Dim RutaDelta As String = Me._rutaStartProcess + "\DELTA"
                    If Directory.Exists(RutaDelta) Then
                        ProgressForm.Process = "Carpeta DELTA"
                        Dim Archivo_Zip = RutaDelta + "\DELTA_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + "_" + Date.Now.Hour.ToString("00") + "0000.zip"
                        Dim FilesZip As String() = Directory.GetFiles(RutaDelta)

                        If FilesZip.Count > 0 Then
                            Using zip As ZipFile = New ZipFile()
                                EscribeLog(_StrArchivoLog, "Inicio de compresion de la carpeta: " + RutaDelta, False, True)
                                zip.AddFiles(FilesZip, False, "")
                                zip.Save(Archivo_Zip)
                                EscribeLog(_StrArchivoLog, "Compresion de carpeta: " + RutaDelta + " finalizada.", False, True)
                            End Using

                            For Each ItemFilesZip In FilesZip
                                File.Delete(ItemFilesZip)
                            Next

                            Dim FilespgpDelta As String() = Directory.GetFiles(RutaDelta, "*.zip")

                            If FilespgpDelta.Length > 0 Then
                                EscribeLog(_StrArchivoLog, "Inicio de cifrado de la carpeta: " + RutaDelta, False, True)
                                Encriptar(FilespgpDelta, RutaDelta)
                                EscribeLog(_StrArchivoLog, "Cifrado de carpeta: " + RutaDelta + " finalizada.", False, True)
                            End If

                            If (nBorrarZip) Then
                                For Each ItemFilespgpDelta In FilespgpDelta
                                    File.Delete(ItemFilespgpDelta)
                                Next
                            End If

                        End If
                    End If
                ElseIf NombreDirectorio = "INFORMES" Then
                    Dim DirectoryNamesInformes As String()
                    Dim NombreDirectorioInformes As String

                    DirectoryNamesInformes = Directory.GetDirectories(Directorio)
                    ProgressForm.Process = "Carpeta INFORMES"

                    For Each DirectorioInforme In DirectoryNamesInformes
                        NombreDirectorioInformes = DirectorioInforme.Substring(DirectorioInforme.LastIndexOf("\"c) + 1)
                        If NombreDirectorioInformes = "DOE_TAPAS" Then
                            Dim DirectoryNamesTapas As String()
                            DirectoryNamesTapas = Directory.GetDirectories(DirectorioInforme)
                            Dim Archivo_Zip_Tapas = DirectorioInforme + "\" + DirectorioInforme.Substring(DirectorioInforme.LastIndexOf("\"c) + 1) + "_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + Date.Now.Hour.ToString("00") + "0000.zip"

                            If DirectoryNamesTapas.Count > 0 Then
                                Using zip As ZipFile = New ZipFile()
                                    EscribeLog(_StrArchivoLog, "Inicio de compresion de la carpeta: " + DirectorioInforme, False, True)
                                    zip.AddDirectory(DirectorioInforme)
                                    zip.Save(Archivo_Zip_Tapas)
                                    EscribeLog(_StrArchivoLog, "Compresion de carpeta: " + DirectorioInforme + " finalizada.", False, True)
                                End Using

                                For Each ItemDirectoriesTapas In DirectoryNamesTapas
                                    Directory.Delete(ItemDirectoriesTapas, True)
                                Next

                                Dim FilespgpDoeTapas As String() = Directory.GetFiles(DirectorioInforme, "*.zip")

                                If FilespgpDoeTapas.Length > 0 Then
                                    EscribeLog(_StrArchivoLog, "Inicio de cifrado de la carpeta: " + DirectorioInforme, False, True)
                                    Encriptar(FilespgpDoeTapas, DirectorioInforme)
                                    EscribeLog(_StrArchivoLog, "Cifrado de carpeta: " + DirectorioInforme + " finalizada.", False, True)
                                End If

                                If (nBorrarZip) Then
                                    For Each ItemFilespgpDoeTapas In FilespgpDoeTapas
                                        File.Delete(ItemFilespgpDoeTapas)
                                    Next
                                End If
                            End If
                        Else
                            Dim Archivo_Zip_Carpetas_Informes = DirectorioInforme + "\" + DirectorioInforme.Substring(DirectorioInforme.LastIndexOf("\"c) + 1) + "_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + Date.Now.Hour.ToString("00") + "0000.zip"
                            Dim FilesCarpetasInformes As String() = Directory.GetFiles(DirectorioInforme)

                            If FilesCarpetasInformes.Count > 0 Then
                                Using zip As ZipFile = New ZipFile()
                                    EscribeLog(_StrArchivoLog, "Inicio de compresion de la carpeta: " + DirectorioInforme, False, True)
                                    zip.AddFiles(FilesCarpetasInformes, False, "")
                                    zip.Save(Archivo_Zip_Carpetas_Informes)
                                    EscribeLog(_StrArchivoLog, "Compresion de carpeta: " + DirectorioInforme + " finalizada.", False, True)
                                End Using

                                For Each ItemFilesCarpetasInformes In FilesCarpetasInformes
                                    File.Delete(ItemFilesCarpetasInformes)
                                Next

                                Dim FilespgpInformes As String() = Directory.GetFiles(DirectorioInforme, "*.zip")

                                If FilespgpInformes.Length > 0 Then
                                    EscribeLog(_StrArchivoLog, "Inicio de cifrado de la carpeta: " + DirectorioInforme, False, True)
                                    Encriptar(FilespgpInformes, DirectorioInforme)
                                    EscribeLog(_StrArchivoLog, "Cifrado de carpeta: " + DirectorioInforme + " finalizada.", False, True)
                                End If

                                If (nBorrarZip) Then
                                    For Each ItemFilespgpInformes In FilespgpInformes
                                        File.Delete(ItemFilespgpInformes)
                                    Next
                                End If
                            End If
                        End If
                    Next

                    Dim Archivo_Zip_Sobrantes = Directorio + "\SOBRANTES_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + Date.Now.Hour.ToString("00") + "0000.zip"
                    Dim FilesSobrantes As String() = Directory.GetFiles(Directorio, "SOBRANTES*")

                    If FilesSobrantes.Count > 0 Then
                        Using zip As ZipFile = New ZipFile()
                            EscribeLog(_StrArchivoLog, "Inicio de compresion de la carpeta: " + Directorio + " files SOBRANTES.", False, True)
                            zip.AddFiles(FilesSobrantes, False, "")
                            zip.Save(Archivo_Zip_Sobrantes)
                            EscribeLog(_StrArchivoLog, "Compresion de carpeta: " + Directorio + " files SOBRANTES finalizada.", False, True)
                        End Using

                        For Each ItemFilesSobrantes In FilesSobrantes
                            File.Delete(ItemFilesSobrantes)
                        Next

                        Dim FilespgpSobrantes As String() = Directory.GetFiles(Directorio, "*.zip")

                        If FilespgpSobrantes.Length > 0 Then
                            EscribeLog(_StrArchivoLog, "Inicio de cifrado de la carpeta: " + Directorio, False, True)
                            Encriptar(FilespgpSobrantes, Directorio)
                            EscribeLog(_StrArchivoLog, "Cifrado de carpeta: " + Directorio + " finalizada.", False, True)
                        End If

                        If (nBorrarZip) Then
                            For Each ItemFilespgpSobrantes In FilespgpSobrantes
                                File.Delete(ItemFilespgpSobrantes)
                            Next
                        End If
                    End If
                ElseIf NombreDirectorio = "GORO" Then
                    ProgressForm.Process = "Carpeta GORO"

                    Dim Archivo_Zip_Goro = Directorio + "\" + Directorio.Substring(Directorio.LastIndexOf("\"c) + 1) + "_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + Date.Now.Hour.ToString("00") + "0000.zip"
                    Dim FilesGoro As String() = Directory.GetFiles(Directorio)

                    If FilesGoro.Count > 0 Then
                        Using zip As ZipFile = New ZipFile()
                            EscribeLog(_StrArchivoLog, "Inicio de compresion de la carpeta: " + Directorio, False, True)
                            zip.AddFiles(FilesGoro, False, "")
                            zip.Save(Archivo_Zip_Goro)
                            EscribeLog(_StrArchivoLog, "Compresion de carpeta: " + Directorio + " finalizada.", False, True)
                        End Using

                        For Each ItemFilesGoro In FilesGoro
                            File.Delete(ItemFilesGoro)
                        Next

                        Dim FilespgpGoro As String() = Directory.GetFiles(Directorio, "*.zip")

                        If FilespgpGoro.Length > 0 Then
                            EscribeLog(_StrArchivoLog, "Inicio de cifrado de la carpeta: " + Directorio, False, True)
                            Encriptar(FilespgpGoro, Directorio)
                            EscribeLog(_StrArchivoLog, "Cifrado de carpeta: " + Directorio + " finalizada.", False, True)
                        End If

                        If (nBorrarZip) Then
                            For Each ItemFilespgpGoro In FilespgpGoro
                                File.Delete(ItemFilespgpGoro)
                            Next
                        End If
                    End If

                    Dim DirectoryNamesGoro As String()
                    Dim NombreDirectorioGoro As String

                    DirectoryNamesGoro = Directory.GetDirectories(Directorio)

                    For Each DirectorioGoro In DirectoryNamesGoro
                        NombreDirectorioGoro = DirectorioGoro.Substring(DirectorioGoro.LastIndexOf("\"c) + 1)
                        If NombreDirectorioGoro = "GORO_VOLUMEN" Then

                            Dim Archivo_Zip_Goro_Volumen = DirectorioGoro + "\" + DirectorioGoro.Substring(DirectorioGoro.LastIndexOf("\"c) + 1) + "_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + Date.Now.Hour.ToString("00") + "0000.zip"
                            Dim FilesGoro_Volumen As String() = Directory.GetFiles(DirectorioGoro)

                            If FilesGoro_Volumen.Count > 0 Then
                                Using zip As ZipFile = New ZipFile()
                                    EscribeLog(_StrArchivoLog, "Inicio de compresion de la carpeta: " + DirectorioGoro, False, True)
                                    zip.AddFiles(FilesGoro_Volumen, False, "")
                                    zip.Save(Archivo_Zip_Goro_Volumen)
                                    EscribeLog(_StrArchivoLog, "Compresion de carpeta: " + DirectorioGoro + " finalizada.", False, True)
                                End Using

                                For Each ItemFilesGoroVolumen In FilesGoro_Volumen
                                    File.Delete(ItemFilesGoroVolumen)
                                Next

                                Dim FilespgpGoroVolumen As String() = Directory.GetFiles(DirectorioGoro, "*.zip")

                                If FilespgpGoroVolumen.Length > 0 Then
                                    EscribeLog(_StrArchivoLog, "Inicio de cifrado de la carpeta: " + DirectorioGoro, False, True)
                                    Encriptar(FilespgpGoroVolumen, DirectorioGoro)
                                    EscribeLog(_StrArchivoLog, "Cifrado de carpeta: " + DirectorioGoro + " finalizada.", False, True)
                                End If

                                If (nBorrarZip) Then
                                    For Each ItemFilespgpGoroVolumen In FilespgpGoroVolumen
                                        File.Delete(ItemFilespgpGoroVolumen)
                                    Next
                                End If
                            End If
                        End If
                    Next
                Else
                    If NombreDirectorio <> "DELTA" Then
                        ProgressForm.Process = "Carpeta " + NombreDirectorio.ToString()

                        Dim Archivo_Zip = Directorio + "\" + Directorio.Substring(Directorio.LastIndexOf("\"c) + 1) + "_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + Date.Now.Hour.ToString("00") + "0000.zip"
                        Dim FilesZip As String() = Directory.GetFiles(Directorio)

                        If FilesZip.Count > 0 Then
                            Using zip As ZipFile = New ZipFile()
                                EscribeLog(_StrArchivoLog, "Inicio de compresion de la carpeta: " + Directorio, False, True)
                                zip.AddFiles(FilesZip, False, "")
                                zip.Save(Archivo_Zip)
                                EscribeLog(_StrArchivoLog, "Compresion de carpeta: " + Directorio + " finalizada.", False, True)
                            End Using

                            For Each ItemFilesZip In FilesZip
                                File.Delete(ItemFilesZip)
                            Next

                            Dim Filespgp As String() = Directory.GetFiles(Directorio, "*.zip")

                            If Filespgp.Length > 0 Then
                                EscribeLog(_StrArchivoLog, "Inicio de cifrado de la carpeta: " + Directorio, False, True)
                                Encriptar(Filespgp, Directorio)
                                EscribeLog(_StrArchivoLog, "Cifrado de carpeta: " + Directorio + " finalizada.", False, True)
                            End If

                            If (nBorrarZip) Then
                                For Each ItemFilespgp In Filespgp
                                    File.Delete(ItemFilespgp)
                                Next
                            End If
                        End If
                    End If
                End If

                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                ContadorProcess += 1
                ProgressForm.ValueProcess = ContadorProcess
            Next

            If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
            contador_action += 1
            ProgressForm.ValueAction = contador_action
        End Sub

        Private Sub ComprimrDeltaImagen(ByVal nDirectorio As String)
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim Length As Long = 0
            Dim sizeLimiteZIP As Integer = 0
            Dim RutaInicial As String = ""
            Dim Nombre_Directorio As String = ""


            ArrayNotificacion = New ArrayList
            BloqueoConcurrencia = New Object

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Nombre_Directorio = nDirectorio.Substring(nDirectorio.LastIndexOf("\"c) + 1)

                If (Nombre_Directorio) = "DELTA_PRODUCTOS_CRUZADOS" Then
                    Nombre_Directorio = "DELTA"
                End If

                Dim ConfigReporteDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBFindByNombre_Reporte(Nombre_Directorio)

                If ConfigReporteDataTable.Count > 0 Then

                    Dim sizeLimiteZIPDataTable = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "SizeLimiteZIP")
                    If (sizeLimiteZIPDataTable.Rows.Count > 0) Then
                        sizeLimiteZIP = CInt(sizeLimiteZIPDataTable(0).Valor_Parametro_Sistema.ToString())
                    End If

                    Dim objFolder As New List(Of FolderDeltaImagen)

                    Dim SubDirectorios As String()
                    SubDirectorios = Directory.GetDirectories(nDirectorio)

                    For i As Integer = 0 To SubDirectorios.Count - 1
                        Dim objFolderItem = New FolderDeltaImagen

                        objFolderItem.Directorio = SubDirectorios(i)
                        objFolderItem.Peso = Directory.GetFiles(SubDirectorios(i), "*", SearchOption.AllDirectories).Sum(Function(t) (New FileInfo(t).Length))

                        objFolder.Add(objFolderItem)
                    Next

                    RutaInicial = nDirectorio.Substring(0, nDirectorio.LastIndexOf("\"c)) + "\DELTAIMAGEN_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + "_"

                    If objFolder.Count > 0 Then
                        'Comprimir carpetas con peso igual o mayor
                        Dim objCarpetasPesoIgualMayor = objFolder.Where(Function(folderDeltaImagen) ConvertBytesToMegabytes(folderDeltaImagen.Peso) >= sizeLimiteZIP.ToString()).ToList()
                        For Each ItemPesoIgualMayor In objCarpetasPesoIgualMayor
                            Dim ZipFileName As String = RutaInicial & CStr(ContadorZip).PadLeft(6, "0"c) & ".zip"
                            Dim FolderNames As New List(Of String)
                            Dim Cajas As String = ""
                            Dim Ruta_Imagen As String = RutaInicial.Substring(RutaInicial.LastIndexOf("\"c)) & CStr(ContadorZip).PadLeft(6, "0"c)
                            Dim Nombre_Archivo_Hilo = DevuelveNombreReporte(ConfigReporteDataTable(0).id_Reporte, Me.dtpFechaProceso.Value.ToString("yyyyMMdd") & "_" & CStr(ContadorZip).PadLeft(6, "0"c), ConfigReporteDataTable)

                            FolderNames.Add(ItemPesoIgualMayor.Directorio)
                            Cajas = ItemPesoIgualMayor.Directorio.Substring(ItemPesoIgualMayor.Directorio.LastIndexOf("_"c) + 1)

                            Dim ArraListParameters As ArrayList = New ArrayList

                            ArraListParameters.Add(ArrayNotificacion.Count)
                            ArraListParameters.Add(FolderNames)
                            ArraListParameters.Add(ZipFileName)
                            ArraListParameters.Add(Cajas)
                            ArraListParameters.Add(Ruta_Imagen)
                            ArraListParameters.Add(ConfigReporteDataTable)
                            ArraListParameters.Add(Nombre_Archivo_Hilo)
                            ArraListParameters.Add(nDirectorio.Substring(nDirectorio.LastIndexOf("\"c) + 1))

                            SyncLock BloqueoConcurrencia
                                ArrayNotificacion.Add(False)
                            End SyncLock

                            Dim NewThread As New Thread(AddressOf ComprimirDeltaImagenHilo)
                            NewThread.Start(ArraListParameters)

                            ContadorZip += 1
                        Next

                        'Comprimir carpetas con peso menor
                        Dim objCarpetasPesoMenor = objFolder.Where(Function(folderDeltaImagen) ConvertBytesToMegabytes(folderDeltaImagen.Peso) < sizeLimiteZIP.ToString()).ToList()

                        If objCarpetasPesoMenor.Count > 0 Then
                            Dim objFolderOrdenadoDesc = objCarpetasPesoMenor.OrderByDescending(Function(folderDeltaImagen) folderDeltaImagen.Peso).ToList()
                            Dim objCarpetasAsc = objCarpetasPesoMenor.ToList()

                            Dim FolderNames As New List(Of String)
                            Dim Cajas As String = ""

                            For j As Integer = 0 To objFolderOrdenadoDesc.Count - 1
                                If objCarpetasAsc.Count > 0 Then
                                    Dim Peso As Long = 0
                                    Dim PesoTotal As Long = 0

                                    Peso = objFolderOrdenadoDesc(j).Peso
                                    PesoTotal = Peso
                                    FolderNames.Add(objFolderOrdenadoDesc(j).Directorio)
                                    Cajas = "'" + objFolderOrdenadoDesc(j).Directorio.Substring(objFolderOrdenadoDesc(j).Directorio.LastIndexOf("_"c) + 1) + "'"

                                    For Each ItemObjCarpetasAsc In objCarpetasAsc
                                        If ItemObjCarpetasAsc.Directorio = objFolderOrdenadoDesc(j).Directorio Then
                                            objCarpetasAsc.Remove(ItemObjCarpetasAsc)
                                            Exit For
                                        End If
                                    Next

                                    Dim objFolderOrdenadoAsc = objCarpetasAsc.OrderBy(Function(folderDeltaImagen) folderDeltaImagen.Peso).ToList()
                                    If objFolderOrdenadoAsc.Count > 0 Then
                                        For k As Integer = 0 To objFolderOrdenadoAsc.Count - 1
                                            If objFolderOrdenadoDesc(j).Directorio <> objFolderOrdenadoAsc(k).Directorio Then
                                                Dim PesoAsc As Long = 0
                                                PesoAsc = objFolderOrdenadoAsc(k).Peso

                                                PesoTotal = PesoTotal + PesoAsc

                                                If (ConvertBytesToMegabytes(PesoTotal) <= sizeLimiteZIP) Then
                                                    FolderNames.Add(objFolderOrdenadoAsc(k).Directorio)
                                                    Cajas = Cajas + ",'" + objFolderOrdenadoAsc(k).Directorio.Substring(objFolderOrdenadoAsc(k).Directorio.LastIndexOf("_"c) + 1) + "'"

                                                    For Each ItemObjCarpetasAsc In objCarpetasAsc
                                                        If ItemObjCarpetasAsc.Directorio = objFolderOrdenadoAsc(k).Directorio Then
                                                            objCarpetasAsc.Remove(ItemObjCarpetasAsc)
                                                            Exit For
                                                        End If
                                                    Next
                                                Else

                                                    Dim ZipFileName As String = RutaInicial & CStr(ContadorZip).PadLeft(6, "0"c) & ".zip"
                                                    Dim Ruta_Imagen As String = RutaInicial.Substring(RutaInicial.LastIndexOf("\"c)) & CStr(ContadorZip).PadLeft(6, "0"c)
                                                    Dim Nombre_Archivo_Hilo = DevuelveNombreReporte(ConfigReporteDataTable(0).id_Reporte, Me.dtpFechaProceso.Value.ToString("yyyyMMdd") & "_" & CStr(ContadorZip).PadLeft(6, "0"c), ConfigReporteDataTable)

                                                    Dim ArraListParameters As ArrayList = New ArrayList

                                                    ArraListParameters.Add(ArrayNotificacion.Count)
                                                    ArraListParameters.Add(FolderNames)
                                                    ArraListParameters.Add(ZipFileName)
                                                    ArraListParameters.Add(Cajas)
                                                    ArraListParameters.Add(Ruta_Imagen)
                                                    ArraListParameters.Add(ConfigReporteDataTable)
                                                    ArraListParameters.Add(Nombre_Archivo_Hilo)
                                                    ArraListParameters.Add(nDirectorio.Substring(nDirectorio.LastIndexOf("\"c) + 1))

                                                    SyncLock BloqueoConcurrencia
                                                        ArrayNotificacion.Add(False)
                                                    End SyncLock

                                                    Dim NewThread As New Thread(AddressOf ComprimirDeltaImagenHilo)
                                                    NewThread.Start(ArraListParameters)

                                                    ContadorZip += 1
                                                    Cajas = ""
                                                    FolderNames = New List(Of String)
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    ElseIf FolderNames.Count > 0 Then
                                        Dim ZipFileName As String = RutaInicial & CStr(ContadorZip).PadLeft(6, "0"c) & ".zip"
                                        Dim Ruta_Imagen As String = RutaInicial.Substring(RutaInicial.LastIndexOf("\"c)) & CStr(ContadorZip).PadLeft(6, "0"c)
                                        Dim Nombre_Archivo_Hilo = DevuelveNombreReporte(ConfigReporteDataTable(0).id_Reporte, Me.dtpFechaProceso.Value.ToString("yyyyMMdd") & "_" & CStr(ContadorZip).PadLeft(6, "0"c), ConfigReporteDataTable)

                                        Dim ArraListParameters As ArrayList = New ArrayList

                                        ArraListParameters.Add(ArrayNotificacion.Count)
                                        ArraListParameters.Add(FolderNames)
                                        ArraListParameters.Add(ZipFileName)
                                        ArraListParameters.Add(Cajas)
                                        ArraListParameters.Add(Ruta_Imagen)
                                        ArraListParameters.Add(ConfigReporteDataTable)
                                        ArraListParameters.Add(Nombre_Archivo_Hilo)
                                        ArraListParameters.Add(nDirectorio.Substring(nDirectorio.LastIndexOf("\"c) + 1))

                                        SyncLock BloqueoConcurrencia
                                            ArrayNotificacion.Add(False)
                                        End SyncLock

                                        Dim NewThread As New Thread(AddressOf ComprimirDeltaImagenHilo)
                                        NewThread.Start(ArraListParameters)

                                        ContadorZip += 1
                                        Cajas = ""
                                        FolderNames = New List(Of String)
                                    End If
                                ElseIf FolderNames.Count > 0 Then
                                    Dim ZipFileName As String = RutaInicial & CStr(ContadorZip).PadLeft(6, "0"c) & ".zip"
                                    Dim Ruta_Imagen As String = RutaInicial.Substring(RutaInicial.LastIndexOf("\"c)) & CStr(ContadorZip).PadLeft(6, "0"c)
                                    Dim Nombre_Archivo_Hilo = DevuelveNombreReporte(ConfigReporteDataTable(0).id_Reporte, Me.dtpFechaProceso.Value.ToString("yyyyMMdd") & "_" & CStr(ContadorZip).PadLeft(6, "0"c), ConfigReporteDataTable)

                                    Dim ArraListParameters As ArrayList = New ArrayList

                                    ArraListParameters.Add(ArrayNotificacion.Count)
                                    ArraListParameters.Add(FolderNames)
                                    ArraListParameters.Add(ZipFileName)
                                    ArraListParameters.Add(Cajas)
                                    ArraListParameters.Add(Ruta_Imagen)
                                    ArraListParameters.Add(ConfigReporteDataTable)
                                    ArraListParameters.Add(Nombre_Archivo_Hilo)
                                    ArraListParameters.Add(nDirectorio.Substring(nDirectorio.LastIndexOf("\"c) + 1))

                                    SyncLock BloqueoConcurrencia
                                        ArrayNotificacion.Add(False)
                                    End SyncLock

                                    Dim NewThread As New Thread(AddressOf ComprimirDeltaImagenHilo)
                                    NewThread.Start(ArraListParameters)
                                    ContadorZip += 1
                                    Cajas = ""
                                    FolderNames = New List(Of String)
                                End If
                            Next
                        End If
                    End If

                    Dim SalirDeltaImagenComprimir As Boolean = False
                    While SalirDeltaImagenComprimir = False
                        SalirDeltaImagenComprimir = True
                        SyncLock BloqueoConcurrencia
                            For Each HiloTerminadoDeltaImagenComprimir As Boolean In ArrayNotificacion
                                If HiloTerminadoDeltaImagenComprimir = False Then
                                    SalirDeltaImagenComprimir = False
                                End If
                            Next
                        End SyncLock
                        Thread.Sleep(1000)
                    End While

                    'Eliminar carpetas DELTAIMAGEN\DELTA,  DELTAIMAGEN\DELTA_EMPRESARIAL y DELTAIMAGEN\DELTA_PRODUCTOS_CRUZADOS
                    If Directory.Exists(nDirectorio) Then
                        If Directory.GetFiles(nDirectorio).Count = 0 And Directory.GetDirectories(nDirectorio).Count = 0 Then
                            Directory.Delete(nDirectorio)
                        Else
                            EscribeLog(Me._StrArchivoLog, "No se puede eliminar carpeta " + nDirectorio + " por que contiene archivos y/o carpetas.", False, True)
                        End If
                    End If
                End If
            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error comprimiendo carpetas deltaimagen " + ex.Message, False, True)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

        End Sub

        Private Sub ComprimirDeltaImagenHilo(ByVal objectArray As Object)
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Dim ArraListParametersDeltaImagenComprimir As ArrayList = objectArray

            Dim Hilo_Indice_Comprimir_DeltaImagen As Integer = ArraListParametersDeltaImagenComprimir(0)
            Dim nFolderNames As List(Of String) = ArraListParametersDeltaImagenComprimir(1)
            Dim nZipFileName As String = ArraListParametersDeltaImagenComprimir(2)
            Dim nCajas As String = ArraListParametersDeltaImagenComprimir(3)
            Dim nRutaImagen As String = ArraListParametersDeltaImagenComprimir(4)
            Dim nConfigReporteDataTable As DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteDataTable = ArraListParametersDeltaImagenComprimir(5)
            Dim nNombreArchivoHilo As String = ArraListParametersDeltaImagenComprimir(6)
            Dim nGrupoProceso As String = ArraListParametersDeltaImagenComprimir(7)

            pathDelta = Me._rutaStartProcess + "\DELTA"

            If (Not Directory.Exists(pathDelta)) Then
                CrearDirectorio(pathDelta)
            End If

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                SyncLock BloqueoConcurrencia
                    dbmIntegration.SchemaBCSCarpetaUnica.PA_Actualizacion_Campos_Delta.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), nCajas, nNombreArchivoHilo + ".txt", nRutaImagen, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, nGrupoProceso)
                    Dim ReporteDatatable = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_DELTA_2.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), "CAJAS", nCajas, Nothing, Nothing, nGrupoProceso)

                    If ReporteDatatable IsNot Nothing Then
                        For Each Registro As DataRow In ReporteDatatable.Rows
                            Registro("Nombre del archivo Metadata") = nNombreArchivoHilo + ".txt"
                        Next

                        ReporteDatatable.AcceptChanges()
                    End If

                    SeleccionExtensionGenerar(nConfigReporteDataTable, pathDelta, nNombreArchivoHilo, ReporteDatatable, nConfigReporteDataTable(0).id_Reporte, nConfigReporteDataTable(0).Nombre_Reporte)

                    If (ReporteDatatable IsNot Nothing) Then ReporteDatatable.Dispose()
                End SyncLock

                Using zip As ZipFile = New ZipFile()
                    SyncLock BloqueoConcurrencia
                        EscribeLog(_StrArchivoLog, "Inicio de compresion de la carpeta: " + nZipFileName, False, True)
                    End SyncLock
                    For Each folder In nFolderNames
                        zip.AddDirectory(folder)
                    Next
                    zip.AddFile(pathDelta + "\" + nNombreArchivoHilo + ".txt", "")
                    zip.Save(nZipFileName)
                    SyncLock BloqueoConcurrencia
                        EscribeLog(_StrArchivoLog, "Compresion de carpeta: " + nZipFileName + " finalizada.", False, True)
                    End SyncLock
                End Using

                For Each folder In nFolderNames
                    Directory.Delete(folder, True)
                Next

            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error comprimiendo carpeta delta imagen con ruta: " + nZipFileName + " " + ex.Message, False, True)
            Finally
                SyncLock BloqueoConcurrencia
                    ArrayNotificacion(Hilo_Indice_Comprimir_DeltaImagen) = True
                End SyncLock
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub Encriptar(ByVal nFilespgp As String(), ByVal nRuta As String)
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim RutaEncriptor As String
            Dim ParametrosPgP As String = ""

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                RutaEncriptor = ""
                ParametrosPgP = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "NombreLlaveCifrado")(0).Valor_Parametro_Sistema

                If ParametrosPgP <> "" Then
                    'Buscar Encriptor
                    If File.Exists("C:\Program Files\GNU\GnuPG\gpg2.exe") = True Then
                        RutaEncriptor = "C:\Program Files\GNU\GnuPG\gpg2.exe"
                    End If
                    If File.Exists("C:\Archivos de programa\GNU\GnuPG\gpg2.exe") = True Then
                        RutaEncriptor = "C:\Archivos de programa\GNU\GnuPG\gpg2.exe"
                    End If
                    If File.Exists("C:\Program Files (x86)\GNU\GnuPG\gpg2.exe") = True Then
                        RutaEncriptor = "C:\Program Files (x86)\GNU\GnuPG\gpg2.exe"
                    End If

                    'Buscar parámetros encriptor
                    Dim Temp As New TextBox
                    Temp.Text = Replace(ParametrosPgP, ";", vbNewLine)
                    ParametrosPgP = ""
                    Dim i As Integer
                    For i = 0 To Temp.Lines.Length - 1
                        ParametrosPgP += " -r " & """" & Trim(Temp.Lines.GetValue(i)) & """ "
                    Next i

                    ParametrosPgP = " --encrypt  --yes  --always-trust " & ParametrosPgP

                    'Encriptar
                    Dim P As New Process

                    For i = 0 To UBound(nFilespgp)
                        If File.Exists(nFilespgp(i) & ".pgp") = True Then
                            Continue For
                        End If
                        P.StartInfo.FileName = """" & RutaEncriptor & """"
                        P.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        P.StartInfo.CreateNoWindow = False
                        P.StartInfo.UseShellExecute = False
                        P.StartInfo.Arguments = " " & ParametrosPgP & " """ & nFilespgp(i) & """"
                        P.Start()

                        Do
                        Loop While P.HasExited = False
                        P.Refresh()
                        If File.Exists(nFilespgp(i) & ".gpg") = True Then
                            File.Move(nFilespgp(i) & ".gpg", nFilespgp(i) & ".pgp")
                        Else
                            If File.Exists(nFilespgp(i) & ".pgp") = False Then
                                EscribeLog(_StrArchivoLog, "Archivo " + nFilespgp(i) + " no fue encriptado.", False, True)
                            End If
                        End If
                    Next i
                Else
                    EscribeLog(_StrArchivoLog, "No se puede cifrar la carpeta " + nRuta + ": No se encuentra parametrizada la llave de cifrado.", False, True)
                End If
            Catch ex As Exception
                EscribeLog(_StrArchivoLog, "Error cifrando la carpeta " + nRuta + ": " + ex.Message, False, True)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub
#End Region

#Region " Funciones "
        Private Function Cargar_ArchivoRotulos() As Boolean

            If Not File.Exists(ArchivoRotulosTextBox.Text) Then
                DesktopMessageBoxControl.DesktopMessageShow("no se puede Encontrar el archivo de Rotulo Seleccionado", "Generacion de Reportes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Return False
            End If

            Dim Filename = Path.GetFileNameWithoutExtension(ArchivoRotulosTextBox.Text)
            Dim FileExtension = Path.GetExtension(ArchivoRotulosTextBox.Text)

            If Not Filename.ToUpper.StartsWith("ROTULO") Then
                DesktopMessageBoxControl.DesktopMessageShow("Archivo Incorrecto, Por favor Verificar", "Generacion de Reportes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Return False
            ElseIf Not validarFecha(Filename.Substring(Filename.Length - 8, 8), "yyyyMMdd") Then
                DesktopMessageBoxControl.DesktopMessageShow("Archivo Incorrecto, Por favor Verificar", "Generacion de Reportes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Return False
            ElseIf Not FileExtension.ToUpper().EndsWith("TXT") Then
                DesktopMessageBoxControl.DesktopMessageShow("Extensión de Archivo Incorrecto, Por favor Verificar", "Generacion de Reportes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Return False
            End If

            objCSV.LoadCSV(ArchivoRotulosTextBox.Text, True)
            _Rotulos = objCSV.DataTable.ToDataTable()

            'Validar si archivo esta lleno
            If Not _Rotulos.Rows.Count > 0 Then
                DesktopMessageBoxControl.DesktopMessageShow("Archivo de Rotulos Vacío, Por favor Verificar", "Generacion de Reportes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Return False
            End If

            Return True
        End Function

        Private Function validarFecha(str As String, formato As String) As Boolean
            Try
                Dim dt = DateTime.ParseExact(str, formato, CultureInfo.InvariantCulture)
                If IsDate(dt) Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Return False
            End Try
        End Function

        Private Function Validar() As Boolean
            If dtpFechaProceso.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una fecha de proceso", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                Return False
            End If

            If ReporteDesktopComboBox.SelectedValue = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Reporte Seleccionado no válido", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                Return False
            End If

            If RutaTextBox.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe Seleccionar Un directorio", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                Return False
            End If

            If Not ValidarRuta() Then
                Return False
            End If

            'Validar que programa de encriptar este arriba
            Dim p As Process
            Dim Kleo As Boolean = False
            For Each p In Process.GetProcesses()
                If Not p Is Nothing Then
                    If Microsoft.VisualBasic.Left(Trim(p.ProcessName).ToUpper, 9) = "KLEOPATRA" Then
                        Kleo = True
                        Exit For
                    End If
                End If
            Next
            If Kleo = False Then
                DesktopMessageBoxControl.DesktopMessageShow("Programa para encriptar(Kleopatra) no se esta ejecutando, favor validar su ejecución y cargue de las llaves de encripción", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                Return False
            End If

            'Validacion de Generacion de archivos
            Dim Resultado_SP As Boolean = True
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try
                AbreConexionIntegration(dbmIntegration, _Plugin.Manager.Sesion.Usuario.id)
                Dim Resultado As DBIntegration.SchemaBCSCarpetaUnica.TBL_Report_Log_PublicacionDataTable = Nothing
                Dim idReporte As SlygNullable(Of Short) = Nothing
                idReporte = Short.Parse(ReporteDesktopComboBox.SelectedValue)
                Dim cantidadReportes As DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_ReporteDataTable = Nothing
                Resultado = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Log_Publicacion.DBFindByfk_fecha_procesoPublicado(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), True)

                If (ReporteDesktopComboBox.SelectedValue = "-1") Then
                    Dim cantidadReportes_aux = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBGet(Nothing).Select("Vigente = 1 AND Aplica_Publicacion = 1")
                    'If (cantidadReportes_aux IsNot Nothing) Then
                    '    If Resultado.Rows.Count < cantidadReportes_aux.Count Then
                    '        DesktopMessageBoxControl.DesktopMessageShow("No se pudo completar la validacion por que no hay publicación para la fecha de proceso " + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + ", Por favor Consulte con el administrador", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                    '        Resultado_SP = False
                    '    End If
                    'End If
                Else
                    Dim Aplica_publicacion = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBGet(CShort(idReporte))
                    If (Aplica_publicacion.Rows.Count > 0) Then
                        If (CBool(Aplica_publicacion.Rows(0)("Aplica_Publicacion"))) Then
                            Dim auxResult = Resultado.Select("fk_Reporte = " + idReporte.ToString())

                            If (auxResult.Count = 0) Then
                                DesktopMessageBoxControl.DesktopMessageShow("No se pudo completar la validacion por que no hay publicación para el proecso " + Me.ReporteDesktopComboBox.Text + " y la fecha de proceso " + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + ", Por favor Consulte con el administrador", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                                Resultado_SP = False
                            End If
                        Else
                            Resultado_SP = True
                        End If
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Validar", ex)
                Return False
            Finally
                CierraConexionIntegration(dbmIntegration)
            End Try
            If Resultado_SP = True Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Function ValidarRuta() As Boolean
            If (RutaTextBox.Text = "") Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()

            ElseIf (Not Directory.Exists(Me.SelectedPath)) Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                RutaTextBox.Focus()
                RutaTextBox.SelectAll()
            Else
                Return True
            End If

            Return False
        End Function

        Private Function ValidarRotulos(ByRef data As DataTable) As Respuesta_ValidaRotulo
            Rta_ValidaRotulo = Nothing
            Rta_ValidaRotulo.msj = ""
            Rta_ValidaRotulo.Result = True

            If data.Rows.Count = 0 Then

                Rta_ValidaRotulo.Result = False
                Rta_ValidaRotulo.msj = "No se encontraron Registros para exportar con los rotulos Cargados."
                Return Rta_ValidaRotulo
            End If

            If ProcesoDesktopComboBox.SelectedValue <> "-1" Then
                Dim DataProceso = data.Select("fk_Tipo_proceso <> " + ProcesoDesktopComboBox.SelectedValue.ToString())

                If DataProceso.Count() > 0 Then
                    Rta_ValidaRotulo.Result = False
                    Rta_ValidaRotulo.msj = "El archivo contiene Rótulos de Tipos de Proceso Distintos al tipo de proceso seleccionado, no se puee continuar con el proceso de generación"
                    Return Rta_ValidaRotulo
                End If
            End If

            Dim DataFechaDiff = data.Select("[Fecha de Proceso] <> '" + dtpFechaProceso.Value.ToString("yyyy/MM/dd") + "'")

            If DataFechaDiff.Count() > 0 Then
                Rta_ValidaRotulo.Result = False
                Rta_ValidaRotulo.msj = "El archivo contiene Rótulos de fechas diferentes a la seleccionada, no se puede continuar con el proceso de generación"
                Return Rta_ValidaRotulo
            End If

            Return Rta_ValidaRotulo

        End Function

        Private Function Genera_ReporteArchivoPlano(ByVal RutaDir As String, ByVal Extension As String, ByVal NombreReporte As String, ByVal DtFinal As DataTable, Optional EliminaFolders As Boolean = True, Optional generaConTAB As Boolean = True, Optional EliminaArchivo As Boolean = Nothing, Optional InsertarDebajo As Boolean = Nothing) As Boolean
            Dim retorno As Boolean = True
            Try
                If (DtFinal.Rows.Count > 0) Then
                    If (DtFinal.Columns.Contains("Nombre_Tipo_Proceso_GORO")) Then
                        DtFinal.Columns.Remove("Nombre_Tipo_Proceso_GORO")
                    End If
                    If (DtFinal.Columns.Contains("TipoProcesoGoro")) Then
                        DtFinal.Columns.Remove("TipoProcesoGoro")
                    End If
                    If (DtFinal.Columns.Contains("Aplica_Goro")) Then
                        DtFinal.Columns.Remove("Aplica_Goro")
                    End If
                    If (DtFinal.Columns.Contains("IdCarpetaUnica_Novedad")) Then
                        DtFinal.Columns.Remove("IdCarpetaUnica_Novedad")
                    End If
                End If
                CrearDirectorio(RutaDir, False)
                Dim auxDir_total = RutaDir + "\" + NombreReporte + Extension

                If (EliminaArchivo = True) Then
                    If (File.Exists(auxDir_total)) Then
                        File.Delete(auxDir_total)
                    End If
                End If
                Dim contadorGeneral As Integer = DtFinal.Columns.Count
                Dim contadorInterno As Integer = 0


                If (InsertarDebajo = False Or InsertarDebajo = Nothing) Then
                    Using file As New System.IO.StreamWriter(auxDir_total, EliminaFolders, System.Text.ASCIIEncoding.Default)
                        For Each itemRow As DataRow In DtFinal.Rows
                            For Each itemColumn As DataColumn In DtFinal.Columns
                                contadorInterno += 1
                                Dim strInsertar As String = itemRow(itemColumn.ColumnName.Replace("\\", "\")).ToString()

                                If (contadorInterno = contadorGeneral) Then
                                    contadorInterno = 0

                                    file.Write(strInsertar)
                                Else
                                    If (generaConTAB) Then
                                        file.Write(strInsertar + ControlChars.Tab)
                                    Else
                                        file.Write(strInsertar)
                                    End If
                                End If
                            Next
                            file.Write(ControlChars.CrLf)
                        Next
                    End Using
                Else
                    If (Not File.Exists(auxDir_total)) Then
                        Using fs As FileStream = New FileStream(auxDir_total, FileMode.Create)
                        End Using
                    End If

                    Dim File_aux As StreamWriter = System.IO.File.AppendText(auxDir_total)

                    For Each itemRow As DataRow In DtFinal.Rows
                        For Each itemColumn As DataColumn In DtFinal.Columns
                            contadorInterno += 1
                            If (contadorInterno = contadorGeneral) Then
                                contadorInterno = 0

                                File_aux.Write(itemRow(itemColumn.ColumnName.Replace("\\", "\")).ToString())
                            Else
                                If (generaConTAB) Then
                                    File_aux.Write(itemRow(itemColumn.ColumnName).ToString().Replace("\\", "\") + ControlChars.Tab)
                                Else
                                    File_aux.Write(itemRow(itemColumn.ColumnName).ToString().Replace("\\", "\"))
                                End If
                            End If
                        Next
                        File_aux.Write(ControlChars.CrLf)
                    Next
                    File_aux.Close()
                End If

            Catch ex As Exception
                Me._ltErroresReporte.Add("Error, Genera_ReporteArchivoPlano " + ex.Message + " - " + DateTime.Now)
                Return False
            End Try
            Return retorno
        End Function

        Private Function Genera_ReporteExcel(ByVal RutaDir As String, ByVal Extension As String, ByVal NombreReporte As String, ByVal DtFinal As DataTable, ByVal nombreHojaExcel As String) As Boolean
            Dim retorno As Boolean = True

            Try

                If (DtFinal Is Nothing Or DtFinal.Rows.Count = 0) Then
                    DtFinal = New DataTable
                    DtFinal.Columns.Add(" ")
                    Dim ItemsDinamicos As New List(Of String)
                    For Each row In DtFinal.Columns
                        ItemsDinamicos.Add(" ")
                    Next
                    DtFinal.Rows.Add(ItemsDinamicos.ToArray())
                End If

                'Creae an Excel application instance
                Dim excelApp As New Microsoft.Office.Interop.Excel.Application()
                Dim excel As Microsoft.Office.Interop.Excel.Application
                Dim worKbooK As Microsoft.Office.Interop.Excel.Workbook
                Dim worKsheeT As Microsoft.Office.Interop.Excel.Worksheet

                excel = New Microsoft.Office.Interop.Excel.Application()
                excel.Visible = False
                excel.DisplayAlerts = False
                worKbooK = excel.Workbooks.Add(Type.Missing)

                worKsheeT = DirectCast(worKbooK.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)
                worKsheeT.Name = nombreHojaExcel
                worKsheeT.Cells.NumberFormat = "@"
                worKsheeT.Columns.NumberFormat = "@"
                worKsheeT.Rows.NumberFormat = "@"

                For i As Integer = 1 To DtFinal.Columns.Count
                    worKsheeT.Cells(1, i) = DtFinal.Columns(i - 1).ColumnName
                Next

                Dim dataMatriz = New Object(DtFinal.Rows.Count, DtFinal.Columns.Count - 1) {}
                Dim ContadorProgress As Integer = 2


                For j As Integer = 0 To DtFinal.Rows.Count - 1
                    For i As Integer = 0 To DtFinal.Columns.Count - 1
                        If ContadorProgress < 100 Then
                            'bw.ReportProgress(ContadorProgress)
                        End If

                        dataMatriz(j, i) = DtFinal.Rows(j)(i).ToString()
                        ContadorProgress += 2
                    Next
                Next
                ContadorProgress = 2


                worKsheeT.Cells.NumberFormat = "@"
                worKsheeT.Columns.NumberFormat = "@"
                worKsheeT.Rows.NumberFormat = "@"
                Dim startCell = CType(worKsheeT.Cells(2, 1), Microsoft.Office.Interop.Excel.Range)
                startCell.NumberFormat = "@"
                Dim endCell = CType(worKsheeT.Cells(DtFinal.Rows.Count + 1, DtFinal.Columns.Count), Microsoft.Office.Interop.Excel.Range)
                endCell.NumberFormat = "@"
                Dim writeRange = worKsheeT.Range(startCell, endCell)
                writeRange.NumberFormat = "@"
                writeRange.Value2 = dataMatriz

                worKbooK.SaveAs(RutaDir.Replace("\\", "\") + NombreReporte + Extension)
                worKbooK.Close()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worKsheeT)
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worKbooK)
                excel.Quit()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel)

            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error generando reporte  " + Extension.ToString() + " " + NombreReporte + ": " + ex.Message, False, True)
                Return False
            End Try
            Return retorno
        End Function

        Private Shared Function ConvertBytesToMegabytes(bytes As Long) As Double
            Return (bytes / 1048576.0F)
        End Function
#End Region

        

    End Class
End Namespace
