Imports Slyg.Tools
Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.IO
Imports System.Globalization
Imports System.Windows.Forms
Imports Slyg.Tools.Progress
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library.Config
Imports System.Threading
Imports Miharu.FileProvider.Library
Imports Slyg.Tools.Imaging.ImageManager
Imports Ionic.Zip

Namespace Imaging.Hipotecarios.Forms.Reportes
    Public Class FormReportes

#Region " Declaraciones "
        Private _Plugin As HipotecariosPlugin
        Dim _EventManager As EventManager
        Private _Rotulos As DataTable
        Private objCSV As New Slyg.Tools.CSV.CSVData
        Private ContadorZip As Integer
        Private GenerarDelta As Boolean
        Private CarpetaTempImagenes As String
        Dim _StrArchivoLog As String
        Private ProgressFormR As New FormProgress()
        Private DTResultReport As DataTable
        Dim _rutaGenerar As String
        Dim _rutaStartProcess As String
        Private DateTimeNombreArchivo As String
        Private DTResultReportReporte As DataTable
        Private pathDelta As String = ""
        Private pathDeltaImg As String = ""
        Private ArrayNotificacion As ArrayList
        Private BloqueoConcurrencia As Object
        Private ContadorDeltaImagen As Integer = 0
        Dim DateTimeGeneracionNombreArchivo As String
        Private servidor As DBImaging.SchemaCore.CTA_ServidorSimpleType
        Private centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType
        Public Shared FileNamesCons As New List(Of String)
        Dim fk_Tipo_Proceso_CU As Integer = 0
        Dim fk_Reporte_Delta_CU As Integer = 0

        Friend Structure FolderDeltaImagen
            Dim Directorio As String
            Dim Peso As Long
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

        Public Sub New(ByVal nHipotecariosDesktopPlugin As HipotecariosPlugin)
            InitializeComponent()

            _Plugin = nHipotecariosDesktopPlugin
            'CargaTablas()
        End Sub

#End Region



#Region "Metodos"
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

        Private Sub GeneraCruce()
            If ValidarCrucePublicacion() Then
                Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
                Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                Dim Mensaje As String = ""
                Dim ProgressForm As New Slyg.Tools.Progress.FormProgress()

                Try
                    Me.Enabled = False
                    Me.Cursor = Cursors.WaitCursor

                    dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                    dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)


                    Dim ValidacionRespuesta = dbmIntegration.SchemaBCSHipotecarios.PA_Validacion_Cruce_Desembolsados.DBExecute(CInt(dtpFechaProceso.Value.ToString("yyyyMMdd")), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)

                    If ValidacionRespuesta.ToString = "OK" Then
                        dbmIntegration.SchemaBCSHipotecarios.PA_Cruce_Desembolsados.DBExecute(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CInt(dtpFechaProceso.Value.ToString("yyyyMMdd")), _Plugin.Manager.Sesion.Usuario.id)

                        MessageBox.Show("Cruce Finalizado", "Cruzar Desembolsados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        MessageBox.Show("No se puede realizar el cruce de la fecha de proceso seleccionada : " & dtpFechaProceso.Value.ToString("yyyyMMdd") & " debido a: " & ValidacionRespuesta.ToString & ".", "Cruce Desembolsados", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If

                Catch ex As Exception
                    ProgressForm.Hide()
                    Application.DoEvents()
                    DesktopMessageBoxControl.DesktopMessageShow("CruzarDesembolsados", ex)
                Finally
                    ProgressForm.Visible = False
                    ProgressForm.Close()

                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()

                    Me.Enabled = True
                    Me.Cursor = Cursors.Default
                End Try
            End If
        End Sub

        Private Sub PublicarInformacion()
            If ValidarCrucePublicacion() Then
                Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                Dim ProgressForm1 As New FormProgress

                Try
                    Me.Enabled = False
                    Me.Cursor = Cursors.WaitCursor

                    dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim ValidacionRespuesta = dbmIntegration.SchemaBCSHipotecarios.PA_Validacion_Publicacion_Desembolsados.DBExecute(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CInt(dtpFechaProceso.Value.ToString("yyyyMMdd")), _Plugin.Manager.Sesion.Usuario.id)

                    If ValidacionRespuesta.ToString = "OK" Then
                        dbmIntegration.SchemaBCSHipotecarios.PA_New_Report_00_Publicar.DBExecute(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CInt(dtpFechaProceso.Value.ToString("yyyyMMdd")), _Plugin.Manager.Sesion.Usuario.id)
                        MessageBox.Show("Publicación Finalizada", "Publicar Desembolsados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        MessageBox.Show("No se puede realizar la publicación de la fecha de proceso seleccionada : " & dtpFechaProceso.Value.ToString("yyyyMMdd") & " debido a: " & ValidacionRespuesta.ToString & ".", "Publicación Desembolsados", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If

                Catch ex As Exception
                    ProgressForm1.Hide()
                    Application.DoEvents()
                    DesktopMessageBoxControl.DesktopMessageShow("PublicarDesembolsados", ex)
                Finally
                    ProgressForm1.Visible = False
                    ProgressForm1.Close()

                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()

                    Me.Enabled = True
                    Me.Cursor = Cursors.Default
                End Try
            End If
        End Sub

        Private Sub GenerarReportes()
            If Validar() Then
                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                    dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    fk_Tipo_Proceso_CU = CInt(dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "fk_Tipo_Proceso_CU")(0).Valor_Parametro_Sistema)
                    fk_Reporte_Delta_CU = CInt(dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "fk_Reporte_Delta_CU")(0).Valor_Parametro_Sistema)

                    Dim TipoProcesoDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByid_Tipo_Proceso(fk_Tipo_Proceso_CU)


                    Dim PublicadoDataTable = dbmIntegration.SchemaBCSHipotecarios.PA_Get_TBL_Report_Log_Publicacion.DBExecute(CInt(dtpFechaProceso.Value.ToString("yyyyMMdd")), fk_Reporte_Delta_CU, True)
                    If PublicadoDataTable.Rows.Count > 0 Then
                        ContadorZip = 1
                        GenerarDelta = False

                        CarpetaTempImagenes = Program.AppPath & Program.TempPath & "DeltaCSH_" & Guid.NewGuid().ToString()
                        If Not Directory.Exists(CarpetaTempImagenes) Then
                            CrearDirectorio(CarpetaTempImagenes)
                        End If

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
                        ReportesGenerar = dbmIntegration.SchemaBCSCarpetaUnica.PA_Get_Reporte.DBExecute(fk_Reporte_Delta_CU, True)

                        'Se adicionan los reportes
                        For Each reporte In ReportesGenerar
                            ReportesDictionary.Add(reporte.id_Reporte, reporte.Nombre_Reporte)
                        Next

                        'Iniciar proceso                
                        ProgressFormR.Process = ""
                        ProgressFormR.Action = ""
                        ProgressFormR.ValueProcess = 0
                        ProgressFormR.ValueAction = 0
                        ProgressFormR.MaxValueAction = ReportesDictionary.Count + 1

                        ProgressFormR.Show()

                        Dim Count_Action As Integer = 0
                        For Each Item In ReportesDictionary
                            ProgressFormR.Action = Item.Value
                            Application.DoEvents()

                            Dim ConfigReporteDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBFindByid_ReporteVigente(Item.Key, True)
                            DateTimeNombreArchivo = Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + "_" + CStr(Now.Hour).PadLeft(2, "0"c)

                            DTResultReportReporte = Nothing

                            DTResultReport = dbmIntegration.SchemaBCSHipotecarios.PA_Get_Registros_Delta.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), 1, fk_Tipo_Proceso_CU)
                            pathDeltaImg = Me._rutaStartProcess + "\DELTAIMAGENCSH\" + Item.Value.ToUpper()

                            If (Not Directory.Exists(pathDeltaImg)) Then
                                CrearDirectorio(pathDeltaImg)
                            End If

                            If DTResultReport.Rows.Count > 0 Then
                                ArrayNotificacion = New ArrayList
                                BloqueoConcurrencia = New Object

                                ArmarProceso(fk_Tipo_Proceso_CU, TipoProcesoDataTable(0).Nombre_Tipo_Proceso, ConfigReporteDataTable, Compresion)

                                If (ProgressFormR.Cancel) Then Throw New Exception("Operación cancelada por el usuario")

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
                            End If

                            If (ConfigReporteDataTable IsNot Nothing) Then ConfigReporteDataTable.Dispose()
                            If (DTResultReportReporte IsNot Nothing) Then DTResultReportReporte.Dispose()

                            EscribeLog(_StrArchivoLog, "Reporte " + ConfigReporteDataTable(0).Nombre_Reporte.ToString() + " finalizado.", False, True)
                            InsertarExportacion(Item.Key, Date.Now, pathDelta.Replace("\\", "\"))
                        Next

                        'Comprimir
                        Dim BorrarZip As Boolean = False

                        BorrarZip = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "BorrarZip")(0).Valor_Parametro_Sistema

                        ComprimirArchivos(1, BorrarZip)

                        'Eliminar imagenes de carpeta temporal 2.0
                        If Directory.Exists(CarpetaTempImagenes) Then
                            EscribeLog(_StrArchivoLog, "Eliminación de imágenes carpeta temporal: " + CarpetaTempImagenes + " iniciada.", False, True)
                            Directory.Delete(CarpetaTempImagenes, True)
                            EscribeLog(_StrArchivoLog, "Eliminación de imágenes carpeta temporal: " + CarpetaTempImagenes + " terminada.", False, True)
                        End If


                        'Fin Reporte
                        EscribeLog(_StrArchivoLog, "FIN LOG DE REPORTE PARA FECHA DE PROCESO: " + Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), False, True)
                        MessageBox.Show("Proceso Finalizado, favor revisar log", "Generar Reportes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        MessageBox.Show("No se puede realizar la generación de reportes de la fecha de proceso seleccionada : " & dtpFechaProceso.Value.ToString("yyyyMMdd") & " porque la fecha no está publicada", "Generación Delta Desembolsados", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                Catch ex As Exception
                    EscribeLog(Me._StrArchivoLog, "Error Generacion Delta Hipotecarios " + ex.Message, False, True)
                    MessageBox.Show(ex.Message.ToString(), "Error Generación Delta Hipotecarios", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    ProgressFormR.Visible = False
                    ProgressFormR.Hide()

                    Me.Enabled = True
                    Me.Cursor = Cursors.Default

                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (DTResultReport IsNot Nothing) Then DTResultReport.Dispose()

                    If Directory.Exists(CarpetaTempImagenes) Then
                        Directory.Delete(CarpetaTempImagenes, True)
                    End If
                End Try
            End If
        End Sub

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
                        pathDeltaImgxCaja = pathDeltaImg + "\DELTAIMAGENCSH_" + DateTimeGeneracionNombreArchivo + "_" + Cajas(i)
                        pathFinalImg_0 = pathDeltaImgxCaja + "\" + nameProceso + "\" + Cajas(i)
                        If (Not Directory.Exists(pathFinalImg_0)) Then
                            CrearDirectorio(pathFinalImg_0)
                        End If

                        Dim dtResulFiltradaCaja = DTResultReportProceso.Select("Caja =" + Cajas(i).ToString()).CopyToDataTable
                        Dim Ots As List(Of Object) = Nothing
                        Ots = (From a In dtResulFiltradaCaja Group a By groupDt = a.Field(Of Integer)("fk_OT") Into Group Select Group.Select(Function(x) x("fk_OT")).First()).ToList()
                        If (Ots.Count > 0) Then
                            For Each itemOt In Ots
                                servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Convert.ToInt32(itemOt))(0).ToCTA_ServidorSimpleType()
                                centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                Dim dtResulFiltradaCajaOT = dtResulFiltradaCaja.Select("fk_OT =" + itemOt.ToString()).CopyToDataTable

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
                    GC.Collect()
                End SyncLock
            End Try
        End Sub

        Private Sub ExportarImagen(nManager As FileProviderManager, ByVal nfk_Expediente As Long, ByVal nfk_Folder As Short, ByVal nfk_File As Short, ByVal nFileName As String, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, ByVal nfk_Anexo As Long)
            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim Folios As Short

            Try
                If nfk_Anexo = 0 Then
                    Folios = nManager.GetFolios(nfk_Expediente, nfk_Folder, nfk_File, 1)
                Else
                    Folios = nManager.GetFolios(nfk_Anexo)
                End If

                If Folios > 0 Then
                    For folio As Short = 1 To Folios
                        Dim Imagen() As Byte = Nothing
                        Dim Thumbnail() As Byte = Nothing

                        If nfk_Anexo = 0 Then
                            nManager.GetFolio(nfk_Expediente, nfk_Folder, nfk_File, 1, folio, Imagen, Thumbnail)
                        Else
                            nManager.GetFolio(nfk_Anexo, folio, Imagen, Thumbnail)
                        End If

                        FileName = CarpetaTempImagenes & "\" & Guid.NewGuid().ToString() & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                        FileNames.Add(FileName)

                        FileNamesCons.Add(FileName)

                        Using fs = New FileStream(FileName, FileMode.Create)
                            fs.Write(Imagen, 0, Imagen.Length)
                            fs.Close()
                        End Using
                    Next

                    FileName = nFileFolderName & "\" & nFileName.ToString() & "_" & CStr(nfk_File).PadLeft(5, "0"c) & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida

                    '-------------------------------------------------------------------------
                    ImageManager.Save(FileNames, FileName, ".tiff", EnumFormat.Tiff, nCompresion, False, nFileFolderName, True)
                    '-------------------------------------------------------------------------
                Else
                    EscribeLog(Me._StrArchivoLog, "La imagen del expediente, folder, file: " + nfk_Expediente.ToString() + ", " + nfk_Folder.ToString() + ", " + nfk_File.ToString() + " no existe.", False, True)
                End If
            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Exportar imagen " + nFileFolderName + " - " + nfk_Expediente.ToString() + ", " + nfk_Folder.ToString() + ", " + nfk_File.ToString() + " mensaje: " + ex.Message, False, True)
            End Try
        End Sub

        Private Sub InsertarExportacion(fk_Reporte As Integer, FechaExportacion As Date, Ruta As String)
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim ObjInsertarExportacion As New DBIntegration.SchemaBCSHipotecarios.TBL_Report_Log_ExportacionType
                ObjInsertarExportacion.id_reporte_Exportacion = dbmIntegration.SchemaBCSHipotecarios.TBL_Report_Log_Exportacion.DBNextId()
                ObjInsertarExportacion.fk_fecha_proceso = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")
                ObjInsertarExportacion.fk_Reporte = CInt(fk_Reporte)
                ObjInsertarExportacion.Fecha_Exportacion = FechaExportacion
                ObjInsertarExportacion.fk_Usuario = Me._Plugin.Manager.Sesion.Usuario.id
                ObjInsertarExportacion.IP_Exportacion = Me._Plugin.Manager.DesktopGlobal.ClientIpAddress
                ObjInsertarExportacion.Ruta_Exportacion = Ruta
                dbmIntegration.SchemaBCSHipotecarios.TBL_Report_Log_Exportacion.DBInsert(ObjInsertarExportacion)
            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error Generacion Delta Hipotecarios " + ex.Message, False, True)
                MessageBox.Show(ex.Message.ToString(), "Error Generación Delta Hipotecarios InsertarExportacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Private Sub ComprimirArchivos(ByVal contador_action As Integer, ByVal nBorrarZip As Boolean)
            Dim DirectoryNames As String()
            DirectoryNames = Directory.GetDirectories(Me._rutaStartProcess)

            Dim ContadorProcess As Integer = 0
            Dim NombreDirectorio As String

            ProgressFormR.Action = "COMPRIMIENDO..."
            ProgressFormR.ValueProcess = ContadorProcess
            ProgressFormR.MaxValueProcess = DirectoryNames.Count

            For Each Directorio In DirectoryNames
                NombreDirectorio = Directorio.Substring(Directorio.LastIndexOf("\"c) + 1)
                If NombreDirectorio = "DELTAIMAGENCSH" Then
                    ProgressFormR.Process = "Carpeta DELTAIMAGENCSH"
                    Dim DirectoryDeltaImagen = Directory.GetDirectories(Directorio)
                    For Each DirectorioDeltaImagenitem In DirectoryDeltaImagen
                        ComprimrDeltaImagen(DirectorioDeltaImagenitem)
                    Next

                    GC.Collect()

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

                    If (ProgressFormR.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                    ContadorProcess += 1
                    ProgressFormR.ValueProcess = ContadorProcess

                    'Comprimir carpeta DELTA, ya que se creó al finalizar los DELTAIMAGEN
                    Dim RutaDelta As String = Me._rutaStartProcess + "\DELTACSH"
                    If Directory.Exists(RutaDelta) Then
                        ProgressFormR.Process = "Carpeta DELTACSH"
                        Dim Archivo_Zip = RutaDelta + "\DELTACSH_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + "_" + Date.Now.Hour.ToString("00") + "0000.zip"
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
                    ProgressFormR.Process = "Carpeta INFORMES"

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
                    ProgressFormR.Process = "Carpeta GORO"

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
                    If NombreDirectorio <> "DELTACSH" Then
                        ProgressFormR.Process = "Carpeta " + NombreDirectorio.ToString()

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

                If (ProgressFormR.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                ContadorProcess += 1
                ProgressFormR.ValueProcess = ContadorProcess
            Next

            If (ProgressFormR.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
            contador_action += 1
            ProgressFormR.ValueAction = contador_action
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

                    RutaInicial = nDirectorio.Substring(0, nDirectorio.LastIndexOf("\"c)) + "\DELTAIMAGENCSH_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + "_"

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
                                    Dim Peso As Integer = 0
                                    Dim PesoTotal As Integer = 0

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
                                                Dim PesoAsc As Integer = 0
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
                EscribeLog(Me._StrArchivoLog, "Error comprimiendo carpetas DELTAIMAGENCSH " + ex.Message, False, True)
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

            pathDelta = Me._rutaStartProcess + "\DELTACSH"

            If (Not Directory.Exists(pathDelta)) Then
                CrearDirectorio(pathDelta)
            End If

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                SyncLock BloqueoConcurrencia
                    dbmIntegration.SchemaBCSHipotecarios.PA_Actualizacion_Campos_Delta.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), nCajas, nNombreArchivoHilo + ".txt", nRutaImagen, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, nGrupoProceso)
                    Dim ReporteDatatable = dbmIntegration.SchemaBCSHipotecarios.PA_Report_DELTA_2.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), "CAJAS", nCajas, Nothing, Nothing, nGrupoProceso)

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
                EscribeLog(Me._StrArchivoLog, "Error comprimiendo carpeta delta imagen csh con ruta: " + nZipFileName + " " + ex.Message, False, True)
            Finally
                SyncLock BloqueoConcurrencia
                    ArrayNotificacion(Hilo_Indice_Comprimir_DeltaImagen) = True
                End SyncLock
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
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

                    For Each itemExtension As DataRow In DTExtensionGenerar.Rows
                        Dim fk_Reporte = itemExtension("id_Reporte").ToString()
                        If (Convert.ToBoolean(itemExtension("Aplica_TXT"))) Then
                            extensiones.Add(".txt")

                            If (nReporte = "DELTA") Then
                                eliminaFolders = False
                            End If
                            If (Genera_ReporteArchivoPlano(RutaGenerar, ".txt", NombreReporteArchivo, DTResultReport, eliminaFolders, generaConTAB)) Then
                                EscribeLog(_StrArchivoLog, "Reporte " + NombreReporteArchivo + " TXT generado con exito en la ruta " + RutaGenerar, False, True)
                            End If
                        End If

Segunda_Validacion:

                        If (Convert.ToBoolean(itemExtension("Aplica_Excel"))) Then

                            extensiones.Add(".xlsx")
                            If Genera_ReporteExcel(RutaGenerar, ".xlsx", NombreReporteArchivo, DTResultReport, itemExtension("Nombre_Reporte").ToString()) Then
                                EscribeLog(_StrArchivoLog, "Reporte " + NombreReporteArchivo + " generado con exito en la ruta " + RutaGenerar, False, True)
                                InsertarExportacion(fk_Reporte, Date.Now, RutaGenerar)
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

        Private Shared Function ConvertBytesToMegabytes(bytes As Long) As Double
            Return (bytes / 1048576.0F)
        End Function

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
#End Region

#Region " Funciones "
        Private Function ValidarCrucePublicacion() As Boolean

            If dtpFechaProceso.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una fecha de proceso", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
                Return False
            End If

            Return True
        End Function

        Private Function Validar() As Boolean

            If dtpFechaProceso.Text = "" Then
                DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar una fecha de proceso", "Validar", DesktopMessageBoxControl.IconEnum.AdvertencyIcon)
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

            Return True
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

        Public Function DevuelveNombreReporte(ByVal idTipoReporte As String, ByVal nDateTimeGeneracionNombreArchivo As String, Optional DTResultReport As DataTable = Nothing, Optional ByVal NombreZip As Boolean = False) As String
            Dim NombreReporte As String = ""
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

            Try
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

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
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
            Return NombreReporte

        End Function

        Private Function Genera_ReporteArchivoPlano(ByVal RutaDir As String, ByVal Extension As String, ByVal NombreReporte As String, ByVal DtFinal As DataTable, Optional EliminaFolders As Boolean = True, Optional generaConTAB As Boolean = True, Optional EliminaArchivo As Boolean = Nothing, Optional InsertarDebajo As Boolean = Nothing) As Boolean
            Dim retorno As Boolean = True
            Try

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
                'Me._ltErroresReporte.Add("Error, Genera_ReporteArchivoPlano " + ex.Message + " - " + DateTime.Now) -- Revisar
                EscribeLog(Me._StrArchivoLog, "Error comprimiendo carpetas deltaimagencsh " + ex.Message, False, True)
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
#End Region

#Region " Eventos "
        Private Sub CruzarButton_Click(sender As System.Object, e As System.EventArgs) Handles CruzarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar el Cruce?", "Cruce", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                GeneraCruce()
            End If
        End Sub

        Private Sub PublicarButton_Click(sender As System.Object, e As System.EventArgs) Handles PublicarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar la publicación?", "Publicación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                PublicarInformacion()
            End If
        End Sub

        Private Sub GenerarButton_Click(sender As System.Object, e As System.EventArgs) Handles GenerarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar reportes?", "Generacion de Reportes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                GenerarReportes()
            End If
        End Sub

        Private Sub SelectFolderButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectFolderButton.Click
            SelectFolderPath()
        End Sub
#End Region
    End Class
End Namespace
