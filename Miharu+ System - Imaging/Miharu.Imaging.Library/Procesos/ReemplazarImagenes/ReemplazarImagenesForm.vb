Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports System.IO
Imports Miharu.FileProvider.Library
Imports Slyg.Tools.Imaging
Imports Miharu.Desktop.Library.Config
Imports System.Linq
Imports System.Drawing
Imports System.Data.SqlClient
Imports DBImaging

Public Class ReemplazarImagenesForm
#Region " Declaraciones "
    Const MaxThumbnailWidth As Integer = 60
    Const MaxThumbnailHeight As Integer = 80
#End Region

#Region " Propiedades "
    Property SelectedPath As String
        Get
            Return RutaTextBox.Text.TrimEnd("\"c) & "\"
        End Get
        Set(ByVal value As String)
            RutaTextBox.Text = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New()
        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    End Sub
#End Region

#Region " Funciones "
    Private Function Validar() As Boolean
        If (RutaTextBox.Text = "") Then
            DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            RutaTextBox.Focus()

        ElseIf (Not Directory.Exists(Me.SelectedPath)) Then
            DesktopMessageBoxControl.DesktopMessageShow("Debe seleccionar un directorio válido", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
            RutaTextBox.Focus()
            RutaTextBox.SelectAll()
        Else
            Return True
        End If
        Return False
    End Function

    Private Function GetSize(ByVal lista As List(Of String), ByVal Formato As Slyg.Tools.Imaging.ImageManager.EnumFormat, ByVal Compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression) As Long

        Dim Peso As Long
        Dim Image_Binary As Byte() = Nothing

        For Each linea In lista
            Dim path2 As String = System.IO.Path.GetDirectoryName(linea) & "\" & System.IO.Path.GetFileNameWithoutExtension(linea) + "_tmp.jpeg"
            Try
                'Dim path2 As String = Path.Substring(0, Path.LastIndexOf("\")) & "\"   log.txt"
                Dim imgBMP As New Bitmap(linea)
                If imgBMP.Width > 5669 And imgBMP.Height > 4282 Then
                    imgBMP.Dispose()
                    'imgBMP = New Bitmap(New Bitmap(img), 6520, 4945)
                    Dim imgnueva = Image.FromFile(linea)
                    imgBMP = New Bitmap(imgnueva, 5669, 4282)
                    imgBMP.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg)
                    imgnueva.Dispose()
                Else
                    path2 = linea
                End If

                imgBMP.Dispose()

            Catch ex As Exception
                Throw New Exception("Error en GetSize - ReemplazarImagenesForm.vb " + ex.ToString)
            End Try

            Image_Binary = ImageManager.GetFolioData(path2, 1, Formato, Compresion)
            Peso = Peso + Image_Binary.Length

        Next

        Return Peso

    End Function
    'Private Function Reemplazar(Imagen As String, Path As String) As Boolean
    Private Function Reemplazar(Imagenes As List(Of String)) As Boolean
        Dim DicImagenes As New Dictionary(Of String, List(Of String))
        Dim listaAux As New List(Of String)
        Dim dbmCore As DBCore.DBCoreDataBaseManager
        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
        Dim manager As FileProviderManager = Nothing
        Dim Valido As Boolean = False

        For Each Imagn As String In Imagenes
            Dim NombrePartido = System.IO.Path.GetFileNameWithoutExtension(Imagn).Split(New Char() {"_"c})
            Dim NombreArchivo As String = NombrePartido(0) + "_" + NombrePartido(1) + "_" + NombrePartido(2)
            If DicImagenes.ContainsKey(NombreArchivo) Then
                listaAux = DicImagenes(NombreArchivo)
                listaAux.Add(Imagn)
                DicImagenes(NombreArchivo) = listaAux
            Else
                listaAux = New List(Of String)
                listaAux.Add(Imagn)
                DicImagenes.Add(NombreArchivo, listaAux)
            End If
        Next

        Dim Cuenta As Integer = 1
        For Each KeyExpFolFil As String In DicImagenes.Keys
            ProgressBarReemplazar.Visible = True
            Dim Porcentaje = (Cuenta / DicImagenes.Keys.Count()) * 100
            ProgressBarReemplazar.Value = CInt(Porcentaje)
            Dim percent As Integer = CInt(((CDbl(ProgressBarReemplazar.Value) / CDbl(ProgressBarReemplazar.Maximum)) * 100))
            ProgressBarReemplazar.CreateGraphics().DrawString(percent.ToString() & "%", New Font("Arial", CSng(8.25), FontStyle.Regular), Brushes.Black, New PointF(CSng(ProgressBarReemplazar.Width / 2 - 10), CSng(ProgressBarReemplazar.Height / 2 - 7)))
            Cuenta = Cuenta + 1
            listaAux = DicImagenes(KeyExpFolFil)
            dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
            dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            dbmCore.Connection_Open(Program.Sesion.Usuario.id)
            dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
            Dim boolInsertoTBL_FileType As Boolean = False
            Dim boolInsertoTBL_FileType2 As Boolean = False
            Dim boolManCreoItem As Boolean = False
            Dim version As Slyg.Tools.SlygNullable(Of Short) = 1
            Dim rutaLog As String = Me.SelectedPath & "log.txt"
            Dim Log As New StreamWriter(rutaLog, True, System.Text.Encoding.Default)
            Log.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss") & " -> {0} Imagenes encontradas para el Expediente_Folder_File {1} ", listaAux.Count, KeyExpFolFil)
            Log.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss") & " -> Buscando Expediente_Folder_File {0} ", KeyExpFolFil)
            Dim DataTableReemplazar = PA_Reemplazar_Imagenes(dbmImaging, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, KeyExpFolFil)

            If DataTableReemplazar.Rows.Count > 0 Then
                Try
                    'Dim rutaLog As String = img.Substring(0, img.LastIndexOf("\")) & "\log.txt"
                    'Dim Log As New StreamWriter(rutaLog, True, System.Text.Encoding.Default)
                    For Each img As String In listaAux

                        Dim resultImagen = System.IO.Path.GetFileNameWithoutExtension(img)
                        'Dim Caracteres = resultImagen.LastIndexOf("_")
                        'Dim ImagenTipolgiaCC = resultImagen.Substring(0, Caracteres)
                        'Dim DataTableReemplazar = dbmImaging.SchemaProcess.PA_Reemplazar_Imagenes.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, resultImagen)

                        'Dim DataTableReemplazar = PA_Reemplazar_Imagenes(dbmImaging, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, resultImagen.Substring(0, resultImagen.LastIndexOf("_")))
                        'If DataTableReemplazar.Rows.Count > 0 Then
                        'Dim CantidadFolios = resultImagen.Length() - Caracteres
                        'For Each ItemReemplazar As DataRow In DataTableReemplazar.Rows
                        Dim ItemReemplazar = DataTableReemplazar.Rows(0)

                        Log.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss") & " -> Se encontro, reemplazando imagen {0}", Path.GetFileName(img))
                        Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(CShort(ItemReemplazar.Item("fk_Entidad_Servidor")), CShort(ItemReemplazar.Item("fk_Servidor")))(0).ToCTA_ServidorSimpleType()
                        Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(CShort(ItemReemplazar.Item("fk_Entidad_Procesamiento")), CShort(ItemReemplazar.Item("fk_Sede_Procesamiento")), CShort(ItemReemplazar.Item("fk_Centro_Procesamiento")))(0).ToCTA_Centro_ProcesamientoSimpleType()

                        manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                        manager.Connect()
                        manager.TransactionBegin()
                        dbmImaging.Transaction_Begin()

                        Dim idExpediente = CLng(ItemReemplazar.Item("fk_Expediente"))
                        Dim idFolder = CShort(ItemReemplazar.Item("fk_Folder"))
                        Dim idFile = CShort(ItemReemplazar.Item("fk_File"))
                        Dim Folios = CInt(ItemReemplazar.Item("Folios_File"))

                        Dim DashBoardType As New DBImaging.SchemaProcess.TBL_Dashboard_CapturasType()
                        DashBoardType.Actualizado = True
                        dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(DashBoardType, idExpediente, idFolder, idFile)

                        Dim FileImgType As New DBCore.SchemaImaging.TBL_FileType()
                        FileImgType.fk_Expediente = idExpediente
                        FileImgType.fk_Folder = idFolder
                        FileImgType.fk_File = idFile
                        If boolInsertoTBL_FileType = False Then
                            FileImgType.id_Version = dbmImaging.SchemaProcess.TBL_File.DBNextId_for_id_Version(idExpediente, idFolder, idFile)
                            version = FileImgType.id_Version
                            'boolInsertoTBL_FileType = True
                        Else
                            FileImgType.id_Version = version
                        End If
                        FileImgType.File_Unique_Identifier = Guid.NewGuid()
                        'FileImgType.Folios_Documento_File = CShort(ImageManager.GetFolios(img))
                        FileImgType.Folios_Documento_File = CShort(listaAux.Count)
                        FileImgType.Tamaño_Imagen_File = 0
                        FileImgType.Nombre_Imagen_File = System.IO.Path.GetFileName(img)
                        FileImgType.Key_Cargue_Item = FileImgType.Nombre_Imagen_File
                        FileImgType.Save_FileName = FileImgType.Nombre_Imagen_File
                        FileImgType.fk_Content_Type = Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                        FileImgType.fk_Usuario_Log = Program.Sesion.Usuario.id
                        FileImgType.Validaciones_Opcionales = False
                        FileImgType.Es_Anexo = False
                        FileImgType.fk_Anexo = Nothing
                        If boolInsertoTBL_FileType = False Then
                            dbmCore.SchemaImaging.TBL_File.DBInsert(FileImgType)
                            boolInsertoTBL_FileType = True
                        End If


                        'Crea la nueva versión del file e inserta la nueva imágen.
                        Dim FileCoreType As New DBImaging.SchemaProcess.TBL_FileType
                        FileCoreType.fk_Expediente = idExpediente
                        FileCoreType.fk_Folder = idFolder
                        FileCoreType.fk_File = idFile
                        FileCoreType.id_Version = FileImgType.id_Version
                        If boolInsertoTBL_FileType2 = False Then
                            dbmImaging.SchemaProcess.TBL_File.DBInsert(FileCoreType)
                            boolInsertoTBL_FileType2 = True
                        End If

                        If boolManCreoItem = False Then
                            manager.CreateItem(idExpediente, idFolder, idFile, FileImgType.id_Version, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, FileImgType.File_Unique_Identifier)
                            boolManCreoItem = True
                        End If

                        Dim Formato = Utilities.GetEnumFormat(Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                        Dim Compresion = Utilities.GetEnumCompression(CType(Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

                        'Se crean los folios del file.
                        Dim Image_Binary As Byte() = Nothing
                        Dim Thumbnail_Binary As List(Of Byte()) = Nothing
                        Dim nFolioReemplazar As Short = CShort(resultImagen.Split(New Char() {"_"c})(3))
                        For nFolioImage = 1 To listaAux.Count
                            'If nFolioImage = 1 Then
                            If nFolioImage = nFolioReemplazar Then
                                'Verificamos que la imagen no sea mayor a 6520 x 4924 pixeles ya que un tamaño superior a este puede desbordar la memoria en imagingsearchcontrol.
                                Dim path2 As String = System.IO.Path.GetDirectoryName(img) & "\" & System.IO.Path.GetFileNameWithoutExtension(img) + "tmp.jpeg"
                                Try
                                    'Dim path2 As String = Path.Substring(0, Path.LastIndexOf("\")) & "\"   log.txt"
                                    Dim imgBMP As New Bitmap(img)
                                    If imgBMP.Width > 5669 And imgBMP.Height > 4282 Then
                                        imgBMP.Dispose()
                                        'imgBMP = New Bitmap(New Bitmap(img), 6520, 4945)
                                        Dim imgnueva = Image.FromFile(img)
                                        imgBMP = New Bitmap(imgnueva, 5669, 4282)
                                        imgBMP.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg)
                                        imgnueva.Dispose()
                                    Else
                                        path2 = img
                                    End If

                                    imgBMP.Dispose()

                                Catch ex As Exception
                                    Log.WriteLine("Error al intentar abrir la imagen, cambiando de tamaño la imagen {0}..", Path.GetFileName(img))
                                    Log.WriteLine("Error: {0}", ex.Message)
                                    Dim imgnueva = Image.FromFile(img)
                                    Dim imgBMP As New Bitmap(imgnueva, 5669, 4282)
                                    imgBMP.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg)
                                    imgnueva.Dispose()
                                    imgBMP.Dispose()
                                    Log.WriteLine("Tamaño cambiado")
                                End Try

                                Image_Binary = ImageManager.GetFolioData(path2, 1, Formato, Compresion)
                                Thumbnail_Binary = ImageManager.GetThumbnailData(path2, 1, 1, MaxThumbnailWidth, MaxThumbnailHeight)
                                If File.Exists(path2) And Path.GetFileNameWithoutExtension(path2).Contains("tmp") Then
                                    File.Delete(path2)
                                End If
                                manager.CreateFolio(idExpediente, idFolder, idFile, FileImgType.id_Version, CShort(nFolioImage), Image_Binary, Thumbnail_Binary(0), False)
                            End If
                        Next
                        'Dim nFolio As Short = CShort(resultImagen.Split(New Char() {"_"c})(3))
                        'Image_Binary = ImageManager.GetFolioData(Path, nFolio, Formato, Compresion)
                        'Thumbnail_Binary = ImageManager.GetThumbnailData(Path, nFolio, nFolio, MaxThumbnailWidth, MaxThumbnailHeight)
                        'manager.CreateFolio(idExpediente, idFolder, idFile, FileImgType.id_Version, nFolio, Image_Binary, Thumbnail_Binary(0), False)
                        Valido = True
                        'dbmImaging.Transaction_Commit()
                        'manager.TransactionCommit()
                        'Log.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss") & " -> Imagen reemplazada.")
                        'Next
                        'Else
                        '    Log.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss") & " -> El expediente_folder_file {0} no se encontro o no pertenece al proyecto, revisar", resultImagen.Substring(0, resultImagen.LastIndexOf("_")))
                        '    Log.Close()
                        '    Valido = False
                        'End If

                        If Valido Then
                            Dim RutaDestino As String = Me.SelectedPath & "ImagenesCargadas" & ".#"
                            MoverCarpeta(RutaDestino, img)
                        End If
                        Log.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss") & " -> Imagen {0} reemplazada.", Path.GetFileName(img))
                        manager.TransactionCommit()
                        dbmImaging.Transaction_Commit()

                    Next

                Catch ex As Exception
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                    If (manager IsNot Nothing) Then manager.TransactionRollback()
                    Application.DoEvents()
                    DesktopMessageBoxControl.DesktopMessageShow("ReemplazarItems", ex)
                    Valido = False
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (manager IsNot Nothing) Then manager.Disconnect()
                End Try
                Log.Close()
            Else
                Log.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss") & " -> El expediente_folder_file {0} no se encontro o no pertenece al proyecto, revisar", KeyExpFolFil)
                Log.Close()
                Valido = False
            End If
        Next
        Return Valido
    End Function

    Private Function PA_Reemplazar_Imagenes(ByVal dbmImaging As DBImagingDataBaseManager, fk_Entidad As Integer, fk_proyecto As Integer, Imagen As String) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim sqlquery As String = "	SELECT	DISTINCT Folder.fk_Entidad_Servidor,Folder.fk_Servidor, Cargue.fk_Entidad_Procesamiento,OT.fk_Sede_Procesamiento, " +
          "  OT.fk_Centro_Procesamiento, FD.fk_Expediente, FD.fk_Folder, FD.fk_File " +
  " ,FormatoEntrada.Extension_Formato_Imagen AS Extension_Formato_Imagen_Entrada, " +
  " FormatoSalida.Extension_Formato_Imagen AS Extension_Formato_Imagen_Salida " +
  ",UPPER([DB_Miharu.Core].[Process].Fn_getDatos(FD.fk_Expediente, FD.fk_Folder, FD.fk_File, null, FD.fk_Documento, 1)) AS Folios " +
  ",UPPER([DB_Miharu.Core].[Process].Fn_getDatos(FD.fk_Expediente, FD.fk_Folder, FD.fk_File, null, FD.fk_Documento, 2)) AS Caja " +
  ",UPPER([DB_Miharu.Core].[Process].Fn_getDatos(FD.fk_Expediente, FD.fk_Folder, FD.fk_File, null, FD.fk_Documento, 3)) AS Carpeta " +
  " ,UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, null, Fil.fk_Documento, 4)) AS Cedula " +
  " ,DOC.fk_Tipologia AS Tipologia " +
  " ,Fil.Folios_File " +
  " FROM [DB_Miharu.Imaging_Core].Process.TBL_OT AS OT  " +
  " INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Cargue AS Cargue " +
    " ON	Cargue.fk_OT = OT.id_OT " +
  " INNER JOIN	[DB_Miharu.Imaging_Core].Core.CTA_Folder AS Folder " +
    " ON	Cargue.id_Cargue = Folder.fk_Cargue " +
  " INNER JOIN	[DB_Miharu.Core].Process.TBL_Expediente E " +
    " ON	Folder.fk_Expediente = E.id_Expediente " +
    " AND OT.fk_Entidad = E.fk_Entidad " +
    " AND	OT.fk_Proyecto = E.fk_Proyecto " +
  " INNER JOIN	[DB_Miharu.Core].Process.TBL_File Fil " +
    " ON	Fil.fk_Expediente = Folder.fk_Expediente " +
    " AND Fil.fk_Folder = Folder.fk_Folder " +
  " INNER JOIN	[DB_Miharu.Core].Process.TBL_File_Data FD " +
    " ON	Folder.fk_Expediente = FD.fk_Expediente " +
    " AND Folder.fk_Folder = FD.fk_Folder " +
    " AND Fil.fk_Documento = FD.fk_Documento " +
  " INNER JOIN	[DB_Miharu.Core].Config.TBL_Documento DOC " +
    " ON	DOC.id_Documento = FD.fk_Documento " +
    " AND	DOC.fk_Entidad = OT.fk_Entidad " +
    " AND DOC.fk_Proyecto = OT.fk_Proyecto " +
  " INNER JOIN	[DB_Miharu.Imaging_Core].Core.CTA_Process_File AS ProcessFile  " +
               "  ON	Fil.fk_Expediente = ProcessFile.fk_Expediente " +
               "  AND Fil.fk_Folder = ProcessFile.fk_Folder " +
               "  AND Fil.id_File = ProcessFile.id_File " +
  " INNER JOIN	[DB_Miharu.Imaging_Core].Config.TBL_Proyecto Proyecto " +
    " ON	Proyecto.fk_Entidad = OT.fk_Entidad " +
    " AND Proyecto.fk_Proyecto = OT.fk_Proyecto " +
  " INNER JOIN	[DB_Miharu.Imaging_Core].Config.TBL_Formato_Imagen AS FormatoEntrada  " +
    " ON	Proyecto.fk_Formato_Entrada = FormatoEntrada.id_Formato_Imagen  " +
  " INNER JOIN	[DB_Miharu.Imaging_Core].Config.TBL_Formato_Imagen AS FormatoSalida  " +
    " ON	Proyecto.fk_Formato_Salida = FormatoSalida.id_Formato_Imagen  " +
  " WHERE OT.fk_Entidad = @fk_Entidad " +
    " AND OT.fk_Proyecto = @fk_Proyecto " +
    " AND CONCAT(fd.fk_Expediente,'_',fd.fk_Folder,'_',fd.fk_File) = @IdentificadorImagen  "
    
            Dim SqlParameter = New SqlParameter() _
           {
               New SqlParameter("@fk_Entidad", fk_Entidad),
                New SqlParameter("@fk_Proyecto", fk_proyecto),
                New SqlParameter("@IdentificadorImagen", Imagen)
           }
            dt = ExecuteQuery(sqlquery, dbmImaging, SqlParameter)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return dt
    End Function
    Private Function ExecuteQuery(ByVal s As String, ByVal dbmImaging As DBImagingDataBaseManager, ByVal ParamArray params() As SqlParameter) As DataTable
        Dim dt As DataTable = Nothing
        Using da As New System.Data.SqlClient.SqlDataAdapter(s, dbmImaging.DataBase.ConnectionString)
            Try
                dt = New DataTable
                If params.Length > 0 Then
                    da.SelectCommand.Parameters.AddRange(params)
                End If
                da.SelectCommand.CommandTimeout = 86400
                da.Fill(dt)
                Return dt
            Catch ex As SqlException
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return dt
        End Using
    End Function
#End Region
#Region " Metodos "
    Private Sub SelectFolderButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectFolderButton.Click
        SelectFolderPath()
    End Sub
    Private Sub SelectFolderPath()
        Dim LectorFolderBrowserDialog = New FolderBrowserDialog()
        Dim Respuesta As DialogResult

        LectorFolderBrowserDialog.SelectedPath = RutaTextBox.Text
        LectorFolderBrowserDialog.ShowNewFolderButton = False
        LectorFolderBrowserDialog.Description = "Seleccione la carpeta"

        Respuesta = LectorFolderBrowserDialog.ShowDialog()

        If (Respuesta = DialogResult.OK) Then
            RutaTextBox.Text = LectorFolderBrowserDialog.SelectedPath
        End If
    End Sub
    Private Sub Reemplazarimagenes()
        If Validar() Then
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Dim FilesNames As String()
                Dim Imagenes As New List(Of String)
                FilesNames = Directory.GetFiles(Me.SelectedPath, "*" + Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                'Dim RutaDestino As String = Me.SelectedPath & "ImagenesCargadas" & ".#"

                For Each Files In FilesNames
                    If (Not Files.EndsWith("#")) Then
                        Imagenes.Add(Files)
                    End If
                Next
                'Dim Cuenta As Integer = 1
                Dim varError As Boolean = False
                If Imagenes.Count > 0 Then

                    'For Each imagen In Imagenes
                    '    ProgressBarReemplazar.Visible = True
                    '    Dim Porcentaje = (Cuenta / Imagenes.Count()) * 100
                    '    Dim Path = imagen
                    '    Dim Images = imagen.Substring(imagen.LastIndexOf("\") + 1)
                    'If (Reemplazar(Images, Path)) Then
                    'MoverCarpeta(RutaDestino, imagen)
                    If Not (Reemplazar(Imagenes)) Then
                        varError = True
                        'Else
                        '    varError = True
                    End If
                    'ProgressBarReemplazar.Value = CInt(Porcentaje)
                    'Dim percent As Integer = CInt(((CDbl(ProgressBarReemplazar.Value) / CDbl(ProgressBarReemplazar.Maximum)) * 100))
                    'ProgressBarReemplazar.CreateGraphics().DrawString(percent.ToString() & "%", New Font("Arial", CSng(8.25), FontStyle.Regular), Brushes.Black, New PointF(CSng(ProgressBarReemplazar.Width / 2 - 10), CSng(ProgressBarReemplazar.Height / 2 - 7)))
                    'Cuenta = Cuenta + 1
                    'Next
                Else
                    DesktopMessageBoxControl.DesktopMessageShow("La ruta seleccionada no contiene subdirectorios", "Directorio inválido", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If
                ProgressBarReemplazar.Visible = False
                If varError Then
                    DesktopMessageBoxControl.DesktopMessageShow("No se reemplazaron algunas imágenes por favor validar el log", "Reemplazar Imagenes", DesktopMessageBoxControl.IconEnum.WarningIcon, True)
                End If
                DesktopMessageBoxControl.DesktopMessageShow("Proceso Finalizado", "Reemplazar Imagenes", DesktopMessageBoxControl.IconEnum.SuccessfullIcon, True)
                Me.RutaTextBox.Text = String.Empty
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End If
    End Sub
    Private Sub MoverCarpeta(nRutaDestino As String, nPath As String)
        Try
            If Not Directory.Exists(nRutaDestino) Then
                Directory.CreateDirectory(nRutaDestino)
            End If
            Directory.Move(nPath, nRutaDestino & "\" & System.IO.Path.GetFileName(nPath))
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AceptarButton_Click(sender As System.Object, e As System.EventArgs) Handles AceptarButton.Click
        Reemplazarimagenes()
    End Sub

#End Region

End Class