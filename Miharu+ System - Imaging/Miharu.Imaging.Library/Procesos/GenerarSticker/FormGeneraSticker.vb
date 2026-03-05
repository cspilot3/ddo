Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls
Imports System.Windows.Forms
Imports Slyg.Tools
Imports Miharu.Tools.Progress
Imports Miharu.Desktop.Library.Config
Imports System.Text
Imports Miharu.Security.Library.Session
Imports Microsoft.Reporting.WinForms
Imports Miharu.Desktop.Library
Imports Miharu.FileProvider.Library
Imports System.Drawing
Imports System.Xml
Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading
Namespace Procesos.GenerarSticker
    Public Class FormGeneraSticker

#Region " Declaraciones "

        Public Property WorkSpace() As FormImagingWorkSpace
        Protected _TempPath As String
        Private ProgressForm As New FormProgress()
        Private ArrayNotificacion As ArrayList
        Private BloqueoConcurrencia As Object
        Private AumentaVersion As Short = 1
        Private _StrArchivoLog As String
        Private Count_Process As Integer = 0

        Dim tempImagePaths As New List(Of String)()   ' Lista de rutas de imagenes temporales creadas por el sistema

#End Region

#Region " Eventos "

        Private Sub FormCruce_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            _TempPath = Program.AppPath & Program.TempPath
            _StrArchivoLog = _TempPath + "\Log_Genera_Sticker_" + Date.Now.ToString("yyyyMMdd") + "_" + Date.Now.Hour.ToString("00") + Date.Now.Minute.ToString("00") + Date.Now.Second.ToString("00") + ".dat"
            CargarOT()
        End Sub

        Private Sub FechaProcesoDateTimePicker_ValueChanged(sender As System.Object, e As System.EventArgs) Handles FechaProcesodateTimePicker.ValueChanged
            CargarOT()
        End Sub

        Private Sub CruzarButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles GenerarButton.Click
            Dim Respuesta = DesktopMessageBoxControl.DesktopMessageShow("¿Está seguro que desea generar el Estampado?", "Estampado", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, False)
            If (Respuesta = DialogResult.OK) Then
                IndexarFolioSticker()
            End If
        End Sub

#End Region

#Region " Métodos "

        Private Sub IndexarFolioSticker()

            Dim manager As FileProviderManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim AumentaFolio As Short = 1
            Dim ProgressForm As New Slyg.Tools.Progress.FormProgress()
            Dim procesadorHilosInstance As ProcesadorHilos
            Dim Respuesta As String = ""
            Dim NumeroHilos As Integer = 5

            Try
                If Validar() Then

                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim valueOTDesktop As Integer = CInt(IIf(OTDesktopComboBox.SelectedValue.Equals("--TODOS--"), -1, OTDesktopComboBox.SelectedValue))

                    ' Valida si almenos una OT se encuentra procesada
                    Dim RespuestaOT = dbmImaging.SchemaProcess.PA_Consultar_Estado_OT_x_Fecha_Proceso_Estampado.DBExecute(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")), valueOTDesktop)
                    If RespuestaOT = 0 Then
                        MessageBox.Show("No se puede realizar el proceso de Generar Cartas ya que hay una o más OT's tienen registros pendientes de proceso.", "Rechazos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else

                        Me.Enabled = False
                        Me.Cursor = Cursors.WaitCursor
                        ArrayNotificacion = New ArrayList
                        BloqueoConcurrencia = New Object
                        Count_Process = 0
                        Dim ProcesoDataTable As DBImaging.SchemaProcess.TBL_Fecha_ProcesoDataTable
                        ProcesoDataTable = dbmImaging.SchemaProcess.TBL_Fecha_Proceso.DBFindByfk_Entidadfk_Proyectoid_fecha_proceso(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")))

                        If (ProcesoDataTable.Count = 1) Then
                            If (ProcesoDataTable(0).Cerrado = False) Then
                                ' CONSULTA TABLA PARAMETRICA PARA GENERAR STICKER
                                Dim _documentosStickerDataTable As DBImaging.SchemaConfig.TBL_Documento_StickerDataTable
                                _documentosStickerDataTable = dbmImaging.SchemaConfig.TBL_Documento_Sticker.DBFindByfk_Entidadfk_Proyectogenera_Sticker_Fisicofk_Documento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False, -1)
                                If _documentosStickerDataTable.Rows.Count > 0 Then

                                    'CONSULTA PARAMETROS EN FORMATO XML
                                    Dim docXML As XmlDocument = New XmlDocument()
                                    docXML.LoadXml(_documentosStickerDataTable(0).parametro_Reporte)
                                    Dim nodes As XmlNodeList = docXML.SelectNodes("/Configuracion/Sticker")

                                    Dim dt As New DataTable
                                    With dt
                                        .Columns.Add(New DataColumn("PosicionX", GetType(String)))
                                        .Columns.Add(New DataColumn("PosicionY", GetType(String)))
                                        .Columns.Add(New DataColumn("CodigoBarras", GetType(String)))
                                        .Columns.Add(New DataColumn("NombreParametrosPrincipal", GetType(String)))
                                        .Columns.Add(New DataColumn("ValorParametrosPrincipal", GetType(String)))
                                        .Columns.Add(New DataColumn("NombreParametrosSticker", GetType(String)))
                                        .Columns.Add(New DataColumn("ValorParametrosSticker", GetType(String)))
                                    End With

                                    For Each n1 As XmlNode In docXML.DocumentElement.ChildNodes
                                        If n1.HasChildNodes Then
                                            Dim row As DataRow
                                            row = dt.NewRow
                                            For Each n2 As XmlNode In n1.ChildNodes
                                                For Each n3 As XmlNode In n2.ChildNodes
                                                    row(n2.Name) = n3.InnerText
                                                Next
                                            Next
                                            dt.Rows.Add(row)
                                        End If
                                    Next

                                    'CONSULTA DATOS PRINCIPALES 
                                    Dim resultadoDataTable = DatosParametros(dbmImaging, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")), CInt(IIf(OTDesktopComboBox.SelectedValue.Equals("--TODOS--"), -1, OTDesktopComboBox.SelectedValue)), Nothing, Nothing, Nothing, Nothing,
                                        _documentosStickerDataTable(0).sqlPrincipal, dt.Rows(0).Item("NombreParametrosPrincipal").ToString(),
                                                       dt.Rows(0).Item("ValorParametrosPrincipal").ToString())
                                    Try
                                        If _documentosStickerDataTable(0).Numero_Hilos > 0 Then
                                            procesadorHilosInstance = New ProcesadorHilos(_documentosStickerDataTable(0).Numero_Hilos)
                                        Else
                                            procesadorHilosInstance = New ProcesadorHilos(NumeroHilos)
                                        End If
                                    Catch ex As Exception
                                        procesadorHilosInstance = New ProcesadorHilos(NumeroHilos)
                                    End Try
                                    'Iniciar proceso                
                                    ProgressForm.Process = "Generando Sticker"
                                    ProgressForm.Action = ""
                                    ProgressForm.ValueProcess = 0
                                    ProgressForm.ValueAction = 0
                                    ProgressForm.MaxValueProcess = 1
                                    ProgressForm.MaxValueAction = resultadoDataTable.Rows.Count
                                    ProgressForm.Show()
                                    If resultadoDataTable.Rows.Count > 0 Then
                                        Try
                                            For Each ItemCrear As DataRow In resultadoDataTable.Rows

                                                Dim ArraListParameters As ArrayList = New ArrayList
                                                Dim _docStickerDataTable As DBImaging.SchemaConfig.TBL_Documento_StickerDataTable
                                                Dim dtp As DataTable = resultadoDataTable.Clone()
                                                dtp.Rows.Add(ItemCrear.ItemArray)

                                                Count_Process += 1
                                                ProgressForm.ValueProcess = 1
                                                ProgressForm.ValueAction = Count_Process
                                                ProgressForm.Action = "Expediente: " & ItemCrear.Item("fk_Expediente").ToString() & " Folder: " & ItemCrear.Item("fk_folder").ToString() & " File: " & ItemCrear.Item("fk_File").ToString()
                                                Application.DoEvents()
                                                If (ProgressForm.Cancel) Then Throw New Exception("Operación cancelada por el usuario")
                                                Dim Documento = CType(ItemCrear.Item("fk_Documento"), Integer)
                                                _docStickerDataTable = dbmImaging.SchemaConfig.TBL_Documento_Sticker.DBFindByfk_Entidadfk_Proyectogenera_Sticker_Fisicofk_Documento(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, False, CType(ItemCrear.Item("fk_Documento"), Integer))

                                                If _docStickerDataTable.Count > 0 Then
                                                    'CONSULTA PARAMETROS EN FORMATO XML
                                                    Dim docXML1 As XmlDocument = New XmlDocument()
                                                    docXML1.LoadXml(_docStickerDataTable(0).parametro_Reporte)
                                                    Dim nodes1 As XmlNodeList = docXML1.SelectNodes("/Configuracion/Sticker")

                                                    Dim dts As New DataTable
                                                    With dts
                                                        .Columns.Add(New DataColumn("PosicionX", GetType(String)))
                                                        .Columns.Add(New DataColumn("PosicionY", GetType(String)))
                                                        .Columns.Add(New DataColumn("CodigoBarras", GetType(String)))
                                                        .Columns.Add(New DataColumn("WidthCodigoBarras", GetType(String)))
                                                        .Columns.Add(New DataColumn("HeightCodigoBarras", GetType(String)))
                                                        .Columns.Add(New DataColumn("NombreParametrosPrincipal", GetType(String)))
                                                        .Columns.Add(New DataColumn("ValorParametrosPrincipal", GetType(String)))
                                                        .Columns.Add(New DataColumn("NombreParametrosSticker", GetType(String)))
                                                        .Columns.Add(New DataColumn("ValorParametrosSticker", GetType(String)))
                                                    End With

                                                    For Each n1 As XmlNode In docXML1.DocumentElement.ChildNodes
                                                        If n1.HasChildNodes Then
                                                            Dim row As DataRow
                                                            row = dts.NewRow
                                                            For Each n2 As XmlNode In n1.ChildNodes
                                                                For Each n3 As XmlNode In n2.ChildNodes
                                                                    row(n2.Name) = n3.InnerText
                                                                Next
                                                            Next
                                                            dts.Rows.Add(row)
                                                        End If
                                                    Next
                                                    ArraListParameters.Add(dbmImaging)
                                                    ArraListParameters.Add(_docStickerDataTable)
                                                    ArraListParameters.Add(dtp)
                                                    ArraListParameters.Add(dts)
                                                    ArraListParameters.Add(CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")))
                                                    ArraListParameters.Add(CInt(IIf(OTDesktopComboBox.SelectedValue.Equals("--TODOS--"), -1, OTDesktopComboBox.SelectedValue)))

                                                    GeneraStickerHilos(ArraListParameters)
                                                    'procesadorHilosInstance.servicio = Me
                                                    'procesadorHilosInstance.AgregarHilo(ArraListParameters)

                                                    'While (procesadorHilosInstance.TerminoHilos = False)
                                                    '    System.Threading.Thread.Sleep(100)
                                                    'End While
                                                End If
                                            Next
                                        Catch ex As Exception
                                        End Try
                                    End If

                                    If resultadoDataTable.Rows.Count = 0 Then
                                        MessageBox.Show("!No se encontro información para estampar¡", "Estampado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Else
                                        Dim totalDataTable = DatosParametros(dbmImaging, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CInt(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd")), CInt(IIf(OTDesktopComboBox.SelectedValue.Equals("--TODOS--"), -1, OTDesktopComboBox.SelectedValue)), Nothing, Nothing, Nothing, Nothing,
                                            _documentosStickerDataTable(0).sqlSticker, dt.Rows(0).Item("NombreParametrosSticker").ToString(),
                                                           dt.Rows(0).Item("NombreParametrosSticker").ToString())
                                        Respuesta = totalDataTable.Rows(0).Item("Total").ToString()
                                        If totalDataTable.Rows.Count > 0 Then
                                            MessageBox.Show("Se genero sticker en los siguientes documentos: " & vbCrLf & Respuesta, "Estampado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Else
                                            MessageBox.Show("!Generación de Stickers Completado¡", "Estampado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        End If
                                    End If
                                End If

                                If _documentosStickerDataTable.Rows.Count = 0 Then
                                    MessageBox.Show("!No se encontro información para estampar¡", "Estampado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If

                            Else
                                MessageBox.Show("La Fecha de Proceso seleccionada ya fue cerrada.", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If
                        Else
                            MessageBox.Show("No se encuentra información asociada a la Fecha de Proceso seleccionada.", "Cruce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If  '  RespuestaOT = 0 ELSE
                End If

            Catch ex As Exception
                ProgressForm.Hide()
                Application.DoEvents()
                EscribeLog(Me._StrArchivoLog, "Error Generacion Estampado " + ex.Message, False, True)
                DesktopMessageBoxControl.DesktopMessageShow("GeneraEstampado", ex)
            Finally
                ProgressForm.Visible = False
                ProgressForm.Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                Me.Enabled = True
                Me.Cursor = Cursors.Default
            End Try
        End Sub

        Public Sub GeneraStickerHilos(ByVal objectArray As Object)

            Dim nFolder, nFile, nFolio, nVersion As Short
            Dim nDocumento, nExpediente As Long

            Try

                Dim ArrayParameters As ArrayList = CType(objectArray, ArrayList)
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = CType(ArrayParameters(0), DBImaging.DBImagingDataBaseManager)
                Dim _documentosStickerDataTable As DBImaging.SchemaConfig.TBL_Documento_StickerDataTable = CType(ArrayParameters(1), DBImaging.SchemaConfig.TBL_Documento_StickerDataTable)
                Dim dtp As DataTable = CType(ArrayParameters(2), DataTable)
                Dim dts As DataTable = CType(ArrayParameters(3), DataTable)
                Dim nFechaProceso = CType(ArrayParameters(4), Integer)
                Dim nIdOt = CType(ArrayParameters(5), Integer)

                Dim FileName = _TempPath & Guid.NewGuid().ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                Dim FileNameNuevo = _TempPath & Guid.NewGuid().ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida

                tempImagePaths.Clear()  ' Vacía la lista de imagenes temporales antes de comenzar

                Try
                    If dtp.Rows(0).Item("fk_Expediente").ToString() <> "" Then
                        nExpediente = CLng(dtp.Rows(0).Item("fk_Expediente"))
                    End If
                Catch ex As Exception
                    nExpediente = 0
                End Try
                Try
                    If dtp.Rows(0).Item("fk_Folder").ToString() <> "" Then
                        nFolder = CShort(dtp.Rows(0).Item("fk_Folder"))
                    End If
                Catch ex As Exception
                    nFolder = 0
                End Try
                Try
                    If dtp.Rows(0).Item("fk_File").ToString() <> "" Then
                        nFile = CShort(dtp.Rows(0).Item("fk_File"))
                    End If
                Catch ex As Exception
                    nFile = 0
                End Try
                Try
                    If dtp.Rows(0).Item("fk_File_Record_Folio").ToString() <> "" Then
                        nFolio = CShort(CInt(dtp.Rows(0).Item("fk_File_Record_Folio")))
                    End If
                Catch ex As Exception
                    nFolio = 0
                End Try
                Try
                    If dtp.Rows(0).Item("fk_Version").ToString() <> "" Then
                        nVersion = CShort(dtp.Rows(0).Item("fk_Version"))
                    End If
                Catch ex As Exception
                    nVersion = 0
                End Try
                Try
                    If dtp.Rows(0).Item("fk_Documento").ToString() <> "" Then
                        nDocumento = CShort(dtp.Rows(0).Item("fk_Documento"))
                    End If
                Catch ex As Exception
                    nDocumento = 0
                End Try

                ' Genera la imagen del sticker completo
                Dim topImage = GenerarSticker(dbmImaging, _documentosStickerDataTable, dtp, dts, Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto,
                                           Nothing, Nothing, nDocumento, nExpediente, nFolder, nFile)

                If topImage IsNot Nothing Then

                    Dim manager As FileProviderManager = Nothing
                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

                    Dim imagen() As Byte = Nothing
                    Dim thumbnail() As Byte = Nothing

                    Try
                        manager = New FileProviderManager(nExpediente, nFolder, dbmImaging, Program.Sesion.Usuario.id)
                        dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                        dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                        manager.Connect()

                        'Obtiene la imagen Actual en su version 1
                        manager.GetFolio(nExpediente, nFolder, nFile, nVersion, nFolio, imagen, thumbnail)
                        tempImagePaths.Add(FileName)
                        Using fileImage = New FileStream(FileName, FileMode.Create)
                            fileImage.Write(imagen, 0, imagen.Length)
                            fileImage.Close()
                        End Using

                        nVersion = nVersion + AumentaVersion

                        Dim backImage = ByteArrayToImage(imagen)
                        Dim imagenFinal As Byte() = OverlayImageAndSave(backImage, topImage, FileNameNuevo, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, Integer.Parse(dts.Rows(0).Item("PosicionX").ToString()), Integer.Parse(dts.Rows(0).Item("PosicionY").ToString()))

                        Dim existFolioVersion = ExistFolio(manager, nExpediente, nFolder, nFile, nVersion, nFolio)

                        Dim fileImgType As New DBCore.SchemaImaging.TBL_FileType()
                        fileImgType.fk_Expediente = nExpediente
                        fileImgType.fk_Folder = nFolder
                        fileImgType.fk_File = nFile
                        fileImgType.id_Version = nVersion
                        fileImgType.File_Unique_Identifier = Guid.NewGuid()
                        fileImgType.Folios_Documento_File = 1
                        fileImgType.Tamaño_Imagen_File = 0
                        fileImgType.Nombre_Imagen_File = ""
                        fileImgType.Key_Cargue_Item = ""
                        fileImgType.Save_FileName = ""
                        fileImgType.fk_Content_Type = Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                        fileImgType.fk_Usuario_Log = Program.Sesion.Usuario.id
                        fileImgType.Validaciones_Opcionales = False
                        fileImgType.Es_Anexo = False
                        fileImgType.fk_Anexo = Nothing
                        fileImgType.fk_Entidad_Servidor = Program.DesktopGlobal.ServidorImagenRow.fk_Entidad
                        fileImgType.fk_Servidor = Program.DesktopGlobal.ServidorImagenRow.id_Servidor
                        fileImgType.Fecha_Creacion = SlygNullable.SysDate
                        fileImgType.Fecha_Transferencia = Nothing
                        fileImgType.En_Transferencia = False
                        fileImgType.fk_Entidad_Servidor_Transferencia = Nothing
                        fileImgType.fk_Servidor_Transferencia = Nothing

                        If existFolioVersion Then
                            dbmCore.SchemaImaging.TBL_File.DBUpdate(fileImgType, nExpediente, nFolder, nFile, nVersion)
                            manager.UpdateFolio(nExpediente, nFolder, nFile, nVersion, nFolio, imagenFinal, thumbnail)
                        Else
                            manager.CreateItem(nExpediente, nFolder, nFile, nVersion, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, Guid.NewGuid())
                            dbmCore.SchemaImaging.TBL_File.DBInsert(fileImgType)
                            manager.CreateFolio(nExpediente, nFolder, nFile, nVersion, nFolio, imagenFinal, thumbnail, True)
                        End If


                    Catch ex As Exception
                        Throw
                    Finally
                        If dbmCore IsNot Nothing Then
                            dbmCore.Connection_Close()
                        End If

                        If manager IsNot Nothing Then
                            manager.Disconnect()
                        End If
                    End Try

                    Dim InsertSticker As New DBImaging.SchemaProcess.TBL_Sticker_ProcesadosType
                    InsertSticker.fk_Expediente = nExpediente
                    InsertSticker.fk_Folder = nFolder
                    InsertSticker.fk_File = nFile
                    dbmImaging.SchemaProcess.TBL_Sticker_Procesados.DBInsert(InsertSticker)

                    CleanupTemporaryImages(tempImagePaths)   ' Elimina las imagenes temporales creadas por el sistema

                    EscribeLog(_StrArchivoLog,
                               "Expediente:" & nExpediente & vbCrLf & _
                               "Folder:" & nFolder & vbCrLf & _
                               "File:" & nFile & vbCrLf & _
                               "Version:" & nVersion & vbCrLf & _
                               "Folio:" & nFolio & vbCrLf & _
                               "ArchivoSinSticker:" & FileName & vbCrLf & _
                               "ArchivoConSticker:" & FileNameNuevo & vbCrLf,
                               False, True)
                End If

            Catch ex As Exception
                CleanupTemporaryImages(tempImagePaths)   ' Elimina las imagenes temporales creadas por el sistema
                EscribeLog(Me._StrArchivoLog, "Error Generacion Sticker " + ex.Message, False, True)
            Finally
            End Try
        End Sub

        Private Sub CargarOT()
            Dim dbmImaging As New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
            Try
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBFindByfk_Entidadfk_Proyectofk_fecha_procesoCerrado(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto, CType(FechaProcesodateTimePicker.Value.ToString("yyyyMMdd"), Global.Slyg.Tools.SlygNullable(Of Integer)), False)
                OTDesktopComboBox.DataSource = Nothing
                If OTDataTable.Count > 0 Then
                    Utilities.LlenarCombo(OTDesktopComboBox, OTDataTable, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, DBImaging.SchemaProcess.TBL_OTEnum.id_OT.ColumnName, True, "-1", "--TODOS--")
                End If
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("Cargar OT", ex)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

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

        ''' <summary>
        ''' Elimina archivos temporales de una lista de rutas, 
        ''' verificando primero si la ruta es válida y si el archivo existe.
        ''' </summary>
        ''' <param name="imagePaths">Lista de rutas de imágenes a limpiar.</param>
        Private Sub CleanupTemporaryImages(ByVal imagePaths As List(Of String))

            For Each imagePath As String In imagePaths
                If Not String.IsNullOrWhiteSpace(imagePath) AndAlso Not IsNothing(imagePath) Then
                    If File.Exists(imagePath) Then
                        File.Delete(imagePath)   ' Elimina el archivo
                    End If
                End If
            Next
        End Sub

#End Region

#Region " Funciones "

        ' Verifica si existe el folio - version proporcionado
        Private Function ExistFolio(ByRef manager As FileProviderManager, ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal nFolio As Short) As Boolean

            Dim folioExists As Boolean = False

            Try
                Dim imagen() As Byte = Nothing
                Dim thumbnail() As Byte = Nothing

                manager.GetFolio(nExpediente, nFolder, nFile, nVersion, nFolio, imagen, thumbnail)

                ' Verifica si se obtuvo una imagen del folio
                If imagen IsNot Nothing Then
                    folioExists = True
                End If
            Catch
                Return folioExists
            End Try

            Return folioExists
        End Function

        ''' Genera la imagen completa del Sticker de acuerdo al RDLC seleccionado
        Private Function GenerarSticker(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager,
                                   ByVal _documentosStickerDataTable As DBImaging.SchemaConfig.TBL_Documento_StickerDataTable,
                                   ByVal dtp As DataTable,
                                   ByVal dts As DataTable,
                                   ByVal nEntidad As Short,
                                   ByVal nProyecto As Short,
                                   ByVal nFechaProceso As Integer,
                                   ByVal nIdOt As Integer,
                                   ByVal nDocumento As Long,
                                   ByVal nExpediente As Long,
                                   ByVal nFolder As Short,
                                   ByVal nFile As Short) As Image

            Dim Extension_Formato_Imagen_Salida As String = String.Empty
            Dim ImagenSticker As Image = Nothing
            Dim NombreArchivo As String
            Dim barcodeImage As BarcodeLib.Barcode = New BarcodeLib.Barcode()
            barcodeImage.IncludeLabel = False

            Try
                dtp.Columns.Add("IMAGEN", GetType(Byte()))

                If dtp.Rows.Count > 0 Then

                    ' Genera el codigo de barras 
                    For Each item As DataRow In dtp.Rows

                        Try
                            Dim valueFormatoCodigoBarras = dts.Rows(0).Item("CodigoBarras").ToString()
                            Dim img As Byte() = Nothing

                            If String.IsNullOrWhiteSpace(valueFormatoCodigoBarras) Then
                                Dim whiteImage As System.Drawing.Image = CreateEmptyImage()
                                item("IMAGEN") = ImageToByteArray(whiteImage)
                                Continue For
                            End If

                            Dim valueCodigoBaras = item("CodigoBarras").ToString()
                            Dim valueWidthCodigoBarras = CInt(dts.Rows(0).Item("WidthCodigoBarras"))
                            Dim valueHeightCodigoBarras = CInt(dts.Rows(0).Item("HeightCodigoBarras"))

                            Select Case valueFormatoCodigoBarras
                                Case "CODE39Extended"
                                    'img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE39Extended, item("CodigoBarras").ToString(), Color.Black, Color.White, 200, 25))
                                    img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE39Extended, valueCodigoBaras, Color.Black, Color.White, valueWidthCodigoBarras, valueHeightCodigoBarras))
                                Case "CODE39"
                                    'img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE39, item("CodigoBarras").ToString(), Color.Black, Color.White, 200, 25))
                                    img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE39, valueCodigoBaras, Color.Black, Color.White, valueWidthCodigoBarras, valueHeightCodigoBarras))
                                Case "CODE128"
                                    'img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE128, item("CodigoBarras").ToString(), Color.Black, Color.White, 200, 25))
                                    img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.CODE128, valueCodigoBaras, Color.Black, Color.White, valueWidthCodigoBarras, valueHeightCodigoBarras))
                                Case "EAN13"
                                    'img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.EAN13, item("CodigoBarras").ToString(), Color.Black, Color.White, 200, 25))
                                    img = ImageToByteArray(barcodeImage.Encode(BarcodeLib.TYPE.EAN13, valueCodigoBaras, Color.Black, Color.White, valueWidthCodigoBarras, valueHeightCodigoBarras))
                            End Select
                            item("IMAGEN") = img

                        Catch ex As Exception
                            Continue For
                        End Try
                    Next

                    dtp.AcceptChanges()

                    Dim viewer As ReportViewer = New ReportViewer()
                    Dim docXMLDevive As XmlDocument = New XmlDocument()

                    Dim rds As ReportDataSource = New ReportDataSource("DataSet1", dtp)

                    ' Permite revisar los recursos embebidos del sistema para verificar la ruta del rdlc
                    'Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
                    'Dim resourceNames As String() = assembly.GetManifestResourceNames()

                    'For Each resourceName As String In resourceNames
                    '    Console.WriteLine(resourceName)
                    'Next

                    viewer.Reset()
                    viewer.LocalReport.ReportEmbeddedResource = "Miharu.Imaging.Library." & _documentosStickerDataTable(0).nombre_RDLC & ".rdlc"
                    viewer.LocalReport.DataSources.Clear()
                    viewer.LocalReport.DataSources.Add(rds)

                    docXMLDevive.LoadXml(_documentosStickerDataTable(0).parametro_Dispositivo)

                    Dim nodo As XmlNodeList = docXMLDevive.GetElementsByTagName("OutputFormat")
                    Extension_Formato_Imagen_Salida = nodo(0).InnerText

                    NombreArchivo = _TempPath & Guid.NewGuid().ToString() & "." & Extension_Formato_Imagen_Salida
                    ImagenSticker = SaveSticker(viewer, NombreArchivo, _documentosStickerDataTable(0).parametro_Dispositivo)
                End If

                Return ImagenSticker

            Catch ex As Exception
                EscribeLog(Me._StrArchivoLog, "Error Generacion ImagenSticker " + ex.Message, False, True)
                Return Nothing
            End Try

        End Function


        Private Function DatosParametros(ByVal dbmImaging As DBImaging.DBImagingDataBaseManager,
                                         ByVal nEntidad As Integer,
                                         ByVal nProyecto As Integer,
                                         ByVal nFechaProceso As Integer,
                                         ByVal nIdOt As Integer,
                                         ByVal nExpediente As Long,
                                         ByVal nFolder As Short,
                                         ByVal nFile As Short,
                                         ByVal nDocumento As Long,
                                         ByVal sqlquery As String,
                                         ByVal nombreParametros As String,
                                         ByVal valorParametros As String) As DataTable
            Dim dt As DataTable = Nothing
            Try
                Dim parametros As String()
                Dim valores As String()
                Dim SqlParameter() As SqlParameter
                parametros = nombreParametros.Split(","c)
                valores = valorParametros.Split(","c)
                ReDim SqlParameter(parametros.Length - 1)
                For i As Integer = 0 To UBound(parametros, 1)
                    Select Case parametros(i)
                        Case "fkEntidad"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nEntidad)
                        Case "fkProyecto"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nProyecto)
                        Case "fkOt"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nIdOt)
                        Case "fkFechaProceso"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nFechaProceso)
                        Case "fkExpediente"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nExpediente)
                        Case "fkFolder"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nFolder)
                        Case "fkFile"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nFile)
                        Case "fkDocumento"
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", nDocumento)
                        Case Else
                            SqlParameter(i) = New SqlParameter("@" + parametros(i) + "", valores(i))
                    End Select
                Next i
                dt = ExecuteQuery(sqlquery, dbmImaging, SqlParameter)
                Return dt
            Catch ex As Exception
            End Try
            Return dt
        End Function

        Private Function ExecuteQuery(ByVal s As String,
                                      ByVal dbmImaging As DBImaging.DBImagingDataBaseManager,
                                      ByVal ParamArray params() As SqlParameter) As DataTable
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

        ' Método para crear una imagen en blanco de 1x1 píxel
        Public Shared Function CreateEmptyImage() As System.Drawing.Image
            Dim whiteImage As New Bitmap(1, 1) ' Crear una imagen de 1x1 píxel
            Using g As Graphics = Graphics.FromImage(whiteImage)
                g.Clear(Color.White) ' Rellenar con blanco (o el color que prefieras)
            End Using
            Return whiteImage
        End Function

        Public Shared Function ImageToByteArray(ByVal Image As System.Drawing.Image) As Byte()

            Try
                Using Ms = New MemoryStream()
                    ' Forzar la clonación de la imagen para evitar que esté bloqueada
                    Using tempImage As New Bitmap(Image.Width, Image.Height, Image.PixelFormat)
                        Using g As Graphics = Graphics.FromImage(tempImage)
                            g.DrawImage(Image, New Rectangle(0, 0, Image.Width, Image.Height))
                        End Using
                        tempImage.SetResolution(Image.HorizontalResolution, Image.VerticalResolution)  ' Establecer la misma resolución en la imagen clonada
                        tempImage.Save(Ms, System.Drawing.Imaging.ImageFormat.Png)                     ' Guardar la imagen clonada en su formato original
                    End Using
                    Return Ms.ToArray()
                End Using
            Catch ex As Exception
                Throw New InvalidOperationException("Error al convertir la imagen a un arreglo de bytes", ex)
            End Try
        End Function

        Public Shared Function ByteArrayToImage(ByVal Bit As Byte()) As System.Drawing.Image
            Using Ms = New MemoryStream(Bit)
                Return Image.FromStream(Ms)
            End Using
        End Function

        Public Function SaveSticker(ByVal viewer As ReportViewer,
                                    ByVal savePath As String,
                                    ByVal DeviceInfo As String) As System.Drawing.Image

            Try

                Dim warnings As Warning() = Nothing
                Dim streamIds As String() = Nothing
                Dim mimeType As String = String.Empty
                Dim encoding As String = String.Empty
                Dim extension As String = String.Empty
                Dim filetype As String = String.Empty
                Dim bytes As Byte() = viewer.LocalReport.Render("Image", DeviceInfo, mimeType, encoding, extension, streamIds, warnings)

                Dim fs As FileStream = New FileStream(savePath, FileMode.OpenOrCreate)
                fs.Write(bytes, 0, bytes.Length)
                fs.Close()

                tempImagePaths.Add(savePath)   ' Almacena la ruta de la imagen temporal

                Return ByteArrayToImage(bytes)

            Catch ex As Exception
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Superpone una imagen sobre otra y guarda el resultado en el formato especificado.
        ''' La posición de la imagen superior se puede ajustar mediante porcentajes relativos.
        ''' </summary>
        ''' <param name="backImage">La imagen de fondo sobre la que se superpondrá la imagen superior.</param>
        ''' <param name="topImage">La imagen que se superpondrá sobre la imagen de fondo.</param>
        ''' <param name="FileName">El nombre del archivo donde se guardará la imagen resultante.</param>
        ''' <param name="FormatoSalida">El formato en el que se guardará la imagen (ej. .tif, .png, .jpeg).</param>
        ''' <param name="TopPosXPercentage">El porcentaje de posición horizontal de la imagen superior.</param>
        ''' <param name="TopPosYPercentage">El porcentaje de posición vertical de la imagen superior.</param>
        ''' <returns>Un arreglo de bytes que representa la imagen resultante.</returns>
        Public Function OverlayImageAndSave(ByVal backImage As Image,
                                                ByVal topImage As Image,
                                                ByVal FileName As String,
                                                ByVal FormatoSalida As String,
                                                Optional ByVal TopPosXPercentage As Integer = 0,
                                                Optional ByVal TopPosYPercentage As Integer = 0) As Byte()

            If backImage Is Nothing Then Throw New ArgumentNullException(paramName:="backImage")

            ' Verificar propiedades de la imagen de fondo (original)
            Dim backImageFormat As String = backImage.RawFormat.ToString()
            Dim backImageWidth As Integer = backImage.Width
            Dim backImageHeight As Integer = backImage.Height
            Dim backImagePixelFormat As Drawing.Imaging.PixelFormat = backImage.PixelFormat

            Dim pppBackImageHorizontal = backImage.HorizontalResolution
            Dim pppBackImageVertical = backImage.VerticalResolution
            Dim pppTopImageHorizontal = topImage.HorizontalResolution
            Dim pppTopImageVertical = topImage.VerticalResolution

            ' Calcular el factor de reescalado
            Dim scaleFactorX As Single = pppTopImageHorizontal / pppBackImageHorizontal
            Dim scaleFactorY As Single = pppTopImageVertical / pppBackImageVertical

            ' Calcular nuevas dimensiones
            Dim newWidth As Integer = CInt(topImage.Width * scaleFactorX)
            Dim newHeight As Integer = CInt(topImage.Height * scaleFactorY)

            Dim resizedTopImage As Image = ResizeImage(topImage, newWidth, newHeight, backImage)

            If topImage Is Nothing Then Throw New ArgumentNullException(paramName:="topImage")
            If (topImage.Width > backImageWidth) OrElse (topImage.Height > backImageHeight) Then
                Throw New ArgumentException("Los límites de la imagen son mayores que la imagen de fondo.", "topImage")
            End If

            ' Calcular las posiciones en píxeles
            Dim posXmax = backImageWidth - resizedTopImage.Width
            Dim posYmax = backImageHeight - resizedTopImage.Height
            Dim topPosX As Integer = Math.Max(0, Math.Min(CInt((posXmax / 100) * TopPosXPercentage), posXmax))
            Dim topPosY As Integer = Math.Max(0, Math.Min(CInt((posYmax / 100) * TopPosYPercentage), posYmax))

            ' Crear un nuevo bitmap y dibujar las imágenes
            Using bmp As New Bitmap(backImageWidth, backImageHeight, backImagePixelFormat)

                ' Establecer la resolución de la nueva imagen igual a la de la imagen original
                bmp.SetResolution(backImage.HorizontalResolution, backImage.VerticalResolution)

                tempImagePaths.Add(FileName)

                Using canvas As Graphics = Graphics.FromImage(bmp)
                    canvas.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    canvas.DrawImage(backImage, 0, 0, bmp.Width, bmp.Height)
                    canvas.DrawImage(resizedTopImage, topPosX, topPosY, resizedTopImage.Width, resizedTopImage.Height)
                End Using

                ' Guardar la imagen en el formato deseado
                Select Case FormatoSalida.ToLowerInvariant()
                    Case ".tif"
                        bmp.Save(FileName, System.Drawing.Imaging.ImageFormat.Tiff)
                    Case ".png"
                        bmp.Save(FileName, System.Drawing.Imaging.ImageFormat.Png)
                    Case Else
                        bmp.Save(FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
                End Select

                Dim ImageArray = ImageToByteArray(bmp)  ' convierte la imagen en bytes()
                Return ImageArray
            End Using
        End Function

        ' Función para reescalar la imagen
        Function ResizeImage(img As Image, width As Integer, height As Integer, backImage As Image) As Image
            Dim newImage As New Bitmap(width, height)
            newImage.SetResolution(backImage.HorizontalResolution, backImage.VerticalResolution)  ' Establecer la resolución de la nueva imagen igual a la de la imagen original
            Using g As Graphics = Graphics.FromImage(newImage)
                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                g.DrawImage(img, 0, 0, width, height)
            End Using
            Return newImage
        End Function


        Private Function Validar() As Boolean
            If OTDesktopComboBox.Items.Count <= 0 Then
                MessageBox.Show("La Fecha de Proceso seleccionada no contiene OT(s)", "Estampado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
            Return True
        End Function
#End Region
    End Class

    Public Class ProcesadorHilos

        Private NumHilos As Integer
        Private ListaHilos As New List(Of Thread)
        Public servicio As FormGeneraSticker

        Public Sub New(nHilos As Integer)
            Me.NumHilos = nHilos
        End Sub

        Public Sub AgregarHilo(ByVal objectArray As Object)
            Dim ArrayParameters As ArrayList = CType(objectArray, ArrayList)
            If TieneHiloslibres() = False Then
                Do
                    Thread.Sleep(100)
                    If TieneHiloslibres() Then
                        Exit Do
                    End If
                Loop
            End If
            Dim x As New System.Threading.Thread(AddressOf servicio.GeneraStickerHilos)
            x.Start(ArrayParameters)
            ListaHilos.Add(x)
        End Sub

        Public Function TieneHiloslibres() As Boolean
            For Each hilo In ListaHilos
                If hilo.ThreadState = ThreadState.Stopped Then
                    ListaHilos.Remove(hilo)
                End If
                If ListaHilos.Count = 0 Then
                    Exit For
                End If
            Next
            If ListaHilos.Count < NumHilos Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Function TerminoHilos() As Boolean
            For Each hilo In ListaHilos
                If hilo.ThreadState <> ThreadState.Stopped Then
                    Return False
                End If
            Next
            Return True
        End Function
        Public Function TotalHilos() As Integer
            Return ListaHilos.Count
        End Function
    End Class

End Namespace