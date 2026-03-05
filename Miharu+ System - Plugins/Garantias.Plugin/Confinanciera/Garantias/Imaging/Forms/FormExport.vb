Imports System.IO
Imports System.Windows.Forms
Imports Miharu.Desktop.Library.Config
Imports Miharu.FileProvider.Library
Imports Miharu.Tools.Progress
Imports Slyg.Tools.Imaging
Imports Slyg.Tools

Namespace Confinanciera.Garantias.Imaging.Forms

    Public Class FormExport

#Region " Declaraciones "

        Private otDatatTable As DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable

#End Region

#Region " Propiedades "

        Public Property Plugin() As Plugin

#End Region

#Region " Eventos "

        Private Sub BuscarFechaButton_Click(sender As System.Object, e As EventArgs) Handles BuscarFechaButton.Click
            CargarOTs()
        End Sub

        Private Sub BuscarCarpetaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarCarpetaButton.Click
            Dim Selector As New FolderBrowserDialog()

            Selector.SelectedPath = CarpetaSalidaTextBox.Text
            If (Selector.ShowDialog() = DialogResult.OK) Then
                Me.CarpetaSalidaTextBox.Text = Selector.SelectedPath
            End If
        End Sub

        Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click
            Exportar()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

#End Region
        
#Region " Metodos "

        Private Sub CargarOTs()
            otDatatTable = getOTs()

            Me.OTDataGridView.AutoGenerateColumns = False
            Me.OTDataGridView.DataSource = otDatatTable
            Me.OTDataGridView.Refresh()

            If (Me.OTDataGridView.RowCount = 0) Then
                MessageBox.Show("No se encontraron OTs para la fecha de proceso seleccionada", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub Exportar()
            If (Validar()) Then
                Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim ProgressForm As New FormProgress
                Dim DataExportarDataTable As DBImaging.SchemaExport.CTA_ConfinancieraDataTable
                Dim idFechaProceso As Integer = Integer.Parse(FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"))
                
                Try
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                    dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)
                    
                    DataExportarDataTable = dbmImaging.SchemaExport.PA_Confinanciera.DBExecute(idFechaProceso)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try

                If (DataExportarDataTable.Rows.Count > 0) Then
                    Dim Respuesta As DialogResult

                    Respuesta = MessageBox.Show("Se encontró : " & DataExportarDataTable.Count & " imagenes pendientes por exportar" & vbCrLf & _
                                                "¿Desea Exportar esta informacion?", "Plugin", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If (Respuesta = DialogResult.Yes) Then
                        Try
                            Me.Enabled = False

                            dbmCore = New DBCore.DBCoreDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

                            dbmImaging.Connection_Open(1)
                            dbmCore.Connection_Open(1)

                            Dim firtsOTRow = CType(CType(Me.OTDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, DBImaging.SchemaProcess.CTA_Exportacion_OTRow)
                            Dim ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(firtsOTRow.id_OT)
                            
                            Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"
                            Dim FilesDataView As New DataView(DataExportarDataTable)
                            Dim Progreso As Integer = 0

#If Not Debug Then
                            ProgressForm.Show()
#End If

                            ProgressForm.SetProceso("Exportar")
                            ProgressForm.SetAccion("Obteniendo imágenes...")
                            ProgressForm.SetProgreso(0)
                            ProgressForm.SetMaxValue(FilesDataView.Count)

                            Application.DoEvents()

                            ' Crear el directorio de las imágenes
                            Directory.CreateDirectory(OutputFolder & "imagenes")
                            
                            Dim Compresion As ImageManager.EnumCompression

                            If (Plugin.Manager.ImagingGlobal.ProyectoImagingRow.id_Formato_Imagen_Salida = DesktopConfig.FormatoImagenEnum.TIFF_Bitonal) Then
                                Compresion = ImageManager.EnumCompression.CCITT4
                            Else
                                Compresion = ImageManager.EnumCompression.LZW
                            End If

                            For Each RowServidor In ServidoresDataTable
                                Dim manager As FileProviderManager = Nothing

                                Try
                                    Dim servidor = dbmImaging.SchemaCore.CTA_Servidor.DBFindByfk_Entidadid_Servidor(Plugin.Manager.DesktopGlobal.ServidorImagenRow.fk_Entidad, Plugin.Manager.DesktopGlobal.ServidorImagenRow.id_Servidor)(0).ToCTA_ServidorSimpleType()
                                    Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()

                                    manager = New FileProviderManager(servidor, centro, dbmImaging, Plugin.Manager.Sesion.Usuario.id)
                                    manager.Connect()

                                    ' Obtener los Files a transferir
                                    FilesDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor

                                    Const FileFolderName = "imagenes\"
                                    If (Not Directory.Exists(OutputFolder & FileFolderName)) Then
                                        Directory.CreateDirectory(OutputFolder & FileFolderName)
                                    End If

                                    For Each ItemFile As DataRowView In FilesDataView
                                        If ProgressForm.Cancelar Then Throw New Exception("La acción fue cancelada por el usuario")

                                        Dim RowFile = CType(ItemFile.Row, DBImaging.SchemaExport.CTA_ConfinancieraRow)

                                        ' Enviar el archivo
                                        ExportarImagen(manager, RowFile, Compresion, OutputFolder & FileFolderName)

                                        RowFile.Nombre_Imagen_File = FileFolderName & RowFile.CBarras_File & ".tif"
                                        ItemFile.Item("Nombre_Imagen_File") = RowFile.Nombre_Imagen_File

                                        Progreso += 1
                                        ProgressForm.SetProgreso(Progreso)
                                        Application.DoEvents()
                                    Next

                                Catch ex As Exception
                                    If (manager IsNot Nothing) Then manager.Disconnect()
                                    Throw
                                End Try
                            Next

                            ' Exportar data
                            Dim swData As New StreamWriter(CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\" & "Export_Imagenes_" & DateTime.Now.ToString("yyyyMMdd") & ".csv", False, System.Text.Encoding.UTF8)

                            For Each DataExportarRow In DataExportarDataTable
                                Dim Nombre_Imagen_File = "E:\Imagenes\" & DataExportarRow.CBarras_File & ".tif"
                                swData.WriteLine(DataExportarRow.Credito & "," & _
                                                 DataExportarRow.Cedula & "," & _
                                                 CStr(IIf(DataExportarRow.Coodeudor = "", "", DataExportarRow.Coodeudor & ",")) & _
                                                 Nombre_Imagen_File & "," & _
                                                 DataExportarRow.Nombre_Documento & "," & _
                                                 DataExportarRow.Nombre & "," & _
                                                 DataExportarRow.Fecha & CStr(IIf(DataExportarRow.Placa <> "", ",", "")) & _
                                                 DataExportarRow.Placa)
                            Next

                            swData.Flush()
                            swData.Close()
                            
                            ' Crear la exportación
                            Try
                                dbmImaging.Transaction_Begin()

                                Dim ExportacionType = New DBImaging.SchemaProcess.TBL_ExportacionType()

                                ExportacionType.fk_Exportacion_Tipo = DBImaging.TipoExportacionEnum.TEXTO_PLANO
                                ExportacionType.Fecha_Exportacion = SlygNullable.SysDate
                                ExportacionType.fk_Usuario = Plugin.Manager.Sesion.Usuario.id
                                ExportacionType.fk_Sede_Procesamiento = Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Sede
                                ExportacionType.fk_Centro_Procesamiento = Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.fk_Centro_Procesamiento
                                ExportacionType.fk_Puesto_Trabajo = Plugin.Manager.DesktopGlobal.PuestoTrabajoRow.id_Puesto_Trabajo
                                ExportacionType.Total_Folders = -1
                                ExportacionType.Total_Files = DataExportarDataTable.Count
                                ExportacionType.Ruta = OutputFolder


                                Dim OTDataTable = dbmImaging.SchemaProcess.TBL_OT.DBGet(firtsOTRow.id_OT)
                                Dim Exportado As Boolean = False

                                If (Not OTDataTable(0).Isfk_ExportacionNull()) Then
                                    Exportado = dbmImaging.SchemaProcess.TBL_Exportacion.DBGet(OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso, OTDataTable(0).fk_Exportacion).Count > 0
                                End If

                                Dim OTType = New DBImaging.SchemaProcess.TBL_OTType()
                                OTType.Exportado = True

                                If (Exportado) Then
                                    dbmImaging.SchemaProcess.TBL_Exportacion.DBUpdate(ExportacionType, OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso, OTDataTable(0).fk_Exportacion)

                                    OTType.fk_Exportacion = OTDataTable(0).fk_Exportacion
                                Else
                                    ExportacionType.fk_Entidad_Procesamiento = OTDataTable(0).fk_Entidad_Procesamiento
                                    ExportacionType.fk_Entidad = OTDataTable(0).fk_Entidad
                                    ExportacionType.fk_Proyecto = OTDataTable(0).fk_Proyecto
                                    ExportacionType.fk_Fecha_Proceso = OTDataTable(0).fk_fecha_proceso
                                    ExportacionType.id_Exportacion = dbmImaging.SchemaProcess.TBL_Exportacion.DBNextId(OTDataTable(0).fk_Entidad_Procesamiento, OTDataTable(0).fk_Entidad, OTDataTable(0).fk_Proyecto, OTDataTable(0).fk_fecha_proceso)

                                    dbmImaging.SchemaProcess.TBL_Exportacion.DBInsert(ExportacionType)

                                    OTType.fk_Exportacion = ExportacionType.id_Exportacion
                                End If
                                
                                For Each otRow In otDatatTable                                    
                                    dbmImaging.SchemaProcess.TBL_OT.DBUpdate(OTType, otRow.id_OT)                                    
                                Next

                                dbmImaging.Transaction_Commit()
                            Catch
                                dbmImaging.Transaction_Rollback()
                                Throw
                            End Try

                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)

                            ProgressForm.Hide()
                            Application.DoEvents()

                            Return
                        Finally
                            Me.Enabled = True

                            If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()

                            BorrarTemporal()

                            ProgressForm.Close()
                        End Try

                        MessageBox.Show("La información se exportó con éxito", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        CargarOTs()
                    Else
                        MessageBox.Show("Operación cancelada por Usuario", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                Else
                    MessageBox.Show("No se encontraron registros para exportar", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If

            BorrarTemporal()
        End Sub

        Private Sub ExportarImagen(nManager As FileProviderManager, ByVal RowFile As DBImaging.SchemaExport.CTA_ConfinancieraRow, nCompresion As ImageManager.EnumCompression, nFileFolderName As String)
            Dim Folios = nManager.GetFolios(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version)

            Dim FileNames As New List(Of String)
            Dim FileName As String = ""

            For folio As Short = 1 To Folios
                Dim Imagen() As Byte = Nothing
                Dim Thumbnail() As Byte = Nothing

                nManager.GetFolio(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version, folio, Imagen, Thumbnail)

                If (Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida.ToUpper() = ".TIF") Then
                    FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                    FileNames.Add(FileName)
                Else
                    FileName = nFileFolderName & RowFile.File_Unique_Identifier.ToString() & "_" & folio.ToString("0000") & Plugin.Manager.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida
                End If

                Using fs = New FileStream(FileName, FileMode.Create)
                    fs.Write(Imagen, 0, Imagen.Length)
                    fs.Close()
                End Using
            Next

            If (FileNames.Count > 0) Then
                FileName = nFileFolderName & RowFile.File_Unique_Identifier.ToString() & "_0001" & ".tif"

                ImageManager.Save(FileNames, FileName, "", ImageManager.EnumFormat.TIFF, nCompresion, False, Program.AppPath & Program.TempPath, True)
            End If

            File.Move(FileName, nFileFolderName & RowFile.CBarras_File & ".tif")
        End Sub

        Private Shared Sub BorrarTemporal()
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

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If (Not Directory.Exists(CarpetaSalidaTextBox.Text)) Then
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

            ElseIf (Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 Or Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0) Then
                MessageBox.Show("La carpeta debe estar vacia para exportar los datos", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.CarpetaSalidaTextBox.Focus()

            ElseIf (Me.OTDataGridView.SelectedRows.Count = 0) Then
                MessageBox.Show("Se debe seleccionar una OT", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.OTDataGridView.Focus()
            Else
                Return True
            End If

            Return False
        End Function

        Private Function getFecha() As Integer
            Return CInt(Me.FechaProcesoDateTimePicker.Value.ToString("yyyyMMdd"))
        End Function

        Private Function getOTs() As DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                dbmImaging.Connection_Open(Plugin.Manager.Sesion.Usuario.id)

                Return dbmImaging.SchemaProcess.CTA_Exportacion_OT.DBFindByfk_Entidad_Procesamientofk_Entidadfk_Proyectofk_fecha_proceso(Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _
                                                                                                                                        Plugin.Manager.ImagingGlobal.Entidad, _
                                                                                                                                        Plugin.Manager.ImagingGlobal.Proyecto, _
                                                                                                                                        getFecha())
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return New DBImaging.SchemaProcess.CTA_Exportacion_OTDataTable()
        End Function

#End Region

    End Class

End Namespace