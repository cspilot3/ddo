Imports System
Imports System.IO
Imports Miharu.Security.Library.WebService
Imports System.Threading
Imports Slyg.Tools
Imports DBSofTrac
Imports System.Collections.Specialized
Imports Miharu.FileProvider.Library
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library.Config

Namespace Servicio
    Public Class SofTracService

#Region " Declaraciones "
        Private Detener As Boolean
        Private Const fk_Entidad_Procesamiento As Short = 11 'P&C
        Private Const fk_Sede_Procesamiento As Short = 3 'P&C - Bogota
        Private Const id_Centro_Procesamiento As Short = 1 'P&C - Bogota - Scanner Bogota
        Private id_Calendario As Short = 0
        Private _ExtensionAux As String
        Private formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private formato As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Public Const MaxThumbnailWidth As Integer = 60
        Public Const MaxThumbnailHeight As Integer = 80

        'Public Property FechaProcesoInt() As Integer
        'Public Property NewOT() As Integer

        Private ContenedorValido As Boolean
        Private ContenedorMsg As String

        Private objectLock As New Object

        Private Archivos() As String


        Protected Class RenamePath
            Public Property Path As String
            Public Property Cargue As Integer
            Public Property Paquete As Short

            Public Sub New(nPath As String, nCargue As Integer, nPaquete As Short)
                Me.Path = nPath.TrimEnd("\"c)
                Me.Cargue = nCargue
                Me.Paquete = nPaquete
            End Sub

            Public Sub RenameDirectory()
                Try
                    Directory.Move(Me.Path, Me.Path & "." & Me.Cargue & "." & Me.Paquete & "#")
                Catch
                End Try
            End Sub

        End Class
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
            If (File.Exists(Program.AppDataPath + SofTracConfig.ConfigFileName)) Then
                Program.Config = SofTracConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub

        Public Sub IniciarServicio()

            JWriteLog("Funcion Iniciar Servicio Version 1.1", EventLogEntryType.Information)

            Try
                Dim WebService As SecurityWebService

                LoadConfig()

                WebService = New SecurityWebService(Program.Config.SecurityWebServiceURL, "")
                JWriteLog(Program.Config.SecurityWebServiceURL, EventLogEntryType.Information)
                'WebService = New Miharu.Security.Library.SecurityWebService("http://localhost:51500/SecurityService.asmx", "")

#If Not Debug Then
            ' Validar que la versión corresponda
            Dim VersionApp As String = WebService.getAssemblyVersion(Program.AssemblyName)

            If Not VersionApp = Program.AssemblyVersion Then
                WriteErrorLog("La versión del aplicativo no corresponde a la registrada en la base de datos," & vbCrLf & vbCrLf & _
                                "Versión registrada: [" & VersionApp & "]" & vbCrLf & _
                                "Versión ejecutable: [" & Program.AssemblyVersion & "]")

                Me.Stop()

                Return
            End If
#End If

                WebService.CrearCanalSeguro()
                WebService.setUser(Program.Config.User, SofTracConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = SofTracConfig.getCadenasConexion(WebService)

                If Program.ConnectionStrings.SofTrac = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos SofTrac")
                    Me.Stop()

                    Return
                End If

                If Program.ConnectionStrings.Security = "" Then
                    WriteErrorLog("No se pudo obtener la cadena de conexión a la base de datos Security")
                    Me.Stop()

                    Return
                End If

                Dim NewThread As New Thread(AddressOf Proceso)

                Detener = False
                NewThread.Start()


            Catch ex As Exception
                JWriteLog("Error IniciarServicio ex: " & ex.Message & " " & ex.ToString(), EventLogEntryType.Error)
                Me.Stop()
            End Try
        End Sub

        Public Sub DetenerServicio()
            Detener = True
        End Sub

        Private Sub Proceso()

            Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Program.ConnectionStrings.SofTrac)
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)

                While Not Detener
                    If Detener Then Return

                    Try
                        dbmSecurity.Connection_Open(1)
                        dbmSofTrac.Connection_Open(1)
                        dbmImaging.Connection_Open(1)

                        Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBFindByfk_EntidadNombre_Calendario(fk_Entidad_Procesamiento, "TransferenciaSofTrac")

                        If CalendarioDataTable.Count > 0 Then
                            id_Calendario = CalendarioDataTable(0).id_Calendario
                        Else
                            WriteErrorLog("No hay calendario programado para el servicio")
                        End If

                        Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(fk_Entidad_Procesamiento, id_Calendario)

                        

                        If (habil) Then

                            Dim TransferenciaDataTable = dbmSofTrac.SchemaProcess.CTA_Registros_Transferencia.DBFindByLineaTransferidoTransferido_Proyecto_Destino("CONVENIOS6", True, False)

                            Dim procesadorHilosInstance As New ProcesadorHilos
                            procesadorHilosInstance.servicio = Me
                            For Each TransferenciaRow In TransferenciaDataTable
                                    procesadorHilosInstance.AgregarHilo(TransferenciaRow)

                            Next
                            While (procesadorHilosInstance.TerminoHilos = False)
                                System.Threading.Thread.Sleep(100)
                            End While

                        Else
                            WriteErrorLog("No es una hora habil para ejecutar el proceso")
                        End If
                    Catch ex As Exception
                        WriteErrorLog("Error Proceso ex: " & ex.Message & " " & ex.ToString())
                    Finally
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
                        If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                    End Try

                    If Detener Then Return

                    Thread.Sleep(Program.Config.Intervalo) ' Esperar n segundos antes de continuar
                End While
            Catch ex As Exception
                WriteErrorLog("Error Proceso ex: " & ex.Message & " " & ex.ToString())
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Public Sub ProcesoPrincipalHilo(nParametroHilo As Object)

            Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Dim Transferenciarow As DBSofTrac.SchemaProcess.CTA_Registros_TransferenciaRow = nParametroHilo

            Dim fk_Precinto As Short = 0
            Dim id_Contenedor As Short = 0
            Dim fk_Fecha_Proceso As Integer = 0
            Dim fk_OT As Integer = 0
            Dim _LoadImagesDirectories As Boolean

            Try
                dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Program.ConnectionStrings.SofTrac)
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)

                dbmSecurity.Connection_Open(1)
                dbmSofTrac.Connection_Open(1)
                dbmImaging.Connection_Open(1)

                Dim idCargueDetalleTransferencia = dbmSofTrac.SchemaAudit.PA_Cargue_Detalle_Transferencia_Proyecto_Destino.DBExecute(Transferenciarow.id_Transferencia)

                Dim ProyectoImagingDataTable = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Transferenciarow.fk_Entidad, Transferenciarow.fk_Proyecto)

                If (ProyectoImagingDataTable(0).Usa_Creacion_Automatica_Destape) Then

                    fk_Fecha_Proceso = CrearFechaProceso(dbmImaging, fk_Entidad_Procesamiento, Transferenciarow.fk_Entidad, Transferenciarow.fk_Proyecto, 1)
                    If fk_Fecha_Proceso > 0 Then
                        fk_OT = CrearOT(dbmImaging, fk_Entidad_Procesamiento, Transferenciarow.fk_Entidad, Transferenciarow.fk_Proyecto, fk_Fecha_Proceso, 1, fk_Sede_Procesamiento, id_Centro_Procesamiento, CShort(1))
                    End If

                    If fk_Fecha_Proceso > 0 And fk_OT > 0 Then
                        'Crear Destape
                        Dim DestapeDataTable = dbmImaging.SchemaProcess.PA_Crear_Precinto_Contenedor_Sin_Campo_Softrac.DBExecute(fk_OT, Transferenciarow.Contenedor)

                        If (CShort(DestapeDataTable.Rows(0)("fk_Precinto").ToString()) = 0) Then
                            If (CShort(DestapeDataTable.Rows(0)("id_Contenedor").ToString()) = 0) Then
                                WriteErrorLog("El precinto y contenedor ya fueron destapados para la OT y Fecha de Proceso")
                            End If
                        End If

                        fk_Precinto = CShort(DestapeDataTable.Rows(0)("fk_Precinto").ToString())
                        id_Contenedor = CShort(DestapeDataTable.Rows(0)("id_Contenedor").ToString())
                    End If

                Else
                    fk_OT = Transferenciarow.fk_OT
                    fk_Fecha_Proceso = Integer.Parse(Transferenciarow.Fecha_Proceso.ToString())
                End If

                _ExtensionAux = IIf(ProyectoImagingDataTable(0).Usa_Cargue_PDF, ".pdf", ProyectoImagingDataTable(0).Extension_Formato_Imagen_Salida).ToString()
                Dim _Cargue As New DBImaging.Esquemas.xsdCargue
                If Directory.Exists(Transferenciarow.Ruta_Imagenes) Then
                    Dim _Paquetes As StringCollection
                    Dim idPaquete As Short

                    LoadImagesDirectories(Transferenciarow.Ruta_Imagenes, _LoadImagesDirectories, _Paquetes)
                    If (_LoadImagesDirectories) Then

                        Dim idDirectorio As Integer = 0

                        idPaquete = 0

                        For Each Directorio As String In _Paquetes

                            _Cargue = New DBImaging.Esquemas.xsdCargue
                            Dim RowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow = Nothing

                            If (Not ProyectoImagingDataTable(0).Usa_Paquete_x_Imagen) Then
                                idPaquete = CShort(idPaquete + 1)

                                RowPaquete = _Cargue.Paquete.AddPaqueteRow(idPaquete, Directorio, 0, 0, 0, True, "", "", 0, 0, "")
                            End If

                            If (ProyectoImagingDataTable(0).Usa_Archivo_Indices) Then
                                LoadImagesIndice(Directorio, RowPaquete, ProyectoImagingDataTable.Rows(0), Transferenciarow, idPaquete, _Cargue)
                            Else
                                LoadImagesNoIndice(Directorio, RowPaquete, ProyectoImagingDataTable.Rows(0), Transferenciarow, idPaquete, _Cargue)
                            End If

                            If (_Cargue.Paquete.Rows.Count = 0) Then
                                ActualizarTransferenciaProyectoDestino(idCargueDetalleTransferencia, "No se encontraron paquetes para cargar")
                            Else
                                If (Archivos.Count = 0) Then
                                    ActualizarTransferenciaProyectoDestino(idCargueDetalleTransferencia, "No se encontraron imágenes para cargar")
                                Else
                                    Validar(ProyectoImagingDataTable(0), Transferenciarow, fk_OT, _Cargue)
                                    If (ContenedorValido) Then
                                        If (Not ProyectoImagingDataTable(0).Aplica_Fisico AndAlso ProyectoImagingDataTable(0).Usa_Indexacion) Then
                                            CargarConIndexacion(ProyectoImagingDataTable.Rows(0), Transferenciarow, idCargueDetalleTransferencia, fk_Precinto, id_Contenedor, fk_OT, CStr(fk_Fecha_Proceso).ToString(), _Cargue)
                                        Else
                                            CargarSinIndexacion(ProyectoImagingDataTable.Rows(0))
                                        End If
                                    Else
                                        ActualizarTransferenciaProyectoDestino(idCargueDetalleTransferencia, RowPaquete.Descripcion)
                                    End If
                                End If
                            End If
                        Next
                    End If
                Else
                    ActualizarTransferenciaProyectoDestino(idCargueDetalleTransferencia, "Ruta no existe")
                End If

            Catch ex As Exception
                WriteErrorLog("Error transfiriendo imagenes a Miharu: " + ex.Message & " " & ex.ToString())

                If ((fk_Precinto <> 0) And (id_Contenedor <> 0)) Then
                    dbmImaging.SchemaProcess.TBL_Contenedor.DBDelete(fk_OT, fk_Precinto, id_Contenedor)
                    dbmImaging.SchemaProcess.TBL_Precinto.DBDelete(fk_OT, fk_Precinto)
                End If

            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try


        End Sub

        Private Sub LoadImagesIndice(ByVal nPaquete As String, ByRef nRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow, ByVal _ProyectoImagingrow As DBImaging.SchemaConfig.CTA_ProyectoRow, ByVal _TransferenciaRow As DBSofTrac.SchemaProcess.CTA_Registros_TransferenciaRow, ByRef nidPaquete As Short, ByRef n_Cargue As DBImaging.Esquemas.xsdCargue)
            Try
                Dim IndicesFileName = nPaquete.TrimEnd("\"c) & "\" & _ProyectoImagingrow.Nombre_Archivo_Indices

                If (File.Exists(IndicesFileName)) Then
                    Dim CSVData As New Slyg.Tools.CSV.CSVData
                    Dim Separador As Char
                    Dim DelimitadorTexto As Char

                    Separador = FormatValidator.getSeparador(_ProyectoImagingrow.fk_Separador)
                    DelimitadorTexto = FormatValidator.getDelimitadorTexto(_ProyectoImagingrow.fk_Identificador_Texto)

                    CSVData.LoadCSV(IndicesFileName, _ProyectoImagingrow.Usa_Encabezado_Columnas, Separador, DelimitadorTexto)

                    InsertImage(CSVData.DataTable, nPaquete, nRowPaquete, _ProyectoImagingrow, _TransferenciaRow, nidPaquete, n_Cargue)
                Else
                    Throw New Exception("No se encontró el archivo de índices: " & IndicesFileName)
                End If
            Catch ex As Exception
                WriteErrorLog("Error LoadImagesIndice ex: " & ex.Message & " " & ex.ToString())
            End Try

        End Sub

        Private Sub LoadImagesNoIndice(ByVal nPaquete As String, ByRef nRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow, ByVal _ProyectoImagingrow As DBImaging.SchemaConfig.CTA_ProyectoRow, ByVal _TransferenciaRow As DBSofTrac.SchemaProcess.CTA_Registros_TransferenciaRow, ByRef nidPaquete As Short, ByRef n_Cargue As DBImaging.Esquemas.xsdCargue)
            Dim Codigo As String
            Dim i As Integer = 0
            Dim NewExtension As String

            NewExtension = _ExtensionAux

            Archivos = Directory.GetFiles(nPaquete, "*" & NewExtension)

            For Each Archivo As String In Archivos
                i += 1

                Codigo = Path.GetFileNameWithoutExtension(Archivo)
                InsertImage(Archivo, nPaquete, Codigo, i, _ProyectoImagingrow.Default_Esquema, _ProyectoImagingrow.Default_Documento, nRowPaquete, _ProyectoImagingrow, _TransferenciaRow, nidPaquete, n_Cargue)
            Next
        End Sub

        Private Sub InsertImage(ByVal nData As Slyg.Tools.CSV.CSVTable, ByVal nPaquete As String, ByRef nRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow, ByVal _ProyectoImagingrow As DBImaging.SchemaConfig.CTA_ProyectoRow, ByVal _TransferenciaRow As DBSofTrac.SchemaProcess.CTA_Registros_TransferenciaRow, ByRef nidPaquete As Short, ByRef n_Cargue As DBImaging.Esquemas.xsdCargue)
            Dim CamposDataTable As DBImaging.SchemaConfig.CTA_CampoDataTable = Nothing

            Dim nDataCSV = nData.ToDataTable()

            ' Cargar la parametrización de campos
            If (Not _ProyectoImagingrow.Usa_Indexacion) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(1)

                    CamposDataTable = dbmImaging.SchemaConfig.CTA_Campo.DBFindByfk_Entidadfk_Proyectoid_Esquema(_ProyectoImagingrow.fk_Entidad, _ProyectoImagingrow.fk_Proyecto, Nothing)
                Catch ex As Exception
                    WriteErrorLog("Error InsertImage1 ex: " & ex.Message & " " & ex.ToString())
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If

            For i As Integer = 1 To nDataCSV.Rows.Count
                Dim Row = nDataCSV.Rows(i - 1)
                Dim NombreImagen = CStr(Row.Item(_ProyectoImagingrow.Columna_Imagen - 1))

                If (Path.GetExtension(NombreImagen) = "") Then
                    NombreImagen &= _ProyectoImagingrow.Extension_Formato_Imagen_Entrada
                End If

                NombreImagen = NombreImagen.Substring(_ProyectoImagingrow.Caracteres_Omitir)
                NombreImagen = Path.GetFileName(NombreImagen)

                Dim Esquema As Short
                If (_ProyectoImagingrow.Usa_Columna_Esquema) Then
                    Dim Valor As String = Row.Item(_ProyectoImagingrow.Columna_Esquema - 1).ToString()

                    If (Not Slyg.Tools.DataConvert.IsNumeric(Valor)) Then
                        Throw New Exception("El valor de la columna Esquema debe ser numérico")
                    End If

                    Esquema = CShort(Valor)
                Else
                    Esquema = _ProyectoImagingrow.Default_Esquema
                End If

                Dim Documento As Short

                If (_ProyectoImagingrow.Usa_Columna_Documento) Then
                    Dim Valor As String = Row.Item(_ProyectoImagingrow.Columna_Documento - 1).ToString()

                    If (Not Slyg.Tools.DataConvert.IsNumeric(Valor)) Then
                        Throw New Exception("El valor de la columna Documento debe ser numérico")
                    End If

                    Documento = CShort(Valor)
                Else
                    Documento = _ProyectoImagingrow.Default_Documento
                End If
                InsertImage(nPaquete & NombreImagen, nPaquete, CStr(Row.Item(_ProyectoImagingrow.Columna_Key - 1)), i, Esquema, Documento, nRowPaquete, _ProyectoImagingrow, _TransferenciaRow, nidPaquete, n_Cargue)
                ' Cargar la data adicional
                If (Not _ProyectoImagingrow.Usa_Indexacion) Then
                    Dim NewCampos() = CamposDataTable.Select("fk_Documento = " & Documento & "AND Columna_Cargue_Campo > 0")
                    For j = 0 To NewCampos.Length - 1
                        Dim CampoRow = NewCampos(j)
                        If (n_Cargue.Campo.Select("fk_Paquete = " & nRowPaquete.id_Paquete & " AND fk_Item = " & i & " AND id_Campo = " & CShort(CampoRow.Item("id_Campo"))).Length = 0) Then
                            n_Cargue.Campo.AddCampoRow(nRowPaquete.id_Paquete, i, CShort(CampoRow.Item("id_Campo")), Row.Item(CInt(CampoRow.Item("Columna_Cargue_Campo")) - 1).ToString())
                        End If
                    Next
                End If
            Next
        End Sub

        Private Sub InsertImage(ByVal nNombre As String, ByVal nPathPaquete As String, ByVal nCodigo As String, ByVal nId As Integer, nEsquema As Short, nDocumento As Short, ByRef nRowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow, ByVal _ProyectoImagingrow As DBImaging.SchemaConfig.CTA_ProyectoRow, ByVal _TransferenciaRow As DBSofTrac.SchemaProcess.CTA_Registros_TransferenciaRow, ByRef nidPaquete As Short, ByRef n_Cargue As DBImaging.Esquemas.xsdCargue)
            If (nRowPaquete Is Nothing) Then
                nidPaquete = CShort(nidPaquete + 1)

                nRowPaquete = n_Cargue.Paquete.AddPaqueteRow(nidPaquete, nPathPaquete, 0, 0, 0, True, "", "", 0, 0, "")
            End If

            ' Calcular el Key del paquete            
            nRowPaquete.Key = _TransferenciaRow.Contenedor

            If (File.Exists(nNombre)) Then
                n_Cargue.Item.AddItemRow(nRowPaquete, nId, CShort(ImageManager.GetFolios(nNombre)), getTamaño(nNombre), Path.GetFileName(nNombre), nCodigo, True, "", nEsquema, nDocumento, 0, 0, "")
            Else
                n_Cargue.Item.AddItemRow(nRowPaquete, nId, 0, 0, Path.GetFileName(nNombre), nCodigo, False, "No existe el archivo", nEsquema, nDocumento, 0, 0, "")
            End If
        End Sub

        Private Sub CargarConIndexacion(ByVal _ProyectoImagingrow As DBImaging.SchemaConfig.CTA_ProyectoRow, ByVal _TransferenciaRow As DBSofTrac.SchemaProcess.CTA_Registros_TransferenciaRow, ByVal _idCargueDetalleTransferencia As Long, nfk_Precinto As Short, nid_Contenedor As Short, nfk_OT As Integer, nfk_Fecha_Proceso As String, ByRef n_Cargue As DBImaging.Esquemas.xsdCargue)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing
            Dim cargueType As New DBImaging.SchemaProcess.TBL_CargueType()
            Dim manager As FileProviderManager = Nothing
            Dim carpetasRenombrar As New List(Of RenamePath)

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Program.ConnectionStrings.SofTrac)

                dbmCore.Connection_Open(1)
                dbmImaging.Connection_Open(1)
                dbmSofTrac.Connection_Open(1)

                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(_ProyectoImagingrow.fk_Entidad_Servidor, _ProyectoImagingrow.fk_Servidor)(0).ToCTA_ServidorSimpleType
                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(fk_Entidad_Procesamiento, fk_Sede_Procesamiento, id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                manager = New FileProviderManager(servidor, centro, dbmImaging, 1)
                manager.Connect()

                ' Obtener el nuevo id para el cargue
                cargueType.fk_Entidad = _TransferenciaRow.fk_Entidad
                cargueType.fk_Proyecto = _TransferenciaRow.fk_Proyecto
                cargueType.fk_Estado = DBCore.EstadoEnum.Creado
                cargueType.fk_Entidad_Procesamiento = _TransferenciaRow.fk_Entidad_Procesamiento
                cargueType.fk_Sede_Procesamiento_Cargue = _TransferenciaRow.fk_Sede_Procesamiento
                cargueType.fk_Centro_Procesamiento_Cargue = _TransferenciaRow.fk_Centro_Procesamiento
                cargueType.fk_Entidad_Servidor = servidor.fk_Entidad
                cargueType.fk_Servidor = servidor.id_Servidor
                cargueType.fk_OT = nfk_OT
                cargueType.Fecha_Proceso = Date.ParseExact(nfk_Fecha_Proceso, "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                cargueType.Observaciones = "SOFTRAC"
                cargueType.fk_Usuario_Log = 1

                cargueType.id_Cargue = dbmImaging.SchemaProcess.PA_Guardar_TBL_Cargue.DBExecute(
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


                Dim formato = Utilities.GetEnumFormat(_ProyectoImagingrow.Extension_Formato_Imagen_Salida)
                Dim compresion = Utilities.GetEnumCompression(CType(_ProyectoImagingrow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                Dim totalPaquetes As Integer = n_Cargue.Paquete.Select("PaqueteValido = 1").Length
                Dim paquete As Integer = 0

                ' Actualizar la data de los paquetes
                For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In n_Cargue.Paquete.Rows

                    'Se procesa si el paquete es válido.
                    If (rowPaquete.PaqueteValido) Then
                        Dim items As Integer = n_Cargue.Item.Select("fk_Paquete = " & rowPaquete.id_Paquete & " AND Valido = 1").Length

                        If (items > 0) Then
                            paquete += 1

                            Dim paqueteType As New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()

                            paqueteType.fk_Cargue = cargueType.id_Cargue
                            paqueteType.id_Cargue_Paquete = rowPaquete.id_Paquete
                            paqueteType.fk_Estado = DBCore.EstadoEnum.Indexacion
                            paqueteType.fk_Usuario_Log = 1
                            paqueteType.Fecha_Proceso = cargueType.Fecha_Proceso
                            paqueteType.Path_Cargue_Paquete = rowPaquete.Path
                            paqueteType.Bloqueado = False
                            paqueteType.Data_Path = rowPaquete.Path
                            paqueteType.fk_Sede_Procesamiento_Asignada = _TransferenciaRow.fk_Sede_Procesamiento
                            paqueteType.fk_Centro_Procesamiento_Asignado = _TransferenciaRow.fk_Centro_Procesamiento

                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(paqueteType)

                            ' Actualizar el contenedor                            
                            Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                            contenedorType.Cargado = True
                            contenedorType.fk_Cargue = cargueType.id_Cargue
                            contenedorType.fk_Paquete = rowPaquete.id_Paquete
                            dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, nfk_OT, rowPaquete.Precinto, rowPaquete.Contenedor)

                            carpetasRenombrar.Add(New RenamePath(rowPaquete.Path, cargueType.id_Cargue, rowPaquete.id_Paquete))

                            Dim item As Integer = 0

                            For Each rowItem As DBImaging.Esquemas.xsdCargue.ItemRow In rowPaquete.GetItemRows()
                                If (rowItem.Valido) Then
                                    item += 1

                                    Dim itemType As New DBImaging.SchemaProcess.TBL_Cargue_ItemType()

                                    itemType.fk_Cargue = cargueType.id_Cargue
                                    itemType.fk_Cargue_Paquete = rowPaquete.id_Paquete
                                    itemType.id_Cargue_Item = rowItem.id_Item
                                    itemType.Folios_Cargue_Item = rowItem.Folios
                                    itemType.Tamaño_Cargue_Item = rowItem.Tamaño
                                    itemType.Path_Cargue_Item = Path.GetFileName(rowItem.Path)
                                    itemType.Key_Cargue_Item = rowItem.Key
                                    itemType.fk_Estado = DBCore.EstadoEnum.Indexacion
                                    itemType.Bloqueado = False
                                    itemType.fk_Usuario_Log = 1

                                    dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert(itemType)

                                    ''20210226
                                    'manager.EvaluateVolumen(itemType.fk_Cargue, rowItem)

                                    manager.CreateItem(itemType.fk_Cargue, itemType.fk_Cargue_Paquete, itemType.id_Cargue_Item)


                                    'Se crean los Folios del item.
                                    For folio As Short = 1 To rowItem.Folios

                                        '20210226 - Insertar Folio
                                        Dim folioType = New DBImaging.SchemaProcess.TBL_Cargue_FolioType()
                                        folioType.fk_Cargue = itemType.fk_Cargue
                                        folioType.fk_Cargue_Paquete = itemType.fk_Cargue_Paquete
                                        folioType.fk_Cargue_Item = itemType.id_Cargue_Item
                                        folioType.id_Folio = folio
                                        folioType.Indexado = False
                                        dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBInsert(folioType)

                                        If Not formatoAux = ImageManager.EnumFormat.Pdf Then
                                            Dim dataImage = ImageManager.GetFolioData(rowPaquete.Path & rowItem.Path, folio, formato, compresion)
                                            Dim dataImageThumbnail = ImageManager.GetThumbnailData(rowPaquete.Path & rowItem.Path, folio, folio, MaxThumbnailWidth, MaxThumbnailHeight)

                                            'Dim dataImage2 = Bitmap.FromStream(New MemoryStream(ImageManager.GetFolioData2(rowPaquete.Path & rowItem.Path, folio, formato, compresion)))

                                            'Dim bmpdataImage = Bitmap.FromStream(New MemoryStream(dataImage))
                                            'Dim bmpdataImageThumbnail = Bitmap.FromStream(New MemoryStream(dataImageThumbnail(0)))

                                            'bmpdataImage = ImageManager.ComprimirImagen(bmpdataImage, System.Drawing.Imaging.ImageFormat.Jpeg, 20)
                                            'bmpdataImageThumbnail = ImageManager.ComprimirImagen(bmpdataImageThumbnail, System.Drawing.Imaging.ImageFormat.Jpeg, 20)

                                            'Dim Converter As ImageConverter = New ImageConverter()
                                            'Converter.ConvertTo(bmpdataImage, typeof(Byte()))

                                            'Using m As MemoryStream = New MemoryStream
                                            '    bmpdataImage.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg)
                                            '    dataImage = m.ToArray()
                                            'End Using  


                                            manager.CreateFolio(itemType.fk_Cargue, itemType.fk_Cargue_Paquete, itemType.id_Cargue_Item, folio, dataImage, dataImageThumbnail(0), False)

                                            dataImage = Nothing
                                            dataImageThumbnail = Nothing
                                        Else
                                            Dim flags As FreeImageAPI.FREE_IMAGE_SAVE_FLAGS = Utilities.GetEnumDefaultFlags(formato)

                                            Dim dataImage = ImageManager.GetFolioDataPdfOnly(rowPaquete.Path & rowItem.Path, folio, formato, compresion, flags)

                                            Using dataImageStream As Stream = New MemoryStream()
                                                dataImageStream.Write(dataImage, 0, dataImage.Length)

                                                Using bitmap = New FreeImageAPI.FreeImageBitmap(dataImageStream, ImageManager.GetImageFormat(formato))

                                                    Dim dataImageThumbnail = ImageManager.GetThumbnailData(bitmap, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                                                    manager.CreateFolio(itemType.fk_Cargue, itemType.fk_Cargue_Paquete, itemType.id_Cargue_Item, folio, dataImage, dataImageThumbnail(0), False)

                                                    dataImageThumbnail = Nothing
                                                End Using
                                            End Using

                                            dataImage = Nothing
                                        End If

                                        ' Insertar Folio - Se mueve unas lineas más arriba

                                        Utilities.ClearMemory()
                                        Application.DoEvents()
                                    Next
                                End If
                            Next
                        End If
                    End If

                    Dim TransferenciaUpdateType As New DBSofTrac.SchemaProcess.TBL_TransferenciaType
                    TransferenciaUpdateType.Transferido_Proyecto_Destino = True
                    TransferenciaUpdateType.fk_Cargue = CType(cargueType.id_Cargue, Long)
                    TransferenciaUpdateType.fk_Cargue_Paquete = CType(rowPaquete.id_Paquete, Int32)
                    dbmSofTrac.SchemaProcess.TBL_Transferencia.DBUpdate(TransferenciaUpdateType, _TransferenciaRow.id_Transferencia)

                    ActualizarTransferenciaProyectoDestino(_idCargueDetalleTransferencia, "")
                Next

                Dim cargueUpdateType As New DBImaging.SchemaProcess.TBL_CargueType()
                cargueUpdateType.fk_Estado = DBCore.EstadoEnum.Indexacion
                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(cargueUpdateType, cargueType.id_Cargue)

                ' Renombrar paquete
                For Each CarpetaRenombrar In carpetasRenombrar
                    CarpetaRenombrar.RenameDirectory()
                Next

                ' Actualizar Dashboard
                dbmImaging.SchemaProcess.PA_Dashboard_Paquetes_insert.DBExecute(cargueType.id_Cargue)

                Application.DoEvents()
            Catch ex As Exception
                'Elimina el cargue realizado
                If (Not cargueType.id_Cargue Is Nothing AndAlso (dbmImaging IsNot Nothing) AndAlso (manager IsNot Nothing)) Then
                    ' Actualizar el contenedor
                    For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In n_Cargue.Paquete.Rows
                        Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                        contenedorType.Cargado = False
                        contenedorType.fk_Cargue = DBNull.Value
                        contenedorType.fk_Paquete = DBNull.Value
                        dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, nfk_OT, rowPaquete.Precinto, rowPaquete.Contenedor)
                    Next

                    If ((nfk_Precinto <> 0) And (nid_Contenedor <> 0)) Then
                        Dim NewOT As Integer
                        dbmImaging.SchemaProcess.TBL_Contenedor.DBDelete(NewOT, nfk_Precinto, nid_Contenedor)
                        dbmImaging.SchemaProcess.TBL_Precinto.DBDelete(NewOT, nfk_Precinto)
                    End If

                    dbmImaging.SchemaProcess.PA_Liberar_Contenedores_Cargue.DBExecute(nfk_OT, cargueType.id_Cargue)
                    dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(cargueType.id_Cargue)
                    dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(cargueType.id_Cargue, Nothing)
                    manager.DeleteCargue(cargueType.id_Cargue)
                End If
                WriteErrorLog("Error CargarConIndexacion ex: " & ex.Message)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Private Sub CargarSinIndexacion(ByVal _ProyectoImagingrow As DBImaging.SchemaConfig.CTA_ProyectoRow)
            Throw New NotImplementedException
        End Sub

        Private Sub Validar(ByVal _ProyectoImagingRow As DBImaging.SchemaConfig.CTA_ProyectoRow, ByVal _TransferenciaRow As DBSofTrac.SchemaProcess.CTA_Registros_TransferenciaRow, _fk_OT As Integer, n_Cargue As DBImaging.Esquemas.xsdCargue)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            ContenedorValido = False
            ContenedorMsg = ""

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)

                ' Actualizar los datos del cargue
                dbmImaging.Connection_Open(1)
                dbmCore.Connection_Open(1)


                For Each rowPaquete As DBImaging.Esquemas.xsdCargue.PaqueteRow In n_Cargue.Paquete.Rows
                    rowPaquete.PaqueteValido = True

                    ' Validar destape
                    If (_ProyectoImagingRow.Usa_Destape_Contenedor AndAlso Not _ProyectoImagingRow.Usa_Paquete_x_Imagen) Then
                        ' Validar el contenedor
                        Dim contenedorDataTable = dbmImaging.SchemaProcess.TBL_Contenedor.DBFindByfk_OTToken(_fk_OT, rowPaquete.Key)

                        If (contenedorDataTable.Count = 0) Then
                            rowPaquete.PaqueteValido = False
                            rowPaquete.Descripcion = "No se encontró un contenedor que coincida en la OT"
                        ElseIf (contenedorDataTable(0).Cargado) Then
                            rowPaquete.PaqueteValido = False
                            rowPaquete.Descripcion = "El contenedor ya fué cargado"
                        Else
                            ' Validar que el precinto este cerrado
                            Dim precintoDataTable = dbmImaging.SchemaProcess.TBL_Precinto.DBGet(_fk_OT, contenedorDataTable(0).fk_Precinto)

                            If (Not precintoDataTable(0).Cerrado) Then
                                rowPaquete.PaqueteValido = False
                                rowPaquete.Descripcion = "El Precinto: " & precintoDataTable(0).Precinto & ", no esta cerrado"
                            Else
                                rowPaquete.Precinto = contenedorDataTable(0).fk_Precinto
                                rowPaquete.Contenedor = contenedorDataTable(0).id_Contenedor
                                rowPaquete.CBarrasContenedor = contenedorDataTable(0).Token
                                ContenedorValido = True
                            End If
                        End If
                    End If
                Next
            Catch ex As Exception
                WriteErrorLog("Error Validar ex: " & ex.Message & " " & ex.ToString())
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

        Private Sub ActualizarTransferenciaProyectoDestino(ByVal _idCargueDetalleTransferencia As Long, ByVal _Mensaje As String)
            Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing

            Try
                dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Program.ConnectionStrings.SofTrac)
                dbmSofTrac.Connection_Open(1)

                dbmSofTrac.SchemaAudit.PA_Cargue_Detalle_Transferencia_Proyecto_Destino_Fin.DBExecute(_idCargueDetalleTransferencia, _Mensaje.ToString())

            Catch ex As Exception
                WriteErrorLog("Error ActualizarTransferenciaProyectoDestino ex: " & ex.Message & " " & ex.ToString())
            Finally
                If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
            End Try

        End Sub

        Private Sub WriteErrorLog(ByVal nMessage As String)

            SyncLock objectLock

                Try

                    JWriteLog("WriteErrorLog Path: " & Program.AppDataPath & "log.txt", EventLogEntryType.Information)

                    JWriteLog(nMessage, EventLogEntryType.Error)

                    Dim sw As New StreamWriter(Program.AppDataPath & "log.txt", True)

                    sw.WriteLine("--------------------------------------------------------------")
                    sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    sw.WriteLine("Mensaje: " & nMessage)
                    sw.WriteLine("--------------------------------------------------------------")
                    sw.WriteLine("")

                    sw.Flush()
                    sw.Close()
                Catch ex As Exception
                    Try : JWriteLog(ex.Message, EventLogEntryType.Error) : Catch : End Try
                End Try
                Windows.Forms.Application.DoEvents()

            End SyncLock

        End Sub

        Public Function CrearFechaProceso(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidadProcesamiento As Short, ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nUser As Integer) As Integer
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim FechaProceso = DateTime.Now
            Dim FechaProcesoInt As Integer = 0
            Dim FechaProcesoType As New DBImaging.SchemaProcess.TBL_Fecha_ProcesoType()

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)
                dbmCore.Connection_Open(1)

                Try
                    FechaProceso = dbmCore.SchemaProcess.PA_getSiguiente_Fecha_Habil.DBExecute(FechaProceso)
                Catch ex As Exception
                    WriteErrorLog("dbmCore.SchemaProcess.PA_getSiguiente_Fecha_Habil: " + ex.Message & " " & ex.ToString())
                End Try

                Dim DatosFechaProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(nEntidadCliente, nProyecto, Integer.Parse(FechaProceso.ToString("yyyyMMdd")), nEntidadProcesamiento)

                If DatosFechaProcesoDataTable.Count = 0 Then

                    FechaProcesoType.fk_Entidad_Procesamiento = nEntidadProcesamiento
                    FechaProcesoType.fk_Entidad = nEntidadCliente
                    FechaProcesoType.fk_Proyecto = nProyecto
                    FechaProcesoType.id_fecha_proceso = Integer.Parse(FechaProceso.ToString("yyyyMMdd"))
                    FechaProcesoType.Fecha_Proceso = FechaProceso
                    FechaProcesoType.fk_Usuario_Apertura = nUser
                    FechaProcesoType.Fecha_Apertura = SlygNullable.SysDate
                    FechaProcesoType.Cerrado = False

                    dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBInsert(FechaProcesoType)
                    FechaProcesoInt = FechaProcesoType.id_fecha_proceso.Value
                Else
                    FechaProcesoInt = DatosFechaProcesoDataTable(0).id_fecha_proceso
                End If

                Return FechaProcesoInt
            Catch ex As Exception
                WriteErrorLog("Error creando fecha de proceso: " + ex.Message & " " & ex.ToString())
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Function

        Public Function CrearOT(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager, ByVal nEntidadProcesamiento As Short, ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nFechaProceso As Integer, ByVal nUser As Integer, ByVal nSedeProcesamiento_Cargue As Short, ByVal nCentroProcesamiento_Cargue As Short, ByVal nfk_Tipo_OT As Short) As Integer


            Dim OT_Type As New DBImaging.SchemaProcess.TBL_OTType()

            Dim NewOt As Integer = 0
            Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_procesofk_OT_TipoCerrado(nEntidadProcesamiento, nEntidadCliente, nProyecto, nFechaProceso, nfk_Tipo_OT, False)

            If OTDataTable.Count = 0 Then

                OT_Type.fk_Entidad_Procesamiento = nEntidadProcesamiento
                OT_Type.fk_Entidad = nEntidadCliente
                OT_Type.fk_Proyecto = nProyecto
                OT_Type.fk_fecha_proceso = nFechaProceso
                OT_Type.fk_OT_Tipo = nfk_Tipo_OT
                OT_Type.fk_Sede_Procesamiento = nSedeProcesamiento_Cargue
                OT_Type.fk_Centro_Procesamiento = nCentroProcesamiento_Cargue
                OT_Type.fk_Usuario_Apertura = nUser
                OT_Type.Fecha_Apertura = SlygNullable.SysDate
                OT_Type.Exportado = False
                OT_Type.Cerrado = False
                OT_Type.id_OT = dbmImaging.SchemaProcess.TBL_OT.DBNextId()

                dbmImaging.SchemaProcess.TBL_OT.DBInsert(OT_Type)

                NewOt = OT_Type.id_OT.Value
            Else
                NewOt = OTDataTable(0).id_OT
            End If

            Return NewOt
        End Function

#End Region

#Region " Eventos "

        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.

        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Sub JWriteLog(ByVal mensaje As String, ByVal tipo As EventLogEntryType)

            If Not EventLog.SourceExists("SofTracService") Then
                EventLog.CreateEventSource("SofTracService", "Application")
            End If

            Dim eventLog1 As EventLog = New EventLog()
            eventLog1.Source = "SofTracService"
            eventLog1.WriteEntry(mensaje, tipo)

        End Sub

#End Region

#Region "Funciones"

        Protected Function LoadImagesDirectories(ByVal _Input_Folder As String, ByRef n_LoadImagesDirectories As Boolean, ByRef n_Paquetes As StringCollection) As Boolean
            Try
                n_Paquetes = New StringCollection
                n_LoadImagesDirectories = False

                Dim SelectedPath = _Input_Folder

                Dim NombrePaquete = SelectedPath.TrimEnd("\"c)

                If (Not NombrePaquete.EndsWith("#")) Then
                    n_Paquetes.Add(NombrePaquete & "\")

                    Application.DoEvents()

                    n_LoadImagesDirectories = True
                    Return True
                End If

            Catch ex As Exception
                WriteErrorLog("Error LoadImagesDirectories ex: " & ex.Message & " " & ex.ToString())
            End Try
            n_LoadImagesDirectories = False
            Return False
        End Function

        Private Function getTamaño(ByVal nFileName As String) As Long
            Dim Archivo As New FileInfo(nFileName)
            ' retornar el valor en Bytes
            Return Archivo.Length
        End Function

#End Region

#Region " Propiedades "

        'Public ReadOnly Property Datos() As DBImaging.Esquemas.xsdCargue
        '    Get
        '        Return _Cargue
        '    End Get
        'End Property

#End Region

    End Class
End Namespace

