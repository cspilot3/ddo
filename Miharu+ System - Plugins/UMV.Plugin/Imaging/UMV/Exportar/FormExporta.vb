Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Desktop.Library.Config
Imports Miharu.Desktop.Library
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports Miharu.Imaging.Library.Eventos
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl
Imports System.Xml.Linq
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Configuration
Imports Miharu.Imaging.Library
Imports Miharu.Imaging
Imports DBImaging
Imports UMV.Plugin.Imaging.UMV
Imports System.Globalization
Imports System.Dynamic
Imports System.Threading
Imports System.Text
Imports DBImaging.SchemaProcess
Imports System.Drawing
Imports Ionic.Zip
Imports System.Drawing.Imaging
Imports GdPicture12
Imports Ionic.Zlib
Imports Slyg.Tools.Zip


Namespace Imaging.Exportar


    Public Class FormExporta
        Inherits FormBase


#Region " Declaraciones "
        Public _plugin As UMVPlugin
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
        Dim directorio As String
        Dim OutputFolder As String
        Dim Nombre_Imagen_File As String
        Dim TableCopia As DataTable
        Dim lineaActualFilesNM As String
        Dim lineaActualFilesHL As String
        Dim lineaActualFilesPF As String
        Dim Form As String = String.Empty
        Dim FormNM As String = String.Empty
        Dim FormPF As String = String.Empty

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
            MostrarDatagrid()
            ' Load_FormatoCargue()

        End Sub

        Private Sub BuscarCarpetaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarCarpetaButton.Click
            Dim Selector As New FolderBrowserDialog()

            Selector.SelectedPath = CarpetaSalidaTextBox.Text
            If (Selector.ShowDialog() = DialogResult.OK) Then
                Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
            End If
        End Sub

        Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click

            ExportarFolios()

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
            'Me.OTDataGridView.AutoGenerateColumns = False
            ' Me.OTDataGridView.DataSource = getOTs()
            'Me.OTDataGridView.Refresh()

            'If (Me.OTDataGridView.RowCount = 0) Then
            'MessageBox.Show("No se encontraron OTs para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If
        End Sub

        Private Sub CargarExpedientes()
            Me.ExpedientesDataGridView.AutoGenerateColumns = False
            Me.ExpedientesDataGridView.DataSource = ObtenerEstiba()
            Me.ExpedientesDataGridView.Refresh()

            If (Me.ExpedientesDataGridView.RowCount = 0) Then
                MessageBox.Show("No se encontraron Estibas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub


        Private Sub ExportarFolios()
            If (Validar()) Then

                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                ArrayNotificacionTapas = New ArrayList
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Me._plugin.Manager.Sesion.Usuario.id)
                FileNamesCons = New List(Of String)
                'Poner Id_Estiba
                'Dim IdEstiba = CType(CType(Me.ExpedientesDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Exportacion_OTRow)
                Dim ID_Estiba = ExpedientesDataGridView.SelectedRows.Item(0).Cells(0).Value.ToString()
                Dim ExportDataFilePDFA = getOT(UMVPlugin.Imaging_EntidadId, UMVPlugin.Imaging_UMV_ProyectoId, ID_Estiba)
                Dim ExportDataFilePDFACSV = getOTCSV(UMVPlugin.Imaging_EntidadId, UMVPlugin.Imaging_UMV_ProyectoId, ID_Estiba)
                Dim dtItemFile As DataTable
                ' Dim ExportDataFilePDFA = dbmImaging.SchemaProcess.PA_Exportacion_Data_File.DBExecute(UMVPlugin.Imaging_EntidadId, UMVPlugin.Imaging_UMV_ProyectoId, Convert.ToInt32(ID_Estiba))
                'Dim ExportDataFilePDFACSV = dbmImaging.SchemaProcess.PA_Exportacion_Data_File_CSV.DBExecute(UMVPlugin.Imaging_EntidadId, UMVPlugin.Imaging_UMV_ProyectoId)

                Dim Proceso() As Process

                Proceso = Process.GetProcessesByName("Miharu.Desktop.vshost.exe")
                For Each pro In Proceso
                    pro.PriorityClass = ProcessPriorityClass.BelowNormal
                Next

#If Not Debug Then
                                ProgressForm.Show()
#End If

                OutputFolder = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                directorio = CarpetaSalidaTextBox.Text.TrimEnd("\"c)
                Dim Progreso As Integer = 0
                ProgressForm.SetProceso("Exportar")
                ProgressForm.SetAccion("Obteniendo imágenes...")
                ProgressForm.SetProgreso(0)
                ProgressForm.SetMaxValue(ExportDataFilePDFA.Rows.Count())
                Application.DoEvents()
                contadorFile = ExpedientesDataGridView.Rows.Item(0).Cells(0).Value.ToString()

                Dim Exp As List(Of Object) = Nothing
                Exp = (From a In ExportDataFilePDFA Group a By GroupDt = a.Field(Of String)("fk_Llave") Into Group Select Group.Select(Function(x) x("fk_Llave")).First()).ToList()
                Dim FolderImg As String
                FolderImg = (OutputFolder & contadorFile.ToString("0000")).TrimEnd("\"c) & "\"



                If (Not Directory.Exists(FolderImg)) Then
                    Directory.CreateDirectory(FolderImg)
                End If
                Dim Compresion As ImageManager.EnumCompression

                If (Me._plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                    Compresion = ImageManager.EnumCompression.Ccitt4
                Else
                    Compresion = ImageManager.EnumCompression.Lzw
                End If

                Dim manager As FileProviderManager = Nothing

                If (Exp.Count > 0) Then

                    For Each ItemFile In Exp
                        contador = 0
                        Try

                            Dim itemOT = From o In ExportDataFilePDFA.Rows.Item(0).ItemArray(7).ToString()
                            'idOT = itemOT.FirstOrDefault()

                            Dim servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Convert.ToInt32(itemOT.FirstOrDefault()))(0).ToCTA_ServidorSimpleType()
                            Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me._plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad,
                                                                                                                                             Me._plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede,
                                                                                                                                             Me._plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                            manager = New FileProviderManager(servidor, centro, dbmImaging, Me._plugin.Manager.Sesion.Usuario.id)
                            manager.Connect()



                            Dim Cedula = ItemFile.ToString()

                            ' Crear el directorio de las imágenes
                            Dim FileFolderName = FolderImg & "imagenes" & "\" & Cedula & "\"

                            If (Not Directory.Exists(FileFolderName)) Then
                                Directory.CreateDirectory(FileFolderName)
                            End If

                            Dim ArraListParametersTapas As ArrayList = New ArrayList

                            dtItemFile = ExportDataFilePDFA.Select("fk_Llave =" + ItemFile.ToString()).CopyToDataTable

                            ArraListParametersTapas.Add(servidor)
                            ArraListParametersTapas.Add(centro)
                            ArraListParametersTapas.Add(manager)
                            ArraListParametersTapas.Add(Compresion)
                            ArraListParametersTapas.Add(dtItemFile)
                            ArraListParametersTapas.Add(FileFolderName)
                            ArraListParametersTapas.Add(ExportDataFilePDFA)
                            ArraListParametersTapas.Add(ArrayNotificacionTapas.Count)


                            Progreso += Progreso
                            ProgressForm.SetProgreso(Progreso)
                            Application.DoEvents()

                            SyncLock BloqueoConcurrenciaTapas
                                ArrayNotificacionTapas.Add(False)
                            End SyncLock

                            'Dim workerThread As New Thread(AddressOf ProcesoHilos)
                            'workerThread.Start(ArraListParametersTapas)
                            If (ExportDataFilePDFA IsNot Nothing) Then ExportDataFilePDFA.Dispose()
                            If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                            ClearMemory()
                        Catch ex As Exception
                            If (manager IsNot Nothing) Then manager.Disconnect()
                            Throw (ex)
                        End Try

                    Next
                    Dim SalirTapas As Boolean = False
                    While SalirTapas = False
                        SalirTapas = True
                        SyncLock BloqueoConcurrenciaTapas
                            For Each HiloTerminado As Boolean In ArrayNotificacionTapas
                                If HiloTerminado = False Then
                                    SalirTapas = False
                                End If
                            Next
                        End SyncLock
                        Thread.Sleep(10000)
                    End While

                    Me.Enabled = True

                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()


                    '******-------Generación de Archivos CSV*******---------

                    Try

                        Dim utf8WithoutBom As New System.Text.UTF8Encoding(False)
                        Dim lineasCSV As New System.Text.StringBuilder
                        Dim lineaActual As String = String.Empty
                        Dim lineasCSV1 As New System.Text.StringBuilder
                        Dim lineaActual1 As String = String.Empty
                        Dim lineasCSV2 As New System.Text.StringBuilder
                        Dim lineaActual2 As String = String.Empty
                        Dim ficheroCSV As String = String.Empty
                        Dim i As Int32 = 0



                        For Each itemsHL In ExportDataFilePDFACSV.Rows
                            If DirectCast(itemsHL, System.Data.DataRow).ItemArray(0) = "HL" Then
                                FormatoCSV = "HL"
                                lineaActual &= (String.Format("{0};", DirectCast(itemsHL, System.Data.DataRow).ItemArray(2)))
                                'Quitar coma final
                            End If

                        Next  'Guardar datos variable temporal a fichero CSV
                        lineasCSV.AppendLine(lineaActual)
                        Dim Sys As New System.IO.StreamWriter(File.Open(FolderImg & FormatoCSV & "_" & contadorFile & "_exp.csv", FileMode.Create), utf8WithoutBom)
                        Sys.WriteLine(lineasCSV.ToString)
                        Sys.Flush()
                        Sys.Dispose()

                        For Each itemsNM In ExportDataFilePDFACSV.Rows
                            If DirectCast(itemsNM, System.Data.DataRow).ItemArray(0) = "NM" Then
                                FormatoCSV = "NM"
                                lineaActual1 &= (String.Format("{0};", DirectCast(itemsNM, System.Data.DataRow).ItemArray(2)))
                            End If

                        Next
                        'Quitar coma final
                        lineasCSV.AppendLine(lineaActual1)
                        'Guardar datos variable temporal a fichero CSV

                        Dim Sys1 As New System.IO.StreamWriter(File.Open(FolderImg & FormatoCSV & "_" & contadorFile & "_exp.csv", FileMode.Create), utf8WithoutBom)
                        Sys1.WriteLine(lineasCSV.ToString)
                        Sys1.Flush()
                        Sys1.Dispose()

                        For Each itemsPF In ExportDataFilePDFACSV.Rows
                            If DirectCast(itemsPF, System.Data.DataRow).ItemArray(0) = "PF" Then
                                FormatoCSV = "PF"
                                lineaActual2 &= (String.Format("{0};", DirectCast(itemsPF, System.Data.DataRow).ItemArray(2)))
                            End If

                        Next
                        'Quitar coma final
                        lineasCSV.AppendLine(lineaActual2)
                        'Guardar datos variable temporal a fichero CSV

                        Dim Sys2 As New System.IO.StreamWriter(File.Open(FolderImg & FormatoCSV & "_" & contadorFile & "_exp.csv", FileMode.Create), utf8WithoutBom)
                        Sys2.WriteLine(lineasCSV.ToString)
                        Sys2.Flush()
                        Sys2.Dispose()
                    Catch ex As Exception
                        If (manager IsNot Nothing) Then manager.Disconnect()
                        Throw (ex)
                    End Try



                    '*******-----------FILES CSV-------*************
                    Try

                        Dim utf8WithoutBom As New System.Text.UTF8Encoding(False)
                        Dim lineasCSV As New System.Text.StringBuilder
                        Dim lineaActual As String = String.Empty
                        Dim lineasCSV1 As New System.Text.StringBuilder
                        Dim lineaActual1 As String = String.Empty
                        Dim lineasCSV2 As New System.Text.StringBuilder
                        Dim lineaActual2 As String = String.Empty
                        Dim ficheroCSV As String = String.Empty
                        Dim i As Int32 = 0


                        If Form = "" Then
                        Else

                            'lineaActualFilesHL &= (String.Format("{0};", FormNM.ToList().Item(34).ItemArray(34)))
                            'Quitar coma final
                            lineasCSV.AppendLine(lineaActualFilesHL)
                            'Guardar datos variable temporal a fichero CSV
                            Dim Sys As New System.IO.StreamWriter(File.Open(FolderImg & "FILES_" & FormatoCSV & "_" & contadorFile & ".csv", FileMode.Create), utf8WithoutBom)
                            Sys.WriteLine(lineasCSV.ToString)
                            Sys.Flush()
                            Sys.Dispose()
                        End If

                        If FormNM = "" Then
                        Else
                            'lineaActualFilesNM &= (String.Format("{0};", FormNM.ToList().Item(34).ItemArray(34)))
                            'For index = 1 To FormNM.ToList().Count()
                            '    lineaActual1.Replace("Nombre_Imagen_File", FormNM.ToList().Item(22).ItemArray(i))
                            'Next

                            'Quitar coma final
                            lineasCSV1.AppendLine(lineaActualFilesNM)
                            'Guardar datos variable temporal a fichero CSV
                            Dim Sys1 As New System.IO.StreamWriter(File.Open(FolderImg & "FILES_" & contadorFile & ".csv", FileMode.Create), utf8WithoutBom)
                            Sys1.WriteLine(lineasCSV1.ToString)
                            Sys1.Flush()
                            Sys1.Dispose()

                        End If

                        If FormPF = "" Then
                        Else

                            'Quitar coma final
                            lineasCSV2.AppendLine(lineaActualFilesPF)
                            'Guardar datos variable temporal a fichero CSV
                            Dim Sys2 As New System.IO.StreamWriter(File.Open(FolderImg & "FILES_" & FormatoCSV & "_" & contadorFile & ".csv", FileMode.Create), utf8WithoutBom)
                            Sys2.WriteLine(lineasCSV2.ToString)
                            Sys2.Flush()
                            Sys2.Dispose()
                        End If

                    Catch ex As Exception
                        MessageBox.Show("Error al generar los CSV", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    If (manager IsNot Nothing) Then manager.Disconnect()
                    'Obtener los Files a transferir 

                    Dim FileNames(0) As String
                    Dim carpetas(0) As String

                    If directorio = "" Then
                        FileNames(0) = "C:"
                    Else
                        FileNames = Directory.GetDirectories(directorio)
                    End If

                    'Crear la carpeta en .ZIP
                    If FileNames.Count > 0 And carpetas.Count > 0 Then
                        Dim ZipFileName As String = directorio & ID_Estiba & ".zip"

                        Using zip As ZipFile = New ZipFile()
                            zip.AddDirectory(FileNames.ToArray(0).ToString())
                            zip.Save(ZipFileName)
                        End Using

                    End If
                Else
                    MessageBox.Show("Operación cancelada por Usuario", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                MessageBox.Show("El archivo .zip se exportó con éxito", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                BorrarTemporal()
                ProgressForm.Close()
            End If
            Return
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

                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)

                manager = New FileProviderManager(nservidor, ncentro, dbmImaging, _plugin.Manager.Sesion.Usuario.id)
                manager.Connect()
                SyncLock BloqueoConcurrenciaTapas
                    For Each itemFileRow In nItemFile.Rows

                        ExportarImagen(manager, itemFileRow, nCompresion, npathFileName, npathFileName, ExportDataFilePDFA, nPathTemp)
                        Dim nombre_linea As String
                        Dim Nombre_Imagen_File = DirectCast(itemFileRow, System.Data.DataRow).ItemArray(22)
                        nombre_linea = DirectCast(itemFileRow, System.Data.DataRow).ItemArray(34)
                        nombre_linea = nombre_linea.Replace("/Nombre_Imagen_File", "/" + Nombre_Imagen_File)
                        Dim file As FileInfo
                        file = New FileInfo(Nombre_Imagen_File)
                        Dim f = file.Length
                        nombre_linea = nombre_linea.Replace(",0,", "," & f & ",")
                        If DirectCast(itemFileRow, System.Data.DataRow).ItemArray(2) = "HL" Then
                            Form = DirectCast(itemFileRow, System.Data.DataRow).ItemArray(2)
                            lineaActualFilesHL &= (String.Format("{0};", nombre_linea))
                        ElseIf DirectCast(itemFileRow, System.Data.DataRow).ItemArray(2) = "NM" Then
                            FormNM = DirectCast(itemFileRow, System.Data.DataRow).ItemArray(2)
                            lineaActualFilesHL &= (String.Format("{0};", nombre_linea))
                        ElseIf DirectCast(itemFileRow, System.Data.DataRow).ItemArray(2) = "PF" Then
                            FormPF = DirectCast(itemFileRow, System.Data.DataRow).ItemArray(2)
                            lineaActualFilesHL &= (String.Format("{0};", nombre_linea))
                        End If
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


        Private Sub ExportAllFillesInTiff(nCompresion As ImageManager.EnumCompression, ByVal nFileName As String)
            Try

                If FileNamesCons.Count > 0 Then

                    Dim FileName As String = nFileName & DateTime.Now.ToString("yyyyMMdd") & ".tiff"

                    Try
                        If File.Exists(FileName) Then
                            File.Delete(FileName)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error eliminando la imagen en la ruta: " & FileName & " - Error: " & ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                    ImageManager.Save(FileNamesCons, FileName, "", ImageManager.EnumFormat.Tiff, ImageManager.EnumCompression.Lzw, False, Program.AppPath & Program.TempPath, True)

                End If

            Catch ex As Exception
                MessageBox.Show("Se ha presentado un error al exportar la imagen TIFF: " & ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Sub

        Private Sub ExportarExpedientes()
            If (Validar()) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim TotalesDataTable As DBImaging.SchemaProcess.CTA_Exportacion_TotalesDataTable
                Dim ProgressForm As New FormProgress
                FileNamesCons = New List(Of String)

                If LlenaExpedientesSeleccion() Then

                    Dim Folders As Integer = 0
                    Dim Files As Integer = 0
                    Dim Folios As Integer = 0
                    Dim Tamaño As Double = 0
                    'Por cada OT se crea una carpeta

                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    For Each row As DataRow In ViewExpedientes.ToTable.Rows
                        TotalesDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Expediente_Totales.DBExecute(CInt(row(0)))
                        If TotalesDataTable.Rows.Count > 0 Then
                            Folders += CInt(TotalesDataTable.Rows(0)("Folders"))
                            Files += CInt(TotalesDataTable.Rows(0)("Files"))
                            Folios += CInt(TotalesDataTable.Rows(0)("Folios"))
                            Tamaño += CDbl(TotalesDataTable.Rows(0)("Tamaño"))
                        End If
                    Next

                    dbmImaging.Connection_Close()

                    Dim Respuesta As DialogResult

                    Respuesta = MessageBox.Show("Se encontró : " & vbCrLf & _
                                                Folders & " Unidades Documentales, " & vbCrLf & _
                                                Files & " Documentos, " & vbCrLf & _
                                                Folios & " Folios con " & vbCrLf & _
                                                (Tamaño / 1024 / 1024).ToString("#,##0.00") & "MB de tamaño, " & vbCrLf & _
                                                "¿Desea Exportar esta información?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If Respuesta = DialogResult.Yes Then
                        Try
                            Me.Enabled = False

                            'Se crea Tabla de OTs
                            Dim OTs As New DataTable
                            OTs = ViewExpedientes.ToTable(True, "fk_OT")

                            Dim ExportacionDataSetXML As New OffLineViewer.Library.xsdOffLineData
                            Dim FolderDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                            Dim FileDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
                            Dim FileDataDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                            Dim FileValidacionDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable

                            If OTs.Rows.Count > 0 Then
                                Dim Progreso As Integer = 0
                                ProgressForm.SetProceso("Exportar")
                                ProgressForm.SetAccion("Obteniendo imágenes...")
                                ProgressForm.SetProgreso(0)
                                ProgressForm.SetMaxValue(Files)
                                Application.DoEvents()

                                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                                dbmCore.Connection_Open(1)
                                dbmImaging.Connection_Open(1)

#If Not Debug Then
                                ProgressForm.Show()
#End If

                                For Each rowOt As DataRow In OTs.Rows

                                    Dim ExpedientesOT As New DataTable
                                    Dim ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(CInt(rowOt(0)))
                                    Dim FolderDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                                    Dim FileDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
                                    Dim FileDataDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                                    Dim FileValidacionDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable

                                    'Filtar expedientes por OT
                                    ViewExpedientes.RowFilter() = "fk_OT = " & rowOt(0).ToString
                                    ExpedientesOT = ViewExpedientes.ToTable()

                                    For Each rowExpediente As DataRow In ViewExpedientes.ToTable.Rows
                                        Dim tmpFolderDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Folders_Expediente.DBExecute(CInt(rowExpediente(0)))
                                        Dim tmpFileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files_Expediente.DBExecute(CInt(rowExpediente(0)))
                                        Dim tmpFileDataDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Data_Expediente.DBExecute(CInt(rowExpediente(0)))
                                        Dim tmpFileValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validaciones_Expediente.DBExecute(CInt(rowExpediente(0)))

                                        TablaDesdeTemporal(FolderDataTable, tmpFolderDataTable)
                                        TablaDesdeTemporal(FileDataTable, tmpFileDataTable)
                                        TablaDesdeTemporal(FileDataDataTable, tmpFileDataDataTable)
                                        TablaDesdeTemporal(FileValidacionDataTable, tmpFileValidacionDataTable)

                                        'If Me.TXTRadioButton.Checked Then
                                        '    TablaDesdeTemporal(FolderDataTableTXT, tmpFolderDataTable)
                                        '    TablaDesdeTemporal(FileDataTableTXT, tmpFileDataTable)
                                        '    TablaDesdeTemporal(FileDataDataTableTXT, tmpFileDataDataTable)
                                        '    TablaDesdeTemporal(FileValidacionDataTableTXT, tmpFileValidacionDataTable)
                                        'End If

                                    Next

                                    Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                                    Dim FilesDataViewExpedientes As New DataView(FileDataTable)

                                    ' Crear el directorio de las imágenes
                                    If Not Directory.Exists(OutputFolder & "images") Then
                                        Directory.CreateDirectory(OutputFolder & "images")
                                    End If

                                    Dim Compresion As ImageManager.EnumCompression

                                    If (Program.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                        Compresion = ImageManager.EnumCompression.Ccitt4
                                    Else
                                        Compresion = ImageManager.EnumCompression.Lzw
                                    End If

                                    For Each RowServidor In ServidoresDataTable
                                        Dim manager As FileProviderManager = Nothing
                                    Next

                                    For Each RowServidor In ServidoresDataTable
                                        Dim manager As FileProviderManager = Nothing

                                        Try
                                            Dim servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(CInt(rowOt(0)))(0).ToCTA_ServidorSimpleType()
                                            Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                            manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                                            manager.Connect()

                                            Dim FileFolderName = "images\" & CInt(rowOt(0)).ToString("0000000000") & "\"
                                            FolderNameOutput = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\" & FileFolderName

                                            If (Not Directory.Exists(OutputFolder & FileFolderName)) Then
                                                Directory.CreateDirectory(OutputFolder & FileFolderName)
                                            End If

                                            Dim GruposD As List(Of Object) = Nothing
                                            GruposD = (From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("fk_Grupo") Into Group Select Group.Select(Function(x) x("fk_Grupo")).First()).ToList()

                                            For Each grupo As Integer In GruposD
                                                Dim FilesbyGroup = FileDataTable.Select("fk_Grupo = " + grupo.ToString()).CopyToDataTable

                                                Dim ExpedientesbyGroup = FileDataTable.Select("fk_Grupo = " + grupo.ToString()).CopyToDataTable

                                                If grupo = 0 Then
                                                    Dim FilesbyGroupDataView As New DataView(FilesbyGroup)
                                                    ' Obtener los Files a transferir   
                                                    FilesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor
                                                    Dim Usa_Renombramiento_Imagen_File As Boolean = CBool(System.Configuration.ConfigurationManager.AppSettings.Get("Usa_Renombramiento_Imagen_File"))
                                                    Dim Usa_Renombramiento_Imagen_Expediente As Boolean = CBool(System.Configuration.ConfigurationManager.AppSettings.Get("Usa_Renombramiento_Imagen_Expediente"))
                                                    Dim ExportDataFilePDFA = dbmImaging.SchemaProcess.PA_Exportacion_Data_File.DBExecute(UMVPlugin.Imaging_EntidadId, UMVPlugin.Imaging_UMV_ProyectoId, 1)
                                                    If Usa_Renombramiento_Imagen_File Then

                                                        For Each ItemFile As DataRowView In FilesbyGroupDataView
                                                            If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")
                                                            Dim nPathTemp = String.Empty
                                                            ExportarImagen(manager, ItemFile.Row, Compresion, OutputFolder & FileFolderName, FileFolderName, ExportDataFilePDFA, nPathTemp)

                                                            For Each Item As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow In FileDataTable
                                                                If Item.fk_Expediente = CLng(ItemFile.Item("fk_Expediente")) And Item.fk_Folder = CShort(ItemFile.Item("fk_Folder")) And Item.fk_File = CShort(ItemFile.Item("fk_File")) Then
                                                                    Item.Nombre_Imagen_File = CStr(ItemFile.Item("Nombre_Imagen_File"))
                                                                End If
                                                            Next

                                                            Progreso += 1
                                                            ProgressForm.SetProgreso(Progreso)
                                                            Application.DoEvents()
                                                        Next

                                                    End If
                                                    If Usa_Renombramiento_Imagen_Expediente Then

                                                        Dim Expedientes As List(Of Object) = Nothing
                                                        Expedientes = (From a In FilesbyGroup Group a By groupDt = a.Field(Of Long)("fk_Expediente") Into Group Select Group.Select(Function(x) x("fk_Expediente")).First()).ToList()

                                                        For Each Expediente As Long In Expedientes
                                                            Dim FilesExpedientesbyGroup = FilesbyGroup.Select("fk_Expediente = " + Expediente.ToString()).CopyToDataTable
                                                            Dim FilesExpedientesbyGroupDataView As New DataView(FilesExpedientesbyGroup)

                                                            ' Obtener los Files a transferir   
                                                            FilesExpedientesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor

                                                            If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                                            ' Enviar el archivo
                                                            ExportarImagenExpediente(manager, FilesExpedientesbyGroupDataView, Compresion, OutputFolder & FileFolderName, FileFolderName)

                                                            For Each ItemFile As DataRowView In FilesbyGroupDataView
                                                                For Each Item As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow In FileDataTable
                                                                    If Item.fk_Expediente = CLng(ItemFile.Item("fk_Expediente")) And Item.fk_Folder = CShort(ItemFile.Item("fk_Folder")) And Item.fk_File = CShort(ItemFile.Item("fk_File")) Then
                                                                        Item.Nombre_Imagen_File = CStr(ItemFile.Item("Nombre_Imagen_File"))
                                                                    End If
                                                                Next
                                                                Progreso += 1
                                                                ProgressForm.SetProgreso(Progreso)
                                                                Application.DoEvents()
                                                            Next
                                                        Next
                                                    End If
                                                Else
                                                    Dim Expedientes As List(Of Object) = Nothing
                                                    Expedientes = (From a In FilesbyGroup Group a By groupDt = a.Field(Of Long)("fk_Expediente") Into Group Select Group.Select(Function(x) x("fk_Expediente")).First()).ToList()

                                                    For Each Expediente As Long In Expedientes
                                                        Dim FilesExpedientesbyGroup = FilesbyGroup.Select("fk_Grupo = " + grupo.ToString() + "AND fk_Expediente = " + Expediente.ToString()).CopyToDataTable
                                                        Dim FilesExpedientesbyGroupDataView As New DataView(FilesExpedientesbyGroup)

                                                        ' Obtener los Files a transferir   
                                                        FilesExpedientesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor

                                                        If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                                        ' Enviar el archivo
                                                        ExportarImagenAgrupada(manager, FilesExpedientesbyGroupDataView, grupo, Expediente, 1, Compresion, OutputFolder & FileFolderName, FileFolderName)

                                                        For Each Item As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow In FileDataTable
                                                            If Item.fk_Expediente = Expediente And Item.fk_Grupo = grupo Then
                                                                Item.Nombre_Imagen_File = CStr(FilesExpedientesbyGroupDataView(0).Item("Nombre_Imagen_File"))
                                                            End If
                                                        Next

                                                        Progreso += 1
                                                        ProgressForm.SetProgreso(Progreso)
                                                        Application.DoEvents()
                                                    Next

                                                End If
                                            Next
                                            manager.Disconnect()
                                        Catch ex As Exception
                                            If (manager IsNot Nothing) Then manager.Disconnect()
                                            Throw
                                        End Try
                                    Next

                                    '------------  Si proyecto tiene configurado Exportar_Unico_Archivo_TIFF  ------------------
                                    If (CBool(Program.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF)) Then
                                        ExportAllFillesInTiff(Compresion, FolderNameOutput)
                                    End If
                                    '--------------------------------

                                    'If (Me.VisorRadioButton.Checked) Then
                                    '    GenerarVisor(dbmCore, dbmImaging, CInt(rowOt(0)), OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable)
                                    'ElseIf (Me.XMLRadioButton.Checked) Then
                                    '    Dim generar As Boolean = False
                                    '    If rowOt.Equals(OTs.Rows(OTs.Rows.Count - 1)) Then
                                    '        generar = True
                                    '    End If
                                    '    GenerarXMLExpedientes(generar, dbmCore, dbmImaging, CInt(rowOt(0)), OutputFolder, FolderDataTable, FileDataTable, FileDataDataTable, FileValidacionDataTable, ExportacionDataSetXML)
                                    'ElseIf rowOt.Equals(OTs.Rows(OTs.Rows.Count - 1)) Then

                                    '    GenerarTXT(OutputFolder, FolderDataTableTXT, FileDataTableTXT, FileDataDataTableTXT, FileValidacionDataTableTXT)

                                    'End If

                                    'Aqui debe ir codigo que genera la ruta

                                Next
                                MessageBox.Show("La información se exportó con éxito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            Me.Enabled = True
                            'Finalizar conexiones
                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()

                            BorrarTemporal()

                            'Ocultar barras de progreso
                            ProgressForm.Close()
                        End Try
                    End If
                End If
            Else
                MessageBox.Show("No se seleccionaron registros para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            BorrarTemporal()

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

        Private Function LlenaExpedientesSeleccion() As Boolean

            'Se crea la tabla que contendrá los expedientes seleccionados en la grilla
            Dim ExpedientesSeleccion_ As New DataTable

            For Each col As DataGridViewColumn In ExpedientesDataGridView.Columns
                ExpedientesSeleccion_.Columns.Add(col.DataPropertyName)
            Next

            For Each row As DataGridViewRow In ExpedientesDataGridView.Rows
                If CBool(row.Cells("Exportar").Value) Then 'Expedientes seleccionados
                    Dim newRow As DataRow = ExpedientesSeleccion_.NewRow()

                    For Each col As DataGridViewColumn In ExpedientesDataGridView.Columns
                        newRow(col.Index) = row.Cells(col.Index).Value
                    Next

                    ExpedientesSeleccion_.Rows.Add(newRow)
                End If
            Next

            ExpedientesSeleccion = ExpedientesSeleccion_

            If ExpedientesSeleccion_.Rows.Count > 0 Then
                ViewExpedientes.RowFilter = String.Empty
                ViewExpedientes = New DataView(ExpedientesSeleccion_)
                Return True
            End If

            Return False

        End Function
        Private Function ObtenerEstiba()
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.PA_Exportacion_Data_FileStoreProcedure = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "SELECT CodigoEstiba, Exportada, Ruta FROM [DB_Miharu.Integration].[UMV].[TBL_Estiba] "
                dt = ExecuteQuery(sqlquery, conn)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function

        Private Function getOT(EntidadId As Int32, ProyectoId As Int32, Estiba As Int32) As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.PA_Exportacion_Data_FileStoreProcedure = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "DECLARE " +
"			 @id_Doc_Tapa_Lote INT " +
"			,@id_Doc_Tapa_Favidi INT " +
"			,@CampoSerie INT " +
"			,@Activos VARCHAR(5000) " +
"			,@FechaComparacionHLUMV DATETIME " +
"			,@fk_DocumentosTipoNovedadAporte VARCHAR(200) " +
"			,@Dependencia VARCHAR(10) " +
"SET @Dependencia = (SELECT	Valor_Parametro_Sistema " +
"							  FROM	[DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"	  						  WHERE	Nombre_Parametro_Sistema = '@CodigoDependencia' " +
"							  AND fk_Entidad = @fk_Entidad " +
"					AND fk_Proyecto = @fk_Proyecto) " +
"SET @FechaComparacionHLUMV = (SELECT	CONVERT(DATETIME, Valor_Parametro_Sistema, 111) " +
"							  FROM	[DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"	  						  WHERE	Nombre_Parametro_Sistema = '@FechaLimiteUMVTipoDoc' " +
"							  AND fk_Entidad = @fk_Entidad " +
"					AND fk_Proyecto = @fk_Proyecto) " +
"SET @Activos = (SELECT Valor_Parametro_Sistema " +
"					FROM [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"					WHERE Nombre_Parametro_Sistema = 'Activos' " +
"					AND fk_Entidad = @fk_Entidad " +
"					AND fk_Proyecto = @fk_Proyecto) " +
"SET @id_Doc_Tapa_Lote = (SELECT TOP 1 ISNULL(Valor_Parametro_Sistema,0) " +
"                         FROM [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"                         WHERE fk_Entidad = @fk_Entidad " +
"                         AND fk_Proyecto = @fk_Proyecto " +
"                         AND Nombre_Parametro_Sistema = '@TapaLote') " +
"SET @id_Doc_Tapa_Favidi = (SELECT TOP 1 ISNULL(Valor_Parametro_Sistema,0) " +
"                         FROM [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"                         WHERE fk_Entidad = @fk_Entidad " +
"                         AND fk_Proyecto = @fk_Proyecto " +
"                         AND Nombre_Parametro_Sistema = '@TapaFavidi') " +
"SET @CampoSerie = (SELECT TOP 1 ISNULL(Valor_Parametro_Sistema,0) " +
"						FROM [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"						WHERE fk_Entidad = @fk_Entidad " +
"						AND fk_Proyecto = @fk_Proyecto " +
"						AND Nombre_Parametro_Sistema = '@CampoSerie') " +
"SELECT DISTINCT	Estiba.CodigoEstiba " +
"			,EmpaqueData.Data_Campo AS TipoSerie " +
"			,PS.Valor_Parametro_Sistema as Sigla_Serie " +
"			,Contenedor.fk_Precinto " +
"			,Contenedor.fk_OT " +
"			,Contenedor.fk_Cargue " +
"			,Contenedor.fk_Paquete " +
"			,Precinto.Precinto fk_Llave " +
"			,Fil.fk_Expediente " +
"			,Fil.fk_Folder " +
"			,Fil.id_File " +
"			,(SELECT (SUM(FOLIOS_FILE)/2) AS NUMERO_FOLIOS FROM [DB_Miharu.Core].Process.TBL_File WHERE FK_EXPEDIENTE =  FIl.fk_Expediente) AS NUMERO_FOLIOS " +
"			,Documento.fk_Tipologia " +
"			,Fil.fk_Documento " +
"			,ImagingFile.id_Version " +
"			,CAST(ImagingFile.File_Unique_Identifier AS VARCHAR(200)) AS File_Unique_Identifier " +
"			,CASE EmpaqueData.Data_Campo " +
"			WHEN '190' THEN '' " +
"			ELSE 'TRD' END GrupoDocumento " +
"			,Folder.fk_Entidad_Servidor " +
"			,Folder.fk_Servidor " +
"			,Fil.Folios_File " +
"			,ImagingFile.Tamaño_Imagen_File			" +
"			,CAST([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 2) AS INT) AS FolioInicial " +
"			,'' AS Nombre_Imagen_File " +
"			,[DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 1) AS Fecha " +
"			,UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 5)) AS NombresFile " +
"			,[DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 6) AS TipoNovedad " +
"			,[DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 7) AS TipoAporte " +
"			,Estiba.Tipo_Archivo " +
"			,1 AS ImagenPrincipal " +
"			,UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 4)) AS Formato " +
"	INTO #Registros " +
"	FROM [DB_Miharu.Integration].[UMV].[TBL_Estiba] AS Estiba WITH(NOLOCK) " +
"	INNER JOIN	[DB_Miharu.Integration].[UMV].TBL_Estiba_Detalle AS EstibaDetalle WITH(NOLOCK) " +
"			ON	EstibaDetalle.fk_Estiba = Estiba.id_Estiba " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Empaque AS Empaque WITH(NOLOCK) " +
"			ON	EstibaDetalle.Caja = Empaque.Precinto " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Empaque_Contenedor AS Empaque_Contenedor WITH(NOLOCK) " +
"			ON	Empaque_Contenedor.fk_OT = Empaque.fk_OT " +
"			AND Empaque_Contenedor.fk_Empaque = Empaque.id_Empaque " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Empaque_Data AS EmpaqueData WITH(NOLOCK) " +
"			ON	EmpaqueData.fk_OT = Empaque.fk_OT " +
"			AND EmpaqueData.fk_Empaque = Empaque.id_Empaque " +
"			AND EmpaqueData.fk_Campo = @CampoSerie " +
"			AND EmpaqueData.Data_Campo ='190' " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Empaque_Data AS EmpaqueDataTipoArchivo WITH(NOLOCK) " +
"			ON	EmpaqueDataTipoArchivo.fk_OT = Empaque.fk_OT " +
"			AND EmpaqueDataTipoArchivo.fk_Empaque = Empaque.id_Empaque " +
"			AND EmpaqueDataTipoArchivo.fk_Campo = 3 " +
"			AND EmpaqueDataTipoArchivo.Data_Campo = Estiba.Tipo_Archivo " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema PS WITH(NOLOCK) " +
"			ON	PS.Nombre_Parametro_Sistema = EmpaqueData.Data_Campo " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Contenedor Contenedor WITH(NOLOCK) " +
"			ON	Empaque_Contenedor.Token = Contenedor.Token " +
"			AND Empaque_Contenedor.fk_OT = Contenedor.fk_OT " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Precinto Precinto WITH(NOLOCK) " +
"			ON	Precinto.fk_OT = Contenedor.fk_OT " +
"			AND	Precinto.id_Precinto = Contenedor.fk_Precinto " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_OT OT WITH(NOLOCK) " +
"			ON	OT.id_OT = Contenedor.fk_OT " +
"			AND OT.fk_OT_Tipo = 2 " +
"	INNER JOIN	[DB_Miharu.Core].Imaging.TBL_Folder Folder WITH(NOLOCK) " +
"			ON	Contenedor.fk_Cargue = Folder.fk_Cargue " +
"			AND Contenedor.fk_Paquete = Folder.fk_Cargue_Paquete " +
"	INNER JOIN	[DB_Miharu.Core].Process.TBL_File Fil WITH(NOLOCK) " +
"			ON	Fil.fk_Expediente = Folder.fk_Expediente " +
"			AND Fil.fk_Folder = Folder.fk_Folder " +
"			AND Fil.fk_Documento NOT IN (@id_Doc_Tapa_Lote, @id_Doc_Tapa_Favidi) " +
"	INNER JOIN	[DB_Miharu.Core].Config.TBL_Documento Documento WITH(NOLOCK) " +
"			ON	Documento.id_Documento = Fil.fk_Documento " +
"	INNER JOIN	[DB_Miharu.Core].Imaging.TBL_File ImagingFile WITH(NOLOCK) " +
"			ON	ImagingFile.fk_Expediente = Fil.fk_Expediente " +
"			AND ImagingFile.fk_Folder = Fil.fk_Folder " +
"			AND ImagingFile.fk_File = Fil.id_File " +
"	WHERE		Exportada = 0 " +
"			AND OT.fk_Entidad = @fk_Entidad " +
"			AND OT.fk_Proyecto = @fk_Proyecto " +
"			AND Estiba.CodigoEstiba = @fk_Estiba " +
"	ALTER TABLE #Registros ADD CodigoDocumento INT " +
"	ALTER TABLE #Registros ADD NombreDocumento VARCHAR(MAX) " +
"	ALTER TABLE #Registros ADD Novedades VARCHAR(500) " +
"	ALTER TABLE #Registros ADD ReportePrecinto VARCHAR(MAX) " +
"	ALTER TABLE #Registros ADD ReporteFiles VARCHAR(MAX) " +
"	ALTER TABLE #Registros ALTER COLUMN GrupoDocumento VARCHAR(10) " +
"	ALTER TABLE #Registros ADD FechaCambio VARCHAR(100) " +
"	ALTER TABLE #Registros ADD Formatos VARCHAR(500) " +
"	ALTER TABLE #Registros ADD TipoTrabajador VARCHAR(500) " +
"	ALTER TABLE #Registros ADD Estado VARCHAR(500) " +
"	ALTER TABLE #Registros ADD FechaInicial VARCHAR(10) " +
"	ALTER TABLE #Registros ADD FechaFinal VARCHAR(10) " +
"	ALTER TABLE #Registros ADD Nombres VARCHAR(500) " +
"	UPDATE #Registros " +
"	SET TipoTrabajador = PD.Campo_50 " +
"		,Estado = PD.Campo_49 " +
"		,Nombres = PD.Campo_48 " +
"	FROM #Registros R " +
"	INNER JOIN	[DB_MIharu.Imaging_Core].Process.TBL_Proceso_Data PD " +
"			ON	PD.fk_Expediente = R.fk_Expediente " +
"			AND PD.fk_Folder = R.fk_Folder " +
"	WHERE		TipoSerie = '190' " +
"	UPDATE #Registros " +
"	SET GrupoDocumento = 'TRD' " +
"	FROM #Registros R " +
"	INNER JOIN	[DB_MIharu.Imaging_Core].Process.TBL_Proceso_Data PD " +
"			ON	PD.fk_Expediente = R.fk_Expediente " +
"			AND PD.fk_Folder = R.fk_Folder " +
"	WHERE		TipoSerie = '190' " +
"			AND ISNULL(GrupoDocumento,'') = '' " +
"		AND PD.Campo_49 IN (SELECT Data FROM  [DB_MIharu.Imaging_Core].[dbo].[Split] (@Activos, ',')); " +
"	UPDATE #Registros " +
"	SET GrupoDocumento = Data.GrupoDocumento " +
"	FROM #Registros R " +
"	INNER JOIN (SELECT R.fk_OT, " +
"				R.fk_Precinto, " +
"				CASE " +
"				WHEN MAX([DB_Miharu.Integration].[dbo].[Fn_Formato_Fecha](CAST(FileData.Valor_File_Data AS VARCHAR(10)), 'yyyy/mm/dd')) <= @FechaComparacionHLUMV THEN 'DAFP' " +
"				ELSE 'TRD' " +
"				END AS GrupoDocumento " +
"				FROM #Registros R " +
"				INNER JOIN	[DB_Miharu.Core].Process.TBL_File_Data AS FileData WITH(NOLOCK) " +
"						ON	FileData.fk_Expediente = R.fk_Expediente " +
"						AND FileData.fk_Folder = R.fk_Folder				" +
"				INNER JOIN [DB_Miharu.Imaging_Core].Config.TBL_Matriz_Documento MD WITH(NOLOCK) " +
"						ON MD.fk_Documento = FileData.fk_Documento " +
"						AND MD.fk_Campo = FileData.fk_Campo " +
"						AND MD.id_Columna = 1 " +
"				WHERE	TipoSerie = '190' " +
"				AND ISNULL(GrupoDocumento,'') = '' " +
"				GROUP BY R.fk_OT, R.fk_Precinto) Data " +
"	ON Data.fk_OT = R.fk_OT " +
"	AND Data.fk_Precinto = R.fk_Precinto	" +
"	UPDATE #Registros " +
"            SET            CodigoDocumento = fk_Documento_UMV " +
"		,NombreDocumento = Nombre_Documento_UMV " +
"	FROM #Registros R " +
"	INNER JOIN [DB_Miharu.Integration].[Config].[TBL_Documento_Homologacion] DocHomo " +
"			ON R.fk_Documento = DocHomo.fk_Documento_Core " +
"			AND R.GrupoDocumento = DocHomo.Grupo_Documento " +
"	SET @fk_DocumentosTipoNovedadAporte = (SELECT Valor_Parametro_Sistema FROM [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema  WHERE Nombre_Parametro_Sistema = 'DocumentosNominaTipoNovedad' AND fk_Entidad = @fk_Entidad AND fk_Proyecto = @fk_Proyecto) " +
"	UPDATE #Registros " +
"           SET             Novedades = Data.Novedades " +
"	FROM #Registros Registros " +
"	INNER JOIN (SELECT R.fk_OT, R.fk_Precinto, STUFF( " +
"				(SELECT DISTINCT '/' + TipoNovedad " +
"				FROM #Registros R2 " +
"				WHERE ISNULL(R2.TipoNovedad,'') NOT IN ('SIN NOVEDAD', '') " +
"				AND R2.fk_OT = R.fk_OT " +
"				AND R2.fk_Precinto = R.fk_Precinto " +
"				FOR XML PATH('')), " +
"				1,1, '') AS Novedades " +
"				FROM #Registros R " +
"				WHERE ISNULL(R.TipoNovedad,'') NOT IN ('SIN NOVEDAD', '') " +
"				GROUP BY R.fk_OT, R.fk_Precinto) AS Data " +
"	ON Data.fk_OT = Registros.fk_OT " +
"	AND Data.fk_Precinto = Registros.fk_Precinto " +
"	UPDATE #Registros " +
"                    SET        Formatos = Data.Formatos " +
"	FROM #Registros Registros " +
"	INNER JOIN (SELECT R.fk_OT, R.fk_Precinto, STUFF( " +
"				(SELECT DISTINCT '/' + Formato " +
"				FROM #Registros R2 " +
"                            WHERE(R2.fk_OT = R.fk_OT) " +
"				AND R2.fk_Precinto = R.fk_Precinto " +
"				FOR XML PATH('')), " +
"				1,1, '') AS Formatos " +
"				FROM #Registros R " +
"				GROUP BY R.fk_OT, R.fk_Precinto) AS Data " +
"	ON Data.fk_OT = Registros.fk_OT " +
"	AND Data.fk_Precinto = Registros.fk_Precinto " +
"	UPDATE #Registros " +
"       SET     FechaInicial = Inicial " +
"			,FechaFinal = Final " +
"		FROM #Registros R " +
"		INNER JOIN (	SELECT R.fk_OT, R.fk_Precinto, MIN(SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 7, 4) + '/' + SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 4, 2) + '/' + SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 1, 2)) AS Inicial " +
"								,MAX(SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 7, 4) + '/' + SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 4, 2) + '/' + SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 1, 2)) AS Final " +
"					   FROM #Registros R " +
"					   INNER JOIN	[DB_Miharu.Core].Process.TBL_File_Data FD WITH(NOLOCK) " +
"							   ON	FD.fk_Expediente = R.fk_Expediente " +
"							   AND FD.fk_Folder = R.fk_Folder " +
"					   INNER JOIN	[DB_Miharu.Imaging_Core].Config.TBL_Matriz_Documento MD WITH(NOLOCK) " +
"								ON	MD.fk_Documento = FD.fk_Documento " +
"								AND MD.fk_Campo = FD.fk_Campo " +
"								AND MD.id_Columna = 1 " +
"						GROUP BY R.fk_OT, R.fk_Precinto) Data " +
"		ON Data.fk_OT = R.fk_OT " +
"		AND Data.fk_Precinto = R.fk_Precinto " +
"		SET LANGUAGE Spanish; " +
"		UPDATE #Registros " +
"		SET FechaCambio = REPLACE(UPPER(FORMAT(CAST(SUBSTRING(CAST(fk_Llave AS VARCHAR(6)),1,4) + '/' + '01' + '/' + SUBSTRING(CAST(fk_Llave AS VARCHAR(6)),5,2)  AS DATETIME), 'MMMM/yyyy')), '/', ' DE ') " +
"		WHERE TipoSerie IN ('270', '50')    " +
"		SET LANGUAGE US_English; " +
"	UPDATE #Registros " +
"	SET Fecha = SUBSTRING(Fecha,7,10) +'/'+ SUBSTRING(Fecha ,4,2) + '/'+SUBSTRING(Fecha,1,2) " +
"	UPDATE #Registros " +
"	SET ReportePrecinto = @Dependencia + '.' + CAST(ISNULL(TipoSerie, '') AS VARCHAR(4)) + " +
"	CASE TipoSerie WHEN '190' THEN ',Historias Laborales/' + ISNULL(CAST(fk_Llave AS VARCHAR(30)),'') + ISNULL('/' + Nombres,'') + ISNULL('/' + TipoTrabajador,'') + ISNULL('/' + Estado,'') " +
"				   WHEN '270' THEN '.10,Nómina/' + FechaCambio + ISNULL('/' + Novedades,'') + ',' + SUBSTRING(CAST(fk_Llave AS VARCHAR(100)),5,2) + '/' + SUBSTRING(CAST(fk_Llave AS VARCHAR(100)),1,4) + ',' + ISNULL(Novedades,'') " +
"				   ELSE ',Autoliquidaciones/' + FechaCambio + ISNULL('/' + Novedades,'') + ',' + SUBSTRING(CAST(fk_Llave AS VARCHAR(100)),5,2) + '/' + SUBSTRING(CAST(fk_Llave AS VARCHAR(100)),1,4) + ',' + ISNULL(Novedades,'') " +
"                                End " +
"	 + ',' + ISNULL(FechaInicial,'') + ',' + ISNULL(FechaFinal,'') + ',' + CAST(Numero_Folios AS VARCHAR(100)) + ',' +  " +
"	 CASE TipoSerie WHEN '190' THEN ISNULL(TipoTrabajador,'') + ',' + ISNULL(Estado,'') ELSE Formatos END " +
"	INSERT #Registros " +
"	SELECT MAX(CodigoEstiba) AS CodigoEstiba, MAX(TipoSerie) AS TipoSerie,  MAX(Sigla_Serie) AS Sigla_Serie, MAX(fk_Precinto) AS fk_Precinto " +
"			,MAX(fk_OT) AS fk_OT, MAX(fk_Cargue) AS fk_Cargue, MAX(fk_Paquete) AS fk_Paquete, MAX(fk_Llave) AS fk_Llave, MIN(fk_Expediente) AS fk_Expediente " +
"			,MAX(fk_Folder) AS fk_Folder, 0 AS id_File, 1 AS NumFolios, 0 AS fk_Tipologia, 0 AS fk_Documento, 1 AS id_Version, '' AS File_Unique_Identifier " +
"			,NULL AS GruppoDocumento, 11 AS fk_Entidad_Servidor, 5 AS fk_Servidor, 1 AS Folios_File, 0 AS Tamaño_Imagen_File, 0 AS FolioInicial, '0' AS Nombre_Imagen_File " +
"			,MIN(Fecha), '' AS NombresFile, NULL AS TipoNovedad " +
"			,NULL AS TipoAporte, MAX(Tipo_Archivo), 1 AS ImagenPrincipal " +
"			,NULL AS Formato, 0 AS CodigoDocumento, 'Imagen Principal' AS NombreDocumento, '' AS Novedades, ReportePrecinto, NULL AS ReporteFiles, MAX(FechaCambio), MAX(Formatos) " +
"			,MAX(TipoTrabajador), MAX(Estado), MAX(FechaInicial), MAX(FechaFinal), MAX(Nombres) AS Nombres " +
"	FROM #Registros " +
"	GROUP BY ReportePrecinto " +
"	UPDATE #Registros " +
"	SET ReporteFiles = ISNULL(Fecha,'') + ',' + ISNULL(NombreDocumento,'') + ISNULL('/' + TipoNovedad,'') + ',' + '@Tamaño_Imagen_File' + ',' + ISNULL(CAST(CodigoDocumento AS VARCHAR(100)),'')  + ',' +  CAST(CodigoEstiba AS VARCHAR(100))  + CAST('/Imagenes/' AS VARCHAR(100)) + CAST(fk_Llave AS VARCHAR(100)) + '/' + '@Nombre_Imagen_File' + ',' + " +
"	CAST(Folios_File AS VARCHAR(100)) + ',' + CAST(fk_Expediente AS VARCHAR(100)) + CAST(fk_Folder AS VARCHAR(100))+ CAST(id_File AS VARCHAR) + ',' + CAST(ImagenPrincipal AS VARCHAR(1)) " +
"	SELECT Sigla_Serie,fk_Llave, ReportePrecinto " +
"	FROM #Registros " +
"	GROUP BY Sigla_Serie, fk_Llave, ReportePrecinto " +
"	ORDER BY Sigla_Serie, fk_Llave, ReportePrecinto ASC " +
"	SELECT * " +
"	FROM #Registros " +
"ORDER BY fk_Llave, fk_Expediente, fk_Folder, id_File ASC "


                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_Entidad", EntidadId),
                   New SqlParameter("@fk_Proyecto", ProyectoId),
                    New SqlParameter("@fk_Estiba", Estiba)
               }
                dt = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt
        End Function

        Private Function getOTCSV(EntidadId As Int32, ProyectoId As Int32, Estiba As Int32) As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt2 As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.PA_Exportacion_Data_File_CSVStoreProcedure = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "DECLARE  @id_Doc_Tapa_Lote INT " +
"			,@id_Doc_Tapa_Favidi INT " +
"			,@CampoSerie INT " +
"			,@Activos VARCHAR(5000) " +
"			,@FechaComparacionHLUMV DATETIME " +
"			,@fk_DocumentosTipoNovedadAporte VARCHAR(200) " +
"	SET @FechaComparacionHLUMV = (SELECT	CONVERT(DATETIME, Valor_Parametro_Sistema, 111) " +
"									  FROM	[DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"									 WHERE	Nombre_Parametro_Sistema = '@FechaLimiteUMVTipoDoc') " +
"	SET @Activos = (SELECT Valor_Parametro_Sistema " +
"					FROM [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"					WHERE Nombre_Parametro_Sistema = 'Activos' " +
"					AND fk_Entidad = @fk_Entidad " +
"					AND fk_Proyecto = @fk_Proyecto) " +
"	SET @id_Doc_Tapa_Lote = (SELECT TOP 1 ISNULL(Valor_Parametro_Sistema,0) " +
"							FROM [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"							WHERE fk_Entidad = @fk_Entidad " +
"							AND fk_Proyecto = @fk_Proyecto " +
"							AND Nombre_Parametro_Sistema = '@TapaLote') " +
"	SET @id_Doc_Tapa_Favidi = (SELECT TOP 1 ISNULL(Valor_Parametro_Sistema,0) " +
"							 FROM [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"							 WHERE fk_Entidad = @fk_Entidad " +
"							 AND fk_Proyecto = @fk_Proyecto " +
"							 AND Nombre_Parametro_Sistema = '@TapaFavidi') " +
"	SET @CampoSerie = (SELECT TOP 1 ISNULL(Valor_Parametro_Sistema,0) " +
"							FROM [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema " +
"							WHERE fk_Entidad = @fk_Entidad " +
"							AND fk_Proyecto = @fk_Proyecto " +
"							AND Nombre_Parametro_Sistema = '@CampoSerie') " +
"	SELECT DISTINCT	Estiba.CodigoEstiba " +
"			,EmpaqueData.Data_Campo AS TipoSerie " +
"			,PS.Valor_Parametro_Sistema as Sigla_Serie " +
"			,Contenedor.fk_Precinto " +
"			,Contenedor.fk_OT " +
"			,Contenedor.fk_Cargue " +
"			,Contenedor.fk_Paquete " +
"			,CASE EmpaqueData.Data_Campo " +
"			WHEN '190' THEN CONVERT(VARCHAR(50), UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, 1, NULL, Fil.fk_Documento, 4))) " +
"			ELSE Precinto.Precinto " +
"			END AS fk_Llave " +
"			,Fil.fk_Expediente " +
"			,Fil.fk_Folder " +
"			,Fil.id_File " +
"			,(SELECT (SUM(FOLIOS_FILE)/2) AS NUMERO_FOLIOS FROM [DB_Miharu.Core].Process.TBL_File WHERE FK_EXPEDIENTE =  FIl.fk_Expediente) AS NUMERO_FOLIOS " +
"			,Documento.fk_Tipologia " +
"			,Fil.fk_Documento " +
"			,ImagingFile.id_Version " +
"			,ImagingFile.File_Unique_Identifier " +
"			,CASE EmpaqueData.Data_Campo " +
"			WHEN '190' THEN '' " +
"			ELSE 'TRD' END GrupoDocumento " +
"			,Folder.fk_Entidad_Servidor " +
"			,Folder.fk_Servidor " +
"			,Fil.Folios_File " +
"			,ImagingFile.Tamaño_Imagen_File			" +
"			,CAST([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 2) AS INT) AS FolioInicial " +
"			,'' AS Nombre_Imagen_File " +
"			,[DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 1) AS Fecha " +
",[DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 1) AS FechaCambio " +
"			,UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 5)) AS Nombres " +
"			,[DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 6) AS TipoNovedad " +
"			,[DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, NULL, Fil.fk_Documento, 7) AS TipoAporte " +
"			,Estiba.Tipo_Archivo " +
"			,0 AS Grupo " +
"	INTO ##Registros " +
"	FROM [DB_Miharu.Integration].[UMV].[TBL_Estiba] AS Estiba " +
"	INNER JOIN	[DB_Miharu.Integration].[UMV].TBL_Estiba_Detalle AS EstibaDetalle " +
"			ON	EstibaDetalle.fk_Estiba = Estiba.id_Estiba " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Empaque AS Empaque " +
"			ON	EstibaDetalle.Caja = Empaque.Precinto " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Empaque_Contenedor AS Empaque_Contenedor " +
"			ON	Empaque_Contenedor.fk_OT = Empaque.fk_OT " +
"			AND Empaque_Contenedor.fk_Empaque = Empaque.id_Empaque " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Empaque_Data AS EmpaqueData " +
"			ON	EmpaqueData.fk_OT = Empaque.fk_OT " +
"			AND EmpaqueData.fk_Empaque = Empaque.id_Empaque " +
"			AND EmpaqueData.fk_Campo = @CampoSerie " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema PS " +
"			ON	PS.Nombre_Parametro_Sistema = EmpaqueData.Data_Campo " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Contenedor Contenedor " +
"			ON	Empaque_Contenedor.Token = Contenedor.Token " +
"			AND Empaque_Contenedor.fk_OT = Contenedor.fk_OT " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_Precinto Precinto " +
"			ON	Precinto.fk_OT = Contenedor.fk_OT " +
"			AND	Precinto.id_Precinto = Contenedor.fk_Precinto " +
"	INNER JOIN	[DB_Miharu.Imaging_Core].Process.TBL_OT OT " +
"			ON	OT.id_OT = Contenedor.fk_OT " +
"			AND OT.fk_OT_Tipo = 2 " +
"	INNER JOIN	[DB_Miharu.Core].Imaging.TBL_Folder Folder " +
"			ON	Contenedor.fk_Cargue = Folder.fk_Cargue " +
"			AND Contenedor.fk_Paquete = Folder.fk_Cargue_Paquete " +
"	INNER JOIN	[DB_Miharu.Core].Process.TBL_File Fil " +
"			ON	Fil.fk_Expediente = Folder.fk_Expediente " +
"			AND Fil.fk_Folder = Folder.fk_Folder " +
"			AND Fil.fk_Documento NOT IN (@id_Doc_Tapa_Lote, @id_Doc_Tapa_Favidi) " +
"	INNER JOIN	[DB_Miharu.Core].Config.TBL_Documento Documento " +
"			ON	Documento.id_Documento = Fil.fk_Documento " +
"	INNER JOIN	[DB_Miharu.Core].Imaging.TBL_File ImagingFile " +
"			ON	ImagingFile.fk_Expediente = Fil.fk_Expediente " +
"			AND ImagingFile.fk_Folder = Fil.fk_Folder " +
"			AND ImagingFile.fk_File = Fil.id_File " +
"	WHERE		Exportada = 0 " +
"			AND OT.fk_Entidad = @fk_Entidad " +
"			AND OT.fk_Proyecto = @fk_Proyecto " +
"			AND Estiba.CodigoEstiba = @fk_Estiba " +
"	ALTER TABLE ##Registros ADD CodigoDocumento VARCHAR(500) " +
"	ALTER TABLE ##Registros ADD NombreDocumento VARCHAR(MAX) " +
"	ALTER TABLE ##Registros ADD Novedades VARCHAR(500) " +
"	ALTER TABLE ##Registros ADD ReportePrecinto VARCHAR(MAX) " +
"	ALTER TABLE ##Registros ADD ReporteFiles VARCHAR(MAX) " +
"	ALTER TABLE ##Registros ALTER COLUMN GrupoDocumento VARCHAR(10) " +
"	UPDATE ##Registros " +
"	SET GrupoDocumento = 'TRD' " +
"	FROM ##Registros R " +
"	INNER JOIN	[DB_MIharu.Imaging_Core].Process.TBL_Proceso_Data PD " +
"			ON	PD.fk_Expediente = R.fk_Expediente " +
"			AND PD.fk_Folder = R.fk_Folder " +
"	WHERE		TipoSerie = '190' " +
"			AND ISNULL(GrupoDocumento,'') = '' " +
"		AND PD.Campo_49 IN (SELECT Data FROM  [DB_MIharu.Imaging_Core].[dbo].[Split] (@Activos, ',')); " +
"	UPDATE ##Registros " +
"	SET GrupoDocumento = Data.GrupoDocumento " +
"	FROM ##Registros R " +
"	INNER JOIN (SELECT R.fk_OT, " +
"				R.fk_Precinto, " +
"				CASE " +
"				WHEN MAX([DB_Miharu.Integration].[dbo].[Fn_Formato_Fecha](CAST(Valor_File_Data AS VARCHAR(10)), 'yyyy/mm/dd')) <= @FechaComparacionHLUMV THEN 'DAFP' " +
"				ELSE 'TRD' " +
"				END AS GrupoDocumento " +
"				FROM ##Registros R " +
"				INNER JOIN	[DB_Miharu.Core].Process.TBL_File_Data AS FileData " +
"						ON	FileData.fk_Expediente = R.fk_Expediente " +
"						AND FileData.fk_Folder = R.fk_Folder				" +
"				INNER JOIN [DB_Miharu.Imaging_Core].Config.TBL_Matriz_Documento MD " +
"						ON MD.fk_Documento = FileData.fk_Documento " +
"						AND MD.fk_Campo = FileData.fk_Campo " +
"						AND MD.id_Columna = 1 " +
"				WHERE	TipoSerie = '190' " +
"				AND ISNULL(GrupoDocumento,'') = '' " +
"				GROUP BY R.fk_OT, R.fk_Precinto) Data " +
"	ON Data.fk_OT = R.fk_OT " +
"	AND Data.fk_Precinto = R.fk_Precinto	" +
"	UPDATE ##Registros " +
"       SET  CodigoDocumento = fk_Documento_UMV " +
"		,NombreDocumento = Nombre_Documento_UMV " +
"	FROM ##Registros R " +
"	INNER JOIN [DB_Miharu.Integration].[Config].TBL_Documento_Homologacion DocHomo " +
"			ON R.fk_Documento = DocHomo.fk_Documento_Core " +
"			AND R.GrupoDocumento = DocHomo.Grupo_Documento " +
"	SET @fk_DocumentosTipoNovedadAporte = (SELECT Valor_Parametro_Sistema FROM [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema  WHERE Nombre_Parametro_Sistema = 'DocumentosNominaTipoNovedad' AND fk_Entidad = @fk_Entidad AND fk_Proyecto = @fk_Proyecto) " +
"	UPDATE ##Registros " +
"                    SET    Novedades = Data.Novedades " +
"	FROM ##Registros Registros " +
"	INNER JOIN (SELECT R.fk_OT, R.fk_Precinto, STUFF( " +
"				(SELECT DISTINCT '/' + TipoNovedad " +
"				FROM ##Registros R2 " +
"				WHERE ISNULL(R2.TipoNovedad,'') NOT IN ('SIN NOVEDAD', '') " +
"				AND R2.fk_OT = R.fk_OT " +
"				AND R2.fk_Precinto = R.fk_Precinto " +
"				FOR XML PATH('')), " +
"				1,1, '') AS Novedades " +
"				FROM ##Registros R " +
"				WHERE ISNULL(R.TipoNovedad,'') NOT IN ('SIN NOVEDAD', '') " +
"				GROUP BY R.fk_OT, R.fk_Precinto) AS Data " +
"	ON Data.fk_OT = Registros.fk_OT " +
"	AND Data.fk_Precinto = Registros.fk_Precinto " +
"	ALTER TABLE ##Registros ADD FechaInicial VARCHAR(10) " +
"		ALTER TABLE ##Registros ADD FechaFinal VARCHAR(10) " +
"	UPDATE ##Registros " +
"                         SET   FechaInicial = Inicial " +
"		FROM ##Registros R " +
"		INNER JOIN (	SELECT R.fk_OT, R.fk_Precinto, MIN(SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 7, 4) + '/' + SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 4, 2) + '/' + SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 1, 2)) AS Inicial " +
"					   FROM ##Registros R " +
"					   INNER JOIN	[DB_Miharu.Core].Process.TBL_File_Data FD " +
"							   ON	FD.fk_Expediente = R.fk_Expediente " +
"							   AND FD.fk_Folder = R.fk_Folder " +
"					   INNER JOIN	[DB_Miharu.Imaging_Core].Config.TBL_Matriz_Documento MD " +
"								ON	MD.fk_Documento = FD.fk_Documento " +
"								AND MD.fk_Campo = FD.fk_Campo " +
"								AND MD.id_Columna =  1 " +
"						GROUP BY R.fk_OT, R.fk_Precinto) Data " +
"		ON Data.fk_OT = R.fk_OT " +
"		AND Data.fk_Precinto = R.fk_Precinto " +
"		UPDATE ##Registros " +
"                          SET  FechaFinal = Final " +
"		FROM ##Registros R " +
"		INNER JOIN (	SELECT R.fk_OT, R.fk_Precinto, MAX(SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 7, 4) + '/' + SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 4, 2) + '/' + SUBSTRING(CAST(FD.Valor_File_Data AS VARCHAR(10)), 1, 2)) AS Final " +
"					   FROM ##Registros R " +
"					   INNER JOIN	[DB_Miharu.Core].Process.TBL_File_Data FD " +
"							   ON	FD.fk_Expediente = R.fk_Expediente " +
"							   AND FD.fk_Folder = R.fk_Folder " +
"					   INNER JOIN	[DB_Miharu.Imaging_Core].Config.TBL_Matriz_Documento MD " +
"								ON	MD.fk_Documento = FD.fk_Documento " +
"								AND MD.fk_Campo = FD.fk_Campo " +
"								AND MD.id_Columna = 1 " +
"						GROUP BY R.fk_OT, R.fk_Precinto) Data " +
"		ON Data.fk_OT = R.fk_OT " +
"		AND Data.fk_Precinto = R.fk_Precinto " +
"		SET LANGUAGE Spanish;" +
"	UPDATE ##Registros " +
"		SET FechaCambio = REPLACE(UPPER(FORMAT( CAST((SELECT CONCAT(CAST((SELECT SUBSTRING(FechaCambio,1,4))AS VARCHAR),CAST((SELECT SUBSTRING(FechaCambio,5,6))AS VARCHAR)))AS DATETIME), 'MMMM/yyyy')),'/', ' DE ')    " +
"                WHERE(IsDate(FechaCambio) = 1) " +
"		SET LANGUAGE US_English; " +
"	UPDATE ##Registros " +
"	SET ReportePrecinto = " +
"    CAST(ISNULL(TipoSerie, '') AS VARCHAR(100)) + ',' + " +
"	CASE " +
"	WHEN NombreDocumento IS NOT NULL THEN CAST(ISNULL(NombreDocumento, '') AS VARCHAR(100)) + '/' + FechaCambio + ',' " +
"	WHEN Novedades IS NOT NULL THEN  + ISNULL(Novedades,'') + ',' END + " +
"	CASE ISNULL(TipoSerie, '') " +
"	WHEN '190' THEN '' " +
"	ELSE SUBSTRING(CAST(fk_Llave AS VARCHAR(100)),5,2) + '/' + SUBSTRING(CAST(fk_Llave AS VARCHAR(100)),1,4) " +
"	END + ',' + CAST(ISNULL(Novedades, '') AS VARCHAR(100)) + ',' + ISNULL(FechaInicial,'') + ',' + ISNULL(FechaFinal,'') + ',' + CAST(Numero_Folios AS VARCHAR(100)) + ',' + UPPER([DB_Miharu.Core].[Process].Fn_getDatos(fk_Expediente, fk_Folder, 1, NULL, fk_Documento, 4)) " +
"	,Fecha = SUBSTRING(Fecha,7,10) +'/'+ SUBSTRING(Fecha ,4,2) + '/'+SUBSTRING(Fecha,1,2) " +
"	,ReporteFiles = Fecha + ',' + NombreDocumento + ',' + CAST(Tamaño_Imagen_File AS VARCHAR(100)) + ',' + CAST(CodigoDocumento AS VARCHAR(100))  + ',' +  CAST(CodigoEstiba AS VARCHAR(100))  + CAST('/Imagenes/' AS VARCHAR(100)) + CAST(fk_Llave AS VARCHAR(100)) + '/' + 'Nombre_Imagen_File' + ',' + " +
"	CAST(Folios_File AS VARCHAR(100)) + ',' + CAST(fk_Expediente AS VARCHAR(100)) + CAST(fk_Folder AS VARCHAR(100))+ CAST(id_File AS VARCHAR) + ',' + CAST(0 AS VARCHAR(1)) " +
"	SELECT Sigla_Serie,fk_Llave, ReportePrecinto FROM ##Registros GROUP BY Sigla_Serie, fk_Llave, ReportePrecinto ORDER BY Sigla_Serie, fk_Llave, ReportePrecinto ASC "

                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@fk_Entidad", EntidadId),
                   New SqlParameter("@fk_Proyecto", ProyectoId),
                   New SqlParameter("@fk_Estiba", Estiba)
               }
                dt2 = ExecuteQuery(sqlquery, conn, SqlParameter)
                Return dt2
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
            Return dt2
        End Function

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

        Shared Function GetEncoderInfo(mimeType As String) As ImageCodecInfo

            Dim encoders As ImageCodecInfo()
            encoders = ImageCodecInfo.GetImageEncoders()
            Dim encoder As ImageCodecInfo = (From enc In encoders Where enc.MimeType = mimeType Select enc).First()
            Return encoder


        End Function

        Shared Function GetMimeType(ext As String) As String
            '    CodecName FilenameExtension FormatDescription MimeType
            '    .BMP;*.DIB;*.RLE BMP ==> image/bmp
            '    .JPG;*.JPEG;*.JPE;*.JFIF JPEG ==> image/jpeg
            '    *.GIF GIF ==> image/gif
            '    *.TIF;*.TIFF TIFF ==> image/tiff
            '    *.PNG PNG ==> image/png

            Select Case ext.ToLower()
                Case ".bmp"
                    Return "image/bmp"
                Case ".jpg"
                    Return "image/jpeg"
                Case "gif"
                    Return "image/gif"
                Case ".tif"
                    Return "image/tiff"
                Case "png"
                    Return "image/png"
                Case Else
                    Return "image/jpeg"

            End Select

        End Function

        Shared Sub ComprimirImagen(inputFile As String)
            Dim oGdPictureImaging As New GdPictureImaging
            Dim oLicenseManager As New LicenseManager()
            oLicenseManager.RegisterKEY("0426337214354855751491240")
            Dim txtDictsPath As String
            txtDictsPath = oLicenseManager.GetRedistPath() + "OCR\\" 'esto es para efectos de prueba no más.
            Dim ImageID As Integer = oGdPictureImaging.CreateGdPictureImageFromFile(inputFile)
            If ImageID <> 0 Then
                oGdPictureImaging.SaveAsTIFF(ImageID, inputFile, TiffCompression.TiffCompressionJPEG, 90)
                oGdPictureImaging.ReleaseGdPictureImage(ImageID)
            Else
                MessageBox.Show("Error: " + oGdPictureImaging.GetStat().ToString())
            End If

            'Dim image As Image = Image.FromFile(inputFile)
            'Dim eps As New EncoderParameters(1)
            'eps.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compression)
            'Dim mimetype As String = GetMimeType(New System.IO.FileInfo(inputFile).Extension)
            'Dim ici As ImageCodecInfo = GetEncoderInfo(mimetype)
            'image.Save(ouputfile, ici, eps)

        End Sub


        Private Sub ExportarImagen(nManager As FileProviderManager, ByVal ItemFile As DataRow, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, nFolderName As String, nExportDataFilePDFA As DataTable, nPathTemp As String)
            Dim Folios = nManager.GetFolios(CLng(ItemFile.Item("fk_Expediente").ToString()), CShort(ItemFile.Item("fk_Folder").ToString()), CShort(ItemFile.Item("id_File").ToString()), CShort(ItemFile.Item("id_Version").ToString()))

            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim FileNameSalida As String = Nothing
            Dim nFileName As String = Nothing
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = String.Empty
            Dim Imagen() As Byte = Nothing
            Dim Thumbnail() As Byte = Nothing
            Dim contadorbmp As Int32 = 0
            Dim Image As String = String.Empty

            nPathTemp = nFileFolderName
            Try
                For folio As Short = 1 To Folios
                    contadorbmp += 1
                    nManager.GetFolio(CLng(ItemFile.Item("fk_Expediente").ToString()), CShort(ItemFile.Item("fk_Folder").ToString()), CShort(ItemFile.Item("id_File").ToString()), CShort(ItemFile.Item("id_Version").ToString()), folio, Imagen, Thumbnail)
                    FileName = UMVPlugin.AppPath & UMVPlugin.TempPath & Guid.NewGuid().ToString() & "_" & ItemFile.Item("fk_Llave").ToString() & "_" & ItemFile.Item("id_File").ToString() & "_" & ItemFile.Item("Folios_File").ToString() & "_" & contadorbmp & ".tif"
                    'FileNameSalida = UMVPlugin.AppPath & UMVPlugin.TempPath & Guid.NewGuid().ToString() & "_" & ItemFile.Item("fk_Llave").ToString() & "_" & ItemFile.Item("id_File").ToString() & "_" & ItemFile.Item("Folios_File").ToString() & "_" & contadorbmp & "_peso.tif"
                    FileNames.Add(FileName)
                    FileNamesCons.Add(FileName)

                    Using fs = New FileStream(FileName, FileMode.Create)
                        Try
                            fs.Write(Imagen, 0, Imagen.Length)
                            fs.Close()
                            ComprimirImagen(FileName)
                        Catch ex As Exception
                            Slyg.Tools.Imaging.ImageManager.CrearLog(nFileFolderName, ex.Message.ToString(), nPathTemp)
                        End Try

                    End Using

                Next

                If Not (Me._plugin.Manager.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF) Then

                    If (FileNames.Count > 0) Then
                        Dim Format As ImageManager.EnumFormat

                        Select Case ".bmp"
                            Case ".bmp"
                                Format = ImageManager.EnumFormat.Bmp
                            Case ".gif"
                                Format = ImageManager.EnumFormat.Gif
                            Case ".jpg"
                                Format = ImageManager.EnumFormat.Jpeg
                                nCompresion = ImageManager.EnumFormat.Jpeg
                            Case ".pdf"
                                Format = ImageManager.EnumFormat.Pdf
                                nCompresion = ImageManager.EnumFormat.Jpeg
                            Case ".png"
                                Format = ImageManager.EnumFormat.Png
                            Case ".tiff"
                                Format = ImageManager.EnumFormat.Tiff
                        End Select

                        Dim Valido As Boolean = True
                        Dim MsgError As String = ""

                        ExtensionAux = IIf(formatoAux = ImageManager.EnumFormat.Pdf, ".pdf", ".pdf").ToString

                        If ItemFile.Item("Sigla_Serie").ToString() = "HL" Then
                            If ((Valido = True) And (FileNameAux = String.Empty)) Then
                                contador += 1
                                FileNameAux = ItemFile.Item("Sigla_Serie").ToString() & "_" & ItemFile.Item("fk_Llave").ToString() & "_" & contador.ToString("0000")
                                nFileName = UMVPlugin.AppPath & UMVPlugin.TempPath & FileNameAux & ExtensionAux
                            ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                                ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, ".pdf", ExtensionAux))
                                nFileName = UMVPlugin.AppPath & UMVPlugin.TempPath & FileNameAux & ExtensionAux
                            ElseIf Valido = False Then
                                Throw New Exception(MsgError)
                            End If
                        ElseIf ItemFile.Item("Sigla_Serie").ToString() = "NM" Or ItemFile.Item("Sigla_Serie").ToString() = "PF" Then
                            contador += 1
                            Dim MesAnio As String
                            MesAnio = ItemFile.Item("fk_Llave").ToString()
                            Dim anio As String = MesAnio.Substring(0, 4)
                            Dim mes As String = MesAnio.Substring(4, 2)

                            If ((Valido = True) And (FileNameAux = String.Empty)) Then

                                FileNameAux = ItemFile.Item("Sigla_Serie").ToString() & "_" & mes & "_" & anio & "_" & contador.ToString("0000")

                                nFileName = UMVPlugin.AppPath & UMVPlugin.TempPath & FileNameAux & ExtensionAux
                            ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                                ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, ".pdf", ExtensionAux))

                                nFileName = UMVPlugin.AppPath & UMVPlugin.TempPath & FileNameAux & ExtensionAux
                            ElseIf Valido = False Then
                                ' EscribeLog(Throw New Exception(MsgError)
                            End If

                        End If
                        '--------------------------------------------k-----------------------------
                        Slyg.Tools.Imaging.ImageManager.SavePDFA(FileNames, nFileFolderName & FileNameAux & ExtensionAux, FileName, "", ImageManager.EnumFormat.Pdf, nCompresion, False, Image, True, nFileName)
                        '-------------------------------------------------------------------------

                        ItemFile.Item("Nombre_Imagen_File") = nFolderName & FileNameAux & ExtensionAux


                        'InsertarLogCargue(ItemFile.Item("fk_Llave"), ItemFile.Item("fk_Expediente"), "True", Date.Now, Date.Now, ItemFile.Item("Nombre_Imagen_File"), "", nFileFolderName, FileNames.Count())

                    End If
                End If
            Catch ex As Exception
                'DMB.DesktopMessageShow("Exportar imagen", ex)
            End Try
        End Sub
        Private Sub InsertarLogCargue(fk_Llave As Int32, fk_expediente As Int32, Estado As Boolean, Fecha_inicio As Date, Fecha_Fin As Date, fk_nombreImagen As String, Token As String, npathFileName As String, NumeroFolios As Int32)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try

                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()
                Dim ExportDataFilePDFA = dbmImaging.SchemaProcess.PA_Exportacion_Log.DBExecute(fk_Llave, fk_expediente, fk_nombreImagen, Token, Estado, Fecha_inicio, Fecha_Fin, NumeroFolios)
                dbmImaging.Transaction_Commit()
            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                EscribeLog(npathFileName, "Error al insertal en tablas TBL_CargueLog y TBLCargueLogDetalle:" + npathFileName + " " + fk_nombreImagen, False, True)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(_plugin.Manager.Sesion.Usuario.id)
                dbmImaging.Transaction_Begin()
                Dim ExportDataFilePDFA = dbmImaging.SchemaProcess.PA_Exportacion_Log.DBExecute(fk_Llave, fk_expediente, fk_nombreImagen, Token, Estado, Fecha_inicio, Fecha_Fin, NumeroFolios)
                dbmImaging.Transaction_Commit()
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


        Private Sub ExportarImagenExpediente(nManager As FileProviderManager, ByVal nExpedientesbyGroupDataView As DataView, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, nFolderName As String)
            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = String.Empty

            Try
                For Each itemfile As DataRowView In nExpedientesbyGroupDataView
                    Dim Folios = nManager.GetFolios(CLng(itemfile.Item("fk_Expediente")), CShort(itemfile.Item("fk_Folder")), CShort(itemfile.Item("fk_File")), CShort(itemfile.Item("id_Version")))
                    For folio As Short = 1 To Folios
                        Dim Imagen() As Byte = Nothing
                        Dim Thumbnail() As Byte = Nothing

                        nManager.GetFolio(CLng(itemfile.Item("fk_Expediente")), CShort(itemfile.Item("fk_Folder")), CShort(itemfile.Item("fk_File")), CShort(itemfile.Item("id_Version")), folio, Imagen, Thumbnail)

                        FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                        FileNames.Add(FileName)

                        FileNamesCons.Add(FileName)

                        Using fs = New FileStream(FileName, FileMode.Create)
                            fs.Write(Imagen, 0, Imagen.Length)
                            fs.Close()
                        End Using
                    Next

                    If Not (Program.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF) Then

                        If (FileNames.Count > 0) Then
                            Dim Format As ImageManager.EnumFormat

                            Select Case Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                                Case ".bmp"
                                    Format = ImageManager.EnumFormat.Bmp
                                Case ".gif"
                                    Format = ImageManager.EnumFormat.Gif
                                Case ".jpg"
                                    Format = ImageManager.EnumFormat.Jpeg
                                    nCompresion = ImageManager.EnumFormat.Jpeg
                                Case ".pdf"
                                    Format = ImageManager.EnumFormat.Pdf
                                    nCompresion = ImageManager.EnumFormat.Jpeg
                                Case ".png"
                                    Format = ImageManager.EnumFormat.Png
                                Case ".tif"
                                    Format = ImageManager.EnumFormat.Tiff
                            End Select

                            Dim Valido As Boolean = True
                            Dim MsgError As String = ""

                            If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Renombramiento_Imagen_Exportacion) Then
                                FileNameAux = EventManager.Nombre_Imagen_Exportar(CLng(itemfile.Item("fk_Expediente")), CShort(itemfile.Item("fk_Folder")), CShort(itemfile.Item("fk_File")), CInt(itemfile.Item("fk_Documento")), CInt(itemfile.Item("fk_Grupo")), Valido, MsgError)

                                If ((Valido = True) And (FileNameAux = String.Empty)) Then
                                    FileNameAux = Nombre_Imagen_Exportar(CLng(itemfile.Item("fk_Expediente")), CShort(itemfile.Item("fk_Folder")), CShort(itemfile.Item("fk_File")), CInt(itemfile.Item("fk_Documento")), CInt(itemfile.Item("fk_Grupo")), Valido, MsgError)
                                End If
                            End If

                            ExtensionAux = IIf(formatoAux = ImageManager.EnumFormat.Pdf, ".pdf", Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString

                            If ((Valido = True) And (FileNameAux = String.Empty)) Then
                                FileNameAux = itemfile.Item("File_Unique_Identifier").ToString() & "_0001"
                                FileName = nFileFolderName & FileNameAux & ExtensionAux
                            ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                                ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, ExtensionAux))
                                FileName = nFileFolderName & FileNameAux & ExtensionAux
                            ElseIf Valido = False Then
                                Throw New Exception(MsgError)
                            End If

                            '-------------------------------------------------------------------------
                            ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, False, Program.AppPath & Program.TempPath, True)
                            '-------------------------------------------------------------------------
                            itemfile.Item("Nombre_Imagen_File") = nFolderName & FileNameAux & ExtensionAux
                        End If
                    End If
                Next
            Catch ex As Exception
                DMB.DesktopMessageShow("Exportar imagen", ex)
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
                                                ExportacionDataSet As OffLineViewer.Library.xsdOffLineData)
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

        Private Sub GenerarTXT(ByVal OutputFolder As String, ByVal FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, ByVal FileDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable, ByVal FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, ByVal FileValidacionDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable)
            Dim CSVData = New CSV.CSVData(vbTab, """", True)

            CSVData.SaveAsCSV(New CSV.CSVTable(FolderDataTable), OutputFolder & "Folders.txt", False)
            CSVData.SaveAsCSV(New CSV.CSVTable(FileDataTable), OutputFolder & "Files.txt", False)
            CSVData.SaveAsCSV(New CSV.CSVTable(FileDataDataTable), OutputFolder & "Data.txt", False)
            CSVData.SaveAsCSV(New CSV.CSVTable(FileValidacionDataTable), OutputFolder & "Validaciones.txt", False)
        End Sub

        Private Sub MostrarDatagrid()

            ExpedientesDataGridView.Visible = True
            CargarExpedientes()
        End Sub

        Private Sub Load_FormatoCargue()
            If (Not Usa_Exportacion_PDF) Then
                formatoAux = formato
                CompresionAux = compresion
            Else
                formatoAux = ImageManager.EnumFormat.Pdf
                CompresionAux = Utilities.GetEnumCompression(CType(formatoAux, DesktopConfig.FormatoImagenEnum))
            End If

            CargarExpedientes()
        End Sub

        Private Sub ExportarImagenAgrupada(nManager As FileProviderManager, ByVal nFilesExpedientesbyGroupDataView As DataView, ngrupo As Integer, nfk_Expediente As Long, nfk_Folder As Short, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, nFolderName As String)
            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
            Dim FileNameAux As String = Nothing
            Dim ExtensionAux As String = String.Empty

            For Each itemfile As DataRowView In nFilesExpedientesbyGroupDataView

                Dim Folios = nManager.GetFolios(CLng(itemfile.Item("fk_Expediente")), CShort(itemfile.Item("fk_Folder")), CShort(itemfile.Item("fk_File")), CShort(itemfile.Item("id_Version")))

                For folio As Short = 1 To Folios
                    Dim Imagen() As Byte = Nothing
                    Dim Thumbnail() As Byte = Nothing

                    nManager.GetFolio(CLng(itemfile.Item("fk_Expediente")), CShort(itemfile.Item("fk_Folder")), CShort(itemfile.Item("fk_File")), CShort(itemfile.Item("id_Version")), folio, Imagen, Thumbnail)


                    FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    FileNames.Add(FileName)

                    Using fs = New FileStream(FileName, FileMode.Create)
                        fs.Write(Imagen, 0, Imagen.Length)
                        fs.Close()
                    End Using
                Next
            Next

            If (FileNames.Count > 0) Then
                Dim Format As ImageManager.EnumFormat

                Select Case Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    Case ".bmp"
                        Format = ImageManager.EnumFormat.Bmp
                    Case ".gif"
                        Format = ImageManager.EnumFormat.Gif
                    Case ".jpg"
                        Format = ImageManager.EnumFormat.Jpeg
                        nCompresion = ImageManager.EnumFormat.Jpeg
                    Case ".pdf"
                        Format = ImageManager.EnumFormat.Pdf
                        nCompresion = ImageManager.EnumFormat.Jpeg
                    Case ".png"
                        Format = ImageManager.EnumFormat.Png
                    Case ".tif"
                        Format = ImageManager.EnumFormat.Tiff
                End Select

                Dim Valido As Boolean = True
                Dim MsgError As String = ""

                If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Renombramiento_Imagen_Exportacion) Then
                    FileNameAux = EventManager.Nombre_Imagen_Agrupada_Exportar(nfk_Expediente, nfk_Folder, ngrupo, Valido, MsgError)

                    If ((Valido = True) And (FileNameAux = String.Empty)) Then
                        FileNameAux = Nombre_Imagen_Agrupada_Exportar(nfk_Expediente, nfk_Folder, ngrupo, Valido, MsgError)
                    End If
                End If

                ExtensionAux = IIf(formatoAux = ImageManager.EnumFormat.Pdf, ".pdf", Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString

                If ((Valido = True) And (FileNameAux = String.Empty)) Then
                    FileName = nFileFolderName & Guid.NewGuid().ToString() & "_0001" & ExtensionAux
                ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                    ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, ExtensionAux))
                    FileName = nFileFolderName & FileNameAux & ExtensionAux
                ElseIf Valido = False Then
                    Throw New Exception(MsgError)
                End If

                '-------------------------------------------------------------------------
                ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, False, Program.AppPath & Program.TempPath, True)
                '-------------------------------------------------------------------------

                For Each itemfile As DataRowView In nFilesExpedientesbyGroupDataView
                    itemfile.Item("Nombre_Imagen_File") = nFolderName & FileNameAux & ExtensionAux
                Next
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

                'ElseIf (Me.OTDataGridView.SelectedRows.Count = 0) And Not Me.CheckBoxExpedientes.Checked Then
                '    MessageBox.Show("Se debe seleccionar una OT", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Me.OTDataGridView.Focus()

                'ElseIf Not Me.CheckBoxExpedientes.Checked Then
                '    Dim OTRow = CType(CType(Me.OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Exportacion_OTRow)

                ' Validar si ya fue exportado
                'If (OTRow.Exportado) Then
                '    Dim Respuesta = MessageBox.Show("La OT: " & OTRow.id_OT & ", ya fue exportada, ¿desea volverla a exportar?", Program.AssemblyTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                '    If (Respuesta = DialogResult.No) Then Return False
                'End If

                '' Validar si la OT se puede exportar
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Me._plugin.Manager.Sesion.Usuario.id)

                    'Dim Resultado = dbmImaging.SchemaProcess.PA_Validar_Cargado_Completo.DBExecute(OTRow.id_OT)

                    'If (Not Resultado) Then
                    '    MessageBox.Show("La OT no ha sido totalmente procesada y no se puede exportar", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    Me.OTDataGridView.Focus()
                    'Else
                    '    Return True
                    'End If
                    Return True
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Me._plugin.GetCode, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try

                'Else
                '    'Validaciones Exportación Expedientes

                '    If (Me.ExpedientesDataGridView.Rows.Count = 0) Then
                '        MessageBox.Show("No se han seleccionado Expedientes para exportar", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        Me.ExpedientesDataGridView.Focus()
                '        Return False

                '    Else
                '        For Each row As DataGridViewRow In ExpedientesDataGridView.Rows
                '            If CBool(row.Cells("Exportar").Value) Then
                '                Return True
                '            End If
                '        Next

                '        MessageBox.Show("No se han seleccionado Expedientes para exportar", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        Me.ExpedientesDataGridView.Focus()
                '        Return False

                '    End If
            Else
                Return True

            End If
            Return False
        End Function

        Private Function validarFechaProceso() As Boolean
            'Dim FechaInicial = New DateTime(FechaProcesoDateTimePicker.Value.Year, FechaProcesoDateTimePicker.Value.Month, FechaProcesoDateTimePicker.Value.Day)
            'Dim FechaFinal = New DateTime(FechaProcesoFinalDateTimePicker.Value.Year, FechaProcesoFinalDateTimePicker.Value.Month, FechaProcesoFinalDateTimePicker.Value.Day)

            'If (FechaInicial > FechaFinal) Then
            '    MessageBox.Show("La fecha de Proceso final no puede ser inferior a la fecha de Proceso inicial", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)

            '    Return False

            'End If

            Return True

        End Function

        Private Function getFechaInicio() As Integer
            '  Return CInt(Me.FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"))
        End Function

        Private Function getFechaFinal() As Integer
            'Return CInt(Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyyMMdd"))
        End Function

        Private Function getOTs() As DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try

                dbmImaging = New DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Me._plugin.Manager.Sesion.Usuario.id)

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

        Private Function getExpedientes() As DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                'Return dbmImaging.SchemaProcess.PA_Exportacion_Expediente.DBExecute(CDate(Me.FechaProcesoDateTimePicker.Value), CDate(Me.FechaProcesoFinalDateTimePicker.Value), Program.ImagingGlobal.Entidad, Program.ImagingGlobal.Proyecto)
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.CTA_Exportacion_ExpedienteDataTable()
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

        Private Function Nombre_Imagen_Agrupada_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal ngrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String
            Dim Nombre_Imagen As String = String.Empty

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                Nombre_Imagen = dbmImaging.SchemaProcess.PA_GetNombreArchivo.DBExecute(nidExpediente, nidFolder, DBNull.Value, DBNull.Value, ngrupo)

                If Nombre_Imagen = String.Empty Then
                    nValido = False
                    nMsgError = "No se encontró nombre de imagen para el expediente: " & nidExpediente.ToString() & ", fk_Documento: " & 0.ToString()
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