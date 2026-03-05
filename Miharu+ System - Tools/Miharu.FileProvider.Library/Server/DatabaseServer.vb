Imports DBStorage.SchemaImaging

Namespace Server
    Friend Class DatabaseServer
        Implements IServer
        
#Region " Declaraciones "

        Private Server As DBStorage.DBStorageDataBaseManager

#End Region

#Region " Constructores "

        Public Sub New(nConnectionString As String)
            Me.Server = New DBStorage.DBStorageDataBaseManager(nConnectionString)
        End Sub

#End Region

#Region " Implementacion IServer "

        Public ReadOnly Property ConnectionOpen As Boolean Implements IServer.ConnectionOpen
            Get
                If Me.Server.DataBase IsNot Nothing Then
                    If Me.Server.DataBase.Connection IsNot Nothing Then
                        If Me.Server.DataBase.Connection.State <> ConnectionState.Closed Then
                            Return True
                        End If
                    End If
                End If
                Return False
                'Return Me.Server.DataBase IsNot Nothing AndAlso Me.Server.DataBase.Connection.State <> ConnectionState.Closed
            End Get
        End Property


        Public Sub Connect(nUser As Integer) Implements IServer.Connect
            Me.Server.Connection_Open(nUser)
        End Sub

        Public Sub TransactionBegin() Implements IServer.TransactionBegin
            Me.Server.Transaction_Begin()
        End Sub

        Public Sub TransactionCommit() Implements IServer.TransactionCommit
            Me.Server.Transaction_Commit()
        End Sub

        Public Sub TransactionRollback() Implements IServer.TransactionRollback
            Me.Server.Transaction_Rollback()
        End Sub


        Public Sub Disconnect() Implements IServer.Disconnect
            Me.Server.Connection_Close()
        End Sub


        Public Sub CreateItem(nAnexo As Long, nContentType As String) Implements IServer.CreateItem
          
            Dim AnexoDataTable = Me.Server.SchemaImaging.TBL_Anexo.DBFindByfk_Anexofk_Content_Type(nAnexo, nContentType)
            
            If AnexoDataTable.Count = 0 Then
                Dim AnexoStorageType As New TBL_AnexoType
                AnexoStorageType.fk_Anexo = nAnexo
                AnexoStorageType.fk_Content_Type = nContentType
                Me.Server.SchemaImaging.TBL_Anexo.DBInsert(AnexoStorageType)
            End If

        End Sub

        Public Sub CreateItem(nCargue As Integer, nPaquete As Short, nItem As Integer) Implements IServer.CreateItem
            Dim ItemStorageType As New TBL_ItemType
            ItemStorageType.fk_Cargue = nCargue
            ItemStorageType.fk_Cargue_Paquete = nPaquete
            ItemStorageType.fk_Cargue_Item = nItem

            Me.Server.SchemaImaging.TBL_Item.DBInsert(ItemStorageType)
        End Sub

        Public Sub CreateItem(nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nContentType As String, nFileUniqueIdentifier As Guid) Implements IServer.CreateItem
            If (Server.SchemaImaging.TBL_File.DBGet(nExpediente, nFolder, nFile, nVersion).Count = 0) Then
                Dim FileStorageType As New TBL_FileType
                FileStorageType.fk_Expediente = nExpediente
                FileStorageType.fk_Folder = nFolder
                FileStorageType.fk_File = nFile
                FileStorageType.id_Version = nVersion
                FileStorageType.fk_Content_Type = nContentType
                FileStorageType.File_Unique_Identifier = nFileUniqueIdentifier

                Me.Server.SchemaImaging.TBL_File.DBInsert(FileStorageType)
            End If
        End Sub


        Public Sub CreateFolio(nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
            Dim AnexoFolioStorageType As New TBL_Anexo_FolioType
            AnexoFolioStorageType.fk_Anexo = nAnexo
            AnexoFolioStorageType.id_Anexo_Record_Folio = nFolio

            If (isRemoting) Then
                AnexoFolioStorageType.Image_Binary = nImagen
                AnexoFolioStorageType.Thumbnail_Binary = nThumbnail

                Me.Server.SchemaImaging.TBL_Anexo_Folio.DBInsert(AnexoFolioStorageType)
            Else
                AnexoFolioStorageType.Image_Binary = New Byte() {0}
                AnexoFolioStorageType.Thumbnail_Binary = New Byte() {0}

                Me.Server.SchemaImaging.TBL_Anexo_Folio.DBInsert(AnexoFolioStorageType)
                Me.Server.ActualizarAnexoFolio(nAnexo, nFolio, nImagen, nThumbnail)
            End If
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub CreateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio

        'End Sub

        Public Sub CreateFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
            Dim ItemFolioStorageType As New TBL_Item_FolioType
            ItemFolioStorageType.fk_Cargue = nCargue
            ItemFolioStorageType.fk_Cargue_Paquete = nPaquete
            ItemFolioStorageType.fk_Cargue_Item = nItem
            ItemFolioStorageType.id_Item_Folio = nFolio

            If (isRemoting) Then
                ItemFolioStorageType.Image_Binary = nImagen
                ItemFolioStorageType.Thumbnail_Binary = nThumbnail

                Me.Server.SchemaImaging.TBL_Item_Folio.DBInsert(ItemFolioStorageType)
            Else
                ItemFolioStorageType.Image_Binary = New Byte() {0}
                ItemFolioStorageType.Thumbnail_Binary = New Byte() {0}

                Me.Server.SchemaImaging.TBL_Item_Folio.DBInsert(ItemFolioStorageType)
                Me.Server.ActualizarItemFolio(nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail)
            End If
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub CreateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio

        'End Sub

        Public Sub CreateFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio
            If (Server.SchemaImaging.CTA_File_Folio.DBFindByfk_Expedientefk_Folderfk_Filefk_Versionid_File_Record_Folio(nExpediente, nFolder, nFile, nVersion, nFolio).Count = 0) Then
                Dim FileFolioStorageType As New TBL_File_FolioType
                FileFolioStorageType.fk_Expediente = nExpediente
                FileFolioStorageType.fk_Folder = nFolder
                FileFolioStorageType.fk_File = nFile
                FileFolioStorageType.fk_Version = nVersion
                FileFolioStorageType.id_File_Record_Folio = nFolio

                If (isRemoting) Then
                    FileFolioStorageType.Image_Binary = nImagen
                    FileFolioStorageType.Thumbnail_Binary = nThumbnail

                    Me.Server.SchemaImaging.TBL_File_Folio.DBInsert(FileFolioStorageType)
                Else
                    FileFolioStorageType.Image_Binary = New Byte() {0}
                    FileFolioStorageType.Thumbnail_Binary = New Byte() {0}

                    Me.Server.SchemaImaging.TBL_File_Folio.DBInsert(FileFolioStorageType)
                    Me.Server.ActualizarFileFolio(nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail)
                End If
            End If
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub CreateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte, isRemoting As Boolean) Implements IServer.CreateFolio

        'End Sub

        Public Sub UpdateFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte) Implements IServer.UpdateFolio
            Me.Server.ActualizarItemFolio(nCargue, nPaquete, nItem, nFolio, nImagen, nThumbnail)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub UpdateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte) Implements IServer.UpdateFolio

        'End Sub

        Public Sub UpdateFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte) Implements IServer.UpdateFolio
            Me.Server.ActualizarFileFolio(nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, nThumbnail)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub UpdateFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, nImagen() As Byte, nThumbnail() As Byte) Implements IServer.UpdateFolio

        'End Sub

        Public Sub MoveFolio(nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short) Implements IServer.MoveFolio
            Me.Server.SchemaImaging.PA_Move_Item_to_File.DBExecute(nCargue, nPaquete, nItem, nFolioItem, nExpediente, nFolder, nFile, nVersion, nFolioFile)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub MoveFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaFile As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolioFile As Short) Implements IServer.MoveFolio
        '    Me.Server.SchemaImaging.PA_Move_Item_to_File.DBExecute(nCargue, nPaquete, nItem, nFolioItem, nExpediente, nFolder, nFile, nVersion, nFolioFile)
        'End Sub

        Public Sub MoveFolio(nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaAnexo As String, nAnexo As Long, nFolioAnexo As Short) Implements IServer.MoveFolio
            Me.Server.SchemaImaging.PA_Move_Item_to_Anexo.DBExecute(nCargue, nPaquete, nItem, nFolioItem, nAnexo, nFolioAnexo)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub MoveFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFechaItem As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolioItem As Short, nFechaAnexo As String, nAnexo As Long, nFolioAnexo As Short) Implements IServer.MoveFolio
        '    Me.Server.SchemaImaging.PA_Move_Item_to_Anexo.DBExecute(nCargue, nPaquete, nItem, nFolioItem, nAnexo, nFolioAnexo)
        'End Sub

        Public Sub DeleteItem(nFecha As String, nAnexo As Long) Implements IServer.DeleteItem
            Me.Server.SchemaImaging.TBL_Anexo.DBDelete(nAnexo)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub DeleteItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long) Implements IServer.DeleteItem
        '    Me.Server.SchemaImaging.TBL_Anexo.DBDelete(nAnexo)
        'End Sub

        Public Sub DeleteItem(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) Implements IServer.DeleteItem
            Me.Server.SchemaImaging.TBL_Item.DBDelete(nCargue, nPaquete, nItem)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub DeleteItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) Implements IServer.DeleteItem
        '    Me.Server.SchemaImaging.TBL_Item.DBDelete(nCargue, nPaquete, nItem)
        'End Sub

        Public Sub DeleteItem(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) Implements IServer.DeleteItem
            Me.Server.SchemaImaging.TBL_File.DBDelete(nExpediente, nFolder, nFile, nVersion)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub DeleteItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) Implements IServer.DeleteItem
        '    Me.Server.SchemaImaging.TBL_File.DBDelete(nExpediente, nFolder, nFile, nVersion)
        'End Sub

        Public Function GetFolios(nFecha As String, nAnexo As Long) As Short Implements IServer.GetFolios
            Return Me.Server.SchemaImaging.PA_Anexo_Count.DBExecute(nAnexo)
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long) As Short Implements IServer.GetFolios
        '    Return Me.Server.SchemaImaging.PA_Anexo_Count.DBExecute(nAnexo)
        'End Function

        Public Function GetFolios(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short Implements IServer.GetFolios
            Return Me.Server.SchemaImaging.PA_Item_Count.DBExecute(nCargue, nPaquete, nItem)
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer) As Short Implements IServer.GetFolios
        '    Return Me.Server.SchemaImaging.PA_Item_Count.DBExecute(nCargue, nPaquete, nItem)
        'End Function

        Public Function GetFolios(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As Short Implements IServer.GetFolios
            Return Me.Server.SchemaImaging.PA_File_Count.DBExecute(nExpediente, nFolder, nFile, nVersion)
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetFolios(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short) As Short Implements IServer.GetFolios
        '    Return Me.Server.SchemaImaging.PA_File_Count.DBExecute(nExpediente, nFolder, nFile, nVersion)
        'End Function

        Public Function GetItem(nFecha As String, nAnexo As Long, nFolio As Short) As FolioStructure Implements IServer.GetItem
            Dim FolioDataTable = Me.Server.SchemaImaging.TBL_Anexo_Folio.DBGet(nAnexo, nFolio)

            If (FolioDataTable.Count > 0) Then
                Dim Data = New FolioStructure()

                Data.Image_Binary = FolioDataTable(0).Image_Binary
                Data.Thumbnail_Binary = FolioDataTable(0).Thumbnail_Binary

                Return Data
            Else
                Throw New Exception("FileProviderServer.GetItem - No se encontro imagen en la base de datos")
            End If
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short) As FolioStructure Implements IServer.GetItem
        '    Dim FolioDataTable = Me.Server.SchemaImaging.TBL_Anexo_Folio.DBGet(nAnexo, nFolio)

        '    If (FolioDataTable.Count > 0) Then
        '        Dim Data = New FolioStructure()

        '        Data.Image_Binary = FolioDataTable(0).Image_Binary
        '        Data.Thumbnail_Binary = FolioDataTable(0).Thumbnail_Binary

        '        Return Data
        '    Else
        '        Throw New Exception("FileProviderServer.GetItem - No se encontro imagen en la base de datos")
        '    End If
        'End Function

        Public Function GetItem(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As FolioStructure Implements IServer.GetItem
            Dim FolioDataTable = Me.Server.SchemaImaging.TBL_Item_Folio.DBGet(nCargue, nPaquete, nItem, nFolio)

            If (FolioDataTable.Count > 0) Then
                Dim Data = New FolioStructure()

                Data.Image_Binary = FolioDataTable(0).Image_Binary
                Data.Thumbnail_Binary = FolioDataTable(0).Thumbnail_Binary

                Return Data
            Else
                Throw New Exception("FileProviderServer.GetItem - No se encontro imagen en la base de datos")
            End If
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As FolioStructure Implements IServer.GetItem
        '    Dim FolioDataTable = Me.Server.SchemaImaging.TBL_Item_Folio.DBGet(nCargue, nPaquete, nItem, nFolio)

        '    If (FolioDataTable.Count > 0) Then
        '        Dim Data = New FolioStructure()

        '        Data.Image_Binary = FolioDataTable(0).Image_Binary
        '        Data.Thumbnail_Binary = FolioDataTable(0).Thumbnail_Binary

        '        Return Data
        '    Else
        '        Throw New Exception("FileProviderServer.GetItem - No se encontro imagen en la base de datos")
        '    End If
        'End Function

        Public Function GetItem(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As FolioStructure Implements IServer.GetItem
            Dim FolioDataTable = Me.Server.SchemaImaging.TBL_File_Folio.DBGet(nExpediente, nFolder, nFile, nVersion, nFolio)

            If (FolioDataTable.Count > 0) Then
                Dim Data = New FolioStructure()

                Data.Image_Binary = FolioDataTable(0).Image_Binary
                Data.Thumbnail_Binary = FolioDataTable(0).Thumbnail_Binary

                Return Data
            Else
                Throw New Exception("FileProviderServer.GetItem - No se encontro imagen en la base de datos")
            End If
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function GetItem(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As FolioStructure Implements IServer.GetItem
        '    Dim FolioDataTable = Me.Server.SchemaImaging.TBL_File_Folio.DBGet(nExpediente, nFolder, nFile, nVersion, nFolio)

        '    If (FolioDataTable.Count > 0) Then
        '        Dim Data = New FolioStructure()

        '        Data.Image_Binary = FolioDataTable(0).Image_Binary
        '        Data.Thumbnail_Binary = FolioDataTable(0).Thumbnail_Binary

        '        Return Data
        '    Else
        '        Throw New Exception("FileProviderServer.GetItem - No se encontro imagen en la base de datos")
        '    End If
        'End Function

        Public Function ExistFolio(nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean Implements IServer.ExistFolio
            Return Me.Server.SchemaImaging.CTA_Item_Folio.DBFindByfk_Carguefk_Cargue_Paquetefk_Cargue_Itemid_Item_Folio(nCargue, nPaquete, nItem, nFolio).Count <> 0
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function ExistFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer, nPaquete As Short, nItem As Integer, nFolio As Short) As Boolean Implements IServer.ExistFolio
        '    Return Me.Server.SchemaImaging.CTA_Item_Folio.DBFindByfk_Carguefk_Cargue_Paquetefk_Cargue_Itemid_Item_Folio(nCargue, nPaquete, nItem, nFolio).Count <> 0
        'End Function

        Public Function ExistFolio(nFecha As String, nAnexo As Long, nFolio As Short) As Boolean Implements IServer.ExistFolio
            Return Me.Server.SchemaImaging.CTA_Anexo_Folio.DBFindByfk_Anexoid_Anexo_Record_Folio(nAnexo, nFolio).Count <> 0
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function ExistFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nAnexo As Long, nFolio As Short) As Boolean Implements IServer.ExistFolio
        '    Return Me.Server.SchemaImaging.CTA_Anexo_Folio.DBFindByfk_Anexoid_Anexo_Record_Folio(nAnexo, nFolio).Count <> 0
        'End Function

        Public Function ExistFolio(nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean Implements IServer.ExistFolio
            Return Me.Server.SchemaImaging.CTA_File_Folio.DBFindByfk_Expedientefk_Folderfk_Filefk_Versionid_File_Record_Folio(nExpediente, nFolder, nFile, nVersion, nFolio).Count <> 0
        End Function

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Function ExistFolio(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short) As Boolean Implements IServer.ExistFolio
        '    Return Me.Server.SchemaImaging.CTA_File_Folio.DBFindByfk_Expedientefk_Folderfk_Filefk_Versionid_File_Record_Folio(nExpediente, nFolder, nFile, nVersion, nFolio).Count <> 0
        'End Function

        Public Sub DeleteCargue(nFecha As String, nCargue As Integer) Implements IServer.DeleteCargue
            Me.Server.SchemaImaging.TBL_Item.DBDelete(nCargue, Nothing, Nothing)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub DeleteCargue(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nCargue As Integer) Implements IServer.DeleteCargue
        '    Me.Server.SchemaImaging.TBL_Item.DBDelete(nCargue, Nothing, Nothing)
        'End Sub

        Public Sub DeleteExpediente(nFecha As String, nExpediente As Long) Implements IServer.DeleteExpediente
            Me.Server.SchemaImaging.TBL_File.DBDelete(nExpediente, Nothing, Nothing, Nothing)
        End Sub

        '*******************SE COMENTA POR CAMBIO DE LÓGICA******************
        'Public Sub DeleteExpediente(nVolumen As Long, sEntidad As String, sProyecto As String, nFecha As String, nExpediente As Long) Implements IServer.DeleteExpediente
        '    Me.Server.SchemaImaging.TBL_File.DBDelete(nExpediente, Nothing, Nothing, Nothing)
        'End Sub

#End Region

    End Class

End Namespace