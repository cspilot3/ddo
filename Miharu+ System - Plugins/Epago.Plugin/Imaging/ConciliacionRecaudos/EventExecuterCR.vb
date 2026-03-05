Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Library.Config
Imports Miharu.FileProvider.Library
Imports DBCore.SchemaImaging
Imports Slyg.Tools.Imaging
Imports System.Drawing
Imports BarcodeLib.BarcodeReader
Imports System.Windows.Forms
Imports Slyg.Tools
Imports System.IO
Imports System.Globalization
Imports System.Linq

Namespace Imaging.ConciliacionRecaudos
    Public Class EventExecuterCR
        Inherits EventExecuter
        Implements IEventExecuter


#Region " Declaraciones "

        ' ReSharper disable once NotAccessedField.Local
        Private _Plugin As ConciliacionRecaudosPlugin

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As ConciliacionRecaudosPlugin)
            Me._Plugin = nPlugin
        End Sub

#End Region

#Region " Implementacion IEventExecuter "

        Public Sub EliminarImagen(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short) Implements IEventExecuter.EliminarImagen

        End Sub

        Public Sub FinalizarCargue(ByVal nidCargue As Integer) Implements IEventExecuter.FinalizarCargue

        End Sub

        Public Sub FinalizarIndexacion(ByVal nidCargue As Integer, ByVal nidPaquete As Short) Implements IEventExecuter.FinalizarIndexacion
            Dim progressFormEsp As New Progress.FormProgress
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing
            Dim NextEstado As Short
            Dim Leidos As Short = 0
            Dim CountFile As Short = 0

            '*** datatable para actualizar codigo de barras del file.. no por folio
            Dim FoliosCodigosBarrasdt As New DataTable
            FoliosCodigosBarrasdt.Columns.Add("fk_Expediente")
            FoliosCodigosBarrasdt.Columns.Add("fk_Folder")
            FoliosCodigosBarrasdt.Columns.Add("fk_File")
            FoliosCodigosBarrasdt.Columns.Add("fk_Documento")
            FoliosCodigosBarrasdt.Columns.Add("CodigoBarras")
            Dim FoliosCodigosBarrasrow As DataRow = FoliosCodigosBarrasdt.NewRow()
            '***

            'Return

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.EpagoConnectionString)

                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)


                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_Plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType
                manager = New FileProviderManager(servidor, centro, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                manager.Connect()

                Dim DashBoardCapturasDt = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBFindByfk_Carguefk_Cargue_Paquete(nidCargue, nidPaquete)


                Dim cBarrasAnterior As String = "**********"
                Dim folio As Short
                Dim Expedientes As Integer = 0
                Dim TotalExpedientes As Integer = DashBoardCapturasDt.Count()
                Dim Folios As Short

                '-----------------------------------------------------------------------------------------------------------------
                progressFormEsp.Process = ""
                progressFormEsp.Action = ""
                progressFormEsp.ValueProcess = 0
                progressFormEsp.ValueAction = 0
                progressFormEsp.Show()
                Application.DoEvents()
                '-----------------------------------------------------------------------------------------------------------------

                Dim actualizarFolder As Boolean = True

                'Extrae datos en un Loop a la tabla DashBoardCapturasDt
                For Each CarguePaqueteFileRow As DBImaging.SchemaProcess.TBL_Dashboard_CapturasRow In DashBoardCapturasDt

                    '-----------------------------------------------------------------------------------------------------------------
                    Expedientes += 1
                    progressFormEsp.MaxValueProcess = TotalExpedientes
                    progressFormEsp.Process = "Procesar Expediente " & Expedientes & " de " & TotalExpedientes
                    progressFormEsp.ValueProcess = Expedientes
                    Dim fileImagingDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(CarguePaqueteFileRow.fk_Expediente, CarguePaqueteFileRow.fk_Folder, CarguePaqueteFileRow.fk_File, Nothing)
                    Dim FileImagingRow = fileImagingDataTable(fileImagingDataTable.Count - 1)
                    Dim Filefk_Documento = CarguePaqueteFileRow.fk_Documento
                    Dim FileTipoCodigoBarrasDataTable = dbmIntegration.SchemaEPago.TBL_Documento_Codigo_Barras.DBFindByfk_Documento(Filefk_Documento)
                    Folios = manager.GetFolios(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                    '-----------------------------------------------------------------------------------------------------------------
                    progressFormEsp.ValueAction = 0
                    progressFormEsp.MaxValueAction = Folios
                    progressFormEsp.BringToFront()
                    Application.DoEvents()


                    If FileTipoCodigoBarrasDataTable.Count() > 0 Then
                        Dim FileTipoCodigoBarrasRow = FileTipoCodigoBarrasDataTable(0)
                        Dim CodigosFolder As New List(Of String)
                        Dim codigosFolderIndex As Integer = 0
                        Dim ActualizarFileData As Boolean = True
                        Dim Codigos As List(Of String) = New List(Of String)
                        Dim actualizarCapturaFile As Boolean = False
                        Dim CountEAN As Integer = 13

                        'Recorrido por Files
                        For index = 1 To fileImagingDataTable.Count
                            'Recorrido por Folios
                            For folio = CShort(1) To Folios
                                Dim imagen() As Byte = Nothing
                                Dim thumbnail() As Byte = Nothing
                                ' Leer folio actual
                                manager.GetFolio(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, folio, imagen, thumbnail)
                                Dim ms = New MemoryStream(imagen)
                                Dim bm = New FreeImageAPI.FreeImageBitmap(ms)
                                Dim CodigoAux = BarcodeReader.read(bm, BarcodeReader.CODE128)
                                If (CodigoAux IsNot Nothing) Then
                                    For Each CodAuxItem In CodigoAux
                                        Codigos.Add(CodAuxItem)
                                    Next
                                End If
                            Next

                            Try
                                'Captura los campos del Documento (si hay mas de dos Codigos de Barras Para al estado captura)
                                    If Codigos.Count >= 2 Or Codigos.Count = 0 Then
                                        'Estado Captura
                                        NextEstado = CShort(dbmImaging.SchemaProcess.PA_Calcular_Siguiente_Estado().DBExecute(FileTipoCodigoBarrasRow.fk_Documento, 32))

                                        'Actualiza Cargue en DashBoard
                                        If (NextEstado = DBCore.EstadoEnum.Indexado) Then
                                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                                        Else
                                            Dim CapDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                                            CapDashboardType.fk_Usuario_log = DBNull.Value
                                            CapDashboardType.Sesion = DBNull.Value
                                            CapDashboardType.fk_Estado = NextEstado
                                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(CapDashboardType, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                                    End If

                                        GoTo NextIteraciónFor
                                    Else
                                        Dim ValoresDeCampos As Dictionary(Of Integer, Object) = New Dictionary(Of Integer, Object)
                                        Dim CodigoEANAux = Codigos(0).Remove(0, 4)
                                        Dim CodigoEAN = CodigoEANAux.Substring(0, 13)
                                        Dim CodBarString As String() = Codigos(0).Split("?")
                                        Leidos += 1

                                        For Each itemTipoDoc In FileTipoCodigoBarrasDataTable
                                            Dim IdentificadorCampo = itemTipoDoc.Identificador
                                            Dim IdCampo = itemTipoDoc.fk_Campo
                                            Dim ValorAux As Object = Nothing

                                            Select Case (IdentificadorCampo)
                                                Case "?8020"
                                                    'Captura Referencia
                                                    ValorAux = CodBarString(1).Remove(0, 20)
                                                Case "?3900"
                                                    'Captura Valor$
                                                    Dim ValorAuxFirst = CodBarString(2).Remove(0, 4)
                                                    ValorAux = Decimal.Parse(ValorAuxFirst)
                                                Case "?415"
                                                    'Captura EAN
                                                Case "?96"
                                                    'Captura la Fecha
                                                    Dim ValorAuxFecha = CodBarString(3).Remove(0, 2)
                                                    Dim VarDate As DateTime
                                                    If DateTime.TryParseExact(ValorAuxFecha, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, VarDate) Then
                                                        ValorAux = VarDate.ToShortDateString()
                                                    End If
                                                Case Else
                                                    'Identificador ?3902 Para Doc EMCALI (Campo Referencia)
                                                    Dim ValorAuxFirst = CodBarString(2).Remove(0, 4)
                                                    ValorAuxFirst = ValorAuxFirst.Remove(ValorAuxFirst.Length - 2, 2)
                                                    ValorAux = Decimal.Parse(ValorAuxFirst)
                                            End Select

                                            ValoresDeCampos.Add(IdCampo, ValorAux)
                                        Next

                                        If (ValoresDeCampos.Count > 0) Then
                                            For Each ValorCampo In ValoresDeCampos
                                                'Guardar en File Data
                                                Dim FileDataType As New DBCore.SchemaProcess.TBL_File_DataType()
                                                FileDataType.fk_Expediente = FileImagingRow.fk_Expediente
                                                FileDataType.fk_Folder = FileImagingRow.fk_Folder
                                                FileDataType.fk_File = FileImagingRow.fk_File
                                                FileDataType.fk_Documento = Filefk_Documento
                                                FileDataType.fk_Campo = ValorCampo.Key
                                                FileDataType.Valor_File_Data = ValorCampo.Value
                                            FileDataType.Conteo_File_Data = ValorCampo.Value.ToString().Length
                                            Dim EncontradoFileData = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, Filefk_Documento, ValorCampo.Key)
                                            If (EncontradoFileData.Rows.Count = 0) Then
                                                dbmCore.SchemaProcess.TBL_File_Data.DBInsert(FileDataType)
                                            End If
                                            Next
                                        End If
                                    End If

                            Catch ex As Exception
                                DesktopMessageBoxControl.DesktopMessageShow("Error al extraer campos!!", ex)
                            End Try
                            progressFormEsp.Action = "Leer Codigo de Barras en Imagen " & folio & " de " & Folios
                            progressFormEsp.ValueAction = folio
                            Application.DoEvents()
                        Next

                        NextEstado = CShort(dbmImaging.SchemaProcess.PA_Calcular_Siguiente_Estado().DBExecute(FileTipoCodigoBarrasRow.fk_Documento, 31))
                        '---------------------------------------------------------------------------
                        ' Actualizar Dashboard
                        '---------------------------------------------------------------------------
                        If (NextEstado = DBCore.EstadoEnum.Indexado) Then
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                        Else
                            Dim CapDashboardType = New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                            CapDashboardType.fk_Usuario_log = DBNull.Value
                            CapDashboardType.Sesion = DBNull.Value
                            CapDashboardType.fk_Estado = NextEstado
                            dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(CapDashboardType, FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File)
                        End If
                        ''---------------------------------------------------------------------------
                    End If


                    ' Actualizar estados
                    'Crea el estado del Folder en Core


                    Dim InsertEstado As New DBCore.SchemaProcess.TBL_Folder_estadoType()
                    InsertEstado.fk_Expediente = FileImagingRow.fk_Expediente
                    InsertEstado.fk_Folder = FileImagingRow.fk_Folder
                    InsertEstado.Modulo = DesktopConfig.Modulo.Imaging
                    InsertEstado.fk_Estado = NextEstado
                    InsertEstado.fk_Usuario = _Plugin.Manager.Sesion.Usuario.id
                    InsertEstado.Fecha_Log = SlygNullable.SysDate
                    Dim EncontradoFolderEstado = dbmCore.SchemaProcess.TBL_Folder_estado.DBFindByfk_Expedientefk_FolderModulo(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, CShort(DesktopConfig.Modulo.Imaging))
                    If (EncontradoFolderEstado.Rows.Count = 0) Then
                        dbmCore.SchemaProcess.TBL_Folder_estado.DBInsert(InsertEstado)
                    End If

NextIteraciónFor:
                    Continue For
                Next

                ' Actualizar el estado de los cargues que ya fueron procesados
                Try
                    dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                    Dim CarguePaqueteFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Paquete_Estado_File.DBFindByid_Cargueid_Cargue_Paquete(nidCargue, nidPaquete)
                    If (CarguePaqueteFileDataTable.Count > 0) Then
                        Dim UpdatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                        UpdatePaquete.fk_Estado = CarguePaqueteFileDataTable(0).Estado_File
                        dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(UpdatePaquete, nidCargue, nidPaquete)

                        Dim CargueFileDataTable = dbmImaging.SchemaProcess.CTA_Cargue_Estado.DBFindByid_Cargue(nidCargue)
                        If (CargueFileDataTable.Count > 0) Then
                            Dim UpdateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                            UpdateCargue.fk_Estado = CargueFileDataTable(0).Estado_File
                            dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(UpdateCargue, nidPaquete)
                        End If
                    End If
                    dbmImaging.Transaction_Commit()
                Catch ex As Exception
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    Throw
                End Try
                MessageBox.Show("Se leyeron " & Leidos & " codigos de barras de " & TotalExpedientes & " documentos")

            Catch ex As Exception
                Throw New Exception("Error en Finalización de Cargue" + ex.Message)
            Finally

                progressFormEsp.Visible = False
                progressFormEsp.Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Public Sub FinalizarReIndexacion(nidCargue As Integer, nidPaquete As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReIndexacion

        End Sub

        Public Sub ValidarSavePrimeraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSavePrimeraCaptura

        End Sub

        Public Sub ValidarSaveSegundaCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveSegundaCaptura

        End Sub

        Public Sub ValidarSaveTerceraCaptura(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveTerceraCaptura

        End Sub

        Public Sub ValidarSaveCalidad(ByVal campos As List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), ByVal fk_documento As Integer, ByRef Result As Boolean) Implements IEventExecuter.ValidarSaveCalidad

        End Sub

        Public Sub FinalizarPrecaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarPreCaptura
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                Dim NextEstado As Integer
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                NextEstado = DBCore.EstadoEnum.Indexado

                'Actualizar estado File
                Dim UpdateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                UpdateEstado.Fecha_Log = SlygNullable.SysDate
                UpdateEstado.fk_Usuario = _Plugin.Manager.Sesion.Usuario.id
                UpdateEstado.fk_Estado = NextEstado
                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(UpdateEstado, nidExpediente, nidFolder, nidFile, DesktopConfig.Modulo.Imaging)

                If (NextEstado = DBCore.EstadoEnum.Indexado) Then
                    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBDelete(nidExpediente, nidFolder, nidFile)
                End If

            Catch ex As Exception
                Throw New Exception("Error en Finalización de Cargue" + ex.Message)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarPrimeraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarPrimeraCaptura

        End Sub

        Public Sub FinalizarSegundaCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarSegundaCaptura

        End Sub

        Public Sub FinalizarTerceraCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarTerceraCaptura

        End Sub

        Public Sub FinalizarCalidad(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarCalidad

        End Sub

        Public Sub FinalizarValidaciones(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarValidaciones

        End Sub

        Public Sub FinalizarReclasificar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarReclasificar

        End Sub

        Public Sub EnviarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.EnviarReproceso

        End Sub

        Public Sub FinalizarReproceso(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarReproceso

        End Sub

        Public Sub AbrirFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer) Implements IEventExecuter.AbrirFechaProceso

        End Sub

        Public Sub AbrirOt(ByVal nidOt As Integer) Implements IEventExecuter.AbrirOt

        End Sub

        Public Sub ValidarCerrarFechaProcesoNoOT(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, ByRef nValido As Boolean, ByRef nValido2 As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarCerrarFechaProcesoNoOT

        End Sub

        Public Sub CerrarFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer) Implements IEventExecuter.CerrarFechaProceso

        End Sub

        Public Sub CerrarOt(ByVal nidOt As Integer) Implements IEventExecuter.CerrarOt

        End Sub

        Public Sub CrearFechaProceso(ByVal nidEntidadProcesamiento As Short, ByVal nidEntidad As Short, ByVal nidProyecto As Short, ByVal nidFechaProceso As Integer) Implements IEventExecuter.CrearFechaProceso

        End Sub

        Public Sub CrearOt(ByVal nidOt As Integer) Implements IEventExecuter.CrearOt

        End Sub

        Public Sub FinalizarRecorte(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarRecorte

        End Sub

        Public Sub FinalizarPrecinto() Implements IEventExecuter.FinalizarPrecinto

        End Sub

        Public Sub ValidarEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Short, ByVal nidEsquema As Short, ByVal nToken As String, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.ValidarEmpaque

        End Sub

        Public Sub FinalizarProcesoAdicionalCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarProcesoAdicionalCaptura

        End Sub

        Public Sub GuardarContenedor(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nidContenedor As Integer) Implements IEventExecuter.GuardarContenedor

        End Sub

        Public Sub ValidarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal ndbImaging As DBImaging.DBImagingDataBaseManager, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.ValidarPrecintoEmpaque

        End Sub

        Public Sub FinalizarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer) Implements IEventExecuter.FinalizarPrecintoEmpaque

        End Sub

        Public Sub CargarPrecinto(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.CargarPrecinto

        End Sub

        Public Sub FinalizarEliminarPrecinto(ByVal nidOt As Integer, ByVal nidEmpaque As Integer) Implements IEventExecuter.FinalizarEliminarPrecinto

        End Sub

        Public Sub ValidarSaveLabelCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short, ByVal TipoCaptura As String, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveLabelCaptura

        End Sub

        Public Sub ValidarActualizarDatoBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short, ByVal newValor_File_Data As Object, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarActualizarDatoBusqueda

        End Sub

        Public Sub FinalizarActualizarDatosBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarActualizarDatosBusqueda

        End Sub

        Public Sub Ejecutar_Cruce_En_Linea(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Ejecutar_Cruce_En_Linea

        End Sub

        Public Sub FinalizarLoadConfig(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, ByVal FileCoreRow As DBCore.SchemaProcess.TBL_FileRow) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarLoadConfig

        End Sub

        Public Sub FinalizarContenedorEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal IdEmpaqueContenedor As Integer) Implements IEventExecuter.FinalizarContenedorEmpaque

        End Sub

        Public Sub FinalizarContenedorEmpaqueEliminar(ByVal nidOt As Integer, ByVal ntoken As String) Implements IEventExecuter.FinalizarContenedorEmpaqueEliminar

        End Sub

        Public Sub Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short) Implements IEventExecuter.Reprocesar
        End Sub

        Public Sub Validar_Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidDocumento As Integer, ByVal nidCampo As Integer, ByVal nidCampoTablaAsociada As Integer, ByVal nesCampo As Boolean, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.Validar_Reprocesar

        End Sub

        Public Function Nombre_Imagen_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nFk_Documento As Integer, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String Implements IEventExecuter.Nombre_Imagen_Exportar
            Return String.Empty
        End Function

        Public Function ExtensionImagen_Plugin(ByVal Entrada As Boolean) As String Implements IEventExecuter.ExtensionImagen_Plugin
            Return String.Empty
        End Function

        Public Function IdFormatoImagen_Plugin(ByVal Entrada As Boolean) As Short Implements IEventExecuter.IdFormatoImagen_Plugin
            Return -1
        End Function

        Public Sub FinalizarCrucegenerico(nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, nidOT As Integer) Implements IEventExecuter.FinalizarCruceGenerico

        End Sub

        Function Nombre_Imagen_Agrupada_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String Implements IEventExecuter.Nombre_Imagen_Agrupada_Exportar
            Return String.Empty
        End Function

        Public Sub FinalizarCorreccionCapturaMaquina(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCorreccionCapturaMaquina

        End Sub

        Public Sub FinalizarPrimeraCapturaAnexo(nidAnexo As Long) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrimeraCapturaAnexo

        End Sub

        Public Sub FinalizarTerceraCapturaAnexo(nidAnexo As Long) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarTerceraCapturaAnexo

        End Sub

        Public Sub FinalizarPrepararDatagenerico(nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, nidOT As Integer) Implements IEventExecuter.FinalizarPrepararDataGenerico

        End Sub
#End Region

    End Class
End Namespace
