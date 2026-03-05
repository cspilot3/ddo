Imports System.IO
Imports Miharu.FileProvider.Library
'Imports Miharu.Web.Controls
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library.Config
Imports Miharu.Web.Controls

Public Class VisorPdf
    Inherits Page

#Region " Declaraciones "

    Private Token As String

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Config_Page()

        Mostrar()
    End Sub

    Private Sub Config_Page()
        If Request.QueryString.Count > 0 Then
            Token = Request.QueryString("token")
        Else
            Return
        End If

        Session("Token") = Token

        Clear_Cache()
    End Sub

    Private Sub Load_Data()
        If (Session("Token") IsNot Nothing AndAlso Session("Token").ToString() <> "") Then
            Token = Session("Token").ToString()
        End If
    End Sub

    Private Sub Clear_Cache()
        Try
            Dim FileNames = Directory.GetFiles(Server.MapPath("~/_temporal"), "*")

            For Each FileName In FileNames
                Dim FileProps = New FileInfo(FileName)
                Dim Diferencia = DateTime.Now - FileProps.LastAccessTime

                If (Diferencia.Minutes > 30) Then
                    Try : File.Delete(FileName) : Catch : End Try
                End If
            Next
        Catch : End Try
    End Sub

    Private Sub Mostrar()
        If (Token <> "") Then
            Try
                'Dim ImageUrl As List(Of String)
                Dim ImageUrl As String

                ImageUrl = DescargarImagenes(Token)

                SavePDF(ImageUrl)

            Catch ex As Exception
                'ShowAlert(ex.Message & " - " & ex.StackTrace, MsgBoxIcon.IconError)
                Response.Write("<script>alert('" & ex.Message & "'); </script>")
            End Try
        Else
            'ShowAlert("No se definio un Token de entrada", MsgBoxIcon.IconError)
        End If
    End Sub

    Public Sub SavePDF(ImageUrl As String)
        Try
            If (ImageUrl.Length > 0) Then
                Dim TempPath = Server.MapPath("~\_temporal").TrimEnd("\"c) & "\"
                Dim SaveFilePath As String = ""
                Dim ExportFilename As String = ImageUrl & ".pdf"
                Dim Content_Type As String = ""

                Content_Type = "application/pdf"
                SaveFilePath = TempPath & ExportFilename

                'ImageManager.Save(ImageUrl, SaveFilePath, "", ImageManager.EnumFormat.Pdf, ImageManager.EnumCompression.Lzw, False, TempPath, True)

                ShowFile(SaveFilePath, ExportFilename, Content_Type)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ShowFile(SaveFilePath As String, ExportFilename As String, Content_Type As String)
        Try

            Response.Clear()
            Response.ClearContent()
            Response.ClearHeaders()
            Response.ContentType = Content_Type        
            Response.WriteFile(SaveFilePath)
            Response.End()

        Catch ex As Exception
        End Try
    End Sub

    Public Function DescargarImagenes(ByVal nToken As String) As String
        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Dim manager As FileProviderManager = Nothing
        Dim FileNames As New List(Of String)
        Dim ExFilename As String = "Imagen-" & Now.ToString("yyyyMMddhhmmss")

        Try
            'traza = "Crear core"
            dbmCore = New DBCore.DBCoreDataBaseManager(Program.CoreConnectionString)
            'traza = "Crear imaging"
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString)

            'traza = "Abrir core"
            dbmCore.Connection_Open(0)

            'traza = "Abrir imaging"
            dbmImaging.Connection_Open(0)

            Dim Identificador As Guid

            'traza = "Convertir token"
            If Not (Guid.TryParse(nToken, Identificador)) Then
                Throw New Exception("Token no válido, " & nToken)
            End If

            ' Leer el File
            'traza = "Leer file"
            Dim FileDataTable As New DBCore.SchemaImaging.TBL_FileDataTable

            FileDataTable.Columns.Remove("Eliminado")
            FileDataTable.AcceptChanges()

            FileDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByFile_Unique_Identifier(Identificador)
            If (FileDataTable.Count = 0) Then Throw New Exception("No se encontro un registro asociado al Token: " & nToken)
            Dim FileRow = FileDataTable(0)

            If (FileRow.Es_Anexo) Then
                'traza = "Crear manager file server"
                manager = New FileProviderManager(FileRow.fk_Anexo, dbmImaging, 0)
            Else
                'traza = "Crear manager database server"
                'If (Program.EsExterno) Then
                '    manager = New FileProviderManager(FileRow.fk_Expediente, FileRow.fk_Folder, dbmImaging, 0, Program.Remoting)
                'Else
                '    manager = New FileProviderManager(FileRow.fk_Expediente, FileRow.fk_Folder, dbmImaging, 0)
                'End If
                If (Program.EsExterno) Then
                    manager = New FileProviderManager(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, dbmImaging, 0, Program.Remoting)
                Else
                    manager = New FileProviderManager(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, dbmImaging, 0)
                End If
            End If

            'traza = "Conectar file provider"
            manager.Connect()

            Dim TempPath As String = Server.MapPath("~/_temporal")

            TempPath = TempPath.TrimEnd("\"c) & "\"

            Dim Folios As Short

            If FileRow.Es_Anexo Then
                Folios = manager.GetFolios(FileRow.fk_Anexo)
            Else
                Folios = manager.GetFolios(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)
            End If

            'Throw New Exception("Folios, " & Folios)

            If Folios = 0 Then



                'Todo: Traer Folder
                Dim FolderRow = GetfolderRow(FileRow.fk_Expediente, FileRow.fk_Folder, dbmImaging)
                'Todo: Buscar los servidores diferentes al actual.
                Dim Servidores = GetServidores(FolderRow, dbmImaging)
                Dim OtrosServidores = Servidores.Select("id_Servidor <>" & FolderRow.fk_Servidor)

                'Todo En FileProviderManager crear Nuevo metodo alque se le envie el servidor adicional y lo cambie
                'TODO: Metodo para Remoting
                'OK
                'Todo: Recorrer Servidores adicionales
                For Each servidor As DBImaging.SchemaCore.CTA_ServidorRow In OtrosServidores

                    If (manager IsNot Nothing) Then manager.Disconnect()

                    If (Program.EsExterno) Then
                        manager = New FileProviderManager(servidor, dbmImaging, 0, Program.Remoting)
                    Else
                        manager = New FileProviderManager(servidor, dbmImaging, 0)
                    End If

                    manager.Connect()

                    If FileRow.Es_Anexo Then
                        Folios = manager.GetFolios(FileRow.fk_Anexo)
                    Else
                        Folios = manager.GetFolios(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)
                    End If

                    If Folios > 0 Then
                        Exit For
                    End If

                Next


            End If

            If (FileRow.Es_Anexo) Then
                ' Es un anexo

                If (Folios = 0) Then Throw New Exception("No se encontro folios asociados al anexo del Token: " & nToken)


                For folio = 1 To Folios
                    Dim Image As Byte() = Nothing
                    Dim Thumbnile As Byte() = Nothing
                    manager.GetFolio(FileRow.fk_Anexo, CShort(1), Image, Thumbnile)

                    Dim FileName = TempPath & Guid.NewGuid().ToString() & FileRow.fk_Content_Type
                    FileNames.Add(FileName)
                    ImageManager.Save(New FreeImageAPI.FreeImageBitmap(New MemoryStream(Image)), FileName, "", Utilities.GetEnumFormat(FileRow.fk_Content_Type), ImageManager.EnumCompression.None, False, TempPath)
                Next
            Else
                ' Es un file
                'traza = "Leer los folios"

                If (Folios = 0) Then Throw New Exception("El documento se encuentra en proceso de transferencia, por favor intente mas tarde: " & nToken)
                Dim tempFileName As String
                tempFileName = TempPath & Guid.NewGuid().ToString()

                For folio As Short = 1 To Folios
                    Dim Imagen As Byte() = Nothing
                    Dim Thumbnile As Byte() = Nothing
                    manager.GetFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, folio, Imagen, Thumbnile)

                    Dim FileName As String

                    FileName = tempFileName & folio.ToString("-0000") & ".jpg"

                    'Using ms = New MemoryStream(Imagen)
                    '    Using bm = New FreeImageAPI.FreeImageBitmap(ms)
                    '        ImageManager.Save(bm, FileName, "", ImageManager.EnumFormat.Jpeg, ImageManager.EnumCompression.Ccitt4, False, TempPath)
                    '    End Using
                    'End Using


                    'Select Case FileRow.fk_Content_Type
                    '    Case ".gif", ".jpg", ".png", ".bmp"
                    'FileName = TempPath & Guid.NewGuid().ToString() & FileRow.fk_Content_Type

                    Using fs As New FileStream(FileName, FileMode.CreateNew)
                        fs.Write(Imagen, 0, Imagen.Length)
                    End Using

                    '    Case Else
                    '        'FileName = TempPath & Guid.NewGuid().ToString() & ".png"
                    '        'ImageManager.Save(New FreeImageAPI.FreeImageBitmap(New MemoryStream(Image)), FileName, "", ImageManager.EnumFormat.Png, ImageManager.EnumCompression.None, False, TempPath)
                    '        FileName = TempPath & Guid.NewGuid().ToString() & ".tiff"
                    '        ImageManager.Save(New FreeImageAPI.FreeImageBitmap(New MemoryStream(Imagen)), FileName, "", ImageManager.EnumFormat.Tiff, ImageManager.EnumCompression.Lzw, False, TempPath)

                    'End Select

                    FileNames.Add(FileName)
                Next
                Dim ExportFilename As String = TempPath & ExFilename
                ExportFilename &= ".pdf"

                ImageManager.CreatePdf(Folios, tempFileName, ExportFilename)
            End If
        Catch
            Throw
        Finally
            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            If (manager IsNot Nothing) Then manager.Disconnect()
        End Try

        Return ExFilename
    End Function

    Private Function GetServidores(ByVal nExpediente As Long, ByVal nFolder As Short) As DBImaging.SchemaCore.CTA_ServidorDataTable
        Dim dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString)

        ' Leer el folder
        Dim FolderDataTable = dbmImaging.SchemaCore.CTA_Folder.DBFindByfk_Expedientefk_Folder(nExpediente, nFolder)
        If (FolderDataTable.Count = 0) Then Throw New Exception("No se encontró el Folder")
        Dim FolderRow = FolderDataTable(0)

        ' Leer el servidor
        Return dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(FolderRow.fk_Entidad_Servidor, Nothing)
    End Function

    Private Function GetfolderRow(ByVal nExpediente As Long, ByVal nFolder As Short, dbmImaging As DBImaging.DBImagingDataBaseManager) As DBImaging.SchemaCore.CTA_FolderRow

        ' Leer el folder
        Return dbmImaging.SchemaCore.CTA_Folder.DBFindByfk_Expedientefk_Folder(nExpediente, nFolder)(0)

    End Function

    Private Function GetServidor(ByVal nFolder As DBImaging.SchemaCore.CTA_FolderRow, dbmImaging As DBImaging.DBImagingDataBaseManager) As DBImaging.SchemaCore.CTA_ServidorRow

        ' Leer el servidor
        Return dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(nFolder.fk_Entidad_Servidor, nFolder.fk_Servidor)(0)
    End Function

    Private Function GetServidores(ByVal nFolder As DBImaging.SchemaCore.CTA_FolderRow, dbmImaging As DBImaging.DBImagingDataBaseManager) As DBImaging.SchemaCore.CTA_ServidorDataTable

        ' Leer el folder
        Dim FolderDataTable = dbmImaging.SchemaCore.CTA_Folder.DBFindByfk_Expedientefk_Folder(nFolder.fk_Expediente, nFolder.fk_Folder)
        If (FolderDataTable.Count = 0) Then Throw New Exception("No se encontró el Folder")
        Dim FolderRow = FolderDataTable(0)

        ' Leer el ser vidor
        Return dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(FolderRow.fk_Entidad_Servidor, Nothing)
    End Function

End Class