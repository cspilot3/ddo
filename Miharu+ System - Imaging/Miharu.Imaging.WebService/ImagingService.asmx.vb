Imports System.Web.Services
Imports System.ComponentModel
Imports Miharu.FileProvider.Library
Imports System.IO
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library.Config

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la siguiente línea.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://slyg.com.co/miharu/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class ImagingService
    Inherits Services.WebService

#Region " Servidor "

    <WebMethod()> _
    Public Overloads Function ExistsKeyA(ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nKey As String) As PR_ExistKey
        Return ExistsKeyB(nEntidad, nProyecto, nEsquema, -1, nKey)
    End Function

    <WebMethod()> _
    Public Overloads Function ExistsKeyB(ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nDocumento As Integer, ByVal nKey As String) As PR_ExistKey
        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.CoreConnectionString)
        Dim Respuesta As New PR_ExistKey
        Dim documento As Slyg.Tools.SlygNullable(Of Integer)

        If (nDocumento = -1) Then
            documento = Nothing
        Else
            documento = nDocumento
        End If

        Try
            dbmCore.Connection_Open(1) ' System

            Dim FileKeyDataTable = dbmCore.SchemaImaging.PA_File_Key.DBExecute(nEntidad, nProyecto, nEsquema, documento, nKey)

            Respuesta.Result = True
            Respuesta.Message = ""

            If FileKeyDataTable.Rows.Count > 0 Then
                Respuesta.Encontrado = True
                Respuesta.Identificador = FileKeyDataTable(0).File_Unique_Identifier.ToString()
                Respuesta.Folios = FileKeyDataTable(0).Folios_Documento_File
                Respuesta.Size = FileKeyDataTable(0).Tamaño_Imagen_File
                Respuesta.Tipo = FileKeyDataTable(0).fk_Content_Type
            Else
                Respuesta.Encontrado = False
            End If

        Catch ex As Exception
            Respuesta.Result = False
            Respuesta.Message = ex.Message
        Finally
            dbmCore.Connection_Close() ' System
        End Try

        Return Respuesta
    End Function

    <WebMethod()> _
    Public Function ExistsKeyC(ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nKey As String) As PR_ExistKey
        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.CoreConnectionString)
        Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString)
        Dim manager As FileProviderManager = Nothing
        Dim Respuesta As New PR_ExistKey

        Try
            dbmCore.Connection_Open(1) ' System
            dbmImaging.Connection_Open(1) ' System

            Dim FileKeyDataTable = dbmCore.SchemaImaging.PA_File_Key.DBExecute(nEntidad, nProyecto, nEsquema, Nothing, nKey)

            Respuesta.Result = True
            Respuesta.Message = ""

            If FileKeyDataTable.Rows.Count > 0 Then
                Respuesta.Encontrado = True
                Respuesta.Identificador = FileKeyDataTable(0).File_Unique_Identifier.ToString()
                Respuesta.Folios = FileKeyDataTable(0).Folios_Documento_File
                Respuesta.Size = FileKeyDataTable(0).Tamaño_Imagen_File
                Respuesta.Tipo = FileKeyDataTable(0).fk_Content_Type
            Else
                Respuesta.Encontrado = False
            End If

            If Respuesta.Encontrado = True Then
                Dim Identificador As Guid
                Dim FileName As String = ""

                If Not (Guid.TryParse(FileKeyDataTable(0).File_Unique_Identifier.ToString(), Identificador)) Then
                    Throw New Exception("Token no válido, " & FileKeyDataTable(0).File_Unique_Identifier.ToString())
                End If
                ' Leer el File
                Dim FileDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByFile_Unique_Identifier(Identificador)
                If (FileDataTable.Count = 0) Then Throw New Exception("No se encontro un registro asociado al Token: " & Identificador.ToString())
                Dim FileRow = FileDataTable(0)

                If (FileRow.Es_Anexo) Then
                    manager = New FileProviderManager(FileRow.fk_Anexo, dbmImaging, 0)
                Else
                    manager = New FileProviderManager(FileRow.fk_Expediente, FileRow.fk_Folder, dbmImaging, 0)
                End If

                manager.Connect()

                Dim TempPath As String = Server.MapPath("~/_temporal")

                TempPath = TempPath.TrimEnd("/"c) & "/"

                Dim FileNames As New List(Of String)

                If (FileDataTable(0).Es_Anexo) Then
                    ' Es un anexo
                    Dim Folios = manager.GetFolios(FileRow.fk_Anexo)

                    If (Folios = 0) Then Throw New Exception("No se encontro folios asociados al anexo del Token: " & Identificador.ToString())

                    Dim Image As Byte() = Nothing
                    Dim Thumbnile As Byte() = Nothing
                    manager.GetFolio(FileRow.fk_Anexo, CShort(1), Image, Thumbnile)
                    FileName = TempPath & Identificador.ToString() & FileDataTable(0).fk_Content_Type

                    Using ImageFile As New FileStream(FileName, FileMode.Create)
                        ImageFile.Write(Image, 0, Image.Length)
                        ImageFile.Close()
                    End Using
                Else
                    ' Es un file
                    Dim Folios = manager.GetFolios(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)

                    If (Folios = 0) Then Throw New Exception("El documento se encuentra en proceso de transferencia, por favor intente mas tarde: " & Identificador.ToString())

                    For folio As Short = 1 To Folios
                        Dim Image As Byte() = Nothing
                        Dim Thumbnile As Byte() = Nothing
                        manager.GetFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, folio, Image, Thumbnile)


                        FileName = TempPath & Guid.NewGuid().ToString() & FileDataTable(0).fk_Content_Type

                        Select Case FileRow.fk_Content_Type
                            Case ".gif", ".jpg", ".png", ".bmp"
                                FileName = TempPath & Guid.NewGuid().ToString() & FileRow.fk_Content_Type

                                Using fs As New FileStream(FileName, FileMode.CreateNew)
                                    fs.Write(Image, 0, Image.Length)
                                End Using

                            Case ".tif", ".tiff"
                                FileName = TempPath & Guid.NewGuid().ToString() & FileRow.fk_Content_Type
                                ImageManager.Save(New FreeImageAPI.FreeImageBitmap(New MemoryStream(Image)), FileName, "", ImageManager.EnumFormat.Tiff, ImageManager.EnumCompression.None, False, TempPath)

                        End Select

                        FileNames.Add(FileName)
                    Next

                    FileName = TempPath & Identificador.ToString() & FileRow.fk_Content_Type

                    '-------------------------------------------------------------------------
                    ImageManager.Save(FileNames, FileName, "", Utilities.GetEnumFormat(FileRow.fk_Content_Type.ToString()), ImageManager.EnumCompression.None, False, TempPath, True)
                    '-------------------------------------------------------------------------

                    Respuesta.Identificador = FileName.Substring(FileName.LastIndexOf("_temporal"))
                End If
            End If

        Catch ex As Exception
            Respuesta.Result = False
            Respuesta.Message = ex.Message
        Finally
            dbmCore.Connection_Close() ' System
            dbmImaging.Connection_Close() 'System
            If (manager IsNot Nothing) Then manager.Disconnect()
        End Try

        Return Respuesta
    End Function

    <WebMethod()> _
    Public Function WriteKey(ByVal nEntidad As Short, ByVal nProyecto As Short, ByVal nEsquema As Short, ByVal nKey As String, ByVal nNombreImagen As String, ByVal nCarpeta As String) As PR_ExistKey
        Dim dbmCore As New DBCore.DBCoreDataBaseManager(Program.CoreConnectionString)
        Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString)
        Dim manager As FileProviderManager = Nothing
        Dim Respuesta As New PR_ExistKey
        Dim Ruta As String = ""
        Dim Compresion As ImageManager.EnumCompression

        Try
            dbmCore.Connection_Open(1) ' System
            dbmImaging.Connection_Open(1) ' System

            Dim FileKeyDataTable = dbmCore.SchemaImaging.PA_File_Key_2.DBExecute(nEntidad, nProyecto, nEsquema, Nothing, nKey)
            Dim RutaDatable = dbmImaging.SchemaConfig.TBL_Parametro_Sistema.DBFindByNombre_Parametro_Sistemafk_Entidadfk_Proyecto("RutaServidor", nEntidad, nProyecto)

            If RutaDatable.Rows.Count > 0 Then
                Ruta = RutaDatable(0).Valor_Parametro_Sistema
            End If

            Respuesta.Result = True
            Respuesta.Message = ""

            If FileKeyDataTable.Rows.Count > 0 Then
                Respuesta.Encontrado = True
                Respuesta.Identificador = nKey.ToString()
                Respuesta.Folios = FileKeyDataTable(0).Folios_Documento_File
                Respuesta.Size = FileKeyDataTable(0).Tamaño_Imagen_File
                Respuesta.Tipo = FileKeyDataTable(0).fk_Content_Type
            Else
                Respuesta.Encontrado = False
            End If

            If (FileKeyDataTable(0).fk_Formato_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                Compresion = ImageManager.EnumCompression.Ccitt4
            Else
                Compresion = ImageManager.EnumCompression.Lzw
            End If

            If Respuesta.Encontrado = True Then
                Dim Identificador As Guid
                Dim FileName As String = ""

                If Not (Guid.TryParse(FileKeyDataTable(0).File_Unique_Identifier.ToString(), Identificador)) Then
                    Throw New Exception("Token no válido, " & FileKeyDataTable(0).File_Unique_Identifier.ToString())
                End If
                ' Leer el File
                Dim FileDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByFile_Unique_Identifier(Identificador)
                If (FileDataTable.Count = 0) Then Throw New Exception("No se encontro un registro asociado al Token: " & Identificador.ToString())
                Dim FileRow = FileDataTable(0)

                If (FileRow.Es_Anexo) Then
                    manager = New FileProviderManager(FileRow.fk_Anexo, dbmImaging, 0)
                Else
                    manager = New FileProviderManager(FileRow.fk_Expediente, FileRow.fk_Folder, dbmImaging, 0)
                End If

                manager.Connect()

                Dim TempPath As String = Server.MapPath("~/_temporal")

                TempPath = TempPath.TrimEnd("/"c) & "/"

                If Not Directory.Exists(Ruta + nCarpeta) Then
                    Directory.CreateDirectory(Ruta + nCarpeta)
                End If

                Dim FileNames As New List(Of String)

                If (FileDataTable(0).Es_Anexo) Then
                    ' Es un anexo
                    Dim Folios = manager.GetFolios(FileRow.fk_Anexo)

                    If (Folios = 0) Then Throw New Exception("No se encontro folios asociados al anexo del Token: " & Identificador.ToString())

                    Dim Image As Byte() = Nothing
                    Dim Thumbnile As Byte() = Nothing
                    manager.GetFolio(FileRow.fk_Anexo, CShort(1), Image, Thumbnile)
                    FileName = TempPath & Identificador.ToString() & FileDataTable(0).fk_Content_Type

                    Using ImageFile As New FileStream(FileName, FileMode.Create)
                        ImageFile.Write(Image, 0, Image.Length)
                        ImageFile.Close()
                    End Using
                Else
                    ' Es un file
                    Dim Folios = manager.GetFolios(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)

                    If (Folios = 0) Then Throw New Exception("El documento se encuentra en proceso de transferencia, por favor intente mas tarde: " & Identificador.ToString())

                    For folio As Short = 1 To Folios
                        Dim Image As Byte() = Nothing
                        Dim Thumbnile As Byte() = Nothing
                        manager.GetFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, folio, Image, Thumbnile)


                        FileName = TempPath & Guid.NewGuid().ToString() & FileDataTable(0).fk_Content_Type

                        Select Case FileRow.fk_Content_Type
                            Case ".gif", ".jpg", ".png", ".bmp"
                                FileName = TempPath & Guid.NewGuid().ToString() & FileRow.fk_Content_Type

                                Using fs As New FileStream(FileName, FileMode.CreateNew)
                                    fs.Write(Image, 0, Image.Length)
                                End Using

                            Case ".tif", ".tiff"
                                FileName = TempPath & Guid.NewGuid().ToString() & FileRow.fk_Content_Type

                                ImageManager.Save(New FreeImageAPI.FreeImageBitmap(New MemoryStream(Image)), FileName, "", ImageManager.EnumFormat.Tiff, Compresion, False, TempPath)
                        End Select

                        FileNames.Add(FileName)
                    Next

                    FileName = Ruta & nCarpeta & "\" & nNombreImagen & FileRow.fk_Content_Type

                    '-------------------------------------------------------------------------
                    ImageManager.Save(FileNames, FileName, "", Utilities.GetEnumFormat(FileRow.fk_Content_Type.ToString()), Compresion, False, TempPath, True)
                    '-------------------------------------------------------------------------

                    'Eliminar temporal

                End If
            End If

        Catch ex As Exception
            Respuesta.Result = False
            Respuesta.Message = ex.Message
        Finally
            dbmCore.Connection_Close() ' System
            dbmImaging.Connection_Close() 'System
            If (manager IsNot Nothing) Then manager.Disconnect()
        End Try

        Return Respuesta
    End Function

#End Region

End Class