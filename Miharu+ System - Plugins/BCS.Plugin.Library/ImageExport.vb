Imports Slyg.Tools.Imaging
Imports Slyg.Tools.Imaging.FreeImageAPI.FreeImageBitmap
Imports Slyg.Tools.Imaging.ImageManager
Imports Miharu.FileProvider.Library
Imports DBImaging.SchemaCore
Imports DBImaging.SchemaSecurity
Imports System.IO

<Serializable()> _
Public Class ImageExport

    Private dbmImaging As DBImaging.DBImagingDataBaseManager
    Private Manager As FileProviderManager = Nothing

    Public Sub Init(servidor As CTA_ServidorSimpleType, centro As CTA_Centro_ProcesamientoSimpleType, connectionString As String, usuario As Integer)
        dbmImaging = New DBImaging.DBImagingDataBaseManager(connectionString)
        dbmImaging.Connection_Open(usuario)

        Manager = New FileProviderManager(servidor, centro, dbmImaging, usuario)
        Manager.Connect()
    End Sub

    Public Sub Unload()
        Manager.Disconnect()
        dbmImaging.Connection_Close()
    End Sub

    Public Sub Save(FileNames As List(Of String), FileName As String, nCompresion As ImageManager.EnumCompression, nFileFolderName As String)
        ImageManager.Save(FileNames, FileName, "", ImageManager.EnumFormat.Tiff, nCompresion, False, nFileFolderName, True)
    End Sub

    Public Sub Save(nInputImages As List(Of FreeImageAPI.FreeImageBitmap), nOutputFileName As String, nSuffixFormat As String, nFormat As EnumFormat, nCompression As EnumCompression, nSinglePage As Boolean, nTempPath As String)
        ImageManager.Save(nInputImages, nOutputFileName, nSuffixFormat, nFormat, nCompression, nSinglePage, nTempPath)
    End Sub


    Public Sub Save(FileName As String, Imagen As Byte())
        Using fs = New FileStream(FileName, FileMode.Create)
            fs.Write(Imagen, 0, Imagen.Length)
            fs.Close()
        End Using
    End Sub

    Public Sub GetFolio(nAnexo As Long, nFolio As Short, ByRef nImagen As Byte())
        Manager.GetFolio(nAnexo, nFolio, nImagen, Nothing)
    End Sub

    Public Sub GetFolio(nExpediente As Long, nFolder As Short, nFile As Short, nVersion As Short, nFolio As Short, ByRef nImagen As Byte())
        Manager.GetFolio(nExpediente, nFolder, nFile, nVersion, nFolio, nImagen, Nothing)
    End Sub

End Class
