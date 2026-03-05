Imports System
Imports System.Configuration
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Runtime.Remoting.Lifetime
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports DBCore
Imports DBCore.Schemadbo
Imports DBImaging
Imports DBImaging.SchemaProcess
Imports DBIntegration
Imports DBIntegration.SchemaBcoBogota
Imports Miharu.FileProvider.Library
Imports Slyg.Data
Imports Slyg.Tools
Imports Slyg.Tools.Imaging
Imports Slyg.Tools.Zip


Namespace Clases

    Public Class FileSendingServer
        Inherits MarshalByRefObject

#Region " Estructuras "

        Private Structure FileComponent
            Public Definition As FileSendingDefinitionServer
            Public File As FileStream
        End Structure

#End Region

#Region " Declaraciones "
        Public Const MaxThumbnailWidth As Integer = 60
        Public Const MaxThumbnailHeight As Integer = 80

        Private _Files As New Dictionary(Of String, FileComponent)

        Private _Detener As Boolean = False
        Private _TempFolder As String
        Private _OutputFolder As String

        Private _CoreConnectionString As String
        Private _ImagingConnectionString As String
        Private _BanagrarioConnectionString As String
        Private _ToolsConnectionString As String
        Private _IntegrationConnectionString As String

        Private _IdentifierDateFormat As String

        Private _DataRemoting As String

        Public Property FechaProcesoInt() As Integer
        Public Property NewOT() As Integer

        Public CargueLote As New DBImaging.SchemaProcess.TBL_Lote_DigitalizacionType()
        Public CargueType As New DBImaging.SchemaProcess.TBL_CargueType()
        Public FechaProcesoType As New DBImaging.SchemaProcess.TBL_Fecha_ProcesoType()
        Public OT_Type As New DBImaging.SchemaProcess.TBL_OTType()
        Public ContenedorType As New DBImaging.SchemaProcess.TBL_ContenedorType()

        Private Shared RutaLogError As String
        Private Shared LogActivo As String

#End Region

#Region " Propiedades "

        Public ReadOnly Property InstanceHash() As Integer
            Get
                Return Me.GetHashCode()
            End Get
        End Property

        Public ReadOnly Property TempFolder As String
            Get
                Return _TempFolder
            End Get
        End Property

        Public ReadOnly Property OutputFolder As String
            Get
                Return _OutputFolder
            End Get
        End Property

#End Region

#Region " Constructores "
        Public Sub New(ByVal nWorkingFolder As String, ByVal nCoreConnectionString As String, ByVal nImagingConnectionString As String, ByVal nBanagrarioConnectionString As String, ByVal nIdentifierDateFormat As String, ByVal nDataRemoting As String, ByVal nToolsConnectionString As String, ByVal nIntegrationConnectionString As String, ByVal nFolderLog As String, ByVal nLogActivo As Boolean)
            InitializeLifetimeService()
            RutaLogError = Trim(nFolderLog)
            If nLogActivo Then
                LogActivo = "S"
            Else
                LogActivo = "N"
            End If
            _TempFolder = nWorkingFolder.TrimEnd("\"c) + "\Temp\"
            _OutputFolder = nWorkingFolder.TrimEnd("\"c) + "\Output\"

            If (Not Directory.Exists(_TempFolder)) Then Directory.CreateDirectory(_TempFolder)
            If (Not Directory.Exists(_OutputFolder)) Then Directory.CreateDirectory(_OutputFolder)

            _CoreConnectionString = nCoreConnectionString
            _ImagingConnectionString = nImagingConnectionString
            _BanagrarioConnectionString = nBanagrarioConnectionString
            '_SecurityConnectionString = nSecurityConnectionString
            _ToolsConnectionString = nToolsConnectionString
            _IntegrationConnectionString = nIntegrationConnectionString
            _IdentifierDateFormat = nIdentifierDateFormat
            _DataRemoting = nDataRemoting



            CargarDefiniciones()

            Dim ProcesoBorradoVencidos As New Thread(AddressOf BorrarVencidos)
            ProcesoBorradoVencidos.Start()

        End Sub
        Protected Overrides Sub Finalize()
            _Detener = True

            ' Almacenar las definiciones
            For Each Componente In _Files.Values
                Try
                    FileSendingDefinitionServer.Serialize(Componente.Definition, Componente.Definition.ID & ".part")
                    Componente.File.Close()
                Catch

                End Try
            Next

            MyBase.Finalize()
        End Sub

#End Region

#Region " Metodos "

        Private Sub CargarDefiniciones()
            Dim FileNames As String()
            _Files.Clear()

            ' Borrar los archivos que no tengan definición
            FileNames = Directory.GetFiles(_TempFolder)
            For Each FileName In FileNames
                If (Path.GetExtension(FileName).ToLower() <> ".definition") Then
                    If (Not File.Exists(Path.GetFileNameWithoutExtension(FileName) & ".definition")) Then
                        Try
                            File.Delete(FileName)
                        Catch
                        End Try
                    End If
                End If
            Next

            ' Cargar definiciones
            FileNames = Directory.GetFiles(_TempFolder, "*.definition")
            For Each FileName In FileNames
                If (Not File.Exists(Path.GetFileNameWithoutExtension(FileName) & ".part")) Then
                    File.Delete(FileName)
                Else
                    Try
                        Dim Definicion = FileSendingDefinitionServer.Deserialize(FileName)

                        Dim Componente As New FileComponent()
                        Componente.Definition = Definicion
                        Componente.File = New FileStream(Path.GetFileNameWithoutExtension(FileName) & ".part", FileMode.Open, FileAccess.Write)
                        _Files.Add(Definicion.ID, Componente)
                    Catch
                    End Try
                End If
            Next
        End Sub

        Private Sub BorrarVencidos()
            Do
                Try


                    ' Buscar los compoenentes vencidos
                    Dim Eliminar As New List(Of String)
                    For Each Componente In _Files.Values
                        If (Componente.Definition.LastUpdate.AddDays(2) < Now) Then
                            Eliminar.Add(Componente.Definition.ID)
                        End If
                    Next

                    ' Eliminar los compoentes vencidos
                    For Each Elemento In Eliminar
                        Try
                            Dim Componente = _Files(Elemento)

                            _Files.Remove(Elemento)

                            Componente.File.Close()
                            File.Delete(_TempFolder & Componente.Definition.ID & ".part")
                            File.Delete(_TempFolder & Componente.Definition.ID & ".definition")
                        Catch

                        End Try
                    Next

                Catch ex As Exception

                End Try

                Thread.Sleep(50000)
            Loop While (Not _Detener)
        End Sub

        Private Sub GetFiles(ByRef nArchivos As List(Of String), ByVal nPath As String, ByVal nPatron As String)
            Dim Archivos = Directory.GetFiles(nPath, nPatron)

            For Each Archivo In Archivos
                nArchivos.Add(Archivo)
            Next
        End Sub

        Public Shared Sub LogError(ByVal mensaje As String)
            If LogActivo <> "S" Then
                Exit Sub
            End If
            'Dim logPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "FileSendingServer_Error.log")
            Dim logPath As String = RutaLogError & "\Log_" & Format(DateTime.Now, "yyyyMMdd_HH") & ".log"
            Try
                ' Asegura que el directorio exista
                Dim logDir As String = Path.GetDirectoryName(logPath)
                If Not Directory.Exists(logDir) Then
                    Directory.CreateDirectory(logDir)
                End If

                ' Escribe el mensaje en el archivo de log
                Using writer As StreamWriter = New StreamWriter(logPath, True)
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR: {mensaje}")
                End Using
            Catch ex As Exception
                ' Si falla el log, puedes escribir en el Event Viewer como respaldo
                EventLog.WriteEntry("ServicioWindows", $"Error al escribir en el log: {ex.Message}", EventLogEntryType.Error)
            End Try
        End Sub

#End Region

#Region " Funciones "

        Public Overrides Function InitializeLifetimeService() As Object
            Dim lease As ILease = CType(MyBase.InitializeLifetimeService(), ILease)

            If lease.CurrentState = LeaseState.Initial Then
                lease.InitialLeaseTime = TimeSpan.Zero
            End If
            Return lease
        End Function

        'Public Function SendFile(ByVal nDefinition As FileSendingDefinitionClient, ByRef nLastPart As Integer, ByVal nOficina As Integer, ByVal nFecha_Movimiento As DateTime, ByVal nfk_Movimiento_Tipo As Short, ByVal nUser As Integer, ByRef MsgError As String, ByRef nLogTransmision As Integer) As Boolean
        '    Dim dbmBanagrario As DBAgrarioDataBaseManager = Nothing

        '    Try
        '        Dim NewComponent As FileComponent

        '        If (Not _Files.ContainsKey(nDefinition.ID)) Then
        '            NewComponent = New FileComponent()
        '            NewComponent.Definition = New FileSendingDefinitionServer(nDefinition)
        '            NewComponent.File = New FileStream(_TempFolder & nDefinition.ID & ".part", FileMode.Create, FileAccess.Write)

        '            Dim Data(CInt(nDefinition.FileSize - 1)) As Byte
        '            NewComponent.File.Write(Data, 0, Data.Length)

        '            _Files.Add(nDefinition.ID, NewComponent)
        '        Else
        '            NewComponent = _Files(nDefinition.ID)
        '        End If

        '        nLastPart = NewComponent.Definition.LastSentPart

        '        dbmBanagrario = New DBAgrarioDataBaseManager(_BanagrarioConnectionString)
        '        'dbmBanagrario.DataBase.Identifier_Date_Format = _IdentifierDateFormat

        '        dbmBanagrario.Connection_Open(1)

        '        dbmBanagrario.Transaction_Begin()

        '        nLogTransmision = dbmBanagrario.SchemaProcess.PA_Insertar_Log_Transmision.DBExecute(nOficina, _
        '                                                                                            Now, _
        '                                                                                            nfk_Movimiento_Tipo, _
        '                                                                                            nUser)

        '        dbmBanagrario.Transaction_Commit()

        '        Return True
        '    Catch ex As Exception
        '        dbmBanagrario.Transaction_Rollback()

        '        MsgError = ex.Message
        '        Return False

        '    Finally
        '        If (dbmBanagrario IsNot Nothing) Then dbmBanagrario.Connection_Close()
        '    End Try
        'End Function

        'Este DDO
        Public Function SendFile(ByVal nDefinition As FileSendingDefinitionClient, ByRef nLastPart As Integer, ByVal nUser As Integer, ByRef MsgError As String, ByRef nLogTransmision As Integer) As Boolean
            Try
                Dim NewComponent As FileComponent

                If (Not _Files.ContainsKey(nDefinition.ID)) Then
                    NewComponent = New FileComponent()
                    NewComponent.Definition = New FileSendingDefinitionServer(nDefinition)
                    NewComponent.File = New FileStream(_TempFolder & nDefinition.ID & ".part", FileMode.Create, FileAccess.Write)

                    Dim Data(CInt(nDefinition.FileSize - 1)) As Byte
                    NewComponent.File.Write(Data, 0, Data.Length)

                    _Files.Add(nDefinition.ID, NewComponent)
                Else
                    NewComponent = _Files(nDefinition.ID)
                End If

                nLastPart = NewComponent.Definition.LastSentPart

                Return True
            Catch ex As Exception
                MsgError = ex.Message
                Return False
            End Try
        End Function
        Public Function SendPart(ByVal nFileIdentifier As String, ByVal nData As Byte(), ByVal nPart As Integer, ByRef MsgError As String) As Boolean
            Try
                If (_Files.ContainsKey(nFileIdentifier)) Then
                    Dim Componente = _Files(nFileIdentifier)

                    If (nPart = Componente.Definition.LastSentPart + 1) Then
                        Dim Inicio As Integer = Componente.Definition.PackageSize * (Componente.Definition.LastSentPart + 1)

                        Componente.File.Position = Inicio
                        Componente.File.Write(nData, 0, nData.Length)
                        Componente.Definition.LastSentPart += 1

                        If (Componente.Definition.LastSentPart + 1 >= Componente.Definition.FilePackages) Then
                            _Files.Remove(nFileIdentifier)

                            Componente.File.Close()

                            File.Move(_TempFolder & Componente.Definition.ID & ".part", _OutputFolder & Componente.Definition.FileName)
                            File.Delete(_TempFolder & Componente.Definition.ID & ".definition")


                        End If

                        Return True
                    Else
                        MsgError = "Se esperaba la parte " & CStr(Componente.Definition.LastSentPart + 1)
                        Return False
                    End If
                Else
                    MsgError = "El archivo no existe"
                    Return False
                End If
            Catch ex As Exception
                MsgError = ex.Message
                Return False

            End Try
        End Function

        Public Function Cancel(ByVal nFileIdentifier As String, ByRef MsgError As String) As Boolean
            Try
                If (_Files.ContainsKey(nFileIdentifier)) Then
                    Dim Componente = _Files(nFileIdentifier)

                    _Files.Remove(nFileIdentifier)

                    Componente.File.Close()

                    File.Delete(_TempFolder & Componente.Definition.ID & ".part")
                    File.Delete(_TempFolder & Componente.Definition.ID & ".definition")

                    Return True
                Else
                    MsgError = "El archivo no existe"
                    Return False
                End If
            Catch ex As Exception
                MsgError = ex.Message
                Return False

            End Try
        End Function

        'Public Function Cargar(ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nFolder As String, ByVal nPaqueteName As String, ByVal nObservaciones As String, ByVal nEntidadProcesamiento As Short, ByVal nSedeProcesamiento_Cargue As Short, ByVal nCentroProcesamiento_Cargue As Short, ByVal nIdLog As Integer, ByVal nOficina As Integer, ByVal nUser As Integer, ByVal nEstado As Short, ByRef nCargueID As Integer, ByRef nFolios As Short, ByRef MsgError As String) As Boolean
        '    Dim dbmCore As DBCoreDataBaseManager = Nothing
        '    Dim IdCargue As Integer

        '    Try
        '        dbmCore = New DBCoreDataBaseManager(_CoreConnectionString)

        '        dbmCore.Connection_Open(1)

        '        ' Validar si existe el directorio
        '        If (Not Directory.Exists(_OutputFolder + nFolder)) Then
        '            Throw New Exception("No se encontró el directorio: " + _OutputFolder + nFolder)
        '        End If

        '        'Validar si existen imagenes
        '        Dim Imagenes = New List(Of String)()
        '        GetFiles(Imagenes, _OutputFolder + nFolder, "*.jpg")
        '        GetFiles(Imagenes, _OutputFolder + nFolder, "*.gif")
        '        GetFiles(Imagenes, _OutputFolder + nFolder, "*.bmp")
        '        GetFiles(Imagenes, _OutputFolder + nFolder, "*.png")
        '        GetFiles(Imagenes, _OutputFolder + nFolder, "*.tif")

        '        If (Imagenes.Count = 0) Then
        '            Throw New Exception("No se encontraron imagenes para cargar en el directorio: " + _OutputFolder + nFolder)
        '        End If

        '        Dim EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(nEntidadCliente, nProyecto, nEsquema)

        '        If (EsquemaDataTable.Count > 0) Then
        '            Dim CargueType As New TBL_CargueType()
        '            Dim FechaProcesoType As New DBImaging.SchemaProcess.TBL_Fecha_ProcesoType()
        '            Dim OT_Type As New DBImaging.SchemaProcess.TBL_OTType()
        '            Dim dbmImaging As DBImagingDataBaseManager = Nothing
        '            Dim dbmBanagrario As DBAgrarioDataBaseManager = Nothing
        '            Dim manager As FileProviderManager = Nothing

        '            Try
        '                dbmImaging = New DBImagingDataBaseManager(_ImagingConnectionString)
        '                dbmBanagrario = New DBAgrarioDataBaseManager(_BanagrarioConnectionString)

        '                'dbmImaging.DataBase.Identifier_Date_Format = _IdentifierDateFormat
        '                '//dbmBanagrario.DataBase.Identifier_Date_Format = _IdentifierDateFormat

        '                dbmImaging.Connection_Open(1)
        '                dbmBanagrario.Connection_Open(1)

        '                Dim ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(EsquemaDataTable(0).fk_Entidad_Servidor, EsquemaDataTable(0).fk_Servidor)
        '                Dim CentroProcesamientoDataTable = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(nEntidadProcesamiento, nSedeProcesamiento_Cargue, nCentroProcesamiento_Cargue)

        '                If (CentroProcesamientoDataTable.Count > 0) Then
        '                    Dim nSedeProcesamiento As Short = CentroProcesamientoDataTable(0).fk_Sede_Asignada
        '                    Dim nCentroProcesamiento As Short = CentroProcesamientoDataTable(0).fk_Centro_Procesamiento_Asignado

        '                    Dim FechaProceso = DateTime.Now
        '                    Dim horaCambioProceso = dbmBanagrario.SchemaProcess.PA_Get_Hora_Cambio_Fecha_Proceso.DBExecute()

        '                    manager = New FileProviderManager(ServidorDataTable(0).ToCTA_ServidorSimpleType(), CentroProcesamientoDataTable(0).ToCTA_Centro_ProcesamientoSimpleType(), dbmImaging, 1)
        '                    manager.Connect()

        '                    ' Transmisión del día 1 para proceso de día 2
        '                    If (FechaProceso.Hour > horaCambioProceso And FechaProceso.Hour < 24) Then
        '                        FechaProceso = FechaProceso.AddDays(1)
        '                    Else
        '                        FechaProceso = FechaProceso
        '                    End If


        '                    FechaProceso = New DateTime(FechaProceso.Year, FechaProceso.Month, FechaProceso.Day)
        '                    FechaProceso = dbmBanagrario.SchemaProcess.PA_getSiguiente_Fecha_Habil.DBExecute(FechaProceso)

        '                    'Crear la fecha de proceso
        '                    Dim DatosFechaProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(nEntidadProcesamiento, nEntidadCliente, nProyecto, CType(Integer.Parse(FechaProceso.ToString("yyyyMMdd")), Global.Slyg.Tools.SlygNullable(Of Short)))

        '                    If DatosFechaProcesoDataTable.Count = 0 Then

        '                        FechaProcesoType.fk_Entidad_Procesamiento = nEntidadProcesamiento
        '                        FechaProcesoType.fk_Entidad = nEntidadCliente
        '                        FechaProcesoType.fk_Proyecto = nProyecto
        '                        FechaProcesoType.id_fecha_proceso = Integer.Parse(FechaProceso.ToString("yyyyMMdd"))
        '                        FechaProcesoType.Fecha_Proceso = FechaProceso
        '                        FechaProcesoType.fk_Usuario_Apertura = nUser
        '                        FechaProcesoType.Fecha_Apertura = SlygNullable.SysDate
        '                        FechaProcesoType.Cerrado = False

        '                        dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBInsert(FechaProcesoType)
        '                        Me.FechaProcesoInt = FechaProcesoType.id_fecha_proceso.Value
        '                    Else
        '                        Me.FechaProcesoInt = DatosFechaProcesoDataTable(0).id_fecha_proceso
        '                    End If


        '                    'Crear la OT de imágenes
        '                    Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerradofk_Entidad_Procesamiento(nEntidadCliente, nProyecto, Integer.Parse(FechaProceso.ToString("yyyyMMdd")), False, nEntidadProcesamiento)

        '                    If OTDataTable.Count = 0 Then

        '                        dbmImaging.Transaction_Begin()

        '                        OT_Type.fk_Entidad_Procesamiento = nEntidadProcesamiento
        '                        OT_Type.fk_Entidad = nEntidadCliente
        '                        OT_Type.fk_Proyecto = nProyecto
        '                        OT_Type.fk_fecha_proceso = Me.FechaProcesoInt
        '                        OT_Type.fk_OT_Tipo = CShort(1) 'Basica
        '                        OT_Type.fk_Sede_Procesamiento = nSedeProcesamiento_Cargue
        '                        OT_Type.fk_Centro_Procesamiento = nCentroProcesamiento_Cargue
        '                        OT_Type.fk_Usuario_Apertura = nUser
        '                        OT_Type.Fecha_Apertura = SlygNullable.SysDate
        '                        OT_Type.Exportado = False
        '                        OT_Type.Cerrado = False
        '                        OT_Type.id_OT = dbmImaging.SchemaProcess.TBL_OT.DBNextId()

        '                        dbmImaging.SchemaProcess.TBL_OT.DBInsert(OT_Type)

        '                        dbmImaging.Transaction_Commit()

        '                        Me.NewOT = OT_Type.id_OT.Value
        '                    Else
        '                        Me.NewOT = OTDataTable(0).id_OT
        '                    End If

        '                    ' Crear el cargue
        '                    CargueType.fk_Entidad = nEntidadCliente
        '                    CargueType.fk_Proyecto = nProyecto
        '                    CargueType.fk_Estado = EstadoEnum.Creado
        '                    CargueType.fk_Entidad_Procesamiento = nEntidadProcesamiento
        '                    CargueType.fk_Sede_Procesamiento_Cargue = nSedeProcesamiento_Cargue
        '                    CargueType.fk_Centro_Procesamiento_Cargue = nCentroProcesamiento_Cargue
        '                    CargueType.fk_Entidad_Servidor = ServidorDataTable(0).fk_Entidad
        '                    CargueType.fk_Servidor = ServidorDataTable(0).id_Servidor
        '                    CargueType.fk_OT = Me.NewOT
        '                    CargueType.Fecha_Proceso = FechaProceso
        '                    CargueType.fk_Usuario_Log = nUser
        '                    CargueType.Observaciones = nObservaciones

        '                    IdCargue = dbmImaging.SchemaProcess.PA_Guardar_TBL_Cargue.DBExecute(
        '                        CargueType.fk_Entidad,
        '                        CargueType.fk_Proyecto,
        '                        CargueType.fk_Estado,
        '                        CargueType.fk_Entidad_Procesamiento,
        '                        CargueType.fk_Sede_Procesamiento_Cargue,
        '                        CargueType.fk_Centro_Procesamiento_Cargue,
        '                        CargueType.fk_Entidad_Servidor,
        '                        CargueType.fk_Servidor,
        '                        CargueType.fk_OT,
        '                        CargueType.Fecha_Proceso,
        '                        CargueType.Observaciones,
        '                        CargueType.fk_Usuario_Log)

        '                    ' Crear el paquete
        '                    Dim PaqueteType = New TBL_Cargue_PaqueteType()

        '                    PaqueteType.fk_Cargue = IdCargue
        '                    PaqueteType.id_Cargue_Paquete = CShort(1)
        '                    PaqueteType.fk_Estado = nEstado
        '                    PaqueteType.fk_Usuario_Log = nUser
        '                    PaqueteType.Fecha_Proceso = SlygNullable.SysDate
        '                    PaqueteType.Path_Cargue_Paquete = nPaqueteName
        '                    PaqueteType.Data_Path = nPaqueteName & nPaqueteName
        '                    PaqueteType.fk_Sede_Procesamiento_Asignada = nSedeProcesamiento
        '                    PaqueteType.fk_Centro_Procesamiento_Asignado = nCentroProcesamiento

        '                    dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(PaqueteType)

        '                    ' Crear los items
        '                    For i As Integer = 0 To Imagenes.Count - 1
        '                        Using Archivo As New FileStream(Imagenes(i), FileMode.Open, FileAccess.Read)
        '                            Dim Data(CInt(Archivo.Length - 1)) As Byte

        '                            Archivo.Read(Data, 0, Data.Length)

        '                            ' Item Imaging
        '                            Dim CargueItemType = New TBL_Cargue_ItemType()

        '                            CargueItemType.fk_Cargue = IdCargue
        '                            CargueItemType.fk_Cargue_Paquete = PaqueteType.id_Cargue_Paquete
        '                            CargueItemType.id_Cargue_Item = i + 1
        '                            CargueItemType.Folios_Cargue_Item = CShort(1)
        '                            CargueItemType.Tamaño_Cargue_Item = Data.Length
        '                            CargueItemType.Path_Cargue_Item = Path.GetFileName(Imagenes(i))
        '                            CargueItemType.Key_Cargue_Item = Path.GetFileNameWithoutExtension(CargueItemType.Path_Cargue_Item)
        '                            CargueItemType.fk_Estado = nEstado
        '                            CargueItemType.fk_Usuario_Log = nUser
        '                            CargueItemType.Bloqueado = False

        '                            dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert(CargueItemType)

        '                            manager.CreateItem(CargueItemType.fk_Cargue, CargueItemType.fk_Cargue_Paquete, CargueItemType.id_Cargue_Item)

        '                            Dim DataThumbnailMemoryStream As MemoryStream = New MemoryStream()
        '                            ImageManager.GetThumbnail(New Bitmap(Image.FromStream(New MemoryStream(Data))), MaxThumbnailWidth, MaxThumbnailHeight).Save(DataThumbnailMemoryStream, Imaging.ImageFormat.Jpeg)

        '                            manager.CreateFolio(CargueItemType.fk_Cargue, CargueItemType.fk_Cargue_Paquete, CargueItemType.id_Cargue_Item, CShort(1), Data, DataThumbnailMemoryStream.GetBuffer(), False)

        '                            ' Insertar Folio
        '                            Dim FolioType = New DBImaging.SchemaProcess.TBL_Cargue_FolioType()
        '                            FolioType.fk_Cargue = CargueItemType.fk_Cargue
        '                            FolioType.fk_Cargue_Paquete = CargueItemType.fk_Cargue_Paquete
        '                            FolioType.fk_Cargue_Item = CargueItemType.id_Cargue_Item
        '                            FolioType.id_Folio = CShort(1)
        '                            FolioType.Indexado = False
        '                            dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBInsert(FolioType)

        '                            Archivo.Close()
        '                            Archivo.Dispose()
        '                        End Using
        '                    Next

        '                    ' Borrar los archivos                        
        '                    Directory.Delete(_OutputFolder + nFolder, True)

        '                    nCargueID = IdCargue
        '                    nFolios = CShort(Imagenes.Count)


        '                    '---------------------------------------------------------------------------
        '                    ' Actualizar Dashboard
        '                    '---------------------------------------------------------------------------

        '                    Dim DashboardPaquetesType = New TBL_Dashboard_PaquetesType()
        '                    DashboardPaquetesType.fk_Cargue = IdCargue 'CargueType.id_Cargue
        '                    DashboardPaquetesType.fk_Cargue_Paquete = PaqueteType.id_Cargue_Paquete
        '                    DashboardPaquetesType.fk_Entidad_Procesamiento = CargueType.fk_Entidad_Procesamiento
        '                    DashboardPaquetesType.fk_Sede_Procesamiento = PaqueteType.fk_Sede_Procesamiento_Asignada
        '                    DashboardPaquetesType.fk_Centro_Procesamiento = PaqueteType.fk_Centro_Procesamiento_Asignado
        '                    DashboardPaquetesType.fk_Entidad = CargueType.fk_Entidad
        '                    DashboardPaquetesType.fk_Proyecto = CargueType.fk_Proyecto
        '                    DashboardPaquetesType.fk_OT = CargueType.fk_OT
        '                    dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBInsert(DashboardPaquetesType)
        '                    '---------------------------------------------------------------------------

        '                    ' Actualizar el estado del cargue
        '                    CargueType = New TBL_CargueType()
        '                    CargueType.fk_Estado = nEstado
        '                    dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, IdCargue)

        '                    dbmBanagrario.Transaction_Begin()

        '                    'Dim LogType = New TBL_Log_TransmisionType()
        '                    'LogType.Fecha_Fin = SlygNullable.SysDate
        '                    'LogType.fk_Cargue = IdCargue

        '                    dbmBanagrario.SchemaProcess.PA_Actualizar_Log_Transmision.DBExecute(nIdLog, IdCargue)
        '                    'dbmBanagrario.SchemaProcess.TBL_Log_Transmision.DBUpdate(LogType, nIdLog)
        '                    dbmBanagrario.Transaction_Commit()

        '                    MsgError = ""
        '                    Return True
        '                Else
        '                    MsgError = "No se encontró una Sede y Centro de Procesamiento asignados para el cargue"
        '                    Return False
        '                End If
        '            Catch
        '                'Elimina el cargue realizado
        '                If Not IsNothing(CargueType.id_Cargue) Then
        '                    dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(CargueType.id_Cargue)
        '                    dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(CargueType.id_Cargue, Nothing)
        '                    manager.DeleteCargue(CargueType.id_Cargue)
        '                End If

        '                Throw
        '            Finally
        '                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        '                If (dbmBanagrario IsNot Nothing) Then dbmBanagrario.Connection_Close()
        '                If (manager IsNot Nothing) Then manager.Disconnect()
        '            End Try
        '        Else
        '            MsgError = "No se encontro el esquema con código: fk_Entidad=" & nEntidadCliente & ", fk_Proyecto=" & nProyecto & ", id_Esquema=" & nEsquema
        '            Return False
        '        End If
        '    Catch ex As Exception
        '        MsgError = ex.Message
        '        Return False
        '    Finally
        '        If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
        '    End Try
        'End Function

        Public Function Cargar(ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nFolder As String, ByVal nPaqueteName As String, ByVal nObservaciones As String, ByVal nEntidadProcesamiento As Short, ByVal nSedeProcesamiento_Cargue As Short, ByVal nCentroProcesamiento_Cargue As Short, ByVal nIdLog As Integer, ByVal nUser As Integer, ByVal nEstado As Short, ByVal nContenedor As String, ByRef nCargueID As Integer, ByRef nFolios As Short, ByRef MsgError As String) As Boolean
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim IdCargue As Integer

            Try

                dbmCore = New DBCoreDataBaseManager(_CoreConnectionString)

                Try
                    dbmCore.Connection_Open(1)
                Catch ex As Exception
                    MsgError = "Error con la cadena de conexion: " & _CoreConnectionString.ToString() & " ex: " & ex.Message
                    Return False
                End Try


                ' Validar si existe el directorio
                If (Not Directory.Exists(_OutputFolder + nFolder)) Then
                    Throw New Exception("No se encontró el directorio: " + _OutputFolder + nFolder)
                End If

                'Validar si existen imagenes
                Dim Imagenes = New List(Of String)()
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.jpg")
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.gif")
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.bmp")
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.png")
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.tif")

                If (Imagenes.Count = 0) Then
                    Throw New Exception("No se encontraron imagenes para cargar en el directorio: " + _OutputFolder + nFolder)
                End If

                Dim EsquemaDataTable As DBCore.SchemaConfig.TBL_EsquemaDataTable
                Try
                    EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(nEntidadCliente, nProyecto, nEsquema)
                Catch ex As Exception
                    Throw New Exception("dbmCore.SchemaConfig.TBL_Esquema.DBGet: nEntidadCliente = " + nEntidadCliente.ToString() + " nProyecto = " + nProyecto.ToString() + " nEsquema = " + nEsquema.ToString)
                End Try


                If (EsquemaDataTable.Count > 0) Then

                    Dim fk_Precinto As Short
                    Dim id_Contenedor As Short

                    Dim dbmImaging As DBImagingDataBaseManager = Nothing
                    Dim dbmIntegration As DBIntegrationDataBaseManager = Nothing
                    Dim manager As FileProviderManager = Nothing

                    Try
                        dbmImaging = New DBImagingDataBaseManager(_ImagingConnectionString)
                        dbmIntegration = New DBIntegrationDataBaseManager(_IntegrationConnectionString)

                        Try
                            dbmImaging.Connection_Open(1)
                        Catch ex As Exception
                            MsgError = "Error con la cadena de conexion: " & _ImagingConnectionString.ToString() & " ex: " & ex.Message
                            Return False
                        End Try
                        Try
                            dbmIntegration.Connection_Open(1)
                        Catch ex As Exception
                            MsgError = "Error con la cadena de conexion: " & _IntegrationConnectionString.ToString() & " ex: " & ex.Message
                            Return False
                        End Try


                        Dim ServidorDataTable As DBImaging.SchemaCore.CTA_ServidorDataTable
                        Try
                            ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(EsquemaDataTable(0).fk_Entidad_Servidor, EsquemaDataTable(0).fk_Servidor)
                        Catch ex As Exception
                            Throw New Exception("dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor: fk_Entidad_Servidor = " + EsquemaDataTable(0).fk_Entidad_Servidor.ToString() + " fk_Servidor = " + EsquemaDataTable(0).fk_Servidor.ToString())
                        End Try

                        Dim CentroProcesamientoDataTable As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoDataTable
                        Try
                            CentroProcesamientoDataTable = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(nEntidadProcesamiento, nSedeProcesamiento_Cargue, nCentroProcesamiento_Cargue)
                        Catch ex As Exception
                            Throw New Exception("dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento: nEntidadProcesamiento = " + nEntidadProcesamiento.ToString() + " nSedeProcesamiento_Cargue = " + nSedeProcesamiento_Cargue.ToString() + " nCentroProcesamiento_Cargue = " + nCentroProcesamiento_Cargue.ToString)
                        End Try




                        If (CentroProcesamientoDataTable.Count > 0) Then
                            Dim nSedeProcesamiento As Short = CentroProcesamientoDataTable(0).fk_Sede_Asignada
                            Dim nCentroProcesamiento As Short = CentroProcesamientoDataTable(0).fk_Centro_Procesamiento_Asignado

                            Dim FechaProceso = DateTime.Now

                            Dim HoraCambioProcesoMatutino As Integer
                            Try
                                HoraCambioProcesoMatutino = dbmIntegration.SchemaProcess.PA_Get_Hora_Cambio_Proceso_Matutino.DBExecute(nEntidadCliente, nProyecto, nEsquema, "Hora_Cambio_Proceso_Matutino")
                            Catch ex As Exception
                                Throw New Exception("dbmIntegration.SchemaProcess.PA_Get_Hora_Cambio_Proceso_Matutino")
                            End Try


                            Dim HoraCambioProcesoVespertino = dbmIntegration.SchemaProcess.PA_Get_Hora_Cambio_Proceso_Vespertino.DBExecute(nEntidadCliente, nProyecto, nEsquema, "Hora_Cambio_Proceso_Vespertino")

                            ServidorDataTable(0).ConnectionString_Servidor = ServidorDataTable(0).ConnectionString_Servidor & _DataRemoting

                            manager = New FileProviderManager(ServidorDataTable(0).ToCTA_ServidorSimpleType(), CentroProcesamientoDataTable(0).ToCTA_Centro_ProcesamientoSimpleType(), dbmImaging, 1)
                            manager.Connect()

                            'FechaProceso = New DateTime(FechaProceso.Year, FechaProceso.Month, FechaProceso.Day)
                            Try
                                FechaProceso = dbmCore.SchemaProcess.PA_getSiguiente_Fecha_Habil.DBExecute(FechaProceso)
                            Catch ex As Exception
                                Throw New Exception("dbmCore.SchemaProcess.PA_getSiguiente_Fecha_Habil")
                            End Try



                            If FechaProceso.Hour < HoraCambioProcesoMatutino Then

                                CrearFechaProceso(dbmImaging, nEntidadProcesamiento, nEntidadCliente, nProyecto, FechaProceso, nUser)
                                CrearOT(dbmImaging, nEntidadProcesamiento, nEntidadCliente, nProyecto, FechaProceso, nUser, nSedeProcesamiento, nCentroProcesamiento, CShort(1))

                            ElseIf FechaProceso.Hour >= HoraCambioProcesoMatutino And FechaProceso.Hour <= HoraCambioProcesoVespertino Then

                                CrearFechaProceso(dbmImaging, nEntidadProcesamiento, nEntidadCliente, nProyecto, FechaProceso, nUser)
                                CrearOT(dbmImaging, nEntidadProcesamiento, nEntidadCliente, nProyecto, FechaProceso, nUser, nSedeProcesamiento, nCentroProcesamiento, CShort(2))

                            ElseIf FechaProceso.Hour > HoraCambioProcesoVespertino And FechaProceso.Hour < 24 Then

                                FechaProceso = dbmCore.SchemaProcess.PA_getSiguiente_Fecha_Habil.DBExecute(FechaProceso.AddDays(1))

                                CrearFechaProceso(dbmImaging, nEntidadProcesamiento, nEntidadCliente, nProyecto, FechaProceso, nUser)
                                CrearOT(dbmImaging, nEntidadProcesamiento, nEntidadCliente, nProyecto, FechaProceso, nUser, nSedeProcesamiento, nCentroProcesamiento, CShort(1))
                            End If



                            'Crear Destape
                            Dim DestapeDataTable = dbmImaging.SchemaProcess.PA_Crear_Precinto_Contenedor_Sin_Campo.DBExecute(Me.NewOT, nContenedor, nEsquema)


                            If (CShort(DestapeDataTable.Rows(0)("fk_Precinto").ToString()) = 0) Then
                                If (CShort(DestapeDataTable.Rows(0)("id_Contenedor").ToString()) = 0) Then
                                    'MsgError = "El precinto y contenedor ya fueron destapados para la OT y Fecha de Proceso"
                                    Return False
                                End If
                            End If

                            fk_Precinto = CShort(DestapeDataTable.Rows(0)("fk_Precinto").ToString())
                            id_Contenedor = CShort(DestapeDataTable.Rows(0)("id_Contenedor").ToString())


                            ' Crear el cargue
                            CargueType.fk_Entidad = nEntidadCliente
                            CargueType.fk_Proyecto = nProyecto
                            CargueType.fk_Estado = EstadoEnum.Creado
                            CargueType.fk_Entidad_Procesamiento = nEntidadProcesamiento
                            CargueType.fk_Sede_Procesamiento_Cargue = nSedeProcesamiento_Cargue
                            CargueType.fk_Centro_Procesamiento_Cargue = nCentroProcesamiento_Cargue
                            CargueType.fk_Entidad_Servidor = ServidorDataTable(0).fk_Entidad
                            CargueType.fk_Servidor = ServidorDataTable(0).id_Servidor
                            CargueType.fk_OT = Me.NewOT
                            CargueType.Fecha_Proceso = FechaProceso
                            CargueType.fk_Usuario_Log = nUser
                            CargueType.Observaciones = nObservaciones

                            IdCargue = dbmImaging.SchemaProcess.PA_Guardar_TBL_Cargue.DBExecute(
                                CargueType.fk_Entidad,
                                CargueType.fk_Proyecto,
                                CargueType.fk_Estado,
                                CargueType.fk_Entidad_Procesamiento,
                                CargueType.fk_Sede_Procesamiento_Cargue,
                                CargueType.fk_Centro_Procesamiento_Cargue,
                                CargueType.fk_Entidad_Servidor,
                                CargueType.fk_Servidor,
                                CargueType.fk_OT,
                                CargueType.Fecha_Proceso,
                                CargueType.Observaciones,
                                CargueType.fk_Usuario_Log)

                            ' Crear el paquete
                            Dim PaqueteType = New TBL_Cargue_PaqueteType()

                            PaqueteType.fk_Cargue = IdCargue
                            PaqueteType.id_Cargue_Paquete = CShort(1)
                            PaqueteType.fk_Estado = nEstado
                            PaqueteType.fk_Usuario_Log = nUser
                            PaqueteType.Fecha_Proceso = SlygNullable.SysDate
                            PaqueteType.Path_Cargue_Paquete = nPaqueteName
                            PaqueteType.Data_Path = nPaqueteName & nPaqueteName
                            PaqueteType.fk_Sede_Procesamiento_Asignada = nSedeProcesamiento
                            PaqueteType.fk_Centro_Procesamiento_Asignado = nCentroProcesamiento

                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(PaqueteType)

                            ' Crear los items
                            For i As Integer = 0 To Imagenes.Count - 1
                                Using Archivo As New FileStream(Imagenes(i), FileMode.Open, FileAccess.Read)
                                    Dim Data(CInt(Archivo.Length - 1)) As Byte

                                    Archivo.Read(Data, 0, Data.Length)

                                    ' Item Imaging
                                    Dim CargueItemType = New TBL_Cargue_ItemType()

                                    CargueItemType.fk_Cargue = IdCargue
                                    CargueItemType.fk_Cargue_Paquete = PaqueteType.id_Cargue_Paquete
                                    CargueItemType.id_Cargue_Item = i + 1
                                    CargueItemType.Folios_Cargue_Item = CShort(1)
                                    CargueItemType.Tamaño_Cargue_Item = Data.Length
                                    CargueItemType.Path_Cargue_Item = Path.GetFileName(Imagenes(i))
                                    CargueItemType.Key_Cargue_Item = Path.GetFileNameWithoutExtension(CargueItemType.Path_Cargue_Item)
                                    CargueItemType.fk_Estado = nEstado
                                    CargueItemType.fk_Usuario_Log = nUser
                                    CargueItemType.Bloqueado = False

                                    Try
                                        dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert(CargueItemType)
                                    Catch ex As Exception
                                        Throw New Exception("dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert")
                                    End Try


                                    manager.CreateItem(CargueItemType.fk_Cargue, CargueItemType.fk_Cargue_Paquete, CargueItemType.id_Cargue_Item)

                                    Dim DataThumbnailMemoryStream As MemoryStream = New MemoryStream()
                                    ImageManager.GetThumbnail(New Bitmap(Image.FromStream(New MemoryStream(Data))), MaxThumbnailWidth, MaxThumbnailHeight).Save(DataThumbnailMemoryStream, Imaging.ImageFormat.Jpeg)

                                    manager.CreateFolio(CargueItemType.fk_Cargue, CargueItemType.fk_Cargue_Paquete, CargueItemType.id_Cargue_Item, CShort(1), Data, DataThumbnailMemoryStream.GetBuffer(), True)

                                    ' Insertar Folio
                                    Dim FolioType = New DBImaging.SchemaProcess.TBL_Cargue_FolioType()
                                    FolioType.fk_Cargue = CargueItemType.fk_Cargue
                                    FolioType.fk_Cargue_Paquete = CargueItemType.fk_Cargue_Paquete
                                    FolioType.fk_Cargue_Item = CargueItemType.id_Cargue_Item
                                    FolioType.id_Folio = CShort(1)
                                    FolioType.Indexado = False
                                    dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBInsert(FolioType)

                                    Archivo.Close()
                                    Archivo.Dispose()
                                End Using
                            Next

                            'Actualizar Contenedor
                            ContenedorType.fk_Cargue = IdCargue
                            ContenedorType.fk_Paquete = CShort(1)
                            dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(ContenedorType, Me.NewOT, fk_Precinto, id_Contenedor)

                            ' Borrar los archivos                        
                            Directory.Delete(_OutputFolder + nFolder, True)

                            nCargueID = IdCargue
                            nFolios = CShort(Imagenes.Count)


                            '---------------------------------------------------------------------------
                            ' Actualizar Dashboard
                            '---------------------------------------------------------------------------

                            Dim DashboardPaquetesType = New TBL_Dashboard_PaquetesType()
                            DashboardPaquetesType.fk_Cargue = IdCargue 'CargueType.id_Cargue
                            DashboardPaquetesType.fk_Cargue_Paquete = PaqueteType.id_Cargue_Paquete
                            DashboardPaquetesType.fk_Entidad_Procesamiento = CargueType.fk_Entidad_Procesamiento
                            DashboardPaquetesType.fk_Sede_Procesamiento = PaqueteType.fk_Sede_Procesamiento_Asignada
                            DashboardPaquetesType.fk_Centro_Procesamiento = PaqueteType.fk_Centro_Procesamiento_Asignado
                            DashboardPaquetesType.fk_Entidad = CargueType.fk_Entidad
                            DashboardPaquetesType.fk_Proyecto = CargueType.fk_Proyecto
                            DashboardPaquetesType.fk_OT = CargueType.fk_OT
                            dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBInsert(DashboardPaquetesType)
                            '---------------------------------------------------------------------------

                            ' Actualizar el estado del cargue
                            CargueType = New DBImaging.SchemaProcess.TBL_CargueType()
                            CargueType.fk_Estado = nEstado
                            dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, IdCargue)


                            'Notificacion Cargue
                            Dim NotificacionDataTable = dbmCore.SchemaConfig.TBL_Notificacion.DBFindByNombre_Notificacion("Cargue Imagenes")
                            If NotificacionDataTable.Count > 0 Then

                                Dim EsquemaNotificacionDataTable = dbmCore.SchemaConfig.TBL_Esquema_Notificacion.DBFindByfk_Entidadfk_Proyectofk_EsquemaModulofk_Notificacion(nEntidadCliente, nProyecto, nEsquema, CShort(2), NotificacionDataTable(0).id_Notificacion)

                                If EsquemaNotificacionDataTable.Count > 0 Then

                                    Dim NotificacionListasDataTable = dbmCore.SchemaConfig.TBL_Notificacion_Lista.DBFindByfk_NotificacionNombre_Lista(NotificacionDataTable(0).id_Notificacion, "Cargue de imagenes Santander")

                                    If NotificacionListasDataTable.Count > 0 Then

                                        Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(NotificacionDataTable(0).id_Notificacion, NotificacionListasDataTable(0).id_Notificacion_Lista)
                                        If CorreoDatatable.Count > 0 Then
                                            Dim Message As String = ""
                                            Dim MailTo As String = ""
                                            Dim MailCC As String = ""
                                            Dim MailCCO As String = ""
                                            Dim Subject As String = ""
                                            Dim nAttachName As String = ""
                                            Dim nAttach As Byte() = Nothing
                                            Dim CuerpoCorreo As String = ""
                                            Dim Novedades As String = ""

                                            Dim EntidadProyectoEsquemaDataTable = dbmCore.SchemaConfig.CTA_Entidad_Proyecto_Esquema.DBFindByid_Entidadid_Proyectoid_Esquema(nEntidadCliente, nProyecto, nEsquema)

                                            CuerpoCorreo = CorreoDatatable(0).CUERPO.Replace("@Proyecto", EntidadProyectoEsquemaDataTable(0).Nombre_Entidad & "-" & EntidadProyectoEsquemaDataTable(0).Nombre_Proyecto)
                                            CuerpoCorreo = CuerpoCorreo.Replace("@Cargue", IdCargue.ToString())
                                            CuerpoCorreo = CuerpoCorreo.Replace("@FechaProceso", Me.FechaProcesoInt.ToString())
                                            CuerpoCorreo = CuerpoCorreo.Replace("@OT", Me.NewOT.ToString())

                                            Message = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA
                                            MailTo = CorreoDatatable(0).CORREOS
                                            Subject = CorreoDatatable(0).ASUNTO


                                            Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing

                                            Try
                                                DBMTools = New DBTools.DBToolsDataBaseManager(Me._ToolsConnectionString)
                                                DBMTools.Connection_Open()

                                                If _DataRemoting <> "" Then
                                                    DBMTools.SchemaMail.PA_Basic_TBL_Queue_insert_Attach.DBExecute(nEntidadCliente, 1, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)
                                                Else
                                                    DBMTools.InsertMail(1, 137, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)
                                                End If


                                            Catch ex As Exception
                                                MsgError = ex.Message.ToString()
                                            Finally
                                                DBMTools.Connection_Close()
                                            End Try

                                        End If
                                    End If
                                End If
                            End If
                            'Fin


                            MsgError = ""
                            Return True
                        Else
                            MsgError = "No se encontró una Sede y Centro de Procesamiento asignados para el cargue"
                            Return False
                        End If
                    Catch ex As Exception

                        'Throw New Exception(dbmImaging.DataBase.LastQuery + dbmIntegration.DataBase.LastQuery + dbmCore.DataBase.LastQuery)

                        If ((fk_Precinto <> 0) And (id_Contenedor <> 0)) Then
                            dbmImaging.SchemaProcess.TBL_Contenedor.DBDelete(Me.NewOT, fk_Precinto, id_Contenedor)
                            dbmImaging.SchemaProcess.TBL_Precinto.DBDelete(Me.NewOT, fk_Precinto)
                        End If

                        'Elimina el cargue realizado
                        If Not IsNothing(CargueType.id_Cargue) Then
                            dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(CargueType.id_Cargue, Nothing)
                            dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBDelete(CargueType.id_Cargue, Nothing, Nothing, Nothing)
                            dbmImaging.SchemaProcess.TBL_Cargue_Item.DBDelete(CargueType.id_Cargue, Nothing, Nothing)
                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBDelete(CargueType.id_Cargue, Nothing)
                            dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(CargueType.id_Cargue)
                            manager.DeleteCargue(CargueType.id_Cargue)
                        End If

                        Throw
                    Finally
                        Try
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                            If (dbmIntegration IsNot Nothing) Then dbmIntegration.Connection_Close()
                            If (manager IsNot Nothing) Then manager.Disconnect()
                        Catch

                        End Try

                    End Try
                Else
                    MsgError = "No se encontro el esquema con código: fk_Entidad=" & nEntidadCliente & ", fk_Proyecto=" & nProyecto & ", id_Esquema=" & nEsquema
                    Return False
                End If
            Catch ex As Exception
                MsgError = ex.Message
                Return False
            Finally
                Try
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                Catch

                End Try
            End Try
        End Function

        Public Function CargarDDO(ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nFolder As String, ByVal nPaqueteName As String, ByVal nObservaciones As String, ByVal nEntidadProcesamiento As Short, ByVal nSedeProcesamiento_Cargue As Short, ByVal nCentroProcesamiento_Cargue As Short, ByVal nIdLog As Integer, ByVal nUser As Integer, ByVal nEstado As Short, ByVal nContenedor As String, ByRef nCargueID As Integer, ByRef nFolios As Short, nCampos As String, ByVal ExtImgSalida As String, ByRef MsgError As String) As Boolean
            Dim dbmCore As DBCoreDataBaseManager = Nothing
            Dim IdCargue As Integer
            Try
                dbmCore = New DBCoreDataBaseManager(_CoreConnectionString)
                Try
                    dbmCore.Connection_Open(1)
                Catch ex As Exception
                    LogError("01- Error con la cadena de conexion: " & ex.Message)
                    'Throw New Exception("Error con la cadena de conexion: " + ex.Message)
                    Return False
                End Try
                ' Validar si existe el directorio
                If (Not Directory.Exists(_OutputFolder + nFolder)) Then
                    LogError("02- No se encontró el directorio: " & _OutputFolder & nFolder)
                    Return False
                End If

                'Validar si existen imagenes
                Dim Imagenes = New List(Of String)()
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.jpg")
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.gif")
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.bmp")
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.png")
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.tif")
                GetFiles(Imagenes, _OutputFolder + nFolder, "*.tiff")

                If (Imagenes.Count = 0) Then
                    LogError("03- No se encontraron imagenes para cargar en el directorio: " + _OutputFolder + nFolder)
                    Return False
                End If

                Dim EsquemaDataTable As DBCore.SchemaConfig.TBL_EsquemaDataTable = Nothing
                Try
                    EsquemaDataTable = dbmCore.SchemaConfig.TBL_Esquema.DBGet(nEntidadCliente, nProyecto, nEsquema)
                Catch ex As Exception
                    LogError("04- dbmCore.SchemaConfig.TBL_Esquema.DBGet: nEntidadCliente = " + nEntidadCliente.ToString() + " nProyecto = " + nProyecto.ToString() + " nEsquema = " + nEsquema.ToString)
                    Return False
                End Try
                If EsquemaDataTable.Count > 0 Then

                    Dim fk_Precinto As Short
                    Dim id_Contenedor As Short
                    Dim dbmImaging As DBImagingDataBaseManager = Nothing
                    Dim manager As FileProviderManager = Nothing

                    Try
                        dbmImaging = New DBImagingDataBaseManager(_ImagingConnectionString)
                        Try
                            dbmImaging.Connection_Open(1)
                        Catch ex As Exception
                            LogError("05- Error con la cadena de conexion: " & _ImagingConnectionString.ToString() & " ex: " & ex.Message)
                            Return False
                        End Try

                        Dim ServidorDataTable As DBImaging.SchemaCore.CTA_ServidorDataTable = Nothing
                        Try
                            ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(EsquemaDataTable(0).fk_Entidad_Servidor, EsquemaDataTable(0).fk_Servidor)
                        Catch ex As Exception
                            LogError("06- dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor: fk_Entidad_Servidor = " + EsquemaDataTable(0).fk_Entidad_Servidor.ToString() + " fk_Servidor = " + EsquemaDataTable(0).fk_Servidor.ToString())
                        End Try

                        Dim CentroProcesamientoDataTable As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoDataTable = Nothing
                        Try
                            CentroProcesamientoDataTable = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(nEntidadProcesamiento, nSedeProcesamiento_Cargue, nCentroProcesamiento_Cargue)
                        Catch ex As Exception
                            LogError("07- dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento: nEntidadProcesamiento = " + nEntidadProcesamiento.ToString() + " nSedeProcesamiento_Cargue = " + nSedeProcesamiento_Cargue.ToString() + " nCentroProcesamiento_Cargue = " + nCentroProcesamiento_Cargue.ToString)
                        End Try

                        If (CentroProcesamientoDataTable.Count > 0) Then
                            Dim nSedeProcesamiento As Short = CentroProcesamientoDataTable(0).fk_Sede_Asignada
                            Dim nCentroProcesamiento As Short = CentroProcesamientoDataTable(0).fk_Centro_Procesamiento_Asignado

                            Dim FechaProceso = DateTime.Now

                            ServidorDataTable(0).ConnectionString_Servidor = ServidorDataTable(0).ConnectionString_Servidor & _DataRemoting

                            manager = New FileProviderManager(ServidorDataTable(0).ToCTA_ServidorSimpleType(), CentroProcesamientoDataTable(0).ToCTA_Centro_ProcesamientoSimpleType(), dbmImaging, 1)
                            manager.Connect()

                            CrearFechaProceso(dbmImaging, nEntidadProcesamiento, nEntidadCliente, nProyecto, FechaProceso, nUser)
                            CrearOT(dbmImaging, nEntidadProcesamiento, nEntidadCliente, nProyecto, FechaProceso, nUser, nSedeProcesamiento, nCentroProcesamiento, CShort(1))

                            'Crear Destape
                            Dim DestapeDataTable = dbmImaging.SchemaProcess.PA_Crear_Precinto_Contenedor.DBExecute(nEntidadCliente, nProyecto, nEsquema, Me.NewOT, nContenedor, nCampos, "|", ";", nUser)

                            If (CShort(DestapeDataTable.Rows(0)("fk_Precinto").ToString()) = 0) Then
                                If (CShort(DestapeDataTable.Rows(0)("fk_Contenedor").ToString()) = 0) Then
                                    LogError("08- El precinto y contenedor ya fueron destapados para la OT " & Me.NewOT & " y Fecha de Proceso con contenedor " & nContenedor)
                                    'NO SE PUEDEN BORRAR PORQUE AMBOS ESTAN EN CERO.
                                    Return False
                                End If
                            End If

                            fk_Precinto = CShort(DestapeDataTable.Rows(0)("fk_Precinto").ToString())
                            id_Contenedor = CShort(DestapeDataTable.Rows(0)("fk_Contenedor").ToString())

                            ' Crear el cargue
                            CargueType.fk_Entidad = nEntidadCliente
                            CargueType.fk_Proyecto = nProyecto
                            CargueType.fk_Estado = EstadoEnum.Creado
                            CargueType.fk_Entidad_Procesamiento = nEntidadProcesamiento
                            CargueType.fk_Sede_Procesamiento_Cargue = nSedeProcesamiento_Cargue
                            CargueType.fk_Centro_Procesamiento_Cargue = nCentroProcesamiento_Cargue
                            CargueType.fk_Entidad_Servidor = ServidorDataTable(0).fk_Entidad
                            CargueType.fk_Servidor = ServidorDataTable(0).id_Servidor
                            CargueType.fk_OT = Me.NewOT
                            CargueType.Fecha_Proceso = FechaProceso
                            CargueType.fk_Usuario_Log = nUser
                            CargueType.Observaciones = nObservaciones

                            IdCargue = dbmImaging.SchemaProcess.PA_Guardar_TBL_Cargue.DBExecute(
                                CargueType.fk_Entidad,
                                CargueType.fk_Proyecto,
                                CargueType.fk_Estado,
                                CargueType.fk_Entidad_Procesamiento,
                                CargueType.fk_Sede_Procesamiento_Cargue,
                                CargueType.fk_Centro_Procesamiento_Cargue,
                                CargueType.fk_Entidad_Servidor,
                                CargueType.fk_Servidor,
                                CargueType.fk_OT,
                                CargueType.Fecha_Proceso,
                                CargueType.Observaciones,
                                CargueType.fk_Usuario_Log)

                            ' Crear el paquete
                            Dim PaqueteType = New TBL_Cargue_PaqueteType()

                            PaqueteType.fk_Cargue = IdCargue
                            PaqueteType.id_Cargue_Paquete = CShort(1)
                            PaqueteType.fk_Estado = nEstado
                            PaqueteType.fk_Usuario_Log = nUser
                            PaqueteType.Fecha_Proceso = SlygNullable.SysDate
                            PaqueteType.Path_Cargue_Paquete = nPaqueteName
                            PaqueteType.Data_Path = nPaqueteName & nPaqueteName
                            PaqueteType.fk_Sede_Procesamiento_Asignada = nSedeProcesamiento
                            PaqueteType.fk_Centro_Procesamiento_Asignado = nCentroProcesamiento

                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBInsert(PaqueteType)

                            ' Crear los items
                            For i As Integer = 0 To Imagenes.Count - 1
                                Using Archivo As New FileStream(Imagenes(i), FileMode.Open, FileAccess.Read)
                                    Dim Data(CInt(Archivo.Length - 1)) As Byte
                                    Archivo.Read(Data, 0, Data.Length)
                                    ' Item Imaging
                                    Dim CargueItemType = New TBL_Cargue_ItemType()
                                    CargueItemType.fk_Cargue = IdCargue
                                    CargueItemType.fk_Cargue_Paquete = PaqueteType.id_Cargue_Paquete
                                    CargueItemType.id_Cargue_Item = i + 1
                                    CargueItemType.Folios_Cargue_Item = CShort(1)
                                    CargueItemType.Tamaño_Cargue_Item = Data.Length
                                    CargueItemType.Path_Cargue_Item = Path.GetFileName(Imagenes(i))
                                    CargueItemType.Key_Cargue_Item = Path.GetFileNameWithoutExtension(CargueItemType.Path_Cargue_Item)
                                    CargueItemType.fk_Estado = nEstado
                                    CargueItemType.fk_Usuario_Log = nUser
                                    CargueItemType.Bloqueado = False
                                    Try
                                        dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert(CargueItemType)
                                    Catch ex As Exception
                                        LogError("09- dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert")
                                        'Throw New Exception("dbmImaging.SchemaProcess.TBL_Cargue_Item.DBInsert")
                                        Throw
                                    End Try
                                    manager.CreateItem(CargueItemType.fk_Cargue, CargueItemType.fk_Cargue_Paquete, CargueItemType.id_Cargue_Item)
                                    Dim DataThumbnailMemoryStream As New MemoryStream()
                                    ImageManager.GetThumbnail(New Bitmap(Image.FromStream(New MemoryStream(Data))), MaxThumbnailWidth, MaxThumbnailHeight).Save(DataThumbnailMemoryStream, Imaging.ImageFormat.Jpeg)
                                    manager.CreateFolio(CargueItemType.fk_Cargue, CargueItemType.fk_Cargue_Paquete, CargueItemType.id_Cargue_Item, CShort(1), Data, DataThumbnailMemoryStream.GetBuffer(), True)
                                    ' Insertar Folio
                                    Dim FolioType = New DBImaging.SchemaProcess.TBL_Cargue_FolioType()
                                    FolioType.fk_Cargue = CargueItemType.fk_Cargue
                                    FolioType.fk_Cargue_Paquete = CargueItemType.fk_Cargue_Paquete
                                    FolioType.fk_Cargue_Item = CargueItemType.id_Cargue_Item
                                    FolioType.id_Folio = CShort(1)
                                    FolioType.Indexado = False
                                    dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBInsert(FolioType)
                                    Archivo.Close()
                                    Archivo.Dispose()
                                End Using
                            Next
                            'Actualizar Contenedor
                            ContenedorType.fk_Cargue = IdCargue
                            ContenedorType.fk_Paquete = CShort(1)
                            ContenedorType.fk_Estado = 10
                            dbmImaging.SchemaProcess.TBL_Contenedor.DBUpdate(ContenedorType, Me.NewOT, fk_Precinto, id_Contenedor)

                            ' Borrar los archivos                        
                            Directory.Delete(_OutputFolder + nFolder, True)

                            nCargueID = IdCargue
                            nFolios = CShort(Imagenes.Count)

                            '---------------------------------------------------------------------------
                            ' Actualizar Dashboard
                            '---------------------------------------------------------------------------
                            Dim DashboardPaquetesType = New TBL_Dashboard_PaquetesType()
                            DashboardPaquetesType.fk_Cargue = IdCargue 'CargueType.id_Cargue
                            DashboardPaquetesType.fk_Cargue_Paquete = PaqueteType.id_Cargue_Paquete
                            DashboardPaquetesType.fk_Entidad_Procesamiento = CargueType.fk_Entidad_Procesamiento
                            DashboardPaquetesType.fk_Sede_Procesamiento = PaqueteType.fk_Sede_Procesamiento_Asignada
                            DashboardPaquetesType.fk_Centro_Procesamiento = PaqueteType.fk_Centro_Procesamiento_Asignado
                            DashboardPaquetesType.fk_Entidad = CargueType.fk_Entidad
                            DashboardPaquetesType.fk_Proyecto = CargueType.fk_Proyecto
                            DashboardPaquetesType.fk_OT = CargueType.fk_OT
                            dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBInsert(DashboardPaquetesType)
                            '---------------------------------------------------------------------------
                            'MARCAR CARGUE OK E INDEXACION CREAR DOS CAMPOS.
                            ' Actualizar el estado del cargue
                            CargueLote = New DBImaging.SchemaProcess.TBL_Lote_DigitalizacionType() With {
                                .Cargado = CBool(1)}
                            dbmImaging.SchemaProcess.TBL_Lote_Digitalizacion.DBUpdate(CargueLote, nPaqueteName)

                            If EsquemaDataTable(0).Indexacion_Automatica Then
                                'Buscamos la configuracion de indexacion
                                Dim DtConfIndex = dbmCore.SchemaConfig.PA_Esquema_Indexacion_Get.DBExecute(nEntidadCliente, nProyecto, nEsquema)
                                If DtConfIndex.Rows.Count > 0 Then
                                    CargarIndexacionDDO(DtConfIndex, nEntidadCliente, nProyecto, IdCargue, PaqueteType.id_Cargue_Paquete, nEntidadProcesamiento, FechaProceso, nUser, nSedeProcesamiento, nCentroProcesamiento, nEsquema, Me.NewOT, ExtImgSalida, EsquemaDataTable, nEstado, nPaqueteName, EsquemaDataTable(0).usa_NombreArchivo_Asociado_Tipologia, EsquemaDataTable(0).usa_Cargue_xDocumento)
                                Else
                                    'VALIDAR INDEXACION POR NOMBRE DE ARCHIVO
                                    If EsquemaDataTable(0).usa_NombreArchivo_Asociado_Tipologia Then
                                        CargarIndexacionDDO(DtConfIndex, nEntidadCliente, nProyecto, IdCargue, PaqueteType.id_Cargue_Paquete, nEntidadProcesamiento, FechaProceso, nUser, nSedeProcesamiento, nCentroProcesamiento, nEsquema, Me.NewOT, ExtImgSalida, EsquemaDataTable, nEstado, nPaqueteName, EsquemaDataTable(0).usa_NombreArchivo_Asociado_Tipologia, EsquemaDataTable(0).usa_Cargue_xDocumento)
                                    Else
                                        ' Actualizar el estado del cargue
                                        CargueType = New DBImaging.SchemaProcess.TBL_CargueType() With {
                                                .fk_Estado = 29} 'Indexación Automatica
                                        dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, IdCargue)

                                        ' Actualizar el estado del cargue
                                        PaqueteType = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                                        PaqueteType.fk_Estado = 29
                                        dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(PaqueteType, IdCargue, 1)
                                    End If
                                End If
                            Else
                                ' Actualizar el estado del cargue
                                CargueType = New DBImaging.SchemaProcess.TBL_CargueType()
                                CargueType.fk_Estado = nEstado 'Indexación Manual
                                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, IdCargue)
                            End If

                            'Notificación cargue exitoso
                            If EsquemaDataTable(0).UsaNotificacionCargue Then
                                Dim NotificacionListasDataTable = dbmCore.SchemaConfig.TBL_Notificacion_Lista.DBFindByfk_Notificacion(EsquemaDataTable(0).fk_Notificacion)
                                If NotificacionListasDataTable.Count > 0 Then
                                    Dim CorreoDatatable = dbmCore.SchemaConfig.PA_Busqueda_Parametros_Correo.DBExecute(EsquemaDataTable(0).fk_Notificacion, NotificacionListasDataTable(0).id_Notificacion_Lista)
                                    If CorreoDatatable.Count > 0 Then
                                        Dim Message As String = ""
                                        Dim MailTo As String = ""
                                        Dim MailCC As String = ""
                                        Dim MailCCO As String = ""
                                        Dim Subject As String = ""
                                        Dim nAttachName As String = ""
                                        Dim nAttach As Byte() = Nothing
                                        Dim CuerpoCorreo As String = ""
                                        Dim Novedades As String = ""

                                        Dim EntidadProyectoEsquemaDataTable = dbmCore.SchemaConfig.CTA_Entidad_Proyecto_Esquema.DBFindByid_Entidadid_Proyectoid_Esquema(nEntidadCliente, nProyecto, nEsquema)

                                        CuerpoCorreo = CorreoDatatable(0).CUERPO.Replace("@Proyecto", EntidadProyectoEsquemaDataTable(0).Nombre_Entidad & "-" & EntidadProyectoEsquemaDataTable(0).Nombre_Proyecto)
                                        CuerpoCorreo = CuerpoCorreo.Replace("@Esquema", EsquemaDataTable(0).Nombre_Esquema.ToString())
                                        CuerpoCorreo = CuerpoCorreo.Replace("@Cargue", IdCargue.ToString())
                                        CuerpoCorreo = CuerpoCorreo.Replace("@FechaProceso", Me.FechaProcesoInt.ToString())
                                        CuerpoCorreo = CuerpoCorreo.Replace("@OT", Me.NewOT.ToString())

                                        Message = CorreoDatatable(0).SALUDO & CuerpoCorreo & CorreoDatatable(0).FIRMA
                                        MailTo = CorreoDatatable(0).CORREOS
                                        Subject = CorreoDatatable(0).ASUNTO

                                        Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing

                                        Try
                                            DBMTools = New DBTools.DBToolsDataBaseManager(Me._ToolsConnectionString)
                                            DBMTools.Connection_Open()

                                            DBMTools.SchemaMail.PA_Basic_TBL_Queue_insert_Attach.DBExecute(nEntidadCliente, 1, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)

                                        Catch ex As Exception
                                            MsgError = ex.Message.ToString()
                                            LogError("11-Error enviando la notificación de cargue exitoso")
                                        Finally
                                            DBMTools.Connection_Close()
                                        End Try
                                    End If
                                End If
                            End If

                            Return True
                        Else
                            LogError("10- No se encontró una Sede y Centro de Procesamiento asignados para el cargue")
                            Return False
                        End If
                    Catch ex As Exception
                        LogError("10- " & ex.Message)

                        If ((fk_Precinto <> 0) And (id_Contenedor <> 0)) Then
                            dbmImaging.SchemaProcess.TBL_Contenedor.DBDelete(Me.NewOT, fk_Precinto, id_Contenedor)
                            dbmImaging.SchemaProcess.TBL_Precinto.DBDelete(Me.NewOT, fk_Precinto)
                        End If

                        'Elimina el cargue realizado
                        If Not IsNothing(CargueType.id_Cargue) Then
                            dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(CargueType.id_Cargue, Nothing)
                            dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBDelete(CargueType.id_Cargue, Nothing, Nothing, Nothing)
                            dbmImaging.SchemaProcess.TBL_Cargue_Item.DBDelete(CargueType.id_Cargue, Nothing, Nothing)
                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBDelete(CargueType.id_Cargue, Nothing)
                            dbmImaging.SchemaProcess.TBL_Cargue.DBDelete(CargueType.id_Cargue)
                            manager.DeleteCargue(CargueType.id_Cargue)
                        End If

                        Throw
                    Finally
                        Try
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                            If (manager IsNot Nothing) Then manager.Disconnect()
                        Catch : End Try
                    End Try
                Else
                    MsgError = "No se encontro el esquema con código: fk_Entidad=" & nEntidadCliente & ", fk_Proyecto=" & nProyecto & ", id_Esquema=" & nEsquema
                    Return False
                End If
            Catch ex As Exception
                LogError("11- " & ex.Message)
                Return False
            Finally
                Try
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                Catch : End Try
            End Try
        End Function

        Public Sub CargarIndexacionDDO(DtConfIndex As DataTable, nEntidadCliente As Short, nProyecto As Short, fk_Cargue As Integer, fk_Cargue_Paquete As Short, nEntidadProcesamiento As Short, FechaProceso As Date, nUser As Integer, nSedeProcesamiento As Short, nCentroProcesamiento As Short, nEsquema As Short, fk_Ot As Integer, ExtImgSalida As String, nEsquemaDataTable As DBCore.SchemaConfig.TBL_EsquemaDataTable, nEstado As Short, nPaqueteName As String, AsociacionTipologia As Boolean, CarguexDocumento As Boolean)

            Dim manager As FileProviderManager = Nothing
            Dim dbmImaging = New DBImagingDataBaseManager(_ImagingConnectionString)
            Try
                dbmImaging.Connection_Open(1)

                Dim ServidorDataTable As DBImaging.SchemaCore.CTA_ServidorDataTable

                ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(nEsquemaDataTable(0).fk_Entidad_Servidor, nEsquemaDataTable(0).fk_Servidor)

                Dim CentroPro As New DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType
                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(nEntidadCliente, ServidorDataTable(0).id_Servidor)(0).ToCTA_ServidorSimpleType

                manager = New FileProviderManager(servidor, CentroPro, dbmImaging, nUser)
                manager.Connect()

                Dim sqlAnexoItem As New StringBuilder()
                Dim sqlAnexoFolioItem As New StringBuilder()
                Dim sqlFolderItem As New StringBuilder()
                Dim sqlLlaveItem As New StringBuilder()
                Dim sqlDocumentoItem As New StringBuilder()
                Dim sqlFolioItem As New StringBuilder()
                Dim sql As String = ""

                Dim TFolder As Integer = 1
                Dim TContForder As Integer = 0
                Dim TAnexos As Integer = 0
                Dim TDcto As Integer = 0
                Dim TContDcto As Integer = 0
                Dim TAnexoItem As Integer = 0
                Dim TContAnexoI As Integer = 0

                'Buscamos la informacion a indexar.   CarguexDocumento
                Dim DtLoteIndexar = dbmImaging.SchemaProcess.PA_Cargue_a_Indexar_Get.DBExecute(fk_Cargue, fk_Cargue_Paquete, 0)
                If CarguexDocumento = True Then
                    Dim DtAsociacion As DataTable
                    Dim NoIdentificado As DataTable
                    Dim dbmCore As DBCoreDataBaseManager = Nothing
                    dbmCore = New DBCoreDataBaseManager(_CoreConnectionString)
                    Try
                        dbmCore.Connection_Open(1)
                        DtAsociacion = dbmCore.SchemaConfig.PA_Esquema_Asociacion_Tipologia_Get.DBExecute(nEntidadCliente, nProyecto, nEsquema)
                        NoIdentificado = dbmCore.SchemaConfig.PA_Esquema_DAsociacion_Tipologia_Get.DBExecute(nEntidadCliente, nProyecto, nEsquema)
                    Catch ex As Exception
                        Throw New Exception("Error con la cadena de conexion: " + ex.Message)
                    Finally
                        If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    End Try
                    Dim tFolio As Integer = 1
                    Dim Expediente As Integer = 0
                    Dim NomArchivo As String = ""
                    Dim ArchivoAux As String = ""
                    Dim TipoDoc As Integer = 0
                    For Each RowIndex As DataRow In DtLoteIndexar.Rows
                        'Buscamos el nombre de la imagen para verificar su tipologia con la asociacion.
                        NomArchivo = CStr(RowIndex.Item("Key_Cargue_Item")).Trim
                        If InStrRev(NomArchivo, "_") > 0 Then
                            NomArchivo = Mid(NomArchivo, 1, InStrRev(NomArchivo, "_") - 1)
                        End If
                        If ArchivoAux <> NomArchivo Then
                            Expediente += 1
                            Dim Buscar As String = Path.GetFileNameWithoutExtension(NomArchivo)
                            Dim largoEsperado As Integer = Buscar.Length + 4
                            Dim filtro As String = $"Key_Cargue_Item LIKE '{Buscar}%' AND LEN(Key_Cargue_Item) = {largoEsperado}"
                            Dim filas() As DataRow = DtLoteIndexar.Select(filtro)
                            Dim CountFolios As Integer = filas.Length
                            If AsociacionTipologia = True Then
                                For Each Regla As DataRow In DtAsociacion.Rows
                                    TipoDoc = CInt(ValidarNombreContraRegla(NomArchivo, Regla))
                                    If TipoDoc > 0 Then
                                        Exit For
                                    End If
                                Next
                            End If
                            If TipoDoc = 0 Then
                                TipoDoc = CInt(NoIdentificado.Rows(0).Item("id_Documento"))
                            End If
                            tFolio = 1
                            sqlFolderItem.AppendLine($"INSERT INTO #FolderItem (id_FolderItem, fk_Esquema, fk_Documento_Anexo) " & $"VALUES ({Expediente}, {nEsquema}, {0});")

                            sqlDocumentoItem.AppendLine($"INSERT INTO #DocumentoItem (fk_FolderItem, id_DocumentoItem, fk_Documento, Folios) " & $"VALUES ({Expediente}, {TFolder}, {TipoDoc}, {CountFolios});")

                            sqlFolioItem.AppendLine($"INSERT INTO #FolioItem (fk_FolderItem, fk_DocumentoItem, id_FolioItem, fk_Cargue_Item, fk_Item_Folio) " & $"VALUES ({Expediente}, {TFolder}, {tFolio}, {RowIndex.Item("id_Cargue_Item")}, {RowIndex.Item("id_Folio")});")

                            ArchivoAux = NomArchivo
                        Else
                            tFolio += 1
                            sqlFolioItem.AppendLine($"INSERT INTO #FolioItem (fk_FolderItem, fk_DocumentoItem, id_FolioItem, fk_Cargue_Item, fk_Item_Folio) " & $"VALUES ({Expediente}, {TFolder}, {tFolio}, {RowIndex.Item("id_Cargue_Item")}, {RowIndex.Item("id_Folio")});")
                        End If
                    Next
                Else
                    For i = 0 To DtLoteIndexar.Rows.Count - 1
                        Dim WDocuemntoItem As Integer = 1
                        sql = "INSERT INTO #FolderItem (id_FolderItem, fk_Esquema, fk_Documento_Anexo) VALUES (" & TFolder & ", " & nEsquema & ", " & nEsquema & ");"
                        sqlFolderItem.Append(sql)

                        sql = "INSERT INTO #DocumentoItem (fk_FolderItem, id_DocumentoItem, fk_Documento, Folios) VALUES (" & TFolder & ", " & TDcto & ", " & nEsquema & ", " & WDocuemntoItem & ");"
                        sqlDocumentoItem.Append(sql)

                        sql = "INSERT INTO #FolioItem (fk_FolderItem, fk_DocumentoItem, id_FolioItem, fk_Cargue_Item, fk_Item_Folio) VALUES (" & TFolder & ", " & TDcto & ", " & TAnexoItem & ", " & CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item")) & ", " & CInt(DtLoteIndexar.Rows(i).Item("id_Folio")) & ");"
                        sqlFolioItem.Append(sql)
                    Next
                End If
                Try
                    dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                    Dim GuardarNombreImagen As Boolean = True
                    Dim EnTransferencia As Boolean = False
                    Dim IsSofTrac As Boolean = False
                    'If _cargueRow.Observaciones = "SOFTRAC" Then
                    '    IsSofTrac = True
                    'End If
                    Dim ResultadoIndexar = dbmImaging.SchemaProcess.PA_Publicar_Indexacion.DBExecute(
                                fk_Ot,
                                fk_Cargue,
                                fk_Cargue_Paquete,
                                nEntidadProcesamiento,
                                nSedeProcesamiento,
                                nCentroProcesamiento,
                                nEntidadCliente,
                                nProyecto,
                                nUser,
                                nEntidadCliente,
                                ServidorDataTable(0).id_Servidor,
                                EnTransferencia, 'enTransferencia,
                                nEntidadProcesamiento,  'entidadServidorTransferencia,
                                CShort(ServidorDataTable(0).id_Servidor), '1, 'servidorTransferencia,
                                ExtImgSalida,
                                sqlAnexoItem.ToString(),
                                sqlAnexoFolioItem.ToString(),
                                sqlFolderItem.ToString(),
                                sqlLlaveItem.ToString(),
                                sqlDocumentoItem.ToString(),
                                sqlFolioItem.ToString(),
                                IsSofTrac,
                                GuardarNombreImagen)
                    dbmImaging.Transaction_Commit()

                    ''*******Pasar datos destape a captura*****
                    'If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campos_Captura_Destape Then
                    dbmImaging.SchemaProcess.PA_Insertar_Datos_Captura_Destape.DBExecute(fk_Cargue, fk_Cargue_Paquete)
                    'End If

                    '*******************************************************************************************
                    Dim DtImg As New DataTable
                    '[fk_Cargue], [fk_Cargue_Paquete], [id_Cargue_Item],[Path_Cargue_Item],[Key_Cargue_Item]

                    For i = 0 To DtLoteIndexar.Rows.Count - 1
                        Dim CargueItem As Integer = CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item"))
                        DtImg = dbmImaging.SchemaProcess.PA_Cargue_a_Indexar_Get.DBExecute(fk_Cargue, fk_Cargue_Paquete, CargueItem)

                        ' Actualizar estado folio cargue
                        dbmImaging.SchemaProcess.PA_Set_Cargue_Folio_Indexado.DBExecute(fk_Cargue, fk_Cargue_Paquete, CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item")), CInt(DtLoteIndexar.Rows(i).Item("id_Folio")))

                        Dim filaFiltro As DBImaging.SchemaProcess.TBL_Publicar_IndexacionRow
                        ' Si es un File
                        filaFiltro = ResultadoIndexar.FindByEs_Anexoid_Folder_Itemid_Documento_Itemid_Folio_Item(False, CShort(ResultadoIndexar(i).Item("id_folder_item")), CShort(ResultadoIndexar(i).Item("id_Documento_Item")), CShort(ResultadoIndexar(i).Item("id_Folio_item")))

                        manager.CreateItem(filaFiltro.fk_Expediente, filaFiltro.fk_Folder, filaFiltro.fk_File, filaFiltro.fk_Version, ExtImgSalida, Guid.NewGuid())

                        Dim DataImg() As Byte = CType(DtImg.Rows(0).Item("image_binary"), Byte())
                        Dim DataThumbnail() As Byte = CType(DtImg.Rows(0).Item("Thumbnail_Binary"), Byte())

                        manager.CreateFolio(filaFiltro.fk_Expediente, filaFiltro.fk_Folder, filaFiltro.fk_File, filaFiltro.fk_Version, filaFiltro.id_File_Record_Folio, DataImg, DataThumbnail, False)

                        ' Si es un Anexo
                        filaFiltro = ResultadoIndexar.FindByEs_Anexoid_Folder_Itemid_Documento_Itemid_Folio_Item(True, 0, CShort(ResultadoIndexar(i).Item("id_folder_item")), CShort(ResultadoIndexar(i).Item("id_Folio_item")))
                        If (filaFiltro IsNot Nothing) Then
                            manager.CreateItem(filaFiltro.fk_Anexo, ExtImgSalida)
                            manager.CreateFolio(filaFiltro.fk_Anexo, filaFiltro.id_File_Record_Folio, DataImg, DataThumbnail, False)
                        End If
                    Next

                    Try
                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                        Dim carguePaqueteDataTable = dbmImaging.SchemaProcess.PA_Get_Cargue_Folio_Indexado.DBExecute(fk_Cargue, fk_Cargue_Paquete, False)

                        If (carguePaqueteDataTable.Count = 0) Then
                            dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(fk_Cargue, fk_Cargue_Paquete)

                            Dim updatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                            updatePaquete.fk_Estado = DBCore.EstadoEnum.Indexado
                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(updatePaquete, fk_Cargue, fk_Cargue_Paquete)
                            dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBDelete(fk_Cargue, fk_Cargue_Paquete, Nothing, Nothing)

                            Dim cargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBFindByfk_Carguefk_Estado(fk_Cargue, DBCore.EstadoEnum.Indexacion)
                            If (cargueDataTable.Count = 0) Then
                                Dim updateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                                updateCargue.fk_Estado = DBCore.EstadoEnum.Indexado
                                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(updateCargue, fk_Cargue)
                            End If
                        Else
                            '---------------------------------------------------------------------------
                            ' Actualizar Dashboard
                            '---------------------------------------------------------------------------
                            dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Paquetes.DBExecute(fk_Cargue, fk_Cargue_Paquete)
                            '---------------------------------------------------------------------------
                        End If
                        For i = 0 To DtLoteIndexar.Rows.Count - 1
                            manager.DeleteItem(fk_Cargue, fk_Cargue_Paquete, CShort(DtLoteIndexar.Rows(i).Item("id_Cargue_item")))
                        Next

                        dbmImaging.Transaction_Commit()

                        CargueLote = New DBImaging.SchemaProcess.TBL_Lote_DigitalizacionType() With {
                            .Indexado = CBool(1)} 'Indexación Automatica
                        dbmImaging.SchemaProcess.TBL_Lote_Digitalizacion.DBUpdate(CargueLote, nPaqueteName)


                    Catch ex As Exception
                        LogError("12- " & ex.Message)
                        '' Actualizar el estado del cargue
                        CargueType = New DBImaging.SchemaProcess.TBL_CargueType()
                        CargueType.fk_Estado = nEstado
                        dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, fk_Cargue)
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    End Try
                Catch ex As Exception
                    LogError("13- " & ex.Message)
                    '' Actualizar el estado del cargue
                    CargueType = New DBImaging.SchemaProcess.TBL_CargueType()
                    CargueType.fk_Estado = nEstado
                    dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, fk_Cargue)

                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    If (manager IsNot Nothing) Then manager.TransactionRollback()
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (manager IsNot Nothing) Then manager.Disconnect()
                End Try

                'Validacion reconocimiento codigo de barras
                'If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Reconocimiento_CBarras Then
                '    ReconocimientoBarCode()
                'End If

                'EventManager.FinalizarIndexacion(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)
            Catch ex As Exception
                LogError("14- " & ex.Message)
                '' Actualizar el estado del cargue
                CargueType = New DBImaging.SchemaProcess.TBL_CargueType()
                CargueType.fk_Estado = nEstado
                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, fk_Cargue)
                Application.DoEvents()
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Private Shared Function ValidarNombreContraRegla(fileName As String, regla As DataRow) As Integer
            Dim motivos As New List(Of String)
            Dim ok As Boolean = True

            Dim starts = SplitTokens(If(regla("inicia_por")?.ToString(), ""))
            Dim endsOk = True
            Dim ends = SplitTokens(If(regla("finaliza_por")?.ToString(), ""))
            Dim noEnds = SplitTokens(If(regla("no_finaliza_en")?.ToString(), ""))
            Dim andTokens = SplitTokens(If(regla("contiene_and")?.ToString(), ""))
            Dim orTokens = SplitTokens(If(regla("contiene_or")?.ToString(), ""))
            Dim notTokens = SplitTokens(If(regla("no_contiene")?.ToString(), ""))

            ' inicia_por: alguno
            If starts.Count > 0 Then
                Dim hit = starts.Any(Function(t) fileName.StartsWith(t, True, CultureInfo.InvariantCulture))
                If Not hit Then
                    ok = False : motivos.Add("No inicia por: " & String.Join("|", starts))
                End If
            End If

            ' finaliza_Por: alguno
            If ends.Count > 0 Then
                Dim hit = ends.Any(Function(t) fileName.EndsWith(t, True, CultureInfo.InvariantCulture))
                If Not hit Then
                    ok = False : motivos.Add("No finaliza por: " & String.Join("|", ends))
                End If
            End If

            ' no_Finaliza_En: ninguno
            If noEnds.Count > 0 Then
                Dim bad = noEnds.FirstOrDefault(Function(t) fileName.EndsWith(t, True, CultureInfo.InvariantCulture))
                If bad IsNot Nothing Then
                    ok = False : motivos.Add("Finaliza en valor prohibido: " & bad)
                End If
            End If

            ' contiene_and: todos
            For Each t In andTokens
                If fileName.IndexOf(t, StringComparison.OrdinalIgnoreCase) < 0 Then
                    ok = False : motivos.Add("No contiene (AND): " & t)
                End If
            Next

            ' contiene_or: al menos uno (si hay tokens)
            If orTokens.Count > 0 Then
                Dim hit = orTokens.Any(Function(t) fileName.IndexOf(t, StringComparison.OrdinalIgnoreCase) >= 0)
                If Not hit Then
                    ok = False : motivos.Add("No contiene ninguno (OR): " & String.Join("|", orTokens))
                End If
            End If
            ' no_Contiene: ninguno
            For Each t In notTokens
                If fileName.IndexOf(t, StringComparison.OrdinalIgnoreCase) >= 0 Then
                    ok = False : motivos.Add("Contiene prohibido: " & t)
                End If
            Next
            If ok Then
                Return CInt(regla.Item("fk_Tipologia"))
            Else
                Return 0
            End If
        End Function

        Private Shared Function SplitTokens(text As String) As List(Of String)
            If String.IsNullOrWhiteSpace(text) Then Return New List(Of String)
            Dim parts = text.Split(New Char() {"|"c, ";"c, ","c}, StringSplitOptions.RemoveEmptyEntries) _
                        .Select(Function(s) s.Trim()) _
                        .Where(Function(s) s.Length > 0) _
                        .ToList()
            Return parts
        End Function

        Public Sub CargarIndexacionDDO_old(DtConfIndex As DataTable, nEntidadCliente As Short, nProyecto As Short, fk_Cargue As Integer, fk_Cargue_Paquete As Short, nEntidadProcesamiento As Short, FechaProceso As Date, nUser As Integer, nSedeProcesamiento As Short, nCentroProcesamiento As Short, nEsquema As Short, fk_Ot As Integer, ExtImgSalida As String, nEsquemaDataTable As DBCore.SchemaConfig.TBL_EsquemaDataTable, nEstado As Short, nPaqueteName As String)

            Dim manager As FileProviderManager = Nothing
            Dim dbmImaging = New DBImagingDataBaseManager(_ImagingConnectionString)
            Try
                dbmImaging.Connection_Open(1)

                Dim ServidorDataTable As DBImaging.SchemaCore.CTA_ServidorDataTable

                ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(nEsquemaDataTable(0).fk_Entidad_Servidor, nEsquemaDataTable(0).fk_Servidor)

                Dim CentroPro As New DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType
                Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(nEntidadCliente, ServidorDataTable(0).id_Servidor)(0).ToCTA_ServidorSimpleType

                manager = New FileProviderManager(servidor, CentroPro, dbmImaging, nUser)
                manager.Connect()

                Dim sqlAnexoItem As New StringBuilder()
                Dim sqlAnexoFolioItem As New StringBuilder()
                Dim sqlFolderItem As New StringBuilder()
                Dim sqlLlaveItem As New StringBuilder()
                Dim sqlDocumentoItem As New StringBuilder()
                Dim sqlFolioItem As New StringBuilder()
                Dim sql As String = ""

                Dim cUsaAnexo As Integer = CInt(DtConfIndex.Rows(0).Item("Usa_Anexo"))
                Dim cTotAnexos As Integer = CInt(DtConfIndex.Rows(0).Item("ImagenesAnexo"))
                Dim cFolderItem As Integer = CInt(DtConfIndex.Rows(0).Item("idFolderItem"))
                Dim cDocumentoIten As Integer = CInt(DtConfIndex.Rows(0).Item("DocumentoItem"))
                Dim cAnexoItem As Integer = CInt(DtConfIndex.Rows(0).Item("AnexoItem"))
                Dim TFolder As Integer = 1
                Dim TContForder As Integer = 0
                Dim TAnexos As Integer = 0
                Dim TDcto As Integer = 0
                Dim TContDcto As Integer = 0
                Dim TAnexoItem As Integer = 0
                Dim TContAnexoI As Integer = 0

                'Buscamos la informacion a indexar.
                Dim DtLoteIndexar = dbmImaging.SchemaProcess.PA_Cargue_a_Indexar_Get.DBExecute(fk_Cargue, fk_Cargue_Paquete, 0)
                Dim i As Integer
                For i = 0 To DtLoteIndexar.Rows.Count - 1
                    If TAnexos < cTotAnexos Then
                        TAnexos += 1
                        If TAnexos = 1 Then
                            sql = "INSERT INTO #AnexoItem (id_AnexoItem, Folios, fk_Anexo, fk_Documento_Anexo) VALUES (" & TAnexos & ", " & cTotAnexos & ", NULL, " & CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item")) & ");"
                            sqlAnexoItem.Append(sql)
                        End If

                        sql = "INSERT INTO #AnexoFolioItem (fk_AnexoItem, id_AnexoFolioItem, fk_Cargue_Item, fk_Item_Folio) VALUES (" & "1" & ", " & TAnexos & ", " & CStr(DtLoteIndexar.Rows(i).Item("Path_Cargue_Item")) & ", " & CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item")) & ");"
                        sqlAnexoFolioItem.Append(sql)
                    Else
                        TContForder += 1
                        If TContForder > cFolderItem Then
                            TContForder = 1
                            TFolder += 1
                            TContDcto = 0
                            TDcto = 0
                            TAnexoItem = 0
                            TContAnexoI = 0
                        End If
                        If TContForder = 1 Then
                            sql = "INSERT INTO #FolderItem (id_FolderItem, fk_Esquema, fk_Documento_Anexo) VALUES (" & TFolder & ", " & nEsquema & ", " & nEsquema & ");"
                            sqlFolderItem.Append(sql)
                        End If
                        TContDcto += 1
                        If TContDcto > cDocumentoIten Then
                            TContDcto = 1
                            TDcto = 1
                            TContAnexoI = 0
                            TAnexoItem = 0
                        Else
                            TDcto += 1
                        End If
                        Dim WDocuemntoItem As Integer
                        If TDcto > 1 Then
                            WDocuemntoItem = cFolderItem - cDocumentoIten
                        Else
                            WDocuemntoItem = cDocumentoIten
                        End If
                        sql = "INSERT INTO #DocumentoItem (fk_FolderItem, id_DocumentoItem, fk_Documento, Folios) VALUES (" & TFolder & ", " & TDcto & ", " & nEsquema & ", " & WDocuemntoItem & ");"
                        sqlDocumentoItem.Append(sql)
                        TContAnexoI += 1
                        If TContAnexoI > cAnexoItem Then
                            TContAnexoI = 1
                            TAnexoItem = 1
                        Else
                            TAnexoItem += 1
                        End If
                        sql = "INSERT INTO #FolioItem (fk_FolderItem, fk_DocumentoItem, id_FolioItem, fk_Cargue_Item, fk_Item_Folio) VALUES (" & TFolder & ", " & TDcto & ", " & TAnexoItem & ", " & CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item")) & ", " & CInt(DtLoteIndexar.Rows(i).Item("id_Folio")) & ");"
                        sqlFolioItem.Append(sql)
                    End If
                Next

                Try
                    dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                    Dim GuardarNombreImagen As Boolean = True
                    Dim EnTransferencia As Boolean = False
                    Dim IsSofTrac As Boolean = False
                    'If _cargueRow.Observaciones = "SOFTRAC" Then
                    '    IsSofTrac = True
                    'End If
                    Dim ResultadoIndexar = dbmImaging.SchemaProcess.PA_Publicar_Indexacion.DBExecute(
                                fk_Ot,
                                fk_Cargue,
                                fk_Cargue_Paquete,
                                nEntidadProcesamiento,
                                nSedeProcesamiento,
                                nCentroProcesamiento,
                                nEntidadCliente,
                                nProyecto,
                                nUser,
                                nEntidadCliente,
                                ServidorDataTable(0).id_Servidor,
                                EnTransferencia, 'enTransferencia,
                                nEntidadProcesamiento,  'entidadServidorTransferencia,
                                CShort(ServidorDataTable(0).id_Servidor), '1, 'servidorTransferencia,
                                ExtImgSalida,
                                sqlAnexoItem.ToString(),
                                sqlAnexoFolioItem.ToString(),
                                sqlFolderItem.ToString(),
                                sqlLlaveItem.ToString(),
                                sqlDocumentoItem.ToString(),
                                sqlFolioItem.ToString(),
                                IsSofTrac,
                                GuardarNombreImagen)
                    dbmImaging.Transaction_Commit()

                    ''*******Pasar datos destape a captura*****
                    'If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Campos_Captura_Destape Then
                    dbmImaging.SchemaProcess.PA_Insertar_Datos_Captura_Destape.DBExecute(fk_Cargue, fk_Cargue_Paquete)
                    'End If

                    '*******************************************************************************************
                    Dim DtImg As New DataTable
                    '[fk_Cargue], [fk_Cargue_Paquete], [id_Cargue_Item],[Path_Cargue_Item],[Key_Cargue_Item]
                    For i = 0 To DtLoteIndexar.Rows.Count - 1
                        Dim CargueItem As Integer = CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item"))
                        DtImg = dbmImaging.SchemaProcess.PA_Cargue_a_Indexar_Get.DBExecute(fk_Cargue, fk_Cargue_Paquete, CargueItem)

                        ' Actualizar estado folio cargue
                        dbmImaging.SchemaProcess.PA_Set_Cargue_Folio_Indexado.DBExecute(fk_Cargue, fk_Cargue_Paquete, CInt(DtLoteIndexar.Rows(i).Item("id_Cargue_Item")), CInt(ResultadoIndexar.Rows(i).Item("id_Folio_item")))

                        If TAnexos < cTotAnexos Then
                            TAnexos += 1
                            If TAnexos = 1 Then
                                'sql = "INSERT INTO #AnexoItem 

                            End If
                            'sql = "INSERT INTO #AnexoFolioItem 

                        Else
                            TContForder += 1
                            If TContForder > cFolderItem Then
                                TContForder = 1
                                TFolder += 1
                                TContDcto = 0
                                TDcto = 0
                                TAnexoItem = 0
                                TContAnexoI = 0
                            End If
                            If TContForder = 1 Then
                                'sql = "INSERT INTO #FolderItem 

                            End If
                            TContDcto += 1
                            If TContDcto > cDocumentoIten Then
                                TContDcto = 1
                                TDcto = 1
                                TContAnexoI = 0
                                TAnexoItem = 0
                            Else
                                TDcto += 1
                            End If
                            Dim WDocuemntoItem As Integer
                            If TDcto > 1 Then
                                WDocuemntoItem = cFolderItem - cDocumentoIten
                            Else
                                WDocuemntoItem = cDocumentoIten
                            End If
                            'sql = "INSERT INTO #DocumentoItem 

                            TContAnexoI += 1
                            If TContAnexoI > cAnexoItem Then
                                TContAnexoI = 1
                                TAnexoItem = 1
                            Else
                                TAnexoItem += 1
                            End If
                            Dim filaFiltro As DBImaging.SchemaProcess.TBL_Publicar_IndexacionRow
                            ' Si es un File
                            filaFiltro = ResultadoIndexar.FindByEs_Anexoid_Folder_Itemid_Documento_Itemid_Folio_Item(False, CShort(ResultadoIndexar(i).Item("id_folder_item")), CShort(ResultadoIndexar(i).Item("id_Documento_Item")), CShort(ResultadoIndexar(i).Item("id_Folio_item")))

                            manager.CreateItem(filaFiltro.fk_Expediente, filaFiltro.fk_Folder, filaFiltro.fk_File, filaFiltro.fk_Version, ExtImgSalida, Guid.NewGuid())

                            Dim DataImg() As Byte = CType(DtImg.Rows(0).Item("image_binary"), Byte())
                            Dim DataThumbnail() As Byte = CType(DtImg.Rows(0).Item("Thumbnail_Binary"), Byte())

                            manager.CreateFolio(filaFiltro.fk_Expediente, filaFiltro.fk_Folder, filaFiltro.fk_File, filaFiltro.fk_Version, filaFiltro.id_File_Record_Folio, DataImg, DataThumbnail, False)

                            ' Si es un Anexo
                            filaFiltro = ResultadoIndexar.FindByEs_Anexoid_Folder_Itemid_Documento_Itemid_Folio_Item(True, 0, CShort(ResultadoIndexar(i).Item("id_folder_item")), CShort(ResultadoIndexar(i).Item("id_Folio_item")))
                            If (filaFiltro IsNot Nothing) Then
                                manager.CreateItem(filaFiltro.fk_Anexo, ExtImgSalida)
                                manager.CreateFolio(filaFiltro.fk_Anexo, filaFiltro.id_File_Record_Folio, DataImg, DataThumbnail, False)
                            End If

                        End If
                    Next

                    Try
                        dbmImaging.Transaction_Begin(IsolationLevel.ReadUncommitted)

                        Dim carguePaqueteDataTable = dbmImaging.SchemaProcess.PA_Get_Cargue_Folio_Indexado.DBExecute(fk_Cargue, fk_Cargue_Paquete, False)

                        If (carguePaqueteDataTable.Count = 0) Then
                            dbmImaging.SchemaProcess.TBL_Dashboard_Paquetes.DBDelete(fk_Cargue, fk_Cargue_Paquete)

                            Dim updatePaquete = New DBImaging.SchemaProcess.TBL_Cargue_PaqueteType()
                            updatePaquete.fk_Estado = DBCore.EstadoEnum.Indexado
                            dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBUpdate(updatePaquete, fk_Cargue, fk_Cargue_Paquete)
                            dbmImaging.SchemaProcess.TBL_Cargue_Folio.DBDelete(fk_Cargue, fk_Cargue_Paquete, Nothing, Nothing)

                            Dim cargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBFindByfk_Carguefk_Estado(fk_Cargue, DBCore.EstadoEnum.Indexacion)
                            If (cargueDataTable.Count = 0) Then
                                Dim updateCargue = New DBImaging.SchemaProcess.TBL_CargueType()
                                updateCargue.fk_Estado = DBCore.EstadoEnum.Indexado
                                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(updateCargue, fk_Cargue)
                            End If
                        Else
                            '---------------------------------------------------------------------------
                            ' Actualizar Dashboard
                            '---------------------------------------------------------------------------
                            dbmImaging.SchemaProcess.PA_Dashboard_Desbloquear_Paquetes.DBExecute(fk_Cargue, fk_Cargue_Paquete)
                            '---------------------------------------------------------------------------
                        End If
                        For i = 0 To DtLoteIndexar.Rows.Count - 1
                            manager.DeleteItem(fk_Cargue, fk_Cargue_Paquete, CShort(DtLoteIndexar.Rows(i).Item("id_Cargue_item")))
                        Next

                        dbmImaging.Transaction_Commit()

                        CargueLote = New DBImaging.SchemaProcess.TBL_Lote_DigitalizacionType() With {
                            .Indexado = CBool(1)} 'Indexación Automatica
                        dbmImaging.SchemaProcess.TBL_Lote_Digitalizacion.DBUpdate(CargueLote, nPaqueteName)


                    Catch ex As Exception
                        LogError("12- " & ex.Message)
                        '' Actualizar el estado del cargue
                        CargueType = New DBImaging.SchemaProcess.TBL_CargueType()
                        CargueType.fk_Estado = nEstado
                        dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, fk_Cargue)
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    End Try



                Catch ex As Exception
                    LogError("13- " & ex.Message)
                    '' Actualizar el estado del cargue
                    CargueType = New DBImaging.SchemaProcess.TBL_CargueType()
                    CargueType.fk_Estado = nEstado
                    dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, fk_Cargue)

                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    If (manager IsNot Nothing) Then manager.TransactionRollback()
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (manager IsNot Nothing) Then manager.Disconnect()
                End Try

                'Validacion reconocimiento codigo de barras
                'If Me.IndexerImagingGlobal.ProyectoImagingRow.Usa_Reconocimiento_CBarras Then
                '    ReconocimientoBarCode()
                'End If

                'EventManager.FinalizarIndexacion(_paqueteRow.fk_Cargue, _paqueteRow.id_Cargue_Paquete)
            Catch ex As Exception
                LogError("14- " & ex.Message)
                '' Actualizar el estado del cargue
                CargueType = New DBImaging.SchemaProcess.TBL_CargueType()
                CargueType.fk_Estado = nEstado
                dbmImaging.SchemaProcess.TBL_Cargue.DBUpdate(CargueType, fk_Cargue)
                Application.DoEvents()
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Public Function Unzip(ByVal nFileName As String, ByRef MsgError As String) As Boolean
            Try
                If (File.Exists(_OutputFolder & nFileName)) Then
                    ZipUtil.Descomprimir(_OutputFolder & Path.GetFileNameWithoutExtension(nFileName), _OutputFolder & nFileName, True, False)

                    Return True
                Else
                    ' MsgError = "El archivo no existe"
                    Return False
                End If
            Catch ex As Exception
                MsgError = ex.Message
                Return False

            End Try
        End Function

        Public Sub CrearFechaProceso(ByVal dbmImaging As DBImagingDataBaseManager, ByVal nEntidadProcesamiento As Short, ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nFechaProceso As Date, ByVal nUser As Integer)

            Dim DatosFechaProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBGet(nEntidadCliente, nProyecto, Integer.Parse(nFechaProceso.ToString("yyyyMMdd")), nEntidadProcesamiento)

            If DatosFechaProcesoDataTable.Count = 0 Then

                FechaProcesoType.fk_Entidad_Procesamiento = nEntidadProcesamiento
                FechaProcesoType.fk_Entidad = nEntidadCliente
                FechaProcesoType.fk_Proyecto = nProyecto
                FechaProcesoType.id_fecha_proceso = Integer.Parse(nFechaProceso.ToString("yyyyMMdd"))
                FechaProcesoType.Fecha_Proceso = nFechaProceso
                FechaProcesoType.fk_Usuario_Apertura = nUser
                FechaProcesoType.Fecha_Apertura = SlygNullable.SysDate
                FechaProcesoType.Cerrado = False

                dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBInsert(FechaProcesoType)
                Me.FechaProcesoInt = FechaProcesoType.id_fecha_proceso.Value
            Else
                Me.FechaProcesoInt = DatosFechaProcesoDataTable(0).id_fecha_proceso
            End If
        End Sub

        Public Sub CrearOT(ByVal dbmImaging As DBImagingDataBaseManager, ByVal nEntidadProcesamiento As Short, ByVal nEntidadCliente As Short, ByVal nProyecto As Short, ByVal nFechaProceso As Date, ByVal nUser As Integer, ByVal nSedeProcesamiento_Cargue As Short, ByVal nCentroProcesamiento_Cargue As Short, ByVal nfk_Tipo_OT As Short)

            Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_procesofk_OT_TipoCerrado(nEntidadProcesamiento, nEntidadCliente, nProyecto, Integer.Parse(nFechaProceso.ToString("yyyyMMdd")), nfk_Tipo_OT, False)

            If OTDataTable.Count = 0 Then

                dbmImaging.Transaction_Begin()

                OT_Type.fk_Entidad_Procesamiento = nEntidadProcesamiento
                OT_Type.fk_Entidad = nEntidadCliente
                OT_Type.fk_Proyecto = nProyecto
                OT_Type.fk_fecha_proceso = Me.FechaProcesoInt
                OT_Type.fk_OT_Tipo = nfk_Tipo_OT
                OT_Type.fk_Sede_Procesamiento = nSedeProcesamiento_Cargue
                OT_Type.fk_Centro_Procesamiento = nCentroProcesamiento_Cargue
                OT_Type.fk_Usuario_Apertura = nUser
                OT_Type.Fecha_Apertura = SlygNullable.SysDate
                OT_Type.Exportado = False
                OT_Type.Cerrado = False
                OT_Type.id_OT = dbmImaging.SchemaProcess.TBL_OT.DBNextId()

                dbmImaging.SchemaProcess.TBL_OT.DBInsert(OT_Type)

                dbmImaging.Transaction_Commit()

                Me.NewOT = OT_Type.id_OT.Value
            Else
                Me.NewOT = OTDataTable(0).id_OT
            End If
        End Sub

#End Region


    End Class

End Namespace