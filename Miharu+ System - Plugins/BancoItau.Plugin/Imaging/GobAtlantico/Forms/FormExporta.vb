Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports Miharu.Imaging.Library.Eventos
Imports System.Windows.Forms
Imports Miharu.Tools.Progress
Imports System.IO
Imports Slyg.Tools.Imaging
Imports System.Threading
Imports Miharu.Imaging.Indexer
Imports System.Data.SqlClient
Imports Miharu.FileProvider.Library
Imports DBImaging
Imports Miharu.Imaging
Imports System.Globalization
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports BancoItau.Plugin.Imaging.GobAtlantico
Imports Ionic.Zip
Imports DBIntegration




Namespace Imaging.GobAtlantico.Forms
    Public Class FormExporta
        Inherits FormBase

#Region " Declaraciones "
        Public _plugin As GobAtlanticoPlugin
        Private Usa_Exportacion_PDF As Boolean
        Private formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Private CompresionAux As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Private formato As Slyg.Tools.Imaging.ImageManager.EnumFormat
        Dim compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression
        Private ViewExpedientes As New DataView
        Private ExpedientesSeleccion As New DataTable
        Public Shared FileNamesCons As New List(Of String)
        Dim FolderNameOutput As String
        Dim _EventManager As EventManager
        Dim StrArchivoLog As String
        Dim Cedula As String
        Dim FormatoCSV As String
        Dim FormatoCSVfile As String
        Private ArrayNotificacion As ArrayList
        Private BloqueoConcurrencia As Object
        Private ArrayNotificacionTapas As ArrayList
        Private BloqueoConcurrenciaTapas As Object = New Object
        Private ProgressForm As New FormProgress()
        Private Shared opened As Integer
        Dim contador As Integer
        Dim contadorFile As Integer
        Dim idOT As Integer
        Dim FechaInicial As DateTime
        Dim FechaFinal As DateTime
        Dim _tempPath As String = Nothing
        Dim FileName As String
        Private ConnectionString_Tools As String
        Dim directorio As String
        Dim OutputFolder As String
#End Region

#Region " Propiedades"
        Public Property EventManager As EventManager
            Get
                Return Me._EventManager
            End Get
            Set(value As EventManager)
                _EventManager = value
            End Set
        End Property
#End Region

#Region " Eventos "
        Public Sub FormExporta_Load(sender As Object, e As System.EventArgs)
            If Not (Me._plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_Validos) Then
            End If

            Usa_Exportacion_PDF = Me._plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF
            formato = Utilities.GetEnumFormat(Me._plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida)
            compresion = Utilities.GetEnumCompression(CType(Me._plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

            Load_FormatoCargue()

        End Sub

        Private Sub BuscarFechaButton_Click(sender As System.Object, e As EventArgs)

            If validarFechaProceso() Then
                CargarOTs()
            End If

        End Sub

        Private Sub BuscarCarpetaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarCarpetaButton.Click
            Dim Selector As New FolderBrowserDialog()
            If validarFechaProceso() Then
                Selector.SelectedPath = CarpetaSalidaTextBox.Text
                If (Selector.ShowDialog() = DialogResult.OK) Then
                    Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
                End If
            End If

        End Sub

        Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click
            ExportarImagenes()
        End Sub

        Private Sub EnviarCorreoButton_Click(sender As System.Object, e As System.EventArgs) Handles EnviarCorreoButton.Click
            Dim Respuesta = DMB.DesktopMessageShow("¿Está seguro que desea Enviar Correo?", "EnviarCorreo", DMB.IconEnum.AdvertencyIcon, False)

            If (Respuesta = DialogResult.OK) Then
                EnviarCorreo()
            End If
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

        Private Sub ExpedientesDataGridView_ColumnHeaderMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ExpedientesDataGridView.ColumnHeaderMouseDoubleClick
            If e.ColumnIndex = 3 Then
                For i = 0 To ExpedientesDataGridView.RowCount - 1
                    ExpedientesDataGridView.Rows(i).Cells("Exportar").Value = True
                Next
            End If
        End Sub
#End Region


#Region " Metodos "
        Private Sub CargarOTs()
            Me.OTDataGridView.AutoGenerateColumns = False
            Me.OTDataGridView.DataSource = getOTs()
            Me.OTDataGridView.Refresh()

            If (Me.OTDataGridView.RowCount = 0) Then
                MessageBox.Show("No se encontraron OTs para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub ExportarImagenes()
            If (Validar()) Then
                Dim ProgressForm As New FormProgress
                FileNamesCons = New List(Of String)
                Dim dtItemFile As DataTable
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                ArrayNotificacionTapas = New ArrayList
                Dim Fecha_Inicial As Date = FechaInicial
                Dim fdtIni As String = Format(Fecha_Inicial, "yyyy-MM-dd")
                Dim Fecha_Final As Date = FechaFinal
                Dim fdtFin As String = Format(Fecha_Final, "yyyy-MM-dd")

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Me._plugin.Manager.Sesion.Usuario.id)


                    Dim ExportDataFile = dbmImaging.SchemaProcess.PA_Exportacion_Files_GobAntioquia.DBExecute(fdtIni, fdtFin, GobAtlanticoPlugin.Imaging_EntidadId, GobAtlanticoPlugin.Imaging_BancoItau_ProyectoId)


                    Try
                        Me.Enabled = False

                        dbmCore = New DBCore.DBCoreDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Core)


                        OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                        Dim FilesDataView As New DataView(ExportDataFile)
                        Dim Progreso As Integer = 0

#If Not Debug Then
                            ProgressForm.Show()
#End If

                        ProgressForm.SetProceso("Exportar")
                        ProgressForm.SetAccion("Obteniendo imágenes...")
                        ProgressForm.SetProgreso(0)
                        ' ProgressForm.SetMaxValue(TotalesDataTable(0).Folios)

                        Application.DoEvents()

                        ' Crear el directorio de las imágenes
                        'Directory.CreateDirectory(OutputFolder & "images")

                        Dim Compresion As ImageManager.EnumCompression

                        If (Me._plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                            Compresion = ImageManager.EnumCompression.Ccitt4
                        Else
                            Compresion = ImageManager.EnumCompression.Lzw
                        End If

                        Dim manager As FileProviderManager = Nothing


                        Try

                            Dim CodDANE As List(Of Object) = Nothing

                            CodDANE = (From a In ExportDataFile Group a By GroupDt = a.Field(Of Integer)("Codigo_Dane") Into Group Select Group.Select(Function(x) x("Codigo_Dane")).First()).ToList()
                            For Each ItemFile In CodDANE

                                Dim FileFolderName = ItemFile & "\"
                                FolderNameOutput = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\" & FileFolderName

                                If (Not Directory.Exists(OutputFolder & FileFolderName)) Then
                                    Directory.CreateDirectory(OutputFolder & FileFolderName)
                                End If

                                Dim itemOT = From o In ExportDataFile
                                Where o.ItemArray(0) = ItemFile
                                Select o.ItemArray(2)

                                idOT = itemOT.FirstOrDefault()

                                Dim servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Convert.ToInt32(itemOT.FirstOrDefault()))(0).ToCTA_ServidorSimpleType()
                                Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me._plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad,
                                                                                                                                                 Me._plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede,
                                                                                                                                                 Me._plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                                manager = New FileProviderManager(servidor, centro, dbmImaging, Me._plugin.Manager.Sesion.Usuario.id)
                                manager.Connect()


                                dtItemFile = ExportDataFile.Select("Codigo_Dane =" + ItemFile.ToString()).CopyToDataTable

                                For Each itemrow In dtItemFile.Rows
                                    '    ' Enviar el archivo
                                    ExportarImagen(manager, itemrow, Compresion, OutputFolder & FileFolderName, FileFolderName)

                                    Progreso += 1
                                    ProgressForm.SetProgreso(Progreso)
                                    Application.DoEvents()

                                Next
                                If (manager IsNot Nothing) Then manager.Disconnect()
                                ' Obtener los Files a transferir 
                                Dim FileNames(0) As String
                                Dim filtro As String
                                filtro = ".jpg"
                                directorio = FolderNameOutput
                                If directorio = "" Then
                                    FileNames(0) = filtro
                                Else
                                    FileNames = Directory.GetFiles(directorio)
                                End If

                                ' Crear la carpeta en .ZIP
                                If FileNames.Count > 0 Then
                                    Dim ZipFileName As String = OutputFolder & ItemFile & ".zip"

                                    Using zip As ZipFile = New ZipFile()
                                        zip.AddFiles(FileNames, False, "")
                                        zip.Save(ZipFileName)
                                    End Using

                                    My.Computer.FileSystem.DeleteDirectory(OutputFolder & ItemFile, FileIO.DeleteDirectoryOption.DeleteAllContents)

                                End If
                            Next
                        Catch ex As Exception
                            If (manager IsNot Nothing) Then manager.Disconnect()
                            Throw (ex)
                        End Try

                        If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")


                    Catch ex As Exception
                        MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ProgressForm.Hide()
                        Application.DoEvents()
                        Return
                    Finally
                        Me.Enabled = True
                        EnviarCorreoButton.Enabled = True
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()

                        BorrarTemporal()

                        ProgressForm.Close()
                    End Try
                    MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If

            BorrarTemporal()
        End Sub
        Private Declare Auto Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal procHandle As IntPtr, ByVal min As Int32, ByVal max As Int32) As Boolean
        Public Sub ClearMemory()

            Try
                Dim Mem As Process
                Mem = Process.GetCurrentProcess()
                SetProcessWorkingSetSize(Mem.Handle, -1, -1)
            Catch ex As Exception
                'Control de errores
            End Try
        End Sub

        Private Sub ProcesoHilos(ByVal objectArray As Object)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim manager As FileProviderManager = Nothing

            Dim ArraListParameters As ArrayList = objectArray

            Dim nservidor As DBImaging.SchemaCore.CTA_ServidorSimpleType = ArraListParameters(0)
            Dim ncentro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType = ArraListParameters(1)
            Dim nManager As FileProviderManager = ArraListParameters(2)
            Dim nCompresion As Slyg.Tools.Imaging.ImageManager.EnumCompression = ArraListParameters(3)
            Dim nItemFile As DataTable = ArraListParameters(4)
            Dim npathFileName As String = ArraListParameters(5)
            Dim ExportDataFilePDFA As DataTable = ArraListParameters(6)
            Dim nHiloPrin As Integer = ArraListParameters(7)
            Dim nPathTemp As String = String.Empty
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                manager = New FileProviderManager(nservidor, ncentro, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()
                SyncLock BloqueoConcurrenciaTapas
                    For Each itemFileRow In nItemFile.Rows
                        ' ExportarImagen(manager, itemFileRow, nCompresion, npathFileName, npathFileName, ExportDataFilePDFA, nPathTemp)
                        ClearMemory()
                    Next

                End SyncLock
                If (manager IsNot Nothing) Then manager.Disconnect()
                EscribeLog(npathFileName, "Exportacion de imagenes del hilo con ruta: " + npathFileName + " terminada.", False, True)
            Catch ex As Exception
                EscribeLog(npathFileName, "Error Proceso Hilos: " + npathFileName + " " + ex.Message, False, True)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
                SyncLock BloqueoConcurrenciaTapas
                    ArrayNotificacionTapas(nHiloPrin) = True
                End SyncLock
                ' Interlocked.Decrement(opened)
            End Try
        End Sub

        Public Sub EscribeLog(pathStrFile As String, StrLine As String, Optional CrearFile As Boolean = False, Optional LeerFile As Boolean = False)
            Dim modeFile As FileMode = Nothing
            Dim modeFile_2 As FileMode = Nothing
            Dim strMessageComplete As String = StrLine + Environment.NewLine
            Try
                If (CrearFile) Then
                    modeFile = FileMode.CreateNew
                    modeFile_2 = FileAccess.Write
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

        Private Sub TablaDesdeTemporal(ByVal tabla As DataTable, ByVal temporal As DataTable)
            For Each drow As DataRow In temporal.Rows

                Dim newRow As DataRow = tabla.NewRow()

                For Each col As DataColumn In temporal.Columns
                    newRow(col.Ordinal) = drow(col.Ordinal)
                Next

                tabla.Rows.Add(newRow)
            Next
        End Sub

        Private Function ExecuteQuery(ByVal s As String, ByVal condb As SqlConnection, ByVal ParamArray params() As SqlParameter) As DataTable
            Dim dt As DataTable = Nothing
            Using da As New System.Data.SqlClient.SqlDataAdapter(s, condb)
                Try
                    dt = New DataTable
                    If params.Length > 0 Then
                        da.SelectCommand.Parameters.AddRange(params)
                    End If
                    If da.SelectCommand.Connection.State <> ConnectionState.Open Then da.SelectCommand.Connection.Open()
                    da.SelectCommand.CommandTimeout = 86400
                    da.Fill(dt)
                    Return dt
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (da IsNot Nothing) Then da.SelectCommand.Connection.Close()
                End Try
                Return dt
            End Using
        End Function

        Private Sub ExportarImagen(nManager As FileProviderManager, ByVal ItemFile As DataRow, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, nFolderName As String)
            Dim Folios = nManager.GetFolios(CLng(ItemFile.Item("fk_Expediente")), CShort(ItemFile.Item("fk_Folder")), CShort(ItemFile.Item("fk_File")), CShort(ItemFile.Item("id_Version")))

            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = String.Empty

            Try
                For folio As Short = 1 To Folios
                    Dim Imagen() As Byte = Nothing
                    Dim Thumbnail() As Byte = Nothing

                    nManager.GetFolio(CLng(ItemFile.Item("fk_Expediente")), CShort(ItemFile.Item("fk_Folder")), CShort(ItemFile.Item("fk_File")), CShort(ItemFile.Item("id_Version")), folio, Imagen, Thumbnail)

                    FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Me._plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    FileNames.Add(FileName)

                    FileNamesCons.Add(FileName)

                    Using fs = New FileStream(FileName, FileMode.Create)
                        fs.Write(Imagen, 0, Imagen.Length)
                        fs.Close()
                    End Using
                Next

                If Not (Me._plugin.Manager.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF) Then

                    If (FileNames.Count > 0) Then
                        Dim Format As ImageManager.EnumFormat

                        Select Case Me._plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                            Case ".bmp"
                                Format = ImageManager.EnumFormat.Bmp
                            Case ".gif"
                                Format = ImageManager.EnumFormat.Gif
                            Case ".jpg"
                                Format = ImageManager.EnumFormat.Jpeg
                                nCompresion = ImageManager.EnumCompression.Jpeg
                            Case ".pdf"
                                Format = ImageManager.EnumFormat.Pdf
                                nCompresion = ImageManager.EnumCompression.Jpeg
                            Case ".png"
                                Format = ImageManager.EnumFormat.Png
                            Case ".tif"
                                Format = ImageManager.EnumFormat.Tiff
                        End Select

                        Dim Valido As Boolean = True
                        Dim MsgError As String = ""

                        ExtensionAux = IIf(formatoAux = ImageManager.EnumFormat.Jpeg, ".jpg", ".jpg").ToString
                        'formatoAux = ImageManager.EnumFormat.Jpeg
                        If ((Valido = True) And (FileNameAux = String.Empty)) Then
                            FileNameAux = ItemFile.Item("No_Formulario").ToString()
                            FileName = nFileFolderName & FileNameAux & ExtensionAux
                        ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                            ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, formatoAux, ExtensionAux))
                            FileName = nFileFolderName & FileNameAux & ExtensionAux
                        ElseIf Valido = False Then
                            Throw New Exception(MsgError)
                        End If

                        '-------------------------------------------------------------------------
                        ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, False, Program.AppPath & Program.TempPath, False, False)
                        '-------------------------------------------------------------------------

                        ItemFile.Item("Nombre_Imagen_File") = nFolderName & FileNameAux & ExtensionAux
                    End If
                End If
            Catch ex As Exception
                DMB.DesktopMessageShow("Exportar imagen", ex)
            End Try
        End Sub

        Private Sub EnviarCorreo()
            Dim attach() As Byte = Nothing
            Dim dbmCore = New DBCore.DBCoreDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
            Dim Genera_ZIP_Archivo As Boolean
            Dim dbmIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing
            Dim Id_DANE As Int32
            Genera_ZIP_Archivo = True
            Dim obtenetMunicipio
            Dim strOficina As String
            Dim ruta As String

            Try
                dbmCore.Connection_Open(Me._plugin.Manager.Sesion.Usuario.id)
                dbmIntegration = New DBIntegration.DBIntegrationDataBaseManager(_plugin.BancoItauConnectionString)
                dbmIntegration.Connection_Open(1)
                'Iniciar proceso
                Dim ProgressForm As New FormProgress()
                ProgressForm.SetProceso("Exportar")
                ProgressForm.SetAccion("Obteniendo imágenes...")
                ProgressForm.SetProgreso(0)

#If Not Debug Then
                            ProgressForm.Show()
#End If

                Dim FechaProceso = Integer.Parse(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd").Replace("/", ""))
                Dim Selector As New FolderBrowserDialog()
                If FolderNameOutput = String.Empty Then
                    If (Selector.ShowDialog() = DialogResult.OK) Then
                        Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
                        FolderNameOutput = Me.CarpetaSalidaTextBox.Text
                        obtenetMunicipio = Directory.GetFiles(Selector.SelectedPath)
                        ruta = Selector.SelectedPath
                    End If
                Else
                    obtenetMunicipio = Directory.GetFiles(OutputFolder)
                    ruta = OutputFolder
                End If

                For Each ItemMuni In obtenetMunicipio
                    strOficina = ItemMuni.ToString().Replace(ruta, "")
                    Id_DANE = Convert.ToInt32(strOficina.Replace(".zip", ""))
                    Dim NotificacionDataTable = dbmIntegration.SchemaBcoItau.PA_Consulta_Emails_Por_Oficina.DBExecute(_plugin.Manager.ImagingGlobal.Entidad, Id_DANE)

                    Dim i = 0
                    ProgressForm.SetAccion("Enviando Correo")
                    ProgressForm.SetProgreso(1)
                    Application.DoEvents()

                    For Each itemNotificacion In NotificacionDataTable.Rows

                        Dim Mensaje As String = ""
                        Dim EnvioCorreo As Boolean = False

                        Mensaje = DirectCast(itemNotificacion, System.Data.DataRow).ItemArray(1) & DirectCast(itemNotificacion, System.Data.DataRow).ItemArray(2) & DirectCast(itemNotificacion, System.Data.DataRow).ItemArray(3)

                        ' Crear archivo adjunto
                        Dim mimeType As String = ""
                        Dim encoding As String = ""
                        Dim fileNameExtension As String = ""
                        Dim streamids As String() = Nothing

                        Try
                            If Genera_ZIP_Archivo Then
                                attach = GetFile(ItemMuni)
                            End If
                        Catch
                        End Try

                        'realiza agendamiento de mail para envio
                        If SendMail(DirectCast(itemNotificacion, System.Data.DataRow).ItemArray(4), "", "", DirectCast(itemNotificacion, System.Data.DataRow).ItemArray(0), Mensaje, strOficina, attach) Then
                        Else
                            MessageBox.Show("Se ha presentado un error al generar y enviar el reporte", "Envio correo - Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        ProgressForm.Close()
                    Next
                Next
                MessageBox.Show("El reporte ha sido enviado exitosamente.", "Envio correo - Reporte", MessageBoxButtons.OK, MessageBoxIcon.None)
            Catch ex As Exception
                DMB.DesktopMessageShow("EnvioCorreo", ex)
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Public Function GetFile(ByVal FileName As String) As Byte()
            Dim fsInput As New FileStream(FileName, FileMode.Open, FileAccess.Read)
            Dim Longitud As Integer = CInt(fsInput.Length - 1)
            Dim Data(Longitud) As Byte

            fsInput.Read(Data, 0, Data.Length)
            fsInput.Close()

            Return Data
        End Function

        Private Function SendMail(ByVal MailTo As String, ByVal MailCC As String, ByVal MailCCO As String, ByVal Subject As String, ByVal Message As String, nAttachName As String, nAttach As Byte()) As Boolean
            Dim DBMTools As DBTools.DBToolsDataBaseManager = Nothing
            Dim SendMailExitoso As Boolean = False

            Try
                DBMTools = New DBTools.DBToolsDataBaseManager(_plugin.ToolsConnectionString)
                DBMTools.Connection_Open()

                DBMTools.InsertMail(_plugin.Manager.ImagingGlobal.Entidad, _plugin.Manager.Sesion.Usuario.id, MailTo, MailCC, MailCCO, Subject, Message, nAttachName, nAttach)
                SendMailExitoso = True
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Envio de Correo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SendMailExitoso = False
            Finally
                DBMTools.Connection_Close()
            End Try
            Return SendMailExitoso
        End Function

        Private Sub InsertarLogCargue(fk_Llave As Int32, fk_expediente As Int32, Estado As Boolean, Fecha_inicio As Date, Fecha_Fin As Date, fk_nombreImagen As String, Token As String, npathFileName As String, NumeroFolios As Int32)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try

                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()
                Dim ExportDataFilePDFA = dbmImaging.SchemaProcess.PA_Exportacion_Log.DBExecute(fk_Llave, fk_expediente, fk_nombreImagen, Token, Estado, Fecha_inicio, Fecha_Fin, NumeroFolios)
                dbmImaging.Transaction_Commit()
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                EscribeLog(npathFileName, "Error al insertal en tablas TBL_CargueLog y TBLCargueLogDetalle:" + npathFileName + " " + fk_nombreImagen, False, True)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()
                Dim ExportDataFilePDFA = dbmImaging.SchemaProcess.PA_Exportacion_Log.DBExecute(fk_Llave, fk_expediente, fk_nombreImagen, Token, Estado, Fecha_inicio, Fecha_Fin, NumeroFolios)
                dbmImaging.Transaction_Commit()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Function GetFileName() As String
            'Return _EventManager.Nombre_Imagen_Exportar
            Return ""
        End Function

        Public Sub BorrarTemporal()
            Dim objDirectoryInfo = New DirectoryInfo(Program.AppPath & Program.TempPath)
            Dim fileInfoArray As FileInfo() = objDirectoryInfo.GetFiles()
            Dim objFileInfo As FileInfo
            For Each objFileInfo In fileInfoArray
                Try
                    objFileInfo.Delete()
                Catch
                End Try
            Next objFileInfo
        End Sub

        Private Sub GenerarVisor(dbmCore As DBCore.DBCoreDataBaseManager, dbmImaging As DBImaging.DBImagingDataBaseManager, idOT As Integer, OutputFolder As String, FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable, FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, FileValidacionesDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable)
            Const DataBaseName As String = "ExportedData.accdb"
            Dim ConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & OutputFolder & DataBaseName & ";Persist Security Info=False"

            Dim Conexion As OleDb.OleDbConnection = Nothing


            Try

                ' Copiar visor
                If Not File.Exists(OutputFolder & DataBaseName) Then
                    File.Copy(Program.AppPath & "OffLineViewer\ExportedData.accdb", OutputFolder & DataBaseName, True)
                End If
                If Not File.Exists(OutputFolder & "OffLineViewer.exe") Then
                    File.Copy(Program.AppPath & "OffLineViewer\OffLineViewer.exe", OutputFolder & "OffLineViewer.exe", True)
                End If
                If Not File.Exists(OutputFolder & "OffLineViewer.Library.dll") Then
                    File.Copy(Program.AppPath & "OffLineViewer\OffLineViewer.Library.dll", OutputFolder & "OffLineViewer.Library.dll", True)
                End If


                Conexion = New OleDb.OleDbConnection(ConnectionString)
                Conexion.Open()

                Dim Comando = New OleDb.OleDbCommand("", Conexion)

                ' Llaves
                Dim KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

                Dim KeyName1 As String = ""
                If (KeysDataTable.Count > 0) Then KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave
                Dim KeyName2 As String = ""
                If (KeysDataTable.Count > 1) Then KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave
                Dim KeyName3 As String = ""
                If (KeysDataTable.Count > 2) Then KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave

                ' Crear Configuracion

                Dim Dtresultados As New DataTable
                Dim adapter As OleDb.OleDbDataAdapter


                Comando.CommandText = "SELECT * FROM TBL_Config WHERE id_Entidad = " & Program.ImagingGlobal.Entidad &
                                        " AND id_Proyecto = " & Program.ImagingGlobal.Proyecto & ";"

                adapter = New OleDb.OleDbDataAdapter(Comando)
                adapter.Fill(Dtresultados)

                If Dtresultados.Rows.Count = 0 Then
                    Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Program.ImagingGlobal.Entidad)
                    Comando.CommandText = " INSERT INTO TBL_Config (id_Entidad, Nombre_Entidad, id_Proyecto, Nombre_Proyecto, Key_1, Key_2, Key_3)" &
                                            "SELECT " & Program.ImagingGlobal.Entidad &
                                            ", '" & EntidadDataTable(0).Nombre_Entidad & "', " &
                                            Program.ImagingGlobal.Proyecto &
                                            ", '" & Program.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto &
                                            "', '" & KeyName1 &
                                            "', '" & KeyName2 &
                                            "', '" & KeyName3 & "';"

                    Comando.ExecuteNonQuery()
                End If



                ' Crear los Esquemas
                Dim EsquemasDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Esquema.DBExecute(idOT)



                For Each EsquemaRow In EsquemasDataTable

                    Comando.CommandText = "SELECT * FROM TBL_Esquema WHERE id_Esquema = " & EsquemaRow.id_Esquema & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Esquema (id_Esquema, Nombre_Esquema)" &
                                            "SELECT " & EsquemaRow.id_Esquema &
                                            ", '" & EsquemaRow.Nombre_Esquema & "';"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear los Documentos
                Dim DocumentosDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Documento.DBExecute(idOT)
                For Each DocumentoRow In DocumentosDataTable

                    Comando.CommandText = "SELECT * FROM TBL_Documento WHERE id_Documento = " & DocumentoRow.id_Documento & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)
                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Documento (id_Documento, Nombre_Documento)" &
                                            "SELECT " & DocumentoRow.id_Documento &
                                            ", '" & DocumentoRow.Nombre_Documento & "';"

                        Comando.ExecuteNonQuery()
                    End If

                Next

                ' Crear Campos de Búsqueda
                Dim CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT)
                For Each CampoBusquedaRow In CampoBusquedaDataTable

                    Comando.CommandText = "SELECT * FROM TBL_Campo_Busqueda WHERE" &
                                        " fk_Campo_Tipo = " & CampoBusquedaRow.fk_Campo_Tipo &
                                        " AND id_Campo_Busqueda = " & CampoBusquedaRow.id_Campo_Busqueda & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Campo_Busqueda (fk_Campo_Tipo, id_Campo_Busqueda, Nombre_Campo_Busqueda)" &
                                            "SELECT " & CampoBusquedaRow.fk_Campo_Tipo &
                                            ", " & CampoBusquedaRow.id_Campo_Busqueda &
                                            ", '" & CampoBusquedaRow.Nombre_Campo_Busqueda & "';"

                        Comando.ExecuteNonQuery()

                    End If

                Next

                ' Crear los Campos
                Dim CamposDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo.DBExecute(idOT)
                For Each CampoRow In CamposDataTable

                    Comando.CommandText = "SELECT * FROM TBL_Campo WHERE" &
                                        " fk_Documento = " & CampoRow.fk_Documento &
                                        " AND id_Campo = " & CampoRow.id_Campo & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Campo (fk_Documento, id_Campo, Nombre_Campo, Es_Campo_Busqueda, fk_Campo_Tipo, fk_Campo_Busqueda)" &
                                            "SELECT " & CampoRow.fk_Documento &
                                            ", " & CampoRow.id_Campo &
                                            ", '" & CampoRow.Nombre_Campo & "'" &
                                            ", " & IIf(CampoRow.Es_Campo_Busqueda, "1", "0").ToString() &
                                            ", " & CampoRow.fk_Campo_Tipo &
                                            ", " & CampoRow.fk_Campo_Busqueda & ";"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear las Validaciones
                Dim ValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validacion.DBExecute(idOT)
                For Each ValidacionRow In ValidacionDataTable

                    Comando.CommandText = "SELECT * FROM TBL_Validacion WHERE" &
                                        " fk_Documento = " & ValidacionRow.fk_Documento &
                                        " AND id_Validacion = " & ValidacionRow.id_Validacion & ";"

                    Dtresultados = New DataTable
                    adapter = New OleDb.OleDbDataAdapter(Comando)
                    adapter.Fill(Dtresultados)

                    If Dtresultados.Rows.Count = 0 Then
                        Comando.CommandText = "INSERT INTO TBL_Validacion (fk_Documento, id_Validacion, Pregunta_Validacion)" &
                                            "SELECT " & ValidacionRow.fk_Documento &
                                            ", " & ValidacionRow.id_Validacion &
                                            ", '" & ValidacionRow.Pregunta_Validacion & "';"

                        Comando.ExecuteNonQuery()
                    End If
                Next

                ' Crear Folders            
                For Each FolderRow In FolderDataTable
                    Comando.CommandText = "INSERT INTO TBL_Folder (fk_Expediente, id_Folder, fk_Esquema, Key_1, Key_2, Key_3, CBarras_Folder)" &
                                        "SELECT " & FolderRow.fk_Expediente &
                                        ", " & FolderRow.fk_Folder &
                                        ", " & FolderRow.id_Esquema &
                                        ", '" & FolderRow.Key_1 & "'" &
                                        ", '" & FolderRow.Key_2 & "'" &
                                        ", '" & FolderRow.Key_3 & "'" &
                                        ", '" & FolderRow.CBarras_Folder & "';"

                    Comando.ExecuteNonQuery()
                Next

                ' Crear Files            
                For Each FileRow In FileDataTable
                    Comando.CommandText = "INSERT INTO TBL_File (fk_Expediente, fk_Folder, id_File, id_Version, File_Unique_Identifier, fk_Documento, Nombre_Imagen_File, Folios_Documento_File, Tamaño_Imagen_File)" &
                                        "SELECT " & FileRow.fk_Expediente &
                                        ", " & FileRow.fk_Folder &
                                        ", " & FileRow.fk_File &
                                        ", " & FileRow.id_Version &
                                        ", '" & FileRow.File_Unique_Identifier.ToString() & "'" &
                                        ", " & FileRow.fk_Documento &
                                        ", '" & FileRow.Nombre_Imagen_File & "'" &
                                        ", " & FileRow.Folios_Documento_File &
                                        ", " & FileRow.Tamaño_Imagen_File & ";"

                    Comando.ExecuteNonQuery()
                Next

                ' Crear File Data            
                For Each DataRow In FileDataDataTable
                    'Dim valor As String = ""
                    'If (Not DataRow.IsNull("Valor_File_Data")) Then valor = DataRow.Valor_File_Data
                    Comando.CommandText = "INSERT INTO TBL_File_Data (fk_Expediente, fk_Folder, fk_File, fk_Version, fk_Campo, fk_Documento, fk_Campo_Tipo, Valor_File_Data)" &
                                        "SELECT " & DataRow.fk_Expediente &
                                        ", " & DataRow.fk_Folder &
                                        ", " & DataRow.fk_File &
                                        ", " & DataRow.id_Version &
                                        ", " & DataRow.id_Campo &
                                        ", " & DataRow.fk_Documento &
                                        ", " & DataRow.fk_Campo_Tipo &
                                        ", '" & DataRow.Valor_File_Data & "';"

                    Comando.ExecuteNonQuery()
                Next

                ' Crear File Validacion            
                For Each DataRow In FileValidacionesDataTable
                    Comando.CommandText = "INSERT INTO TBL_File_Validacion (fk_Expediente, fk_Folder, fk_File, fk_Version, fk_Validacion, fk_Documento, Respuesta)" &
                                        "SELECT " & DataRow.fk_Expediente &
                                        ", " & DataRow.fk_Folder &
                                        ", " & DataRow.fk_File &
                                        ", " & DataRow.id_Version &
                                        ", " & DataRow.id_Validacion &
                                        ", " & DataRow.fk_Documento &
                                        ", " & IIf(DataRow.Respuesta, "1", "0").ToString() & ";"

                    Comando.ExecuteNonQuery()
                Next
            Catch ex As Exception
                Throw
            Finally
                If (Conexion IsNot Nothing) Then Conexion.Close()
            End Try
        End Sub

        Private Sub GenerarXML(dbmCore As DBCore.DBCoreDataBaseManager, dbmImaging As DBImaging.DBImagingDataBaseManager, idOT As Integer, OutputFolder As String, FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable, FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable)
            Dim ExportacionDataSet As New OffLineViewer.Library.xsdOffLineData

            ' Llaves
            Dim KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

            Dim KeyName1 As String = ""
            If (KeysDataTable.Count > 0) Then KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave
            Dim KeyName2 As String = ""
            If (KeysDataTable.Count > 1) Then KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave
            Dim KeyName3 As String = ""
            If (KeysDataTable.Count > 2) Then KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave

            ' Configuración
            Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Program.ImagingGlobal.Entidad)
            ExportacionDataSet.TBL_Config.AddTBL_ConfigRow(Program.ImagingGlobal.Entidad, EntidadDataTable(0).Nombre_Entidad, Program.ImagingGlobal.Proyecto, Program.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto, KeyName1, KeyName2, KeyName3)

            ' Folder
            For Each FolderRow In FolderDataTable
                ExportacionDataSet.TBL_Folder.AddTBL_FolderRow(FolderRow.fk_Expediente, FolderRow.fk_Folder, FolderRow.id_Esquema, FolderRow.Nombre_Esquema, FolderRow.Key_1, FolderRow.Key_2, FolderRow.Key_3, FolderRow.CBarras_Folder)
            Next

            ' File
            For Each FileRow In FileDataTable
                ExportacionDataSet.TBL_File.AddTBL_FileRow(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, FileRow.File_Unique_Identifier, FileRow.Nombre_Documento, FileRow.Nombre_Imagen_File, FileRow.Folios_Documento_File, FileRow.Tamaño_Imagen_File)
            Next

            ' Data
            For Each FileDataRow In FileDataDataTable
                ExportacionDataSet.TBL_File_Data.AddTBL_File_DataRow(FileDataRow.fk_Expediente, FileDataRow.fk_Folder, FileDataRow.fk_File, FileDataRow.id_Version, FileDataRow.id_Campo, FileDataRow.Nombre_Campo, FileDataRow.Es_Campo_Busqueda, FileDataRow.fk_Campo_Tipo, FileDataRow.fk_Campo_Busqueda, FileDataRow.Valor_File_Data, FileDataRow.fk_Documento)
            Next

            ' Validaciones
            For Each FileValidacionRow In FileValidacionDataTable
                ExportacionDataSet.TBL_File_Validacion.AddTBL_File_ValidacionRow(FileValidacionRow.fk_Expediente, FileValidacionRow.fk_Folder, FileValidacionRow.fk_File, FileValidacionRow.id_Version, FileValidacionRow.id_Validacion, FileValidacionRow.Pregunta_Validacion, FileValidacionRow.Respuesta, FileValidacionRow.fk_Documento)
            Next

            ' Busqueda
            Dim CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT)
            For Each CampoBusquedaRow In CampoBusquedaDataTable
                ExportacionDataSet.TBL_Campo_Busqueda.AddTBL_Campo_BusquedaRow(CampoBusquedaRow.fk_Campo_Tipo, CampoBusquedaRow.id_Campo_Busqueda, CampoBusquedaRow.Nombre_Campo_Busqueda)
            Next


            ExportacionDataSet.WriteXml(OutputFolder & "\" & "ExportedData.xml")
        End Sub

        Private Sub GenerarXMLExpedientes(Generar As Boolean, dbmCore As DBCore.DBCoreDataBaseManager, dbmImaging As DBImaging.DBImagingDataBaseManager, _
                                                idOT As Integer, OutputFolder As String, _
                                                FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, _
                                                FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable, _
                                                FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, _
                                                FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable, _
                                                ExportacionDataSet As Miharu.Imaging.OffLineViewer.Library.xsdOffLineData)
            'Dim ExportacionDataSet As New OffLineViewer.Library.xsdOffLineData

            ' Llaves
            Dim KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)

            Dim KeyName1 As String = ""
            If (KeysDataTable.Count > 0) Then KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave
            Dim KeyName2 As String = ""
            If (KeysDataTable.Count > 1) Then KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave
            Dim KeyName3 As String = ""
            If (KeysDataTable.Count > 2) Then KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave

            ' Configuración
            If ExportacionDataSet.TBL_Config.Select("id_Entidad = " & Program.ImagingGlobal.Entidad & "and  id_Proyecto = " & Program.ImagingGlobal.Proyecto).Length = 0 Then
                Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(Program.ImagingGlobal.Entidad)
                ExportacionDataSet.TBL_Config.AddTBL_ConfigRow(Program.ImagingGlobal.Entidad, EntidadDataTable(0).Nombre_Entidad, Program.ImagingGlobal.Proyecto, Program.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto, KeyName1, KeyName2, KeyName3)
            End If

            ' Folder
            For Each FolderRow In FolderDataTable
                ExportacionDataSet.TBL_Folder.AddTBL_FolderRow(FolderRow.fk_Expediente, FolderRow.fk_Folder, FolderRow.id_Esquema, FolderRow.Nombre_Esquema, FolderRow.Key_1, FolderRow.Key_2, FolderRow.Key_3, FolderRow.CBarras_Folder)
            Next

            ' File
            For Each FileRow In FileDataTable
                ExportacionDataSet.TBL_File.AddTBL_FileRow(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, FileRow.File_Unique_Identifier, FileRow.Nombre_Documento, FileRow.Nombre_Imagen_File, FileRow.Folios_Documento_File, FileRow.Tamaño_Imagen_File)
            Next

            ' Data
            For Each FileDataRow In FileDataDataTable
                ExportacionDataSet.TBL_File_Data.AddTBL_File_DataRow(FileDataRow.fk_Expediente, FileDataRow.fk_Folder, FileDataRow.fk_File, FileDataRow.id_Version, FileDataRow.id_Campo, FileDataRow.Nombre_Campo, FileDataRow.Es_Campo_Busqueda, FileDataRow.fk_Campo_Tipo, FileDataRow.fk_Campo_Busqueda, FileDataRow.Valor_File_Data, FileDataRow.fk_Documento)
            Next

            ' Validaciones
            For Each FileValidacionRow In FileValidacionDataTable
                ExportacionDataSet.TBL_File_Validacion.AddTBL_File_ValidacionRow(FileValidacionRow.fk_Expediente, FileValidacionRow.fk_Folder, FileValidacionRow.fk_File, FileValidacionRow.id_Version, FileValidacionRow.id_Validacion, FileValidacionRow.Pregunta_Validacion, FileValidacionRow.Respuesta, FileValidacionRow.fk_Documento)
            Next

            ' Busqueda
            Dim CampoBusquedaDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Campo_Busqueda.DBExecute(idOT)
            For Each CampoBusquedaRow In CampoBusquedaDataTable
                If ExportacionDataSet.TBL_Campo_Busqueda.Select("fk_Campo_Tipo = " & CampoBusquedaRow.fk_Campo_Tipo & "and  id_Campo_Busqueda = " & CampoBusquedaRow.id_Campo_Busqueda).Length = 0 Then
                    ExportacionDataSet.TBL_Campo_Busqueda.AddTBL_Campo_BusquedaRow(CampoBusquedaRow.fk_Campo_Tipo, CampoBusquedaRow.id_Campo_Busqueda, CampoBusquedaRow.Nombre_Campo_Busqueda)
                End If
            Next



            If Generar Then
                If Not File.Exists(OutputFolder & "\" & "ExportedData.xml") Then
                    ExportacionDataSet.WriteXml(OutputFolder & "\" & "ExportedData.xml")
                End If

            End If

        End Sub

        Private Sub MostrarDatagrid()
            ExpedientesDataGridView.Visible = False
            OTDataGridView.Visible = False
        End Sub

        Private Sub Load_FormatoCargue()
            If (Not Usa_Exportacion_PDF) Then
                formatoAux = formato
                CompresionAux = compresion
            Else
                formatoAux = ImageManager.EnumFormat.Pdf
                CompresionAux = Utilities.GetEnumCompression(CType(formatoAux, DesktopConfig.FormatoImagenEnum))
            End If

        End Sub
#End Region

#Region " Funciones "
        Private Function Validar() As Boolean

            If Not validarFechaProceso() Then
                Return False
            End If

            If (Not Directory.Exists(CarpetaSalidaTextBox.Text)) Then
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

            ElseIf (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 Or Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0) Then
                MessageBox.Show("La carpeta debe estar vacia para exportar los datos", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Return True
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Me._plugin.GetCode, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try

            Else
                Return True

            End If
            Return False
        End Function

        Private Function validarFechaProceso() As Boolean
            FechaInicial = New DateTime(FechaProcesoDateTimePicker.Value.Year, FechaProcesoDateTimePicker.Value.Month, FechaProcesoDateTimePicker.Value.Day)
            FechaFinal = New DateTime(FechaProcesoFinalDateTimePicker.Value.Year, FechaProcesoFinalDateTimePicker.Value.Month, FechaProcesoFinalDateTimePicker.Value.Day)

            If (FechaInicial > FechaFinal) Then
                MessageBox.Show("La fecha de Proceso final no puede ser inferior a la fecha de Proceso inicial", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Return False

            End If

            Return True

        End Function

        Private Function getFechaInicio() As Integer
            Return CInt(Me.FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"))
        End Function

        Private Function getFechaFinal() As Integer
            Return CInt(Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd"))
        End Function

        Private Function getOTs() As DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try

                dbmImaging = New DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Return dbmImaging.SchemaProcess.PA_Exportacion_OT.DBExecute(Me._plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                                                                                Me._plugin.Manager.ImagingGlobal.Entidad, _
                                                                                Me._plugin.Manager.ImagingGlobal.Proyecto, _
                                                                                getFechaInicio(), _
                                                                                getFechaFinal())
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable()
        End Function

        Private Function Nombre_Imagen_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nFk_Documento As Integer, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String
            Dim Nombre_Imagen As String = String.Empty

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Nombre_Imagen = dbmImaging.SchemaProcess.PA_GetNombreArchivo.DBExecute(nidExpediente, nidFolder, nidFile, nFk_Documento, nGrupo)

                If Nombre_Imagen = String.Empty Then
                    nValido = False
                    nMsgError = "No se encontró nombre de imagen para el expediente: " & nidExpediente.ToString() & ", fk_Documento: " & nFk_Documento.ToString()
                End If
            Catch ex As Exception
                nValido = False
                Throw New Exception("Error al generar la Imagen del Expediente: (" + nidExpediente.ToString + ") Se genero el error:" + ex.Message, ex.InnerException)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try


            Return Nombre_Imagen
        End Function
#End Region


    End Class

    Public Class ImageManagerDomain
        Inherits MarshalByRefObject

        Public Sub Save(ByVal nInputFileNames As List(Of String), ByVal nOutputFileName As String, ByVal nSuffixFormat As String, ByVal nFormat As ImageManager.EnumFormat, ByVal nCompression As ImageManager.EnumCompression, ByVal nSinglePage As Boolean, ByVal nTempPath As String, ByVal nIsInputSingle As Boolean)
            Slyg.Tools.Imaging.ImageManager.Save(nInputFileNames, nOutputFileName, nSuffixFormat, nFormat, nCompression, nSinglePage, nTempPath, nIsInputSingle)
        End Sub
    End Class
End Namespace
