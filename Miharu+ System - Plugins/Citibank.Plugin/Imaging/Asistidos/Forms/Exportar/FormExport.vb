Imports System.Windows.Forms
Imports System.IO
Imports Citibank.Plugin.Imaging.Asistidos
Imports Miharu.Desktop.Library.Config
Imports Slyg.Tools.Imaging
Imports System.Xml.Linq
Imports System.Linq
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Imaging
Imports System.Threading

#Region " Implemetación de hilos "

Public Class ProcesadorHilosExportar

    Dim NumHilos As Integer = 5
    Dim ListaHilos As New List(Of Thread)
    Public formulario As FormExport
    Public Shared objLock = New Object()

    Public Sub AgregarHilo(ByVal ArrayParameters As ArrayList)

        If TieneHiloslibres() = False Then
            Do
                Thread.Sleep(1000)

                If TieneHiloslibres() Then
                    Exit Do
                End If
            Loop
        End If

        Dim Threads As New System.Threading.Thread(AddressOf formulario.ProcesoHilosLlaves)
        Threads.Start(ArrayParameters)

        SyncLock objLock
            ListaHilos.Add(Threads)
        End SyncLock

    End Sub

    Public Function TieneHiloslibres() As Boolean
        SyncLock objLock
            Dim ListaHilosBorrar As New List(Of Thread)
            For Each hilo In ListaHilos
                If hilo.ThreadState = ThreadState.Stopped Then
                    ListaHilosBorrar.Add(hilo)
                End If
            Next
            For Each hilo In ListaHilosBorrar
                ListaHilos.Remove(hilo)
            Next
        End SyncLock
        If ListaHilos.Count < NumHilos Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function TerminoHilos() As Boolean
        SyncLock objLock
            For Each hilo In ListaHilos
                If hilo.ThreadState <> ThreadState.Stopped Then
                    Return False
                End If
            Next
        End SyncLock

        Return True
    End Function
End Class

#End Region

Public Class FormExport

#Region " Declaraciones "

    Private Usa_Exportacion_PDF As Boolean
    Private formatoAux As Slyg.Tools.Imaging.ImageManager.EnumFormat
    Private CompresionAux As Slyg.Tools.Imaging.ImageManager.EnumCompression
    Private formato As Slyg.Tools.Imaging.ImageManager.EnumFormat
    Dim compresion As Slyg.Tools.Imaging.ImageManager.EnumCompression

    Private _Plugin As AsistidosImagingPlugin

    Private ViewExpedientes As New DataView
    Private ExpedientesSeleccion As New DataTable
    Public Shared FileNamesCons As New List(Of String)
    Dim FolderNameOutput As String

    Private BloqueoConcurrencia As New Object

#End Region

#Region " Constructores "

    Public Sub New(ByVal nAsistidosImagingPlugin As AsistidosImagingPlugin)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _Plugin = nAsistidosImagingPlugin

        If Not (_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_Validos) Then

        End If

        'Usa_Exportacion_PDF = _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Exportacion_PDF
        formato = Utilities.GetEnumFormat(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
        compresion = Utilities.GetEnumCompression(CType(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida, DesktopConfig.FormatoImagenEnum))

        Load_FormatoCargue()
    End Sub

#End Region

#Region " Metodos "
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
            Dim progressForm As New Miharu.Tools.Progress.FormProgress
            FileNamesCons = New List(Of String)


            If LlenaExpedientesSeleccion() Then

                Dim Folders As Integer = 0
                Dim Files As Integer = 0
                Dim Folios As Integer = 0
                Dim Tamaño As Double = 0
                'Por cada OT se crea una carpeta

                dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

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

                        'Se crea tabla de fechas
                        Dim FechaRecaudos As New DataTable
                        FechaRecaudos = ViewExpedientes.ToTable(True, {"Fecha_Recaudo", "fk_OT"})

                        Dim ExportacionDataSetXML As New OffLineViewer.Library.xsdOffLineData
                        Dim FolderDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                        Dim FileDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
                        Dim FileDataDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                        Dim FileValidacionDataTableTXT = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable

                        If FechaRecaudos.Rows.Count > 0 Then
                            Dim Progreso As Integer = 0
                            progressForm.SetProceso("Exportar")
                            progressForm.SetAccion("Obteniendo imágenes...")
                            progressForm.SetProgreso(0)
                            progressForm.SetMaxValue(Files)
                            Application.DoEvents()

                            dbmCore = New DBCore.DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                            dbmCore.Connection_Open(1)
                            dbmImaging.Connection_Open(1)

                            '#If Not Debug Then
                            progressForm.Show()
                            '#End If

                            For Each rowFechaRecaudos As DataRow In FechaRecaudos.Rows

                                Dim ExpedientesOT As New DataTable
                                Dim ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(CInt(rowFechaRecaudos(1)))
                                Dim FolderDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable
                                'Dim FileDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_FilesDataTable
                                Dim FileDataTable = New DBIntegration.SchemaBcoCitibank.CTA_Exportacion_Expedientes_AsistidosDataTable()
                                Dim FileDataDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable
                                Dim FileValidacionDataTable = New DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable

                                'Filtar expedientes por OT
                                ViewExpedientes.RowFilter() = "Fecha_Recaudo = '" & rowFechaRecaudos(0).ToString & "'"
                                ExpedientesOT = ViewExpedientes.ToTable()

                                TablaDesdeTemporal(FileDataTable, ViewExpedientes.ToTable())

                                For Each rowExpediente As DataRow In ViewExpedientes.ToTable.Rows
                                    Dim tmpFolderDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Folders_Expediente.DBExecute(CInt(rowExpediente(0)))
                                    Dim tmpFileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files_Expediente.DBExecute(CInt(rowExpediente(0)))
                                    Dim tmpFileDataDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Data_Expediente.DBExecute(CInt(rowExpediente(0)))
                                    Dim tmpFileValidacionDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Validaciones_Expediente.DBExecute(CInt(rowExpediente(0)))

                                    TablaDesdeTemporal(FolderDataTable, tmpFolderDataTable)
                                    'TablaDesdeTemporal(FileDataTable, tmpFileDataTable)
                                    TablaDesdeTemporal(FileDataDataTable, tmpFileDataDataTable)
                                    TablaDesdeTemporal(FileValidacionDataTable, tmpFileValidacionDataTable)
                                Next

                                Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                                'Dim FilesDataViewExpedientes As New DataView(FileDataTable)

                                Dim FileFolderName As String = rowFechaRecaudos(0).ToString.Replace("/", "") & "\"

                                ' Crear el directorio de las imágenes
                                If Not Directory.Exists(OutputFolder & FileFolderName) Then
                                    Directory.CreateDirectory(OutputFolder & FileFolderName)
                                End If

                                Dim Compresion As ImageManager.EnumCompression

                                If (_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
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
                                        SyncLock BloqueoConcurrencia

                                            Dim servidor = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(CInt(rowFechaRecaudos(1)))(0).ToCTA_ServidorSimpleType()
                                            Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                            manager = New FileProviderManager(servidor, centro, dbmImaging, _Plugin.Manager.Sesion.Usuario.id)
                                            manager.Connect()
                                        End SyncLock

                                        Dim GruposD As List(Of Object) = Nothing
                                        GruposD = (From a In FileDataTable Group a By groupDt = a.Field(Of Integer)("fk_Grupo") Into Group Select Group.Select(Function(x) x("fk_Grupo")).First()).ToList()

                                        For Each grupo As Integer In GruposD
                                            Dim FilesbyGroup = FileDataTable.Select("fk_Grupo = " + grupo.ToString()).CopyToDataTable

                                            If grupo = 0 Then
                                                Dim FilesbyGroupDataView As New DataView(FilesbyGroup)

                                                ' Obtener los Files a transferir   
                                                FilesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor
                                                For Each ItemFile As DataRowView In FilesbyGroupDataView

                                                    If progressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                                    ' Enviar el archivo

                                                    Dim procesador As New ProcesadorHilosExportar
                                                    procesador.formulario = Me

                                                    Dim ArrayParameters As ArrayList = New ArrayList
                                                    ArrayParameters.Add(manager)
                                                    ArrayParameters.Add(ItemFile)
                                                    ArrayParameters.Add(Compresion)
                                                    ArrayParameters.Add(OutputFolder + FileFolderName)
                                                    ArrayParameters.Add(FileDataTable)
                                                    ArrayParameters.Add(FileFolderName)

                                                    procesador.AgregarHilo(ArrayParameters)

                                                    For Each Item As DBIntegration.SchemaBcoCitibank.CTA_Exportacion_Expedientes_AsistidosRow In FileDataTable
                                                        If Item.fk_Expediente = CLng(ItemFile.Item("fk_Expediente")) And Item.fk_Folder = CShort(ItemFile.Item("fk_Folder")) And Item.fk_File = CShort(ItemFile.Item("fk_File")) Then
                                                            Item.Nombre_Imagen_File = CStr(ItemFile.Item("Nombre_Imagen_File"))
                                                            Exit For
                                                        End If
                                                    Next

                                                    Progreso += 1
                                                    progressForm.SetProgreso(Progreso)
                                                    Application.DoEvents()
                                                Next
                                            Else
                                                Dim Expedientes As List(Of Object) = Nothing
                                                Expedientes = (From a In FilesbyGroup Group a By groupDt = a.Field(Of Long)("fk_Expediente") Into Group Select Group.Select(Function(x) x("fk_Expediente")).First()).ToList()

                                                For Each Expediente As Long In Expedientes
                                                    Dim FilesExpedientesbyGroup = FilesbyGroup.Select("fk_Grupo = " + grupo.ToString() + "AND fk_Expediente = " + Expediente.ToString()).CopyToDataTable
                                                    Dim FilesExpedientesbyGroupDataView As New DataView(FilesExpedientesbyGroup)

                                                    ' Obtener los Files a transferir   
                                                    FilesExpedientesbyGroupDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor

                                                    If progressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                                    ' Enviar el archivo
                                                    'ExportarImagenAgrupada(manager, FilesExpedientesbyGroupDataView, grupo, Expediente, 1, Compresion, OutputFolder & FileFolderName, FileFolderName)

                                                    'For Each Item As DBImaging.SchemaProcess.CTA_Exportacion_FilesRow In FileDataTable
                                                    '    If Item.fk_Expediente = Expediente And Item.fk_Grupo = grupo Then
                                                    '        Item.Nombre_Imagen_File = CStr(FilesExpedientesbyGroupDataView(0).Item("Nombre_Imagen_File"))
                                                    '    End If
                                                    'Next

                                                    Progreso += 1
                                                    'progressForm.SetProgreso(Progreso)
                                                    Application.DoEvents()
                                                Next

                                            End If
                                        Next

                                        SyncLock BloqueoConcurrencia
                                            manager.Disconnect()
                                        End SyncLock

                                    Catch ex As Exception
                                        SyncLock BloqueoConcurrencia
                                            If (manager IsNot Nothing) Then manager.Disconnect()
                                            Throw
                                        End SyncLock
                                    End Try
                                Next

                                '------------  Si proyecto tiene configurado Exportar_Unico_Archivo_TIFF  ------------------
                                If (CBool(_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF)) Then
                                    ExportAllFillesInTiff(Compresion, FolderNameOutput)
                                End If
                                '--------------------------------

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
                        progressForm.Close()
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
            'If CBool(row.Cells("Exportar").Value) Then 'Expedientes seleccionados
            Dim newRow As DataRow = ExpedientesSeleccion_.NewRow()

            For Each col As DataGridViewColumn In ExpedientesDataGridView.Columns
                newRow(col.Index) = row.Cells(col.Index).Value
            Next

            ExpedientesSeleccion_.Rows.Add(newRow)
            'End If
        Next

        ExpedientesSeleccion = ExpedientesSeleccion_

        If ExpedientesSeleccion_.Rows.Count > 0 Then
            ViewExpedientes.RowFilter = String.Empty
            ViewExpedientes = New DataView(ExpedientesSeleccion_)
            Return True
        End If

        Return False

    End Function

    Private Sub GenerarVisor(dbmCore As DBCore.DBCoreDataBaseManager, dbmImaging As DBImaging.DBImagingDataBaseManager, idOT As Integer, OutputFolder As String, FolderDataTable As DBImaging.SchemaProcess.CTA_Exportacion_FoldersDataTable, FileDataTable As DBIntegration.SchemaBcoCitibank.CTA_Exportacion_Expedientes_AsistidosDataTable, FileDataDataTable As DBImaging.SchemaProcess.CTA_Exportacion_DataDataTable, FileValidacionesDataTable As DBImaging.SchemaProcess.CTA_Exportacion_ValidacionesDataTable)
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
            Dim KeysDataTable = dbmCore.SchemaConfig.TBL_Proyecto_Llave.DBFindByfk_Entidadfk_Proyecto(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)

            Dim KeyName1 As String = ""
            If (KeysDataTable.Count > 0) Then KeyName1 = KeysDataTable(0).Nombre_Proyecto_Llave
            Dim KeyName2 As String = ""
            If (KeysDataTable.Count > 1) Then KeyName2 = KeysDataTable(1).Nombre_Proyecto_Llave
            Dim KeyName3 As String = ""
            If (KeysDataTable.Count > 2) Then KeyName3 = KeysDataTable(2).Nombre_Proyecto_Llave

            ' Crear Configuracion

            Dim Dtresultados As New DataTable
            Dim adapter As OleDb.OleDbDataAdapter


            Comando.CommandText = "SELECT * FROM TBL_Config WHERE id_Entidad = " & _Plugin.Manager.ImagingGlobal.Entidad &
                                    " AND id_Proyecto = " & _Plugin.Manager.ImagingGlobal.Proyecto & ";"

            adapter = New OleDb.OleDbDataAdapter(Comando)
            adapter.Fill(Dtresultados)

            If Dtresultados.Rows.Count = 0 Then
                Dim EntidadDataTable = dbmImaging.SchemaSecurity.CTA_Entidad.DBFindByid_Entidad(_Plugin.Manager.ImagingGlobal.Entidad)
                Comando.CommandText = " INSERT INTO TBL_Config (id_Entidad, Nombre_Entidad, id_Proyecto, Nombre_Proyecto, Key_1, Key_2, Key_3)" &
                                        "SELECT " & _Plugin.Manager.ImagingGlobal.Entidad &
                                        ", '" & EntidadDataTable(0).Nombre_Entidad & "', " &
                                        _Plugin.Manager.ImagingGlobal.Proyecto &
                                        ", '" & _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Nombre_Proyecto &
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

    Private Sub BorrarTemporal()
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

    Private Sub ExportarImagen(nManager As FileProviderManager, ByVal ItemFile As DataRowView, nCompresion As ImageManager.EnumCompression, nFileFolderName As String, nFolderName As String)
        Try
            SyncLock BloqueoConcurrencia
                Dim Folios As Short = nManager.GetFolios(CLng(ItemFile.Item("fk_Expediente")), CShort(ItemFile.Item("fk_Folder")), CShort(ItemFile.Item("fk_File")), CShort(ItemFile.Item("id_Version")))


                Dim FileNames As New List(Of String)
                Dim FileName As String = Nothing
                Dim FileNameAux As String = Nothing
                Dim ExtensionAux As String = String.Empty

                For folio As Short = 1 To Folios
                    Dim Imagen() As Byte = Nothing
                    Dim Thumbnail() As Byte = Nothing

                    nManager.GetFolio(CLng(ItemFile.Item("fk_Expediente")), CShort(ItemFile.Item("fk_Folder")), CShort(ItemFile.Item("fk_File")), CShort(ItemFile.Item("id_Version")), folio, Imagen, Thumbnail)

                    FileName = Program.AppPath + Program.TempPath + Guid.NewGuid().ToString() + _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    FileNames.Add(FileName)

                    FileNamesCons.Add(FileName)

                    Using fs = New FileStream(FileName, FileMode.Create)
                        fs.Write(Imagen, 0, Imagen.Length)
                        fs.Close()
                    End Using
                Next

                If Not (_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Exportar_Unico_Archivo_TIFF) Then

                    If (FileNames.Count > 0) Then
                        Dim Format As ImageManager.EnumFormat

                        Select Case _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
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

                        If (_Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Usa_Renombramiento_Imagen_Exportacion) Then
                            'FileNameAux = Nombre_Imagen_Exportar(CLng(ItemFile.Item("fk_Expediente")), CShort(ItemFile.Item("fk_Folder")), CShort(ItemFile.Item("fk_File")), CInt(ItemFile.Item("fk_Documento")), CInt(ItemFile.Item("fk_Grupo")), Valido, MsgError)
                            FileNameAux = ItemFile.Item("Nombre_Imagen_File").ToString

                            If (FileNameAux = String.Empty) Then
                                Valido = False
                                MsgError = "No se encontró nombre de imagen para el expediente: " + CLng(ItemFile.Item("fk_Expediente")).ToString + ", fk_Documento: " + Convert.ToInt64(ItemFile.Item("fk_Documento")).ToString
                            End If
                        End If

                        ExtensionAux = IIf(formatoAux = ImageManager.EnumFormat.Pdf, ".pdf", _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida).ToString

                        If ((Valido = True) And (FileNameAux = String.Empty)) Then
                            FileNameAux = ItemFile.Item("File_Unique_Identifier").ToString() & "_0001"
                            FileName = nFileFolderName & FileNameAux & ExtensionAux
                        ElseIf ((Valido = True) And (FileNameAux <> String.Empty)) Then
                            ExtensionAux = Convert.ToString(IIf(ExtensionAux Is String.Empty, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, ExtensionAux))
                            FileName = nFileFolderName & FileNameAux & ExtensionAux
                        ElseIf Valido = False Then
                            Throw New Exception(MsgError)
                        End If

                        ItemFile.Item("Nombre_Imagen_File") = nFolderName & FileNameAux & ExtensionAux

                        '-------------------------------------------------------------------------
                        SyncLock (BloqueoConcurrencia)
                            ImageManager.Save(FileNames, FileName, "", formatoAux, CompresionAux, False, Program.AppPath & Program.TempPath, True)
                        End SyncLock
                        '-------------------------------------------------------------------------
                    End If
                End If
            End SyncLock
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exportar imagen", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub ProcesoHilosLlaves(ByVal objectArray As Object)
        Dim ArraListParameters As ArrayList = objectArray

        Dim manager As FileProviderManager = ArraListParameters(0)
        Dim ItemFile As DataRowView = ArraListParameters(1)
        Dim nCompresion As ImageManager.EnumCompression = ArraListParameters(2)
        Dim nFileFolderName As String = ArraListParameters(3)
        Dim FileDataTable As DBIntegration.SchemaBcoCitibank.CTA_Exportacion_Expedientes_AsistidosDataTable = ArraListParameters(4)
        Dim nFolderName As String = ArraListParameters(5)

        Try
            ExportarImagen(manager, ItemFile, nCompresion, nFileFolderName, nFolderName)

        Catch ex As Exception
            'SyncLock BloqueoConcurrencia
            'EscribeLog(Me._StrArchivoLog, "Error ProcesoHilosExportacionImagenes: " + ex.Message, False, True)
            'End SyncLock
        End Try
    End Sub
#End Region

#Region " Eventos "
    Private Sub BuscarFechaButton_Click(sender As System.Object, e As System.EventArgs) Handles BuscarFechaButton.Click

    End Sub

    Private Sub BuscarCarpetaButton_Click(sender As System.Object, e As System.EventArgs) Handles BuscarCarpetaButton.Click
        Dim Selector As New FolderBrowserDialog()

        Selector.SelectedPath = CarpetaSalidaTextBox.Text
        If (Selector.ShowDialog() = DialogResult.OK) Then
            Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
        End If
    End Sub

    Private Sub ExportarButton_Click(sender As System.Object, e As System.EventArgs) Handles ExportarButton.Click
        If validarFechaProceso() Then
            If (CargarExpedientes()) Then
                ExportarExpedientes()
            End If
        End If
    End Sub

    Private Sub CancelarButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelarButton.Click
        Me.Close()
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
    Private Function CargarExpedientes() As Boolean
        Me.ExpedientesDataGridView.AutoGenerateColumns = False
        Me.ExpedientesDataGridView.DataSource = getExpedientes()
        Me.ExpedientesDataGridView.Refresh()

        If (Me.ExpedientesDataGridView.RowCount = 0) Then
            MessageBox.Show("No se encontraron Expedientes para el rango de fechas de proceso seleccionadas", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Return True
    End Function

    Private Function getExpedientes() As DataTable
        Dim dbIntegration As DBIntegration.DBIntegrationDataBaseManager = Nothing

        Try
            dbIntegration = New DBIntegration.DBIntegrationDataBaseManager(_Plugin.IntegrationConnectionString)
            dbIntegration.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

            Return dbIntegration.SchemaBcoCitibank.PA_Exportacion_Expediente_Asistidos.DBExecute(Me.FechaProcesoDateTimePicker.Value.ToString("yyyy/MM/dd"), Me.FechaProcesoFinalDateTimePicker.Value.ToString("yyyy/MM/dd"), _Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (dbIntegration IsNot Nothing) Then dbIntegration.Connection_Close()
        End Try

        Return New DataTable()
    End Function

    Private Function Validar() As Boolean

        If Not validarFechaProceso() Then
            Return False
        End If

        If (Not Directory.Exists(CarpetaSalidaTextBox.Text)) Then
            MessageBox.Show("El directorio no existe, Seleccione un directorio existente", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.CarpetaSalidaTextBox.Focus()
            Return False
        ElseIf (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 Or Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0) Then
            MessageBox.Show("La carpeta debe estar vacia para exportar los datos", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.CarpetaSalidaTextBox.Focus()
            Return False
        Else
            If (Me.ExpedientesDataGridView.Rows.Count = 0) Then
                MessageBox.Show("No se han seleccionado Expedientes para exportar", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.ExpedientesDataGridView.Focus()
                Return False
            Else
                For Each row As DataGridViewRow In ExpedientesDataGridView.Rows
                    Return True
                Next
            End If
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

    Private Function Nombre_Imagen_Exportar(ByVal nidExpediente As Long, ByVal nidFolder As Short, ByVal nidFile As Short, ByVal nFk_Documento As Integer, ByVal nGrupo As Integer, ByRef nValido As Boolean, ByRef nMsgError As String) As String
        Dim Nombre_Imagen As String = String.Empty

        Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

        Try
            dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

            dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

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
            dbmImaging = New DBImaging.DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)

            dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

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

#Region " Propiedades"

#End Region
End Class

