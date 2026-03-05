Imports System
Imports System.IO
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Lifetime

Public Class FileProvider
    Inherits MarshalByRefObject

#Region " Declaraciones "

    Private _WorkingFolder As String

#End Region

#Region " Propiedades "

    Public ReadOnly Property InstanceHash() As Integer
        Get
            Return Me.GetHashCode()
        End Get
    End Property

    Public ReadOnly Property WorkingFolder As String
        Get
            Return _WorkingFolder
        End Get
    End Property


#End Region

#Region " Constructores "

    Public Sub New(nWorkingFolder As String)
        InitializeLifetimeService()

        _WorkingFolder = nWorkingFolder.TrimEnd("\"c) & "\"c
    End Sub

    Public Sub New()
        InitializeLifetimeService()
    End Sub

#End Region

#Region " Metodos "

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Shared Sub WriteFiles(nFileName As String, nImagen() As Byte, nThumbnail() As Byte)
        WriteFile(nFileName & ".image", nImagen)
        WriteFile(nFileName & ".thumbnail", nThumbnail)
    End Sub

    Private Shared Sub WriteFile(nFileName As String, nData() As Byte)
        Try
            Dim Directorio = Path.GetDirectoryName(nFileName)

            If (Not Directory.Exists(Directorio)) Then
                Directory.CreateDirectory(Directorio)
            End If

            Using fsOutput As New FileStream(nFileName, FileMode.Create, FileAccess.Write)

                fsOutput.Write(nData, 0, nData.Length)
                fsOutput.Close()
            End Using
        Catch ex As Exception
            Throw New Exception("FileProvider.WriteFile: Error al escribir la imagen " & nFileName & ", " & ex.Message)
        End Try
    End Sub

    Private Shared Sub CopyFile(nFileNameOrigen As String, nFileNameDestino As String)
        Try
            Dim Directorio = Path.GetDirectoryName(nFileNameDestino)

            If (Not Directory.Exists(Directorio)) Then
                Directory.CreateDirectory(Directorio)
            End If

            File.Copy(nFileNameOrigen, nFileNameDestino)
        Catch ex As Exception
            Throw New Exception("FileProvider.WriteFile: Error al copiar la imagen " & nFileNameOrigen & " a " & nFileNameDestino & "," & ex.Message)
        End Try
    End Sub

    Private Shared Sub DeleteFile(nDirectoryName As String, nHeadName As String)
        If (Directory.Exists(nDirectoryName)) Then
            Dim Files = Directory.GetFiles(nDirectoryName, nHeadName & "*")

            For Each fileName As String In Files
                File.Delete(fileName)
            Next

            If (Directory.GetFiles(nDirectoryName).Length = 0) Then
                Directory.Delete(nDirectoryName)
            End If
        End If
    End Sub

    'Sub DeleteCargue(nFecha As String, nCargue As Integer)
    '    Const Paquete As Short = 0
    '    Dim DirectoryName = getDirectoryName(nFecha, nCargue, Paquete)

    '    If (Directory.Exists(DirectoryName)) Then Directory.Delete(DirectoryName, True)
    'End Sub

    Sub DeleteCargue(nWorkingFolder As String, nFecha As String, nCargue As Integer)
        Const Paquete As Short = 0
        Dim DirectoryName = getDirectoryName(nWorkingFolder, nFecha, nCargue, Paquete)

        If (Directory.Exists(DirectoryName)) Then Directory.Delete(DirectoryName, True)
    End Sub

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Sub DeleteCargue(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer)
    '    Const Paquete As Short = 0
    '    Dim DirectoryName = getDirectoryName(nVolumen, sEntidad, sProyecto, nFecha, nCargue, Paquete)

    '    If (Directory.Exists(DirectoryName)) Then Directory.Delete(DirectoryName, True)
    'End Sub

    'Sub DeleteExpediente(nFecha As String, nExpediente As Long)
    '    Const Folder As Short = 0
    '    Dim DirectoryName = getDirectoryName(nFecha, nExpediente, Folder)
    '    If (Directory.Exists(DirectoryName)) Then Directory.Delete(DirectoryName, True)
    'End Sub

    Sub DeleteExpediente(nWorkingFolder As String, nFecha As String, nExpediente As Long)
        Const Folder As Short = 0
        Dim DirectoryName = getDirectoryName(nWorkingFolder, nFecha, nExpediente, Folder)
        If (Directory.Exists(DirectoryName)) Then Directory.Delete(DirectoryName, True)
    End Sub

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Sub DeleteExpediente(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long)
    '    Const Folder As Short = 0
    '    Dim DirectoryName = getDirectoryName(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, Folder)
    '    If (Directory.Exists(DirectoryName)) Then Directory.Delete(DirectoryName, True)
    'End Sub

#End Region

#Region " Funciones "

    Public Overrides Function InitializeLifetimeService() As Object
        Dim lease As ILease = CType(MyBase.InitializeLifetimeService(), ILease)

        If lease.CurrentState = LeaseState.Initial Then
            lease.InitialLeaseTime = TimeSpan.Zero
        End If

        Return lease
    End Function


    'Public Function CreateFile(nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getFileName(nFecha, nAnexo, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function CreateFile(nWorkinFolder As String, nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        Try
            WriteFiles(getFileName(nWorkinFolder, nFecha, nAnexo, nFolio), nImagen, nThumbnail)

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.CreateFile: " & ex.Message
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function CreateFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getFileName(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function CreateFile(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getFileName(nFecha, nCargue, nPaquete, nItem, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function CreateFile(nWorkinFolder As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        Try
            WriteFiles(getFileName(nWorkinFolder, nFecha, nCargue, nPaquete, nItem, nFolio), nImagen, nThumbnail)

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.CreateFile: " & ex.Message
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function CreateFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getFileName(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function CreateFile(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getFileName(nFecha, nExpediente, nFolder, nFile, nVersion, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function CreateFile(nWorkinFolder As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        Try
            WriteFiles(getFileName(nWorkinFolder, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio), nImagen, nThumbnail)

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.CreateFile: " & ex.Message
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function CreateFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getFileName(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function CreateCache(nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        Try
            WriteFiles(getCacheName(nAnexo, nFolio), nImagen, nThumbnail)

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.CreateCache: " & ex.Message
            Return False
        End Try
    End Function

    'Public Function CreateCache(nWorkinFolder As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getCacheName(nWorkinFolder, nAnexo, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateCache: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function CreateCache(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getCacheName(nVolumen, sEntidad, sProyecto, nAnexo, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateCache: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function CreateCache(nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        Try
            WriteFiles(getCacheName(nCargue, nPaquete, nItem, nFolio), nImagen, nThumbnail)

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.CreateCache: " & ex.Message
            Return False
        End Try
    End Function

    'Public Function CreateCache(nWorkinFolder As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getCacheName(nWorkinFolder, nCargue, nPaquete, nItem, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateCache: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function CreateCache(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getCacheName(nVolumen, sEntidad, sProyecto, nCargue, nPaquete, nItem, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateCache: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function CreateCache(nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        Try
            WriteFiles(getCacheName(nExpediente, nFolder, nFile, nVersion, nFolio), nImagen, nThumbnail)

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.CreateCache: " & ex.Message
            Return False
        End Try
    End Function

    'Public Function CreateCache(nWorkinFolder As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getCacheName(nWorkinFolder, nExpediente, nFolder, nFile, nVersion, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateCache: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function CreateCache(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    Try
    '        WriteFiles(getCacheName(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio), nImagen, nThumbnail)

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.CreateCache: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function MoveFile(nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short, ByRef MsgError As String) As Boolean
    '    Try
    '        Dim ItemName = getFileName(nFechaItem, nCargue, nPaquete, nItem, nFolioItem)
    '        Dim FileName = getFileName(nFechaFile, nExpediente, nFolder, nFile, nVersion, nFolioFile) & ".thumbnail"

    '        CopyFile(ItemName & ".image", FileName & ".image")
    '        CopyFile(ItemName & ".thumbnail", FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.MoveFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function MoveFile(nWorkinFolder As String, nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short, ByRef MsgError As String) As Boolean
        Try
            Dim ItemName = getFileName(nWorkinFolder, nFechaItem, nCargue, nPaquete, nItem, nFolioItem)
            Dim FileName = getFileName(nWorkinFolder, nFechaFile, nExpediente, nFolder, nFile, nVersion, nFolioFile) & ".thumbnail"

            CopyFile(ItemName & ".image", FileName & ".image")
            CopyFile(ItemName & ".thumbnail", FileName & ".thumbnail")

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.MoveFile: " & ex.Message
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function MoveFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short, ByRef MsgError As String) As Boolean
    '    Try
    '        Dim ItemName = getFileName(nVolumen, sEntidad, sProyecto, nFechaItem, nCargue, nPaquete, nItem, nFolioItem)
    '        Dim FileName = getFileName(nVolumen, sEntidad, sProyecto, nFechaFile, nExpediente, nFolder, nFile, nVersion, nFolioFile) & ".thumbnail"

    '        CopyFile(ItemName & ".image", FileName & ".image")
    '        CopyFile(ItemName & ".thumbnail", FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.MoveFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function MoveFile(nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaAnexo As String, nAnexo As Long, nFolioAnexo As Short, ByRef MsgError As String) As Boolean
    '    Try
    '        Dim ItemName = getFileName(nFechaItem, nCargue, nPaquete, nItem, nFolioItem)
    '        Dim FileName = getFileName(nFechaAnexo, nAnexo, nFolioAnexo) & ".thumbnail"

    '        FileCopy(ItemName & ".image", FileName & ".image")
    '        FileCopy(ItemName & ".thumbnail", FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.MoveFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function MoveFile(nWorkinFolder As String, nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaAnexo As String, nAnexo As Long, nFolioAnexo As Short, ByRef MsgError As String) As Boolean
        Try
            Dim ItemName = getFileName(nWorkinFolder, nFechaItem, nCargue, nPaquete, nItem, nFolioItem)
            Dim FileName = getFileName(nWorkinFolder, nFechaAnexo, nAnexo, nFolioAnexo) & ".thumbnail"

            FileCopy(ItemName & ".image", FileName & ".image")
            FileCopy(ItemName & ".thumbnail", FileName & ".thumbnail")

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.MoveFile: " & ex.Message
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function MoveFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaAnexo As String, nAnexo As Long, nFolioAnexo As Short, ByRef MsgError As String) As Boolean
    '    Try
    '        Dim ItemName = getFileName(nVolumen, sEntidad, sProyecto, nFechaItem, nCargue, nPaquete, nItem, nFolioItem)
    '        Dim FileName = getFileName(nVolumen, sEntidad, sProyecto, nFechaAnexo, nAnexo, nFolioAnexo) & ".thumbnail"

    '        FileCopy(ItemName & ".image", FileName & ".image")
    '        FileCopy(ItemName & ".thumbnail", FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.MoveFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function MoveCache(nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short, ByRef MsgError As String) As Boolean
        Try
            Dim ItemName = getCacheName(nCargue, nPaquete, nItem, nFolioItem)
            Dim FileName = getCacheName(nExpediente, nFolder, nFile, nVersion, nFolioFile) & ".thumbnail"

            FileCopy(ItemName & ".image", FileName & ".image")
            FileCopy(ItemName & ".thumbnail", FileName & ".thumbnail")

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.MoveCache: " & ex.Message
            Return False
        End Try
    End Function

    'Public Function MoveCache(nWorkinFolder As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short, ByRef MsgError As String) As Boolean
    '    Try
    '        Dim ItemName = getCacheName(nWorkinFolder, nCargue, nPaquete, nItem, nFolioItem)
    '        Dim FileName = getCacheName(nWorkinFolder, nExpediente, nFolder, nFile, nVersion, nFolioFile) & ".thumbnail"

    '        FileCopy(ItemName & ".image", FileName & ".image")
    '        FileCopy(ItemName & ".thumbnail", FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.MoveCache: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function MoveCache(nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nAnexo As Long, nFolioAnexo As Short, ByRef MsgError As String) As Boolean
        Try
            Dim ItemName = getCacheName(nCargue, nPaquete, nItem, nFolioItem)
            Dim FileName = getCacheName(nAnexo, nFolioAnexo) & ".thumbnail"

            FileCopy(ItemName & ".image", FileName & ".image")
            FileCopy(ItemName & ".thumbnail", FileName & ".thumbnail")

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.MoveCache: " & ex.Message
            Return False
        End Try
    End Function

    'Public Function MoveCache(nWorkinFolder As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nAnexo As Long, nFolioAnexo As Short, ByRef MsgError As String) As Boolean
    '    Try
    '        Dim ItemName = getCacheName(nWorkinFolder, nCargue, nPaquete, nItem, nFolioItem)
    '        Dim FileName = getCacheName(nWorkinFolder, nAnexo, nFolioAnexo) & ".thumbnail"

    '        FileCopy(ItemName & ".image", FileName & ".image")
    '        FileCopy(ItemName & ".thumbnail", FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.MoveCache: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function MoveCache(nVolumen As Long, sEntidad As String, sProyecto As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nAnexo As Long, nFolioAnexo As Short, ByRef MsgError As String) As Boolean
    '    Try
    '        Dim ItemName = getCacheName(nVolumen, sEntidad, sProyecto, nCargue, nPaquete, nItem, nFolioItem)
    '        Dim FileName = getCacheName(nVolumen, sEntidad, sProyecto, nAnexo, nFolioAnexo) & ".thumbnail"

    '        FileCopy(ItemName & ".image", FileName & ".image")
    '        FileCopy(ItemName & ".thumbnail", FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.MoveCache: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function GetFile(nFecha As String, nAnexo As Long, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getFileName(nFecha, nAnexo, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function GetFile(nWorkinFolder As String, nFecha As String, nAnexo As Long, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        nImagen = Nothing
        nThumbnail = Nothing

        Try
            Dim FileName = getFileName(nWorkinFolder, nFecha, nAnexo, nFolio)
            Dim Ruta = FileName.Substring(0, FileName.LastIndexOf("\"))

            If Not (Directory.Exists(Ruta)) Then
                Ruta = Replace(nFecha.Substring(0, 10), "\", "")
                If (Directory.Exists(Ruta)) Then
                    FileName = getFileName(nWorkinFolder, nFecha, nAnexo, nFolio)
                End If
            End If

            nImagen = GetFile(FileName & ".image")
            nThumbnail = GetFile(FileName & ".thumbnail")

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.GetFile: " & ex.Message
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function GetFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getFileName(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function GetFile(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getFileName(nFecha, nCargue, nPaquete, nItem, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function GetFile(nWorkinFolder As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        nImagen = Nothing
        nThumbnail = Nothing

        Try
            Dim FileName = getFileName(nWorkinFolder, nFecha, nCargue, nPaquete, nItem, nFolio)

            nImagen = GetFile(FileName & ".image")
            nThumbnail = GetFile(FileName & ".thumbnail")

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.GetFile: " & ex.Message
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function GetFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getFileName(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function GetFile(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getFileName(nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)
    '        Dim Ruta = FileName.Substring(0, FileName.LastIndexOf("\"))

    '        If Not (Directory.Exists(Ruta)) Then
    '            Ruta = Replace(nFecha.Substring(0, 10), "\", "")
    '            If (Directory.Exists(Ruta)) Then
    '                FileName = getFileName(Ruta, nExpediente, nFolder, nFile, nVersion, nFolio)
    '            End If
    '        End If

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFile: " & ex.ToString()
    '        Return False
    '    End Try
    'End Function

    Public Function GetFile(nWorkinFolder As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        nImagen = Nothing
        nThumbnail = Nothing

        Try
            Dim FileName = getFileName(nWorkinFolder, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)
            Dim Ruta = FileName.Substring(0, FileName.LastIndexOf("\"))

            If Not (Directory.Exists(Ruta)) Then
                Ruta = Replace(nFecha.Substring(0, 10), "\", "")
                If (Directory.Exists(Ruta)) Then
                    FileName = getFileName(nWorkinFolder, Ruta, nExpediente, nFolder, nFile, nVersion, nFolio)
                End If
            End If

            nImagen = GetFile(FileName & ".image")
            nThumbnail = GetFile(FileName & ".thumbnail")

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.GetFile: " & ex.ToString()
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function GetFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getFileName(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function DeleteFile(nFecha As String, nAnexo As Long, ByRef MsgError As String) As Boolean
    '    Try
    '        DeleteFile(getDirectoryName(nFecha, nAnexo), getHeadName(nAnexo))

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.DeleteFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function DeleteFile(nRuta As String, nAnexo As Long, ByRef MsgError As String) As Boolean
    '    Try
    '        DeleteFile(getDirectoryName(nRuta, nAnexo), getHeadName(nAnexo))

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.DeleteFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function DeleteFile(nWorkingFolder As String, nRuta As String, nAnexo As Long, ByRef MsgError As String) As Boolean
        Try
            DeleteFile(getDirectoryName(nWorkingFolder, nRuta, nAnexo), getHeadName(nAnexo))

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.DeleteFile: " & ex.Message
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function DeleteFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, ByRef MsgError As String) As Boolean
    '    Try
    '        DeleteFile(getDirectoryName(nVolumen, sEntidad, sProyecto, nFecha, nAnexo), getHeadName(nAnexo))

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.DeleteFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function DeleteFile(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, ByRef MsgError As String) As Boolean
    '    Try
    '        DeleteFile(getDirectoryName(nFecha, nCargue, nPaquete), getHeadName(nCargue, nPaquete, nItem))

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.DeleteFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function DeleteFile(nWorkingFolder As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, ByRef MsgError As String) As Boolean
        Try
            DeleteFile(getDirectoryName(nWorkingFolder, nFecha, nCargue, nPaquete), getHeadName(nCargue, nPaquete, nItem))

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.DeleteFile: " & ex.Message
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function DeleteFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, ByRef MsgError As String) As Boolean
    '    Try
    '        DeleteFile(getDirectoryName(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete), getHeadName(nCargue, nPaquete, nItem))

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.DeleteFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function DeleteFile(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, ByRef MsgError As String) As Boolean
    '    Try
    '        DeleteFile(getDirectoryName(nFecha, nExpediente, nFolder), getHeadName(nExpediente, nFolder, nFile, nVersion))

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.DeleteFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function DeleteFile(nWorkingFolder As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, ByRef MsgError As String) As Boolean
        Try
            DeleteFile(getDirectoryName(nWorkingFolder, nFecha, nExpediente, nFolder), getHeadName(nExpediente, nFolder, nFile, nVersion))

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.DeleteFile: " & ex.Message
            Return False
        End Try
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function DeleteFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, ByRef MsgError As String) As Boolean
    '    Try
    '        DeleteFile(getDirectoryName(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder), getHeadName(nExpediente, nFolder, nFile, nVersion))

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.DeleteFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Function GetFolios(nFecha As String, nAnexo As Long) As Short
    '    Dim DirectoryName = getDirectoryName(nFecha, nAnexo)

    '    If (Directory.Exists(DirectoryName)) Then
    '        Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nAnexo) & "*.image").Length)
    '    End If

    '    Return 0
    'End Function

    'Function GetFolios(nRuta As String, nAnexo As Long) As Short
    '    Dim DirectoryName = getDirectoryName(nRuta, nAnexo)
    '    Dim Folios As Short = 0

    '    'If (Directory.Exists(DirectoryName)) Then
    '    '    Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nAnexo) & "*.image").Length)
    '    'End If

    '    If (Directory.Exists(DirectoryName)) Then
    '        Folios = CShort(Directory.GetFiles(DirectoryName, getHeadName(nAnexo) & "*.image").Length)
    '    End If

    '    If Folios = 0 Then
    '        DirectoryName = getDirectoryName(Replace(nRuta.Substring(0, 10), "\", ""), nAnexo)
    '        If (Directory.Exists(DirectoryName)) Then
    '            Folios = CShort(Directory.GetFiles(DirectoryName, getHeadName(nAnexo) & "*.image").Length)
    '        End If
    '    End If

    '    'Return 0
    '    Return Folios
    'End Function

    Function GetFolios(nWorkingFolder As String, nRuta As String, nAnexo As Long) As Short
        Dim DirectoryName = getDirectoryName(nWorkingFolder, nRuta, nAnexo)
        Dim Folios As Short = 0

        'If (Directory.Exists(DirectoryName)) Then
        '    Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nAnexo) & "*.image").Length)
        'End If

        If (Directory.Exists(DirectoryName)) Then
            Folios = CShort(Directory.GetFiles(DirectoryName, getHeadName(nAnexo) & "*.image").Length)
        End If

        If Folios = 0 Then
            DirectoryName = getDirectoryName(nWorkingFolder, Replace(nRuta.Substring(0, 10), "\", ""), nAnexo)
            If (Directory.Exists(DirectoryName)) Then
                Folios = CShort(Directory.GetFiles(DirectoryName, getHeadName(nAnexo) & "*.image").Length)
            End If
        End If

        'Return 0
        Return Folios
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long) As Short
    '    Dim DirectoryName = getDirectoryName(nVolumen, sEntidad, sProyecto, nFecha, nAnexo)

    '    If (Directory.Exists(DirectoryName)) Then
    '        Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nAnexo) & "*.image").Length)
    '    End If

    '    Return 0
    'End Function

    'Function GetFolios(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short
    '    Dim DirectoryName = getDirectoryName(nFecha, nCargue, nPaquete)

    '    If (Directory.Exists(DirectoryName)) Then
    '        Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nCargue, nPaquete, nItem) & "*.image").Length)
    '    End If

    '    Return 0
    'End Function

    'Function GetFolios(nRuta As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short
    '    Dim DirectoryName = getDirectoryName(nRuta, nCargue, nPaquete)

    '    If (Directory.Exists(DirectoryName)) Then
    '        Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nCargue, nPaquete, nItem) & "*.image").Length)
    '    End If

    '    Return 0
    'End Function

    Function GetFolios(nWorkingFolder As String, nRuta As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short
        Dim DirectoryName = getDirectoryName(nWorkingFolder, nRuta, nCargue, nPaquete)

        If (Directory.Exists(DirectoryName)) Then
            Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nCargue, nPaquete, nItem) & "*.image").Length)
        End If

        Return 0
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short
    '    Dim DirectoryName = getDirectoryName(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete)

    '    If (Directory.Exists(DirectoryName)) Then
    '        Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nCargue, nPaquete, nItem) & "*.image").Length)
    '    End If

    '    Return 0
    'End Function

    'Function GetFolios(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As Short
    '    Dim DirectoryName = getDirectoryName(nFecha, nExpediente, nFolder)

    '    If (Directory.Exists(DirectoryName)) Then
    '        Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nExpediente, nFolder, nFile, nVersion) & "*.image").Length)
    '    End If

    '    Return 0
    'End Function

    'Function GetFolios(nRuta As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As Short
    '    Dim DirectoryName = getDirectoryName(nRuta, nExpediente, nFolder)
    '    Dim Folios As Short = 0

    '    'If (Directory.Exists(DirectoryName)) Then
    '    '    Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nExpediente, nFolder, nFile, nVersion) & "*.image").Length)
    '    'End If

    '    If (Directory.Exists(DirectoryName)) Then
    '        Folios = CShort(Directory.GetFiles(DirectoryName, getHeadName(nExpediente, nFolder, nFile, nVersion) & "*.image").Length)
    '    End If

    '    If Folios = 0 Then
    '        DirectoryName = getDirectoryName(Replace(nRuta.Substring(0, 10), "\", ""), nExpediente, nFolder)
    '        If (Directory.Exists(DirectoryName)) Then
    '            Folios = CShort(Directory.GetFiles(DirectoryName, getHeadName(nExpediente, nFolder, nFile, nVersion) & "*.image").Length)
    '        End If
    '    End If

    '    'Return 0
    '    Return Folios
    'End Function

    Function GetFolios(nWorkingFolder As String, nRuta As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As Short
        Dim DirectoryName = getDirectoryName(nWorkingFolder, nRuta, nExpediente, nFolder)
        Dim Folios As Short = 0

        'If (Directory.Exists(DirectoryName)) Then
        '    Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nExpediente, nFolder, nFile, nVersion) & "*.image").Length)
        'End If

        If (Directory.Exists(DirectoryName)) Then
            Folios = CShort(Directory.GetFiles(DirectoryName, getHeadName(nExpediente, nFolder, nFile, nVersion) & "*.image").Length)
        End If

        If Folios = 0 Then
            DirectoryName = getDirectoryName(nWorkingFolder, Replace(nRuta.Substring(0, 10), "\", ""), nExpediente, nFolder)
            If (Directory.Exists(DirectoryName)) Then
                Folios = CShort(Directory.GetFiles(DirectoryName, getHeadName(nExpediente, nFolder, nFile, nVersion) & "*.image").Length)
            End If
        End If

        'Return 0
        Return Folios
    End Function



    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As Short
    '    Dim DirectoryName = getDirectoryName(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder)

    '    If (Directory.Exists(DirectoryName)) Then
    '        Return CShort(Directory.GetFiles(DirectoryName, getHeadName(nExpediente, nFolder, nFile, nVersion) & "*.image").Length)
    '    End If

    '    Return 0
    'End Function

    Public Function GetCache(nAnexo As Long, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        nImagen = Nothing
        nThumbnail = Nothing

        Try
            Dim FileName = getCacheName(nAnexo, nFolio)

            nImagen = GetFile(FileName & ".image")
            nThumbnail = GetFile(FileName & ".thumbnail")

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.GetFile: " & ex.Message
            Return False
        End Try
    End Function

    'Public Function GetCache(nWorkinFolder As String, nAnexo As Long, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getCacheName(nWorkinFolder, nAnexo, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function GetCache(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getCacheName(nVolumen, sEntidad, sProyecto, nAnexo, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function GetCache(nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        nImagen = Nothing
        nThumbnail = Nothing

        Try
            Dim FileName = getCacheName(nCargue, nPaquete, nItem, nFolio)

            nImagen = GetFile(FileName & ".image")
            nThumbnail = GetFile(FileName & ".thumbnail")

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.GetFile: " & ex.Message
            Return False
        End Try
    End Function

    'Public Function GetCache(nWorkinFolder As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getCacheName(nWorkinFolder, nCargue, nPaquete, nItem, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function GetCache(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getCacheName(nVolumen, sEntidad, sProyecto, nCargue, nPaquete, nItem, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFile: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    Public Function GetCache(nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
        nImagen = Nothing
        nThumbnail = Nothing

        Try
            Dim FileName = getCacheName(nExpediente, nFolder, nFile, nVersion, nFolio)

            nImagen = GetFile(FileName & ".image")
            nThumbnail = GetFile(FileName & ".thumbnail")

            Return True
        Catch ex As Exception
            MsgError = "FileProvider.GetFolio: " & ex.Message
            Return False
        End Try
    End Function

    'Public Function GetCache(nWorkinFolder As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getCacheName(nWorkinFolder, nExpediente, nFolder, nFile, nVersion, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFolio: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function GetCache(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, ByRef nImagen() As Byte, ByRef nThumbnail() As Byte, ByRef MsgError As String) As Boolean
    '    nImagen = Nothing
    '    nThumbnail = Nothing

    '    Try
    '        Dim FileName = getCacheName(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)

    '        nImagen = GetFile(FileName & ".image")
    '        nThumbnail = GetFile(FileName & ".thumbnail")

    '        Return True
    '    Catch ex As Exception
    '        MsgError = "FileProvider.GetFolio: " & ex.Message
    '        Return False
    '    End Try
    'End Function

    'Public Function ExistFile(nFecha As String, nAnexo As Long, nFolio As Short) As Boolean
    '    Dim Filename = getFileName(nFecha, nAnexo, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    Public Function ExistFile(nWorkinFolder As String, nFecha As String, nAnexo As Long, nFolio As Short) As Boolean
        Dim Filename = getFileName(nWorkinFolder, nFecha, nAnexo, nFolio)
        Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function ExistFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short) As Boolean
    '    Dim Filename = getFileName(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    'Public Function ExistFile(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean
    '    Dim Filename = getFileName(nFecha, nCargue, nPaquete, nItem, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    Public Function ExistFile(nWorkinFolder As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean
        Dim Filename = getFileName(nWorkinFolder, nFecha, nCargue, nPaquete, nItem, nFolio)
        Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function ExistFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean
    '    Dim Filename = getFileName(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    'Public Function ExistFile(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean
    '    Dim Filename = getFileName(nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    Public Function ExistFile(nWorkinFolder As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean
        Dim Filename = getFileName(nWorkinFolder, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)
        Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function ExistFile(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean
    '    Dim Filename = getFileName(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    Public Function ExistCache(nAnexo As Long, nFolio As Short) As Boolean
        Dim Filename = getCacheName(nAnexo, nFolio)
        Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    End Function

    'Public Function ExistCache(nWorkinFolder As String, nAnexo As Long, nFolio As Short) As Boolean
    '    Dim Filename = getCacheName(nWorkinFolder, nAnexo, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function ExistCache(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short) As Boolean
    '    Dim Filename = getCacheName(nVolumen, sEntidad, sProyecto, nAnexo, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    Public Function ExistCache(nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean
        Dim Filename = getCacheName(nCargue, nPaquete, nItem, nFolio)
        Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    End Function

    'Public Function ExistCache(nWorkinFolder As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean
    '    Dim Filename = getCacheName(nWorkinFolder, nCargue, nPaquete, nItem, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function ExistCache(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean
    '    Dim Filename = getCacheName(nVolumen, sEntidad, sProyecto, nCargue, nPaquete, nItem, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    Public Function ExistCache(nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean
        Dim Filename = getCacheName(nExpediente, nFolder, nFile, nVersion, nFolio)
        Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    End Function

    'Public Function ExistCache(nWorkinFolder As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean
    '    Dim Filename = getCacheName(nWorkinFolder, nExpediente, nFolder, nFile, nVersion, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Public Function ExistCache(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean
    '    Dim Filename = getCacheName(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)
    '    Return File.Exists(Filename & ".image") And File.Exists(Filename & ".thumbnail")
    'End Function

    ' ReSharper disable UnusedParameter.Local

    'Private Function getDirectoryName(nFecha As String, nAnexo As Long) As String
    '    Return Me.WorkingFolder & "Fileserver\Anexos\" & nFecha & "\" & nAnexo & "\"
    'End Function

    'Private Function getDirectoryName(nRuta As String, nAnexo As Long) As String
    '    Return Me.WorkingFolder & "Fileserver\Anexos\" & nRuta & "\" & nAnexo & "\"
    'End Function

    Private Function getDirectoryName(nWorkingFolder As String, nRuta As String, nAnexo As Long) As String
        Return nWorkingFolder & "Fileserver\Anexos\" & nRuta & "\" & nAnexo & "\"
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Private Function getDirectoryName(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long) As String
    '    Return Me.WorkingFolder & "Fileserver\V" & nVolumen & "\" & sEntidad & "\" & sProyecto & "\Anexos\" & nFecha & "\" & nAnexo & "\"
    'End Function

    'Private Function getDirectoryName(nFecha As String, nCargue As Integer, nPaquete As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\Items\" & nFecha & "\" & nCargue & "\"
    'End Function

    'Private Function getDirectoryName(nRuta As String, nCargue As Integer, nPaquete As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\Items\" & nRuta & "\" & nCargue & "\"
    'End Function

    Private Function getDirectoryName(nWorkingFolder As String, nRuta As String, nCargue As Integer, nPaquete As Short) As String
        Return nWorkingFolder & "Fileserver\Items\" & nRuta & "\" & nCargue & "\"
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Private Function getDirectoryName(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\V" & nVolumen & "\" & sEntidad & "\" & sProyecto & "\Items\" & nFecha & "\" & nCargue & "\"
    'End Function

    'Private Function getDirectoryName(nFecha As String, nExpediente As Long, nFolder As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\Files\" & nFecha & "\" & nExpediente
    'End Function

    'Private Function getDirectoryName(nRuta As String, nExpediente As Long, nFolder As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\Files\" & nRuta & "\" & nExpediente
    'End Function

    Private Function getDirectoryName(nWorkingFolder As String, nRuta As String, nExpediente As Long, nFolder As Short) As String
        Return nWorkingFolder & "Fileserver\Files\" & nRuta & "\" & nExpediente
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Private Function getDirectoryName(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\V" & nVolumen & "\" & sEntidad & "\" & sProyecto & "\Files\" & nFecha & "\" & nExpediente
    'End Function

    ' ReSharper restore UnusedParameter.Local

    Private Function getHeadName(nAnexo As Long) As String
        Return nAnexo & "_"
    End Function

    Private Function getHeadName(nCargue As Integer, nPaquete As Short, nItem As Integer) As String
        Return nCargue & "_" & nPaquete & "_" & nItem & "_"
    End Function

    Private Function getHeadName(nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As String
        Return nExpediente & "_" & nFolder & "_" & nFile & "_" & nVersion & "_"
    End Function



    'getFileName

    'Private Function getFileName(nFecha As String, nAnexo As Long, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\Anexos\" & nFecha & "\" & nAnexo & "\" & nAnexo & "_" & nFolio
    'End Function

    'Private Function getFileName(nRuta As String, nAnexo As Long, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\Anexos\" & nRuta & "\" & nAnexo & "\" & nAnexo & "_" & nFolio
    'End Function

    Private Function getFileName(nWorkingFolder As String, nRuta As String, nAnexo As Long, nFolio As Short) As String
        Return nWorkingFolder & "Fileserver\Anexos\" & nRuta & "\" & nAnexo & "\" & nAnexo & "_" & nFolio
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Private Function getFileName(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\V" & nVolumen & "\" & sEntidad & "\" & sProyecto & "\Anexos\" & nFecha & "\" & nAnexo & "\" & nAnexo & "_" & nFolio
    'End Function

    'Private Function getFileName(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\Items\" & nFecha & "\" & nCargue & "\" & nCargue & "_" & nPaquete & "_" & nItem & "_" & nFolio
    'End Function

    'Private Function getFileName(nRuta As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\Items\" & nRuta & "\" & nCargue & "\" & nCargue & "_" & nPaquete & "_" & nItem & "_" & nFolio
    'End Function

    Private Function getFileName(nWorkingFolder As String, nRuta As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As String
        Return nWorkingFolder & "Fileserver\Items\" & nRuta & "\" & nCargue & "\" & nCargue & "_" & nPaquete & "_" & nItem & "_" & nFolio
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Private Function getFileName(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\V" & nVolumen & "\" & sEntidad & "\" & sProyecto & "\Items\" & nFecha & "\" & nCargue & "\" & nCargue & "_" & nPaquete & "_" & nItem & "_" & nFolio
    'End Function

    'Private Function getFileName(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\Files\" & nFecha & "\" & nExpediente & "\" & nExpediente & "_" & nFolder & "_" & nFile & "_" & nVersion & "_" & nFolio
    'End Function

    'Private Function getFileName(nRuta As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\Files\" & nRuta & "\" & nExpediente & "\" & nExpediente & "_" & nFolder & "_" & nFile & "_" & nVersion & "_" & nFolio
    'End Function

    Private Function getFileName(nWorkingFolder As String, nRuta As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As String
        Return nWorkingFolder & "Fileserver\Files\" & nRuta & "\" & nExpediente & "\" & nExpediente & "_" & nFolder & "_" & nFile & "_" & nVersion & "_" & nFolio
    End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Private Function getFileName(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Fileserver\V" & nVolumen & "\" & sEntidad & "\" & sProyecto & "\Files\" & nFecha & "\" & nExpediente & "\" & nExpediente & "_" & nFolder & "_" & nFile & "_" & nVersion & "_" & nFolio
    'End Function



    'getCacheName

    Private Function getCacheName(nAnexo As Long, nFolio As Short) As String
        Return Me.WorkingFolder & "Cache\Anexos\" & nAnexo & "\" & nAnexo & "_" & nFolio
    End Function

    'Private Function getCacheName(nWorkingFolder As String, nAnexo As Long, nFolio As Short) As String
    '    Return nWorkingFolder & "Cache\Anexos\" & nAnexo & "\" & nAnexo & "_" & nFolio
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Private Function getCacheName(nVolumen As Long, sEntidad As String, sProyecto As String, nAnexo As Long, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Cache\V" & nVolumen & "\" & sEntidad & "\" & sProyecto & "\Anexos\" & nAnexo & "\" & nAnexo & "_" & nFolio
    'End Function

    Private Function getCacheName(nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As String
        Return Me.WorkingFolder & "Cache\Items\" & nCargue & "\" & nCargue & "_" & nPaquete & "_" & nItem & "_" & nFolio
    End Function

    'Private Function getCacheName(nWorkingFolder As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As String
    '    Return nWorkingFolder & "Cache\Items\" & nCargue & "\" & nCargue & "_" & nPaquete & "_" & nItem & "_" & nFolio
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Private Function getCacheName(nVolumen As Long, sEntidad As String, sProyecto As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Cache\V" & nVolumen & "\" & sEntidad & "\" & sProyecto & "\Items\" & nCargue & "\" & nCargue & "_" & nPaquete & "_" & nItem & "_" & nFolio
    'End Function

    Private Function getCacheName(nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As String
        Return Me.WorkingFolder & "Cache\Files\" & nExpediente & "\" & nExpediente & "_" & nFolder & "_" & nFile & "_" & nVersion & "_" & nFolio
    End Function

    'Private Function getCacheName(nWorkingFolder As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As String
    '    Return nWorkingFolder & "Cache\Files\" & nExpediente & "\" & nExpediente & "_" & nFolder & "_" & nFile & "_" & nVersion & "_" & nFolio
    'End Function

    '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
    'Private Function getCacheName(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As String
    '    Return Me.WorkingFolder & "Cache\V" & nVolumen & "\" & sEntidad & "\" & sProyecto & "\Files\" & nFecha & "\" & nExpediente & "\" & nExpediente & "_" & nFolder & "_" & nFile & "_" & nVersion & "_" & nFolio
    'End Function

    Private Shared Function GetFile(nFileName As String) As Byte()
        Using fsInput As New FileStream(nFileName, FileMode.Open, FileAccess.Read)
            Dim Data(CInt(fsInput.Length - 1)) As Byte

            fsInput.Read(Data, 0, Data.Length)
            fsInput.Close()

            Return Data
        End Using
    End Function

    Public Shared Function Create(nIpName As String, nPort As Integer, nAppName As String) As FileProvider
        Dim NewFileProvider As FileProvider

        Dim Channel As TcpChannel

        Try
            Channel = New TcpChannel(0)
            ChannelServices.RegisterChannel(Channel, False)
        Catch : End Try

        NewFileProvider = CType(Activator.GetObject(GetType(FileProvider), "tcp://" & nIpName & ":" & nPort & "/" & nAppName), FileProvider)

        Return (NewFileProvider)
    End Function

    'Public Shared Function Create(nIpName As String, nPort As Integer, nAppName As String, nWorkingFolder As String) As FileProvider
    '    Dim NewFileProvider As FileProvider

    '    Dim Channel As TcpChannel

    '    Try
    '        Channel = New TcpChannel(0)
    '        ChannelServices.RegisterChannel(Channel, False)
    '    Catch : End Try

    '    NewFileProvider = CType(Activator.GetObject(GetType(FileProvider), "tcp://" & nIpName & ":" & nPort & "/" & nAppName), FileProvider)
    '    'NewFileProvider.ReplaceWorkingFolder(nWorkingFolder)

    '    Return (NewFileProvider)
    'End Function

    'Public Function ReplaceWorkingFolder(ByVal nWorkingFolder As String) As Boolean

    '    Try
    '        _WorkingFolder = nWorkingFolder.TrimEnd("\"c) & "\"c
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try

    'End Function

#End Region

End Class
