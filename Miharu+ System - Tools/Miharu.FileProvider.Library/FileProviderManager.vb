Imports System
Imports System.IO
Imports Miharu.FileProvider.Library.Server


Public Class FileProviderManager

#Region " Enumeraciones "

    Public Enum ModoEnum
        Lectura
        Escritura
    End Enum

#End Region

#Region " Declaraciones "

    Private Server As IServer
    Private User As Integer

    Private dbmImaging As DBImaging.DBImagingDataBaseManager

    Private _Modo As ModoEnum

    Private FileProviderLocal As FileProvider
    Private FileProviderAlterno As New FileProvider()
    'Private FileProviderAlterno As New FileProvider("")

    Private _Anexo As Long
    Private _Cargue As Integer
    Private _Expediente As Long
    Private _Folder As Short
    Private _File As Short
    Private _Version As Short
    Private _Fecha As String
    'Private _Date As Date
    Private _Entidad As Integer
    Private _Proyecto As Integer
    'Private _nVolumen As Integer
    'Private _nPaquete As Short
    Private _WorkingFolder As String


    'Private _nEntidad_Servidor As Short
    'Private _nEntidad_Proyecto As Integer
    'Private _nServidor As Short
    'Private _ServidorTipo As DBCore.ServidorTipoEnum

#End Region

#Region " Propiedades "

    Public Property Modo As ModoEnum
        Get
            Return _Modo
        End Get
        Private Set(ByVal value As ModoEnum)
            _Modo = value
        End Set
    End Property

    Public ReadOnly Property ConnectionOpen As Boolean
        Get
            Return Server.ConnectionOpen
        End Get
    End Property

    'Public ReadOnly Property nVolumen As Integer
    '    Get
    '        Return _nVolumen
    '    End Get
    'End Property

    'Public ReadOnly Property Entidad As String
    '    Get
    '        Return _Entidad
    '    End Get
    'End Property

    'Public ReadOnly Property Proyecto As String
    '    Get
    '        Return _Proyecto
    '    End Get
    'End Property

    'Public ReadOnly Property nEntidad_Servidor As Short
    '    Get
    '        Return _nEntidad_Servidor
    '    End Get
    'End Property

    'Public ReadOnly Property nServidor As Short
    '    Get
    '        Return _nServidor
    '    End Get
    'End Property

    'Public ReadOnly Property ServidorTipo As DBCore.ServidorTipoEnum
    '    Get
    '        Return _ServidorTipo
    '    End Get
    'End Property

    Public ReadOnly Property WorkingFolder As String
        Get
            Return _WorkingFolder
        End Get
    End Property

    'Public ReadOnly Property Fecha As String
    '    Get
    '        Return _Fecha
    '    End Get
    'End Property
#End Region

#Region " Constructores "

    Private Sub New(ByVal nUser As Integer, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nModo As ModoEnum)
        Me.User = nUser
        Me.dbmImaging = nDBMImaging
        Me.Modo = nModo
    End Sub

    Public Sub New(ByVal nServidor As DBImaging.SchemaCore.CTA_ServidorSimpleType, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        ' Inicializar el Manager
        Inicializar(nServidor, Nothing, Nothing)
    End Sub

    Public Sub New(ByVal nServidor As DBImaging.SchemaCore.CTA_ServidorSimpleType, ByVal nCentroProcesamiento As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        Dim CentroProcesamientoLocalRow As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = Nothing
        If (nCentroProcesamiento.Usa_Cache_Local) Then
            CentroProcesamientoLocalRow = nCentroProcesamiento
        End If

        ' Inicializar el Manager
        Inicializar(nServidor, CentroProcesamientoLocalRow, Nothing)
    End Sub

    Public Sub New(ByVal nCargue As Integer, ByVal nCentroProcesamiento As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        ' Leer el cargue
        Dim CargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(nCargue)
        If (CargueDataTable.Count = 0) Then Throw New Exception("No se encontró el Cargue")
        Dim CargueRow = CargueDataTable(0)

        ' Leer el servidor
        Dim ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(CargueRow.fk_Entidad_Servidor, CargueRow.fk_Servidor)
        If (ServidorDataTable.Count = 0) Then Throw New Exception("No se encontró el Servidor")
        Dim ServidorRow = ServidorDataTable(0).ToCTA_ServidorSimpleType()

        Dim CentroProcesamientoLocalRow As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = Nothing
        Dim CentroProcesamientoRecursivoRow As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = Nothing
        If (nCentroProcesamiento.Usa_Cache_Local) Then
            CentroProcesamientoLocalRow = nCentroProcesamiento

            ' Leer el centro de procesamiento recursivo
            If (CargueRow.fk_Sede_Procesamiento_Cargue <> nCentroProcesamiento.fk_Sede And CargueRow.fk_Centro_Procesamiento_Cargue <> nCentroProcesamiento.id_Centro_Procesamiento) Then
                Dim CentroProcesamientoLocalDataTable = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(CargueRow.fk_Entidad_Procesamiento, CargueRow.fk_Sede_Procesamiento_Cargue, CargueRow.fk_Centro_Procesamiento_Cargue)
                If (CentroProcesamientoLocalDataTable.Count = 0) Then Throw New Exception("No se encontró el Centro de procesamiento de Cargue")
                If (CentroProcesamientoLocalDataTable(0).Usa_Cache_Local_Recursiva) Then
                    CentroProcesamientoRecursivoRow = CentroProcesamientoLocalDataTable(0).ToCTA_Centro_ProcesamientoSimpleType()
                End If
            End If
        End If

        ' Inicializar el Manager
        Inicializar(ServidorRow, CentroProcesamientoLocalRow, CentroProcesamientoRecursivoRow)
    End Sub

    Public Sub New(ByVal nAnexo As Long, ByVal nCentroProcesamiento As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        ' Leer el Anexo
        Dim AnexoDataTable = dbmImaging.SchemaCore.CTA_Imaging_Anexo.DBFindByid_Anexo(nAnexo)
        If (AnexoDataTable.Count = 0) Then Throw New Exception("No se encontró el Anexo")
        Dim AnexoRow = AnexoDataTable(0)

        ' Leer el servidor
        Dim ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(AnexoRow.fk_Entidad_Servidor, AnexoRow.fk_Servidor)
        If (ServidorDataTable.Count = 0) Then Throw New Exception("No se encontró el Servidor")
        Dim ServidorRow = ServidorDataTable(0).ToCTA_ServidorSimpleType()

        Dim CentroProcesamientoLocalRow As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = Nothing
        If (nCentroProcesamiento.Usa_Cache_Local) Then
            CentroProcesamientoLocalRow = nCentroProcesamiento
        End If

        ' Inicializar el Manager
        Inicializar(ServidorRow, CentroProcesamientoLocalRow, Nothing)
    End Sub

    Public Sub New(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nCentroProcesamiento As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        ' Leer el folder
        Dim FolderDataTable = dbmImaging.SchemaCore.CTA_Folder.DBFindByfk_Expedientefk_Folder(nExpediente, nFolder)
        If (FolderDataTable.Count = 0) Then Throw New Exception("No se encontró el Folder")
        Dim FolderRow = FolderDataTable(0)

        ' Leer el cargue
        Dim CargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(FolderRow.fk_Cargue)
        If (CargueDataTable.Count = 0) Then Throw New Exception("No se encontró el Cargue")
        Dim CargueRow = CargueDataTable(0)

        ' Leer el servidor
        Dim ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(FolderRow.fk_Entidad_Servidor, FolderRow.fk_Servidor)
        If (ServidorDataTable.Count = 0) Then Throw New Exception("No se encontró el Servidor")
        Dim ServidorRow = ServidorDataTable(0).ToCTA_ServidorSimpleType()

        Dim CentroProcesamientoLocalRow As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = Nothing
        Dim CentroProcesamientoRecursivoRow As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = Nothing
        If (nCentroProcesamiento.Usa_Cache_Local) Then
            CentroProcesamientoLocalRow = nCentroProcesamiento

            ' Leer el centro de procesamiento recursivo
            If (CargueRow.fk_Sede_Procesamiento_Cargue <> nCentroProcesamiento.fk_Sede And CargueRow.fk_Centro_Procesamiento_Cargue <> nCentroProcesamiento.id_Centro_Procesamiento) Then
                Dim CentroProcesamientoLocalDataTable = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(CargueRow.fk_Entidad_Procesamiento, CargueRow.fk_Sede_Procesamiento_Cargue, CargueRow.fk_Centro_Procesamiento_Cargue)
                If (CentroProcesamientoLocalDataTable.Count = 0) Then Throw New Exception("No se encontró el Centro de procesamiento de Cargue")
                If (CentroProcesamientoLocalDataTable(0).Usa_Cache_Local_Recursiva) Then
                    CentroProcesamientoRecursivoRow = CentroProcesamientoLocalDataTable(0).ToCTA_Centro_ProcesamientoSimpleType()
                End If
            End If
        End If

        ' Inicializar el Manager
        Inicializar(ServidorRow, CentroProcesamientoLocalRow, CentroProcesamientoRecursivoRow)
    End Sub

    Public Sub New(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nCentroProcesamiento As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        ' Leer el file
        Dim FileDataTable = dbmImaging.SchemaCore.CTA_File_RF.DBFindByfk_Expedientefk_Folderfk_Fileid_Version(nExpediente, nFolder, nFile, nVersion)
        If (FileDataTable.Count = 0) Then Throw New Exception("No se encontró el File - Versión")
        Dim FileRow = FileDataTable(0)

        ' Leer el folder
        Dim FolderDataTable = dbmImaging.SchemaCore.CTA_Folder.DBFindByfk_Expedientefk_Folder(nExpediente, nFolder)
        If (FolderDataTable.Count = 0) Then Throw New Exception("No se encontró el Folder")
        Dim FolderRow = FolderDataTable(0)

        ' Leer el cargue
        Dim CargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(FolderRow.fk_Cargue)
        If (CargueDataTable.Count = 0) Then Throw New Exception("No se encontró el Cargue")
        Dim CargueRow = CargueDataTable(0)

        ' Leer el servidor
        Dim ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(FileRow.fk_Entidad_Servidor, FileRow.fk_Servidor)
        If (ServidorDataTable.Count = 0) Then Throw New Exception("No se encontró el Servidor")
        Dim ServidorRow = ServidorDataTable(0).ToCTA_ServidorSimpleType()

        Dim CentroProcesamientoLocalRow As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = Nothing
        Dim CentroProcesamientoRecursivoRow As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = Nothing
        If (nCentroProcesamiento.Usa_Cache_Local) Then
            CentroProcesamientoLocalRow = nCentroProcesamiento

            ' Leer el centro de procesamiento recursivo
            If (CargueRow.fk_Sede_Procesamiento_Cargue <> nCentroProcesamiento.fk_Sede And CargueRow.fk_Centro_Procesamiento_Cargue <> nCentroProcesamiento.id_Centro_Procesamiento) Then
                Dim CentroProcesamientoLocalDataTable = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(CargueRow.fk_Entidad_Procesamiento, CargueRow.fk_Sede_Procesamiento_Cargue, CargueRow.fk_Centro_Procesamiento_Cargue)
                If (CentroProcesamientoLocalDataTable.Count = 0) Then Throw New Exception("No se encontró el Centro de procesamiento de Cargue")
                If (CentroProcesamientoLocalDataTable(0).Usa_Cache_Local_Recursiva) Then
                    CentroProcesamientoRecursivoRow = CentroProcesamientoLocalDataTable(0).ToCTA_Centro_ProcesamientoSimpleType()
                End If
            End If
        End If

        ' Inicializar el Manager
        Inicializar(ServidorRow, CentroProcesamientoLocalRow, CentroProcesamientoRecursivoRow)
    End Sub

    Public Sub New(ByVal nAnexo As Long, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        ' Leer el Anexo
        Dim AnexoDataTable = dbmImaging.SchemaCore.CTA_Imaging_Anexo.DBFindByid_Anexo(nAnexo)
        If (AnexoDataTable.Count = 0) Then Throw New Exception("No se encontró el Anexo")
        Dim AnexoRow = AnexoDataTable(0)

        ' Leer el servidor
        Dim ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(AnexoRow.fk_Entidad_Servidor, AnexoRow.fk_Servidor)
        If (ServidorDataTable.Count = 0) Then Throw New Exception("No se encontró el Servidor")
        Dim ServidorRow = ServidorDataTable(0).ToCTA_ServidorSimpleType()

        ' Inicializar el Manager
        Inicializar(ServidorRow, Nothing, Nothing)
    End Sub

    Public Sub New(ByVal nExpediente As Long, ByVal nFolder As Short, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        ' Leer el folder
        Dim FolderDataTable = dbmImaging.SchemaCore.CTA_Folder.DBFindByfk_Expedientefk_Folder(nExpediente, nFolder)
        If (FolderDataTable.Count = 0) Then Throw New Exception("No se encontró el Folder")
        Dim FolderRow = FolderDataTable(0)

        ' Leer el servidor
        Dim ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(FolderRow.fk_Entidad_Servidor, FolderRow.fk_Servidor)
        If (ServidorDataTable.Count = 0) Then Throw New Exception("No se encontró el Servidor")
        Dim ServidorRow = ServidorDataTable(0).ToCTA_ServidorSimpleType()

        ' Inicializar el Manager
        Inicializar(ServidorRow, Nothing, Nothing)
    End Sub

    Public Sub New(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        ' Leer el file
        Dim FileDataTable = dbmImaging.SchemaCore.CTA_File_RF.DBFindByfk_Expedientefk_Folderfk_Fileid_Version(nExpediente, nFolder, nFile, nVersion)
        If (FileDataTable.Count = 0) Then Throw New Exception("No se encontró el File - Versión")
        Dim FileRow = FileDataTable(0)

        ' Leer el folder
        Dim FolderDataTable = dbmImaging.SchemaCore.CTA_Folder.DBFindByfk_Expedientefk_Folder(nExpediente, nFolder)
        If (FolderDataTable.Count = 0) Then Throw New Exception("No se encontró el Folder")
        Dim FolderRow = FolderDataTable(0)

        ' Leer el servidor
        Dim ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(FileRow.fk_Entidad_Servidor, FileRow.fk_Servidor)
        If (ServidorDataTable.Count = 0) Then Throw New Exception("No se encontró el Servidor")
        Dim ServidorRow = ServidorDataTable(0).ToCTA_ServidorSimpleType()

        ' Inicializar el Manager
        Inicializar(ServidorRow, Nothing, Nothing)
    End Sub

    Public Sub New(ByVal nExpediente As Long, ByVal nFolder As Short, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer, ByVal Remoting As String)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        ' Leer el folder
        Dim FolderDataTable = dbmImaging.SchemaCore.CTA_Folder.DBFindByfk_Expedientefk_Folder(nExpediente, nFolder)
        If (FolderDataTable.Count = 0) Then Throw New Exception("No se encontró el Folder")
        Dim FolderRow = FolderDataTable(0)


        ' Leer el servidor
        Dim ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(FolderRow.fk_Entidad_Servidor, FolderRow.fk_Servidor)
        If (ServidorDataTable.Count = 0) Then Throw New Exception("No se encontró el Servidor")
        Dim ServidorRow = ServidorDataTable(0).ToCTA_ServidorSimpleType()

        ' Inicializar el Manager
        Inicializar(ServidorRow, Nothing, Nothing, Remoting)
    End Sub

    Public Sub New(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer, ByVal Remoting As String)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        ' Leer el file
        Dim FileDataTable = dbmImaging.SchemaCore.CTA_File_RF.DBFindByfk_Expedientefk_Folderfk_Fileid_Version(nExpediente, nFolder, nFile, nVersion)
        If (FileDataTable.Count = 0) Then Throw New Exception("No se encontró el File - Versión")
        Dim FileRow = FileDataTable(0)

        ' Leer el folder
        Dim FolderDataTable = dbmImaging.SchemaCore.CTA_Folder.DBFindByfk_Expedientefk_Folder(nExpediente, nFolder)
        If (FolderDataTable.Count = 0) Then Throw New Exception("No se encontró el Folder")
        Dim FolderRow = FolderDataTable(0)

        ' Leer el servidor
        Dim ServidorDataTable = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(FileRow.fk_Entidad_Servidor, FileRow.fk_Servidor)
        If (ServidorDataTable.Count = 0) Then Throw New Exception("No se encontró el Servidor")
        Dim ServidorRow = ServidorDataTable(0).ToCTA_ServidorSimpleType()

        ' Inicializar el Manager
        Inicializar(ServidorRow, Nothing, Nothing, Remoting)
    End Sub


    Public Sub New(ByVal nServidorRow As DBImaging.SchemaCore.CTA_ServidorRow, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer, ByVal Remoting As String)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        Dim ServidorRow = nServidorRow.ToCTA_ServidorSimpleType()

        ' Inicializar el Manager
        Inicializar(ServidorRow, Nothing, Nothing, Remoting)
    End Sub

    Public Sub New(ByVal nServidorRow As DBImaging.SchemaCore.CTA_ServidorRow, ByRef nDBMImaging As DBImaging.DBImagingDataBaseManager, ByVal nUser As Integer)
        Me.New(nUser, nDBMImaging, ModoEnum.Escritura)

        Dim ServidorRow = nServidorRow.ToCTA_ServidorSimpleType()

        ' Inicializar el Manager
        Inicializar(ServidorRow, Nothing, Nothing)
    End Sub


    Protected Overrides Sub Finalize()
        If (ConnectionOpen) Then
            Disconnect()

            Me.FileProviderLocal = Nothing
            Me.FileProviderAlterno = Nothing
        End If
    End Sub

#End Region

#Region " Metodos "

    Private Sub Inicializar(ByVal nServidor As DBImaging.SchemaCore.CTA_ServidorSimpleType, ByVal nCentroProcesamientoLocal As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByVal nCentroProcesamientoCargue As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType)
        ' Crear el conector al servidor principal
        Select Case CType(nServidor.fk_Servidor_Tipo, DBCore.ServidorTipoEnum)
            Case DBCore.ServidorTipoEnum.Database
                Me.Server = New DatabaseServer(nServidor.ConnectionString_Servidor)
                '_ServidorTipo = DBCore.ServidorTipoEnum.Database

            Case DBCore.ServidorTipoEnum.Fileserver
                Me.Server = New FileServer(nServidor.IPName_Servidor, nServidor.Port_Servidor, nServidor.AppName_Servidor, nServidor.Directorio_Trabajo)
                'InicializarFileServer(nServidor.Directorio_Trabajo)

            Case Else
                Throw New Exception("Tipo de Servidor no válido: " & nServidor.fk_Servidor_Tipo)
        End Select

        ' Crear conector a Caché Local
        If (nCentroProcesamientoLocal IsNot Nothing) Then
            Me.FileProviderLocal = FileProvider.Create(nCentroProcesamientoLocal.IPName_Servidor, nCentroProcesamientoLocal.Port_Servidor, nCentroProcesamientoLocal.AppName_Servidor)

            ' Crear conector a Caché Recursivo
            If (nCentroProcesamientoCargue IsNot Nothing) Then
                Me.FileProviderAlterno = FileProvider.Create(nCentroProcesamientoCargue.IPName_Servidor, nCentroProcesamientoCargue.Port_Servidor, nCentroProcesamientoCargue.AppName_Servidor)
            End If
        End If
    End Sub


    Private Sub Inicializar(ByVal nServidor As DBImaging.SchemaCore.CTA_ServidorSimpleType, ByVal nCentroProcesamientoLocal As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByVal nCentroProcesamientoCargue As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType, ByVal Remoting As String)
        ' Crear el conector al servidor principal
        Select Case CType(nServidor.fk_Servidor_Tipo, DBCore.ServidorTipoEnum)
            Case DBCore.ServidorTipoEnum.Database
                Me.Server = New DatabaseServer(nServidor.ConnectionString_Servidor & Remoting)
                '_ServidorTipo = DBCore.ServidorTipoEnum.Database

            Case DBCore.ServidorTipoEnum.Fileserver
                Me.Server = New FileServer(nServidor.IPName_Servidor, nServidor.Port_Servidor, nServidor.AppName_Servidor, nServidor.Directorio_Trabajo)
                'InicializarFileServer(nServidor.Directorio_Trabajo)

            Case Else
                Throw New Exception("Tipo de Servidor no válido: " & nServidor.fk_Servidor_Tipo)
        End Select

        ' Crear conector a Caché Local
        If (nCentroProcesamientoLocal IsNot Nothing) Then
            Me.FileProviderLocal = FileProvider.Create(nCentroProcesamientoLocal.IPName_Servidor, nCentroProcesamientoLocal.Port_Servidor, nCentroProcesamientoLocal.AppName_Servidor)

            ' Crear conector a Caché Recursivo
            If (nCentroProcesamientoCargue IsNot Nothing) Then
                Me.FileProviderAlterno = FileProvider.Create(nCentroProcesamientoCargue.IPName_Servidor, nCentroProcesamientoCargue.Port_Servidor, nCentroProcesamientoCargue.AppName_Servidor)
            End If
        End If
    End Sub

    'Private Sub InicializarFileServer(ByVal workingFolder As String)
    '    _WorkingFolder = workingFolder
    '    _nVolumen = -1
    '    _ServidorTipo = DBCore.ServidorTipoEnum.Fileserver
    'End Sub

    Public Sub Connect()
        If (Me.ConnectionOpen) Then Throw New Exception("La conexión ya se encuentra abierta")
        Server.Connect(Me.User)
    End Sub

    Public Sub TransactionBegin()
        ValidateConnection(True)
        Server.TransactionBegin()
    End Sub

    Public Sub TransactionCommit()
        ValidateConnection(True)
        Server.TransactionCommit()
    End Sub

    Public Sub TransactionRollback()
        ValidateConnection(True)
        Server.TransactionRollback()
    End Sub

    Public Sub Disconnect()
        If (Me.ConnectionOpen) Then Server.Disconnect()
    End Sub

    'Private Sub Actualizar_Peso_Cantidad_Volumen_Anexo(ByVal nAnexo As Long, ByVal nVolumen As Integer, ByVal nEntidad As Short, ByVal nServidor As Short, ByVal FechaCreacion As Date, Peso As Long)

    '    dbmImaging.SchemaProcess.PA_Actualizar_File_Volumen_Anexo.DBExecute(nAnexo, nVolumen, nEntidad, nServidor, CType(FechaCreacion, DateTime), Peso)

    'End Sub

    'Private Sub Actualizar_Peso_Cantidad_Volumen_Expediente(ByVal nExpediente As Long, ByVal Folder As Short, ByVal File As Short, ByVal Version As Short, ByVal nVolumen As Integer, ByVal nEntidad As Short, ByVal nServidor As Short, ByVal FechaCreacion As Date, ByVal Peso As Long, ByVal Cantidad As Short)

    '    dbmImaging.SchemaProcess.PA_Actualizar_File_Volumen.DBExecute(nExpediente, Folder, File, Version, nVolumen, nEntidad, nServidor, CType(FechaCreacion, DateTime), Peso, Cantidad)

    'End Sub

    'Private Sub Actualizar_Peso_Cantidad_Volumen_Cargue(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short, ByVal nVolumen As Integer, Peso As Long)

    '    dbmImaging.SchemaProcess.PA_Actualizar_Cargue_Volumen.DBExecute(nCargue, nPaquete, nItem, nFolio, nVolumen, Peso)

    'End Sub

    'Private Sub Actualizar_Peso_Cantidad_Volumen(ByVal nVolumen As Integer, ByVal Peso As Long, ByVal Cantidad As Integer)

    '    dbmImaging.SchemaProcess.PA_Actualizar_Peso_Cantidad_Volumen.DBExecute(nVolumen, Peso, Cantidad)

    'End Sub

    Public Sub CreateItem(ByVal nAnexo As Long, ByVal nContentType As String)
        ValidateConnection(True)

        Me.Server.CreateItem(nAnexo, nContentType)
    End Sub

    Public Sub CreateItem(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer)
        ValidateConnection(True)

        Me.Server.CreateItem(nCargue, nPaquete, nItem)
    End Sub

    Public Sub CreateItem(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nContentType As String, ByVal nFileUniqueIdentifier As Guid)
        ValidateConnection(True)

        Me.Server.CreateItem(nExpediente, nFolder, nFile, nVersion, nContentType, nFileUniqueIdentifier)
    End Sub

    'Public Sub CreateFolio(ByVal nAnexo As Long, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte, isRemoting As Boolean)
    '    ValidateConnection(True)

    '    Me.Server.CreateFolio(getFecha(nAnexo), nAnexo, nFolio, nImagen, nThumbnail, isRemoting)

    '    If (Me.FileProviderLocal Is Nothing) Then Return

    '    Dim MsgError As String = ""
    '    If (Not Me.FileProviderLocal.CreateCache(nAnexo, nFolio, nImagen, nThumbnail, MsgError)) Then
    '        Throw New Exception(MsgError)
    '    End If

    'End Sub

    Public Sub CreateFolio(ByVal nAnexo As Long, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte, isRemoting As Boolean)
        ValidateConnection(True)

        Me.Server.CreateFolio(getRuta(nAnexo), nAnexo, nFolio, nImagen, nThumbnail, isRemoting)

        If (Me.FileProviderLocal Is Nothing) Then Return

        Dim MsgError As String = ""
        'If (Not Me.FileProviderLocal.CreateCache(nAnexo, nFolio, nImagen, nThumbnail, MsgError)) Then
        '    Throw New Exception(MsgError)
        'End If

        If (Not Me.FileProviderLocal.CreateCache(nAnexo, nFolio, nImagen, nThumbnail, MsgError)) Then
            Throw New Exception(MsgError)
        End If

    End Sub

    'Public Sub CreateFolio(ByVal nAnexo As Long, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte, isRemoting As Boolean)
    '    ValidateConnection(True)

    '    If GetVolumenEntidadProyectoInUse(nAnexo) Then
    '        Me.Server.CreateFolio(nVolumen, sEntidad, sProyecto, getFecha(nAnexo), nAnexo, nFolio, nImagen, nThumbnail, isRemoting)
    '        Actualizar_Peso_Cantidad_Volumen_Anexo(nAnexo, nVolumen, Me._nEntidad_Servidor, Me._nServidor, Me._Date, nImagen.Length)

    '        If (Me.FileProviderLocal Is Nothing) Then Return

    '        Dim MsgError As String = ""
    '        If (Not Me.FileProviderLocal.CreateCache(nAnexo, nFolio, nImagen, nThumbnail, MsgError)) Then
    '            Throw New Exception(MsgError)
    '        End If
    '    Else
    '        Me.Server.CreateFolio(getFecha(nAnexo), nAnexo, nFolio, nImagen, nThumbnail, isRemoting)

    '        If (Me.FileProviderLocal Is Nothing) Then Return

    '        Dim MsgError As String = ""
    '        If (Not Me.FileProviderLocal.CreateCache(nAnexo, nFolio, nImagen, nThumbnail, MsgError)) Then
    '            Throw New Exception(MsgError)
    '        End If
    '    End If

    'End Sub

    'Public Sub CreateFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte, isRemoting As Boolean)
    '    ValidateConnection(True)

    '    Me.Server.CreateFolio(getFecha(nCargue), nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, isRemoting)

    '    If (Me.FileProviderLocal Is Nothing) Then Return

    '    Dim MsgError As String = ""
    '    If (Not Me.FileProviderLocal.CreateCache(nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, MsgError)) Then
    '        Throw New Exception(MsgError)
    '    End If
    'End Sub

    Public Sub CreateFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte, isRemoting As Boolean)
        ValidateConnection(True)

        Me.Server.CreateFolio(getRuta(nCargue), nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, isRemoting)

        If (Me.FileProviderLocal Is Nothing) Then Return

        Dim MsgError As String = ""
        If (Not Me.FileProviderLocal.CreateCache(nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, MsgError)) Then
            Throw New Exception(MsgError)
        End If
    End Sub

    'Public Sub CreateFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte, isRemoting As Boolean)
    '    ValidateConnection(True)

    '    If GetVolumenEntidadProyectoInUse(nCargue, nPaquete) Then
    '        Me.Server.CreateFolio(nVolumen, sEntidad, sProyecto, getFecha(nCargue), nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, isRemoting)
    '        Actualizar_Peso_Cantidad_Volumen_Cargue(nCargue, nPaquete, nItem, nFolio, nVolumen, nImagen.Length)
    '    Else
    '        Me.Server.CreateFolio(getFecha(nCargue), nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, isRemoting)
    '    End If

    '    If (Me.FileProviderLocal Is Nothing) Then Return

    '    Dim MsgError As String = ""
    '    If (Not Me.FileProviderLocal.CreateCache(nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, MsgError)) Then
    '        Throw New Exception(MsgError)
    '    End If
    'End Sub

    'Public Sub CreateFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte, isRemoting As Boolean)
    '    ValidateConnection(True)

    '    Me.Server.CreateFolio(getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, isRemoting)

    '    If (Me.FileProviderLocal Is Nothing) Then Return

    '    Dim MsgError As String = ""
    '    If (Not Me.FileProviderLocal.CreateCache(nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, MsgError)) Then
    '        Throw New Exception(MsgError)
    '    End If
    'End Sub

    Public Sub CreateFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte, isRemoting As Boolean)
        ValidateConnection(True)

        Me.Server.CreateFolio(getRuta(nExpediente, nFolder, nFile, nVersion), nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, isRemoting)

        If (Me.FileProviderLocal Is Nothing) Then Return

        Dim MsgError As String = ""
        If (Not Me.FileProviderLocal.CreateCache(nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, MsgError)) Then
            Throw New Exception(MsgError)
        End If
    End Sub

    'Public Sub CreateFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte, isRemoting As Boolean)
    '    ValidateConnection(True)

    '    Dim MsgError As String = ""
    '    If GetVolumenEntidadProyectoInUse(nExpediente, nFolder, nFile, nVersion) Then

    '        Me.Server.CreateFolio(nVolumen, sEntidad, sProyecto, getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, isRemoting)
    '        Actualizar_Peso_Cantidad_Volumen_Expediente(nExpediente, nFolder, nFile, nVersion, nVolumen, Me._nEntidad_Servidor, Me._nServidor, Me._Date, nImagen.Length, 1)

    '        If (Me.FileProviderLocal Is Nothing) Then Return

    '        If (Not Me.FileProviderLocal.CreateCache(nVolumen, sEntidad, sProyecto, getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, MsgError)) Then
    '            Throw New Exception(MsgError)
    '        End If

    '    Else

    '        Me.Server.CreateFolio(getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, isRemoting)

    '        If (Me.FileProviderLocal Is Nothing) Then Return

    '        If (Not Me.FileProviderLocal.CreateCache(nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, MsgError)) Then
    '            Throw New Exception(MsgError)
    '        End If

    '    End If


    'End Sub


    'Public Sub UpdateFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte)
    '    ValidateConnection(True)

    '    Me.Server.UpdateFolio(getFecha(nCargue), nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail)
    'End Sub

    Public Sub UpdateFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte)
        ValidateConnection(True)

        Me.Server.UpdateFolio(getRuta(nCargue), nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail)
    End Sub

    'Public Sub UpdateFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte)
    '    ValidateConnection(True)
    '    If GetVolumenEntidadProyecto(nCargue, nPaquete) Then
    'Dim Peso_Old As Long = GetSizeFolio(nCargue, nPaquete, nItem, nFolio)
    '        Me.Server.UpdateFolio(nVolumen, sEntidad, sProyecto, getFecha(nCargue), nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail)
    '    Else
    '        Me.Server.UpdateFolio(getFecha(nCargue), nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail)
    '    End If
    'End Sub

    'Public Sub UpdateFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte)
    '    ValidateConnection(True)

    '    Me.Server.UpdateFolio(getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail)
    'End Sub

    Public Sub UpdateFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte)
        ValidateConnection(True)

        Me.Server.UpdateFolio(getRuta(nExpediente, nFolder, nFile, nVersion), nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail)
    End Sub

    'Public Sub UpdateFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short, ByVal nImagen() As Byte, ByVal nThumbnail() As Byte)
    '    ValidateConnection(True)
    '    If GetVolumenEntidadProyecto(nExpediente, nFolder, nFile, nVersion) Then
    '        Dim Peso_Old As Long = GetSizeFolio(nExpediente, nFolder, nFile, nVersion, nFolio)
    '        Me.Server.UpdateFolio(nVolumen, sEntidad, sProyecto, getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail)
    '        Actualizar_Peso_Cantidad_Volumen_Expediente(nExpediente, nFolder, nFile, nVersion, nVolumen, Me._nEntidad_Servidor, Me._nServidor, _Date, nImagen.Length - Peso_Old, 0)
    '    Else
    '        Me.Server.UpdateFolio(getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail)
    '    End If

    'End Sub

    'Public Sub MoveFolio(nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short)
    '    ValidateConnection(True)

    '    Me.Server.MoveFolio(getFecha(nCargue), nCargue, nPaquete, nItem, nFolioItem, getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolioFile)

    '    If (Me.FileProviderLocal Is Nothing) Then Return

    '    Dim MsgError As String = ""
    '    If (Me.FileProviderLocal.MoveCache(nCargue, nPaquete, nItem, nFolioItem, nExpediente, nFolder, nFile, nVersion, nFolioFile, MsgError)) Then
    '        Throw New Exception(MsgError)
    '    End If
    'End Sub

    Public Sub MoveFolio(nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short)
        ValidateConnection(True)

        Me.Server.MoveFolio(getRuta(nCargue), nCargue, nPaquete, nItem, nFolioItem, getRuta(nExpediente, nFolder, nFile, nVersion), nExpediente, nFolder, nFile, nVersion, nFolioFile)

        If (Me.FileProviderLocal Is Nothing) Then Return

        Dim MsgError As String = ""
        If (Me.FileProviderLocal.MoveCache(nCargue, nPaquete, nItem, nFolioItem, nExpediente, nFolder, nFile, nVersion, nFolioFile, MsgError)) Then
            Throw New Exception(MsgError)
        End If
    End Sub

    'Public Sub MoveFolio(nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nAnexo As Long, nFolioAnexo As Short)
    '    ValidateConnection(True)

    '    Me.Server.MoveFolio(getFecha(nCargue), nCargue, nPaquete, nItem, nFolioItem, getFecha(nAnexo), nAnexo, nFolioAnexo)

    '    If (Me.FileProviderLocal Is Nothing) Then Return

    '    Dim MsgError As String = ""
    '    If (Me.FileProviderLocal.MoveCache(nCargue, nPaquete, nItem, nFolioItem, nAnexo, nFolioAnexo, MsgError)) Then
    '        Throw New Exception(MsgError)
    '    End If
    'End Sub

    Public Sub MoveFolio(nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nAnexo As Long, nFolioAnexo As Short)
        ValidateConnection(True)

        Me.Server.MoveFolio(getRuta(nCargue), nCargue, nPaquete, nItem, nFolioItem, getRuta(nAnexo), nAnexo, nFolioAnexo)

        If (Me.FileProviderLocal Is Nothing) Then Return

        Dim MsgError As String = ""
        If (Me.FileProviderLocal.MoveCache(nCargue, nPaquete, nItem, nFolioItem, nAnexo, nFolioAnexo, MsgError)) Then
            Throw New Exception(MsgError)
        End If
    End Sub


    'Public Sub DeleteItem(ByVal nAnexo As Long)
    '    ValidateConnection(True)

    '    Me.Server.DeleteItem(getFecha(nAnexo), nAnexo)
    'End Sub

    Public Sub DeleteItem(ByVal nAnexo As Long)
        ValidateConnection(True)

        Me.Server.DeleteItem(getRuta(nAnexo), nAnexo)
    End Sub

    'Public Sub DeleteItem(ByVal nAnexo As Long)
    '    ValidateConnection(True)
    '    If GetVolumenEntidadProyecto(nAnexo) Then
    '        Me.Server.DeleteItem(nVolumen, sEntidad, sProyecto, getFecha(nAnexo), nAnexo)
    '    Else
    '        Me.Server.DeleteItem(getFecha(nAnexo), nAnexo)
    '    End If

    'End Sub

    'Public Sub DeleteItem(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer)
    '    ValidateConnection(True)

    '    Me.Server.DeleteItem(getFecha(nCargue), nCargue, nPaquete, nItem)
    'End Sub

    Public Sub DeleteItem(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer)
        ValidateConnection(True)

        Me.Server.DeleteItem(getRuta(nCargue), nCargue, nPaquete, nItem)
    End Sub


    'Public Sub DeleteItem(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer)
    '    ValidateConnection(True)
    '    If GetVolumenEntidadProyecto(nCargue, nPaquete) Then
    '        Me.Server.DeleteItem(nVolumen, sEntidad, sProyecto, getFecha(nCargue), nCargue, nPaquete, nItem)
    '    Else
    '        Me.Server.DeleteItem(getFecha(nCargue), nCargue, nPaquete, nItem)
    '    End If
    'End Sub

    'Public Sub DeleteItem(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short)
    '    ValidateConnection(True)
    '    Me.Server.DeleteItem(getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion)
    'End Sub

    Public Sub DeleteItem(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short)
        ValidateConnection(True)
        Me.Server.DeleteItem(getRuta(nExpediente, nFolder, nFile, nVersion), nExpediente, nFolder, nFile, nVersion)
    End Sub

    'Public Sub DeleteItem(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short)
    '    ValidateConnection(True)
    '    If GetVolumenEntidadProyecto(nExpediente, nFolder, nFile, nVersion) Then
    '        Actualizar_Peso_DeleteItem(nExpediente, nFolder, nFile, nVersion)
    '        Me.Server.DeleteItem(nVolumen, sEntidad, sProyecto, getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion)
    '    Else
    '        Me.Server.DeleteItem(getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion)
    '    End If

    'End Sub

    'Private Sub Actualizar_Peso_DeleteItem(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short)
    '    Dim nImagen As Byte()
    '    Dim nThumbnail As Byte()
    '    Dim Folios As Short
    '    Dim Peso As Long
    '    Folios = GetFolios(nExpediente, nFolder, nFile, nVersion)
    '    For folio As Short = 1 To Folios
    '        nImagen = Nothing
    '        nThumbnail = Nothing
    '        GetFolio(nExpediente, nFolder, nFile, nVersion, folio, nImagen, nThumbnail)
    '        Peso += nImagen.Length
    '    Next
    '    Actualizar_Peso_Cantidad_Volumen_Expediente(nExpediente, nFolder, nFile, nVersion, Me.nVolumen, Me._nEntidad_Servidor, Me._nServidor, Me._Date, -Peso, -Folios)

    'End Sub

    'Public Sub DeleteCargue(ByVal nCargue As Integer)
    '    ValidateConnection(True)

    '    Me.Server.DeleteCargue(getFecha(nCargue), nCargue)
    'End Sub

    Public Sub DeleteCargue(ByVal nCargue As Integer)
        ValidateConnection(True)

        Me.Server.DeleteCargue(getRuta(nCargue), nCargue)
    End Sub

    'Public Sub DeleteCargue(ByVal nCargue As Integer)
    '    ValidateConnection(True)

    '    If GetVolumenEntidadProyectoInUse(nCargue, 0) Then
    '        Me.Server.DeleteCargue(nVolumen, sEntidad, sProyecto, getFecha(nCargue), nCargue)
    '    Else
    '        Me.Server.DeleteCargue(getFecha(nCargue), nCargue)
    '    End If

    'End Sub

    'Public Sub DeleteExpediente(ByVal nExpediente As Long)
    '    ValidateConnection(True)

    '    Me.Server.DeleteExpediente(getFecha(nExpediente, 1), nExpediente)
    'End Sub

    'revisarlo porque serian varias rutas JJBM.
    Public Sub DeleteExpediente(ByVal nExpediente As Long)
        ValidateConnection(True)
        'ajustar jjbm
        'Me.Server.DeleteExpediente(getRuta(nExpediente, 1, 1), nExpediente)
    End Sub

    'Public Sub DeleteExpediente(ByVal nExpediente As Long)
    '    ValidateConnection(True)
    '    If GetVolumenEntidadProyecto(nExpediente, Nothing, Nothing, Nothing) Then
    '        Me.Server.DeleteExpediente(nVolumen, sEntidad, sProyecto, getFecha(nExpediente, 1), nExpediente)
    '    Else
    '        Me.Server.DeleteExpediente(getFecha(nExpediente, 1), nExpediente)
    '    End If

    'End Sub


    Private Sub ValidateConnection(ByVal IsWrite As Boolean)
        If (Not Me.ConnectionOpen) Then Throw New Exception("Se debe realizar la conexión antes de acceder a la data")
        If (IsWrite AndAlso Me._Modo = ModoEnum.Lectura) Then Throw New Exception("El Manager fue creado en modo lectura y no se puede realizar modificaciones")
    End Sub

#End Region

#Region " Funciones "

    Public Function GetLastVersion(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short) As Short
        Dim ImagenDataTable = dbmImaging.SchemaCore.CTA_File.DBFindByfk_Expedientefk_Folderfk_Fileid_Version(nExpediente, nFolder, nFile, Nothing, 0, New DBImaging.SchemaCore.CTA_FileEnumList(DBImaging.SchemaCore.CTA_FileEnum.id_Version, False))

        If (ImagenDataTable.Count = 0) Then Throw New Exception("La imagen no existe, Ex: " & nExpediente & ", Fo: " & nFolder & ", Fi: " & nFile)

        Return ImagenDataTable(0).id_Version
    End Function


    'Public Function GetFolio(ByVal nAnexo As Long, ByVal nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte) As Boolean
    '    ValidateConnection(False)

    '    Dim Fecha = getFecha(nAnexo)
    '    If (Not Me.FileProviderLocal Is Nothing) Then
    '        Dim Folio = GetFolioCache(Fecha, nAnexo, nFolio)

    '        nImagen = Folio.Image_Binary
    '        nThumbnail = Folio.Thumbnail_Binary
    '    Else
    '        Dim Folio = Me.Server.GetItem(Fecha, nAnexo, nFolio)

    '        nImagen = Folio.Image_Binary
    '        nThumbnail = Folio.Thumbnail_Binary
    '    End If

    '    Return True
    'End Function

    Public Function GetFolio(ByVal nAnexo As Long, ByVal nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte) As Boolean
        ValidateConnection(False)

        Dim Ruta = getRuta(nAnexo)
        If (Not Me.FileProviderLocal Is Nothing) Then
            'Dim Folio = GetFolioCache(Ruta, nAnexo, nFolio)
            Dim Folio As Miharu.FileProvider.Library.Server.FolioStructure

            Try
                Folio = GetFolioCache(Ruta, nAnexo, nFolio)
            Catch ex As Exception
                Folio = GetFolioCache(Replace(Ruta.Substring(0, 10), "\", ""), nAnexo, nFolio)
            End Try

            'If Folio.Image_Binary.LongLength = 0 Then
            '    Folio = GetFolioCache(Replace(Ruta.Substring(0, 10), "\", ""), nAnexo, nFolio)
            'End If

            nImagen = Folio.Image_Binary
            nThumbnail = Folio.Thumbnail_Binary
        Else
            'Dim Folio = Me.Server.GetItem(Ruta, nAnexo, nFolio)
            Dim Folio As Miharu.FileProvider.Library.Server.FolioStructure

            Try
                Folio = Me.Server.GetItem(Ruta, nAnexo, nFolio)
            Catch ex As Exception
                Folio = Me.Server.GetItem(Replace(Ruta.Substring(0, 10), "\", ""), nAnexo, nFolio)
            End Try

            'If Folio.Image_Binary.LongLength = 0 Then
            '    Folio = GetFolioCache(Replace(Ruta.Substring(0, 10), "\", ""), nAnexo, nFolio)
            'End If

            nImagen = Folio.Image_Binary
            nThumbnail = Folio.Thumbnail_Binary
        End If

        Return True
    End Function

    'Public Function GetFolio(ByVal nAnexo As Long, ByVal nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte) As Boolean
    '    ValidateConnection(False)

    '    Dim Folio As Miharu.FileProvider.Library.Server.FolioStructure
    '    Dim Fecha = getFecha(nAnexo)

    '    If GetVolumenEntidadProyecto(nAnexo) Then

    '        If (Not Me.FileProviderLocal Is Nothing) Then
    '            Folio = GetFolioCache(Fecha, nAnexo, nFolio)
    '        Else
    '            Folio = Me.Server.GetItem(nVolumen, sEntidad, sProyecto, Fecha, nAnexo, nFolio)
    '        End If
    '    Else
    '        If (Not Me.FileProviderLocal Is Nothing) Then
    '            Folio = GetFolioCache(Fecha, nAnexo, nFolio)
    '        Else
    '            Folio = Me.Server.GetItem(Fecha, nAnexo, nFolio)
    '        End If
    '    End If

    '    nImagen = Folio.Image_Binary
    '    nThumbnail = Folio.Thumbnail_Binary

    '    Return True

    'End Function

    'Public Function GetFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte) As Boolean
    '    ValidateConnection(False)

    '    Dim Fecha = getFecha(nCargue)
    '    If (Not Me.FileProviderLocal Is Nothing) Then
    '        Dim Folio = GetFolioCache(Fecha, nCargue, nPaquete, nItem, nFolio)

    '        nImagen = Folio.Image_Binary
    '        nThumbnail = Folio.Thumbnail_Binary
    '    Else
    '        Dim Folio = Me.Server.GetItem(Fecha, nCargue, nPaquete, nItem, nFolio)

    '        nImagen = Folio.Image_Binary
    '        nThumbnail = Folio.Thumbnail_Binary
    '    End If

    '    Return True
    'End Function

    Public Function GetFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte) As Boolean
        ValidateConnection(False)

        Dim Ruta = getRuta(nCargue)
        If (Not Me.FileProviderLocal Is Nothing) Then
            'Dim Folio = GetFolioCache(Ruta, nCargue, nPaquete, nItem, nFolio)
            Dim Folio As Miharu.FileProvider.Library.Server.FolioStructure

            Try
                Folio = GetFolioCache(Ruta, nCargue, nPaquete, nItem, nFolio)
            Catch ex As Exception
                Folio = GetFolioCache(Replace(Ruta.Substring(0, 10), "\", ""), nCargue, nPaquete, nItem, nFolio)
            End Try

            nImagen = Folio.Image_Binary
            nThumbnail = Folio.Thumbnail_Binary
        Else
            'Dim Folio = Me.Server.GetItem(Ruta, nCargue, nPaquete, nItem, nFolio)
            Dim Folio As Miharu.FileProvider.Library.Server.FolioStructure

            Try
                Folio = Me.Server.GetItem(Ruta, nCargue, nPaquete, nItem, nFolio)
            Catch ex As Exception
                Folio = Me.Server.GetItem(Replace(Ruta.Substring(0, 10), "\", ""), nCargue, nPaquete, nItem, nFolio)
            End Try

            nImagen = Folio.Image_Binary
            nThumbnail = Folio.Thumbnail_Binary
        End If

        Return True
    End Function

    'Public Function GetFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte) As Boolean
    '    ValidateConnection(False)

    '    Dim Folio As Miharu.FileProvider.Library.Server.FolioStructure
    '    Dim Fecha = getFecha(nCargue)


    '    If GetVolumenEntidadProyecto(nCargue, nPaquete) Then

    '        If (Not Me.FileProviderLocal Is Nothing) Then
    '            Folio = GetFolioCache(Fecha, nCargue, nPaquete, nItem, nFolio)
    '        Else
    '            Folio = Me.Server.GetItem(nVolumen, sEntidad, sProyecto, Fecha, nCargue, nPaquete, nItem, nFolio)
    '        End If
    '    Else
    '        If (Not Me.FileProviderLocal Is Nothing) Then
    '            Folio = GetFolioCache(Fecha, nCargue, nPaquete, nItem, nFolio)
    '        Else
    '            Folio = Me.Server.GetItem(Fecha, nCargue, nPaquete, nItem, nFolio)
    '        End If
    '    End If

    '    nImagen = Folio.Image_Binary
    '    nThumbnail = Folio.Thumbnail_Binary

    '    Return True
    'End Function

    'Public Function GetFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte) As Boolean
    '    ValidateConnection(False)

    '    Dim Fecha = getFecha(nExpediente, nFolder)
    '    If (Not Me.FileProviderLocal Is Nothing) Then
    '        Dim Folio = GetFolioCache(Fecha, nExpediente, nFolder, nFile, nVersion, nFolio)

    '        nImagen = Folio.Image_Binary
    '        nThumbnail = Folio.Thumbnail_Binary
    '    Else
    '        Dim Folio = Me.Server.GetItem(Fecha, nExpediente, nFolder, nFile, nVersion, nFolio)

    '        nImagen = Folio.Image_Binary
    '        nThumbnail = Folio.Thumbnail_Binary
    '    End If

    '    Return True
    'End Function

    Public Function GetFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte) As Boolean
        ValidateConnection(False)

        Dim Ruta = getRuta(nExpediente, nFolder, nFile, nVersion)
        If (Not Me.FileProviderLocal Is Nothing) Then
            'Dim Folio = GetFolioCache(Ruta, nExpediente, nFolder, nFile, nVersion, nFolio)
            Dim Folio As Miharu.FileProvider.Library.Server.FolioStructure

            Try
                Folio = GetFolioCache(Ruta, nExpediente, nFolder, nFile, nVersion, nFolio)
            Catch ex As Exception
                Folio = GetFolioCache(Replace(Ruta.Substring(0, 10), "\", ""), nExpediente, nFolder, nFile, nVersion, nFolio)
            End Try

            'If Folio.Image_Binary.LongLength = 0 Then
            '    Folio = GetFolioCache(Replace(Ruta.Substring(0, 10), "\", ""), nExpediente, nFolder, nFile, nVersion, nFolio)
            'End If

            nImagen = Folio.Image_Binary
            nThumbnail = Folio.Thumbnail_Binary
        Else
            'Dim Folio = Me.Server.GetItem(Ruta, nExpediente, nFolder, nFile, nVersion, nFolio)
            Dim Folio As Miharu.FileProvider.Library.Server.FolioStructure

            Try
                Folio = Me.Server.GetItem(Ruta, nExpediente, nFolder, nFile, nVersion, nFolio)
            Catch ex As Exception
                Folio = Me.Server.GetItem(Replace(Ruta.Substring(0, 10), "\", ""), nExpediente, nFolder, nFile, nVersion, nFolio)
            End Try

            'If Folio.Image_Binary.LongLength = 0 Then
            '    Folio = GetFolioCache(Replace(Ruta.Substring(0, 10), "\", ""), nExpediente, nFolder, nFile, nVersion, nFolio)
            'End If

            nImagen = Folio.Image_Binary
            nThumbnail = Folio.Thumbnail_Binary
        End If

        Return True
    End Function

    'Public Function GetFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte) As Boolean
    '    ValidateConnection(False)

    '    Dim Folio As Miharu.FileProvider.Library.Server.FolioStructure
    '    Dim Fecha = getFecha(nExpediente, nFolder)

    '    If GetVolumenEntidadProyecto(nExpediente, nFolder, nFile, nVersion) Then
    '        If (Not Me.FileProviderLocal Is Nothing) Then
    '            Folio = GetFolioCache(Fecha, nExpediente, nFolder, nFile, nVersion, nFolio)
    '        Else
    '            Folio = Me.Server.GetItem(nVolumen, sEntidad, sProyecto, Fecha, nExpediente, nFolder, nFile, nVersion, nFolio)
    '        End If
    '    Else
    '        If (Not Me.FileProviderLocal Is Nothing) Then
    '            Folio = GetFolioCache(Fecha, nExpediente, nFolder, nFile, nVersion, nFolio)
    '        Else
    '            Folio = Me.Server.GetItem(Fecha, nExpediente, nFolder, nFile, nVersion, nFolio)
    '        End If
    '    End If
    '    nImagen = Folio.Image_Binary
    '    nThumbnail = Folio.Thumbnail_Binary
    '    Return True
    'End Function


    'Public Function GetFolios(ByVal nAnexo As Long) As Short
    '    Return Me.Server.GetFolios(getFecha(nAnexo), nAnexo)
    'End Function

    Public Function GetFolios(ByVal nAnexo As Long) As Short
        Return Me.Server.GetFolios(getRuta(nAnexo), nAnexo)
    End Function

    'Public Function GetFolios(ByVal nAnexo As Long) As Short

    '    If GetVolumenEntidadProyecto(nAnexo) Then
    '        Return Me.Server.GetFolios(nVolumen, sEntidad, sProyecto, getFecha(nAnexo), nAnexo)
    '    Else
    '        Return Me.Server.GetFolios(getFecha(nAnexo), nAnexo)
    '    End If

    'End Function

    'Public Function GetFolios(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer) As Short
    '    Return Me.Server.GetFolios(getFecha(nCargue), nCargue, nPaquete, nItem)
    'End Function

    Public Function GetFolios(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer) As Short
        Return Me.Server.GetFolios(getRuta(nCargue), nCargue, nPaquete, nItem)
    End Function

    'Public Function GetFolios(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer) As Short

    '    If GetVolumenEntidadProyecto(nCargue, nPaquete) Then
    '        Return Me.Server.GetFolios(nVolumen, sEntidad, sProyecto, getFecha(nCargue), nCargue, nPaquete, nItem)
    '    Else
    '        Return Me.Server.GetFolios(getFecha(nCargue), nCargue, nPaquete, nItem)
    '    End If

    'End Function

    'Public Function GetFolios(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short) As Short
    '    Return Me.Server.GetFolios(getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion)
    'End Function

    Public Function GetFolios(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short) As Short
        Return Me.Server.GetFolios(getRuta(nExpediente, nFolder, nFile, nVersion), nExpediente, nFolder, nFile, nVersion)
    End Function

    'Public Function GetFolios(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short) As Short

    '    If GetVolumenEntidadProyecto(nExpediente, nFolder, nFile, nVersion) Then
    '        Return Me.Server.GetFolios(nVolumen, sEntidad, sProyecto, getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion)
    '    Else
    '        Return Me.Server.GetFolios(getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion)
    '    End If

    'End Function

    'Private Function GetFolios(ByVal nVolumen As Long, ByVal sEntidad As String, ByVal sProyecto As String, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short) As Short
    '    Return Me.Server.GetFolios(nVolumen, sEntidad, sProyecto, getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion)
    'End Function

    Private Function GetFolioCache(ByVal nFecha As String, ByVal nAnexo As Long, ByVal nFolio As Short) As FolioStructure
        Dim MsgError As String = ""
        Dim Folio As New FolioStructure
        Dim Respuesta As Boolean

        ' Validar que el Folio exista en cache local
        If (Me.FileProviderLocal.ExistCache(nAnexo, nFolio)) Then
            ' Recuperar el folio desde caché
            Respuesta = Me.FileProviderLocal.GetCache(nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al recuperar el folio
            If (Not Respuesta) Then Throw New Exception(MsgError)

            ' Validar si el folio existe en el cache alterno
        ElseIf (Me.FileProviderAlterno IsNot Nothing AndAlso Me.FileProviderAlterno.ExistCache(nAnexo, nFolio)) Then
            ' Recuperar el folio desde caché alterno
            Respuesta = Me.FileProviderAlterno.GetCache(nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al recuperar el folio
            If (Not Respuesta) Then Throw New Exception("Servidor alterno - " & MsgError)

            ' Crear una copia del archivo en el caché local
            Respuesta = Me.FileProviderLocal.CreateCache(nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al crear el folio
            If (Not Respuesta) Then Throw New Exception(MsgError)
        Else
            ' Recuperar el folio del servidor
            Folio = Me.Server.GetItem(nFecha, nAnexo, nFolio)
            ' Crear una copia del archivo en el caché local
            Respuesta = Me.FileProviderLocal.CreateCache(nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al crear el folio
            If (Not Respuesta) Then Throw New Exception(MsgError)

        End If

        Return Folio
    End Function



    'Private Function GetFolioCache(ByVal nFecha As String, ByVal nAnexo As Long, ByVal nFolio As Short) As FolioStructure
    '    Dim MsgError As String = ""
    '    Dim Folio As New FolioStructure
    '    Dim Respuesta As Boolean

    '    If GetVolumenEntidadProyecto(nAnexo) Then
    '        ' Validar que el Folio exista en cache local
    '        If (Me.FileProviderLocal.ExistCache(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio)) Then
    '            ' Recuperar el folio desde caché
    '            Respuesta = Me.FileProviderLocal.GetCache(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al recuperar el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)

    '            ' Validar si el folio existe en el cache alterno
    '        ElseIf (Me.FileProviderAlterno IsNot Nothing AndAlso Me.FileProviderAlterno.ExistCache(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio)) Then
    '            ' Recuperar el folio desde caché alterno
    '            Respuesta = Me.FileProviderAlterno.GetCache(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al recuperar el folio
    '            If (Not Respuesta) Then Throw New Exception("Servidor alterno - " & MsgError)

    '            ' Crear una copia del archivo en el caché local
    '            Respuesta = Me.FileProviderLocal.CreateCache(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al crear el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)
    '        Else
    '            ' Recuperar el folio del servidor
    '            Folio = Me.Server.GetItem(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio)
    '            ' Crear una copia del archivo en el caché local
    '            Respuesta = Me.FileProviderLocal.CreateCache(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al crear el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)

    '        End If

    '    Else
    '        ' Validar que el Folio exista en cache local
    '        If (Me.FileProviderLocal.ExistCache(nAnexo, nFolio)) Then
    '            ' Recuperar el folio desde caché
    '            Respuesta = Me.FileProviderLocal.GetCache(nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al recuperar el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)

    '            ' Validar si el folio existe en el cache alterno
    '        ElseIf (Me.FileProviderAlterno IsNot Nothing AndAlso Me.FileProviderAlterno.ExistCache(nAnexo, nFolio)) Then
    '            ' Recuperar el folio desde caché alterno
    '            Respuesta = Me.FileProviderAlterno.GetCache(nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al recuperar el folio
    '            If (Not Respuesta) Then Throw New Exception("Servidor alterno - " & MsgError)

    '            ' Crear una copia del archivo en el caché local
    '            Respuesta = Me.FileProviderLocal.CreateCache(nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al crear el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)
    '        Else
    '            ' Recuperar el folio del servidor
    '            Folio = Me.Server.GetItem(nFecha, nAnexo, nFolio)
    '            ' Crear una copia del archivo en el caché local
    '            Respuesta = Me.FileProviderLocal.CreateCache(nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al crear el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)

    '        End If

    '    End If

    '    Return Folio
    'End Function

    Private Function GetFolioCache(ByVal nFecha As String, ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short) As FolioStructure
        Dim MsgError As String = ""
        Dim Folio As New FolioStructure
        Dim Respuesta As Boolean

        ' Validar que el Folio exista en cache local
        If (Me.FileProviderLocal.ExistCache(nCargue, nPaquete, nItem, nFolio)) Then
            ' Recuperar el folio desde caché
            Respuesta = Me.FileProviderLocal.GetCache(nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al recuperar el folio
            If (Not Respuesta) Then Throw New Exception(MsgError)

            ' Validar si el folio existe en el cache alterno
        ElseIf (Me.FileProviderAlterno IsNot Nothing AndAlso Me.FileProviderAlterno.ExistCache(nCargue, nPaquete, nItem, nFolio)) Then
            ' Recuperar el folio desde caché alterno
            Respuesta = Me.FileProviderAlterno.GetCache(nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al recuperar el folio
            If (Not Respuesta) Then Throw New Exception("Servidor alterno - " & MsgError)

            ' Crear una copia del archivo en el caché local
            Respuesta = Me.FileProviderLocal.CreateCache(nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al crear el folio
            If (Not Respuesta) Then Throw New Exception(MsgError)
        Else
            ' Recuperar el folio del servidor
            Folio = Me.Server.GetItem(nFecha, nCargue, nPaquete, nItem, nFolio)
            ' Crear una copia del archivo en el caché local
            Respuesta = Me.FileProviderLocal.CreateCache(nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al crear el folio
            If (Not Respuesta) Then Throw New Exception(MsgError)

        End If

        Return Folio
    End Function

    'Private Function GetFolioCache(ByVal nFecha As String, ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short) As FolioStructure
    '    Dim MsgError As String = ""
    '    Dim Folio As New FolioStructure
    '    Dim Respuesta As Boolean


    '    If GetVolumenEntidadProyecto(nCargue, nPaquete) Then
    '        ' Validar que el Folio exista en cache local
    '        If (Me.FileProviderLocal.ExistCache(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio)) Then
    '            ' Recuperar el folio desde caché
    '            Respuesta = Me.FileProviderLocal.GetCache(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al recuperar el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)

    '            ' Validar si el folio existe en el cache alterno
    '        ElseIf (Me.FileProviderAlterno IsNot Nothing AndAlso Me.FileProviderAlterno.ExistCache(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio)) Then
    '            ' Recuperar el folio desde caché alterno
    '            Respuesta = Me.FileProviderAlterno.GetCache(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al recuperar el folio
    '            If (Not Respuesta) Then Throw New Exception("Servidor alterno - " & MsgError)

    '            ' Crear una copia del archivo en el caché local
    '            Respuesta = Me.FileProviderLocal.CreateCache(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al crear el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)
    '        Else
    '            ' Recuperar el folio del servidor
    '            Folio = Me.Server.GetItem(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio)
    '            ' Crear una copia del archivo en el caché local
    '            Respuesta = Me.FileProviderLocal.CreateCache(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al crear el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)

    '        End If

    '    Else
    '        ' Validar que el Folio exista en cache local
    '        If (Me.FileProviderLocal.ExistCache(nCargue, nPaquete, nItem, nFolio)) Then
    '            ' Recuperar el folio desde caché
    '            Respuesta = Me.FileProviderLocal.GetCache(nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al recuperar el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)

    '            ' Validar si el folio existe en el cache alterno
    '        ElseIf (Me.FileProviderAlterno IsNot Nothing AndAlso Me.FileProviderAlterno.ExistCache(nCargue, nPaquete, nItem, nFolio)) Then
    '            ' Recuperar el folio desde caché alterno
    '            Respuesta = Me.FileProviderAlterno.GetCache(nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al recuperar el folio
    '            If (Not Respuesta) Then Throw New Exception("Servidor alterno - " & MsgError)

    '            ' Crear una copia del archivo en el caché local
    '            Respuesta = Me.FileProviderLocal.CreateCache(nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al crear el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)
    '        Else
    '            ' Recuperar el folio del servidor
    '            Folio = Me.Server.GetItem(nFecha, nCargue, nPaquete, nItem, nFolio)
    '            ' Crear una copia del archivo en el caché local
    '            Respuesta = Me.FileProviderLocal.CreateCache(nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
    '            ' Informar si ocurrio un error al crear el folio
    '            If (Not Respuesta) Then Throw New Exception(MsgError)

    '        End If
    '    End If

    '    Return Folio
    'End Function

    Private Function GetFolioCache(ByVal nFecha As String, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short) As FolioStructure
        Dim MsgError As String = ""
        Dim Folio As New FolioStructure
        Dim Respuesta As Boolean

        ' Validar que el Folio exista en cache local
        If (Me.FileProviderLocal.ExistCache(nExpediente, nFolder, nFile, nVersion, nFolio)) Then
            ' Recuperar el folio desde caché
            Respuesta = Me.FileProviderLocal.GetCache(nExpediente, nFolder, nFile, nVersion, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al recuperar el folio
            If (Not Respuesta) Then Throw New Exception(MsgError)

            ' Validar si el folio existe en el cache alterno
        ElseIf (Me.FileProviderAlterno IsNot Nothing AndAlso Me.FileProviderAlterno.ExistCache(nExpediente, nFolder, nFile, nVersion, nFolio)) Then
            ' Recuperar el folio desde caché alterno
            Respuesta = Me.FileProviderAlterno.GetCache(nExpediente, nFolder, nFile, nVersion, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al recuperar el folio
            If (Not Respuesta) Then Throw New Exception("Servidor alterno - " & MsgError)

            ' Crear una copia del archivo en el caché local
            Respuesta = Me.FileProviderLocal.CreateCache(nExpediente, nFolder, nFile, nVersion, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al crear el folio
            If (Not Respuesta) Then Throw New Exception(MsgError)
        Else
            ' Recuperar el folio del servidor
            Folio = Me.Server.GetItem(nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)
            ' Crear una copia del archivo en el caché local
            Respuesta = Me.FileProviderLocal.CreateCache(nExpediente, nFolder, nFile, nVersion, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            ' Informar si ocurrio un error al crear el folio
            If (Not Respuesta) Then Throw New Exception(MsgError)

        End If

        Return Folio
    End Function

    'Public Function ExistFolio(ByVal nAnexo As Long, ByVal nFolio As Short) As Boolean
    '    Return Me.Server.ExistFolio(getFecha(nAnexo), nAnexo, nFolio)
    'End Function

    Public Function ExistFolio(ByVal nAnexo As Long, ByVal nFolio As Short) As Boolean
        Return Me.Server.ExistFolio(getRuta(nAnexo), nAnexo, nFolio)
    End Function

    'Public Function ExistFolio(ByVal nAnexo As Long, ByVal nFolio As Short) As Boolean

    '    If GetVolumenEntidadProyecto(nAnexo) Then
    '        Return Me.Server.ExistFolio(nVolumen, sEntidad, sProyecto, getFecha(nAnexo), nAnexo, nFolio)
    '    Else
    '        Return Me.Server.ExistFolio(getFecha(nAnexo), nAnexo, nFolio)
    '    End If

    'End Function

    'Public Function ExistFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short) As Boolean
    '    Return Me.Server.ExistFolio(getFecha(nCargue), nCargue, nPaquete, nItem, nFolio)
    'End Function

    Public Function ExistFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short) As Boolean
        Return Me.Server.ExistFolio(getRuta(nCargue), nCargue, nPaquete, nItem, nFolio)
    End Function

    'Public Function ExistFolio(ByVal nCargue As Integer, ByVal nPaquete As Short, ByVal nItem As Integer, ByVal nFolio As Short) As Boolean

    '    If GetVolumenEntidadProyecto(nCargue, nPaquete) Then
    '        Return Me.Server.ExistFolio(nVolumen, sEntidad, sProyecto, getFecha(nCargue), nCargue, nPaquete, nItem, nFolio)
    '    Else
    '        Return Me.Server.ExistFolio(getFecha(nCargue), nCargue, nPaquete, nItem, nFolio)
    '    End If

    'End Function

    'Public Function ExistFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short) As Boolean
    '    Return Me.Server.ExistFolio(getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolio)
    'End Function

    Public Function ExistFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short) As Boolean
        Return Me.Server.ExistFolio(getRuta(nExpediente, nFolder, nFile, nVersion), nExpediente, nFolder, nFile, nVersion, nFolio)
    End Function

    'Public Function ExistFolio(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short) As Boolean

    '    If GetVolumenEntidadProyecto(nExpediente, nFolder, nFile, nVersion) Then
    '        Return Me.Server.ExistFolio(nVolumen, sEntidad, sProyecto, getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolio)
    '    Else
    '        Return Me.Server.ExistFolio(getFecha(nExpediente, nFolder), nExpediente, nFolder, nFile, nVersion, nFolio)
    '    End If

    'End Function

    'Private Function getFecha(ByVal nAnexo As Long) As String
    '    If (Me._Anexo <> nAnexo) Then
    '        Dim AnexoDataTable = dbmImaging.SchemaCore.CTA_Imaging_Anexo.DBFindByid_Anexo(nAnexo)
    '        If (AnexoDataTable.Count = 0) Then Throw New Exception("No se encontró el Anexo")

    '        Me._Anexo = nAnexo
    '        Me._Fecha = AnexoDataTable(0).Fecha_Creacion.ToString("yyyyMMdd")
    '    End If
    '    Return Me._Fecha

    'End Function

    Private Function getRuta(ByVal nAnexo As Long) As String
        If (Me._Anexo <> nAnexo) Then
            Dim AnexoDataTable = dbmImaging.SchemaCore.CTA_Imaging_Anexo_RF.DBFindByid_Anexo(nAnexo)
            If (AnexoDataTable.Count = 0) Then Throw New Exception("No se encontró el Anexo")

            Me._Anexo = nAnexo
            Me._Fecha = AnexoDataTable(0).Fecha_Creacion.ToString("yyyyMMdd")
            Me._Entidad = AnexoDataTable(0).fk_Entidad
            Me._Proyecto = AnexoDataTable(0).fk_Proyecto
        End If
        Return Me._Fecha.Substring(0, 4) & "\" & Me._Fecha.Substring(4, 2) & "\" & Me._Fecha.Substring(6, 2) & "\E" & Me._Entidad & "\P" & Me._Proyecto

    End Function

    'Private Function getFecha(ByVal nCargue As Integer) As String
    '    If (Me._Cargue <> nCargue) Then
    '        Dim CargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(nCargue)
    '        If (CargueDataTable.Count = 0) Then Throw New Exception("No se encontró el Cargue")

    '        Me._Cargue = nCargue
    '        Me._Fecha = CargueDataTable(0).Fecha_Cargue.ToString("yyyyMMdd")
    '    End If
    '    Return Me._Fecha
    'End Function

    Private Function getRuta(ByVal nCargue As Integer) As String
        If (Me._Cargue <> nCargue) Then
            Dim CargueDataTable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(nCargue)
            If (CargueDataTable.Count = 0) Then Throw New Exception("No se encontró el Cargue")

            Me._Cargue = nCargue
            Me._Fecha = CargueDataTable(0).Fecha_Cargue.ToString("yyyyMMdd")
            Me._Entidad = CargueDataTable(0).fk_Entidad
            Me._Proyecto = CargueDataTable(0).fk_Proyecto
        End If
        Return Me._Fecha.Substring(0, 4) & "\" & Me._Fecha.Substring(4, 2) & "\" & Me._Fecha.Substring(6, 2) & "\E" & Me._Entidad & "\P" & Me._Proyecto
    End Function

    'Private Function getFecha(ByVal nExpediente As Long, ByVal nFolder As Short) As String
    '    If (Me._Expediente <> nExpediente Or Me._Folder <> nFolder) Then
    '        Dim FolderDataTable = dbmImaging.SchemaCore.CTA_Folder.DBFindByfk_Expedientefk_Folder(nExpediente, nFolder)
    '        If (FolderDataTable.Count = 0) Then Throw New Exception("No se encontró el Folder")

    '        Me._Expediente = nExpediente
    '        Me._Folder = nFolder
    '        Me._Fecha = FolderDataTable(0).Fecha_Creacion.ToString("yyyyMMdd")
    '    End If
    '    Return Me._Fecha
    'End Function


    Private Function getRuta(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short) As String
        If (Me._Expediente <> nExpediente Or Me._Folder <> nFolder Or Me._File <> nFile Or Me._Version <> nVersion) Then
            Dim FileDataTable = dbmImaging.SchemaCore.CTA_File_RF.DBFindByfk_Expedientefk_Folderfk_Fileid_Version(nExpediente, nFolder, nFile, nVersion)
            If (FileDataTable.Count = 0) Then Throw New Exception("No se encontró el File - Versión")

            Me._Expediente = nExpediente
            Me._Folder = nFolder
            Me._File = nFile
            Me._Version = nVersion
            Me._Fecha = FileDataTable(0).Fecha_Creacion.ToString("yyyyMMdd")
            Me._Entidad = FileDataTable(0).fk_Entidad
            Me._Proyecto = FileDataTable(0).fk_Proyecto
        End If
        Return Me._Fecha.Substring(0, 4) & "\" & Me._Fecha.Substring(4, 2) & "\" & Me._Fecha.Substring(6, 2) & "\E" & Me._Entidad & "\P" & Me._Proyecto
    End Function
#End Region

End Class