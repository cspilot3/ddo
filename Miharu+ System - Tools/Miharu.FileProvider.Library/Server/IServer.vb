Namespace Server

    Friend Structure FolioStructure
        Public Image_Binary As Byte()
        Public Thumbnail_Binary As Byte()
    End Structure

    Friend Interface IServer
        ReadOnly Property ConnectionOpen As Boolean

        Sub Connect(nUser As Integer)

        Sub TransactionBegin()
        Sub TransactionCommit()
        Sub TransactionRollback()

        Sub Disconnect()

        Sub CreateItem(nAnexo As Long, nContentType As String)
        Sub CreateItem(nCargue As Integer, nPaquete As Short, nItem As Integer)        
        Sub CreateItem(nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nContentType As String, nFileUniqueIdentifier As Guid)

        Sub CreateFolio(nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean)
        Sub CreateFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean)
        Sub CreateFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean)

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        ''new implement
        'Sub CreateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean)
        'Sub CreateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean)
        'Sub CreateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean)

        Sub UpdateFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte)
        Sub UpdateFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte)

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        ''new implement
        'Sub UpdateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte)
        'Sub UpdateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte)


        Sub MoveFolio(nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short)
        Sub MoveFolio(nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaAnexo As String, nAnexo As Long, nFolioAnexo As Short)
        

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        ''new implement
        'Sub MoveFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short)
        'Sub MoveFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaAnexo As String, nAnexo As Long, nFolioAnexo As Short)


        'Sub DeleteItem(nFecha As String, nAnexo As Long)
        Sub DeleteItem(nDatos As String, nAnexo As Long)
        Sub DeleteItem(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer)
        Sub DeleteItem(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short)

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        ''new implement
        'Sub DeleteItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short)
        'Sub DeleteItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer)
        'Sub DeleteItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long)


        Function GetFolios(nFecha As String, nAnexo As Long) As Short
        Function GetFolios(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short
        Function GetFolios(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As Short

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        ''new implement
        'Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long) As Short
        'Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short
        'Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As Short


        Function GetItem(nFecha As String, nAnexo As Long, nFolio As Short) As FolioStructure
        Function GetItem(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As FolioStructure
        Function GetItem(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As FolioStructure

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        ''new implement
        'Function GetItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short) As FolioStructure
        'Function GetItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As FolioStructure
        'Function GetItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As FolioStructure

        Function ExistFolio(nFecha As String, nAnexo As Long, nFolio As Short) As Boolean
        Function ExistFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean
        Function ExistFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        ''new implement
        'Function ExistFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short) As Boolean
        'Function ExistFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean
        'Function ExistFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean

        Sub DeleteCargue(nFecha As String, nCargue As Integer)
        Sub DeleteExpediente(nFecha As String, nExpediente As Long)

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        ''new implement
        'Sub DeleteCargue(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer)
        'Sub DeleteExpediente(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long)

    End Interface

End Namespace