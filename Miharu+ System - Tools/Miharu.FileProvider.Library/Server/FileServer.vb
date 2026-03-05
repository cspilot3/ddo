Namespace Server
    Friend Class FileServer
        Implements IServer

#Region " Declaraciones "

        Private Server As FileProvider
        Private IpName As String
        Private Port As Integer
        Private AppName As String
        Private WorkingFolder As String
#End Region

#Region " Constructores "

        Public Sub New(nIpName As String, nPort As Integer, nAppName As String)
            Me.IpName = nIpName
            Me.Port = nPort
            Me.AppName = nAppName
        End Sub

        Public Sub New(nIpName As String, nPort As Integer, nAppName As String, nWorkingFolder As String)
            Me.IpName = nIpName
            Me.Port = nPort
            Me.AppName = nAppName
            Me.WorkingFolder = nWorkingFolder
        End Sub

#End Region

#Region " Implementacion IServer "

        Public ReadOnly Property ConnectionOpen As Boolean Implements IServer.ConnectionOpen
            Get
                Return Me.Server IsNot Nothing
            End Get
        End Property


        'Public Sub Connect(nUser As Integer) Implements IServer.Connect
        '    If Me.WorkingFolder Is Nothing Then
        '        Me.Server = FileProvider.Create(Me.IpName, Me.Port, Me.AppName)
        '    Else
        '        Me.Server = FileProvider.Create(Me.IpName, Me.Port, Me.AppName, Me.WorkingFolder)
        '    End If
        'End Sub

        Public Sub Connect(nUser As Integer) Implements IServer.Connect

            Me.Server = FileProvider.Create(Me.IpName, Me.Port, Me.AppName)

        End Sub

        Public Sub TransactionBegin() Implements IServer.TransactionBegin

        End Sub

        Public Sub TransactionCommit() Implements IServer.TransactionCommit

        End Sub

        Public Sub TransactionRollback() Implements IServer.TransactionRollback

        End Sub

        Public Sub Disconnect() Implements IServer.Disconnect
            If (Me.Server IsNot Nothing) Then Me.Server = Nothing
        End Sub


        Public Sub CreateItem(nAnexo As Long, nContentType As String) Implements IServer.CreateItem

        End Sub

        Public Sub CreateItem(nCargue As Integer, nPaquete As Short, nItem As Integer) Implements IServer.CreateItem

        End Sub

        Public Sub CreateItem(nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nContentType As String, nFileUniqueIdentifier As Guid) Implements IServer.CreateItem

        End Sub


        'Public Sub CreateFolio(nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.CreateFile(nFecha, nAnexo, nFolio, nImagen, nThumbnail, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Sub CreateFolio(nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
            Dim MsgError As String = ""
            Dim Respuesta As Boolean

            Respuesta = Me.Server.CreateFile(WorkingFolder, nFecha, nAnexo, nFolio, nImagen, nThumbnail, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub CreateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.CreateFile(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio, nImagen, nThumbnail, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub


        'Public Sub CreateFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.CreateFile(nFecha, nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Sub CreateFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
            Dim MsgError As String = ""
            Dim Respuesta As Boolean

            Respuesta = Me.Server.CreateFile(WorkingFolder, nFecha, nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub CreateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.CreateFile(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        'Public Sub CreateFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.CreateFile(nFecha, nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Sub CreateFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
            Dim MsgError As String = ""
            Dim Respuesta As Boolean

            Respuesta = Me.Server.CreateFile(WorkingFolder, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub CreateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.CreateFile(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        'Public Sub UpdateFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte) Implements IServer.UpdateFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.CreateFile(nFecha, nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Sub UpdateFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte) Implements IServer.UpdateFolio
            Dim MsgError As String = ""
            Dim Respuesta As Boolean

            Respuesta = Me.Server.CreateFile(WorkingFolder, nFecha, nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub UpdateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte) Implements IServer.UpdateFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.CreateFile(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        'Public Sub UpdateFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte) Implements IServer.UpdateFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.CreateFile(nFecha, nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Sub UpdateFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte) Implements IServer.UpdateFolio
            Dim MsgError As String = ""
            Dim Respuesta As Boolean

            Respuesta = Me.Server.CreateFile(WorkingFolder, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub UpdateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte) Implements IServer.UpdateFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.CreateFile(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        'Public Sub MoveFolio(nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short) Implements IServer.MoveFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.MoveFile(nFechaItem, nCargue, nPaquete, nItem, nFolioItem, nFechaFile, nExpediente, nFolder, nFile, nVersion, nFolioFile, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Sub MoveFolio(nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short) Implements IServer.MoveFolio
            Dim MsgError As String = ""
            Dim Respuesta As Boolean

            Respuesta = Me.Server.MoveFile(WorkingFolder, nFechaItem, nCargue, nPaquete, nItem, nFolioItem, nFechaFile, nExpediente, nFolder, nFile, nVersion, nFolioFile, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub MoveFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short) Implements IServer.MoveFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.MoveFile(nVolumen, sEntidad, sProyecto, nFechaItem, nCargue, nPaquete, nItem, nFolioItem, nFechaFile, nExpediente, nFolder, nFile, nVersion, nFolioFile, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        'Public Sub MoveFolio(nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaAnexo As String, nAnexo As Long, nFolioAnexo As Short) Implements IServer.MoveFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.MoveFile(nFechaItem, nCargue, nPaquete, nItem, nFolioItem, nFechaAnexo, nAnexo, nFolioAnexo, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Sub MoveFolio(nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaAnexo As String, nAnexo As Long, nFolioAnexo As Short) Implements IServer.MoveFolio
            Dim MsgError As String = ""
            Dim Respuesta As Boolean

            Respuesta = Me.Server.MoveFile(WorkingFolder, nFechaItem, nCargue, nPaquete, nItem, nFolioItem, nFechaAnexo, nAnexo, nFolioAnexo, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub MoveFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaAnexo As String, nAnexo As Long, nFolioAnexo As Short) Implements IServer.MoveFolio
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.MoveFile(nVolumen, sEntidad, sProyecto, nFechaItem, nCargue, nPaquete, nItem, nFolioItem, nFechaAnexo, nAnexo, nFolioAnexo, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        'Public Sub DeleteItem(nFecha As String, nAnexo As Long) Implements IServer.DeleteItem
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.DeleteFile(nFecha, nAnexo, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        'Public Sub DeleteItem(nDatos As String, nAnexo As Long) Implements IServer.DeleteItem
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.DeleteFile(nDatos, nAnexo, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Sub DeleteItem(nDatos As String, nAnexo As Long) Implements IServer.DeleteItem
            Dim MsgError As String = ""
            Dim Respuesta As Boolean

            Respuesta = Me.Server.DeleteFile(WorkingFolder, nDatos, nAnexo, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub DeleteItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long) Implements IServer.DeleteItem
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.DeleteFile(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        'Public Sub DeleteItem(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) Implements IServer.DeleteItem
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.DeleteFile(nFecha, nCargue, nPaquete, nItem, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Sub DeleteItem(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) Implements IServer.DeleteItem
            Dim MsgError As String = ""
            Dim Respuesta As Boolean

            Respuesta = Me.Server.DeleteFile(WorkingFolder, nFecha, nCargue, nPaquete, nItem, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub DeleteItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) Implements IServer.DeleteItem
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.DeleteFile(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        'Public Sub DeleteItem(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) Implements IServer.DeleteItem
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.DeleteFile(nFecha, nExpediente, nFolder, nFile, nVersion, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Sub DeleteItem(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) Implements IServer.DeleteItem
            Dim MsgError As String = ""
            Dim Respuesta As Boolean

            Respuesta = Me.Server.DeleteFile(WorkingFolder, nFecha, nExpediente, nFolder, nFile, nVersion, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub DeleteItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) Implements IServer.DeleteItem
        '    Dim MsgError As String = ""
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.DeleteFile(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)
        'End Sub

        Public Function GetFolios(nFecha As String, nAnexo As Long) As Short Implements IServer.GetFolios
            Return Me.Server.GetFolios(WorkingFolder, nFecha, nAnexo)
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long) As Short Implements IServer.GetFolios
        '    Return Me.Server.GetFolios(nVolumen, sEntidad, sProyecto, nFecha, nAnexo)
        'End Function

        'Public Function GetFolios(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short Implements IServer.GetFolios
        '    Return Me.Server.GetFolios(nFecha, nCargue, nPaquete, nItem)
        'End Function

        Public Function GetFolios(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short Implements IServer.GetFolios
            Return Me.Server.GetFolios(WorkingFolder, nFecha, nCargue, nPaquete, nItem)
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short Implements IServer.GetFolios
        '    Return Me.Server.GetFolios(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem)
        'End Function

        Public Function GetFolios(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As Short Implements IServer.GetFolios
            Return Me.Server.GetFolios(WorkingFolder, nFecha, nExpediente, nFolder, nFile, nVersion)
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As Short Implements IServer.GetFolios
        '    Return Me.Server.GetFolios(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion)
        'End Function

        Public Function GetItem(nFecha As String, nAnexo As Long, nFolio As Short) As FolioStructure Implements IServer.GetItem
            Dim MsgError As String = ""
            Dim Folio As New FolioStructure
            Dim Respuesta As Boolean

            Respuesta = Me.Server.GetFile(WorkingFolder, nFecha, nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)

            Return Folio
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short) As FolioStructure Implements IServer.GetItem
        '    Dim MsgError As String = ""
        '    Dim Folio As New FolioStructure
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.GetFile(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)

        '    Return Folio
        'End Function

        'Public Function GetItem(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As FolioStructure Implements IServer.GetItem
        '    Dim MsgError As String = ""
        '    Dim Folio As New FolioStructure
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.GetFile(nFecha, nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)

        '    Return Folio
        'End Function

        Public Function GetItem(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As FolioStructure Implements IServer.GetItem
            Dim MsgError As String = ""
            Dim Folio As New FolioStructure
            Dim Respuesta As Boolean

            Respuesta = Me.Server.GetFile(WorkingFolder, nFecha, nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)

            Return Folio
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As FolioStructure Implements IServer.GetItem
        '    Dim MsgError As String = ""
        '    Dim Folio As New FolioStructure
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.GetFile(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)

        '    Return Folio
        'End Function

        Public Function GetItem(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As FolioStructure Implements IServer.GetItem
            Dim MsgError As String = ""
            Dim Folio As New FolioStructure
            Dim Respuesta As Boolean

            Respuesta = Me.Server.GetFile(WorkingFolder, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
            If (Not Respuesta) Then Throw New Exception(MsgError)

            Return Folio
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As FolioStructure Implements IServer.GetItem
        '    Dim MsgError As String = ""
        '    Dim Folio As New FolioStructure
        '    Dim Respuesta As Boolean

        '    Respuesta = Me.Server.GetFile(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio, Folio.Image_Binary, Folio.Thumbnail_Binary, MsgError)
        '    If (Not Respuesta) Then Throw New Exception(MsgError)

        '    Return Folio
        'End Function

        'Public Function ExistFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean Implements IServer.ExistFolio
        '    Return Me.Server.ExistFile(nFecha, nCargue, nPaquete, nItem, nFolio)
        'End Function

        Public Function ExistFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean Implements IServer.ExistFolio
            Return Me.Server.ExistFile(WorkingFolder, nFecha, nCargue, nPaquete, nItem, nFolio)
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function ExistFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean Implements IServer.ExistFolio
        '    Return Me.Server.ExistFile(nVolumen, sEntidad, sProyecto, nFecha, nCargue, nPaquete, nItem, nFolio)
        'End Function

        'Public Function ExistFolio(nFecha As String, nAnexo As Long, nFolio As Short) As Boolean Implements IServer.ExistFolio
        '    Return Me.Server.ExistFile(nFecha, nAnexo, nFolio)
        'End Function

        Public Function ExistFolio(nFecha As String, nAnexo As Long, nFolio As Short) As Boolean Implements IServer.ExistFolio
            Return Me.Server.ExistFile(WorkingFolder, nFecha, nAnexo, nFolio)
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function ExistFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short) As Boolean Implements IServer.ExistFolio
        '    Return Me.Server.ExistFile(nVolumen, sEntidad, sProyecto, nFecha, nAnexo, nFolio)
        'End Function

        'Public Function ExistFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean Implements IServer.ExistFolio
        '    Return Me.Server.ExistFile(nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)
        'End Function

        Public Function ExistFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean Implements IServer.ExistFolio
            Return Me.Server.ExistFile(WorkingFolder, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function ExistFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean Implements IServer.ExistFolio
        '    Return Me.Server.ExistFile(nVolumen, sEntidad, sProyecto, nFecha, nExpediente, nFolder, nFile, nVersion, nFolio)
        'End Function

        'Public Sub DeleteCargue(nFecha As String, nCargue As Integer) Implements IServer.DeleteCargue
        '    Me.Server.DeleteCargue(nFecha, nCargue)
        'End Sub

        Public Sub DeleteCargue(nFecha As String, nCargue As Integer) Implements IServer.DeleteCargue
            Me.Server.DeleteCargue(WorkingFolder, nFecha, nCargue)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub DeleteCargue(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer) Implements IServer.DeleteCargue
        '    Me.Server.DeleteCargue(nFecha, nCargue)
        'End Sub

        'Public Sub DeleteExpediente(nFecha As String, nExpediente As Long) Implements IServer.DeleteExpediente
        '    Me.Server.DeleteExpediente(nFecha, nExpediente)
        'End Sub

        Public Sub DeleteExpediente(nFecha As String, nExpediente As Long) Implements IServer.DeleteExpediente
            Me.Server.DeleteExpediente(WorkingFolder, nFecha, nExpediente)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub DeleteExpediente(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long) Implements IServer.DeleteExpediente
        '    Me.Server.DeleteExpediente(nFecha, nExpediente)
        'End Sub

#End Region

    End Class

End Namespace