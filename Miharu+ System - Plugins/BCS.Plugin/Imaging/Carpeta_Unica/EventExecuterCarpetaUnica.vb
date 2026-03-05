Imports Miharu.Imaging.Library.Eventos
Imports Miharu.Desktop.Library.Plugins
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Library.Config
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports Slyg.Tools.Imaging
Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Imaging.Library.Procesos.Empaque
Imports BarcodeLib.BarcodeReader
Imports DBSofTrac

Namespace Imaging.Carpeta_Unica
    Public Class EventExecuterCarpetaUnica
        Inherits EventExecuter
        Implements IEventExecuter

#Region " Declaraciones "

        ' ReSharper disable once NotAccessedField.Local
        Private _Plugin As CarpetaUnicaPlugin

#End Region

#Region " Constructores "

        Public Sub New(ByVal nPlugin As CarpetaUnicaPlugin)
            Me._Plugin = nPlugin
        End Sub

#End Region

#Region " Implementacion IEventExecuter "

        Public Sub AbrirFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.AbrirFechaProceso

        End Sub

        Public Sub AbrirOt(nidOt As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.AbrirOt

        End Sub

        Public Sub ValidarCerrarFechaProcesoNoOT(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, ByRef nValido As Boolean, ByRef nValido2 As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarCerrarFechaProcesoNoOT
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim RegistrosPorEmpacar = dbmIntegration.SchemaBCSCarpetaUnica.PA_New_13_Validar_Cierre_Fecha_Proceso_Empaque.DBExecute(nidFechaProceso)
                If RegistrosPorEmpacar = "" Then

                    Dim Respuesta = dbmIntegration.SchemaBCSCarpetaUnica.PA_Destape_Vs_Empaque.DBExecute(nidFechaProceso, nidEntidad, nidProyecto)

                    If Respuesta <> "" Then
                        nValido2 = False
                        nMsgError = Respuesta.ToString()
                    End If
                Else
                    nValido = False
                    MessageBox.Show("No se puede cerrar la fecha de proceso puesto que hay " & RegistrosPorEmpacar.ToString(), "Cerrar Fecha Proceso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                nValido = False
                nValido2 = False
                nMsgError = ex.Message
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try



        End Sub

        Public Sub CerrarFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CerrarFechaProceso
            If nidFechaProceso > 0 Then
                Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
                Try
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
                    dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    'Actualiza procesos cruzados que fueron empacados.
                    dbmIntegration.SchemaBCSCarpetaUnica.PA_Actualiza_Cruzados_Empacados.DBExecute(nidFechaProceso, nidEntidad, nidProyecto)

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("Cerrar Fecha Proceso", ex)
                Finally
                    If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                End Try
            End If
        End Sub

        Public Sub CerrarOt(nidOt As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CerrarOt

        End Sub

        Public Sub CrearFechaProceso(nidEntidadProcesamiento As Short, nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CrearFechaProceso
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                If nidFechaProceso > 0 Then
                    dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
                    dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                    dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    Dim FechaProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_Proyectoid_fecha_proceso(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, nidFechaProceso)

                    If FechaProcesoDataTable.Rows.Count > 0 Then
                        If Not CType(FechaProcesoDataTable.Rows(0).Item("Cerrado"), Boolean) Then
                            Dim Respuesta = dbmIntegration.SchemaBCSCarpetaUnica.PA_Actualiza_Control_Proceso.DBExecute(nidFechaProceso)
                        End If
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FechaProceso", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

        End Sub

        Public Sub CrearOt(nidOt As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.CrearOt

        End Sub

        Public Sub EliminarImagen(nidExpediente As Long, nidFolder As Short, nidFile As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.EliminarImagen

        End Sub

        Public Sub EnviarReproceso(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.EnviarReproceso

        End Sub

        Public Sub FinalizarCalidad(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCalidad

        End Sub

        Public Sub FinalizarCargue(nidCargue As Integer) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarCargue

        End Sub

        Public Sub FinalizarIndexacion(nidCargue As Integer, nidPaquete As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarIndexacion
            ''******0.1 Pasar data de la máquina******
            'Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing
            'dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Me._Plugin.SofTracConnectionString)
            'dbmSofTrac.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            'Try
            '    dbmSofTrac.SchemaProcess.PA_Transmision_Data_Miharu.DBExecute(nidCargue, nidPaquete)
            'Catch ex As Exception
            '    MessageBox.Show("Error finalizando indexación transmitiendo data SofTrac,  cargue: " + nidCargue.ToString + " , paquete" + nidPaquete.ToString() + ex.Message, "Error transmisión Data: máquina a Miharu", MessageBoxButtons.OK)
            '    'Throw New Exception("Error finalizando indexación transmitiendo data SofTrac: " + ex.Message)
            'Finally
            '    If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
            'End Try

            '****1. Obtener Datos Tapa Proceso****
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
                dbmIntegration.Connection_Open(Me._Plugin.Manager.Sesion.Usuario.id)
                dbmIntegration.SchemaBCSCarpetaUnica.PA_Obtiene_Datos_TapaProceso.DBExecute(Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, nidCargue, nidPaquete)
            Catch ex As Exception
            Finally
                dbmIntegration.Connection_Close()
            End Try

            '****2. Leer Codigo de barras (Label) del documento ****
            Dim progressFormEsp As New Progress.FormProgress
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Dim Leidos As Short = 0

            'Return

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)


                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_Plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType


                manager = New FileProviderManager(servidor, centro, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                manager.Connect()

                Dim DashBoardCapturasDt = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBFindByfk_Carguefk_Cargue_Paquete(nidCargue, nidPaquete)
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

                For Each CarguePaqueteFileRow As DBImaging.SchemaProcess.TBL_Dashboard_CapturasRow In DashBoardCapturasDt

                    '-----------------------------------------------------------------------------------------------------------------
                    Expedientes += 1
                    progressFormEsp.MaxValueProcess = TotalExpedientes
                    progressFormEsp.Process = "Procesar Expediente " & Expedientes & " de " & TotalExpedientes
                    progressFormEsp.ValueProcess = Expedientes



                    Dim fileImagingDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(CarguePaqueteFileRow.fk_Expediente, CarguePaqueteFileRow.fk_Folder, CarguePaqueteFileRow.fk_File, Nothing)

                    Dim FileImagingRow = fileImagingDataTable(fileImagingDataTable.Count - 1)

                    Dim Filefk_Documento = CarguePaqueteFileRow.fk_Documento

                    Dim Campos_Documento = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documentoid_Campo(Me._Plugin.Manager.ImagingGlobal.Entidad, Filefk_Documento, Nothing)
                    Dim fk_Campo As Integer

                    If Campos_Documento.Select("Nombre_Campo = 'LABEL'").Count > 0 Then 'Si no hay campos label continua con el siguiente

                        fk_Campo = Integer.Parse(Campos_Documento.Select("Nombre_Campo = 'LABEL'")(0)("id_Campo"))

                        Dim TipoCBarrasDataTable = dbmIntegration.SchemaConfig.TBL_Tipo_Cbarras.DBFindByid_Tipo_Cbarras(Nothing)
                        Dim FormatoCBarrasDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Config_Formato_Label.DBFindByid_Formato_Label(Nothing)

                        Folios = manager.GetFolios(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version)

                        '-----------------------------------------------------------------------------------------------------------------
                        progressFormEsp.ValueAction = 0
                        progressFormEsp.MaxValueAction = Folios
                        progressFormEsp.BringToFront()
                        Application.DoEvents()

                        If TipoCBarrasDataTable.Count() > 0 Then



                            'Dim TipoCodigoBarrasRow = TipoCodigoBarrasDataTable(0)
                            'Dim FileTipoCodigoBarras = TipoCodigoBarrasDataTable.
                            Dim CodigosFolder As New List(Of String)
                            Dim codigosFolderIndex As Integer = 0
                            Dim ActualizarFileData As Boolean = True

                            Dim Codigos() As String
                            Dim actualizarCapturaFile As Boolean = False
                            Dim CodigoReconocido As String = ""

                            For folio = CShort(1) To Folios

                                Dim imagen() As Byte = Nothing
                                Dim thumbnail() As Byte = Nothing

                                Try
                                    ' Leer folio actual
                                    manager.GetFolio(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, FileImagingRow.id_Version, folio, imagen, thumbnail)

                                    Dim ms = New MemoryStream(imagen)
                                    Dim bm = New FreeImageAPI.FreeImageBitmap(ms)
                                    Dim FinalizaReconoceCbarras As Boolean = False
                                    Dim Formato() As String
                                    Dim ContadorCumpleFormato As Integer



                                    For Each CodigoBarrasRow In TipoCBarrasDataTable


                                        'TipoCodigoBarras
                                        Select Case CodigoBarrasRow.Tipo_Cbarras
                                            Case "CODE128"
                                                Codigos = BarcodeReader.read(bm, BarcodeReader.CODE128)
                                            Case "CODE39"
                                                Codigos = BarcodeReader.read(bm, BarcodeReader.CODE39)
                                        End Select

                                        If Not Codigos Is Nothing Then

                                            For Each codigo In Codigos
                                                For Each FormatoCBarrasRow As DBIntegration.SchemaBCSCarpetaUnica.TBL_Config_Formato_LabelRow In FormatoCBarrasDataTable.Select("fk_Tipo_CBarras = " & CodigoBarrasRow.id_Tipo_Cbarras)
                                                    If FormatoCBarrasRow.fk_Tipo_CBarras <> CodigoBarrasRow.id_Tipo_Cbarras Then
                                                        'Si el Formato del Formato del codigo de barras no Es el Tipo de codigo de barras actual Continuar con el siguiente formato
                                                        Exit For
                                                    End If

                                                    Dim desde As Integer = 0
                                                    Dim hasta As Integer = 0

                                                    Formato = Split(FormatoCBarrasRow.Formato, "|")

                                                    ContadorCumpleFormato = 0

                                                    For Each format_ In Formato
                                                        If ContadorCumpleFormato < Array.IndexOf(Formato, format_) Then
                                                            'Formato.IndexOf(format_) Then
                                                            'Si la primera validacion no se cumple, salir.
                                                            Exit For
                                                        End If
                                                        If desde = 0 And codigo.StartsWith("?") Then
                                                            'Si el primer caracter es un signo ?, se quita de la cadena.
                                                            codigo = codigo.Substring(1, codigo.Length - 1)
                                                        End If
                                                        If format_.Contains("<T>") Then

                                                            hasta = hasta + format_.Replace("<T>", "").Length

                                                            If codigo.Length >= hasta Then
                                                                If codigo.Substring(desde, hasta - desde) = format_.Replace("<T>", "") Then
                                                                    ContadorCumpleFormato = ContadorCumpleFormato + 1
                                                                End If
                                                            End If

                                                        End If

                                                        If format_.Contains("<N>") Then

                                                            hasta = hasta + Integer.Parse(format_.Replace("<N>", ""))

                                                            If codigo.Length <= hasta Then
                                                                If IsNumeric(codigo.Substring(desde, hasta - desde)) Then
                                                                    ContadorCumpleFormato = ContadorCumpleFormato + 1
                                                                End If
                                                            End If

                                                        End If

                                                        If ContadorCumpleFormato = Formato.Count And codigo.Length = hasta Then
                                                            'Si cumple todos los formatos encontrados y la longitud. Salir y guardar.
                                                            CodigoReconocido = codigo
                                                            Exit For
                                                        End If

                                                        desde = hasta

                                                    Next

                                                    If CodigoReconocido <> "" Then
                                                        Exit For
                                                    End If

                                                Next

                                                If CodigoReconocido <> "" Then
                                                    Exit For
                                                End If

                                            Next

                                        End If

                                        If CodigoReconocido <> "" Then
                                            Exit For
                                        End If

                                    Next

                                Catch ex As Exception

                                End Try

                                progressFormEsp.Action = "Leer Codigo de Barras en Imagen " & folio & " de " & Folios
                                progressFormEsp.ValueAction = folio
                                Application.DoEvents()

                                If CodigoReconocido <> "" Then
                                    Exit For
                                End If

                            Next


                            If CodigoReconocido <> "" Then
                                'Guardar en File Data

                                Leidos += 1

                                Dim FileDataType As New DBCore.SchemaProcess.TBL_File_DataType()

                                If dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(FileImagingRow.fk_Expediente, FileImagingRow.fk_Folder, FileImagingRow.fk_File, Filefk_Documento, fk_Campo).Count = 0 Then
                                    FileDataType.fk_Expediente = FileImagingRow.fk_Expediente
                                    FileDataType.fk_Folder = FileImagingRow.fk_Folder
                                    FileDataType.fk_File = FileImagingRow.fk_File
                                    FileDataType.fk_Documento = Filefk_Documento
                                    FileDataType.fk_Campo = fk_Campo
                                    FileDataType.Valor_File_Data = CodigoReconocido
                                    FileDataType.Conteo_File_Data = CodigoReconocido.Length
                                    dbmCore.SchemaProcess.TBL_File_Data.DBInsert(FileDataType)
                                End If


                            End If
                        End If

                    End If

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
                Throw New Exception("Error en Finalización de Cargue: " + ex.Message)
            Finally

                progressFormEsp.Visible = False
                progressFormEsp.Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try

            '****'3. Logica de Capturas para nuevos y adiciones****

            dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            Try
                dbmIntegration.SchemaBCSCarpetaUnica.PA_Procesa_Captura_Files.DBExecute(nidCargue, nidPaquete)
            Catch ex As Exception
                Throw New Exception("Error en Finalización de Cargue: " + ex.Message)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

        End Sub

        Public Sub FinalizarPreCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPreCaptura

        End Sub

        Public Sub FinalizarPrecinto() Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrecinto

        End Sub

        Public Sub FinalizarPrimeraCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarPrimeraCaptura
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_00_Cruce_En_Linea_Cola.DBExecute(nidExpediente, nidFolder, nidFile, "1")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Primera", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarReclasificar(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReclasificar
       
        End Sub

        Public Sub FinalizarRecorte(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarRecorte

        End Sub

        Public Sub FinalizarReIndexacion(nidCargue As Integer, nidPaquete As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReIndexacion

        End Sub

        Public Sub FinalizarReproceso(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarReproceso

            'Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing
            'dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Me._Plugin.SofTracConnectionString)
            'dbmSofTrac.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            'Try
            '    dbmSofTrac.SchemaProcess.PA_Transmision_Data_Miharu_Expediente.DBExecute(nidExpediente, nidFolder, nidFile)
            'Catch ex As Exception
            '    Throw New Exception("Error transmitiendo data SofTrac: " + ex.Message)
            'Finally
            '    If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
            'End Try
        End Sub

        Public Sub FinalizarSegundaCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarSegundaCaptura
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)


                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)


                dbmIntegration.SchemaBCSCarpetaUnica.PA_Finalizar_Segunda_Captura.DBExecute(nidExpediente, nidFolder, nidFile)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Reprocesar", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarTerceraCaptura(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarTerceraCaptura
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_00_Cruce_En_Linea_Cola.DBExecute(nidExpediente, nidFolder, nidFile, "3")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Tercera", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarValidaciones(nidExpediente As Long, nidFolder As Short, nidFile As Short, nidVersion As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarValidaciones

            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)
            dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            Try
                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_00_Finalizar_Validaciones.DBExecute(nidExpediente, nidFolder, nidFile)
            Catch ex As Exception
                Throw New Exception("Error insertando validaciones: " + ex.Message)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub ValidarEmpaque(nidOt As Integer, nidEmpaque As Short, nidEsquema As Short, nToken As String, ByRef nValido As Boolean, ByRef nMsgError As String) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarEmpaque

            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                ' Validar si ya se realizó el cierre
                'Dim cruces = dbmIntegration.SchemaBCSCarpetaUnica.PA_New_12_Existe_Cruce.DBExecute(nidOt, nidEmpaque, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)
                'If (cruces = 0) Then Throw New Exception("No se puede realizar el empaque si no se ha realizado el proceso de cruce")

                Dim valida = dbmIntegration.SchemaBCSCarpetaUnica.PA_New_10_Empaque_Validar_Token.DBExecute(nidOt, nidEmpaque, nToken)
                If (valida = 0) Then
                    Throw New Exception("EL Cod de barras: " & nToken & " no coincide con el tipo de empaque")
                ElseIf (valida = -1) Then
                    Throw New Exception("El Cod de barras: " & nToken & " no pertenece a la OT actual")
                End If

            Catch ex As Exception
                nValido = False
                nMsgError = ex.Message
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub ValidarSaveCalidad(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveCalidad

        End Sub

        Public Sub ValidarSavePrimeraCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSavePrimeraCaptura

        End Sub

        Public Sub ValidarSaveSegundaCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveSegundaCaptura

        End Sub

        Public Sub ValidarSaveTerceraCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveTerceraCaptura

        End Sub

        Public Sub FinalizarProcesoAdicionalCaptura(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nidVersion As Short) Implements IEventExecuter.FinalizarProcesoAdicionalCaptura
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_00_Cruce_En_Linea_Cola.DBExecute(nidExpediente, nidFolder, nidFile, "A")
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Adicional", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub GuardarContenedor(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nidContenedor As Integer) Implements IEventExecuter.GuardarContenedor
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim MostrarVentana = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(Me._Plugin.Manager.ImagingGlobal.Entidad, Me._Plugin.Manager.ImagingGlobal.Proyecto, "MostrarVentanaNovedades")(0).Valor_Parametro_Sistema

                If MostrarVentana = "1" Then
                    Dim CapturaTapa = New Forms.Destape.FormCapturaTapa(_Plugin, nidOt, nidPrecinto, nidContenedor)
                    CapturaTapa.Show()
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("FinalizarGuardarContenedor", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try


            
        End Sub

        Public Sub ValidarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal ndbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.ValidarPrecintoEmpaque
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim NumCaja As Integer = 0
            Dim TipoEmpaque As Integer = 0
            Dim NumCajaInicial As Integer = 0
            Dim TipoEmpaqueData As String = ""


            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                ' Validar si ya se realizó el cierre
                '                Dim cruces = dbmIntegration.SchemaBCSCarpetaUnica.PA_Existe_Cruce.DBExecute(nidOt)
                Dim Empaque_CampoDataTable = ndbmImaging.SchemaConfig.TBL_Empaque_Campo.DBFindByfk_Entidadfk_ProyectoNombre_CampoEliminado(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "Tipo Empaque", 0)

                If Empaque_CampoDataTable.Rows.Count > 0 Then
                    Dim EmpaqueDataTable = ndbmImaging.SchemaProcess.TBL_Empaque_Data.DBFindByfk_OTfk_Empaquefk_Campo(nidOt, nidEmpaque, CInt(Empaque_CampoDataTable(0).id_Campo))

                    If EmpaqueDataTable.Rows.Count > 0 Then
                        Dim ProcesoDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByNombre_Tipo_Proceso(EmpaqueDataTable(0).Data_Campo.ToString())

                        If ProcesoDataTable.Rows.Count > 0 Then
                            Dim id_Tipo_Proceso = ProcesoDataTable(0).id_Tipo_Proceso

                            Dim cruces = dbmIntegration.SchemaBCSCarpetaUnica.PA_New_12_Existe_Cruce.DBExecute(nidOt, id_Tipo_Proceso, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)
                            If (cruces = 0) Then Throw New Exception("No se puede realizar el empaque si no se ha realizado el proceso de cruce")
                        End If

                    End If
                End If

                Dim CampoTipoEmpaqueDataTable = ndbmImaging.SchemaConfig.TBL_Empaque_Campo.DBFindByfk_Entidadfk_ProyectoNombre_CampoEliminado(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "Tipo Empaque", False)

                If CampoTipoEmpaqueDataTable.Count > 0 Then
                    Dim TipoEmpaqueDataDataTable = ndbmImaging.SchemaProcess.TBL_Empaque_Data.DBFindByfk_OTfk_Empaquefk_Campo(nidOt, nidEmpaque, CShort(CampoTipoEmpaqueDataTable(0).id_Campo))

                    If TipoEmpaqueDataDataTable.Count > 0 Then
                        TipoEmpaqueData = TipoEmpaqueDataDataTable(0).Data_Campo

                        Dim TipoEmpaqueDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Tipo_Proceso.DBFindByNombre_Tipo_Proceso(CStr(TipoEmpaqueDataDataTable(0).Data_Campo))
                        If TipoEmpaqueDataTable.Count > 0 Then
                            TipoEmpaque = CInt(TipoEmpaqueDataTable(0).id_Tipo_Proceso)
                        Else
                            Throw New Exception("El tipo de empaque no esta relacionado a un tipo de proceso, Por favor verifique.")
                        End If
                    Else
                        Throw New Exception("No se encontró valor para el campo: Tipo Empaque, Por favor verifique.")
                    End If
                Else
                    Throw New Exception("El campo: Tipo Empaque, no está configurado para el proceso de empaque, Por favor verifique.")
                End If

                Dim CampoNumCajaDataTable = ndbmImaging.SchemaConfig.TBL_Empaque_Campo.DBFindByfk_Entidadfk_ProyectoNombre_CampoEliminado(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "Numero de Caja", False)

                If CampoNumCajaDataTable.Count > 0 Then
                    Dim NumCajaDataTable = ndbmImaging.SchemaProcess.TBL_Empaque_Data.DBFindByfk_OTfk_Empaquefk_Campo(nidOt, nidEmpaque, CShort(CampoNumCajaDataTable(0).id_Campo))

                    If NumCajaDataTable.Count > 0 Then
                        NumCaja = CInt(NumCajaDataTable(0).Data_Campo)
                    Else
                        Throw New Exception("No se encontró valor para el campo: Numero de Caja, Por favor verifique.")
                    End If
                Else
                    Throw New Exception("El campo: Numero de Caja, no está configurado para el proceso de empaque, Por favor verifique.")
                End If


                If (TipoEmpaque > 0 And NumCaja > 0) Then
                    Dim TBLCajaDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Caja.DBGet(Nothing)

                    If TBLCajaDataTable.Count = 0 Then
                        Dim ParametroDatatable = dbmIntegration.SchemaConfig.TBL_Parametro_Sistema.DBFindByfk_Entidadfk_ProyectoNombre_Parametro_Sistema(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, "Caja")
                        If ParametroDatatable.Count > 0 Then
                            NumCajaInicial = CInt(ParametroDatatable(0).Valor_Parametro_Sistema)
                            If NumCajaInicial <> NumCaja Then
                                Throw New Exception("El Numero de caja para iniciar debe ser: " + NumCajaInicial.ToString() + ", Por favor verifique.")
                            End If
                        Else
                            Throw New Exception("El parámetro: Caja, no está configurado para el proyecto, Por favor verifique.")
                        End If
                    ElseIf TBLCajaDataTable.Count > 0 Then
                        Dim CajaDataTable = dbmIntegration.SchemaBCSCarpetaUnica.TBL_Caja.DBGet(NumCaja)
                        If CajaDataTable.Count > 0 Then
                            If CajaDataTable(0).fk_Tipo_Proceso = TipoEmpaque Then
                                If (CajaDataTable(0).Cerrada) Then
                                    Throw New Exception("La caja: " + NumCaja.ToString() + ", esta cerada, Por favor verifique.")
                                End If
                            Else
                                Throw New Exception("La caja: " + NumCaja.ToString() + ", pertenece a otro proceso, Por favor verifique.")
                            End If

                        End If
                    End If
                End If


                If (nValido) Then
                    nContenedorDesktop.Text = TipoEmpaqueData.ToString()
                    nContenedorDesktop.Enabled = False
                End If

            Catch ex As Exception
                nValido = False
                nMsgError = ex.Message
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarPrecintoEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer) Implements IEventExecuter.FinalizarPrecintoEmpaque
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim Registros = dbmIntegration.SchemaBCSCarpetaUnica.PA_New_11B_Empaque_Finalizar_Precinto.DBExecute(nidOt, nidEmpaque, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)

                If Registros > 0 Then
                    MessageBox.Show("Quedan " & Registros.ToString() & " registros pendientes por empacar.", "Finalizar precinto empaque", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Finalizar precinto empaque", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub CargarPrecinto(ByVal nidOt As Integer, ByVal nidPrecinto As Integer, ByVal nContenedorDesktop As Miharu.Desktop.Controls.DesktopComboBox.DesktopComboBoxControl, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.CargarPrecinto
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim NumCaja As Integer = 0
            Dim TipoEmpaque As String = ""

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim CamposPrecintoDataTable = dbmIntegration.SchemaBCSCarpetaUnica.PA_Empaque_Traer_Campos_Precinto.DBExecute(nidOt, nidPrecinto)

                If CamposPrecintoDataTable.Count > 0 Then
                    TipoEmpaque = CamposPrecintoDataTable(0).Tipo_Empaque
                    NumCaja = CamposPrecintoDataTable(0).Num_Caja
                    If TipoEmpaque = "" Then
                        Throw New Exception("No se encontró valor para el campo: Tipo Empaque, Por favor verifique.")
                    End If

                    If NumCaja = 0 Then
                        Throw New Exception("No se encontró valor para el campo: Numero de Caja, Por favor verifique.")
                    End If

                    nContenedorDesktop.Text = TipoEmpaque
                    nContenedorDesktop.Enabled = False
                End If


            Catch ex As Exception
                nValido = False
                nMsgError = ex.Message
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarEliminarPrecinto(ByVal nidOt As Integer, ByVal nidEmpaque As Integer) Implements IEventExecuter.FinalizarEliminarPrecinto
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmIntegration.SchemaBCSCarpetaUnica.PA_Empaque_Finalizar_Eliminar_Precinto.DBExecute(nidOt, nidEmpaque, _Plugin.Manager.Sesion.Usuario.id)

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Finalizar Eliminar precinto empaque", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub ValidarSaveLabelCaptura(campos As System.Collections.Generic.List(Of Miharu.Imaging.Indexer.View.Indexacion.CampoCaptura), fk_documento As Integer, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short, ByVal TipoCaptura As String, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarSaveLabelCaptura
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim id_Campo As Int16
            Dim Label As String = ""

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim CamposDataTable = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documento(_Plugin.Manager.ImagingGlobal.Entidad, fk_documento)
                Dim drowCampoFiltrado = CamposDataTable.Select("Nombre_Campo = 'LABEL'")

                If (drowCampoFiltrado.Count > 0) Then
                    id_Campo = CType(drowCampoFiltrado(0).Item("id_Campo").ToString(), Integer)

                    'recorre cada campo para encontrar valor de label
                    For Each Item In campos
                        If Item.id = id_Campo Then
                            Label = Item.Control.Value
                        End If
                    Next
                    If Trim(Label) <> "" Then
                        Dim dtResult = dbmIntegration.SchemaBCSCarpetaUnica.PA_File_Label.DBExecute(Trim(Label), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, fk_Expediente, fk_Folder, fk_File, TipoCaptura)
                        If dtResult.Rows.Count > 0 Then
                            If dtResult.Rows(0).Item("TipoResultado").ToString = "ERROR" Then
                                DesktopMessageBoxControl.DesktopMessageShow(dtResult.Rows(0).Item("Mensaje").ToString(), "Guardar Captura", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                Result = False
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Adicional", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub ValidarActualizarDatoBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short, ByVal newValor_File_Data As Object, ByRef Result As Boolean) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.ValidarActualizarDatoBusqueda

            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim id_Campo As Int16
            Dim fk_ExpedienteAnt As Integer
            Dim fk_FolderAnt As Integer
            Dim Label As String = ""

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim FechaProcesoDataTable = dbmImaging.SchemaProcess.PA_FechaProceso_Expediente.DBExecute(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, fk_Expediente)

                If FechaProcesoDataTable.Rows.Count > 0 Then
                    If ((Convert.ToBoolean(FechaProcesoDataTable.Rows(0)("Cerrado"))) And _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Fecha_Proceso_Cerrada_F11 = False) Then
                        'Si la fecha de proceso se encuentra cerrada no permite modificacion en Busqueda - F11
                        DesktopMessageBoxControl.DesktopMessageShow("La Fecha de Proceso se encuentra cerrada, No es posible realizar modificación.", "Modificar Busqueda", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        Result = False
                    End If
                End If



                ' Validacion Label - Modificación

                Dim CamposDataTable = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documento(_Plugin.Manager.ImagingGlobal.Entidad, nCampo.fk_Documento)
                Dim drowCampoFiltrado = CamposDataTable.Select("Nombre_Campo = 'LABEL'")

                If (drowCampoFiltrado.Count > 0) Then
                    id_Campo = CType(drowCampoFiltrado(0).Item("id_Campo").ToString(), Integer)

                    If nCampo.fk_Campo = id_Campo Then



                        Dim dtLabel = dbmIntegration.SchemaBCSCarpetaUnica.TBL_File_Label.DBFindByid_Label(newValor_File_Data.ToString())

                        If dtLabel.Rows.Count > 0 Then

                            If dtLabel.Rows(0).Item("id_Label") <> "" Then
                                fk_ExpedienteAnt = dtLabel.Rows(0).Item("fk_Expediente")
                                fk_FolderAnt = dtLabel.Rows(0).Item("fk_Folder")

                                If (fk_Expediente <> fk_ExpedienteAnt Or fk_Folder <> fk_FolderAnt) Then
                                    DesktopMessageBoxControl.DesktopMessageShow("No es posible guardar!! Este Label ya ha sido utilizado en otro Documento.", "Modificar Busqueda", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                    Result = False
                                End If
                            End If

                        End If
                    End If
                End If

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Adicional", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarActualizarDatosBusqueda(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarActualizarDatosBusqueda

            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim id_Campo As Int16
            Dim Label As String = ""

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                ' Validacion Label - Modificación

                Dim CamposDataTable = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documento(_Plugin.Manager.ImagingGlobal.Entidad, nCampo.fk_Documento)
                Dim drowCampoFiltrado = CamposDataTable.Select("Nombre_Campo = 'LABEL'")

                If (drowCampoFiltrado.Count > 0) Then
                    id_Campo = CType(drowCampoFiltrado(0).Item("id_Campo").ToString(), Integer)

                    If nCampo.fk_Campo = id_Campo Then
                        Label = nCampo.Valor_File_Data

                        If Trim(Label) <> "" Then
                            Dim dtResult = dbmIntegration.SchemaBCSCarpetaUnica.PA_File_Label.DBExecute(Trim(Label), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, fk_Expediente, fk_Folder, fk_File, "M")
                            If dtResult.Rows.Count > 0 Then
                                If dtResult.Rows(0).Item("TipoResultado").ToString = "ERROR" Then
                                    DesktopMessageBoxControl.DesktopMessageShow(dtResult.Rows(0).Item("Mensaje").ToString(), "Modificación", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                                End If
                            End If
                        End If
                    End If

                End If

                ''Actualiza usuario que realiza modificacion
                'Dim TBL_File_Update As New DBImaging.SchemaProcess.TBL_FileType
                'TBL_File_Update.Usuario_Modifica_Correccion = _Plugin.Manager.Sesion.Usuario.id
                'TBL_File_Update.Fecha_Modifica_Correccion = SlygNullable.SysDate
                'dbmImaging.SchemaProcess.TBL_File.DBUpdate(TBL_File_Update, fk_Expediente, fk_Folder, fk_File, CShort(1))

                ''Actualiza Valor correccion
                'Dim TBL_File_Data_MC_Update As New DBImaging.SchemaProcess.TBL_File_Data_MCType
                'TBL_File_Data_MC_Update.Valor_Correccion = nCampo.Valor_File_Data
                'dbmImaging.SchemaProcess.TBL_File_Data_MC.DBUpdate(TBL_File_Data_MC_Update, fk_Expediente, fk_Folder, fk_File, CShort(1), nCampo.fk_Documento, nCampo.fk_Campo)

                'Actualiza Cruce en Linea
                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_00_Cruce_En_Linea_Cola.DBExecute(fk_Expediente, fk_Folder, fk_File, "C")

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Adicional", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try


        End Sub

        Public Sub Ejecutar_Cruce_En_Linea(ByVal nCampo As DBCore.SchemaImaging.CTA_Busqueda_Files_DataRow, ByVal fk_Expediente As Long, ByVal fk_Folder As Short, ByVal fk_File As Short) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.Ejecutar_Cruce_En_Linea

            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_00_Cruce_En_Linea_Cola.DBExecute(fk_Expediente, fk_Folder, fk_File, "C")

                DesktopMessageBoxControl.DesktopMessageShow("Registro actualizado para cruce.", "Cruce", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cruce en Linea", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

        End Sub

        Public Sub FinalizarLoadConfig(ByRef IndexerView As Miharu.Imaging.Indexer.View.Indexacion.IIndexerView, ByVal FileCoreRow As DBCore.SchemaProcess.TBL_FileRow) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarLoadConfig

            Dim dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            Try

                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim fk_Campo = dbmCore.SchemaConfig.TBL_Campo.DBFindByfk_Entidadfk_Documento(_Plugin.Manager.ImagingGlobal.Entidad, FileCoreRow.fk_Documento).Select("Nombre_Campo = 'LABEL'")(0)("id_Campo")

                'Traer valor file data
                Dim Value = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(FileCoreRow.fk_Expediente, Short.Parse(FileCoreRow.fk_Folder), Short.Parse(FileCoreRow.id_File), FileCoreRow.fk_Documento, Short.Parse(fk_Campo))(0).Valor_File_Data

                For Each campo In IndexerView.Campos
                    If campo.id = fk_Campo Then
                        campo.Control.Value = Value
                        Exit For
                    End If
                Next


            Catch ex As Exception

            Finally
                dbmCore.Connection_Close()

            End Try


        End Sub

        Public Sub FinalizarContenedorEmpaque(ByVal nidOt As Integer, ByVal nidEmpaque As Integer, ByVal IdEmpaqueContenedor As Integer) Implements IEventExecuter.FinalizarContenedorEmpaque
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_11A_Empaque_Finalizar_Contenedor.DBExecute(nidOt, nidEmpaque, IdEmpaqueContenedor, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.Sesion.Usuario.id)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Contenedor Empaque", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try

        End Sub

        Public Sub FinalizarContenedorEmpaqueEliminar(ByVal nidOt As Integer, ByVal ntoken As String) Implements IEventExecuter.FinalizarContenedorEmpaqueEliminar
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Me._Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_11A_Empaque_Finalizar_Eliminar_Contenedor.DBExecute(nidOt, _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, ntoken, _Plugin.Manager.Sesion.Usuario.id)

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Eliminar Contenedor Empaque", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short) Implements IEventExecuter.Reprocesar

            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)
                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim Respuesta = dbmIntegration.SchemaBCSCarpetaUnica.PA_Reprocesar.DBExecute(nidExpediente, nidFolder, nidFile, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto, _Plugin.Manager.Sesion.Usuario.id)

                If Respuesta.ToString() <> "" Then
                    DesktopMessageBoxControl.DesktopMessageShow("El expediente: " & CStr(nidExpediente) & " quedó reprocesado en la Fecha de Proceso: " & CStr(Respuesta) & ".", "Reprocesar", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("El expediente: " & CStr(nidExpediente) & " no quedó para reprocesar ya que la fecha previamente seleccionada no cumple con los requisitos (proceso sin cruce valido).", "Modificar Busqueda", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                End If


            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Reprocesar", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub Validar_Reprocesar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidDocumento As Integer, ByVal nidCampo As Integer, ByVal nidCampoTablaAsociada As Integer, ByVal nesCampo As Boolean, ByRef nValido As Boolean, ByRef nMsgError As String) Implements IEventExecuter.Validar_Reprocesar
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing


            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)


                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)


                Dim Respuesta = dbmIntegration.SchemaBCSCarpetaUnica.PA_Get_Fecha_Proceso_Reprocesar.DBExecute(nidExpediente, nidFolder, nidDocumento, nidCampo, nidCampoTablaAsociada, nesCampo, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)

                If Respuesta.ToString() <> "" Then
                    nValido = False
                    nMsgError = Respuesta.ToString()
                End If


            Catch ex As Exception
                nValido = False
                nMsgError = ex.Message
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
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
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmIntegration.SchemaBCSCarpetaUnica.PA_Insercion_Destape.DBExecute(nidAnexo)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Primera Captura Anexo", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarTerceraCapturaAnexo(nidAnexo As Long) Implements Miharu.Imaging.Library.Eventos.IEventExecuter.FinalizarTerceraCapturaAnexo
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

            Try
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.CajaSocialConnectionString)

                dbmIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                dbmIntegration.SchemaBCSCarpetaUnica.PA_Insercion_Destape.DBExecute(nidAnexo)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Finalizar Tercera Captura Anexo", ex)
            Finally
                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
            End Try
        End Sub

        Public Sub FinalizarPrepararDatagenerico(nidEntidad As Short, nidProyecto As Short, nidFechaProceso As Integer, nidOT As Integer) Implements IEventExecuter.FinalizarPrepararDataGenerico

        End Sub
#End Region

    End Class
End Namespace

