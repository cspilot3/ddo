Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Controls
Imports Miharu.FileProvider.Library
Imports System.Windows.Forms
Imports System.IO
Imports Slyg.Tools
Imports Miharu.Imaging
Imports Slyg.Tools.Imaging
Imports Miharu.Tools.Progress
Imports Ionic.Zip
Imports System.Dynamic
Imports System.Threading
Imports BCS.Plugin.Library
Imports DBImaging.SchemaCore
Imports DBImaging.SchemaSecurity
Imports Slyg.Tools.Imaging.ImageManager
Imports System.Globalization
Imports System.Drawing
Imports System.Drawing.Imaging

Namespace Imaging.Carpeta_Unica.Forms.Reportes

    Public Class FRM_Reporte

#Region " Declaraciones "

        Private _Plugin As CarpetaUnicaPlugin
        Private _Reportes As DataTable
        Private _Reportes_Delta As DataTable
        Private _Procesos As DataTable
        Private _Rotulos As DataTable
        Dim dtProcesosPublicados As New DataTable
        Private ViewCargues_DELTA As New DataView
        Private _dtImagesTodas As DataTable

        Dim Progress As New Miharu.Tools.Progress.FormProgress
        Dim Progreso As Integer = 0
        Dim _TipoReporteSeleced As Integer
        Dim _GeneraNuevamente As Boolean
        Dim contadorArchivosPlanos As Integer = 0
        Dim dtExtensionGenerarDELTA As DataTable
        Dim dtResultReportDELTA As New DataTable
        Dim dtResultReportDELTA_Constructor As New DataTable
        Dim dtPeriodicidad As DataTable
        Dim _rutaFinalDELTA As String
        Dim _nombreReporteDELTA As New Dictionary(Of Integer, String)
        Dim dcListadoReportes As New Dictionary(Of String, String)
        Dim _fechaProceso As String
        Dim _tipoReporteID As String
        Dim _rutaGenerar As String
        Dim _rutaStartProcess As String
        Dim _rotuloDelta As String
        Dim _horaDeltaReport As Integer = 0
        Dim _minutosDeltaReport As Integer = 0
        Dim _segundosDeltaReport As Integer = 0
        Dim _ltRutasComprimir As New Dictionary(Of String, String)
        Dim _ltTapasDoe As New List(Of String)
        Dim _ltTapasPasivo As New List(Of String)
        Dim _dtImagenesTapasEncontradas As New DataTable
        Dim _DirCarpeta_TapasDoe As New List(Of String)
        Dim Dir_Eliminar As New List(Of String)
        Dim ContadorTapas As Integer = 0
        Dim _NombreZipActual_DELTA As String
        Dim _ltErroresReporte As New List(Of String)
        Dim _StrArchivoLog As String
        Dim _ltStringFilesPartition As New List(Of List(Of String))
        Dim _ltStringFilesPartition_DemasFiles As New List(Of List(Of String))
        Dim _ComprimioParticiones As Boolean = False
        Dim _nombreReporte As String
        Dim TapasGeneradas As Boolean
        Dim dtTopImg As New DataTable
        Dim dtImagesComprimir As New DataTable
        Dim thr As Thread 'declaracion del hilo
        Dim thr1 As Thread 'declaracion del hilo
        Dim ContadorZip As Integer
        Dim NumListas As Integer
        Dim PathArchivo, NombreZipFile As String
        Dim Quitar_SalidaDeltaImagen As Boolean
        Dim TiempoInicial As DateTime
        Dim SegundosTotales As Long
        Dim MsjFinal As String = ""
        Dim nInputImages As New List(Of FreeImageAPI.FreeImageBitmap)

        Dim TopImgFolios As DataTable
        Dim dcArchivosPlanos_Aux As New Dictionary(Of Int32, String)
        Dim contadorGlobalImagenes As Integer
        Dim cantidadTotalImg As Integer
        'Parametros hilo
        Dim param_obj(5) As Object
        Dim ControlHiloComprimir As Boolean
        Dim fechaGeneracionArchivo As DateTime
        Private objCSV As New Slyg.Tools.CSV.CSVData

        Friend Structure Respuesta_ValidaRotulo
            Dim msj As String
            Dim Result As Boolean
        End Structure

        Dim Rta_ValidaRotulo As Respuesta_ValidaRotulo





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

#End Region

#Region " Contructores "

        Public Sub New(ByVal nCarpetaUnicaDesktopPlugin As CarpetaUnicaPlugin)
            InitializeComponent()

            _Plugin = nCarpetaUnicaDesktopPlugin
            CargaTablas()
        End Sub

#End Region

#Region " Metodos "

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

        Private Sub GeneraTapas()
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

            Try

                dbmIntegration.Connection_Open(Me._Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Me._Plugin.Manager.Sesion.Usuario.id)

                Dim RutaPredeterminada = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, "RutaTapasBCS").Rows(0)("Valor_Parametro_Sistema")

                If (TapasGeneradas = False) Then
                    Dim contadorTapas As Integer = 0
                    Dim ProcesosAplicaTapas = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByAplica_Reporte_Tapas(True)
                    Dim rutaGenerarFinalTapas As String = ""
                    Dim JornadasBD = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Jornada.DBGet(Nothing).ToList()
                    Dim camposTapaContenedor = dbmImaging.SchemaConfig.TBL_Contenedor_Campo.DBGet(Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, Nothing)
                    Dim CampoOficina As String = ""
                    Dim CampoProceso As String = ""
                    Dim CampoFechaApertura As String = ""
                    Dim CampoJornada As String = ""

                    If (camposTapaContenedor.Rows.Count > 0) Then
                        CampoOficina = "Campo_" + camposTapaContenedor.ToList().Where(Function(x) x.Nombre_Campo.ToUpper() = "OFICINA").Select(Function(X) X.id_Campo).ToList().FirstOrDefault().ToString()
                        CampoProceso = "Campo_" + camposTapaContenedor.ToList().Where(Function(x) x.Nombre_Campo.ToUpper() = "PROCESO").Select(Function(X) X.id_Campo).ToList().FirstOrDefault().ToString()
                        CampoFechaApertura = "Campo_" + camposTapaContenedor.ToList().Where(Function(x) x.Nombre_Campo.ToUpper() = "FECHA_APERTURA").Select(Function(X) X.id_Campo).ToList().FirstOrDefault().ToString()
                        CampoJornada = "Campo_" + camposTapaContenedor.ToList().Where(Function(x) x.Nombre_Campo.ToUpper() = "JORNADA").Select(Function(X) X.id_Campo).ToList().FirstOrDefault().ToString()
                    End If

                    For Each itemAplicaTapa As DataRow In ProcesosAplicaTapas.Rows
                        Dim TapasProceso = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tapas_Procesos.DBFindByfk_fecha_procesofk_Tipo_Proceso(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), CInt(itemAplicaTapa("id_Tipo_Proceso")))
                        rutaGenerarFinalTapas = Me._rutaStartProcess + RutaPredeterminada + itemAplicaTapa("Nombre_Tipo_Proceso") + "\"
                        If (Not Directory.Exists(rutaGenerarFinalTapas)) Then
                            CrearDirectorio(rutaGenerarFinalTapas)
                        End If
                        If (TapasProceso.Rows.Count > 0) Then
                            For Each itemTapa As DataRow In TapasProceso
                                Dim fk_Anexo = itemTapa("fk_Anexo").ToString()
                                Dim ImageTapaTableLt = Me._dtImagenesTapasEncontradas.AsEnumerable().Where(Function(x) x("fk_Anexo").ToString() = fk_Anexo).ToList()
                                Dim ImagenTapaTable As Object = Nothing

                                If (ImageTapaTableLt.Count > 0) Then
                                    ImagenTapaTable = Me._dtImagenesTapasEncontradas.AsEnumerable().Where(Function(x) x("fk_Anexo").ToString() = fk_Anexo).CopyToDataTable()
                                    Dim fileNames As New List(Of String)
                                    Dim Jornada = itemTapa(CampoJornada).ToString()
                                    Dim rutaAuxFinal As String = ""

                                    Dim encontradaJornada = JornadasBD.Where(Function(x) x.Nombre_Tipo_Jornada.ToUpper() = Jornada.ToUpper()).ToList()

                                    If (encontradaJornada.Count > 0) Then
                                        rutaAuxFinal = rutaGenerarFinalTapas + itemTapa(CampoJornada)
                                    Else
                                        rutaAuxFinal = rutaGenerarFinalTapas
                                    End If

                                    If (Not Directory.Exists(rutaAuxFinal)) Then
                                        CrearDirectorio(rutaAuxFinal)
                                    End If

                                    'Dim FileNameTapa = rutaAuxFinal + "\" + itemTapa(CampoFechaApertura).ToString().Replace("/", "") + "_" + itemTapa(CampoOficina).ToString() + ".tiff"
                                    Dim FileNameTapa = rutaAuxFinal + "\" + itemTapa(CampoFechaApertura).ToString().Replace("/", "") + "_" + itemTapa(CampoOficina).ToString() + "_" + itemTapa("fk_Cargue").ToString() + "_" + itemTapa("fk_Cargue_Paquete").ToString() + ".tiff"
                                    Dim imageByteFolioRuta = ImagenTapaTable.Rows(0)("Image_Binary_Ruta").ToString()

                                    Try
                                        File.Copy(imageByteFolioRuta, FileNameTapa)
                                    Catch ex As Exception
                                    End Try

                                    Try
                                        If (Directory.Exists(rutaAuxFinal + "\temp\")) Then
                                            Directory.Delete(rutaAuxFinal + "\temp\", True)
                                        End If
                                    Catch ex As Exception
                                    End Try
                                End If
                            Next
                        End If
                    Next

                    ''Comprimir TAPAS
                    Dim nombreZipTapas = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "NombreZIPTapas_BCS")
                    Dim nombreZipTapas_Aux As String = ""
                    If (nombreZipTapas.Rows.Count > 0) Then
                        nombreZipTapas_Aux = nombreZipTapas.Rows(0)("Valor_Parametro_Sistema").ToString()
                    End If
                    ComprimirArchivos(Me._rutaStartProcess + RutaPredeterminada, nombreZipTapas_Aux + "_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + Date.Now.Hour.ToString("00") + Date.Now.Minute.ToString("00") + Date.Now.Second.ToString("00"), Nothing, False)
                    Me.TapasGeneradas = True
                End If
            Catch ex As Exception

            Finally
                dbmIntegration.Connection_Close()
                dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub VisibleImagenes(ByVal opcion As Boolean)
            Me.lblProcesoActual.Visible = opcion
            Me.lblCantidadImgs.Visible = opcion
            Me.lblImgProcesadas.Visible = opcion
            Me.lblTiempoEstimado.Visible = opcion
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

        Private Sub SelectFilePath()
            Dim LectorFileDialog = New OpenFileDialog() 'FolderBrowserDialog()
            Dim Respuesta As DialogResult

            Respuesta = LectorFileDialog.ShowDialog()
            LectorFileDialog.Title = "Seleccione un Archivo"

            If (Respuesta = DialogResult.OK) Then
                ArchivoRotulosTextBox.Text = LectorFileDialog.FileName
            End If
        End Sub

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

                worKbooK.SaveAs(RutaDir.Replace("\\", "\") + "\" + NombreReporte + Extension)
                worKbooK.Close()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worKsheeT)
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(worKbooK)
                excel.Quit()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel)

            Catch ex As Exception
                Me._ltErroresReporte.Add("Error, Genera_ReporteExcel " + ex.Message + " - " + DateTime.Now)
                Return False
            End Try
            Return retorno
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

                                If (Me.Quitar_SalidaDeltaImagen) Then
                                    If (strInsertar.ToUpper().Contains("\SALIDA\DELTAIMAGEN")) Then
                                        strInsertar = strInsertar.Replace("\SALIDA\DELTAIMAGEN", "").ToString()
                                    End If
                                End If

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

        Private Sub insertarExportacion(fk_Reporte As Integer, FechaExportacion As Date, Ruta As String)
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

        Private Sub SeleccionExtensionGenerar(ByVal DTExtensionGenerar As DataTable, ByVal RutaGenerar As String, ByVal NombreReporte As String, ByVal DTResultReport As DataTable, Optional dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing)
            Dim eliminaFolders As Boolean = True

            Try
                If (DTExtensionGenerar.Rows.Count > 0) Then

                    Dim extensiones As New List(Of String)
                    Dim generaConTAB As Boolean = True
                    If NombreReporte.Contains("DOE18") Or NombreReporte.Contains("FATCA") Then
                        generaConTAB = False
                    End If

                    For Each itemExtension As DataRow In DTExtensionGenerar.Rows
                        Dim fk_Reporte = itemExtension("id_Reporte").ToString()
                        If (Convert.ToBoolean(itemExtension("Aplica_TXT"))) Then
                            extensiones.Add(".txt")

                            If (Me._nombreReporte = "FATCA") Then
                                Dim DTFatca = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_FATCA.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), Me._tipoReporteID, "TXT", NombreReporte + ".txt", Me._TipoReporteSeleced)
                                eliminaFolders = False
                                If (Genera_ReporteArchivoPlano(RutaGenerar, ".txt", NombreReporte, DTFatca, eliminaFolders, generaConTAB)) Then
                                    Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " TXT generado con exito en la ruta " + RutaGenerar)
                                Else
                                    Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " TXT tuvo error al generar en la ruta " + RutaGenerar)
                                End If
                                GoTo Segunda_Validacion
                            End If


                            If (fk_Reporte = "1") Then
                                eliminaFolders = False
                            End If
                            If (fk_Reporte = "8") Then
                                extensiones.Add(".dat")
                                If (Genera_ReporteArchivoPlano(RutaGenerar, ".dat", NombreReporte, DTResultReport, eliminaFolders, generaConTAB)) Then
                                    Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " generado con exito en la ruta " + RutaGenerar)
                                End If
                            ElseIf (fk_Reporte = "4") Then
                                Dim AUXDT_Acu = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_GORO_ACUSE_RECIBIDO.DBExecute(Me._fechaProceso, CInt(fk_Reporte))
                                AUXDT_Acu.Columns.Remove("FechaProceso")
                                Dim NombreReporte_ACU = "7089-N9V9P9E3-" + Me._fechaProceso + Date.Now.Hour.ToString("00") + Date.Now.Minute.ToString("00") + Date.Now.Second.ToString("00")
                                Dim RutaGenerar_ACU = Me._rutaStartProcess + "\GORO\"
                                CrearDirectorio(RutaGenerar_ACU, False)

                                'Acuses
                                If (Genera_ReporteArchivoPlano(RutaGenerar_ACU, ".txt", NombreReporte_ACU, AUXDT_Acu, eliminaFolders)) Then
                                    Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " generado con exito en la ruta " + RutaGenerar)
                                End If

                                'General
                                Dim auxDt_General = DTResultReport.AsEnumerable().Where(Function(x) CBool(x("Aplica_Goro")) = True)
                                If auxDt_General.Count > 0 Then
                                    Dim FinalDt = auxDt_General.CopyToDataTable()
                                    If (Genera_ReporteArchivoPlano(RutaGenerar, ".txt", NombreReporte, FinalDt, eliminaFolders)) Then
                                        Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " generado con exito en la ruta " + RutaGenerar)
                                    End If
                                End If
                            Else
                                If (Genera_ReporteArchivoPlano(RutaGenerar, ".txt", NombreReporte, DTResultReport, eliminaFolders, generaConTAB)) Then
                                    Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " generado con exito en la ruta " + RutaGenerar)
                                    insertarExportacion(fk_Reporte, Date.Now, RutaGenerar)
                                End If
                            End If


                        End If

Segunda_Validacion:

                        If (Convert.ToBoolean(itemExtension("Aplica_Excel"))) Then

                            If (Me._nombreReporte = "FATCA") Then
                                Dim DTFatca = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_FATCA.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), Me._tipoReporteID, "XLS", NombreReporte + ".xls", Me._TipoReporteSeleced)
                                eliminaFolders = False
                                If (Genera_ReporteExcel(RutaGenerar, ".xlsx", NombreReporte, DTFatca, "FATCA")) Then
                                    Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " XLS generado con exito en la ruta " + RutaGenerar)
                                Else
                                    Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " XLS tuvo error al generar en la ruta " + RutaGenerar)
                                End If
                                Return
                            End If

                            extensiones.Add(".xlsx")

                            If Genera_ReporteExcel(RutaGenerar, ".xlsx", NombreReporte, DTResultReport, itemExtension("Nombre_Reporte").ToString()) Then
                                Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " generado con exito en la ruta " + RutaGenerar)
                                insertarExportacion(fk_Reporte, Date.Now, RutaGenerar)
                            End If

                            If (fk_Reporte = "5") Then
                                Dim fechaInicial = (CDate(Me.dtpFechaProceso.Value).AddDays(-30)).ToString("yyyyMMdd")
                                Dim fechaFinal = Me._fechaProceso
                                Dim dtHis_inc = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_HISTORICOS_INC_NOV.DBExecute(fechaInicial, fechaFinal, CInt(fk_Reporte), Me._TipoReporteSeleced)
                                Me.BackgroundWorkerReport.ReportProgress(0, "Exportando Reporte de Historicos para INCONSISTENCIAS")

                                If (Genera_ReporteExcel(RutaGenerar, ".xlsx", "INCONSISTENCIAS_H_" + Date.Now.ToString("yyyyMMdd"), dtHis_inc, itemExtension("Nombre_Reporte").ToString())) Then
                                    Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " generado con exito en la ruta " + RutaGenerar)
                                End If
                            End If

                            If (fk_Reporte = "6") Then
                                Dim fechaInicial = (CDate(Me.dtpFechaProceso.Value).AddDays(-30)).ToString("yyyyMMdd")
                                Dim fechaFinal = Me._fechaProceso
                                Dim dtHis_nov = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_HISTORICOS_INC_NOV.DBExecute(fechaInicial, fechaFinal, CInt(fk_Reporte), Me._TipoReporteSeleced)
                                Me.BackgroundWorkerReport.ReportProgress(0, "Exportando Reporte de Historicos para NOVEDADES")

                                If (Genera_ReporteExcel(RutaGenerar, ".xlsx", "NOVEDADES_H_" + Me._fechaProceso, dtHis_nov, itemExtension("Nombre_Reporte").ToString())) Then
                                    Me.BackgroundWorkerReport.ReportProgress(0, "Reporte " + NombreReporte + " generado con exito en la ruta " + RutaGenerar)
                                End If
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                Me._ltErroresReporte.Add("Error, SeleccionExtensionGenerar " + ex.Message + " - " + DateTime.Now)
            End Try

        End Sub

        Public Function DevuelveNombreReporte(ByVal idTipoReporte As String, Optional DTResultReport As DataTable = Nothing, Optional ByVal NombreZip As Boolean = False) As String
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
                    NombreReporte = String.Format(dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Formatos_Salida.DBFindByid_Formato_Salidafk_Reporte(Nothing, Me._tipoReporteID).ToList().FirstOrDefault().Formato_Salida_ZIP, fechaGeneracionArchivo)
                Else
                    NombreReporte = String.Format(dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_Formatos_Salida.DBFindByid_Formato_Salidafk_Reporte(Nothing, Me._tipoReporteID).ToList().FirstOrDefault().Formato_Salida_Plano, fechaGeneracionArchivo)
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("DevuelveNombreReporte", ex)
            Finally
                CierraConexionIntegration(dbmIntegration)
            End Try
            Return NombreReporte

        End Function

        Public Sub ComprimirArchivos(ByVal PathArchivos As String, ByVal NombreFileZip As String, ByVal Extensiones As DataTable, ByVal filesOnly As Boolean, Optional ByVal EliminarFilesContenidos As Boolean = Nothing, Optional ByVal FiltroBusquedaExtension As String() = Nothing)
            Dim ArchivoZip As String
            Dim filename = NombreFileZip & ".zip"
            Dim files() As String = Nothing
            ArchivoZip = PathArchivos & "\" & filename
            Dim Zip As New Ionic.Zip.ZipFile(ArchivoZip)
            Zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed
            Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            Dim sizeLimiteZIP_Aux As Integer = 0

            Try
                files = Directory.GetFiles(PathArchivos)
                AbreConexionIntegration(dbmIntegration, _Plugin.Manager.Sesion.Usuario.id)
                If (Not File.Exists(ArchivoZip)) Then
                    Dim sizeLimiteZIP = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "SizeLimiteZIP")
                    If (sizeLimiteZIP.Rows.Count > 0) Then
                        sizeLimiteZIP_Aux = CInt(sizeLimiteZIP.Rows(0)("Valor_Parametro_Sistema").ToString())
                    End If
                    If (filesOnly) Then
                        Me.BackgroundWorkerReport.ReportProgress(0, "Comprimiendo Achivo " + Environment.NewLine + ArchivoZip)
                        Zip.AddFiles(files, False, "")
                        Zip.ParallelDeflateThreshold = -1
                        Zip.Save()

                        If (files.Count > 0) Then
                            For Each File_x In files
                                Me.BackgroundWorkerReport.ReportProgress(0, "Comprimiendo Achivo " + ArchivoZip + Environment.NewLine + "Comprimiendo: " + File_x)
                                Try
                                    If (File.Exists(File_x)) Then
                                        File.Delete(File_x)
                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                    Else
                        Me.BackgroundWorkerReport.ReportProgress(0, "Comprimiendo Achivo " + Environment.NewLine + ArchivoZip)
                        If (Directory.Exists(PathArchivos) And PathArchivos.Contains("DELTAIMAGEN") And _ComprimioParticiones = False) Then
                            Dim FoldersCount As Integer = Directory.EnumerateDirectories(PathArchivos).Count
                            Dim FoldersAll As List(Of String) = Directory.EnumerateDirectories(PathArchivos).ToList()
                            Dim xs = Directory.GetFiles(PathArchivos, "*", SearchOption.AllDirectories)
                            Dim xs_aux = xs.AsEnumerable().Where(Function(x) x.ToString().Contains(".tiff")).Select(Function(x) x.ToString())
                            Dim xs_aux_Constructor = xs.AsEnumerable().Where(Function(x) x.ToString().ToUpper().Contains("\CONSTRUCTOR")).Select(Function(x) x.ToString())

                            Dim sizeFolders As Integer = 0
                            Dim ltAux As New List(Of String)
                            Dim encontrado As Boolean = False
                            Dim ltAux_files As List(Of String) = New List(Of String)
                            Dim contadorParticiones As Integer

                            Dim length As Long = Directory.GetFiles(PathArchivos, "*", SearchOption.AllDirectories).Sum(Function(t) (New FileInfo(t).Length))

                            Dim SumSize As Long = 0
                            Dim ltNewListPatition As New List(Of String)
                            Dim dtNewDataTablePatition As New DataTable
                            Dim dtNewDataTablePatition_CONSTRUCTOR As New DataTable
                            Dim SumSize_2 As Long = 0
                            Dim pesoTotalMegabytes = ConvertBytesToMegabytes(length)
                            Dim pesoTotalMegabytes_Constructor = ConvertBytesToMegabytes(length)
                            Dim contadorGeneral = xs_aux.Count
                            Dim contadorInterno As Integer = 0
                            Dim encontradosFiles As List(Of String) = New List(Of String)
                            Dim dcArchivosPlanos As New DataTable
                            dcArchivosPlanos.Columns.Add("idParticion")
                            dcArchivosPlanos.Columns.Add("ArchivoPlano")



                            If (Me.dtResultReportDELTA.Rows.Count > 0) Then
                                For Each itemRow As DataColumn In Me.dtResultReportDELTA.Columns
                                    dtNewDataTablePatition.Columns.Add(itemRow.ColumnName)
                                Next
                            End If

                            If (xs_aux_Constructor.Count > 0) Then
                                For Each itemRow As DataColumn In Me.dtResultReportDELTA_Constructor.Columns
                                    dtNewDataTablePatition_CONSTRUCTOR.Columns.Add(itemRow.ColumnName)
                                Next
                            End If

                            If (pesoTotalMegabytes > sizeLimiteZIP_Aux) Then
                                Dim NumParticiones = pesoTotalMegabytes / sizeLimiteZIP_Aux
                                Dim intPart = Math.Truncate(NumParticiones)
                                If (NumParticiones > intPart) Then
                                    contadorParticiones = intPart + 1
                                End If
                                For index = 0 To contadorParticiones - 1
                                    For Each itemFolder In FoldersAll
                                        Dim strA() As String = Directory.GetFiles(itemFolder, "*", SearchOption.AllDirectories)
                                        If (index = contadorParticiones - 1) Then
                                            contadorArchivosPlanos += 1
                                            Dim dtAdicionalIndividual As New DataTable

                                            'CONSTRUCTOR
                                            If (xs_aux_Constructor.Count > 0) Then
                                                For Each itemRowdtNew As DataRow In dtNewDataTablePatition_CONSTRUCTOR.Rows
                                                    Dim archivoMetadata = itemRowdtNew("Nombre del archivo Metadata")
                                                    archivoMetadata = archivoMetadata.ToString().Remove(archivoMetadata.ToString().IndexOf("."), archivoMetadata.ToString().Length - archivoMetadata.ToString().IndexOf("."))

                                                    If (dtAdicionalIndividual.Columns.Count = 0) Then
                                                        For Each itemRow As DataColumn In dtNewDataTablePatition_CONSTRUCTOR.Columns
                                                            dtAdicionalIndividual.Columns.Add(itemRow.ColumnName)
                                                        Next
                                                    End If

                                                    dtAdicionalIndividual.Rows.Add(itemRowdtNew.ItemArray)
                                                    Genera_ReporteArchivoPlano(Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString(), ".txt", archivoMetadata, dtAdicionalIndividual, False, True, False, True)
                                                    Dim encontradoPLano = dcArchivosPlanos.AsEnumerable().Where(Function(x) x("ArchivoPlano") = Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString() + "\" + archivoMetadata + ".txt")
                                                    If (encontradoPLano.Count = 0) Then
                                                        dcArchivosPlanos.Rows.Add(contadorArchivosPlanos.ToString(), Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString() + "\" + archivoMetadata + ".txt")
                                                    End If
                                                    dtAdicionalIndividual.Clear()
                                                Next
                                            End If

                                            For Each itemRowdtNew As DataRow In dtNewDataTablePatition.Rows
                                                Dim archivoMetadata = itemRowdtNew("Nombre del archivo Metadata")
                                                archivoMetadata = archivoMetadata.ToString().Remove(archivoMetadata.ToString().IndexOf("."), archivoMetadata.ToString().Length - archivoMetadata.ToString().IndexOf("."))

                                                If (dtAdicionalIndividual.Columns.Count = 0) Then
                                                    For Each itemRow As DataColumn In Me.dtResultReportDELTA.Columns
                                                        dtAdicionalIndividual.Columns.Add(itemRow.ColumnName)
                                                    Next
                                                End If

                                                dtAdicionalIndividual.Rows.Add(itemRowdtNew.ItemArray)
                                                Genera_ReporteArchivoPlano(Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString(), ".txt", archivoMetadata, dtAdicionalIndividual, False, True, False, True)

                                                Dim encontradoPLano = dcArchivosPlanos.AsEnumerable().Where(Function(x) x("ArchivoPlano") = Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString() + "\" + archivoMetadata + ".txt")
                                                If (encontradoPLano.Count = 0) Then
                                                    dcArchivosPlanos.Rows.Add(contadorArchivosPlanos.ToString(), Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString() + "\" + archivoMetadata + ".txt")
                                                End If
                                                dtAdicionalIndividual.Clear()
                                            Next

                                            Me._ltStringFilesPartition_DemasFiles.Add(ltNewListPatition)
                                            Exit For
                                        End If
                                        For Each itemFile In strA
                                            'If (itemFile.ToUpper().Contains("\CONSTRUCTOR")) Then
                                            'Else
                                            If (Not encontradosFiles.Contains(itemFile)) Then
                                                encontradosFiles.Add(itemFile)
                                                contadorInterno += 1
                                                Dim info As FileInfo = New FileInfo(itemFile)
                                                Dim strFullName = info.FullName
                                                ltNewListPatition.Add(strFullName)
                                                Dim str = itemFile.Remove(0, itemFile.LastIndexOf("\SALIDA\DELTAIMAGEN"))
                                                If (str.Contains("\SALIDA\DELTAIMAGEN")) Then
                                                    str = str.Replace("\SALIDA\DELTAIMAGEN", Me._NombreZipActual_DELTA)
                                                End If

                                                If (Me.Quitar_SalidaDeltaImagen) Then
                                                    str = str.Replace("\SALIDA\DELTAIMAGEN", "")
                                                End If

                                                If (str.ToUpper().Contains("\CONSTRUCTOR")) Then
                                                    'CONSTRUCTOR
                                                    Dim paraDataTable_CONSTRUCTOR = Me.dtResultReportDELTA_Constructor.AsEnumerable().Where(Function(x) x("Ruta del Archivo").ToString().Replace(" ", "") = str)
                                                    If (paraDataTable_CONSTRUCTOR.Count > 0) Then
                                                        Dim paraDataTableAux = paraDataTable_CONSTRUCTOR.CopyToDataTable()
                                                        If (paraDataTableAux.Rows.Count > 0) Then
                                                            For Each itemRowDt As DataRow In paraDataTableAux.Rows
                                                                Dim xsFile = itemRowDt("Ruta del Archivo")
                                                                If (Me.Quitar_SalidaDeltaImagen) Then
                                                                    Me._NombreZipActual_DELTA = Me._NombreZipActual_DELTA.Replace("\SALIDA\DELTAIMAGEN", "")
                                                                End If
                                                                xsFile = xsFile.ToString().Replace(Me._NombreZipActual_DELTA, Me._NombreZipActual_DELTA + "_" + (contadorArchivosPlanos + 1).ToString("000"))
                                                                itemRowDt("Ruta del Archivo") = xsFile

                                                                Dim archivoMetadata = Me._nombreReporteDELTA.Where(Function(x) x.Value.ToString().Contains("c" + DateTime.Now.Year.ToString())).FirstOrDefault().Value
                                                                'archivoMetadata = archivoMetadata.ToString().Remove(archivoMetadata.ToString().IndexOf("."), archivoMetadata.ToString().Length - archivoMetadata.ToString().IndexOf("."))
                                                                itemRowDt("Nombre del archivo Metadata") = archivoMetadata + "_" + (contadorArchivosPlanos + 1).ToString("000") + ".txt"
                                                                dtNewDataTablePatition_CONSTRUCTOR.Rows.Add(itemRowDt.ItemArray)
                                                            Next
                                                        End If
                                                    End If
                                                Else
                                                    Dim paraDataTable = Me.dtResultReportDELTA.AsEnumerable().Where(Function(x) x("Ruta del Archivo").ToString() = str)
                                                    If (paraDataTable.Count > 0) Then
                                                        Dim paraDataTableAux = paraDataTable.CopyToDataTable()
                                                        If (paraDataTableAux.Rows.Count > 0) Then
                                                            For Each itemRowDt As DataRow In paraDataTableAux.Rows
                                                                Dim xsFile = itemRowDt("Ruta del Archivo")
                                                                If (Me.Quitar_SalidaDeltaImagen) Then
                                                                    Me._NombreZipActual_DELTA = Me._NombreZipActual_DELTA.Replace("\SALIDA\DELTAIMAGEN", "")
                                                                End If
                                                                xsFile = xsFile.ToString().Replace(Me._NombreZipActual_DELTA, Me._NombreZipActual_DELTA + "_" + (contadorArchivosPlanos + 1).ToString("000"))
                                                                itemRowDt("Ruta del Archivo") = xsFile

                                                                Dim archivoMetadata = itemRowDt("Nombre del archivo Metadata")
                                                                archivoMetadata = archivoMetadata.ToString().Remove(archivoMetadata.ToString().IndexOf("."), archivoMetadata.ToString().Length - archivoMetadata.ToString().IndexOf("."))
                                                                itemRowDt("Nombre del archivo Metadata") = archivoMetadata + "_" + (contadorArchivosPlanos + 1).ToString("000") + ".txt"
                                                                dtNewDataTablePatition.Rows.Add(itemRowDt.ItemArray)
                                                            Next
                                                        End If
                                                    End If
                                                End If

                                                SumSize += info.Length
                                                If (ConvertBytesToMegabytes(SumSize) > sizeLimiteZIP_Aux) Then
                                                    contadorArchivosPlanos += 1
                                                    Me._ltStringFilesPartition.Add(ltNewListPatition)
                                                    ltNewListPatition = New List(Of String)
                                                    Dim dtAdicionalIndividual As New DataTable

                                                    For Each itemRowdtNew As DataRow In dtNewDataTablePatition.Rows
                                                        Dim archivoMetadata = itemRowdtNew("Nombre del archivo Metadata")
                                                        archivoMetadata = archivoMetadata.ToString().Remove(archivoMetadata.ToString().IndexOf("."), archivoMetadata.ToString().Length - archivoMetadata.ToString().IndexOf("."))

                                                        If (dtAdicionalIndividual.Columns.Count = 0) Then
                                                            For Each itemRow As DataColumn In Me.dtResultReportDELTA.Columns
                                                                dtAdicionalIndividual.Columns.Add(itemRow.ColumnName)
                                                            Next
                                                        End If

                                                        dtAdicionalIndividual.Rows.Add(itemRowdtNew.ItemArray)
                                                        Genera_ReporteArchivoPlano(Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString(), ".txt", archivoMetadata, dtAdicionalIndividual, False, True, False, True)
                                                        Dim encontradoPLano = dcArchivosPlanos.AsEnumerable().Where(Function(x) x("ArchivoPlano") = Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString() + "\" + archivoMetadata + ".txt")
                                                        If (encontradoPLano.Count = 0) Then
                                                            dcArchivosPlanos.Rows.Add(contadorArchivosPlanos.ToString(), Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString() + "\" + archivoMetadata + ".txt")
                                                        End If
                                                        dtAdicionalIndividual.Clear()
                                                    Next

                                                    dtAdicionalIndividual = New DataTable

                                                    'CONSTRUCTOR
                                                    If (xs_aux_Constructor.Count > 0) Then
                                                        For Each itemRowdtNew As DataRow In dtNewDataTablePatition_CONSTRUCTOR.Rows
                                                            Dim archivoMetadata = itemRowdtNew("Nombre del archivo Metadata")
                                                            archivoMetadata = archivoMetadata.ToString().Remove(archivoMetadata.ToString().IndexOf("."), archivoMetadata.ToString().Length - archivoMetadata.ToString().IndexOf("."))

                                                            If (dtAdicionalIndividual.Columns.Count = 0) Then
                                                                For Each itemRow As DataColumn In dtNewDataTablePatition_CONSTRUCTOR.Columns
                                                                    dtAdicionalIndividual.Columns.Add(itemRow.ColumnName)
                                                                Next
                                                            End If

                                                            dtAdicionalIndividual.Rows.Add(itemRowdtNew.ItemArray)
                                                            Genera_ReporteArchivoPlano(Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString(), ".txt", archivoMetadata, dtAdicionalIndividual, False, True, False, True)
                                                            Dim encontradoPLano = dcArchivosPlanos.AsEnumerable().Where(Function(x) x("ArchivoPlano") = Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString() + "\" + archivoMetadata + ".txt")
                                                            If (encontradoPLano.Count = 0) Then
                                                                dcArchivosPlanos.Rows.Add(contadorArchivosPlanos.ToString(), Me._rutaGenerar + "\SALIDA\DELTAIMAGEN\particion_" + contadorArchivosPlanos.ToString() + "\" + archivoMetadata + ".txt")
                                                            End If
                                                            dtAdicionalIndividual.Clear()
                                                        Next
                                                    End If


                                                    dtNewDataTablePatition.Clear()
                                                    dtNewDataTablePatition_CONSTRUCTOR.Clear()


                                                    SumSize = 0
                                                End If
                                            End If

                                        Next
                                    Next
                                Next
                                Me.dtResultReportDELTA.Clear()
                                Dim ContadorZip As Integer = 1
                                EliminaDELTACARPETA()
                                If Me._ltStringFilesPartition.Count > 0 Then
                                    For Each itemZipFolder In Me._ltStringFilesPartition
                                        Dim Zip_x As New Ionic.Zip.ZipFile(PathArchivos & "\" & NombreFileZip + "_" + ContadorZip.ToString("000") + ".zip")
                                        Dim archivoPlano_encontrado = dcArchivosPlanos.AsEnumerable.Where(Function(x) x("idParticion") = ContadorZip.ToString())
                                        If (archivoPlano_encontrado.Count > 0) Then
                                            For Each itemPlano In archivoPlano_encontrado
                                                Zip_x.AddFile(itemPlano("ArchivoPlano").ToString(), "\")
                                            Next
                                        End If
                                        'CrearDirectorio(PathArchivos & "\" & NombreFileZip + "_" + ContadorZip.ToString("000"))
                                        Try
                                            For Each itemPlano In archivoPlano_encontrado
                                                Dim nameDelta = itemPlano("ArchivoPlano").Substring(itemPlano("ArchivoPlano").LastIndexOf("\"), itemPlano("ArchivoPlano").Length - itemPlano("ArchivoPlano").LastIndexOf("\"))
                                                File.Copy(itemPlano("ArchivoPlano").ToString(), Me._rutaGenerar + "\SALIDA\DELTA" + nameDelta)
                                                'File.Copy(itemPlano("ArchivoPlano").ToString(), PathArchivos & "\" & NombreFileZip + "_" + ContadorZip.ToString("000") +  nameDelta)
                                            Next
                                        Catch ex As Exception
                                        End Try

                                        'For Each itemZipFile In itemZipFolder
                                        '    Dim str = itemZipFile.Remove(itemZipFile.LastIndexOf("\"), itemZipFile.Length - itemZipFile.LastIndexOf("\"))
                                        '    Dim str_aux = itemZipFile.Remove(itemZipFile.LastIndexOf("\"), itemZipFile.Length - itemZipFile.LastIndexOf("\"))
                                        '    CrearDirectorio(str_aux)
                                        '    Dim Lastintex = str.LastIndexOf("\")
                                        '    str = str.Remove(0, Lastintex)
                                        '    File.Copy(itemZipFile.Replace("\\", "\"), PathArchivos.Replace("\\", "\") & "\" & NombreFileZip + "_" + ContadorZip.ToString("000") + str_aux + str)
                                        'Next

                                        For Each itemZipFile In itemZipFolder
                                            Dim str = itemZipFile.Remove(0, itemZipFile.LastIndexOf("\DELTAIMAGEN") + 12)
                                            Dim Lastintex = str.LastIndexOf("\")
                                            str = str.Remove(Lastintex, str.Length - Lastintex)
                                            Zip_x.AddFile(itemZipFile, str)
                                        Next
                                        Zip_x.ParallelDeflateThreshold = -1
                                        Zip_x.Save()
                                        ContadorZip += 1
                                    Next
                                End If

                                If Me._ltStringFilesPartition_DemasFiles.Count > 0 Then
                                    For Each itemZipFolder_2 As List(Of String) In Me._ltStringFilesPartition_DemasFiles

                                        If (itemZipFolder_2.Count > 0) Then
                                            Dim Zip_y As New Ionic.Zip.ZipFile(PathArchivos & "\" & NombreFileZip + "_" + ContadorZip.ToString("000") + ".zip")
                                            Dim archivoPlano_encontrado = dcArchivosPlanos.AsEnumerable.Where(Function(x) x("idParticion") = ContadorZip.ToString())
                                            If (archivoPlano_encontrado.Count > 0) Then
                                                For Each itemPlano In archivoPlano_encontrado
                                                    Zip_y.AddFile(itemPlano("ArchivoPlano").ToString(), "\")
                                                Next
                                            End If
                                            Try
                                                For Each itemPlano In archivoPlano_encontrado
                                                    Dim nameDelta = itemPlano("ArchivoPlano").Substring(itemPlano("ArchivoPlano").LastIndexOf("\"), itemPlano("ArchivoPlano").Length - itemPlano("ArchivoPlano").LastIndexOf("\"))
                                                    File.Copy(itemPlano("ArchivoPlano").ToString(), Me._rutaGenerar + "\SALIDA\DELTA\" + nameDelta)
                                                Next
                                            Catch ex As Exception
                                            End Try
                                            For Each itemZipFile In itemZipFolder_2
                                                Dim str = itemZipFile.Remove(0, itemZipFile.LastIndexOf("\DELTAIMAGEN") + 12)
                                                Dim Lastintex = str.LastIndexOf("\")
                                                str = str.Remove(Lastintex, str.Length - Lastintex)
                                                Zip_y.AddFile(itemZipFile, str)
                                            Next
                                            Zip_y.ParallelDeflateThreshold = -1
                                            Zip_y.Save()
                                            ContadorZip += 1
                                        End If
                                    Next
                                End If
                                Me._ltStringFilesPartition_DemasFiles.Clear()
                                Me._ltStringFilesPartition.Clear()
                                ltAux.Clear()
                                If (Directory.Exists(Me._rutaGenerar + "\SALIDA\DELTA\")) Then
                                    Dim strFinalRutaZIP As String = Me._rutaGenerar

                                    If (Me.Quitar_SalidaDeltaImagen) Then
                                        strFinalRutaZIP = strFinalRutaZIP + "\SALIDA\DELTA\"
                                    End If

                                    Dim Zip_DELTA As New Ionic.Zip.ZipFile(strFinalRutaZIP + Me._NombreZipActual_DELTA.Replace("DELTAIMAGEN", "DELTA") + ".zip")
                                    Zip_DELTA.AddDirectory(strFinalRutaZIP)
                                    Zip_DELTA.ParallelDeflateThreshold = -1
                                    Zip_DELTA.Save()
                                End If
                                EliminaDELTACARPETA(".txt")
                                contadorInterno = 0
                                _ComprimioParticiones = True
                            Else
                                If (Directory.Exists(PathArchivos)) Then
                                    Zip.AddDirectory(PathArchivos)
                                    Zip.ParallelDeflateThreshold = -1
                                    Zip.Save()
                                End If
                            End If
                        Else
                            If (Directory.Exists(PathArchivos)) Then
                                Zip.AddDirectory(PathArchivos)
                                Zip.ParallelDeflateThreshold = -1
                                Zip.Save()
                            End If
                        End If
                        Dim folders = Directory.EnumerateDirectories(PathArchivos)
                        If (EliminarFilesContenidos) Then
                            If (FiltroBusquedaExtension.Count > 0) Then
                                For Each extensionFiltrar In FiltroBusquedaExtension
                                    Dim filesContenidos = Directory.GetFiles(PathArchivos, extensionFiltrar)
                                    For Each itemFileContenido In filesContenidos
                                        Try
                                            If (File.Exists(itemFileContenido)) Then
                                                File.Delete(itemFileContenido)
                                            End If
                                        Catch ex As Exception
                                        End Try
                                    Next
                                Next
                            End If
                        End If
                        If (folders.Count > 0) Then
                            For Each itemFolder In folders
                                Me.BackgroundWorkerReport.ReportProgress(0, "Comprimiendo Achivo " + ArchivoZip + Environment.NewLine + "Comprimiendo Carpeta: " + itemFolder)
                                Try
                                    If (Directory.Exists(itemFolder)) Then
                                        Directory.Delete(itemFolder, True)
                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
                Dim st = New StackTrace(ex, True)
                Dim frame = st.GetFrame(0)
                Dim line = frame.GetFileLineNumber()
                EscribeLog(Me._StrArchivoLog, "Error, Genera_ReporteDELTA " + ex.Message + DateTime.Now + " Linea Error: " + line.ToString(), False, True)
                Me._ltErroresReporte.Add("Error, Genera_ReporteDELTA " + ex.Message + " - " + DateTime.Now)
            Finally
                CierraConexionIntegration(dbmIntegration)
            End Try
        End Sub

        Public Sub EliminaDELTACARPETA(Optional ByVal filterDelete As String = Nothing)
            'Elimina los archivos existentes en DELTA carpeta
            Dim filesDELTA = Directory.EnumerateFiles(Me._rutaGenerar + "\SALIDA\DELTA\", "*", SearchOption.AllDirectories)
            If (filterDelete <> "") Then
                filesDELTA = filesDELTA.AsEnumerable().Where(Function(x) x.ToString().Contains(filterDelete)).Select(Function(x) x.ToString()).ToArray()
            End If
            For Each itemzip In filesDELTA
                Try
                    File.Delete(itemzip)
                Catch ex As Exception
                End Try
            Next
        End Sub

        Private Shared Function ConvertBytesToMegabytes(bytes As Long) As Double
            Return (bytes / 1048576.0F)
        End Function



        Private Function Genera_ReporteDELTA(ByVal DTExtensionGenerar As DataTable, DTResultReport As DataTable, fk_Reporte As Integer, rutaFinal As String, nombreReportePrincipalDELTA As String, dbmIntegration As DBIntegration.DBIntegrationDataBaseManager) As Boolean
            Dim dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Dim manager As FileProviderManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim ExportacionDataSetXML As New OffLineViewer.Library.xsdOffLineData
            Dim rutaInit = rutaFinal

            Dim pathImgFinalUpdate As String = ""
            Dim NumFolios As Integer
            Dim lt_40DELTA As New Dictionary(Of String, String)
            Dim contadorNombreRegistro As Integer = 0
            Dim retorno As Boolean = True
            Dim ultimoExpediente As Boolean
            Dim RutaPredeterminada As String
            If (Not Me._nombreReporteDELTA.ContainsKey(CInt(Me._tipoReporteID))) Then
                Me._nombreReporteDELTA.Add(CInt(Me._tipoReporteID), nombreReportePrincipalDELTA)
            End If

            If (Me._dtImagenesTapasEncontradas.Columns.Count = 0) Then
                Me._dtImagenesTapasEncontradas.Columns.Add("fk_Anexo", GetType(Integer))
                Me._dtImagenesTapasEncontradas.Columns.Add("Image_Binary_Ruta")
            End If

            Dim LimiteFoliosBCS_Aux As Integer
            Dim dtDELTAAdicionales As New DataTable
            Dim Ots As List(Of Object) = Nothing
            Dim nombreComprimir_ZIP As String


            Try
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                RutaPredeterminada = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, "RutaTapasBCS").Rows(0)("Valor_Parametro_Sistema")

                Dim Compresion As ImageManager.EnumCompression
                If (_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                    Compresion = ImageManager.EnumCompression.Ccitt4
                Else
                    Compresion = ImageManager.EnumCompression.Lzw
                End If

                Dim LimiteFoliosBCS = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "LimiteFoliosBCS")
                If (LimiteFoliosBCS.Rows.Count > 0) Then
                    LimiteFoliosBCS_Aux = CInt(LimiteFoliosBCS.Rows(0)("Valor_Parametro_Sistema").ToString())
                End If

                'Traer los procesos que corresponden a ese fk-reporte (tabla TBL-Reporte_Proceso)
                Dim procesosPorReporte = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Reporte_Proceso.DBFindByfk_ReporteActivo(fk_Reporte, True).ToList()


                rutaFinal = rutaFinal.Remove(rutaFinal.LastIndexOf("\"), rutaFinal.Length - rutaFinal.LastIndexOf("\"))

                If (Me._nombreReporte.ToUpper() <> "DELTA_CONSTRUCTOR") Then
                    'llenar DTResultReport.
                    If Not ArchivoRotulosCheckBox.Checked = True Then
                        'ACTUAL
                        DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_DELTA.DBExecute(Me._fechaProceso, fk_Reporte, Me._TipoReporteSeleced, Nothing, Nothing, Nothing)
                    Else
                        'Archivo Rotulos
                        Try
                            'Eliminar de la tabla rotulos la columna Registro
                            If _Rotulos.Columns.IndexOf("REGISTRO") <> -1 Then
                                _Rotulos.Columns.Remove("REGISTRO")
                            End If
                            BulkInsert.InsertDataTableReport(_Rotulos, dbmIntegration, "#TMP_ROTULO")
                        Catch ex As Exception

                        End Try

                        'Llamar SPs de seleccion de rotulos.
                        DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_DELTA_Rotulo.DBExecute(Me._fechaProceso, fk_Reporte, Me._TipoReporteSeleced)

                        'Eliminar temporal
                        BulkInsert.DropTempTable(dbmIntegration, "#TMP_ROTULO")

                        If Not ValidarRotulos(DTResultReport).Result Then
                            Return False
                        End If

                    End If

                ElseIf (Me._nombreReporte.ToUpper() = "DELTA_CONSTRUCTOR") Then

                    'llenar DTResultReport.
                    If Not ArchivoRotulosCheckBox.Checked = True Then
                        'ACTUAL
                        DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_SeleccionaReporte().DBExecute(Me._fechaProceso, fk_Reporte, Me._nombreReporte, Me._TipoReporteSeleced, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)
                    Else
                        'Archivo Rotulos
                        Try
                            If _Rotulos.Columns.IndexOf("REGISTRO") <> -1 Then
                                _Rotulos.Columns.Remove("REGISTRO")
                            End If
                            BulkInsert.InsertDataTableReport(_Rotulos, dbmIntegration, "#TMP_ROTULO")
                        Catch ex As Exception

                        End Try

                        'Llamar SPs de seleccion de rotulos.
                        DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_SeleccionaReporte_Rotulo.DBExecute(Me._fechaProceso, Me._tipoReporteID, Me._nombreReporte, Me._TipoReporteSeleced, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)

                        'Eliminar temporal
                        BulkInsert.DropTempTable(dbmIntegration, "#TMP_ROTULO")

                        If Not ValidarRotulos(DTResultReport).Result Then
                            Me._ltErroresReporte.Add(Rta_ValidaRotulo.msj + " - " + DateTime.Now)
                            Return False
                        End If

                    End If

                End If

                Ots = (From a In DTResultReport Group a By groupDt = a.Field(Of Integer)("Imaging_fk_OT") Into Group Select Group.Select(Function(x) x("Imaging_fk_OT")).First()).ToList()

                If DTResultReport.Rows.Count > 0 Then

                    Me.cantidadTotalImg = DTResultReport.Rows.Count.ToString()
                    Me.ProgressBarExportacíon.Value = 0
                    Me.ProgressBarExportacíon.Minimum = 0
                    Me.ProgressBarExportacíon.Maximum = DTResultReport.Rows.Count.ToString()
                    Me.contadorGlobalImagenes = 0

                    If (Me._nombreReporte.ToUpper() = "DELTA" Or Me._nombreReporte.ToUpper() = "DELTA_EMPRESARIAL" Or Me._nombreReporte.ToUpper() = "DELTA_CONSTRUCTOR") Then
                        nombreComprimir_ZIP = "DELTAIMAGEN_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + "_" + Me.fechaGeneracionArchivo.Hour.ToString("00") + Me.fechaGeneracionArchivo.Minute.ToString("00") + Me.fechaGeneracionArchivo.Second.ToString("00")
                        If (Not Me._ltRutasComprimir.Keys.Contains(nombreComprimir_ZIP) And Not Me._ltRutasComprimir.Values.Contains(rutaFinal.Remove(rutaFinal.LastIndexOf("\"), rutaFinal.Length - rutaFinal.LastIndexOf("\")) + "\SALIDA\DELTAIMAGEN")) Then
                            Me._ltRutasComprimir.Add(nombreComprimir_ZIP, rutaFinal.Remove(rutaFinal.LastIndexOf("\"), rutaFinal.Length - rutaFinal.LastIndexOf("\")) + "\SALIDA\DELTAIMAGEN")
                        End If
                    End If

                    If (Ots.Count > 0) Then
                        For Each itemOt In Ots

                            Dim servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Convert.ToInt32(itemOt))(0).ToCTA_ServidorSimpleType()
                            Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                            manager = New FileProviderManager(servidor, centro, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                            manager.Connect()

                            Dim FileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files.DBExecute(CInt(itemOt), Nothing)
                            Dim FilesDataViewExpedientes As New DataView(FileDataTable)

                            For Each itemRow In procesosPorReporte
                                Dim Proceso = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBGet(CInt(itemRow.fk_Tipo_proceso)).ToList().First()
                                Me.MsjFinal = "Reporta Cantidad, " + DTResultReport.Rows.Count.ToString() + ", " + Proceso.Nombre_Tipo_Proceso
                                Me.BackgroundWorkerReport.ReportProgress(0)
                                Dim pathFinalImg As String = ""
                                Dim pathFinalImg_0 As String = ""
                                Dim dtResulFiltrada = DTResultReport.Select("fk_Tipo_Proceso =" + Proceso.id_Tipo_Proceso.ToString() + " AND Imaging_fk_OT = " + itemOt.ToString())
                                If (dtResulFiltrada.Count > 0) Then
                                    Dim dtresultFiltradaAux = dtResulFiltrada.CopyToDataTable()
                                    Dim nameProceso = Proceso.Nombre_Tipo_Proceso.Replace(" ", "_")

                                    pathFinalImg_0 = rutaFinal + "\DELTAIMAGEN\" + nameProceso + "\"
                                    If (Not Directory.Exists(pathFinalImg_0)) Then
                                        CrearDirectorio(pathFinalImg_0, True)
                                    End If

                                    'Dim pathFinalImg_ant As String = ""

                                    For i = 0 To dtresultFiltradaAux.Rows.Count - 1
                                        'For i = 0 To 10
                                        Dim RowFile_2 As Object = New ExpandoObject()
                                        RowFile_2.fk_file = CInt(dtresultFiltradaAux.Rows(i)("fk_File"))
                                        RowFile_2.fk_expediente = CType((dtresultFiltradaAux.Rows(i)("fk_Expediente")), Long)
                                        RowFile_2.fk_folder = CInt(dtresultFiltradaAux.Rows(i)("fk_Folder"))
                                        RowFile_2.id_File_Record_Folio = CInt(dtresultFiltradaAux.Rows(i)("id_File_Record_Folio"))
                                        RowFile_2.id = CInt(dtresultFiltradaAux.Rows(i)("id"))
                                        Dim RowDELTA_Actual = CType(dtresultFiltradaAux.Rows(i), DataRow)
                                        Dim contadorExpediente = dtresultFiltradaAux.AsEnumerable().Where(Function(x) CInt(x("fk_expediente")) = RowFile_2.fk_expediente)
                                        Dim Oficina = dtresultFiltradaAux(i)("Oficina").ToString()
                                        Dim FechaAperturaActual As String = "_"

                                        If (RowFile_2.fk_file = contadorExpediente.Last().Item("fk_File")) Then
                                            ultimoExpediente = True
                                        Else
                                            ultimoExpediente = False
                                        End If

                                        Try
                                            If (String.IsNullOrEmpty(dtresultFiltradaAux.Rows(i)("Fecha de Apertura").ToString())) Then
                                                FechaAperturaActual = ""
                                            Else
                                                FechaAperturaActual = CDate(dtresultFiltradaAux.Rows(i)("Fecha de Apertura").ToString().Trim()).ToString("yyyyMMdd")
                                            End If
                                        Catch ex As Exception
                                            FechaAperturaActual = "NoEncontrada"
                                        End Try


                                        Me._rotuloDelta = dtresultFiltradaAux.Rows(i)("Rotulo")
                                        Dim CajaStr = dtresultFiltradaAux.Rows(i)("Caja")

                                        If (Not String.IsNullOrEmpty(CajaStr.ToString())) Then
                                            pathFinalImg = pathFinalImg_0 + dtresultFiltradaAux.Rows(i)("Caja")
                                            pathFinalImg.Replace("\\", "\")

                                            CrearDirectorio(pathFinalImg, False)

                                            ' Obtener los Files a transferir
                                            FilesDataViewExpedientes.RowFilter = "fk_Servidor = " + servidor.id_Servidor.ToString() + " AND fk_Expediente = " + dtresultFiltradaAux.Rows(i)("fk_Expediente").ToString() + " AND fk_folder = " + dtresultFiltradaAux.Rows(i)("fk_folder").ToString() + " AND fk_file = " + dtresultFiltradaAux.Rows(i)("fk_file").ToString()

                                            For Each ItemFile As DataRowView In FilesDataViewExpedientes
                                                'If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")
                                                Dim RowFile = CType(ItemFile.Row, DBImaging.SchemaProcess.CTA_Exportacion_FilesRow)
                                                Dim expFiltrado = ItemFile.Row("fk_expediente")
                                                Dim fileFiltrado = CInt(ItemFile.Row("fk_file"))
                                                Dim Nombre_Documental As String = ItemFile.Row("Nombre_Documento")
                                                If (fileFiltrado = 1) Then
                                                    contadorNombreRegistro = 1
                                                Else
                                                    contadorNombreRegistro += 1
                                                End If

                                                If (Me._nombreReporte <> "DELTA_CONSTRUCTOR") Then
                                                    If (dtDELTAAdicionales.Columns.Count = 0 And RowFile.Folios_Documento_File > LimiteFoliosBCS_Aux And Me._nombreReporte <> "DELTA") Then
                                                        Dim Columnas = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_DELTA.DBExecute(Me._fechaProceso, fk_Reporte, Me._TipoReporteSeleced, True, Nothing, Nothing)
                                                        For Each itemColum As DataColumn In Columnas.Columns
                                                            dtDELTAAdicionales.Columns.Add(itemColum.ColumnName)
                                                        Next
                                                    End If
                                                Else
                                                    If (dtDELTAAdicionales.Columns.Count = 0 And RowFile.Folios_Documento_File > LimiteFoliosBCS_Aux) Then
                                                        For Each itemColum As DataColumn In DTResultReport.Columns
                                                            dtDELTAAdicionales.Columns.Add(itemColum.ColumnName)
                                                        Next
                                                    End If
                                                End If

                                                ' Enviar el archivo
                                                ExportarImagen(RowFile, Compresion, pathFinalImg, dbmIntegration, NumFolios, lt_40DELTA, _
                                                    nombreReportePrincipalDELTA, Proceso.id_Tipo_Proceso, CInt(itemOt), RowFile.fk_File, _
                                                    dtresultFiltradaAux.Rows(i)("Caja"), Proceso.Nombre_Tipo_Proceso.Replace(" ", "_"), _
                                                    FechaAperturaActual, RowFile.Folios_Documento_File, Nombre_Documental, Oficina, _
                                                    LimiteFoliosBCS_Aux, dtDELTAAdicionales, RowDELTA_Actual, servidor.fk_Servidor_Tipo, _
                                                    RowFile_2, fk_Reporte, RutaPredeterminada, manager)

                                            Next
                                        Else
                                            Me.BackgroundWorkerReport.ReportProgress(0, "Error al Exportar registro DELTA," + Environment.NewLine + " registro Expediente: " + RowFile_2.fk_expediente.ToString() + ", folder: " + RowFile_2.fk_folder.ToString() + " y file: " + RowFile_2.fk_folder.ToString() + " tiene Caja Vacia  ")
                                            Me._ltErroresReporte.Add("Error al Exportar registro DELTA," + Environment.NewLine + " registro Expediente: " + RowFile_2.fk_expediente.ToString() + ", folder: " + RowFile_2.fk_folder.ToString() + " y file: " + RowFile_2.fk_file.ToString() + " tiene Caja Vacia  ")
                                            retorno = False
                                        End If
                                    Next
                                End If
                            Next
                            manager.Disconnect()
                        Next
                    End If


                    'llenar DTResultReport.
                    If Not ArchivoRotulosCheckBox.Checked = True Then
                        'ACTUAL
                        DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_SeleccionaReporte().DBExecute(Me._fechaProceso, fk_Reporte, Me._nombreReporte, Me._TipoReporteSeleced, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)
                    Else
                        'Archivo Rotulos
                        Try
                            If _Rotulos.Columns.IndexOf("REGISTRO") <> -1 Then
                                _Rotulos.Columns.Remove("REGISTRO")
                            End If
                            BulkInsert.InsertDataTableReport(_Rotulos, dbmIntegration, "#TMP_ROTULO")
                        Catch ex As Exception

                        End Try

                        'Llamar SPs de seleccion de rotulos.
                        DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_SeleccionaReporte_Rotulo.DBExecute(Me._fechaProceso, fk_Reporte, Me._nombreReporte, Me._TipoReporteSeleced, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)

                        'Eliminar temporal
                        BulkInsert.DropTempTable(dbmIntegration, "#TMP_ROTULO")

                        If Not ValidarRotulos(DTResultReport).Result Then
                            Me._ltErroresReporte.Add(Rta_ValidaRotulo.msj + " - " + DateTime.Now)
                            Return False
                        End If

                    End If

                    DTResultReport = DTResultReport.AsEnumerable().OrderBy(Function(x) x("fk_Expediente")).ThenBy(Function(x) x("fk_folder")).ThenBy(Function(x) x("fk_file")).CopyToDataTable()
                    DTResultReport.Columns.Remove("Imaging_fk_OT")
                    DTResultReport.Columns.Remove("fk_Expediente")
                    DTResultReport.Columns.Remove("fk_folder")
                    DTResultReport.Columns.Remove("fk_file")
                    DTResultReport.Columns.Remove("fk_Documento")
                    DTResultReport.Columns.Remove("fk_Reporte")
                    DTResultReport.Columns.Remove("Nombre_Tipo_Proceso")
                    DTResultReport.Columns.Remove("fk_Tipo_Proceso")
                    DTResultReport.Columns.Remove("Caja")
                    DTResultReport.Columns.Remove("Oficina")
                    If (DTResultReport.Columns.Contains("Nombre_Tipo_Documental_Salida")) Then
                        DTResultReport.Columns.Remove("Nombre_Tipo_Documental_Salida")
                    End If
                    If (DTResultReport.Columns.Contains("id_File_Record_Folio")) Then
                        DTResultReport.Columns.Remove("id_File_Record_Folio")
                    End If
                    If (DTResultReport.Columns.Contains("Folios_Documento_File")) Then
                        DTResultReport.Columns.Remove("Folios_Documento_File")
                    End If

                    If (dtDELTAAdicionales.Rows.Count > 0) Then
                        dtDELTAAdicionales.Columns.Remove("Imaging_fk_OT")
                        dtDELTAAdicionales.Columns.Remove("fk_Expediente")
                        dtDELTAAdicionales.Columns.Remove("fk_folder")
                        dtDELTAAdicionales.Columns.Remove("fk_file")
                        dtDELTAAdicionales.Columns.Remove("fk_Documento")
                        dtDELTAAdicionales.Columns.Remove("fk_Reporte")
                        dtDELTAAdicionales.Columns.Remove("Nombre_Tipo_Proceso")
                        dtDELTAAdicionales.Columns.Remove("fk_Tipo_Proceso")
                        dtDELTAAdicionales.Columns.Remove("Caja")
                        dtDELTAAdicionales.Columns.Remove("Oficina")
                        If (dtDELTAAdicionales.Columns.Contains("Nombre_Tipo_Documental_Salida")) Then
                            dtDELTAAdicionales.Columns.Remove("Nombre_Tipo_Documental_Salida")
                        End If
                        If (dtDELTAAdicionales.Columns.Contains("id_File_Record_Folio")) Then
                            dtDELTAAdicionales.Columns.Remove("id_File_Record_Folio")
                        End If
                        If (dtDELTAAdicionales.Columns.Contains("Folios_Documento_File")) Then
                            dtDELTAAdicionales.Columns.Remove("Folios_Documento_File")
                        End If

                        For Each itemRowAdicional As DataRow In dtDELTAAdicionales.Rows
                            If (Me.Quitar_SalidaDeltaImagen) Then
                                Dim Str = itemRowAdicional("Ruta del Archivo")
                                Str = Str.Replace("\SALIDA\DELTAIMAGEN", "")
                                itemRowAdicional("Ruta del Archivo") = Str

                                Dim id = itemRowAdicional("id")

                                Dim encontradoGeneral = DTResultReport.AsEnumerable().Where(Function(x) x("id").ToString() = id.ToString())
                                Dim encontradoMetadata = encontradoGeneral.FirstOrDefault()("Nombre del Archivo Metadata").ToString()
                                itemRowAdicional("Nombre del archivo Metadata") = encontradoMetadata

                                If (Me._nombreReporte = "DELTA_CONSTRUCTOR") Then
                                    encontradoGeneral.FirstOrDefault()("Paquete") = itemRowAdicional("Paquete").ToString()
                                End If
                            End If
                            DTResultReport.Rows.Add(itemRowAdicional.ItemArray)
                        Next
                    End If

                    If (DTResultReport.Columns.Contains("id")) Then
                        DTResultReport.Columns.Remove("id")
                    End If

                    SeleccionExtensionGenerar(DTExtensionGenerar, rutaInit.Replace("\\", "\"), nombreReportePrincipalDELTA, DTResultReport)
                    If (dtResultReportDELTA_Constructor.Columns.Contains("id")) Then
                        dtResultReportDELTA_Constructor.Columns.Remove("id")
                    End If
                    If (Me._nombreReporte = "DELTA_CONSTRUCTOR") Then
                        Me.dtResultReportDELTA_Constructor.Merge(DTResultReport)
                        Me.dtResultReportDELTA_Constructor = Me.dtResultReportDELTA_Constructor.AsEnumerable().OrderBy(Function(x) x("Ruta del Archivo")).CopyToDataTable()
                    Else
                        Me.dtResultReportDELTA.Merge(DTResultReport)
                    End If

                    Try
                        File.Copy(rutaInit.Replace("\\", "\") + "\" + nombreReportePrincipalDELTA + ".txt", rutaFinal + "\DELTAIMAGEN\" + nombreReportePrincipalDELTA + ".txt")
                    Catch ex As Exception
                    End Try
                ElseIf (DTResultReport.Rows.Count = 0) Then
                    Me.cantidadTotalImg = 0
                    Me.ProgressBarExportacíon.Value = 0
                    Me.ProgressBarExportacíon.Minimum = 0
                    Me.ProgressBarExportacíon.Maximum = 0
                    Me.contadorGlobalImagenes = 0
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Genera_ReporteDELTA", ex)
                Dim st = New StackTrace(ex, True)
                Dim frame = st.GetFrame(0)
                Dim line = frame.GetFileLineNumber()
                EscribeLog(Me._StrArchivoLog, "Error, Genera_ReporteDELTA " + ex.Message + " Linea Error: " + line.ToString(), False, True)
                Me._ltErroresReporte.Add("Error, Genera_ReporteDELTA " + ex.Message + " - " + DateTime.Now)
            Finally
            End Try

            Return retorno
        End Function

        Private Sub ExportarImagen(ByVal RowFile As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow, nCompresion As ImageManager.EnumCompression, _
                                   nFileFolderName As String, dbmIntegration As DBIntegration.DBIntegrationDataBaseManager, ByRef NumFolios As Integer, _
                                   ByRef lt_40DELTA As Dictionary(Of String, String), nombreReportePrincipalDELTA As String, _
                                   ByVal id_Tipo_Proceso As Integer, ByVal fK_Ot As Integer, contadorNombreRegistro As Integer, ByVal CajaStr As String, Proceso As String, FechaApertura As String, _
                                   ByVal Folios As Short, ByVal Nombre_Documento As String, ByVal Oficina As String, LimiteFoliosBCS_Aux As Integer, dtDELTAAdicionales As DataTable, _
                                   ByVal rowDELTAActual As DataRow, serverType As DBCore.ServidorTipoEnum, RowFile_2 As Object, fk_reporte As Integer, RutaPredeterminada As String, _
                                   ByVal domainManager As FileProviderManager)
            Try
                Dim FileNames As New List(Of String)
                Dim FileName As String = ""
                Dim NombreRegistroImg As String = ""
                Dim contador40 As Integer = 0
                Dim contador40_individual As Integer = 0
                Dim Imagen() As Byte = Nothing
                Dim Thumbnail() As Byte = Nothing
                Dim Llego40 As Boolean = False
                lt_40DELTA = Nothing
                lt_40DELTA = New Dictionary(Of String, String)
                Dim rutaTapa As String = ""
                nInputImages.Clear()

                'Parametros creación imagen Tiff
                Dim flag As Boolean = True
                Dim encoder As Encoder = System.Drawing.Imaging.Encoder.SaveFlag
                Dim encoderInfo As ImageCodecInfo = ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/tiff")
                Dim encoderParameters As EncoderParameters = New EncoderParameters(1)
                encoderParameters.Param(0) = New EncoderParameter(encoder, (CLng(EncoderValue.MultiFrame)))
                Dim firstImage As Bitmap = Nothing

                If Not ArchivoRotulosCheckBox.Checked Then
                    If (Proceso = "PASIVO" Or Proceso = "DOE") Then
                        rutaTapa = Me._rutaStartProcess + RutaPredeterminada + Proceso + "\"
                        If (Not Me._DirCarpeta_TapasDoe.Contains(rutaTapa)) Then
                            Me._DirCarpeta_TapasDoe.Add(rutaTapa)
                            CrearDirectorio(rutaTapa, False)
                        End If
                    End If
                End If

                Dim compresionInicial = Utilities.GetEnumCompression(CType(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                'Verificar folios mas de 40 (si es asi partir en paquetes de 40 todos el file)
                If (Nombre_Documento.ToUpper() = "TAPA") Then
                    Dim anexoTapa = dbmIntegration.SchemaBCSCarpetaUnica.PA_Obtiene_Anexo.DBExecute(CType(RowFile.fk_Expediente, Long), CInt(RowFile.fk_Folder), Nothing)

                    Dim ContadorTapaAnexo As Integer = 1
                    If (anexoTapa.Rows.Count > 0) Then
                        For Each itemRowTapa As DataRow In anexoTapa.Rows
                            Dim aux_fkAnexo = itemRowTapa("fk_Anexo").ToString()
                            domainManager.GetFolio(CType(aux_fkAnexo, Long), ContadorTapaAnexo, Imagen, Nothing)
                            Dim token = itemRowTapa(2)

                            If (Imagen IsNot Nothing) Then
                                FileName = nFileFolderName & "\" & _rotuloDelta.Trim() & "_" + contadorNombreRegistro.ToString("00000") + ".tiff"
                                FileNames.Add(FileName)

                                Me.BackgroundWorkerReport.ReportProgress(0, "Exportando Reporte DELTA " + Environment.NewLine + " - " + DateTime.Now + "..." + FileName)

                                Using fs = New FileStream(FileName, FileMode.Create)
                                    fs.Write(Imagen, 0, Imagen.Length)
                                    fs.Close()
                                End Using

                                'If (FileNames.Count = Folios) Then
                                Me._dtImagenesTapasEncontradas.Rows.Add(itemRowTapa("fk_Anexo"), FileName)
                                '    '-------------------------------------------------------------------------
                                '    ImageManager.Save(FileNames, FileName, "", ImageManager.EnumFormat.Tiff, nCompresion, False, nFileFolderName, True)
                                '    '-------------------------------------------------------------------------
                                'End If
                                'End If

                                Dim FileNameZIP = Me._NombreZipActual_DELTA + "\" + Proceso + "\" + CajaStr + "\" + Me._rotuloDelta.Trim() & "_" + contadorNombreRegistro.ToString("00000") + ".tiff"
                                If (Not lt_40DELTA.Values.Contains(FileNameZIP)) Then
                                    lt_40DELTA.Add(RowFile.fk_Expediente.ToString() + "," + RowFile.fk_Folder.ToString() + "," + RowFile.fk_File.ToString() + "," + "1", FileNameZIP)
                                End If
                            End If
                            ContadorTapaAnexo += 1
                        Next

                        anexoTapa.Dispose()
                    End If
                ElseIf (Folios > LimiteFoliosBCS_Aux And Me._nombreReporte = "DELTA_CONSTRUCTOR") Then

                    'Calcula cantidad de Paquetes de 40
                    Dim contadorPaquetes As Integer
                    Dim numPaquetes = Folios / LimiteFoliosBCS_Aux
                    Dim intPart = Math.Truncate(numPaquetes)
                    If (numPaquetes > intPart) Then
                        contadorPaquetes = intPart + 1
                    End If

                    For folio As Short = 1 To Folios
                        contador40_individual += 1
                        domainManager.GetFolio(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version, folio, Imagen, Nothing)

                        If (contador40_individual = LimiteFoliosBCS_Aux) Then
                            contador40 += 1
                            contador40_individual = 0
                            FileName = nFileFolderName & "\" & Me._rotuloDelta.Trim() & "_" + contadorNombreRegistro.ToString("00000") + "_" + contador40.ToString("00000") + ".tiff"
                            Me.BackgroundWorkerReport.ReportProgress(0, "Exportando Reporte DELTA " + Environment.NewLine + " - " + DateTime.Now + "..." + FileName)
                            FileName.Replace("\\", "\")
                            If (contador40 > 1) Then
                                Dim xfile = Me._NombreZipActual_DELTA + "\" + Proceso + "\" + CajaStr + "\" + Me._rotuloDelta.Trim() & "_" + contadorNombreRegistro.ToString("00000") + "_" + contador40.ToString("00000") + ".tiff"

                                If (Me.Quitar_SalidaDeltaImagen) Then
                                    If (xfile.Replace("\\", "\").ToUpper().Contains("\SALIDA\DELTAIMAGEN")) Then
                                        xfile = xfile.Replace("\\", "\").Replace("\SALIDA\DELTAIMAGEN", "").ToString()
                                    End If
                                End If

                                Dim encontrado = dtDELTAAdicionales.AsEnumerable().Where(Function(x) x("Ruta del Archivo").ToString().Trim() = xfile.Replace("\\", "\")).ToList()
                                If (encontrado.Count = 0) Then
                                    Dim rowInsertarDt As DataRow = Nothing
                                    rowInsertarDt = rowDELTAActual

                                    Dim strFinal = RellenarString(xfile.Replace("\\", "\"), " ", "r", 100)
                                    rowInsertarDt("Ruta del Archivo") = strFinal
                                    rowInsertarDt("Paquete") = RellenarString(contadorPaquetes.ToString(), " ", "r", 10)
                                    dtDELTAAdicionales.Rows.Add(rowInsertarDt.ItemArray)
                                End If
                            End If

                            Dim stream As Stream = New MemoryStream(Imagen)
                            nInputImages.Add(Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap.FromStream(stream))
                            Me.BackgroundWorkerReport.ReportProgress(0, "Exportando Reporte DELTA " + Environment.NewLine + " - " + DateTime.Now + "..." + FileName)

                            '-------------------------------------------------------------------------
                            ImageManager.Save(nInputImages, FileName, ".tiff", EnumFormat.Tiff, nCompresion, False, nFileFolderName)
                            'domainManager.Save(nInputImages, FileName, ".tiff", EnumFormat.Tiff, nCompresion, False, nFileFolderName)
                            nInputImages.Clear()
                            '-------------------------------------------------------------------------

                            FileNames.Clear()
                        Else
                            FileName = nFileFolderName & "\" & Me._rotuloDelta.Trim() & "_" + contadorNombreRegistro.ToString("00000") + "_" + (contador40 + 1).ToString("00000") + ".tiff"
                            Me.BackgroundWorkerReport.ReportProgress(0, "Exportando Reporte DELTA " + Environment.NewLine + " - " + DateTime.Now + "..." + FileName)
                            FileName.Replace("\\", "\")

                            If ((contador40 + 1) > 1) Then
                                Dim xfile = Me._NombreZipActual_DELTA + "\" + Proceso + "\" + CajaStr + "\" + Me._rotuloDelta.Trim() & "_" + contadorNombreRegistro.ToString("00000") + "_" + (contador40 + 1).ToString("00000") + ".tiff"

                                If (Me.Quitar_SalidaDeltaImagen) Then
                                    If (xfile.Replace("\\", "\").ToUpper().Contains("\SALIDA\DELTAIMAGEN")) Then
                                        xfile = xfile.Replace("\\", "\").Replace("\SALIDA\DELTAIMAGEN", "").ToString()
                                    End If
                                End If

                                Dim encontrado = dtDELTAAdicionales.AsEnumerable().Where(Function(x) x("Ruta del Archivo").ToString().Trim() = xfile.Replace("\\", "\")).ToList()
                                If (encontrado.Count = 0) Then
                                    Dim rowInsertarDt As DataRow = Nothing
                                    rowInsertarDt = rowDELTAActual

                                    Dim strFinal = RellenarString(xfile.Replace("\\", "\"), " ", "r", 100)
                                    rowInsertarDt("Ruta del Archivo") = strFinal
                                    rowInsertarDt("Paquete") = RellenarString(contadorPaquetes.ToString(), " ", "r", 10)
                                    dtDELTAAdicionales.Rows.Add(rowInsertarDt.ItemArray)
                                End If
                            End If

                            Dim stream As Stream = New MemoryStream(Imagen)
                            nInputImages.Add(Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap.FromStream(stream))
                            Me.BackgroundWorkerReport.ReportProgress(0, "Exportando Reporte DELTA " + Environment.NewLine + " - " + DateTime.Now + "..." + FileName)

                            If (folio = Folios) Then
                                '-------------------------------------------------------------------------
                                ImageManager.Save(nInputImages, FileName, ".tiff", EnumFormat.Tiff, nCompresion, False, nFileFolderName)
                                nInputImages.Clear()
                                '-------------------------------------------------------------------------
                            End If
                        End If

                        Dim FileNameZIP = Me._NombreZipActual_DELTA + "\" + Proceso + "\" + CajaStr + "\" + Me._rotuloDelta.Trim() & "_" + contadorNombreRegistro.ToString("00000") + "_" + (contador40 + 1).ToString("00000") + ".tiff"
                        If (Not lt_40DELTA.Values.Contains(FileNameZIP)) Then
                            lt_40DELTA.Add(RowFile.fk_Expediente.ToString() + "," + RowFile.fk_Folder.ToString() + "," + RowFile.fk_File.ToString() + "," + folio.ToString(), FileNameZIP)
                        End If
                        Imagen = Nothing
                    Next
                Else
                    For folio As Short = 1 To Folios
                        domainManager.GetFolio(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version, folio, Imagen, Nothing)

                        Dim stream As Stream = New MemoryStream(Imagen)
                        'nInputImages.Add(Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap.FromStream(stream))
                        Dim imagePage = Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap.FromStream(stream)

                        If (folio = 1) Then
                            FileName = nFileFolderName & "\" & Me._rotuloDelta.Trim() & "_" + contadorNombreRegistro.ToString("00000") + ".tiff"
                            Me.BackgroundWorkerReport.ReportProgress(0, "Exportando Reporte DELTA " + Environment.NewLine + " - " + DateTime.Now + "..." + FileName)
                            FileName.Replace("\\", "\")

                            firstImage = imagePage
                            'Save the first frame of the multi page tiff
                            firstImage.Save(FileName, encoderInfo, encoderParameters) 'throws Generic GDI+ error if the memory streams are not open when this is called
                            encoderParameters.Param(0) = New EncoderParameter(encoder, (CLng(EncoderValue.FrameDimensionPage)))
                        Else
                            firstImage.SaveAdd(imagePage, encoderParameters) 'throws Generic GDI+ error if the memory streams are not open when this is called
                        End If

                        'If (nInputImages.Count = Folios) Then
                        '    'ImageManager.Save(nInputImages, FileName, ".tiff", EnumFormat.Tiff, nCompresion, pmultipage, nFileFolderName)
                        '    SaevAsMultiPageTiff(FileName, nInputImages)
                        '    nInputImages.Clear()
                        'End If

                        Dim FileNameZIP = Me._NombreZipActual_DELTA + "\" + Proceso + "\" + CajaStr + "\" + Me._rotuloDelta.Trim() & "_" + contadorNombreRegistro.ToString("00000") + ".tiff"
                        If (Not lt_40DELTA.Values.Contains(FileNameZIP)) Then
                            lt_40DELTA.Add(RowFile.fk_Expediente.ToString() + "," + RowFile.fk_Folder.ToString() + "," + RowFile.fk_File.ToString() + "," + folio.ToString(), FileNameZIP)
                        End If
                    Next
                    'Close out the file
                    encoderParameters.Param(0) = New EncoderParameter(encoder, (CLng(EncoderValue.Flush)))
                    firstImage.SaveAdd(encoderParameters)
                End If
                Imagen = Nothing
                Try
                    If (Directory.Exists(nFileFolderName + "\temp\")) Then
                        Directory.Delete(nFileFolderName + "\temp\", True)
                    End If
                Catch ex As Exception
                End Try

                Actualiza_PathImg(lt_40DELTA, nombreReportePrincipalDELTA, dbmIntegration, id_Tipo_Proceso, fK_Ot)

                Imagen = Nothing
            Catch ex As Exception
                'DesktopMessageBoxControl.DesktopMessageShow("ExportarImagen", ex)
                Dim st = New StackTrace(ex, True)
                Dim frame = st.GetFrame(0)
                Dim line = frame.GetFileLineNumber()
                EscribeLog(Me._StrArchivoLog, "Error, ExportaImagen " + ex.Message + " - " + DateTime.Now + " Linea Error: " + line.ToString(), False, True)
                Me._ltErroresReporte.Add("Error, ExportaImagen " + ex.Message + " - " + DateTime.Now)
            End Try
        End Sub


        Private Sub SaveAsMultiPageTiff(sOutFile As String, nImageList As List(Of FreeImageAPI.FreeImageBitmap))

            Dim flag As Boolean = True
            Dim encoder As Encoder = System.Drawing.Imaging.Encoder.SaveFlag
            'Dim encoderInfo As ImageCodecInfo = ImageCodecInfo.GetImageEncoders().First(i >= i.MimeType == "image/tiff")
            Dim encoderInfo As ImageCodecInfo = ImageCodecInfo.GetImageEncoders().First(Function(i) i.MimeType = "image/tiff")
            Dim encoderParameters As EncoderParameters = New EncoderParameters(1)
            encoderParameters.Param(0) = New EncoderParameter(encoder, (CLng(EncoderValue.MultiFrame)))

            Dim firstImage As Bitmap = Nothing
            Try
                'Add the remining images to the tiff
                For Each imagePage As Bitmap In nImageList
                    If (flag) Then
                        firstImage = imagePage
                        'Save the first frame of the multi page tiff
                        firstImage.Save(sOutFile, encoderInfo, encoderParameters) 'throws Generic GDI+ error if the memory streams are not open when this is called
                        encoderParameters.Param(0) = New EncoderParameter(encoder, (CLng(EncoderValue.FrameDimensionPage)))
                        flag = False
                    End If
                    'End Using
                    firstImage.SaveAdd(imagePage, encoderParameters) 'throws Generic GDI+ error if the memory streams are not open when this is called
                    'End Using
                Next

            Catch ex As Exception
                'ensure the errors are not missed while allowing for flush in finally block so files dont get locked up.
                Throw
            Finally
                'Close out the file
                encoderParameters.Param(0) = New EncoderParameter(encoder, (CLng(EncoderValue.Flush)))
                firstImage.SaveAdd(encoderParameters)
            End Try
        End Sub


        Private Sub Actualiza_PathImg(ByVal lt_40DELTA As Dictionary(Of String, String), nombreReportePrincipalDELTA As String, dbmIntegration As DBIntegration.DBIntegrationDataBaseManager, ByVal id_Tipo_Proceso As Integer, ByVal fk_Ot As Integer)
            Me.BackgroundWorkerReport.ReportProgress(0, "Exportando Reporte DELTA " + Environment.NewLine + " - " + DateTime.Now + "..." + lt_40DELTA.First().Value)
            Dim datosExp = lt_40DELTA.First().Key.ToString().Split(",")
            Dim expediente = datosExp(0)
            Dim folder = datosExp(1)
            Dim file = datosExp(2)
            Dim encontrado As Object = Nothing

            Dim strInsertar_AUX As String = lt_40DELTA.First().Value.ToString().Replace("\\", "\")

            'If (Me.Quitar_SalidaDeltaImagen) Then
            '    If (strInsertar_AUX.ToUpper().Contains("\SALIDA\DELTAIMAGEN")) Then
            '        strInsertar_AUX = strInsertar_AUX.Replace("\SALIDA\DELTAIMAGEN", "").ToString()
            '    End If
            'End If

            'dbmIntegration.SchemaBCSCarpetaUnica.PA_ActualizaFileName_DELTA.DBExecute(nombreReportePrincipalDELTA + ".txt", strInsertar_AUX, id_Tipo_Proceso, Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), CType(expediente, Long), CInt(folder), CInt(file), Me._nombreReporte)

            If (lt_40DELTA.Count > 0 And _nombreReporte <> "DELTA_CONSTRUCTOR") Then
                'encontrado = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_DELTA.DBFindByfk_ReporteFecha_Procesofk_Tipo_ProcesoImaging_fk_OTfk_Expedientefk_folderfk_file(CInt(Me._tipoReporteID), Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), id_Tipo_Proceso, fk_Ot, CType(expediente, Long), CInt(folder), CInt(file))
                'Modificacion de DBFind, Se agreaga fecha de proceso Ejecucion y se quita Fecha de Proceso.
                encontrado = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_DELTA.DBFindByfk_Reportefk_Tipo_ProcesoImaging_fk_OTfk_Expedientefk_folderfk_fileFecha_Proceso_Ejecucion(CInt(Me._tipoReporteID), id_Tipo_Proceso, fk_Ot, CType(expediente, Long), CInt(folder), CInt(file), Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"))
            ElseIf (lt_40DELTA.Count > 0 And _nombreReporte = "DELTA_CONSTRUCTOR") Then
                encontrado = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_DELTA_Constructor.DBFindByfk_Reportefk_Tipo_ProcesoImaging_fk_OTfk_Expedientefk_folderfk_fileFecha_Proceso_Ejecucion(CInt(Me._tipoReporteID), id_Tipo_Proceso, fk_Ot, CType(expediente, Long), CInt(folder), CInt(file), Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"))
            End If

            If (encontrado.Rows.Count > 0) Then
                For Each itemEncontrado In encontrado.Rows
                    Dim rowDelta As Object = Nothing
                    If (_nombreReporte = "DELTA_CONSTRUCTOR") Then
                        rowDelta = CType(itemEncontrado, DBIntegration.SchemaBCSCarpetaUnica.TBL_Report_DELTA_ConstructorRow)
                    Else
                        rowDelta = CType(itemEncontrado, DBIntegration.SchemaBCSCarpetaUnica.TBL_Report_DELTARow)
                    End If

                    'Columna Modificado en NULL  'Acutualizar primer registro
                    Dim idActualizar = rowDelta.id
                    Dim rowActualizar As Object = Nothing
                    If (_nombreReporte = "DELTA_CONSTRUCTOR") Then
                        rowActualizar = New DBIntegration.SchemaBCSCarpetaUnica.TBL_Report_DELTA_ConstructorType
                    Else
                        rowActualizar = New DBIntegration.SchemaBCSCarpetaUnica.TBL_Report_DELTAType
                    End If


                    Dim strInsertar As String = lt_40DELTA.First().Value.ToString().Replace("\\", "\")

                    If (Me.Quitar_SalidaDeltaImagen) Then
                        If (strInsertar.ToUpper().Contains("\SALIDA\DELTAIMAGEN")) Then
                            strInsertar = strInsertar.Replace("\SALIDA\DELTAIMAGEN", "").ToString()
                        End If
                    End If

                    rowActualizar.Modificado = "A"
                    rowActualizar.Path_Img = strInsertar
                    rowActualizar.NombreArchivo = nombreReportePrincipalDELTA + ".txt"

                    If (_nombreReporte = "DELTA") Then
                        rowActualizar.fk_Usuario_Actualiza = _Plugin.Manager.Sesion.Usuario.id
                        rowActualizar.Fecha_Usuario_Actualiza = DateTime.Now
                    End If

                    If (_nombreReporte = "DELTA_CONSTRUCTOR") Then
                        dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_DELTA_Constructor.DBUpdate(rowActualizar, CType(idActualizar, Long))
                    Else
                        dbmIntegration.SchemaBCSCarpetaUnica.TBL_Report_DELTA.DBUpdate(rowActualizar, CType(idActualizar, Long))
                    End If


                Next

                Me.contadorGlobalImagenes += 1
                Me.BackgroundWorkerReport.ReportProgress(0, "Reporta Progress")
                If (Me.contadorGlobalImagenes Mod 20 = 0) Then
                    Utilities.ClearMemory()
                End If

            End If
        End Sub

        Private Sub EjecutaBCP()
            Dim PathTemp As String = System.IO.Path.GetTempPath()
            Const quote = """"
            Dim strBcp = "bcp " + quote + " SELECT * FROM [DB_Miharu.Integration].[BCSCarpetaUnica].[TBL_File_Folio_Replica]" + quote + " queryout " + PathTemp + "bco_co_02.txt -S10.64.18.59 -T -c "
            Dim pathFull = PathTemp + "bcp.bat"

            Using w As New StreamWriter(pathFull)
                w.WriteLine(strBcp)
                w.Close()
            End Using

            Dim processInfo As New ProcessStartInfo()
            processInfo.WindowStyle = ProcessWindowStyle.Hidden
            processInfo.FileName = pathFull
            Dim process__1 = Process.Start(processInfo)
            process__1.WaitForExit()
            Me._dtImagesTodas = New DataTable()
            _dtImagesTodas.Columns.Add("fk_Expediente")
            _dtImagesTodas.Columns.Add("fk_folder")
            _dtImagesTodas.Columns.Add("fk_file")
            _dtImagesTodas.Columns.Add("fk_version")
            _dtImagesTodas.Columns.Add("fk_File_Record_Folio")
            _dtImagesTodas.Columns.Add("Base64Str")


            If process__1.HasExited Then
                process__1.Dispose()

                Using str As New System.IO.StreamReader(PathTemp + "bco_co_02.txt", System.Text.Encoding.Default)
                    While Not str.EndOfStream
                        Dim Linea = str.ReadLine().ToString()
                        Dim split = Linea.ToString().Split(ControlChars.Tab)
                        _dtImagesTodas.Rows.Add(split.ToArray())
                    End While
                End Using

            End If
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
                            w.Write(strMessageComplete + " Date : " + Date.Now.ToString())
                        End Using
                    End Using
                ElseIf (LeerFile) Then
                    modeFile = FileMode.Append
                    Using fs As New FileStream(pathStrFile, modeFile)
                        Using w As New BinaryWriter(fs)
                            w.Write(strMessageComplete + " Date : " + Date.Now.ToString())
                        End Using
                    End Using
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Function RellenarString(ByVal StrValor As String, ByVal Caracter As Char, ByVal Alineacion As Char, ByVal CantidadRellenar As Integer) As String
            Dim strValor_Result As String = ""

            Try
                'Right
                If (Alineacion = "r") Then
                    strValor_Result = StrValor.PadRight(CantidadRellenar, Caracter)
                ElseIf (Alineacion = "l") Then 'Left
                    strValor_Result = StrValor.PadLeft(CantidadRellenar, Caracter)
                End If
            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error, RellenarString " + ex.Message + " - " + DateTime.Now, False, True)
            End Try

            Return strValor_Result
        End Function

        Private Sub CargaDataTable_ConSeparador(ByRef Data As DataTable, Optional ByVal PathFileName As String = "")
            'Se realiza el cargue del archivo en un datatable para luego validarlo.
            objCSV.LoadCSV(PathFileName, False)
            Data = objCSV.DataTable.ToDataTable()

        End Sub

        Private Sub Habilitar_Controles()

            'Habilitar/Deshabilitar textbox y boton seleccionar rotulo
            ArchivoRotulosTextBox.Enabled = ArchivoRotulosCheckBox.Checked
            ArchivoRotulosSelectButton.Enabled = ArchivoRotulosCheckBox.Checked

        End Sub

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

            'Dim DataFechaDiff = data.Select("[Fecha de Proceso] <> '" + dtpFechaProceso.Value.ToString("yyyy/MM/dd") + "'")

            'If DataFechaDiff.Count() > 0 Then
            '    Rta_ValidaRotulo.Result = False
            '    Rta_ValidaRotulo.msj = "El archivo contiene Rótulos de fechas diferentes a la seleccionada, no se puede continuar con el proceso de generación"
            '    Return Rta_ValidaRotulo
            'End If

            Return Rta_ValidaRotulo

        End Function

#End Region

#Region " Eventos "

        Private Sub FRM_Reporte_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Cargar_Reportes()

            Habilitar_Controles()
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
            'La validacion aplica tambien para generacion por archivo de rotulos.
            If Validar() Then
                'Reinicia variables
                _fechaProceso = ""
                _tipoReporteID = ""
                _rutaGenerar = ""
                _rutaStartProcess = ""
                _rotuloDelta = ""
                Dim _horaDeltaReport As Integer = 0
                Dim _minutosDeltaReport As Integer = 0
                Dim _segundosDeltaReport As Integer = 0
                _ltRutasComprimir = New Dictionary(Of String, String)
                _ltTapasDoe = New List(Of String)
                _ltTapasPasivo = New List(Of String)
                _DirCarpeta_TapasDoe.Clear()
                Me.Dir_Eliminar.Clear()
                ContadorTapas = 0
                _NombreZipActual_DELTA = ""
                Me._nombreReporte = Me.ReporteDesktopComboBox.Text
                Me.TapasGeneradas = False
                Me.contadorArchivosPlanos = 0
                Me.TiempoInicial = Nothing
                Me.SegundosTotales = 0
                Me.contadorGlobalImagenes = 0
                Me.cantidadTotalImg = 0

                'Mensaje Generar Reporte / Generar Reporte Rotulos
                If DesktopMessageBoxControl.DesktopMessageShow(IIf(Not ArchivoRotulosCheckBox.Checked, "Desea generar el reporte: " & ReporteDesktopComboBox.SelectedText & ", de la fecha: " & dtpFechaProceso.Text & "?", "Desea Generar el Reporte del archivo de Rotulos cargado?"), "Generar Reporte", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False) = Windows.Forms.DialogResult.OK Then

                    Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
                    AbreConexionIntegration(dbmIntegration, _Plugin.Manager.Sesion.Usuario.id)
                    Try
                        dtPeriodicidad = dbmIntegration.SchemaBCSCarpetaUnica.PA_Config_Get_Periodicidad.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), CType(ReporteDesktopComboBox.SelectedValue, Integer))
                    Catch ex As Exception
                        EscribeLog(Me._StrArchivoLog, "Error, TraeReporte " + ex.Message + " - " + DateTime.Now, False, True)
                    Finally

                    End Try

                    Try

                        If dtPeriodicidad.Rows.Count() > 0 Or ArchivoRotulosCheckBox.Checked Then
                            Me._tipoReporteID = Me.ReporteDesktopComboBox.SelectedValue.ToString()
                            Me._rutaGenerar = Me.RutaTextBox.Text
                            Dim NombreReporte As String = ""
                            Dim rutaFinal As String = ""
                            Me._rutaStartProcess = Me._rutaGenerar + "\SALIDA\"
                            Me._StrArchivoLog = Me._rutaGenerar + "\Log_REPORTE_BCS_" + Date.Now.ToString("yyyyMMdd") + "_" + Date.Now.Hour.ToString("00") + Date.Now.Minute.ToString("00") + Date.Now.Second.ToString("00") + ".dat"
                            Me._ComprimioParticiones = False

                            'Para crear la carpeta toma la fecha del drop de fecha Proceso. Cuando Es por archivo de rotulos la fecha del drop se modificó por la actual.
                            If (Me._nombreReporte = "DELTA" Or Me._nombreReporte = "DELTA_EMPRESARIAL" Or Me._nombreReporte = "DELTA_CONSTRUCTOR") Then
                                Me._fechaProceso = Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd")
                            Else
                                Me._fechaProceso = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")
                            End If

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

                            Me.ProgressBarExportacíon.Visible = True
                            Me.ProgressBarExportacionGeneral.Visible = True
                            Me.lblTitleProcess.Visible = True
                            Me.lblTitleProcess.Text = "Exportación de Archivos..."
                            Me.DeshabilitaControles(False)
                            Me.lblTimeEstimate.Text = "00:00:00"
                            Me.TimerReport.Enabled = True
                            Me.lblTimeEstimate.Visible = True
                            Me.ControlBox = False
                            Me._TipoReporteSeleced = CInt(Me.ProcesoDesktopComboBox.SelectedValue)
                            Dim Quitar_SalidaDeltaImagenDT = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "Quitar_SalidaDeltaImagen")
                            If (Quitar_SalidaDeltaImagenDT.Rows.Count > 0) Then
                                Me.Quitar_SalidaDeltaImagen = CBool(Quitar_SalidaDeltaImagenDT.Rows(0)("Valor_Parametro_Sistema").ToString())
                            End If

                            EscribeLog(_StrArchivoLog, "LOG DE REPORTE PARA FECHA " + Date.Now.ToString(), True, False)

                            If (Me.dcListadoReportes.Count = 0) Then
                                For Each itemReporte As DataRowView In ReporteDesktopComboBox.Items
                                    Me.dcListadoReportes.Add(itemReporte(0).ToString(), itemReporte(1).ToString())
                                Next
                            End If

                            'Trae los procesos dependiendo de lo seleccionado.
                            dtProcesosPublicados = dbmIntegration.SchemaBCSCarpetaUnica.PA_Trae_ReportesPublicados.DBExecute(Me.dtpFechaProceso.Value.ToString("yyyyMMdd").ToString(), Me._TipoReporteSeleced, Nothing)
                            If (dtProcesosPublicados.Rows.Count > 0 Or ArchivoRotulosCheckBox.Checked) Then
                                If (Not Me.BackgroundWorkerReport.IsBusy) Then
                                    Me._nombreReporte = Me.ReporteDesktopComboBox.Text
                                    Me.BackgroundWorkerReport.RunWorkerAsync()
                                End If
                            Else
                                DesktopMessageBoxControl.DesktopMessageShow("Error, no hay reportes para generar en Fecha Proceso: " + Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"), "Generacion de Reportes", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)

                                EscribeLog(Me._StrArchivoLog, "Error, no hay reportes para generar en FechaProceso" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd"), False, True)
                                Me._ltErroresReporte.Add("Error, no hay reportes para generar en FechaProceso" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd"))
                                DeshabilitaControles(True)
                                Me.ProgressBarExportacíon.Visible = False
                                Me.ProgressBarExportacionGeneral.Visible = False
                                Me.lblProgresoGeneral.Visible = False
                                Me.lblTimeEstimate.Visible = False
                                Me.lblTitleProcess.Visible = False
                                Me.TimerReport.Enabled = False
                                Return
                            End If
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("Reportes seleccionados no vigentes o sin configuracion de Periodicidad", "Generacion de Reportes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                        End If
                    Catch ex As Exception
                        EscribeLog(Me._StrArchivoLog, "Error, TraeReporte " + ex.Message + " - " + DateTime.Now, False, True)
                    Finally
                        CierraConexionIntegration(dbmIntegration)
                    End Try

                End If
            End If
        End Sub

        Private Sub SelectFolderButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectFolderButton.Click
            SelectFolderPath()
        End Sub

        Private Sub RutaTextBox_Enter(sender As Object, e As System.EventArgs) Handles RutaTextBox.Enter
            'SelectFolderPath()
        End Sub

        Private Sub RutaTextBox_Click(sender As System.Object, e As System.EventArgs) Handles RutaTextBox.Click
            SelectFolderPath()
        End Sub

        Private Sub ArchivoRotulosTextBox_Click(sender As Object, e As System.EventArgs) Handles ArchivoRotulosTextBox.Click
            SelectFilePath()
        End Sub


        Private Sub SelectArchivoRotulosButton_Click(sender As Object, e As System.EventArgs) Handles ArchivoRotulosSelectButton.Click
            SelectFilePath()
        End Sub

        Private Sub BackgroundWorkerReport_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorkerReport.DoWork
            fechaGeneracionArchivo = Me.dtpFechaProceso.Value.ToString()
            Me.TiempoInicial = Date.Now
            CheckForIllegalCrossThreadCalls = False
            VisibleImagenes(True)
            Me.dtResultReportDELTA.Clear()
            Me.dtResultReportDELTA_Constructor.Clear()

            Dim bw = CType(sender, System.ComponentModel.BackgroundWorker)
            If (bw.CancellationPending) Then
                e.Cancel = True
            Else
                Dim ExtensionGenerar As DataTable
                Dim NombreReporte As String = Me._nombreReporte
                Dim DTResultReport As New DataTable
                Dim rutaFinal As String = ""
                Dim dbmIntegration As New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                Dim viewPeriodicidad As DataView
                viewPeriodicidad = dtPeriodicidad.DefaultView

                Me.ProgressBarExportacionGeneral.Maximum = (dcListadoReportes.Count() + 1)
                Me.ProgressBarExportacionGeneral.Minimum = 0
                Me.ProgressBarExportacionGeneral.Value = 0
                Dim contadorReportesGenerados As Integer = 0

                Try
                    AbreConexionIntegration(dbmIntegration, _Plugin.Manager.Sesion.Usuario.id)

                    If (Me._tipoReporteID = "-1") Then
                        For Each itemDic In dcListadoReportes
                            contadorReportesGenerados += 1
                            If (itemDic.Key <> "-1") Then
                                NombreReporte = itemDic.Value.ToString()
                                _tipoReporteID = itemDic.Key.ToString()
                                Me._nombreReporte = itemDic.Value.ToString()
                                viewPeriodicidad.RowFilter = "id_reporte = " & _tipoReporteID &
                                                        " And (Fecha_Dia = '" & Me.dtpFechaProceso.Value.ToString("yyyyMMdd") & "'" &
                                                        " Or Fecha_Dia_Habil = '" & Me.dtpFechaProceso.Value.ToString("yyyyMMdd") & "'" &
                                                        " Or Fecha_Semana = '" & Me.dtpFechaProceso.Value.ToString("yyyyMMdd") & "')"

                                If viewPeriodicidad.Count > 0 Or ArchivoRotulosCheckBox.Checked Then
                                    If (NombreReporte = "DELTA" Or NombreReporte = "DELTA_EMPRESARIAL" Or NombreReporte = "DELTA_CONSTRUCTOR" Or NombreReporte = "SIPLA") Then
                                        Me._fechaProceso = Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd")
                                    Else
                                        Me._fechaProceso = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")
                                    End If

                                    Me.BackgroundWorkerReport.ReportProgress(0, "Exportando Reporte " + itemDic.Value.ToString())
                                    EscribeLog(Me._StrArchivoLog, "Exportando Reporte " + itemDic.Value.ToString() + " - " + DateTime.Now, False, True)
                                    ExtensionGenerar = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBGet(_tipoReporteID)
                                    rutaFinal = Me._rutaGenerar + "\SALIDA\" + ExtensionGenerar.Rows(0)("Carpeta_Generacion").ToString()

                                    If (NombreReporte.ToUpper() = "DELTA" Or NombreReporte.ToUpper() = "DELTA_EMPRESARIAL" Or NombreReporte.ToUpper() = "DELTA_CONSTRUCTOR") Then
                                        Dim dateActual = Date.Now

                                        'Genera TXT Temporales ,Genera Repositorio Imagenes_DELTA Temporales
                                        'Me.BackgroundWorkerReport.ReportProgress(0, "Generando Repositorio de TXT e Imagenes Temporales...esta operación puede tardar unos minutos. ")
                                        'Dim Result = dbmIntegration.SchemaBCSCarpetaUnica.PA_TraeImagenesBase64_BCPTXT.DBExecute("\\10.65.54.158\Compartida RAUV\0123\", Me.dtpFechaProceso.Value.ToString("yyyyMMdd"))


                                        Me._NombreZipActual_DELTA = "\SALIDA\DELTAIMAGEN\DELTAIMAGEN_" + Me.dtpFechaProceso.Value.ToString("yyyyMMdd") + "_" + fechaGeneracionArchivo.Hour.ToString("00") + fechaGeneracionArchivo.Minute.ToString("00") + fechaGeneracionArchivo.Second.ToString("00")

                                        VisibleImagenes(True)
                                        If (Genera_ReporteDELTA(ExtensionGenerar, DTResultReport, _tipoReporteID, rutaFinal, DevuelveNombreReporte(_tipoReporteID, DTResultReport), dbmIntegration) = False) Then
                                            Continue For
                                        End If
                                        VisibleImagenes(False)

                                        Dim nombreComprimir_1 = DevuelveNombreReporte(Me._tipoReporteID, DTResultReport, True)
                                        If (Not Me._ltRutasComprimir.Keys.Contains(nombreComprimir_1) And Not Me._ltRutasComprimir.Values.Contains(rutaFinal)) Then
                                            Me._ltRutasComprimir.Add(nombreComprimir_1, rutaFinal)
                                        End If


                                        nombreComprimir_1 = "DELTAIMAGEN_" + Me.fechaGeneracionArchivo.ToString("yyyyMMdd") + "_" + Me.fechaGeneracionArchivo.Hour.ToString("00") + Me.fechaGeneracionArchivo.Minute.ToString("00") + Me.fechaGeneracionArchivo.Second.ToString("00")
                                        If (Not Me._ltRutasComprimir.Keys.Contains(nombreComprimir_1) And Not Me._ltRutasComprimir.Values.Contains(rutaFinal.Remove(rutaFinal.LastIndexOf("\"), rutaFinal.Length - rutaFinal.LastIndexOf("\")) + "\DELTAIMAGEN")) Then
                                            Me._ltRutasComprimir.Add(nombreComprimir_1, rutaFinal.Remove(rutaFinal.LastIndexOf("\"), rutaFinal.Length - rutaFinal.LastIndexOf("\")) + "\DELTAIMAGEN")
                                        End If
                                    Else
                                        DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_SeleccionaReporte().DBExecute(Me._fechaProceso, CInt(itemDic.Key.ToString()), Me._nombreReporte, Me._TipoReporteSeleced, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)
                                        CrearDirectorio(rutaFinal, False)
                                        If (NombreReporte = "GORO") Then
                                            If (DTResultReport.Columns.Count > 0 And DTResultReport.Columns.Contains("FechaProceso")) Then
                                                DTResultReport.Columns.Remove("FechaProceso")
                                            End If
                                        End If
                                        SeleccionExtensionGenerar(ExtensionGenerar, rutaFinal, DevuelveNombreReporte(_tipoReporteID, DTResultReport), DTResultReport, dbmIntegration)

                                        If DTResultReport.Rows.Count = 0 Then
                                            EscribeLog(Me._StrArchivoLog, "Error, no hay datos en BD para generar Reporte : " & NombreReporte & ", de la fecha: " & _fechaProceso, False, True)
                                            Me.BackgroundWorkerReport.ReportProgress(0, "Error, no hay datos en BD para generar Reporte : " & NombreReporte & ", de la fecha: " & _fechaProceso)
                                        End If
                                    End If

                                    Dim nombreComprimir = DevuelveNombreReporte(Me._tipoReporteID, DTResultReport, True)
                                    If (Not Me._ltRutasComprimir.Keys.Contains(nombreComprimir) And Not Me._ltRutasComprimir.Values.Contains(rutaFinal)) Then
                                        Me._ltRutasComprimir.Add(nombreComprimir, rutaFinal)
                                    End If
                                End If
                            End If
                            Me.ProgressBarExportacionGeneral.Value = contadorReportesGenerados
                            Me.lblProgresoGeneral.Text = "Progreso General (" + CInt(((contadorReportesGenerados / Me.dcListadoReportes.Count) * 100)).ToString() + "%)"
                        Next
                    ElseIf ((NombreReporte.ToUpper() = "DELTA" Or NombreReporte.ToUpper() = "DELTA_EMPRESARIAL" Or NombreReporte.ToUpper() = "DELTA_CONSTRUCTOR") And viewPeriodicidad.Count() > 0) Then
                        'Genera Imagenes DELTA

                        Me._fechaProceso = Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd")
                        Dim dateActual = Date.Now
                        'Generacion Rotulos: No se deshabilita la funcion de Zip.
                        Me._NombreZipActual_DELTA = "\SALIDA\DELTAIMAGEN\DELTAIMAGEN_" + Me.fechaGeneracionArchivo.ToString("yyyyMMdd") + "_" + fechaGeneracionArchivo.Hour.ToString("00") + fechaGeneracionArchivo.Minute.ToString("00") + fechaGeneracionArchivo.Second.ToString("00")

                        'llenar DTResultReport.
                        If Not ArchivoRotulosCheckBox.Checked = True Then
                            'ACTUAL

                            DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_SeleccionaReporte().DBExecute(Me._fechaProceso, Me._tipoReporteID, Me._nombreReporte, Me._TipoReporteSeleced, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)
                        Else
                            'Archivo Rotulos
                            Try
                                If _Rotulos.Columns.IndexOf("REGISTRO") <> -1 Then
                                    _Rotulos.Columns.Remove("REGISTRO")
                                End If
                                BulkInsert.InsertDataTableReport(_Rotulos, dbmIntegration, "#TMP_ROTULO")
                            Catch ex As Exception

                            End Try


                            'Llamar SPs de seleccion de rotulos.
                            DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_SeleccionaReporte_Rotulo.DBExecute(Me._fechaProceso, Me._tipoReporteID, Me._nombreReporte, Me._TipoReporteSeleced, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)

                            'Eliminar temporal
                            BulkInsert.DropTempTable(dbmIntegration, "#TMP_ROTULO")

                            If Not ValidarRotulos(DTResultReport).Result Then
                                Me._ltErroresReporte.Add(Rta_ValidaRotulo.msj + " - " + DateTime.Now)
                                Exit Sub
                            End If

                        End If


                        ExtensionGenerar = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBGet(Me._tipoReporteID)
                        If (ExtensionGenerar.Rows.Count > 0) Then
                            rutaFinal = Me._rutaGenerar + "\SALIDA\" + ExtensionGenerar.Rows(0)("Carpeta_Generacion").ToString()
                        End If
                        CrearDirectorio(rutaFinal, False)

                        ' Cambio JP 09/09/2017
                        'VisibleImagenes(True)
                        'If (Not Genera_ReporteDELTA(ExtensionGenerar, DTResultReport, Me._tipoReporteID, rutaFinal, DevuelveNombreReporte(_tipoReporteID, DTResultReport), dbmIntegration)) Then
                        '    Exit Sub
                        'End If
                        'VisibleImagenes(False)


                        VisibleImagenes(True)
                        If (Genera_ReporteDELTA(ExtensionGenerar, DTResultReport, _tipoReporteID, rutaFinal, DevuelveNombreReporte(_tipoReporteID, DTResultReport), dbmIntegration) = False) Then
                            Exit Sub
                        End If
                        VisibleImagenes(False)

                        Dim nombreComprimir = DevuelveNombreReporte(Me._tipoReporteID, DTResultReport, True)
                        If (Not Me._ltRutasComprimir.Keys.Contains(nombreComprimir) And Not Me._ltRutasComprimir.Values.Contains(rutaFinal)) Then
                            Me._ltRutasComprimir.Add(nombreComprimir, rutaFinal)
                        End If
                    Else
                        Me._fechaProceso = Me.dtpFechaProceso.Value.ToString("yyyyMMdd")

                        If Me._nombreReporte = "SIPLA" Then
                            Me._fechaProceso = Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd")
                        End If

                        If Not ArchivoRotulosCheckBox.Checked = True Then
                            'ACTUAL

                            DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_SeleccionaReporte().DBExecute(Me._fechaProceso, CInt(Me._tipoReporteID), Me._nombreReporte, Me._TipoReporteSeleced, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)

                        Else
                            'Archivo Rotulos
                            Try
                                If _Rotulos.Columns.IndexOf("REGISTRO") <> -1 Then
                                    _Rotulos.Columns.Remove("REGISTRO")
                                End If
                                BulkInsert.InsertDataTableReport(_Rotulos, dbmIntegration, "#TMP_ROTULO")
                            Catch ex As Exception

                            End Try

                            'Llamar SPs de seleccion de rotulos.
                            DTResultReport = dbmIntegration.SchemaBCSCarpetaUnica.PA_Report_SeleccionaReporte_Rotulo.DBExecute(Me._fechaProceso, Me._tipoReporteID, Me._nombreReporte, Me._TipoReporteSeleced, Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)

                            'Eliminar temporal
                            BulkInsert.DropTempTable(dbmIntegration, "#TMP_ROTULO")

                            If Not ValidarRotulos(DTResultReport).Result Then
                                Me._ltErroresReporte.Add(Rta_ValidaRotulo.msj + " - " + DateTime.Now)
                                Exit Sub
                            End If

                        End If


                        If DTResultReport.Rows.Count = 0 Then
                            EscribeLog(Me._StrArchivoLog, "Error, no hay datos en BD para generar Reporte : " & NombreReporte & ", de la fecha: " & _fechaProceso, False, True)
                            Me.BackgroundWorkerReport.ReportProgress(0, "Error, no hay datos en BD para generar Reporte : " & NombreReporte & ", de la fecha: " & _fechaProceso)
                        End If

                        ExtensionGenerar = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBGet(Me._tipoReporteID)
                        rutaFinal = Me._rutaGenerar + "\SALIDA\" + ExtensionGenerar.Rows(0)("Carpeta_Generacion").ToString()
                        CrearDirectorio(rutaFinal, False)
                        If (NombreReporte = "GORO") Then
                            DTResultReport.Columns.Remove("FechaProceso")
                        End If

                        SeleccionExtensionGenerar(ExtensionGenerar, rutaFinal, DevuelveNombreReporte(Me._tipoReporteID), DTResultReport, dbmIntegration)

                        Dim NombreReporteBD = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Reporte.DBGet(CShort(Me._tipoReporteID))
                        If (DTResultReport.Rows.Count = 0) Then
                            Me.BackgroundWorkerReport.ReportProgress(0, "Error, no hay datos en BD para generar Reporte : " & NombreReporteBD.Rows(0)("Nombre_Reporte") & ", de la fecha: " & _fechaProceso)
                            EscribeLog(Me._StrArchivoLog, "Error, no hay datos en BD para generar Reporte : " & NombreReporteBD.Rows(0)("Nombre_Reporte") & ", de la fecha: " & _fechaProceso, False, True)
                        End If



                        Dim nombreComprimir = DevuelveNombreReporte(Me._tipoReporteID, DTResultReport, True)
                        If (Not Me._ltRutasComprimir.Keys.Contains(nombreComprimir) And Not Me._ltRutasComprimir.Values.Contains(rutaFinal)) Then
                            Me._ltRutasComprimir.Add(nombreComprimir, rutaFinal)
                        End If
                    End If

                    'No se generan tapas para Archivo de Rotulos
                    If Not ArchivoRotulosCheckBox.Checked Then
                        GeneraTapas()
                    End If

                    If (Me._ltRutasComprimir.Count > 0) Then
                        contadorReportesGenerados += 1
                        For Each rutaComprimir In Me._ltRutasComprimir
                            If (rutaComprimir.Value.Contains("\DELTAIMAGEN") Or rutaComprimir.Value.Contains("GORO_ACUSES_DE_RECIBIDO_")) Then
                                ComprimirArchivos(rutaComprimir.Value, rutaComprimir.Key, Nothing, False, True, {"*.txt"})
                            Else
                                ComprimirArchivos(rutaComprimir.Value, rutaComprimir.Key, Nothing, True)
                            End If
                        Next
                        Me.ProgressBarExportacionGeneral.Value = contadorReportesGenerados
                    End If



                    'Actualizar a Publicados los Reportes en Tabla Control
                    'Cuando se genera por archivo de Rotulos, NO hay que modificar tabla de control proceso.
                    If Not ArchivoRotulosCheckBox.Enabled Then
                        For index = 0 To dtProcesosPublicados.Rows.Count - 1
                            Dim ProcesoActual_aux = dtProcesosPublicados.Rows(index)("fk_Tipo_Proceso")
                            Dim NombreProcesoActual = dtProcesosPublicados.Rows(index)("Nombre_Tipo_Proceso")
                            Dim idControlProceso = dtProcesosPublicados.Rows(index)("id_Control_Proceso")
                            Dim RegistroControl_Actualizar As New DBIntegration.SchemaBCSCarpetaUnica.TBL_Control_ProcesoType
                            RegistroControl_Actualizar.Generado = True
                            dbmIntegration.SchemaBCSCarpetaUnica.TBL_Control_Proceso.DBUpdate(RegistroControl_Actualizar, CType(idControlProceso, Long))
                        Next

                        'If (DesktopMessageBoxControl.DesktopMessageShow("¿Desar Cifrar los datos de la Fecha Proceso Anterior?", "Cifrado", DesktopMessageBoxControl.IconEnum.WarningIcon, False) = Windows.Forms.DialogResult.OK) Then
                        Dim cifrados As frmProcesosCifrados = New frmProcesosCifrados(Me._Plugin, Me.dtpFechaProceso.Value.ToString("yyyy/MM/dd"))
                        cifrados.ShowDialog()
                        'End If
                    End If




                Catch ex As Exception
                    EscribeLog(Me._StrArchivoLog, "Error, Genera Reporte " + ex.Message + " - " + DateTime.Now, False, True)
                    Me._ltErroresReporte.Add("Error, Genera Reporte " + ex.Message + " - " + DateTime.Now)
                Finally
                    If (dbmIntegration.DataBase.Connection.State = ConnectionState.Open) Then
                        CierraConexionIntegration(dbmIntegration)
                    End If
                End Try
            End If
            Me.Cursor = Cursors.Default
        End Sub


        Private Sub BackgroundWorkerReport_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorkerReport.ProgressChanged

            If (Me.contadorGlobalImagenes < Me.ProgressBarExportacíon.Maximum) Then
                Me.ProgressBarExportacíon.Value = Me.contadorGlobalImagenes
            End If

            If (Me._nombreReporte = "DELTA" Or Me._nombreReporte = "DELTA_CONSTRUCTOR" Or Me._nombreReporte = "DELTA_EMPRESARIAL") Then
                Me.lblImgProcesadas.Text = "Imagenes Procesadas: " + Me.contadorGlobalImagenes.ToString("#,##0")
                If (Me.SegundosTotales > 0) Then
                    Dim varTotal = (Me.SegundosTotales * (Me.cantidadTotalImg - Me.contadorGlobalImagenes)) / (Me.contadorGlobalImagenes)
                    varTotal = (varTotal / 60)
                    Me.lblTiempoEstimado.Text = "Tiempo Restante: " + varTotal.ToString("#,##0") + " minutos"
                End If

                If (MsjFinal <> "") Then
                    Dim split = Me.MsjFinal.ToString().Split(",")
                    Me.lblCantidadImgs.Text = "Cantidad Total IMG - Reporte " + Me._nombreReporte + ": " + CInt(split(1)).ToString("#,##0")
                    Me.lblProcesoActual.Text = "Proceso Actual: " + split(2)
                End If
            End If

            Me.lblTitleProcess.Visible = True
            Me.lblTimeEstimate.Text = Me._horaDeltaReport.ToString("00") + ":" + Me._minutosDeltaReport.ToString("00") + ":" + Me._segundosDeltaReport.ToString("00")
            If (e.UserState <> "") Then
                If (e.UserState.ToString().Length > 50) Then
                    Dim values = e.UserState.ToString.Split(Environment.NewLine)
                    Dim auxPartido As String
                    If (values.Count > 1) Then
                        If (e.UserState.ToString().Length > 60) Then
                            Dim percent = CInt(e.UserState.ToString().Length * 0.6)
                            auxPartido = values(0) + "..." + Environment.NewLine + e.UserState.ToString().Remove(0, percent)
                        Else
                            auxPartido = e.UserState.ToString()
                        End If
                    Else
                        If (e.UserState.ToString().Length > 60) Then
                            Dim percent = CInt(e.UserState.ToString().Length * 0.6)
                            auxPartido = e.UserState.ToString().Remove(0, percent)
                        Else
                            auxPartido = e.UserState.ToString()
                        End If
                    End If


                    Me.lblTitleProcess.Text = auxPartido.Replace("\\", "\")
                    EscribeLog(Me._StrArchivoLog, auxPartido.Replace("\\", "\"), False, True)
                End If
            End If

        End Sub

        Private Sub BackgroundWorkerReport_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorkerReport.RunWorkerCompleted
            Me.ProgressBarExportacíon.Visible = False
            Me.ProgressBarExportacionGeneral.Visible = False
            Me.lblProgresoGeneral.Visible = False
            Me.lblTitleProcess.Visible = False
            Me.DeshabilitaControles(True)
            Me.TimerReport.Enabled = False
            Me.lblTimeEstimate.Visible = False
            Me.dcListadoReportes.Clear()
            Me.ControlBox = True
            If (Me._ltErroresReporte.Count > 0) Then
                Dim Errores As String = ""

                For Each itemError In Me._ltErroresReporte
                    Errores = Errores + Environment.NewLine + itemError
                Next

                If (ReporteDesktopComboBox.SelectedValue = "-1") Then
                    Errores = "Reportes generados pero con error en: " + Environment.NewLine + Errores
                End If

                DesktopMessageBoxControl.DesktopMessageShow(Errores, "Errores de Reporte", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Me._ltErroresReporte.Clear()
            Else
                DesktopMessageBoxControl.DesktopMessageShow("Reportes Generados con exito" + Environment.NewLine + " Duración: " + Me.lblTimeEstimate.Text, "Reportes Generados", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                System.Diagnostics.Process.Start(Me._rutaStartProcess)


            End If

        End Sub

        Private Sub TimerReport_Tick(sender As System.Object, e As System.EventArgs) Handles TimerReport.Tick
            Me._segundosDeltaReport += CInt((Me.TimerReport.Interval / 1000))
            Me.SegundosTotales = SegundosTotales + CInt((Me.TimerReport.Interval / 1000))

            If (_segundosDeltaReport = 60) Then
                Me._minutosDeltaReport += 1
                Me._segundosDeltaReport = 0
            End If

            If (Me._minutosDeltaReport = 60) Then
                Me._horaDeltaReport += 1
                Me._minutosDeltaReport = 0
            End If
            Me.BackgroundWorkerReport.ReportProgress(0, "Reporta Progress")
        End Sub

        Private Sub FRM_Reporte_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
            Me.TimerReport.Stop()
            Me.TimerReport.Enabled = False
            Me.BackgroundWorkerReport.CancelAsync()
            Me.BackgroundWorkerReport.Dispose()
            Me.BackgroundWorkerReport = Nothing
        End Sub

        Private Sub ArchivoRotulosCheckBox_CheckedChanged(sender As Object, e As System.EventArgs) Handles ArchivoRotulosCheckBox.CheckedChanged
            Cargar_Combo_Reportes()
            Habilitar_Controles()


        End Sub

#End Region





    End Class

    Public Class LocalDomainManager

        Private NewDomain As AppDomain = Nothing
        Private NewController As ImageExport = Nothing

        Private NewServidor As CTA_ServidorSimpleType
        Private NewCentro As CTA_Centro_ProcesamientoSimpleType
        Private NewConnectionString As String
        Private NewUsuario As Integer
        Private Counter As Integer



        Public Sub New(servidor As CTA_ServidorSimpleType, centro As CTA_Centro_ProcesamientoSimpleType, connectionString As String, usuario As Integer)
            NewServidor = servidor
            NewCentro = centro
            NewConnectionString = connectionString
            NewUsuario = usuario
            Counter = 10

            ManageController()
        End Sub

        Public Sub Save(FileNames As List(Of String), FileName As String, nCompresion As ImageManager.EnumCompression, nFileFolderName As String)
            ManageController()
            NewController.Save(FileNames, FileName, nCompresion, nFileFolderName)
        End Sub

        Public Sub Save(nInputImages As List(Of FreeImageAPI.FreeImageBitmap), nOutputFileName As String, nSuffixFormat As String, nFormat As EnumFormat, nCompression As EnumCompression, nSinglePage As Boolean, nTempPath As String)
            ManageController()
            NewController.Save(nInputImages, nOutputFileName, nSuffixFormat, nFormat, nCompression, nSinglePage, nTempPath)
        End Sub

        Public Sub Save(FileName As String, Imagen As Byte())
            ManageController()
            NewController.Save(FileName, Imagen)
        End Sub

        Private Sub ManageController()
            Counter += 1

            If (Counter >= 10) Then
                If (NewDomain IsNot Nothing) Then
                    ' Descargar el dominio secundario
                    Try : NewController.Unload() : Catch : End Try
                    Try : AppDomain.Unload(NewDomain) : Catch : End Try
                End If

                NewDomain = CreateDommain()
                NewController = GetController()
                NewController.Init(NewServidor, NewCentro, NewConnectionString, NewUsuario)
                Counter = 0
                Application.DoEvents()
            End If
        End Sub


        Private Shared Function CreateDommain() As AppDomain
            ' Crear las configuraciones del nuevo AppDomain.
            Dim ads = New AppDomainSetup()
            ads.ApplicationBase = Environment.CurrentDirectory
            ads.DisallowBindingRedirects = False
            ads.DisallowCodeDownload = True
            ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile

            ' Crear el nuevo AppDomain.
            Return AppDomain.CreateDomain("Domain-" & Guid.NewGuid().ToString(), Nothing, ads)
        End Function

        Private Function GetController() As ImageExport
            Dim tipo = GetType(ImageExport)
            Return CType(NewDomain.CreateInstanceAndUnwrap(tipo.Assembly.FullName, tipo.FullName), ImageExport)
        End Function

        Public Sub GetFolio(nAnexo As Long, nFolio As Short, ByRef nImagen As Byte())
            NewController.GetFolio(nAnexo, nFolio, nImagen)
        End Sub

        Public Sub GetFolio(nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, ByRef nImagen As Byte())
            NewController.GetFolio(nExpediente, nFolder, nFile, nVersion, nFolio, nImagen)
        End Sub

    End Class

End Namespace
