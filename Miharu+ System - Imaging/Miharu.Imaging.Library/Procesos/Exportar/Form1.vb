Imports System.IO
Imports Miharu.FileProvider.Library

Public Class Form1
    Public Shared FileNamesCons As New List(Of String)
    Private Sub BuscarCarpetaButton_Click(sender As System.Object, e As System.EventArgs) Handles BuscarCarpetaButton.Click

        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
        Dim manager As FileProviderManager = Nothing

        Dim Selector As New FolderBrowserDialog()

        Selector.SelectedPath = CarpetaSalidaTextBox.Text
        If (Selector.ShowDialog() = DialogResult.OK) Then
            Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
        End If

        Dim OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
        Dim FilesNames = Directory.GetFiles(OutputFolder, "*.txt")
        If FilesNames.Length > 0 Then
            Try
                For Each Files In FilesNames
                    Dim sr = New StreamReader(Files)
                    Dim linea As String
                    Dim lineas As List(Of String) = New List(Of String)
                    Dim lineaActual As String()

                    Dim columnnames = sr.ReadLine().Split(Convert.ToChar(vbTab))

                    Dim dt As DataTable = Nothing
                    While Not sr.EndOfStream
                        linea = sr.ReadLine()
                        lineas.Add(linea)
                    End While
                    dt = New DataTable()

                    For Each col As String In columnnames
                        dt.Columns.Add(col)
                    Next
                    For p As Integer = 1 To lineas.Count - 1
                        lineaActual = lineas(p).Split(Convert.ToChar(vbTab))

                        Dim row As DataRow = dt.NewRow()
                        row.ItemArray = lineaActual
                        Dim fk_expediente = row.Item("fk_expediente")
                        Dim fk_Folder = row.Item("fk_Folder")
                        Dim fk_File = row.Item("fk_File")
                        Dim id_Version = row.Item("id_Version")
                        Dim Llave_01 As String = ""
                        Dim Llave_02 As String = ""
                        If (Not String.IsNullOrEmpty(CStr(row.Item("Llave_01")))) Then
                            Llave_01 = CStr(row.Item("Llave_01"))
                        End If
                        If (Not String.IsNullOrEmpty(CStr(row.Item("Llave_02")))) Then
                            Llave_02 = CStr(row.Item("Llave_02"))
                        End If
                        Dim fk_Servidor = row.Item("fk_ServidorStorage")

                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                        dbmImaging.Connection_Open(1)
                        dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                        dbmCore.Connection_Open(1)
                        Try

                            Dim servidore = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, CShort(fk_Servidor))
                            Dim sevidores = servidore(0).ToCTA_ServidorSimpleType()
                            Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                            manager = New FileProviderManager(sevidores, centro, dbmImaging, Program.Sesion.Usuario.id)
                            manager.Connect()


                            Dim Folios = manager.GetFolios(CLng(fk_expediente), CShort(fk_Folder), CShort(fk_File), CShort(id_Version))
                            Dim FileNames As New List(Of String)
                            Dim FileName As String = Nothing
                            Dim FileNameAux As String = Nothing
                            Dim ExtensionAux As String = String.Empty

                            If Folios > 0 Then
                                For folio As Short = 1 To Folios
                                    Dim Imagen() As Byte = Nothing
                                    Dim Thumbnail() As Byte = Nothing

                                    manager.GetFolio(CLng(fk_expediente), CShort(fk_Folder), CShort(fk_File), CShort(id_Version), folio, Imagen, Thumbnail)
                                    FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                                    FileNames.Add(FileName)
                                    FileNamesCons.Add(FileName)
                                Next
                            End If
                            If FileNames.Count > 0 Then
                                Dim Mensaje As String = CStr(fk_expediente) & vbTab & CStr(fk_Folder) & vbTab & CStr(fk_File) & vbTab & CStr(id_Version) & vbTab & Llave_01 & vbTab & Llave_02 & vbTab & "Imagen :  SI"
                                log(CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\", "lOG", Mensaje)
                            Else
                                Dim Mensaje As String = CStr(fk_expediente) & vbTab & CStr(fk_Folder) & vbTab & CStr(fk_File) & vbTab & CStr(id_Version) & vbTab & Llave_01 & vbTab & Llave_02 & vbTab & "Imagen :  NO"
                                log(CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\", "lOG", Mensaje)
                            End If

                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                            If (manager IsNot Nothing) Then manager.Disconnect()
                        Catch ex As Exception
                            Dim Mensaje As String = CStr(fk_expediente) & vbTab & CStr(fk_Folder) & vbTab & CStr(fk_File) & vbTab & CStr(id_Version) & vbTab & CStr(Llave_01) & vbTab & CStr(Llave_02) & vbTab & "Imagen :  NO"
                            log(CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\", "ErroresEX:" & ex.Message, Mensaje)
                        End Try
                    Next
                    sr.Close()
                Next
            Catch ex As Exception
                log(CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\", "ErroresEX:" & ex.Message, ex.Message)
            End Try
        End If
    End Sub
    Public Shared Sub log(ByVal logFileDirectory As String, ByVal filenamePrefix As String, ByVal message As String)

        If Not Directory.Exists(logFileDirectory) Then
            Directory.CreateDirectory(logFileDirectory)
        End If

        Dim filepath As String = logFileDirectory & filenamePrefix & ".txt"
        Dim logMessage As String = message

        If Not File.Exists(filepath) Then

            Using sw As StreamWriter = File.CreateText(filepath)
                sw.WriteLine(logMessage)
            End Using
        Else

            Using sw As StreamWriter = File.AppendText(filepath)
                sw.WriteLine(logMessage)
            End Using
        End If
    End Sub
End Class

