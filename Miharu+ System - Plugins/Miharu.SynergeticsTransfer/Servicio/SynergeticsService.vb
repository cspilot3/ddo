Imports System.IO
Imports Miharu.Security.Library.WebService
Imports System.Threading
Imports Slyg.Tools
Imports System.Collections.Specialized
Imports System.Windows.Forms
Imports Miharu.FileProvider.Library
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Imaging
Imports System.Net
Imports System.Runtime.Serialization.Json

Namespace Servicio
    Public Class RespuestaWS
        Public codret As String
        Public msg As String
    End Class
    Public Class SynergeticsService
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

#Region " Eventos "

        Public Sub New()
            ' This call is required by the designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

#End Region

#Region " Metodos "
        Private Sub LoadConfig()
            ' Leer la configuración
            If (File.Exists(Program.AppDataPath + SynergeticsConfig.ConfigFileName)) Then
                Program.Config = SynergeticsConfig.Deserialize(Program.AppDataPath)
            End If
        End Sub

        Public Sub IniciarServicio()

            'Uso esto para depurar, en produccion no estara.. Se hace para iniciar el servicio lento con eso podemos atar el depurador al proceso
            'Thread.Sleep(15000)

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
                WebService.setUser(Program.Config.User, SynergeticsConfig.Decrypt(Program.Config.Password))
                Program.ConnectionStrings = SynergeticsConfig.getCadenasConexion(WebService)

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

        Public Sub ProcesoPrincipalHilo(nParametroHilo As Object)
            Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Dim Transferenciarow As DBSofTrac.SchemaProcess.CTA_Registros_Transferencia_SynergeticsRow = nParametroHilo

            Dim fk_Precinto As Short = 0
            Dim id_Contenedor As Short = 0
            Dim fk_Fecha_Proceso As Integer = 0
            Dim fk_OT As Integer = 0

            Try
                dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Program.ConnectionStrings.SofTrac)
                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)

                dbmSecurity.Connection_Open(1)
                dbmSofTrac.Connection_Open(1)
                dbmImaging.Connection_Open(1)

                'Inicio de transferencia, cambiar estado a 'Transfiriendo'
                Dim TransferenciaUpdateType As New DBSofTrac.SchemaProcess.TBL_Transferencia_SynergeticsType
                TransferenciaUpdateType.fk_Estado = 2
                dbmSofTrac.SchemaProcess.TBL_Transferencia_Synergetics.DBUpdate(TransferenciaUpdateType, Transferenciarow.id_Transferencia_Synergetics)

                'Inserción de registro para auditoría de cargue
                Dim idCargueDetalleTransferencia = dbmSofTrac.SchemaAudit.PA_Cargue_Detalle_Transferencia_Proyecto_Destino_Synergetics.DBExecute(Transferenciarow.id_Transferencia_Synergetics)

                Dim ProyectoImagingDataTable = dbmImaging.SchemaConfig.CTA_Proyecto.DBFindByfk_Entidadfk_Proyecto(Transferenciarow.fk_Entidad, Transferenciarow.fk_Proyecto)

                If (ProyectoImagingDataTable(0).Usa_Creacion_Automatica_Destape) Then
                    JWriteLog("ProyectoImagingDataTable(0).Usa_Creacion_Automatica_Destape = " & ProyectoImagingDataTable(0).Usa_Creacion_Automatica_Destape, EventLogEntryType.Information)

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
                    JWriteLog("ProyectoImagingDataTable(0).Usa_Creacion_Automatica_Destape = " & ProyectoImagingDataTable(0).Usa_Creacion_Automatica_Destape, EventLogEntryType.Information)
                    fk_OT = Transferenciarow.fk_OT
                    fk_Fecha_Proceso = Integer.Parse(Transferenciarow.fk_Fecha_Proceso.ToString())
                End If

                _ExtensionAux = IIf(ProyectoImagingDataTable(0).Usa_Cargue_PDF, ".pdf", ProyectoImagingDataTable(0).Extension_Formato_Imagen_Salida).ToString()

                Dim ContenedorDataTable As Program.Contenedor = Validar(ProyectoImagingDataTable(0), Transferenciarow, fk_OT)
                If (ContenedorDataTable.Valido) Then
                    CargarConIndexacion(ProyectoImagingDataTable.Rows(0), Transferenciarow, idCargueDetalleTransferencia, ContenedorDataTable.PrecintoId, ContenedorDataTable.ContenedorId, fk_OT, CStr(fk_Fecha_Proceso).ToString())
                    ActualizarTransferenciaProyectoDestino(idCargueDetalleTransferencia)
                Else
                    JWriteLog("ContenedorDataTable no valido para Id_Transferencia_Synergetics: " & Transferenciarow.id_Transferencia_Synergetics & "Observacion: " & ContenedorDataTable.Observacion, EventLogEntryType.Information)
                    JWriteLog("Cambiando estado a 1 Id_Transferencia_Synergetics: " & Transferenciarow.id_Transferencia_Synergetics, EventLogEntryType.Information)
                    'Inicio de transferencia, cambiar estado a 'Transferir' mientras se cuadra otro estado
                    TransferenciaUpdateType.fk_Estado = 1
                    TransferenciaUpdateType.fk_Cargue = DBNull.Value
                    TransferenciaUpdateType.fk_Cargue_Paquete = DBNull.Value
                    dbmSofTrac.SchemaProcess.TBL_Transferencia_Synergetics.DBUpdate(TransferenciaUpdateType, Transferenciarow.id_Transferencia_Synergetics)
                    JWriteLog("Id_Transferencia_Synergetics: " & Transferenciarow.id_Transferencia_Synergetics & " Cambiado a estado 1 ", EventLogEntryType.Information)
                    ActualizarTransferenciaProyectoDestino(idCargueDetalleTransferencia, ContenedorDataTable.Observacion)
                End If

            Catch ex As Exception
                WriteErrorLog("Error transfiriendo imagenes a Miharu: " + ex.ToString())
                Dim TransferenciaUpdateType As New DBSofTrac.SchemaProcess.TBL_Transferencia_SynergeticsType
                TransferenciaUpdateType.fk_Estado = 1
                TransferenciaUpdateType.fk_Cargue = DBNull.Value
                TransferenciaUpdateType.fk_Cargue_Paquete = DBNull.Value
                dbmSofTrac.SchemaProcess.TBL_Transferencia_Synergetics.DBUpdate(TransferenciaUpdateType, Transferenciarow.id_Transferencia_Synergetics)

                Dim idCargueDetalleTransferencia = dbmSofTrac.SchemaAudit.PA_Cargue_Detalle_Transferencia_Proyecto_Destino_Synergetics.DBExecute(Transferenciarow.id_Transferencia_Synergetics)
                ActualizarTransferenciaProyectoDestino(idCargueDetalleTransferencia, ex.Message)

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

        Public Sub ProcesoHiloArchivos(nParametroHilo As Object)
            Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing

            Dim FilaBatch As DBSofTrac.SchemaProcess.CTA_Get_Batch_MasivoRow = nParametroHilo

            Try
                dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Program.ConnectionStrings.SofTrac)

                dbmSofTrac.Connection_Open(1)

                Dim RutaIn = dbmSofTrac.SchemaConfig.TBL_Parametro_Sistema.DBFindByNombre_Parametro_Sistema("RutaInSynergetics")(0).Valor_Parametro_Sistema

                Select Case FilaBatch.Estado_Batch_Synergetics
                    Case 1 'Todo es de synergetics
                        'Inicio de transferencia de mover archivos, cambiar estado a 'En Transferencia'
                        dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(FilaBatch.BatchID, False, True, False)

                        Dim MovioFile As Boolean = True 'Bandera para desmarcar el BatchID si no llego a mover algun archivo
                        Dim CantArchivos As Integer = 0
                        If Directory.Exists(FilaBatch.BatchDirectory) Then
                            Try
                                My.Computer.FileSystem.CopyDirectory(FilaBatch.BatchDirectory, RutaIn + "\" + FilaBatch.Nombre_Batch, True)
                            Catch ex As Exception
                                WriteErrorLog("Excepcion al mover archivos a Synergetics: " + ex.ToString())
                                MovioFile = False
                            End Try
                        Else
                            WriteErrorLog(String.Format("No se encontro el Directorio {0} para el BatchId {1}", FilaBatch.BatchDirectory.ToString(), FilaBatch.BatchID))
                        End If

                        If MovioFile Then
                            'Aqui va el WebService
                            'Dim respuesta = ConsumirWS(FilaBatch.Item("BatchName").ToString(), CantArchivos)
                            'Dependiendo de la respuesta del WebService actualizar a transferido el BatchID

                            'If respuesta.codret = "00" Then
                            dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(FilaBatch.BatchID, True, False, False)
                            'Else
                            '    'Escribir log de la respuesta del web service 
                            '    WriteErrorLog("Error al consumir el servicio web, error: " + respuesta.codret + " mensaje: " + respuesta.msg)
                            '    'Si no confirmo el servicio web, volvemos a marcar el batch en transferencia
                            '    dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(
                            '                                   CInt(FilaBatch.Item("BatchID").ToString()),
                            '                                   False,
                            '                                   False,
                            '                                   True
                            '                                   )
                            'End If
                        Else
                            'Si no pudo mover algun archivo, volvemos a marcar el batch para ser transferido
                            dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(FilaBatch.BatchID, False, False, True)
                        End If

                    Case 2 'Mezclado
                        Dim MovioFile As Boolean = True 'Bandera para desmarcar el BatchID si no llego a mover algun archivo
                        Dim BatchFiles = dbmSofTrac.SchemaProcess.PA_Get_Batch_Files.DBExecute(FilaBatch.BatchID)

                        'Inicio de transferencia de mover archivos, cambiar estado a 'En Transferencia'
                        dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(FilaBatch.BatchID, False, True, False)

                        For Each BatchFilesrow In BatchFiles
                            Dim RutaFin = RutaIn + BatchFilesrow.BatchName

                            Try
                                If Not Directory.Exists(RutaFin) Then
                                    Directory.CreateDirectory(RutaFin)
                                End If

                                If BatchFilesrow.Tipo = "C" Then
                                    File.Copy(BatchFilesrow.BatchDirectory + "\" + BatchFilesrow.NombreImagen, RutaFin + "\" + BatchFilesrow.NombreImagen, True)
                                ElseIf BatchFilesrow.Tipo = "X" Then
                                    File.Move(BatchFilesrow.BatchDirectory + "\" + BatchFilesrow.NombreImagen, RutaFin + "\" + BatchFilesrow.NombreImagen)
                                End If
                            Catch ex As Exception
                                WriteErrorLog("Excepción al mover archivos a Synergetics: " + ex.StackTrace.ToString())
                                MovioFile = False
                            End Try
                        Next

                        If MovioFile Then

                            'Crear destape automatico

                            dbmSofTrac.Transaction_Begin()

                            Dim Resultado = dbmSofTrac.SchemaProcess.PA_Transmision_Data_Softrac_2.DBExecute(FilaBatch.BatchID, 2)

                            If Not Resultado = "OK" Then
                                WriteErrorLog("Error PA_Transmision_Data_Softrac_2, revise [DB_Softrac].TBL_Transferencia BatchID: " + FilaBatch.BatchID)
                                dbmSofTrac.Transaction_Rollback()

                                'Si no pudo mover algun archivo, volvemos a marcar el batch para ser transferido
                                dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(FilaBatch.BatchID, False, False, True)
                            Else
                                dbmSofTrac.Transaction_Commit()
                                dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(FilaBatch.BatchID, True, False, False)
                            End If
                        Else
                            'Si no pudo mover algun archivo, volvemos a marcar el batch para ser transferido
                            dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(FilaBatch.BatchID, False, False, True)
                        End If

                    Case 3 'Nada es de synergetics
                        'Inicio de transferencia de mover archivos, cambiar estado a 'En Transferencia'
                        dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(FilaBatch.BatchID, False, True, False)

                        dbmSofTrac.Transaction_Begin()

                        Dim Resultado = dbmSofTrac.SchemaProcess.PA_Transmision_Data_Softrac_2.DBExecute(FilaBatch.BatchID, 3)

                        If Not Resultado = "OK" Then
                            WriteErrorLog("Error PA_Transmision_Data_Softrac_2, revise [DB_Softrac].TBL_Transferencia BatchID: " + FilaBatch.BatchID)
                            dbmSofTrac.Transaction_Rollback()

                            'Colocar en estado Transferir
                            dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(FilaBatch.BatchID, False, False, True)
                        Else
                            dbmSofTrac.Transaction_Commit()

                            'Colocar en estado Transferido
                            dbmSofTrac.SchemaProcess.PA_Actualizar_Estado_Batch.DBExecute(FilaBatch.BatchID, True, False, False)
                        End If
                End Select
            Catch ex As Exception
                WriteErrorLog("Error procesando BatchID: " + FilaBatch.BatchID.ToString() + " desde la IBML a Miharu o a Synergetics: " + ex.ToString())
            Finally
                If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
            End Try
        End Sub

        Private Sub Proceso()

            Dim dbmSofTracArchivos As DBSofTrac.DBSofTracDataBaseManager = Nothing
            Dim dbmSofTracTransferencia As DBSofTrac.DBSofTracDataBaseManager = Nothing
            Dim dbmSecurity As DBSecurity.DBSecurityDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                'Toca una conexion por cada proceso (MoverArchivos y TransferenciaSynergetics) ya que si abrimos la misma conexion al tiempo en ambos procesos, nos genera error en el datareader por lo que se abre primero en alguno de los dos
                dbmSofTracArchivos = New DBSofTrac.DBSofTracDataBaseManager(Program.ConnectionStrings.SofTrac)
                dbmSofTracTransferencia = New DBSofTrac.DBSofTracDataBaseManager(Program.ConnectionStrings.SofTrac)

                dbmSecurity = New DBSecurity.DBSecurityDataBaseManager(Program.ConnectionStrings.Security)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)

                While Not Detener
                    If Detener Then Return

                    Try
                        dbmSecurity.Connection_Open(1)
                        dbmSofTracArchivos.Connection_Open(1)
                        dbmSofTracTransferencia.Connection_Open(1)
                        dbmImaging.Connection_Open(1)

                        Dim CalendarioDataTable = dbmSecurity.SchemaConfig.TBL_Calendario.DBFindByfk_EntidadNombre_Calendario(fk_Entidad_Procesamiento, "TransferenciaSynergetics")

                        If CalendarioDataTable.Count > 0 Then
                            id_Calendario = CalendarioDataTable(0).id_Calendario
                        Else
                            WriteErrorLog("No hay calendario programado para el servicio")
                        End If

                        Dim habil = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(fk_Entidad_Procesamiento, id_Calendario)
                        'Thread.Sleep(60000)
                        If (habil) Then

                            Dim ThreadArchivos As New Thread(AddressOf MoverArchivosSynergetics)
                            ThreadArchivos.Start(dbmSofTracArchivos)

                            Dim ThreadTransferencia As New Thread(AddressOf TransferenciaSynergetics)
                            ThreadTransferencia.Start(dbmSofTracTransferencia)

                            Dim continuar = False
                            While continuar = False
                                Thread.Sleep(10000)
                                If ThreadArchivos.ThreadState = ThreadState.Stopped And ThreadTransferencia.ThreadState = ThreadState.Stopped Then

                                    continuar = True
                                    JWriteLog("Finalizo Hilos", EventLogEntryType.Information)
                                End If
                            End While
                        Else
                            WriteErrorLog("No es una hora habil para ejecutar el proceso")
                        End If
                    Catch ex As Exception
                        WriteErrorLog("Error Proceso ex: " & ex.ToString())
                    Finally
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        If (dbmSofTracArchivos IsNot Nothing) Then dbmSofTracArchivos.Connection_Close()
                        If (dbmSofTracTransferencia IsNot Nothing) Then dbmSofTracTransferencia.Connection_Close()
                        If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
                    End Try

                    If Detener Then Return

                    Thread.Sleep(Program.Config.Intervalo) ' Esperar n segundos antes de continuar
                End While
            Catch ex As Exception
                WriteErrorLog("Error Proceso ex: " & ex.ToString())
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmSofTracArchivos IsNot Nothing) Then dbmSofTracArchivos.Connection_Close()
                If (dbmSofTracTransferencia IsNot Nothing) Then dbmSofTracTransferencia.Connection_Close()
                If (dbmSecurity IsNot Nothing) Then dbmSecurity.Connection_Close()
            End Try
        End Sub

        Private Sub MoverArchivosSynergetics(ByVal dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager)
            Try
                Dim BatchsDataTable = dbmSofTrac.SchemaProcess.PA_Get_Batch_Masivo.DBExecute()
                Dim procesadorHilosInstance As New ProcesadorHilos

                procesadorHilosInstance.servicio = Me

                'JWriteLog("Inicio Hilo MoverArchivos", EventLogEntryType.Information)
                If Not IsNothing(BatchsDataTable) Then
                    If BatchsDataTable.Rows.Count > 0 Then
                        For Each FilaBatch As DataRow In BatchsDataTable.Rows
                            procesadorHilosInstance.AgregarHiloArchivos(FilaBatch)
                        Next
                    End If
                End If

                While (procesadorHilosInstance.TerminoHilos = False)
                    System.Threading.Thread.Sleep(1000)
                End While
                'JWriteLog("Finalizo Hilo MoverArchivos", EventLogEntryType.Information)

            Catch ex As Exception
                WriteErrorLog("Excepcion en MoverArchivosSynergetics: " + ex.ToString())
            End Try
        End Sub

        Private Sub TransferenciaSynergetics(ByVal dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager)
            Try

                Dim TransferenciaDataTable = dbmSofTrac.SchemaProcess.CTA_Registros_Transferencia_Synergetics.DBFindByLineafk_Estado("CONVENIOS", 1)

                Dim procesadorHilosInstance As New ProcesadorHilos
                procesadorHilosInstance.servicio = Me

                'JWriteLog("Inicio Hilo Transferencia", EventLogEntryType.Information)
                If Not IsNothing(TransferenciaDataTable) Then
                    If TransferenciaDataTable.Rows.Count > 0 Then
                        For Each TransferenciaRow In TransferenciaDataTable
                            procesadorHilosInstance.AgregarHilo(TransferenciaRow)
                        Next
                    End If
                End If

                While (procesadorHilosInstance.TerminoHilos = False)
                    System.Threading.Thread.Sleep(1000)
                End While
                'JWriteLog("Finalizo Hilo Transferencia", EventLogEntryType.Information)
            Catch ex As Exception
                WriteErrorLog("Excepcion en TransferenciaSynergetics: " + ex.Message)
            End Try
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
                    WriteErrorLog("dbmCore.SchemaProcess.PA_getSiguiente_Fecha_Habil: " + ex.ToString())
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
                WriteErrorLog("Error creando fecha de proceso: " + ex.ToString())
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

        Private Sub CargarConIndexacion(ByVal _ProyectoImagingrow As DBImaging.SchemaConfig.CTA_ProyectoRow, ByVal _TransferenciaRow As DBSofTrac.SchemaProcess.CTA_Registros_Transferencia_SynergeticsRow, ByVal _idCargueDetalleTransferencia As Long, nfk_Precinto As Short, nid_Contenedor As Short, nfk_OT As Integer, nfk_Fecha_Proceso As String)
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
                cargueType.Observaciones = "SYNERGETICS"
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


                'Paquete
                Dim paqueteType As New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()

                paqueteType.fk_Cargue = cargueType.id_Cargue
                paqueteType.id_Cargue_Paquete = 1
                paqueteType.fk_Estado = DBCore.EstadoEnum.Indexacion
                paqueteType.fk_Usuario_Log = 1
                paqueteType.Fecha_Proceso = cargueType.Fecha_Proceso
                paqueteType.Path_Cargue_Paquete = _TransferenciaRow.Ruta_Imagenes
                paqueteType.Bloqueado = False
                paqueteType.Data_Path = _TransferenciaRow.Ruta_Imagenes
                paqueteType.fk_Sede_Procesamiento_Asignada = _TransferenciaRow.fk_Sede_Procesamiento
                paqueteType.fk_Centro_Procesamiento_Asignado = _TransferenciaRow.fk_Centro_Procesamiento

                dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(paqueteType)

                ' Actualizar el contenedor                            
                Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                contenedorType.Cargado = True
                contenedorType.fk_Cargue = cargueType.id_Cargue
                contenedorType.fk_Paquete = 1
                dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, nfk_OT, nfk_Precinto, nid_Contenedor)

                '********Indexacion, captura, cargue o relación de imágenes.*******'
                dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)
                manager.TransactionBegin()

                'Indexacion y captura
                Dim dtResultado = dbmSofTrac.SchemaProcess.PA_Indexacion_Data_Synergetics.DBExecute(CType(cargueType.id_Cargue, Long), 1, _TransferenciaRow.fk_Entidad, _TransferenciaRow.fk_Proyecto, _TransferenciaRow.id_Transferencia_Synergetics, 1)

                dbmImaging.Transaction_Commit()

                JWriteLog("Filas dtResultado para Id_Transferencia_Synergetics " & _TransferenciaRow.id_Transferencia_Synergetics & " : " & dtResultado.Rows.Count, EventLogEntryType.Information)
                If dtResultado.Rows.Count > 0 Then
                    'Agrupamos por Expediente_Folder_File
                    Dim ExpedientesFoldersFiles = From datos In dtResultado
                                                  Where datos.Es_Anexo = False
                                                  Group datos By ExpFolFil = datos.fk_Expediente & "_" &
                                                        datos.fk_Folder & "_" &
                                                        datos.fk_File
                                                  Into FilaAgrupada = Group
                                                  Select ExpFolFil, FilaAgrupada
                    'Agrupamos por anexo
                    Dim Anexos = From datos In dtResultado
                               Where datos.Es_Anexo = True
                               Group datos By Anexo = datos.fk_Anexo
                               Into FilaAgrupada = Group
                               Select Anexo, FilaAgrupada

                    Try
                        'Relacionamos las imagenes
                        For Each expfolfil In ExpedientesFoldersFiles
                            Dim contFolio = 1
                            For Each Fila In expfolfil.FilaAgrupada
                                If System.IO.File.Exists(Fila.Ruta_Imagenes) Then
                                    JWriteLog("Cargando Imagen " & Fila.Ruta_Imagenes, EventLogEntryType.Information)
                                    Dim dataImage = ImageManager.GetData(Fila.Ruta_Imagenes)
                                    Dim dataImageThumbnail = ImageManager.GetThumbnailData(Fila.Ruta_Imagenes, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                                    manager.CreateItem(Fila.fk_Expediente, Fila.fk_Folder, Fila.fk_File, Fila.fk_Version, _ProyectoImagingrow.Extension_Formato_Imagen_Salida, Guid.NewGuid())
                                    manager.CreateFolio(Fila.fk_Expediente, Fila.fk_Folder, Fila.fk_File, Fila.fk_Version, contFolio, dataImage, dataImageThumbnail(0), False)
                                    JWriteLog("Imagen " & Fila.Ruta_Imagenes & " Cargada", EventLogEntryType.Information)
                                    contFolio = contFolio + 1
                                Else
                                    JWriteLog("Imagen " & Fila.Ruta_Imagenes & " no encontrada", EventLogEntryType.Information)
                                    Throw New Exception("Imagen " & Fila.Ruta_Imagenes & " no encontrada, o no se tiene acceso al directorio")
                                End If
                            Next
                        Next
                    Catch ex As Exception
                        WriteErrorLog("Error Asociar Imagenes a Expediente Folder File ex: " & ex.ToString())
                        Throw ex
                    End Try


                    Try
                        'Relacionamos los Anexos
                        For Each Anexo In Anexos
                            Dim contAnexos = 1
                            For Each Fila In Anexo.FilaAgrupada
                                If System.IO.File.Exists(Fila.Ruta_Imagenes) Then
                                    JWriteLog("Cargando Imagen Anexo " & Fila.Ruta_Imagenes, EventLogEntryType.Information)
                                    Dim dataImage = ImageManager.GetData(Fila.Ruta_Imagenes)
                                    Dim dataImageThumbnail = ImageManager.GetThumbnailData(Fila.Ruta_Imagenes, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                                    manager.CreateItem(Fila.fk_Anexo, _ProyectoImagingrow.Extension_Formato_Imagen_Salida)
                                    manager.CreateFolio(Fila.fk_Anexo, contAnexos, dataImage, dataImageThumbnail(0), False)
                                    JWriteLog("Imagen Anexo " & Fila.Ruta_Imagenes & " Cargada", EventLogEntryType.Information)
                                    contAnexos = contAnexos + 1
                                Else
                                    JWriteLog("Imagen Anexo " & Fila.Ruta_Imagenes & " no encontrada", EventLogEntryType.Error)
                                    Throw New Exception("Imagen " & Fila.Ruta_Imagenes & " no encontrada, o no se tiene acceso al directorio")
                                End If
                            Next
                        Next
                    Catch ex As Exception
                        WriteErrorLog("Error Asociando Anexos a Expediente Folder File ex: " & ex.ToString())
                        Throw ex
                    End Try

                    'Insertando en cruce en linea cola
                    For Each expfolfil In ExpedientesFoldersFiles
                        For Each Fila In expfolfil.FilaAgrupada
                            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

                            Try
                                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(Program.ConnectionStrings.Integration)
                                dbmIntegration.Connection_Open(1)

                                dbmIntegration.SchemaBCSCarpetaUnica.PA_New_00_Cruce_En_Linea_Cola.DBExecute(Fila.fk_Expediente, Fila.fk_Folder, Fila.fk_File, "1")
                            Catch ex As Exception
                                JWriteLog("Error insertando en PA_New_00_Cruce_En_Linea_Cola fk_transferencia_synergetics: " & _TransferenciaRow.id_Transferencia_Synergetics & " Expediente-Folder-File" & Fila.fk_Expediente & "-" & Fila.fk_Folder & "-" & Fila.fk_File, EventLogEntryType.Information)
                            Finally
                                If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                            End Try
                        Next
                    Next

                    manager.TransactionCommit()

                    Application.DoEvents()

                    Dim TransferenciaUpdateType As New DBSofTrac.SchemaProcess.TBL_Transferencia_SynergeticsType
                    TransferenciaUpdateType.fk_Estado = 3
                    TransferenciaUpdateType.fk_Cargue = CType(cargueType.id_Cargue, Long)
                    TransferenciaUpdateType.fk_Cargue_Paquete = 1
                    dbmSofTrac.SchemaProcess.TBL_Transferencia_Synergetics.DBUpdate(TransferenciaUpdateType, _TransferenciaRow.id_Transferencia_Synergetics)

                    JWriteLog("Imagenes Cargadas Correctamente para el id_Transferencia_Synergetics: " & _TransferenciaRow.id_Transferencia_Synergetics, EventLogEntryType.Information)
                Else
                    JWriteLog("No se cargaron las Imagenes, SP no devolvio datos para: IdCargue: " & CType(cargueType.id_Cargue, Long) & " IdCarguePaquete: " & 1 & " Entidad: " & _TransferenciaRow.fk_Entidad & " Proyecto: " & _TransferenciaRow.fk_Proyecto & " IdTransferenciaSynergetics: " & _TransferenciaRow.id_Transferencia_Synergetics & " Id Usuario: " & 1, EventLogEntryType.Error)
                    Throw New Exception("SP no devolvio datos para IdTransferenciaSynergetics " & _TransferenciaRow.id_Transferencia_Synergetics)

                    'Elimina el cargue realizado
                    If (Not cargueType.id_Cargue Is Nothing AndAlso (dbmImaging IsNot Nothing) AndAlso (manager IsNot Nothing)) Then
                        ' Actualizar el contenedor
                        Try
                            contenedorType.Cargado = False
                            contenedorType.fk_Cargue = DBNull.Value
                            contenedorType.fk_Paquete = DBNull.Value
                            dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, nfk_OT, nfk_Precinto, nid_Contenedor)

                            dbmImaging.SchemaProcess.PA_Liberar_Contenedores_Cargue.DBExecute(nfk_OT, cargueType.id_Cargue)
                            dbmSofTrac.SchemaProcess.PA_Eliminacion_Cargue.DBExecute(CLng(cargueType.id_Cargue), 1)
                            dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(cargueType.id_Cargue)

                            'Es posible que si no se cargan imagenes en el manager, no se encuentre el cargue
                            manager.DeleteCargue(cargueType.id_Cargue)
                        Catch ex As Exception
                            WriteErrorLog(ex.ToString())
                        End Try
                    End If
                End If
            Catch ex As Exception
                'Elimina el cargue realizado
                If (Not cargueType.id_Cargue Is Nothing AndAlso (dbmImaging IsNot Nothing) AndAlso (manager IsNot Nothing)) Then
                    ' Actualizar el contenedor
                    Try
                        Dim contenedorType = New DBImaging.SchemaProcess.TBL_ContenedorType()
                        contenedorType.Cargado = False
                        contenedorType.fk_Cargue = DBNull.Value
                        contenedorType.fk_Paquete = DBNull.Value
                        dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(contenedorType, nfk_OT, nfk_Precinto, nid_Contenedor)

                        dbmImaging.SchemaProcess.PA_Liberar_Contenedores_Cargue.DBExecute(nfk_OT, cargueType.id_Cargue)
                        dbmSofTrac.SchemaProcess.PA_Eliminacion_Cargue.DBExecute(CLng(cargueType.id_Cargue), 1)
                        dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(cargueType.id_Cargue)

                        'Es posible que si no se cargan imagenes en el manager, no se encuentre el cargue
                        manager.DeleteCargue(cargueType.id_Cargue)
                    Catch ex1 As Exception
                        WriteErrorLog(ex.ToString())
                    End Try
                End If
                WriteErrorLog("Error CargarConIndexacion ex: " & ex.ToString())
                Throw ex
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Private Function Validar(ByVal _ProyectoImagingRow As DBImaging.SchemaConfig.CTA_ProyectoRow, ByVal _TransferenciaRow As DBSofTrac.SchemaProcess.CTA_Registros_Transferencia_SynergeticsRow, _fk_OT As Integer) As Program.Contenedor
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Dim Contenedor As New Program.Contenedor

            'ContenedorValido = False
            'ContenedorMsg = ""

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.ConnectionStrings.Core)

                ' Actualizar los datos del cargue
                dbmImaging.Connection_Open(1)
                dbmCore.Connection_Open(1)


                ' Validar destape
                If (_ProyectoImagingRow.Usa_Destape_Contenedor AndAlso Not _ProyectoImagingRow.Usa_Paquete_x_Imagen) Then
                    ' Validar el contenedor
                    Dim contenedorDataTable = dbmImaging.SchemaProcess.TBL_Contenedor.DBFindByfk_OTToken(_fk_OT, _TransferenciaRow.Contenedor)

                    If (contenedorDataTable.Count = 0) Then
                        Contenedor.Valido = False
                        Contenedor.Observacion = "No se encontró un contenedor que coincida en la OT"
                    ElseIf (contenedorDataTable(0).Cargado) Then
                        Contenedor.Valido = False
                        Contenedor.Observacion = "El contenedor ya fué cargado"
                    Else
                        ' Validar que el precinto este cerrado
                        Dim precintoDataTable = dbmImaging.SchemaProcess.TBL_Precinto.DBGet(_fk_OT, contenedorDataTable(0).fk_Precinto)

                        If (Not precintoDataTable(0).Cerrado) Then
                            Contenedor.Valido = False
                            Contenedor.Observacion = "El Precinto: " & precintoDataTable(0).Precinto & ", no esta cerrado"
                        Else
                            Contenedor.PrecintoId = contenedorDataTable(0).fk_Precinto
                            Contenedor.ContenedorId = contenedorDataTable(0).id_Contenedor
                            Contenedor.CBarrasContenedor = contenedorDataTable(0).Token
                            Contenedor.Valido = True
                            'ContenedorValido = True
                        End If
                    End If
                End If

                Return Contenedor

            Catch ex As Exception
                WriteErrorLog("Error Validar ex: " & ex.ToString())
                Contenedor.Valido = False : Contenedor.Observacion = ex.Message
                Return Contenedor
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Function

        Private Sub ActualizarTransferenciaProyectoDestino(ByVal _idCargueDetalleTransferencia As Long, Optional ByVal _Mensaje As String = "")
            Dim dbmSofTrac As DBSofTrac.DBSofTracDataBaseManager = Nothing

            Try
                dbmSofTrac = New DBSofTrac.DBSofTracDataBaseManager(Program.ConnectionStrings.SofTrac)
                dbmSofTrac.Connection_Open(1)

                dbmSofTrac.SchemaAudit.PA_Cargue_Detalle_Transferencia_Proyecto_Destino_Synergetics_Fin.DBExecute(_idCargueDetalleTransferencia, _Mensaje.ToString())


            Catch ex As Exception
                WriteErrorLog("Error ActualizarTransferenciaProyectoDestino ex: " & ex.ToString())
            Finally
                If (dbmSofTrac IsNot Nothing) Then dbmSofTrac.Connection_Close()
            End Try

        End Sub

        Public Sub JWriteLog(ByVal mensaje As String, ByVal tipo As EventLogEntryType)

            If Not EventLog.SourceExists("SynergeticsService") Then
                EventLog.CreateEventSource("SynergeticsService", "Application")
            End If

            Dim eventLog1 As EventLog = New EventLog()
            eventLog1.Source = "SynergeticsService"
            eventLog1.WriteEntry(mensaje, tipo)

        End Sub

        Private Sub WriteErrorLog(ByVal nMessage As String)

            SyncLock objectLock

                Try
                    'JWriteLog("WriteErrorLog Path: " & Program.AppDataPath & "log_" & Now.ToString("yyyyMMdd") & ".txt", EventLogEntryType.Information)

                    'JWriteLog(nMessage, EventLogEntryType.Error)

                    Dim sw As New StreamWriter(Program.AppDataPath & "log_" & Now.ToString("yyyyMMdd") & ".txt", True)

                    sw.WriteLine("--------------------------------------------------------------")
                    sw.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    sw.WriteLine("Mensaje: " & nMessage)
                    sw.WriteLine("--------------------------------------------------------------")
                    sw.WriteLine("")

                    sw.Flush()
                    sw.Close()
                Catch ex As Exception
                    Try : JWriteLog(ex.ToString(), EventLogEntryType.Error) : Catch : End Try
                End Try
                Windows.Forms.Application.DoEvents()

            End SyncLock
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
                WriteErrorLog("Error LoadImagesDirectories ex: " & ex.ToString())
            End Try
            n_LoadImagesDirectories = False
            Return False
        End Function

        Private Function getTamaño(ByVal nFileName As String) As Long
            Dim Archivo As New FileInfo(nFileName)
            ' retornar el valor en Bytes
            Return Archivo.Length
        End Function

        Private Function ConsumirWS(ByVal nomBatch As String, ByVal cantidad As Integer) As RespuestaWS
            Try
                Dim url = "http://10.65.52.53:8080/brinks-iknoplus/rest/carpunica/images"
                Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
                Dim usr = ""
                Dim pwd = ""
                Dim codificado = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(usr + ":" + pwd))
                request.Headers.Add("Authorization", "Basic " + codificado)
                request.Method = "POST"
                request.ContentType = "application/json"

                Using streamWriter As New StreamWriter(request.GetRequestStream())
                    Dim datos As String = "{'batchname':'" + nomBatch + "','pags':" + cantidad + "}"
                    streamWriter.Write(datos)
                End Using


                Dim httpResponse = CType(request.GetResponse(), HttpWebResponse)

                Dim Json As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(RespuestaWS))
                Dim Respuesta = CType(Json.ReadObject(httpResponse.GetResponseStream()), RespuestaWS)

                Return Respuesta
            Catch ex As Exception
                WriteErrorLog("Error en ConsumirWS: " + ex.ToString())
                Dim respuesta As New RespuestaWS
                respuesta.codret = "99"
                respuesta.msg = ex.Message
                Return respuesta
            End Try
        End Function

#End Region

    End Class


End Namespace
