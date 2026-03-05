Imports System.Threading
Imports DBImaging
Imports Miharu.FileProvider.Library
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library.Config

' LOL
Public Class InstanciaAProcesarClinetesEnLog

#Region " Declaraciones "

    Private _caller As ProcesaClientesEnLog
    Private _outputFolder As String
    Private _Llave_01 As String
    Private _Llave_02 As String
    Private _FilesUnArchivo As DataTable
    Private _IndexEnTablaRta As Integer
    Private _Files As Integer
    Private _Folios_Esperados As Integer
    Private _Folios_Recuperables As Integer
    Private _Folios_Errados As Integer
    Private _CreaPdf As Boolean
    Private _threadIndex As Integer
    Private _cts As CancellationTokenSource
    Private formato As Slyg.Tools.Imaging.ImageManager.EnumFormat
    Private Compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression
    Private BloqueoConcurrencia As New Object

#End Region

#Region " Constructor "

    Public Sub New(caller As ProcesaClientesEnLog, outputFolder As String, Llave_01 As String, Llave_02 As String, IndexEnTablaRta As Integer)
        _caller = caller
        _outputFolder = outputFolder
        _Llave_01 = Llave_01
        _Llave_02 = Llave_02
        _IndexEnTablaRta = IndexEnTablaRta
        _FilesUnArchivo = Nothing
        _Files = 0
        _Folios_Esperados = 0
        _Folios_Recuperables = 0
        _Folios_Errados = 0
        _CreaPdf = False
    End Sub

#End Region

    Public Function getSumaDeLlaves() As String
        Return _Llave_01 '+ "_" + _Llave_02
    End Function

    Public Function GetFoliosEsperados() As Integer
        Return _Folios_Esperados
    End Function

    Public Function GetFoliosRecuperables() As Integer
        Return _Folios_Recuperables
    End Function

    Public Function GetFoliosErrados() As Integer
        Return _Folios_Errados
    End Function

    Public Function isCreaPdf() As Boolean
        Return _CreaPdf
    End Function

    Public Sub setCancelationTokenSource(cts As CancellationTokenSource)
        _cts = cts
    End Sub

    Public Sub setFilesUnArchivo(FilesUnArchivo As DataTable)
        If FilesUnArchivo Is Nothing Then
            _FilesUnArchivo = New DataTable()
        Else
            _FilesUnArchivo = FilesUnArchivo
        End If
        _Files = _FilesUnArchivo.Rows.Count
    End Sub

    Public Sub ThreadPoolCallback(threadContext As Object)
        _threadIndex = CType(threadContext, Integer)

        procesarInstanciaCargue()
    End Sub

    Private Sub procesarInstanciaCargue()
        Dim FileNames As New List(Of String)
        Dim dbmImaging As DBImagingDataBaseManager = Nothing
        Dim manager As FileProviderManager = Nothing
        Dim _StrArchivoLog As String
        _StrArchivoLog = Me._outputFolder + "\Log_REPORTE_Exportacion" + Date.Now.ToString("yyyyMMdd") + "_" + "prueba" + ".dat"
        EscribeLog(_StrArchivoLog, "Inicio", True, False)
        Try
            If _FilesUnArchivo IsNot Nothing And Not _cts.IsCancellationRequested Then
                For index As Integer = 0 To _FilesUnArchivo.Rows.Count - 1
                    Dim actualFile = _FilesUnArchivo.Rows(index)
                    Dim valorEntidadServidor As Short = CShort(actualFile.Item("fk_Entidad_Servidor"))
                    Dim valorServidor As Short = CShort(actualFile.Item("fk_Servidor"))
                    Dim valorExpediente As Long = CLng(actualFile.Item("fk_Expediente"))
                    Dim valorFolder As Short = CShort(actualFile.Item("fk_Folder"))
                    Dim valorFile As Short = CShort(actualFile.Item("id_File"))
                    Dim valorVersion As Short = CShort(actualFile.Item("id_Version"))

                    dbmImaging = New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    'Dim servidore = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, valorServidor)
                    Dim servidore = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(valorEntidadServidor, valorServidor)
                    Dim sevidores = servidore(0).ToCTA_ServidorSimpleType()
                    Dim servidor = CType(sevidores, DBImaging.SchemaCore.CTA_ServidorSimpleType)
                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                    manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                    manager.Connect()

                    Dim Folios As Short

                    SyncLock BloqueoConcurrencia
                        Folios = manager.GetFolios(valorExpediente, valorFolder, valorFile, valorVersion)
                    End SyncLock

                    _Folios_Esperados += Folios

                    If Folios > 0 Then
                        Dim FileName As String = Nothing
                        For folio As Short = 1 To Folios
                            Dim bmp As System.Drawing.Image = Nothing
                            Dim Imagen() As Byte = Nothing
                            Dim Thumbnail() As Byte = Nothing

                            GC.Collect()

                            Try
                                SyncLock BloqueoConcurrencia
                                    manager.GetFolio(valorExpediente, valorFolder, valorFile, valorVersion, folio, Imagen, Thumbnail)
                                End SyncLock

                                bmp = Bitmap.FromStream(New MemoryStream(Imagen))
                                'If Program.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF Then
                                '    FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & ".pdf"
                                'Else
                                FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                                'FileName = Program.AppPath & Program.TempPath & valorExpediente.ToString() & valorFolder.ToString() & valorFile.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                                'End If

                                FileNames.Add(FileName)

                                Dim qualityParam As EncoderParameter = New EncoderParameter(Encoder.Quality, 20)
                                Dim jpegCodec As ImageCodecInfo = GetEncoderInfo("image/jpeg")
                                Dim encoderParams As EncoderParameters = New EncoderParameters(1)
                                encoderParams.Param(0) = qualityParam
                                bmp.Save(FileName, jpegCodec, encoderParams)
                                _Folios_Recuperables += 1
                            Catch ex As Exception
                                SyncLock BloqueoConcurrencia
                                    EscribeLog(_StrArchivoLog, "Error Generacion Reportes: " + valorExpediente.ToString() + ", " + valorFile.ToString() + ", " + valorEntidadServidor.ToString() + ", " + valorServidor.ToString() + ": " + ex.Message, False, True)
                                End SyncLock

                                _Folios_Errados += 1
        End Try
                        Next
                    End If

                    If (manager IsNot Nothing) Then manager.Disconnect()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                Next
            End If
        Finally
            If (manager IsNot Nothing) Then manager.Disconnect()
            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
        End Try


        _CreaPdf = (_Folios_Esperados > 0 And _Folios_Esperados = _Folios_Recuperables)
        If _CreaPdf Then
            'Dim auxNombre As String = _outputFolder + "\" + _Llave_01 + "_" + _Llave_02 + ".pdf"
            'Dim auxNombre As String = _outputFolder + "\" + _Llave_01 + "_" + _Llave_02 + Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
            Dim auxNombre As String
            If Program.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF Then
                auxNombre = _outputFolder + "\" + _Llave_01 + ".pdf"
            Else
                auxNombre = _outputFolder + "\" + _Llave_01 + Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
            End If

            ExportAllFillesInPdf(FileNames, auxNombre)
            BorrarTemp(FileNames)
        End If
        _caller.reportarRespuesta(_IndexEnTablaRta, _Files, _Folios_Esperados, _Folios_Recuperables, _Folios_Errados, _CreaPdf, _threadIndex)
    End Sub

    Private Sub ExportAllFillesInPdf(ByVal FileNamesCons As List(Of String), ByVal FileName As String)
        Try
            If FileNamesCons.Count > 0 Then
                Try
                    If File.Exists(FileName) Then
                        File.Delete(FileName)
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error eliminando el archivo en la ruta: " & FileName & " - Error: " & ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try


                If Program.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF Then
                    formato = ImageManager.EnumFormat.Pdf
                Else
                    formato = Utilities.GetEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                End If


                'ImageManager.Save(FileNamesCons, FileName, "", ImageManager.EnumFormat.Pdf, ImageManager.EnumCompression.Lzw, False, Program.AppPath & Program.TempPath, True)
                ImageManager.Save(FileNamesCons, FileName, "", formato, ImageManager.EnumCompression.Lzw, False, Program.AppPath & Program.TempPath, True)
            End If

        Catch ex As Exception
            MessageBox.Show("Se ha presentado un error al exportar el Pdf: " & ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetEncoderInfo(ByVal mimeType As String) As ImageCodecInfo
        Dim encoders As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders()

        For Each encoder In encoders
            If encoder.MimeType = mimeType Then Return encoder
        Next
        Return Nothing
    End Function

    Private Sub EscribeLog(pathStrFile As String, StrLine As String, Optional CrearFile As Boolean = False, Optional LeerFile As Boolean = False)
        Dim modeFile As FileMode = Nothing
        Dim modeFile_2 As FileMode = Nothing
        Dim strMessageComplete As String = StrLine + Environment.NewLine
        Try
            If (CrearFile) Then
                modeFile = FileMode.CreateNew
                modeFile_2 = CType(FileAccess.Write, FileMode)
                Using fs As New FileStream(pathStrFile, modeFile)
                    Using w As New BinaryWriter(fs)
                        w.Write("Date : " + Date.Now.ToString() + " " + strMessageComplete)
                    End Using
                End Using
            ElseIf (LeerFile) Then
                modeFile = FileMode.Append
                Using fs As New FileStream(pathStrFile, modeFile)
                    Using w As New BinaryWriter(fs)
                        w.Write(" Date : " + Date.Now.ToString() + " " + strMessageComplete)
                    End Using
                End Using
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BorrarTemp(ByVal FileNamesCons As List(Of String))

        For Each Fil In FileNamesCons
            Try
                File.Delete(Fil)
            Catch ex As Exception

            End Try
        Next

    End Sub


End Class