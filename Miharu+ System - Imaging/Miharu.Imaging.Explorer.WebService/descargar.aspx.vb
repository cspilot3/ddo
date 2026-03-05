Imports System.IO
Imports Miharu.FileProvider.Library

Partial Public Class descargar
    Inherits Page

#Region " Declaraciones "

    Private Token As String

#End Region

#Region " Eventos "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Config_Page()
        Else
            Load_Data()
        End If
    End Sub

    Protected Sub DescargarLinkButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DescargarLinkButton.Click
        ErrorLabel.Text = ""

        Try
            DescargarFile(Me.Token)
        Catch ex As Exception
            ErrorLabel.Text = ex.Message
        End Try
    End Sub

#End Region

#Region " Metodos "

    Private Sub Config_Page()
        If Request.QueryString.Count > 0 Then
            Token = Request.QueryString("Token")
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

    Private Sub DescargarFile(ByVal nToken As String)
        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Dim manager As FileProviderManager = Nothing
        Dim FileName As String = ""

        Try
            dbmCore = New DBCore.DBCoreDataBaseManager(Program.CoreConnectionString)
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.ImagingConnectionString)

            dbmCore.Connection_Open(0)
            dbmImaging.Connection_Open(0)

            Dim Identificador As Guid

            If Not (Guid.TryParse(nToken, Identificador)) Then
                Throw New Exception("Token no válido, " & Token)
            End If

            ' Leer el File
            Dim FileDataTable = dbmCore.SchemaImaging.TBL_File.DBFindByFile_Unique_Identifier(Identificador)
            If (FileDataTable.Count = 0) Then Throw New Exception("No se encontro un registro asociado al Token: " & Token)
            Dim FileRow = FileDataTable(0)

            If (FileRow.Es_Anexo) Then
                manager = New FileProviderManager(FileRow.fk_Anexo, dbmImaging, 0)
            Else
                manager = New FileProviderManager(FileRow.fk_Expediente, FileRow.fk_Folder, dbmImaging, 0)
            End If

            manager.Connect()

            Dim TempPath As String = Server.MapPath("~/_temporal")

            TempPath = TempPath.TrimEnd("\"c) & "\"

            If (FileDataTable(0).Es_Anexo) Then
                ' Es un anexo
                Dim Folios = manager.GetFolios(FileRow.fk_Anexo)

                If (Folios = 0) Then Throw New Exception("No se encontro folios asociados al anexo del Token: " & Token)

                Dim Image As Byte() = Nothing
                Dim Thumbnile As Byte() = Nothing
                manager.GetFolio(FileRow.fk_Anexo, CShort(1), Image, Thumbnile)
                FileName = TempPath & Guid.NewGuid().ToString() & FileDataTable(0).fk_Content_Type

                Using ImageFile As New FileStream(FileName, FileMode.Create)
                    ImageFile.Write(Image, 0, Image.Length)
                    ImageFile.Close()
                End Using
            Else
                ' Es un file
                Dim Folios = manager.GetFolios(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)

                If (Folios = 0) Then Throw New Exception("El documento se encuentra en proceso de transferencia, por favor intente mas tarde: " & Token)

                Dim Image As Byte() = Nothing
                Dim Thumbnile As Byte() = Nothing
                manager.GetFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, CShort(1), Image, Thumbnile)
                FileName = TempPath & Guid.NewGuid().ToString() & FileDataTable(0).fk_Content_Type

                Using ImageFile As New FileStream(FileName, FileMode.Create)
                    ImageFile.Write(Image, 0, Image.Length)
                    ImageFile.Close()
                End Using
            End If

            Dim ExportFilename As String = "File-" & Now.ToString("yyyyMMddhhmmss") & FileDataTable(0).fk_Content_Type
            Dim Content_Type = dbmCore.SchemaImaging.TBL_Content_Type.DBGet(FileDataTable(0).fk_Content_Type)

            DownloadFile(FileName, ExportFilename, Content_Type(0).ContentType)

        Catch
            Throw
        Finally
            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            If (manager IsNot Nothing) Then manager.Disconnect()
        End Try
    End Sub

    Private Sub DownloadFile(ByVal nFilename As String, ByVal nExportFilename As String, ByVal nContentType As String)
        Response.Clear()
        Response.ClearContent()
        Response.ClearHeaders()

        Response.AddHeader("content-disposition", "attachment; filename=" & nExportFilename)
        Response.ContentType = nContentType
        Response.TransmitFile(nFilename)

        Response.End()
    End Sub

#End Region

#Region " Funciones "

#End Region

End Class