Imports System.Windows.Forms
Imports System.IO
Imports Miharu.Desktop.Library.Config
Imports DBCore
Imports DBImaging
Imports DBImaging.SchemaProcess
Imports Slyg.Tools
Imports Slyg.Tools.Progress

Namespace Imaging.Forms.Exportar

    Public Class FormExport

#Region " Declaraciones "

        Private _Plugin As Plugin

#End Region

#Region " Constructores "

        Public Sub New(ByVal nBanagrarioDesktopPlugin As Plugin)

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _Plugin = nBanagrarioDesktopPlugin
        End Sub

#End Region

#Region " Eventos "

        Private Sub FormExport_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
            LoadData()
        End Sub

        Private Sub BuscarCarpetaButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BuscarCarpetaButton.Click
            fbdCarpetaSalida.SelectedPath = CarpetaSalidaTextBox.Text
            If fbdCarpetaSalida.ShowDialog = DialogResult.OK Then
                CarpetaSalidaTextBox.Text = fbdCarpetaSalida.SelectedPath
            End If
        End Sub

        Private Sub ExportarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportarButton.Click
            'Exportar(dtpFechaInicial.Value.Date, dtpFechaFinal.Value.Date.AddDays(1).AddSeconds(-1))
            Exportar()
        End Sub

        Private Sub CancelarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CancelarButton.Click
            Me.Close()
        End Sub

#End Region

#Region " Metodos "

        Private Sub LoadData()
            Dim dbmCore As New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)

            Try
                dbmCore.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                Dim Esquema = dbmCore.SchemaConfig.TBL_Esquema.DBFindByfk_Entidadfk_Proyecto(_Plugin.Manager.ImagingGlobal.Entidad, Nothing)
                Utilities.LlenarCombo(EsquemaComboBox, Esquema, Esquema.id_EsquemaColumn.ColumnName, Esquema.Nombre_EsquemaColumn.ColumnName, True, "-1", "- Todos -")

                If CInt(EsquemaComboBox.SelectedValue) > -1 Then
                    Dim Documento = dbmCore.SchemaConfig.TBL_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, CType(EsquemaComboBox.SelectedValue.ToString, SlygNullable(Of Short)), False)
                    Utilities.LlenarCombo(DocumentoComboBox, Documento, Documento.id_DocumentoColumn.ColumnName, Documento.Nombre_DocumentoColumn.ColumnName, True, "-1", "- Todos -")
                Else
                    Dim Documento = dbmCore.SchemaConfig.TBL_Documento.DBFindByfk_Entidadfk_Proyectofk_EsquemaEliminado(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, Nothing, False)
                    Utilities.LlenarCombo(DocumentoComboBox, Documento, Documento.id_DocumentoColumn.ColumnName, Documento.Nombre_DocumentoColumn.ColumnName, True, "-1", "- Todos -")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Finally
                dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub Exportar()
            If Validar() Then

                Dim dbmImaging As New DBImagingDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging)
                Dim dbmCore As New DBCoreDataBaseManager(_Plugin.Manager.DesktopGlobal.ConnectionStrings.Core)
                Dim TotalesDataTable As New CTA_Exportacion_TotalesDataTable
                Dim ProgressForm As New FormProgress

                Try
                    dbmImaging.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)

                    'TotalesDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Totales.DBExecute(_Plugin.Manager.ImagingGlobal.Entidad, _Plugin.Manager.ImagingGlobal.Proyecto, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, CShort(DocumentoComboBox.SelectedValue.ToString()), nFechaInicial, nFechaFinal)

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    dbmImaging.Connection_Close()
                End Try

                If (TotalesDataTable.Rows.Count > 0) Then
                    Dim Respuesta As DialogResult

                    Respuesta = MessageBox.Show("Se encontró : " & vbCrLf & _
                                                TotalesDataTable(0).Folders & " Unidades Documentales, " & vbCrLf & _
                                                TotalesDataTable(0).Files & " Documentos, " & vbCrLf & _
                                                TotalesDataTable(0).Folios & " Folios " & vbCrLf & _
                                                "pendientes de exportar ¿Desea Exportar esta información?", "Plugin", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If (Respuesta = DialogResult.Yes) Then
                        Try
                            Me.Cursor = Cursors.WaitCursor
                            Me.Enabled = False

                            dbmImaging.Connection_Open(1)
                            dbmCore.Connection_Open(1)

                            'dbmImaging.SchemaProcess.PA_Exportacion_insert.DBExecute(_Plugin.Manager.Sesion.Usuario.id, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.fk_Sede, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Entidad, _Plugin.Manager.ImagingGlobal.ProyectoImagingRow.fk_Proyecto)
                            Dim FileDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Files.DBExecute(1, Nothing)

                            Dim OutputFolder As String = CarpetaSalidaTextBox.Text.TrimEnd("\"c) & "\"

                            Dim FilesDataView As New DataView(FileDataTable)

                            ProgressForm.Show()
                            ProgressForm.Process = "Exportar"
                            ProgressForm.ValueProcess = 0
                            ProgressForm.MaxValueProcess = TotalesDataTable(0).Files
                            ProgressForm.Action = "Obteniendo imágenes..."

                            Application.DoEvents()

                            Dim ServidoresDataTable = dbmImaging.SchemaProcess.PA_Exportacion_Servidor.DBExecute(1)

                            For Each RowServidor In ServidoresDataTable
                                Dim MsgError As String
                                Dim Resultado As String

                                Dim DBMStorage As New DBStorage.DBStorageDataBaseManager(RowServidor.ConnectionString_Servidor)

                                Try
                                    DBMStorage.Connection_Open(_Plugin.Manager.Sesion.Usuario.id)


                                    ' Obtener los Files a transferir
                                    FilesDataView.RowFilter = "fk_Entidad_Servidor = " & RowServidor.fk_Entidad & " AND fk_Servidor = " & RowServidor.id_Servidor

                                    For Each ItemFile As DataRowView In FilesDataView
                                        If ProgressForm.Cancel Then Throw New Exception("La acción fue cancelada por el usuario")

                                        Dim RowFile = CType(ItemFile.Row, CTA_Exportacion_FilesRow)
                                        
                                        ' Enviar el archivo
                                        MsgError = "Imagen no encontrada"
                                        Resultado = DescargarImagenJPG(DBMStorage, RowFile)
                                        If Resultado = String.Empty Then Throw New Exception(MsgError)

                                        ' leer data
                                        Dim Campos = dbmCore.SchemaProcess.TBL_File_Data.DBGet(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.fk_Documento, Nothing)
                                        Dim Fecha As String = Campos(0).Valor_File_Data.ToString()
                                        Dim MTCN As String = Campos(1).Valor_File_Data.ToString()

                                        Dim FileName = OutputFolder & MTCN & "-" & Fecha & ".jpg"
                                        Dim Contador As Integer = 1

                                        While (File.Exists(FileName))
                                            FileName = OutputFolder & MTCN & "-" & Fecha & "_" & Contador & ".jpg"
                                            Contador += 1
                                        End While

                                        File.Move(Resultado, FileName)

                                        'dbmImaging.SchemaProcess.TBL_Exportacion_Detalle.DBInsert(idExportacion, ItemFile.Item("fk_Expediente"))

                                        ItemFile.Item("Nombre_Imagen_File") = RowFile.Nombre_Imagen_File

                                        ProgressForm.IncrementProcess()

                                        Application.DoEvents()
                                    Next


                                Catch ex As Exception
                                    MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    BorrarTemporal()
                                Finally
                                    DBMStorage.Connection_Close()
                                End Try
                            Next

                            BorrarTemporal()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)

                            ProgressForm.Hide()
                            Application.DoEvents()

                        Finally
                            dbmImaging.Connection_Close()
                            dbmCore.Connection_Close()

                            ProgressForm.Close()

                            Me.Cursor = Cursors.Default
                            Me.Enabled = True
                        End Try
                        MessageBox.Show("La información se exportó con éxito", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                    Else
                        MessageBox.Show("Operación cancelada por Usuario", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                Else
                    MessageBox.Show("No se encontraron registros para exportar", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        End Sub

        Private Function DescargarImagenJPG(ByRef nDBMStorage As DBStorage.DBStorageDataBaseManager, ByVal RowFile As CTA_Exportacion_FilesRow) As String
            'Dim FileProvider = Nothing
            'Dim FoliosDataTable = nDBMStorage.SchemaImaging.TBL_File_Folio.DBGet(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version, Nothing)

            'FileProvider = New Indexer.Controller.FileProviderManager(_Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.Usa_Cache_Local, _Plugin.Manager.DesktopGlobal.ServidorImagenRow.ConnectionString_Servidor, _Plugin.Manager.DesktopGlobal.ConnectionStrings.Imaging, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.IPName_Servidor, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.Port_Servidor, _Plugin.Manager.DesktopGlobal.CentroProcesamientoRow.AppName_Servidor)

            'FileProvider.Connect()
            'Dim FolioRow = FoliosDataTable(0)
            'Dim Imagen() As Byte = Nothing
            'Dim Thumbnail() As Byte = Nothing

            'FileProvider.GetFolio(Imagen, Thumbnail, FolioRow.fk_Expediente, FolioRow.fk_Folder, FolioRow.fk_File, FolioRow.fk_Version, FolioRow.id_File_Record_Folio)

            Dim FileName = Program.AppPath & Program.TempPath & Guid.NewGuid().ToString() & ".jpg"

            'Using ImageStream = New MemoryStream(Imagen)
            '    Using ImageBitmap = New Bitmap(ImageStream)
            '        ImageBitmap.Save(FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
            '        FileProvider.Disconnect()
            '    End Using
            'End Using

            Return FileName
        End Function

        Private Sub BorrarTemporal()
            Try
                Dim objDirectoryInfo = New DirectoryInfo(Program.AppPath & Program.TempPath)
                Dim fileInfoArray As FileInfo() = objDirectoryInfo.GetFiles()
                Dim objFileInfo As FileInfo
                For Each objFileInfo In fileInfoArray
                    objFileInfo.Delete()
                Next objFileInfo
            Catch ex As Exception
            End Try
        End Sub

#End Region

#Region " Funciones "

        Private Function Validar() As Boolean
            If dtpFechaInicial.Value > dtpFechaFinal.Value Then
                MessageBox.Show("La Fecha Final debe ser superior a la Fecha Inicial", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpFechaFinal.Focus()

            ElseIf Not Directory.Exists(CarpetaSalidaTextBox.Text) Then
                MessageBox.Show("El directorio no existe, Seleccione un directorio existente", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CarpetaSalidaTextBox.Focus()

            ElseIf Directory.GetDirectories(CarpetaSalidaTextBox.Text).Length > 0 Or Directory.GetFiles(CarpetaSalidaTextBox.Text).Length > 0 Then
                MessageBox.Show("La carpeta debe estar vacia para exportar los datos", "Plugin", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CarpetaSalidaTextBox.Focus()

            Else
                Return True
            End If

            Return False
        End Function

#End Region

    End Class
End Namespace