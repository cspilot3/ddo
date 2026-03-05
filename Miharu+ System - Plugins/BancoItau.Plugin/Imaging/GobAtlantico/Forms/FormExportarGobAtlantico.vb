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
Imports DMB = Miharu.Desktop.Controls.DesktopMessageBox.DesktopMessageBoxControl


Namespace Imaging.GobAtlantico.Forms
    Public Class FormExportarGobAtlantico
        Inherits FormBase

#Region " Declaraciones "
        Public _plugin As Plugin
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

        Private Sub BuscarFechaButton_Click(sender As System.Object, e As EventArgs) Handles BuscarFechaButton.Click

            If validarFechaProceso() Then
                CargarOTs()
            End If

        End Sub

        Private Sub BuscarCarpetaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarCarpetaButton.Click
            Dim Selector As New FolderBrowserDialog()

            Selector.SelectedPath = CarpetaSalidaTextBox.Text
            If (Selector.ShowDialog() = DialogResult.OK) Then
                Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
            End If
        End Sub

        Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click
            ExportarFiles()
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

        Private Sub ExportarFiles()
            If (Validar()) Then

                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                ArrayNotificacionTapas = New ArrayList
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Me._plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                FileNamesCons = New List(Of String)
                'Dim ExportDataFilePDFA = getOT(OTRow.id_OT, UMVPlugin.Imaging_EntidadId, UMVPlugin.Imaging_UMV_ProyectoId)
                'Dim ExportDataFilePDFACSV = getOTCSV(OTRow.id_OT, UMVPlugin.Imaging_EntidadId, UMVPlugin.Imaging_UMV_ProyectoId)
                Dim dtItemFile As DataTable
                Dim ExportDataFilePDFA = dbmImaging.SchemaProcess.PA_Exportacion_Data_File.DBExecute(Plugin.Imaging_EntidadId, Plugin.Imaging_BancoItau_ProyectoId)
                Dim ExportDataFilePDFACSV = dbmImaging.SchemaProcess.PA_Exportacion_Data_File_CSV.DBExecute(Plugin.Imaging_EntidadId, Plugin.Imaging_BancoItau_ProyectoId)

                Dim Proceso() As Process

                Proceso = Process.GetProcessesByName("Miharu.Desktop.vshost.exe")
                For Each pro In Proceso
                    pro.PriorityClass = ProcessPriorityClass.BelowNormal
                Next



                Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                Dim Progreso As Integer = 0
                ProgressForm.SetProceso("Exportar")
                ProgressForm.SetAccion("Obteniendo imágenes...")
                ProgressForm.SetProgreso(0)
                ProgressForm.SetMaxValue(ExportDataFilePDFA.Rows.Count())
                Application.DoEvents()
                contadorFile = 1

                Dim Exp As List(Of Object) = Nothing
                Exp = (From a In ExportDataFilePDFA Group a By GroupDt = a.Field(Of Int64)("fk_Llave") Into Group Select Group.Select(Function(x) x("fk_Llave")).First()).ToList()
                Dim FolderImg As String
                FolderImg = (OutputFolder & contadorFile.ToString("0000")).TrimEnd("\"c) & "\"

                contadorFile += contadorFile

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

                        Try

                            Dim itemOT = From o In ExportDataFilePDFA
                            Where o.ItemArray(2) = ItemFile
                            Select o.ItemArray(1)

                            idOT = itemOT.FirstOrDefault()

                            Dim servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(Convert.ToInt32(itemOT.FirstOrDefault()))(0).ToCTA_ServidorSimpleType()
                            Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Me._plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad,
                                                                                                                                             Me._plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede,
                                                                                                                                             Me._plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                            manager = New FileProviderManager(servidor, centro, dbmImaging, Program.Sesion.Usuario.id)
                            manager.Connect()
                            Dim nCedula = From a In ExportDataFilePDFA
                                          Where a.ItemArray(1) = idOT
                                          Select a.ItemArray(2)


                            Cedula = nCedula.FirstOrDefault().ToString()

                            ' Crear el directorio de las imágenes
                            Dim FileFolderName = FolderImg & "imagenes" & "\" & nCedula.FirstOrDefault() & "\"

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

                            Dim workerThread As New Thread(AddressOf ProcesoHilos)
                            workerThread.Start(ArraListParametersTapas)
                            If (ExportDataFilePDFA IsNot Nothing) Then ExportDataFilePDFA.Dispose()
                            If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                            ClearMemory()
                        Catch ex As Exception
                            If (manager IsNot Nothing) Then manager.Disconnect()
                            Throw (ex)
                        End Try

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

                            Dim Form = From a In ExportDataFilePDFACSV
                                       Where a.ItemArray(0) = "HL" And a.ItemArray(3) = Cedula
                                       Select a

                            Dim FormNM = From a In ExportDataFilePDFACSV
                                      Where a.ItemArray(0) = "NM" And a.ItemArray(3) = Cedula
                                      Select a

                            Dim FormPF = From a In ExportDataFilePDFACSV
                                     Where a.ItemArray(0) = "PF" And a.ItemArray(3) = Cedula
                                     Select a

                            If Form.ToList().Count() > 0 Then
                                For Each items In Form.ToList
                                    If DirectCast(items, System.Data.DataRow).ItemArray(i) = "HL" Then
                                        For a = 0 To ExportDataFilePDFACSV.Columns.Count() - 1
                                            FormatoCSV = "HL"

                                            lineaActual &= (String.Format("{0};", DirectCast(items, System.Data.DataRow).ItemArray(a)))
                                        Next

                                    End If
                                Next
                                'Quitar coma final
                                lineasCSV.AppendLine(lineaActual.Substring(0, lineaActual.Length))
                                'Guardar datos variable temporal a fichero CSV

                                Dim Sys As New System.IO.StreamWriter(File.Open(FolderImg & FormatoCSV & "_" & Cedula & "_exp.csv", FileMode.Create), utf8WithoutBom)
                                Sys.WriteLine(lineasCSV.ToString)
                                Sys.Flush()
                                Sys.Dispose()
                            End If

                            If FormNM.ToList().Count() > 0 Then
                                For Each itemsNM In FormNM.ToList
                                    i = 0

                                    If DirectCast(itemsNM, System.Data.DataRow).ItemArray(i) = "NM" Then
                                        For i = 0 To ExportDataFilePDFACSV.Columns.Count() - 1
                                            FormatoCSV = "NM"
                                            lineaActual1 &= (String.Format("{0};", DirectCast(itemsNM, System.Data.DataRow).ItemArray(i)))
                                        Next
                                    End If
                                Next

                                'Quitar coma final
                                lineasCSV1.AppendLine(lineaActual1.Substring(0, lineaActual1.Length))
                                'Guardar datos variable temporal a fichero CSV
                                Dim Sys1 As New System.IO.StreamWriter(File.Open(FolderImg & FormatoCSV & "_" & Cedula & "_exp.csv", FileMode.Create), utf8WithoutBom)
                                Sys1.WriteLine(lineasCSV1.ToString)
                                Sys1.Flush()
                                Sys1.Dispose()
                            End If

                            If FormPF.ToList().Count() > 0 Then
                                For Each itemsPF In FormPF.ToList
                                    i = 0

                                    If DirectCast(itemsPF, System.Data.DataRow).ItemArray(i) = "PF" Then
                                        For i = 0 To ExportDataFilePDFACSV.Columns.Count() - 1
                                            FormatoCSV = "PF"
                                            lineaActual2 &= (String.Format("{0};", DirectCast(itemsPF, System.Data.DataRow).ItemArray(i)))
                                        Next

                                    End If
                                Next
                                'Quitar coma final
                                lineasCSV2.AppendLine(lineaActual2.Substring(0, lineaActual2.Length))
                                'Guardar datos variable temporal a fichero CSV
                                Dim Sys2 As New System.IO.StreamWriter(File.Open(FolderImg & FormatoCSV & "_" & Cedula & "_exp.csv", FileMode.Create), utf8WithoutBom)
                                Sys2.WriteLine(lineasCSV2.ToString)
                                Sys2.Flush()
                                Sys2.Dispose()
                            End If
                        Catch ex As Exception

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
                            Dim dt As DataTable
                            Dim dt1 As DataTable
                            Dim dt2 As DataTable



                            Dim Form = From a In dtItemFile
                                       Where a.ItemArray(0) = "HL"
                                       Select a

                            Dim FormNM = From a In dtItemFile
                                      Where a.ItemArray(0) = "NM"
                                      Select a

                            Dim FormPF = From a In dtItemFile
                                     Where a.ItemArray(0) = "PF"
                                     Select a

                            dt = New DataTable()

                            dt.Columns.Add("Fecha_Anexo")
                            dt.Columns.Add("Desc_Documento")
                            dt.Columns.Add("tamano_anexo")
                            dt.Columns.Add("cod_tipologia")
                            dt.Columns.Add("Nombre_Archivo")
                            dt.Columns.Add("Folios")
                            dt.Columns.Add("Id")
                            dt.Columns.Add("imagen_principal")

                            If Form.ToList().Count() > 0 Then

                                For Each items In Form.ToList
                                    i = 0
                                    If DirectCast(items, System.Data.DataRow).ItemArray(i) = "HL" Then
                                        Dim row As DataRow = dt.NewRow()
                                        Dim fecha As Date = Date.Now
                                        row("Fecha_Anexo") = fecha
                                        row("Desc_Documento") = DirectCast(items, System.Data.DataRow).ItemArray(10)
                                        row("tamano_anexo") = "0"
                                        row("cod_tipologia") = DirectCast(items, System.Data.DataRow).ItemArray(18)
                                        row("Nombre_Archivo") = DirectCast(items, System.Data.DataRow).ItemArray(15)
                                        row("Nombre_Archivo") = DirectCast(items, System.Data.DataRow).ItemArray(13)
                                        row("Id") = DirectCast(items, System.Data.DataRow).ItemArray(3)
                                        row("imagen_principal") = "0"
                                        For i = 0 To dt.Columns.Count() - 1
                                            FormatoCSV = "HL"
                                            lineaActual &= (String.Format("{0};", DirectCast(row, System.Data.DataRow).ItemArray(i)))
                                        Next

                                    End If
                                Next
                                'Quitar coma final
                                lineasCSV.AppendLine(lineaActual.Substring(0, lineaActual.Length))
                                'Guardar datos variable temporal a fichero CSV
                                Dim Sys As New System.IO.StreamWriter(File.Open(FolderImg & "FILES_" & FormatoCSV & "_" & Cedula & ".csv", FileMode.Create), utf8WithoutBom)
                                Sys.WriteLine(lineasCSV.ToString)
                                Sys.Flush()
                                Sys.Dispose()
                            End If

                            If FormNM.ToList().Count() > 0 Then
                                dt1 = New DataTable()

                                dt1.Columns.Add("Fecha_Anexo")
                                dt1.Columns.Add("Desc_Documento")
                                dt1.Columns.Add("tamano_anexo")
                                dt1.Columns.Add("cod_tipologia")
                                dt1.Columns.Add("Nombre_Archivo")
                                dt1.Columns.Add("Folios")
                                dt1.Columns.Add("Id")
                                dt1.Columns.Add("imagen_principal")

                                For Each itemsNM In FormNM.ToList

                                    i = 0
                                    If DirectCast(itemsNM, System.Data.DataRow).ItemArray(i) = "NM" Then
                                        Dim rowNM As DataRow = dt1.NewRow()
                                        Dim fecha As Date = Date.Now
                                        rowNM("Fecha_Anexo") = fecha
                                        rowNM("Desc_Documento") = DirectCast(itemsNM, System.Data.DataRow).ItemArray(10)
                                        rowNM("tamano_anexo") = "0"
                                        rowNM("cod_tipologia") = DirectCast(itemsNM, System.Data.DataRow).ItemArray(18)
                                        rowNM("Nombre_Archivo") = DirectCast(itemsNM, System.Data.DataRow).ItemArray(15)
                                        rowNM("Nombre_Archivo") = DirectCast(itemsNM, System.Data.DataRow).ItemArray(13)
                                        rowNM("Id") = DirectCast(itemsNM, System.Data.DataRow).ItemArray(3)
                                        rowNM("imagen_principal") = "0"
                                        For i = 0 To dt1.Columns.Count() - 1
                                            FormatoCSV = "NM"
                                            lineaActual1 &= (String.Format("{0};", DirectCast(rowNM, System.Data.DataRow).ItemArray(i)))
                                        Next
                                    End If
                                Next
                                'Quitar coma final
                                lineasCSV1.AppendLine(lineaActual1.Substring(0, lineaActual1.Length))
                                'Guardar datos variable temporal a fichero CSV
                                Dim Sys1 As New System.IO.StreamWriter(File.Open(FolderImg & "FILES_" & FormatoCSV & "_" & Cedula & ".csv", FileMode.Create), utf8WithoutBom)
                                Sys1.WriteLine(lineasCSV1.ToString)
                                Sys1.Flush()
                                Sys1.Dispose()
                            End If

                            If FormPF.ToList().Count() > 0 Then

                                dt2 = New DataTable()

                                dt2.Columns.Add("Fecha_Anexo")
                                dt2.Columns.Add("Desc_Documento")
                                dt2.Columns.Add("tamano_anexo")
                                dt2.Columns.Add("cod_tipologia")
                                dt2.Columns.Add("Nombre_Archivo")
                                dt2.Columns.Add("Folios")
                                dt2.Columns.Add("Id")
                                dt2.Columns.Add("imagen_principal")

                                For Each itemsPF In FormPF.ToList
                                    i = 0
                                    If DirectCast(itemsPF, System.Data.DataRow).ItemArray(i) = "PF" Then
                                        Dim rowPF As DataRow = dt2.NewRow()
                                        Dim fecha As Date = Date.Now
                                        rowPF("Fecha_Anexo") = fecha
                                        rowPF("Desc_Documento") = DirectCast(itemsPF, System.Data.DataRow).ItemArray(10)
                                        rowPF("tamano_anexo") = "0"
                                        rowPF("cod_tipologia") = DirectCast(itemsPF, System.Data.DataRow).ItemArray(18)
                                        rowPF("Nombre_Archivo") = DirectCast(itemsPF, System.Data.DataRow).ItemArray(15)
                                        rowPF("Nombre_Archivo") = DirectCast(itemsPF, System.Data.DataRow).ItemArray(13)
                                        rowPF("Id") = DirectCast(itemsPF, System.Data.DataRow).ItemArray(3)
                                        rowPF("imagen_principal") = "0"
                                        For i = 0 To dt2.Columns.Count() - 1
                                            FormatoCSV = "PF"
                                            lineaActual2 &= (String.Format("{0};", DirectCast(itemsPF, System.Data.DataRow).ItemArray(i)))
                                        Next

                                    End If
                                Next
                                'Quitar coma final
                                lineasCSV2.AppendLine(lineaActual2.Substring(0, lineaActual2.Length))
                                'Guardar datos variable temporal a fichero CSV
                                Dim Sys2 As New System.IO.StreamWriter(File.Open(FolderImg & "FILES_" & FormatoCSV & "_" & Cedula & ".csv", FileMode.Create), utf8WithoutBom)
                                Sys2.WriteLine(lineasCSV2.ToString)
                                Sys2.Flush()
                                Sys2.Dispose()
                            End If
                        Catch ex As Exception

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

                Else
                    MessageBox.Show("Operación cancelada por Usuario", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                MessageBox.Show("La información se exportó con éxito", Me._plugin.GetName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                manager = New FileProviderManager(nservidor, ncentro, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()
                SyncLock BloqueoConcurrenciaTapas
                    For Each itemFileRow In nItemFile.Rows
                        ExportarImagen(manager, itemFileRow, nCompresion, npathFileName, npathFileName, ExportDataFilePDFA, nPathTemp)
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

        Private Function getOT(OTRow As Int32, EntidadId As Int32, ProyectoId As Int32) As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.PA_Exportacion_Data_FileStoreProcedure = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "Select DISTINCT " +
                         "PS.Valor_Parametro_Sistema as Sigla_Serie, " +
                         "OT.id_OT, " +
                         "convert(bigint, UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, DaAso.id_File_Data_Asociada, Fil.fk_Documento, 4))) AS fk_Llave, " +
                         "convert(bigint, Precinto.Precinto) as Precinto, " +
                         "CLI.Etiqueta_Campo_Lista_Item AS Serie, " +
                         "FIl.fk_Expediente, " +
                         "Fil.fk_Folder, " +
                         "Fil.id_File, " +
                         "ImagingFile.id_Version, " +
                         "ImagingFile.File_Unique_Identifier, " +
                         "Doc.Nombre_Documento, " +
                         "Folder.fk_Entidad_Servidor, " +
                         "Folder.fk_Servidor, " +
                         "Fil.Folios_File, Doc.id_Documento, " +
                         "'' AS Nombre_Imagen_File, " +
                         "[DB_Miharu.Integration].[dbo].[Fn_Formato_Fecha](UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, DaAso.id_File_Data_Asociada, Fil.fk_Documento, 1)),'yyyy/mm/dd') AS Fecha " +
                         ",UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, DaAso.id_File_Data_Asociada, Fil.fk_Documento, 5)) AS Nombres " +
                         "INTO #DetalleRegistros " +
                         "FROM [DB_Miharu.Imaging_Core].Process.TBL_OT OT " +
                         "INNER JOIN [DB_Miharu.Imaging_Core].[Process].[TBL_Precinto] Precinto " +
                         "ON OT.id_OT = Precinto.fk_OT " +
                         "INNER JOIN [DB_Miharu.Imaging_Core].Process.TBL_Precinto_Data PrecintoData " +
                         "ON Precinto.fk_OT = PrecintoData.fk_OT " +
                         "AND Precinto.id_precinto = PrecintoData.fk_precinto " +
                         "INNER JOIN [DB_Miharu.Imaging_Core].Config.TBL_Precinto_Campo PC " +
                         "ON OT.fk_Entidad = PC.fk_entidad " +
                         "AND PrecintoData.fk_Campo = PC.id_Campo " +
                         "AND PC.Nombre_Campo = 'Serie Documental' " +
                         "INNER JOIN [DB_Miharu.Core].[Config].[TBL_Campo_Lista] CL " +
                         "ON Cl.fk_Entidad = OT.fk_Entidad " +
                         "AND PC.fk_Campo_Lista = CL.id_Campo_Lista " +
                         "INNER JOIN [DB_Miharu.Core].[Config].[TBL_Campo_Lista_Item] CLI " +
                         "ON CL.fk_Entidad = CLI.fk_Entidad " +
                         "AND CL.id_Campo_Lista = CLI.fk_Campo_Lista " +
                         "AND cast(PrecintoData.Data_Campo as VARCHAR(100)) = CLI.Valor_campo_lista_item " +
                         "INNER JOIN [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema PS " +
                         "ON PS.Nombre_Parametro_Sistema = CLI.Valor_campo_lista_item collate Latin1_General_CI_AS " +
                         "INNER JOIN [DB_Miharu.Imaging_Core].Process.TBL_contenedor C " +
                         "On C.fk_OT = Precinto.fk_OT " +
                         "AND C.fk_Precinto = Precinto.id_Precinto " +
                         "INNER JOIN [DB_Miharu.Core].Imaging.TBL_Folder Folder " +
                         "ON C.fk_Cargue = Folder.fk_Cargue " +
                         "AND C.fk_Paquete = Folder.fk_Cargue_Paquete " +
                         "INNER JOIN [DB_Miharu.Core].Process.TBL_File Fil " +
                         "ON Fil.fk_Expediente = Folder.fk_Expediente " +
                         "AND Fil.fk_Folder = Folder.fk_Folder " +
                         "and fIL.fk_Documento NOT IN (3692, 3734, 3698, 3737) " +
                         "INNER JOIN [DB_Miharu.Core].Imaging.TBL_File ImagingFile " +
                         "ON ImagingFile.fk_Expediente = Fil.fk_Expediente " +
                         "AND ImagingFile.fk_Folder = Fil.fk_Folder " +
                         "AND ImagingFile.fk_File = Fil.id_File " +
                         "INNER JOIN [DB_Miharu.Core].Config.TBL_Documento Doc " +
                         "ON Doc.id_Documento = Fil.fk_Documento " +
                         "LEFT OUTER JOIN [DB_Miharu.Core].Process.TBL_File_Data_Asociada DaAso " +
                                  "ON     Fil.fk_Expediente = DaAso.fk_Expediente " +
                                  "AND Fil.fk_Folder = DaAso.fk_Folder " +
                                  "AND Fil.id_File = DaAso.fk_File " +
                         "WHERE OT.id_OT = @id_OT " +
                         "SELECT * " +
                         "FROM #DetalleRegistros " +
                         "DROP TABLE #DetalleRegistros"

                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@id_OT", OTRow),
                   New SqlParameter("@fk_Entidad", EntidadId),
                   New SqlParameter("@fk_Proyecto", ProyectoId)
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

        Private Function getOTCSV(OTRow As Int32, EntidadId As Int32, ProyectoId As Int32) As DataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim ListProyectos As New List(Of String)
            Dim dt As DataTable = Nothing
            Dim datatable As DBImaging.SchemaProcess.PA_Exportacion_Data_File_CSVStoreProcedure = Nothing
            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(_plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                Dim conn As New SqlConnection
                conn.ConnectionString = (dbmImaging.DataBase.ConnectionString)
                Dim sqlquery As String = "Select DISTINCT " +
                         "PS.Valor_Parametro_Sistema as Sigla_Serie, " +
                         "OT.id_OT, " +
                         "convert(bigint, UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, DaAso.id_File_Data_Asociada, Fil.fk_Documento, 4))) AS fk_Llave, " +
                         "convert(bigint, Precinto.Precinto) as Precinto, " +
                         "CLI.Etiqueta_Campo_Lista_Item AS Serie, " +
                         "FIl.fk_Expediente, " +
                         "Fil.fk_Folder, " +
                         "Fil.id_File, " +
                         "ImagingFile.id_Version, " +
                         "ImagingFile.File_Unique_Identifier, " +
                         "Doc.Nombre_Documento, " +
                         "Folder.fk_Entidad_Servidor, " +
                         "Folder.fk_Servidor, " +
                         "Fil.Folios_File, Doc.id_Documento, " +
                         "'' AS Nombre_Imagen_File, " +
                         "[DB_Miharu.Integration].[dbo].[Fn_Formato_Fecha](UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, DaAso.id_File_Data_Asociada, Fil.fk_Documento, 1)),'yyyy/mm/dd') AS Fecha " +
                         ",UPPER([DB_Miharu.Core].[Process].Fn_getDatos(Fil.fk_Expediente, Fil.fk_Folder, Fil.id_File, DaAso.id_File_Data_Asociada, Fil.fk_Documento, 5)) AS Nombres " +
                         "INTO #DetalleRegistros " +
                         "FROM [DB_Miharu.Imaging_Core].Process.TBL_OT OT " +
                         "INNER JOIN [DB_Miharu.Imaging_Core].[Process].[TBL_Precinto] Precinto " +
                         "ON OT.id_OT = Precinto.fk_OT " +
                         "INNER JOIN [DB_Miharu.Imaging_Core].Process.TBL_Precinto_Data PrecintoData " +
                         "ON Precinto.fk_OT = PrecintoData.fk_OT " +
                         "AND Precinto.id_precinto = PrecintoData.fk_precinto " +
                         "INNER JOIN [DB_Miharu.Imaging_Core].Config.TBL_Precinto_Campo PC " +
                         "ON OT.fk_Entidad = PC.fk_entidad " +
                         "AND PrecintoData.fk_Campo = PC.id_Campo " +
                         "AND PC.Nombre_Campo = 'Serie Documental' " +
                         "INNER JOIN [DB_Miharu.Core].[Config].[TBL_Campo_Lista] CL " +
                         "ON Cl.fk_Entidad = OT.fk_Entidad " +
                         "AND PC.fk_Campo_Lista = CL.id_Campo_Lista " +
                         "INNER JOIN [DB_Miharu.Core].[Config].[TBL_Campo_Lista_Item] CLI " +
                         "ON CL.fk_Entidad = CLI.fk_Entidad " +
                         "AND CL.id_Campo_Lista = CLI.fk_Campo_Lista " +
                         "AND cast(PrecintoData.Data_Campo as VARCHAR(100)) = CLI.Valor_campo_lista_item " +
                         "INNER JOIN [DB_Miharu.Imaging_Core].Config.TBL_Parametro_Sistema PS " +
                         "ON PS.Nombre_Parametro_Sistema = CLI.Valor_campo_lista_item collate Latin1_General_CI_AS " +
                         "INNER JOIN [DB_Miharu.Imaging_Core].Process.TBL_contenedor C " +
                         "On C.fk_OT = Precinto.fk_OT " +
                         "AND C.fk_Precinto = Precinto.id_Precinto " +
                         "INNER JOIN [DB_Miharu.Core].Imaging.TBL_Folder Folder " +
                         "ON C.fk_Cargue = Folder.fk_Cargue " +
                         "AND C.fk_Paquete = Folder.fk_Cargue_Paquete " +
                         "INNER JOIN [DB_Miharu.Core].Process.TBL_File Fil " +
                         "ON Fil.fk_Expediente = Folder.fk_Expediente " +
                         "AND Fil.fk_Folder = Folder.fk_Folder " +
                         "and fIL.fk_Documento NOT In (3692, 3734) " +
                         "INNER JOIN [DB_Miharu.Core].Imaging.TBL_File ImagingFile " +
                         "ON ImagingFile.fk_Expediente = Fil.fk_Expediente " +
                         "AND ImagingFile.fk_Folder = Fil.fk_Folder " +
                         "AND ImagingFile.fk_File = Fil.id_File " +
                         "INNER JOIN [DB_Miharu.Core].Config.TBL_Documento Doc " +
                         "ON Doc.id_Documento = Fil.fk_Documento " +
                         "LEFT OUTER JOIN [DB_Miharu.Core].Process.TBL_File_Data_Asociada DaAso " +
                                  "ON     Fil.fk_Expediente = DaAso.fk_Expediente " +
                                  "AND Fil.fk_Folder = DaAso.fk_Folder " +
                                  "AND Fil.id_File = DaAso.fk_File " +
                         "WHERE OT.id_OT = @id_OT " +
                        "CREATE TABLE #Registros (id_OT BIGINT, Serie VARCHAR(100), Sigla_Serie VARCHAR(2), fk_Expediente BIGINT, fk_Folder INT, Cedula VARCHAR(30), Nombres VARCHAR(300), Fecha_Inicial VARCHAR(10), Fecha_Final VARCHAR(10)) " +
                        "INSERT INTO #Registros " +
                        "SELECT id_OT, Serie, Sigla_Serie, fk_Expediente, fk_Folder, fk_Llave, Nombres, '' AS Fecha_Inicial, '' AS Fecha_Final " +
                        "FROM #DetalleRegistros " +
                        "WHERE id_File = 1 " +
                        "UPDATE #Registros " +
                        "SET Fecha_Inicial = Data.FI " +
                        ",Fecha_Final = Data.FF " +
                        "FROM #Registros R " +
                        "INNER JOIN ( " +
                        "                           SELECT fk_Expediente, MIN(Fecha) AS FI, MAX(Fecha) FF " +
                        "                           FROM #DetalleRegistros " +
                        "                           GROUP BY fk_Expediente " +
                        "                    ) Data " +
                        "ON Data.fk_Expediente = R.fk_Expediente " +
                        "SELECT sigla_serie, Serie, Nombres + ' - ' + Cedula AS Asunto, Cedula AS [Numero de cedula], Nombres as [Nombres y Apellidos], Fecha_Inicial, Fecha_Final, 0 as [Numero de Folios], '' AS [Tipo de trabajador], '' AS Estatus " +
                        "FROM #Registros " +
                        "ORDER BY sigla_serie " +
                        "DROP TABLE #Registros " +
                        "DROP TABLE #DetalleRegistros "

                Dim SqlParameter = New SqlParameter() _
               {
                   New SqlParameter("@id_OT", OTRow),
                   New SqlParameter("@fk_Entidad", EntidadId),
                   New SqlParameter("@fk_Proyecto", ProyectoId)
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

        Private Sub ExportarImagen(nManager As FileProviderManager, ByVal ItemFile As DataRow, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, nFolderName As String, nExportDataFilePDFA As DataTable, nPathTemp As String)
            Dim Folios = nManager.GetFolios(CLng(ItemFile.Item("fk_Expediente").ToString()), CShort(ItemFile.Item("fk_Folder").ToString()), CShort(ItemFile.Item("id_File").ToString()), CShort(ItemFile.Item("id_Version").ToString()))

            Dim FileNames As New List(Of String)
            Dim FileName As String = Nothing
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
                    FileName = Plugin.AppPath & Plugin.TempPath & Guid.NewGuid().ToString() & "_" & ItemFile.Item("fk_Llave").ToString() & "_" & ItemFile.Item("id_File").ToString() & "_" & ItemFile.Item("Folios_File").ToString() & "_" & contadorbmp & ".tif"
                    FileNames.Add(FileName)
                    FileNamesCons.Add(FileName)

                    Using fs = New FileStream(FileName, FileMode.Create)
                        Try
                            fs.Write(Imagen, 0, Imagen.Length)
                            fs.Close()
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

                        ExtensionAux = IIf(formatoAux = ImageManager.EnumFormat.Pdf, ".pdf", ".tif").ToString

                        If ItemFile.Item("Sigla_Serie").ToString() = "HL" Then
                            If ((Valido = True) And (FileNameAux = String.Empty)) Then
                                contador += 1
                                FileNameAux = ItemFile.Item("Sigla_Serie").ToString() & "_" & ItemFile.Item("fk_Llave").ToString() & "_" & contador.ToString("0000")
                                nFileName = Plugin.AppPath & Plugin.TempPath & FileNameAux & ExtensionAux
                            ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                                ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, "tif", ExtensionAux))
                                nFileName = Plugin.AppPath & Plugin.TempPath & FileNameAux & ExtensionAux
                            ElseIf Valido = False Then
                                Throw New Exception(MsgError)
                            End If
                        ElseIf ItemFile.Item("Sigla_Serie").ToString() = "NM" Or ItemFile.Item("Sigla_Serie").ToString() = "PF" Then
                            contador += 1
                            Dim MesAnio As String
                            MesAnio = ItemFile.Item("Precinto").ToString()
                            Dim anio As String = MesAnio.Substring(0, 4)
                            Dim mes As String = MesAnio.Substring(4, 2)

                            If ((Valido = True) And (FileNameAux = String.Empty)) Then

                                FileNameAux = ItemFile.Item("Sigla_Serie").ToString() & "_" & mes & "_" & anio & "_" & contador.ToString("0000")

                                nFileName = Plugin.AppPath & Plugin.TempPath & FileNameAux & ExtensionAux
                            ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                                ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, ".tif", ExtensionAux))

                                nFileName = Plugin.AppPath & Plugin.TempPath & FileNameAux & ExtensionAux
                            ElseIf Valido = False Then
                                ' EscribeLog(Throw New Exception(MsgError)
                            End If

                        End If
                        '--------------------------------------------k-----------------------------
                        Slyg.Tools.Imaging.ImageManager.SavePDFA(FileNames, nFileFolderName & FileNameAux & ExtensionAux, FileName, "", formatoAux, nCompresion, False, Image, True, nFileName)
                        '-------------------------------------------------------------------------

                        ItemFile.Item("Nombre_Imagen_File") = nFolderName & FileNameAux & ExtensionAux

                        InsertarLogCargue(ItemFile.Item("fk_Llave"), ItemFile.Item("fk_Expediente"), "True", Date.Now, Date.Now, ItemFile.Item("Nombre_Imagen_File"), ItemFile.Item("Token"), nFileFolderName, FileNames.Count())

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
            OTLabel.Text = "OTs"
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
            Dim FechaInicial = New DateTime(FechaProcesoDateTimePicker.Value.Year, FechaProcesoDateTimePicker.Value.Month, FechaProcesoDateTimePicker.Value.Day)
            Dim FechaFinal = New DateTime(FechaProcesoFinalDateTimePicker.Value.Year, FechaProcesoFinalDateTimePicker.Value.Month, FechaProcesoFinalDateTimePicker.Value.Day)

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
