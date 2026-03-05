Imports Miharu.Desktop.Controls.DesktopMessageBox
Imports Miharu.Desktop.Controls.DesktopTextBox
Imports Miharu.Desktop.Library
Imports Miharu.Desktop.Library.Config
Imports DBCore.SchemaImaging
Imports Slyg.Tools.Imaging
Imports System.IO
Imports DBCore.SchemaProcess
Imports DBImaging.SchemaProcess
Imports Miharu.FileProvider.Library
Imports Slyg.Tools
Imports System.Drawing
Imports Miharu.Imaging.OffLineViewer.Library

'Imports Miharu.Imaging.Indexer.Events

'Imports Miharu.Imaging.Library.Eventos

Namespace Controls

    Public Class ImagingSearchControl

#Region " Declaraciones "

        Private FileDataTable As New DBCore.SchemaImaging.CTA_Busqueda_FilesDataTable
        Private FileDataDataTable As New DBCore.SchemaImaging.CTA_Busqueda_Files_DataDataTable
        Private FileValidacionDataTable As New DBCore.SchemaImaging.CTA_Busqueda_Files_ValidacionDataTable
        Private ValoresDataTable As New DBCore.SchemaProcess.TBL_File_Data_AsociadaDataTable

        'Risks para la vista

        Private FileDataTableRisks As New DBCore.SchemaImaging.CTA_Busqueda_Files_RisksDataTable


        Private SearchParameters As IImagingSearchControlParameters

        Private _Locked As Boolean

        Public Workspace As FormImagingWorkSpace

        Private _eventManager As Eventos.EventManager

#End Region

#Region " Propiedades "

        Public ReadOnly Property EventManager As Eventos.EventManager
            Get
                Return Me._eventManager
            End Get
        End Property

#End Region

#Region " Constructores "

        Public Sub New()
            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.SearchParameters = New ImagingSearchControlParameters()

            If (Not Me.DesignMode) Then
                SetSearchControl(Me.SearchParameters)
            End If
        End Sub

#End Region

#Region " Eventos "

        Private Sub ImagingSearchControl_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            SearchParameters.SetFocus()
        End Sub

        Private Sub AddDocumentFileButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AddDocumentFileButton.Click
            'AñadirDocumento()
        End Sub

        Private Sub DeleteFolderButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DeleteFolderButton.Click
            DeleteFolder()
        End Sub

        Private Sub ShowImageButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ShowImageButton.Click
            If TipologiasDataGridView.SelectedRows.Count > 0 Then
                'Dim RowFile = CType(CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_FilesRow)
                'MostrarImagen(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File)
                Dim RowFile = CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row
                'MostrarImagen(CLng(RowFile("fk_Expediente")), CShort(RowFile("fk_Folder")), CShort(RowFile("fk_File")), CInt(RowFile("tipoDB")))
                MostrarImagen(CLng(RowFile("fk_Expediente")), CShort(RowFile("fk_Folder")), CShort(RowFile("fk_File")), CShort(RowFile("id_Version")), CInt(RowFile("tipoDB")))
            End If
        End Sub

        Private Sub ReprocesoButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ReprocesoButton.Click
            Reproceso()
        End Sub

        Private Sub DeleteFileButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DeleteFileButton.Click
            DeleteFile()
        End Sub

        Private Sub ResultadosDataGridView_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ResultadosDataGridView.SelectionChanged
            If ResultadosDataGridView.SelectedRows.Count > 0 Then
                Dim RowFolder = CType(ResultadosDataGridView.CurrentRow.DataBoundItem, DataRowView).Row
                ShowTipologias(CLng(RowFolder("id_Expediente")), CShort(RowFolder("fk_Folder")), CInt(RowFolder("tipoDB")))
                ShowAccesosBusqueda(CInt(RowFolder("tipoDB")))
            End If
        End Sub

        Private Sub TipologiasDataGridView_SelectionChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipologiasDataGridView.SelectionChanged
            If TipologiasDataGridView.SelectedRows.Count > 0 Then
                ' Dim RowFile = CType(CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_FilesRow)
                'ShowInformacion(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.TipoDB)
                Dim RowFile = CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row
                ShowInformacion(CLng(RowFile("fk_Expediente")), CShort(RowFile("fk_Folder")), CShort(RowFile("fk_File")), CInt(RowFile("TipoDB")))
                ShowAccesosBusqueda(CInt(RowFile("tipoDB")))
            End If
        End Sub

        Private Sub DisplayResult(ByVal nData As DataTable)
            FileDataTable.Clear()
            FileDataDataTable.Clear()
            ResultadosDataGridView.DataSource = Nothing


            If nData.Rows.Count > 0 Then
                ResultadosDataGridView.DataSource = nData
                AdicionarButton.Enabled = True
            End If

            If nData.Rows.Count = 0 Then
                MessageBox.Show("No se encontraron coincidencias", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            ResultadosLabel.Text = "Resultados [" & Format(nData.Rows.Count, "#,##0") & "]"

            IndexerTablePanel.Visible = False
            TipologiasDataGridView.ClearSelection()
            If nData.Rows.Count > 0 Then
                ShowAccesosBusqueda(CInt(nData.Rows(0)("TipoDB")))
            Else
                ShowAccesosBusqueda(0)
            End If
        End Sub

        Private Sub BeginSearch()
            Me.Cursor = Cursors.WaitCursor
        End Sub

        Private Sub EndSearch()
            Me.Cursor = Cursors.Default
        End Sub

        Private Sub ValidacionesDataGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles ValidacionesDataGridView.CellDoubleClick
            If (Program.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Captura.Reprocesos)) Then
                If CamposDataGridView.SelectedRows.Count > 0 Then
                    Dim RowFileData = CType(CType(ValidacionesDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_Files_ValidacionRow)

                    ShowEditValidacion(RowFileData)
                End If
            End If
        End Sub

        Private Sub CamposDataGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles CamposDataGridView.CellDoubleClick
            If (Program.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Captura.Reprocesos)) Then
                If CamposDataGridView.SelectedRows.Count > 0 Then
                    Dim RowFileData = CType(CType(CamposDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_Files_DataRow)

                    If (RowFileData.fk_Campo_Tipo <> DesktopConfig.CampoTipo.TablaAsociada) Then
                        ShowEditCampo(RowFileData)
                    End If
                End If
            End If
        End Sub

        Private Sub CamposDataGridView_CellClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles CamposDataGridView.CellClick
            If CamposDataGridView.SelectedRows.Count > 0 Then
                Dim RowFileData = CType(CType(CamposDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_Files_DataRow)

                If (CType(RowFileData.fk_Campo_Tipo, DesktopConfig.CampoTipo) = DesktopConfig.CampoTipo.TablaAsociada) Then
                    ShowGrid(RowFileData)
                Else
                    IndexerTablePanel.Visible = False
                End If
            End If
        End Sub

        Private Sub TablaAsociadaDataGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As DataGridViewCellEventArgs) Handles TablaAsociadaDataGridView.CellDoubleClick
            If (Program.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Captura.Reprocesos)) Then
                If TablaAsociadaDataGridView.SelectedRows.Count > 0 Then
                    Dim RowFileData = CType(CType(CamposDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_Files_DataRow)
                    Dim RowFileDataAsociada = CType(TablaAsociadaDataGridView.CurrentRow.DataBoundItem, DataRowView).Row

                    ShowEditDataAsociada(RowFileData, CShort(RowFileDataAsociada(0)), CShort(e.ColumnIndex))
                End If
            End If
        End Sub

        Private Sub Table_CloseButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Table_CloseButton.Click
            IndexerTablePanel.Visible = False
        End Sub

        Private Sub AdicionarButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles AdicionarButton.Click
            If Not _Locked Then
                Try
                    _Locked = True
                    Dim DatosSolicitud = CType(ResultadosDataGridView.DataSource, DataTable)
                    Dim Expediente = CInt(DatosSolicitud.Rows(0).Item("id_Expediente").ToString())
                    Dim Folder = CShort(DatosSolicitud.Rows(0).Item("fk_Folder").ToString())
                    Dim objFormAdicionarSolicitud As New FormAdicionarDocumento(Expediente, Folder)
                    objFormAdicionarSolicitud.ShowDialog()

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Adicionar Imágen",
                                           DesktopMessageBoxControl.IconEnum.ErrorIcon, False)
                Finally
                    _Locked = False
                End Try
            End If
        End Sub

        Private Sub CruceButton_Click(sender As System.Object, e As System.EventArgs) Handles CruceButton.Click
            If Not _Locked Then
                Try
                    _Locked = True

                    If (Program.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Captura.Reprocesos)) Then
                        If CamposDataGridView.SelectedRows.Count > 0 Then
                            Dim RowFileData = CType(CType(CamposDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_Files_DataRow)

                            EjecutarCruce(RowFileData)

                        End If
                    End If

                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Cruce Documento", DesktopMessageBoxControl.IconEnum.ErrorIcon, False)
                Finally
                    _Locked = False
                    CruceButton.Enabled = False
                End Try
            End If
        End Sub

#End Region

#Region " Metodos "

        Public Sub SetSearchControl(ByVal nSearchControl As IImagingSearchControlParameters)
            Dim SearchControl As Control = CType(nSearchControl, Control)
            SearchControl.Dock = DockStyle.Fill

            AddHandler nSearchControl.DisplayResult, AddressOf DisplayResult
            AddHandler nSearchControl.BeginSearch, AddressOf BeginSearch
            AddHandler nSearchControl.EndSearch, AddressOf EndSearch

            SearchPanel.Controls.Clear()
            SearchPanel.Controls.Add(SearchControl)
        End Sub

        Private Sub ShowTipologias(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal tipoDB As Integer)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim DataTableVista As New DataTable


            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                FileDataTable.Clear()
                'Mira si es imaging o risks
                If tipoDB = 1 Then
                    dbmCore.SchemaImaging.CTA_Busqueda_Files.DBFillByfk_Expedientefk_Folder(FileDataTable, nExpediente, nFolder)
                    DataTableVista = FileDataTable
                Else

                    dbmCore.SchemaImaging.CTA_Busqueda_Files_Risks.DBFillByfk_Expedientefk_Folder(FileDataTableRisks, nExpediente, nFolder)
                    DataTableVista = FileDataTableRisks
                End If



                TipologiasDataGridView.DataSource = DataTableVista

                TipologiasLabel.Text = "Tipologias [" & Format(DataTableVista.Rows.Count, "#,##0") & "]"
            Catch
                Throw
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub ShowInformacion(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal tipoDB As Integer)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                FileDataDataTable.Clear()
                FileValidacionDataTable.Clear()

                FileDataDataTable = dbmCore.SchemaImaging.PA_Busqueda_File_Data.DBExecute(nExpediente, nFolder, nFile, tipoDB)
                FileValidacionDataTable = dbmCore.SchemaImaging.PA_Busqueda_File_Validacion.DBExecute(nExpediente, nFolder, nFile, tipoDB)

                CamposDataGridView.DataSource = FileDataDataTable
                ValidacionesDataGridView.DataSource = FileValidacionDataTable

                InformacionLabel.Text = "Información [" & Format(FileDataDataTable.Rows.Count, "#,##0") & "]"

            Catch
                Throw
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub ShowAccesosBusqueda(ByVal TipoDB As Integer)
            If Not _Locked Then
                If TipologiasDataGridView.SelectedRows.Count > 0 Then
                    'Dim RowFile = CType(CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_FilesRow)
                    Dim RowFile = CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row
                    Try
                        ShowImageButton.Enabled = (Not RowFile("Nombre_Imagen_File") Is Nothing)
                    Catch ex As Exception
                        ShowImageButton.Enabled = False
                    End Try
                    If TipoDB = 1 Then
                        ReprocesoButton.Enabled = (ShowImageButton.Enabled And Program.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Captura.Reprocesos))
                        DeleteFileButton.Enabled = (ShowImageButton.Enabled And Program.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Captura.Reprocesos))
                    Else
                        ReprocesoButton.Enabled = False
                        DeleteFileButton.Enabled = False
                        AdicionarButton.Enabled = False
                    End If
                    
                Else
                    If TipoDB = 1 Then

                        ShowImageButton.Enabled = False
                        ReprocesoButton.Enabled = False
                        DeleteFileButton.Enabled = False
                    Else
                        ShowImageButton.Enabled = False
                        ReprocesoButton.Enabled = False
                        DeleteFileButton.Enabled = False
                        AdicionarButton.Enabled = False
                    End If

                    If ResultadosDataGridView.SelectedRows.Count > 0 Then
                        'AddDocumentFileButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeAcceder1("4.2.2")
                        DeleteFolderButton.Enabled = Program.Sesion.Usuario.PerfilManager.PuedeAcceder(Permisos.Imaging.Proceso.Captura.Reprocesos)
                    End If
                End If
            End If
        End Sub

        Private Sub DeleteFolder()
            Dim Respuesta = MessageBox.Show("Esta acción eliminará el Folder y todo su contenido, ¿desea continuar con el proceso?", Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If Respuesta = DialogResult.Yes Then
                Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                Dim RowFolder = CType(CType(ResultadosDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, OffLineViewer.Library.xsdOffLineData.TBL_FolderRow)

                Try
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dbmCore.Transaction_Begin()

                    dbmCore.SchemaImaging.PA_Especial_Eliminar_Folder.DBExecute(RowFolder.id_Expediente, RowFolder.id_Folder, Nothing)

                    dbmCore.Transaction_Commit()
                Catch ex As Exception
                    If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                    MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                End Try

                MessageBox.Show("El proceso se realizó con exito", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)

                SearchParameters.Search()
            End If
        End Sub

        Private Sub DeleteFile()
            If Not _Locked Then
                Dim Respuesta = MessageBox.Show("Esta acción eliminará el Documento, ¿desea continuar con el proceso?",
                                                Program.AssemblyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If Respuesta = DialogResult.Yes Then
                    Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
                    Dim RowFile = CType(CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, 
                                          CTA_Busqueda_FilesRow)
                    Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
                    Dim manager As FileProviderManager = Nothing

                    Try
                        dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                        dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                        dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                        dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                        Dim centro = dbmImaging.SchemaSecurity.CTA_Centro_Procesamiento.DBFindByfk_Entidadfk_Sedeid_Centro_Procesamiento(Program.DesktopGlobal.CentroProcesamientoRow.fk_Entidad, Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede, Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento)(0).ToCTA_Centro_ProcesamientoSimpleType()
                        manager = New FileProviderManager(RowFile.fk_Expediente, RowFile.fk_Folder, centro, dbmImaging, Program.Sesion.Usuario.id)
                        manager.Connect()

                        dbmCore.Transaction_Begin()
                        dbmImaging.Transaction_Begin()

                        dbmCore.SchemaImaging.TBL_File.DBDelete(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, Nothing)
                        dbmCore.SchemaProcess.TBL_File.DBDelete(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File)

                        ' Borrar los folios
                        Dim VersionesDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, Nothing)
                        For Each versionRow In VersionesDataTable
                            manager.DeleteItem(versionRow.fk_Expediente, versionRow.fk_Folder, versionRow.fk_File, versionRow.id_Version)
                        Next

                        ' Borra el File
                        dbmImaging.SchemaProcess.TBL_File.DBDelete(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, Nothing)

                        dbmImaging.Transaction_Commit()
                        dbmCore.Transaction_Commit()
                    Catch ex As Exception
                        If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                        If (manager IsNot Nothing) Then manager.TransactionRollback()
                        MessageBox.Show(ex.Message, Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    Finally
                        If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                        If (manager IsNot Nothing) Then manager.Disconnect()
                    End Try

                    MessageBox.Show("El proceso se realizó con exito", Program.AssemblyName, MessageBoxButtons.OK,
                                    MessageBoxIcon.Information)

                    Me.SearchParameters.Search()
                End If
            End If
        End Sub

        Private Sub MostrarImagen(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal TipoDB As Integer)
            Dim tempPath = Program.AppPath & Program.TempPath

            If (Not Directory.Exists(tempPath)) Then
                Directory.CreateDirectory(tempPath)
            Else
                Try
                    CentralOffLineViewer.ImagePath = Nothing

                    For Each fileImagen In Directory.GetFiles(tempPath)
                        File.Delete(fileImagen)
                    Next
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("EliminaElementosTemp", ex)
                End Try
            End If

            Dim manager As FileProviderManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

       

            Try
                'verifica si es Imaging o Risks
                If TipoDB = 1 Then
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                Else
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core_Risks)
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging_Risks)
                End If


                dbmCore.Connection_Open(0)
                dbmImaging.Connection_Open(0)

                manager = New FileProviderManager(nExpediente, nFolder, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()

                Dim FilesDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(nExpediente, nFolder, nFile, Nothing, 1, New DBCore.SchemaImaging.TBL_FileEnumList(DBCore.SchemaImaging.TBL_FileEnum.id_Version, False))

                If TipoDB = 1 Then
                    If (FileDataTable.Count = 0) Then Throw New Exception("File no encontrado")
                Else
                    If (FileDataTableRisks.Count = 0) Then Throw New Exception("File no encontrado")
                End If

                Dim FileRow = FilesDataTable(0)

                Dim ListImages = New List(Of String)
                Const Formato As ImageManager.EnumFormat = ImageManager.EnumFormat.Tiff
                Const Compresion As ImageManager.EnumCompression = ImageManager.EnumCompression.Lzw

                'Obtiene el File a visualizar.
                If (FileRow.Es_Anexo) Then
                    Dim folios = manager.GetFolios(FileRow.fk_Anexo)
                    If (folios > 0) Then
                        For folio = CShort(1) To folios
                            Dim Imagen As Byte() = Nothing
                            Dim Thumbnail As Byte() = Nothing
                            manager.GetFolio(FilesDataTable(0).fk_Anexo, folio, Imagen, Thumbnail)
                            ImageManager.Save(New FreeImageAPI.FreeImageBitmap(New MemoryStream(Imagen)), tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", Formato, Compresion, False, tempPath)
                            ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                        Next
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("No existe una imagen asociada para visualizar.", "Imágen no encontrada", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                Else
                    'Se obtienen los folios del file.
                    Dim folios = manager.GetFolios(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)
                    If (folios > 0) Then
                        For folio = CShort(1) To folios
                            Dim Imagen As Byte() = Nothing
                            Dim Thumbnail As Byte() = Nothing
                            Dim filePath As String
                            manager.GetFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, folio, Imagen, Thumbnail)

                            Using ms = New MemoryStream(Imagen)
                                ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                                filePath = ListImages(folio - 1).ToString()
                                Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                                img.Save(filePath, System.Drawing.Imaging.ImageFormat.Tiff)
                                ms.Dispose()
                            End Using

                            'Código anterior
                            'Using bm = New FreeImageAPI.FreeImageBitmap(ms)
                            '    ImageManager.Save(bm, tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", Formato, Compresion, False, tempPath)
                            '    bm.Dispose()
                            'End Using

                            'ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                        Next
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("No existe una imagen asociada para visualizar.", "Imágen no encontrada", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                End If

                CentralOffLineViewer.ImagePath = ListImages
                btnGuardarCambios.Enabled = True

                'Si el documento esta compuesto por más de una imagen, se crea un TIFF para pasarlo al visor.
                'ImageManager.Save(ListImages, tempPath & "ImageTIFF.TIF", "", ImageManager.EnumFormat.TIFF, ImageManager.EnumCompression.LZW, False, tempPath, True)

                'Dim arrayImage As New ArrayList
                'Dim ThumbnailWhith, ThumbnailHeight As Integer
                'Dim ThumbnailPictureBox As PictureBox
                'Dim Pagina As Integer = 0

                'For Each folio In ListImages
                '    Using SingleBitmap = New FreeImageAPI.FreeImageBitmap(folio)

                '        ThumbnailWhith = 60
                '        ThumbnailHeight = CInt((ThumbnailWhith / SingleBitmap.Width) * SingleBitmap.Height)

                '        ThumbnailPictureBox = New PictureBox
                '        ThumbnailPictureBox.Tag = (Pagina + 1)
                '        ThumbnailPictureBox.BorderStyle = BorderStyle.FixedSingle
                '        ThumbnailPictureBox.Cursor = Cursors.Hand
                '        ThumbnailPictureBox.SizeMode = PictureBoxSizeMode.AutoSize
                '        ThumbnailPictureBox.Image = CType(SingleBitmap.GetThumbnailImage(ThumbnailWhith, ThumbnailHeight, AddressOf ThumbnailCallback, IntPtr.Zero), Bitmap)

                '        Pagina += 1
                '        arrayImage.Add(ThumbnailPictureBox)
                '    End Using
                'Next

                'CentralOffLineViewer.ImagePath = tempPath & "ImageTIFF.TIF"

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarImagen", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        Private Sub MostrarImagen(ByVal nExpediente As Long, ByVal nFolder As Short, ByVal nFile As Short, ByVal nVersion As Short, ByVal TipoDB As Integer)
            Dim tempPath = Program.AppPath & Program.TempPath

            If (Not Directory.Exists(tempPath)) Then
                Directory.CreateDirectory(tempPath)
            Else
                Try
                    CentralOffLineViewer.ImagePath = Nothing

                    For Each fileImagen In Directory.GetFiles(tempPath)
                        File.Delete(fileImagen)
                    Next
                Catch ex As Exception
                    DesktopMessageBoxControl.DesktopMessageShow("EliminaElementosTemp", ex)
                End Try
            End If

            Dim manager As FileProviderManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                'verifica si es Imaging o Risks
                If TipoDB = 1 Then
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                Else
                    dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core_Risks)
                    dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging_Risks)
                End If

                dbmCore.Connection_Open(0)
                dbmImaging.Connection_Open(0)

                manager = New FileProviderManager(nExpediente, nFolder, nFile, nVersion, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()

                Dim FilesDataTable = dbmCore.SchemaImaging.TBL_File.DBGet(nExpediente, nFolder, nFile, nVersion, 1, New DBCore.SchemaImaging.TBL_FileEnumList(DBCore.SchemaImaging.TBL_FileEnum.id_Version, False))

                If TipoDB = 1 Then
                    If (FileDataTable.Count = 0) Then Throw New Exception("File no encontrado")
                Else
                    If (FileDataTableRisks.Count = 0) Then Throw New Exception("File no encontrado")
                End If

                Dim FileRow = FilesDataTable(0)

                Dim ListImages = New List(Of String)
                Const Formato As ImageManager.EnumFormat = ImageManager.EnumFormat.Tiff
                Const Compresion As ImageManager.EnumCompression = ImageManager.EnumCompression.Lzw

                'Obtiene el File a visualizar.
                If (FileRow.Es_Anexo) Then
                    Dim folios = manager.GetFolios(FileRow.fk_Anexo)
                    If (folios > 0) Then
                        For folio = CShort(1) To folios
                            Dim Imagen As Byte() = Nothing
                            Dim Thumbnail As Byte() = Nothing
                            manager.GetFolio(FilesDataTable(0).fk_Anexo, folio, Imagen, Thumbnail)
                            ImageManager.Save(New FreeImageAPI.FreeImageBitmap(New MemoryStream(Imagen)), tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", Formato, Compresion, False, tempPath)
                            ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                        Next
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("No existe una imagen asociada para visualizar.", "Imágen no encontrada", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                Else
                    'Se obtienen los folios del file.
                    Dim folios = manager.GetFolios(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)
                    If (folios > 0) Then
                        For folio = CShort(1) To folios
                            Dim Imagen As Byte() = Nothing
                            Dim Thumbnail As Byte() = Nothing
                            Dim filePath As String
                            manager.GetFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, folio, Imagen, Thumbnail)

                            Using ms = New MemoryStream(Imagen)
                                ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                                filePath = ListImages(folio - 1).ToString()
                                Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                                img.Save(filePath, System.Drawing.Imaging.ImageFormat.Tiff)
                                ms.Dispose()
                            End Using

                            'Código anterior
                            'Using bm = New FreeImageAPI.FreeImageBitmap(ms)
                            '    ImageManager.Save(bm, tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", Formato, Compresion, False, tempPath)
                            '    bm.Dispose()
                            'End Using

                            'ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                        Next
                    Else
                        DesktopMessageBoxControl.DesktopMessageShow("No existe una imagen asociada para visualizar.", "Imágen no encontrada", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                    End If
                End If

                CentralOffLineViewer.ImagePath = ListImages
                btnGuardarCambios.Enabled = True

                'Si el documento esta compuesto por más de una imagen, se crea un TIFF para pasarlo al visor.
                'ImageManager.Save(ListImages, tempPath & "ImageTIFF.TIF", "", ImageManager.EnumFormat.TIFF, ImageManager.EnumCompression.LZW, False, tempPath, True)

                'Dim arrayImage As New ArrayList
                'Dim ThumbnailWhith, ThumbnailHeight As Integer
                'Dim ThumbnailPictureBox As PictureBox
                'Dim Pagina As Integer = 0

                'For Each folio In ListImages
                '    Using SingleBitmap = New FreeImageAPI.FreeImageBitmap(folio)

                '        ThumbnailWhith = 60
                '        ThumbnailHeight = CInt((ThumbnailWhith / SingleBitmap.Width) * SingleBitmap.Height)

                '        ThumbnailPictureBox = New PictureBox
                '        ThumbnailPictureBox.Tag = (Pagina + 1)
                '        ThumbnailPictureBox.BorderStyle = BorderStyle.FixedSingle
                '        ThumbnailPictureBox.Cursor = Cursors.Hand
                '        ThumbnailPictureBox.SizeMode = PictureBoxSizeMode.AutoSize
                '        ThumbnailPictureBox.Image = CType(SingleBitmap.GetThumbnailImage(ThumbnailWhith, ThumbnailHeight, AddressOf ThumbnailCallback, IntPtr.Zero), Bitmap)

                '        Pagina += 1
                '        arrayImage.Add(ThumbnailPictureBox)
                '    End Using
                'Next

                'CentralOffLineViewer.ImagePath = tempPath & "ImageTIFF.TIF"

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarImagen", ex)
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

        'Private Sub AñadirDocumento()
        '    If dlgInsertarImagen.ShowDialog = Windows.Forms.DialogResult.OK Then
        '        Dim dbmImaging As Imaging.DataAccess.DB_Miharu_Imaging.DB_Miharu_Imaging_DataBaseManager(Program.ConnectionStrings.Imaging)
        '        Dim RowFolder = CType(CType(ResultadosDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, OffLineViewer.Library.OffLineDataDataSet.TBL_FolderRow)
        '        Dim tbFiles As New SchemaProcess.TBL_FileDataTable
        '        Dim RowFile = tbFiles.NewTBL_FileRow()
        '        Dim Archivo As New System.IO.FileInfo(dlgInsertarImagen.FileName)

        '        With RowFile
        '            .fk_Folder = RowFolder.id_Folder
        '            .id_File = -1
        '            .id_Version = 1
        '            .File_Unique_Identifier = Guid.NewGuid
        '            .fk_Entidad = Program.Proceso.EntidadId
        '            .fk_Proyecto = Program.Proceso.ProyectoId
        '            .fk_Esquema = Program.Proceso.EsquemaId
        '            .fk_Documento = -1
        '            .Folios_Documento_File = 1
        '            .Tamaño_Imagen_File = Archivo.Length
        '            .Nombre_Imagen_File = dlgInsertarImagen.FileName
        '            .fk_Usuario_Index = Program.Sesion.Usuario.id
        '            .Key_Cargue_Item = ""
        '            .fk_Estado = 1
        '            .fk_Documento_Tipo = Program.Proceso.Esquema.fk_Formato_Salida
        '            .Save_FileName = System.IO.Path.GetFileName(dlgInsertarImagen.FileName)
        '        End With

        '        Dim RowServidor = tblServidor.FindByfk_Entidadid_Servidor(RowFolder.fk_Entidad_Servidor, RowFolder.fk_Servidor)
        '        Dim RowServidorVolumen = tblServidorVolumen.FindByfk_Entidadfk_Servidorid_Servidor_Volumen(RowFolder.fk_Entidad_Servidor, RowFolder.fk_Servidor, RowFolder.fk_Servidor_Volumen)
        '        Dim EditarForm As New FormEditar(RowFile, RowFolder, RowServidor, RowServidorVolumen.Path_Servidor_Volumen)

        '        EditarForm.LoadData()
        '        EditarForm.ConfigForm()
        '        EditarForm.ShowDialog()

        '        Buscar()
        '    End If
        'End Sub

        Public Sub ReprocesoDocumento(ByVal nMotivo As Short)
            Dim RowFile = CType(CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_FilesRow)
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)
                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                dbmImaging.Transaction_Begin()
                dbmCore.Transaction_Begin()

                Dim FilesDataTable = dbmImaging.SchemaProcess.TBL_File.DBGet(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version)
                Dim fk_Documento As Integer = FilesDataTable(0).fk_Documento

                ' Actualizar File
                Dim FileType As New DBImaging.SchemaProcess.TBL_FileType()
                FileType.Fecha_Reproceso = DBNull.Value
                FileType.Usuario_Primera_Captura = DBNull.Value
                FileType.Fecha_Primera_Captura = DBNull.Value
                FileType.Usuario_Segunda_Captura = DBNull.Value
                FileType.Fecha_Segunda_Captura = DBNull.Value
                FileType.Usuario_Tercera_Captura = DBNull.Value
                FileType.Fecha_Tercera_Captura = DBNull.Value
                FileType.Usuario_Calidad = DBNull.Value
                FileType.Fecha_Calidad = DBNull.Value
                FileType.Usuario_Validacion_Listas = DBNull.Value
                FileType.Fecha_Validacion_Listas = DBNull.Value
                FileType.Usuario_Recortes = DBNull.Value
                FileType.Fecha_Recortes = DBNull.Value
                FileType.Usuario_Calidad_Recortes = DBNull.Value
                FileType.Fecha_Calidad_Recortes = DBNull.Value
                FileType.Usuario_Proceso_Adicional = DBNull.Value
                FileType.Fecha_Proceso_Adicional = DBNull.Value
                FileType.Usuario_Modifica_Correccion = DBNull.Value
                FileType.Fecha_Modifica_Correccion = DBNull.Value
                FileType.Usuario_Correccion_Maquina = DBNull.Value
                FileType.Fecha_Correccion_Maquina = DBNull.Value

                dbmImaging.SchemaProcess.TBL_File.DBUpdate(FileType, RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version)

                ' Actualizar estado
                Dim UpdateEstado As New DBCore.SchemaProcess.TBL_File_EstadoType()
                UpdateEstado.Fecha_Log = SlygNullable.SysDate
                UpdateEstado.fk_Usuario = Program.Sesion.Usuario.id
                UpdateEstado.fk_Estado = DBCore.EstadoEnum.Reproceso

                dbmCore.SchemaProcess.TBL_File_Estado.DBUpdate(UpdateEstado, RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, DesktopConfig.Modulo.Imaging)

                RowFile.Nombre_Estado = "Reproceso"

                'Elimina la data capturada del documento
                'Core
                dbmCore.SchemaProcess.TBL_File_Data.DBDelete(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, Nothing, Nothing)
                dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBDelete(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, Nothing, Nothing, Nothing, Nothing)

                'Imaging
                dbmImaging.SchemaProcess.TBL_File_Data_MC.DBDelete(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version, Nothing, Nothing)
                dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBDelete(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version, Nothing, Nothing, Nothing, Nothing)

                'Elimina las validaciones del documento
                dbmCore.SchemaProcess.TBL_File_Validacion.DBDelete(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, Nothing, fk_Documento)

                'Solicita nuevamente validaciones opcionales
                Dim ImagingFileType As New DBCore.SchemaImaging.TBL_FileType
                ImagingFileType.Validaciones_Opcionales = False
                dbmCore.SchemaImaging.TBL_File.DBUpdate(ImagingFileType, RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, Nothing)

                '---------------------------------------------------------------------------
                ' Actualizar Dashboard
                '---------------------------------------------------------------------------
                'Obtener el Cargue/Paquete
                Dim CargueImgDataTable = dbmCore.SchemaImaging.TBL_Folder.DBGet(RowFile.fk_Expediente, RowFile.fk_Folder)

                'Obtener el detalle de Asignación del Cargue/Paquete
                Dim CargueDatatable = dbmImaging.SchemaProcess.TBL_Cargue.DBGet(CargueImgDataTable(0).fk_Cargue)
                Dim CarguePaqueteDatatable = dbmImaging.SchemaProcess.TBL_Cargue_Paquete.DBGet(CargueImgDataTable(0).fk_Cargue, CargueImgDataTable(0).fk_Cargue_Paquete)

                ' Actualizar validaciones opcionales validaciones opcionales
                Dim DashboardCapturasDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBGet(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File)
                If (DashboardCapturasDataTable.Count = 0) Then
                    Dim CapDashboardType = New TBL_Dashboard_CapturasType()
                    CapDashboardType.fk_Expediente = RowFile.fk_Expediente
                    CapDashboardType.fk_Folder = RowFile.fk_Folder
                    CapDashboardType.fk_File = RowFile.fk_File
                    CapDashboardType.fk_Documento = fk_Documento
                    CapDashboardType.fk_Cargue = CarguePaqueteDatatable(0).fk_Cargue
                    CapDashboardType.fk_Cargue_Paquete = CarguePaqueteDatatable(0).id_Cargue_Paquete
                    CapDashboardType.fk_Entidad_Procesamiento = CargueDatatable(0).fk_Entidad_Procesamiento
                    CapDashboardType.fk_Sede_Procesamiento = CarguePaqueteDatatable(0).fk_Sede_Procesamiento_Asignada
                    CapDashboardType.fk_Centro_Procesamiento = CarguePaqueteDatatable(0).fk_Centro_Procesamiento_Asignado
                    CapDashboardType.fk_Entidad = CargueDatatable(0).fk_Entidad
                    CapDashboardType.fk_Proyecto = CargueDatatable(0).fk_Proyecto
                    CapDashboardType.fk_Estado = DBCore.EstadoEnum.Reproceso
                    CapDashboardType.fk_Reproceso_Motivo = nMotivo
                    CapDashboardType.fk_OT = CargueDatatable(0).fk_OT
                    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBInsert(CapDashboardType)
                Else
                    Dim CapDashboardType = New TBL_Dashboard_CapturasType()
                    CapDashboardType.fk_Estado = DBCore.EstadoEnum.Reproceso
                    CapDashboardType.fk_Reproceso_Motivo = nMotivo
                    dbmImaging.SchemaProcess.TBL_Dashboard_Capturas.DBUpdate(CapDashboardType, RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File)
                End If

                ' Actualizar validaciones opcionales validaciones opcionales
                Dim DashboardValidacionesDataTable = dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBGet(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File)
                If (DashboardValidacionesDataTable.Count = 0) Then
                    Dim ValDashboardType = New TBL_Dashboard_ValidacionesType()
                    ValDashboardType.fk_Expediente = RowFile.fk_Expediente
                    ValDashboardType.fk_Folder = RowFile.fk_Folder
                    ValDashboardType.fk_File = RowFile.fk_File
                    ValDashboardType.fk_Documento = fk_Documento
                    ValDashboardType.fk_Cargue = CarguePaqueteDatatable(0).fk_Cargue
                    ValDashboardType.fk_Cargue_Paquete = CarguePaqueteDatatable(0).id_Cargue_Paquete
                    ValDashboardType.fk_Entidad_Procesamiento = CargueDatatable(0).fk_Entidad_Procesamiento
                    ValDashboardType.fk_Sede_Procesamiento = CarguePaqueteDatatable(0).fk_Sede_Procesamiento_Asignada
                    ValDashboardType.fk_Centro_Procesamiento = CarguePaqueteDatatable(0).fk_Centro_Procesamiento_Asignado
                    ValDashboardType.fk_Entidad = CargueDatatable(0).fk_Entidad
                    ValDashboardType.fk_Proyecto = CargueDatatable(0).fk_Proyecto
                    ValDashboardType.Procesado = True
                    ValDashboardType.fk_OT = CargueDatatable(0).fk_OT
                    dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBInsert(ValDashboardType)
                Else
                    Dim ValDashboardType = New TBL_Dashboard_ValidacionesType()
                    ValDashboardType.Procesado = True
                    dbmImaging.SchemaProcess.TBL_Dashboard_Validaciones.DBUpdate(ValDashboardType, RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File)
                End If

                '---------------------------------------------------------------------------

                dbmImaging.Transaction_Commit()
                dbmCore.Transaction_Commit()

                ' Notificar envio reproceso
                Me.Workspace.EventManager.EnviarReproceso(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version)

                MessageBox.Show("El Documento se envió a reproceso", Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                If (dbmImaging IsNot Nothing) Then dbmImaging.Transaction_Rollback()
                If (dbmCore IsNot Nothing) Then dbmCore.Transaction_Rollback()

                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub ShowEditCampo(ByVal nCampo As CTA_Busqueda_Files_DataRow)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim EditarCampoForm As New FormEditarCampo()
            Dim OTEstado As Boolean

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)


                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                'Validar si ot del expediente esta cerrada
                OTEstado = dbmImaging.SchemaProcess.PA_Consultar_OT_Estado_x_Expediente.DBExecute(nCampo.fk_Expediente)

                If (OTEstado = False Or Program.ImagingGlobal.ProyectoImagingRow.Usa_Fecha_Proceso_Cerrada_F11 = True) Then
                    Dim CampoDataTable = dbmCore.SchemaConfig.TBL_Campo.DBGet(nCampo.fk_Documento, nCampo.fk_Campo)

                    If (OTEstado = True And Program.ImagingGlobal.ProyectoImagingRow.Usa_Fecha_Proceso_Cerrada_F11 = True) Then
                        Dim Validar As Boolean = True
                        Dim MsgError As String = ""

                        Me.Workspace.EventManager.Validar_Reprocesar(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_Documento, nCampo.fk_Campo, 0, True, Validar, MsgError)

                        If Not Validar Then
                            MessageBox.Show(MsgError.ToString(), Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End If

                    EditarCampoForm.AnteriorDesktopTextBox.Visible = True
                    EditarCampoForm.NuevoDesktopTextBox.Visible = True

                    Select Case CType(nCampo.fk_Campo_Tipo, DesktopConfig.CampoTipo)
                        Case DesktopConfig.CampoTipo.Texto
                            EditarCampoForm.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                            EditarCampoForm.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal

                        Case DesktopConfig.CampoTipo.Numerico
                            EditarCampoForm.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
                            EditarCampoForm.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico

                        Case DesktopConfig.CampoTipo.Fecha
                            EditarCampoForm.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                            EditarCampoForm.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha

                        Case DesktopConfig.CampoTipo.Lista
                            EditarCampoForm.NuevoComboBox.Visible = True

                            Dim RowFile = CType(CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_FilesRow)

                            Dim ListaDataTable = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBGet(RowFile.fk_Entidad, CShort(nCampo.fk_Campo_Lista), Nothing)

                            Utilities.LlenarCombo(EditarCampoForm.NuevoComboBox, ListaDataTable, "Valor_Campo_Lista_Item", "Etiqueta_Campo_Lista_Item", True, "", "-1")

                        Case Else
                            EditarCampoForm.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                            EditarCampoForm.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal

                    End Select

                    EditarCampoForm.AnteriorDesktopTextBox.MaximumLength = CampoDataTable(0).Length_Campo
                    EditarCampoForm.AnteriorDesktopTextBox.MinimumLength = CampoDataTable(0).Length_Min_Campo
                    EditarCampoForm.AnteriorDesktopTextBox.Usa_Decimales = CampoDataTable(0).Usa_Decimales

                    EditarCampoForm.NuevoDesktopTextBox.MaximumLength = CampoDataTable(0).Length_Campo
                    EditarCampoForm.NuevoDesktopTextBox.MinimumLength = CampoDataTable(0).Length_Min_Campo
                    EditarCampoForm.NuevoDesktopTextBox.Usa_Decimales = CampoDataTable(0).Usa_Decimales

                    If CampoDataTable(0).Usa_Decimales Then
                        EditarCampoForm.AnteriorDesktopTextBox.Caracter_Decimal = CChar(CampoDataTable(0).Caracter_Decimal)
                        EditarCampoForm.AnteriorDesktopTextBox.Cantidad_Decimales = CampoDataTable(0).Cantidad_Decimales

                        EditarCampoForm.NuevoDesktopTextBox.Caracter_Decimal = CChar(CampoDataTable(0).Caracter_Decimal)
                        EditarCampoForm.NuevoDesktopTextBox.Cantidad_Decimales = CampoDataTable(0).Cantidad_Decimales
                    End If

                    EditarCampoForm.AnteriorDesktopTextBox.Text = nCampo.Valor_File_Data
                Else
                    MessageBox.Show("No se puede modificar el expediente seleccionado ya que la OT a la cual pertenece se encuentra cerrada y/o el proyectono usa modificaciones de OT's cerradas", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If


            Catch ex As Exception
                Return
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            If (EditarCampoForm.ShowDialog() = DialogResult.OK) Then
                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim CampoDataType = New TBL_File_DataType()

                    If (CType(nCampo.fk_Campo_Tipo, DesktopConfig.CampoTipo) = DesktopConfig.CampoTipo.Lista) Then
                        CampoDataType.Valor_File_Data = EditarCampoForm.NuevoComboBox.SelectedValue
                    Else
                        CampoDataType.Valor_File_Data = EditarCampoForm.NuevoDesktopTextBox.Text.ToString().Replace(",", "")
                    End If

                    Dim ValidarSave As Boolean = True

                    Me.Workspace.EventManager.ValidarActualizarDatoBusqueda(nCampo, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, CampoDataType.Valor_File_Data, ValidarSave)

                    If Not ValidarSave Then
                        Exit Sub
                    End If


                    Dim TBL_File_DataTable = dbmCore.SchemaProcess.TBL_File_Data.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campo(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, nCampo.fk_Documento, nCampo.fk_Campo)

                    If TBL_File_DataTable.Count > 0 Then
                        dbmCore.SchemaProcess.TBL_File_Data.DBUpdate(CampoDataType, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, nCampo.fk_Documento, nCampo.fk_Campo)
                    Else
                        dbmCore.SchemaProcess.TBL_File_Data.DBInsert(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, nCampo.fk_Documento, nCampo.fk_Campo, CampoDataType.Valor_File_Data, nCampo.Valor_File_Data.Length)
                    End If

                    If (CType(nCampo.fk_Campo_Tipo, DesktopConfig.CampoTipo) = DesktopConfig.CampoTipo.Lista) Then
                        nCampo.Valor_File_Data = EditarCampoForm.NuevoComboBox.Text
                    Else
                        nCampo.Valor_File_Data = EditarCampoForm.NuevoDesktopTextBox.Text
                    End If

                    'Actualiza usuario que realiza modificacion
                    Dim TBL_File_Update As New DBImaging.SchemaProcess.TBL_FileType
                    TBL_File_Update.Usuario_Modifica_Correccion = Program.Sesion.Usuario.id
                    TBL_File_Update.Fecha_Modifica_Correccion = SlygNullable.SysDate
                    dbmImaging.SchemaProcess.TBL_File.DBUpdate(TBL_File_Update, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, CShort(1))

                    'Actualiza Valor correccion
                    'Dim TBL_File_Data_MCDataTable = dbmImaging.SchemaProcess.TBL_File_Data_MC.DBGet(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, CShort(1), CType(nCampo.fk_Documento, Global.Slyg.Tools.SlygNullable(Of Short)), nCampo.fk_Campo)
                    Dim TBL_File_Data_MCDataTable = dbmImaging.SchemaProcess.TBL_File_Data_MC.DBGet(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, nCampo.fk_Documento, nCampo.fk_Campo, CShort(1))
                    Dim TBL_File_Data_MC_Update As New DBImaging.SchemaProcess.TBL_File_Data_MCType
                    TBL_File_Data_MC_Update.Valor_Correccion = nCampo.Valor_File_Data

                    If TBL_File_Data_MCDataTable.Count > 0 Then
                        'dbmImaging.SchemaProcess.TBL_File_Data_MC.DBUpdate(TBL_File_Data_MC_Update, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, CShort(1), CType(nCampo.fk_Documento, Global.Slyg.Tools.SlygNullable(Of Short)), nCampo.fk_Campo)
                        dbmImaging.SchemaProcess.TBL_File_Data_MC.DBUpdate(TBL_File_Data_MC_Update, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, nCampo.fk_Documento, nCampo.fk_Campo, CShort(1))
                    Else
                        'dbmImaging.SchemaProcess.TBL_File_Data_MC.DBInsert(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, CShort(1), CShort(nCampo.fk_Documento), nCampo.fk_Campo, Nothing, Nothing, Nothing, Nothing, TBL_File_Data_MC_Update.Valor_Correccion, Nothing, Nothing)
                        dbmImaging.SchemaProcess.TBL_File_Data_MC.DBInsert(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, CShort(nCampo.fk_Documento), nCampo.fk_Campo, Nothing, Nothing, CShort(1), Nothing, Nothing, TBL_File_Data_MC_Update.Valor_Correccion, Nothing, Nothing)
                    End If

                    If Not (OTEstado) Then
                        'Event Finaliza Modificación F11
                        Me.Workspace.EventManager.FinalizarActualizarDatosBusqueda(nCampo, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File)
                    Else
                        If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Fecha_Proceso_Cerrada_F11) Then
                            'Reproceso
                            Me.Workspace.EventManager.Reprocesar(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File)
                        End If
                    End If


                Catch ex As Exception
                    Return
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If
        End Sub

        Private Sub EjecutarCruce(ByVal nCampo As CTA_Busqueda_Files_DataRow)

            Try
                'Ejecuta evento Cruce en Plugin
                Me.Workspace.EventManager.Ejecutar_Cruce_En_Linea(nCampo, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File)
            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow(ex.Message, "Ejecutar Cruce", DesktopMessageBoxControl.IconEnum.ErrorIcon, False)
            End Try

        End Sub

        Private Sub ShowEditValidacion(ByVal nValidacion As CTA_Busqueda_Files_ValidacionRow)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim EditarCampoForm As New FormEditarCampo()

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)


                'Validar si ot del expediente esta cerrada
                Dim OTEstado = dbmImaging.SchemaProcess.PA_Consultar_OT_Estado_x_Expediente.DBExecute(nValidacion.fk_Expediente)

                If (OTEstado = False Or Program.ImagingGlobal.ProyectoImagingRow.Usa_Fecha_Proceso_Cerrada_F11 = True) Then

                    If (OTEstado = True And Program.ImagingGlobal.ProyectoImagingRow.Usa_Fecha_Proceso_Cerrada_F11 = True) Then
                        Dim Validar As Boolean = True
                        Dim MsgError As String = ""

                        Me.Workspace.EventManager.Validar_Reprocesar(nValidacion.fk_Expediente, nValidacion.id_Folder, nValidacion.fk_Documento, nValidacion.fk_Validacion, 0, False, Validar, MsgError)

                        If Not Validar Then
                            MessageBox.Show(MsgError.ToString(), Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End If


                    EditarCampoForm.AnteriorDesktopTextBox.Visible = False
                    EditarCampoForm.NuevoDesktopTextBox.Visible = False
                    EditarCampoForm.RadioBtnValtrue.Visible = True
                    EditarCampoForm.RadioBtnValfalse.Visible = True

                    If nValidacion.Respuesta = "Si" Then
                        EditarCampoForm.RadioBtnValtrue.Checked = True
                    Else
                        EditarCampoForm.RadioBtnValfalse.Checked = True
                    End If

                    If (EditarCampoForm.ShowDialog() = DialogResult.OK) Then
                        Try
                            dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                            dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                            Dim FileValidacionDataType = New TBL_File_ValidacionType()

                            If EditarCampoForm.RadioBtnValtrue.Checked = True Then
                                FileValidacionDataType.Respuesta = True
                                nValidacion.Respuesta = "Si"
                            Else
                                EditarCampoForm.RadioBtnValfalse.Checked = False
                                FileValidacionDataType.Respuesta = False
                                nValidacion.Respuesta = "No"
                            End If

                            dbmCore.SchemaProcess.TBL_File_Validacion.DBUpdate(FileValidacionDataType, nValidacion.fk_Expediente, nValidacion.id_Folder, nValidacion.id_File, nValidacion.fk_Validacion, nValidacion.fk_Documento)

                            EventManager.Reprocesar(nValidacion.fk_Expediente, nValidacion.id_Folder, nValidacion.id_File)
                        Catch ex As Exception
                            Return
                        Finally
                            If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                        End Try
                    End If

                Else
                    MessageBox.Show("No se puede modificar el expediente seleccionado ya que la OT a la cual pertenece se encuentra cerrada y/o el proyecto no usa modificaciones de OT's cerradas", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            Catch ex As Exception
                Return
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

        End Sub

        Private Sub ShowGrid(ByVal nCampo As CTA_Busqueda_Files_DataRow)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)

                Dim DefinicionDataTable = dbmCore.SchemaConfig.TBL_Tabla_Asociada.DBFindByfk_Documentofk_CampoEliminado_Campo(nCampo.fk_Documento, nCampo.fk_Campo, False)
                ValoresDataTable = dbmCore.SchemaProcess.PA_File_Data_Asociada_Campos_Activos.DBExecute(CShort(nCampo.fk_Documento), nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, nCampo.fk_Campo)

                Dim Data As New DataTable()

                ' Generar columnas
                Dim IndexColumn As New DataColumn()
                IndexColumn.Caption = "Registro"
                IndexColumn.ColumnName = "Registro"
                Data.Columns.Add(IndexColumn)

                For Each DefinicionRow In DefinicionDataTable
                    Dim NewColumna As New DataColumn()
                    NewColumna.Caption = DefinicionRow.Nombre_Campo
                    NewColumna.ColumnName = DefinicionRow.Nombre_Campo

                    Data.Columns.Add(NewColumna)
                Next

                Dim FileDataAsociadaView = New DataView(ValoresDataTable)
                Dim RegistrosAsociada = FileDataAsociadaView.ToTable(True, "fk_Expediente", "fk_Folder", "fk_File", "fk_Documento", "fk_Campo", "id_File_Data_Asociada")

                For Each ItemRow As DataRow In RegistrosAsociada.Rows
                    FileDataAsociadaView.RowFilter = "id_File_Data_Asociada = " & ItemRow.Item("id_File_Data_Asociada").ToString()
                    FileDataAsociadaView.Sort = "fk_Campo_Tabla"

                    Dim NewRow = Data.NewRow()
                    NewRow(0) = ItemRow.Item("id_File_Data_Asociada")

                    For Col As Integer = 0 To FileDataAsociadaView.Count - 1
                        If (Col <= DefinicionDataTable.Count) Then
                            NewRow.Item(Col + 1) = FileDataAsociadaView.Item(Col).Item("Valor_File_Data")
                        End If
                    Next

                    Data.Rows.Add(NewRow)
                Next

                TablaAsociadaDataGridView.DataSource = Data

                TablaLabel.Text = "Tabla asociada [" & Format(Data.Rows.Count, "#,##0") & "]"

                IndexerTablePanel.Visible = True

            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
            End Try
        End Sub

        Private Sub ShowEditDataAsociada(ByVal nCampo As CTA_Busqueda_Files_DataRow, ByVal nFila As Short, ByVal nColumna As Short)
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim EditarCampoForm As New FormEditarCampo()
            Dim OTEstado As Boolean
            Dim CampoDataTable As DBCore.SchemaConfig.TBL_Tabla_AsociadaDataTable

            Try
                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                'Validar si ot del expediente esta cerrada
                OTEstado = dbmImaging.SchemaProcess.PA_Consultar_OT_Estado_x_Expediente.DBExecute(nCampo.fk_Expediente)

                CampoDataTable = dbmCore.SchemaConfig.TBL_Tabla_Asociada.DBGet(nCampo.fk_Documento, nCampo.fk_Campo, nColumna)

                If (OTEstado = False Or Program.ImagingGlobal.ProyectoImagingRow.Usa_Fecha_Proceso_Cerrada_F11 = True) Then

                    If (OTEstado = True And Program.ImagingGlobal.ProyectoImagingRow.Usa_Fecha_Proceso_Cerrada_F11 = True) Then
                        Dim Validar As Boolean = True
                        Dim MsgError As String = ""

                        Me.Workspace.EventManager.Validar_Reprocesar(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_Documento, nCampo.fk_Campo, nColumna, True, Validar, MsgError)

                        If Not Validar Then
                            MessageBox.Show(MsgError.ToString(), Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End If


                    Select Case CType(CampoDataTable(0).fk_Campo_Tipo, DesktopConfig.CampoTipo)
                        Case DesktopConfig.CampoTipo.Texto
                            EditarCampoForm.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                            EditarCampoForm.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal

                        Case DesktopConfig.CampoTipo.Numerico
                            EditarCampoForm.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico
                            EditarCampoForm.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Numerico

                        Case DesktopConfig.CampoTipo.Fecha
                            EditarCampoForm.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha
                            EditarCampoForm.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Fecha

                        Case DesktopConfig.CampoTipo.Lista
                            EditarCampoForm.NuevoComboBox.Visible = True

                            Dim RowFile = CType(CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_FilesRow)

                            Dim ListaDataTable = dbmCore.SchemaConfig.TBL_Campo_Lista_Item.DBGet(RowFile.fk_Entidad, CShort(CampoDataTable(0).fk_Campo_Lista), Nothing)

                            Utilities.LlenarCombo(EditarCampoForm.NuevoComboBox, ListaDataTable, "Valor_Campo_Lista_Item", "Etiqueta_Campo_Lista_Item", True, "", "-1")

                        Case Else
                            EditarCampoForm.AnteriorDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal
                            EditarCampoForm.NuevoDesktopTextBox.Type = DesktopTextBoxControl.TipoTextBox.Normal

                    End Select

                    EditarCampoForm.AnteriorDesktopTextBox.Visible = True
                    EditarCampoForm.NuevoDesktopTextBox.Visible = True

                    EditarCampoForm.AnteriorDesktopTextBox.MaximumLength = CampoDataTable(0).Length_Campo
                    EditarCampoForm.AnteriorDesktopTextBox.MinimumLength = 0
                    EditarCampoForm.AnteriorDesktopTextBox.Usa_Decimales = CampoDataTable(0).Usa_Decimales

                    EditarCampoForm.NuevoDesktopTextBox.MaximumLength = CampoDataTable(0).Length_Campo
                    EditarCampoForm.NuevoDesktopTextBox.MinimumLength = 0
                    EditarCampoForm.NuevoDesktopTextBox.Usa_Decimales = CampoDataTable(0).Usa_Decimales

                    If CampoDataTable(0).Usa_Decimales Then
                        EditarCampoForm.AnteriorDesktopTextBox.Caracter_Decimal = CChar(CampoDataTable(0).Caracter_Decimal)
                        EditarCampoForm.AnteriorDesktopTextBox.Cantidad_Decimales = CampoDataTable(0).Cantidad_Decimales

                        EditarCampoForm.NuevoDesktopTextBox.Caracter_Decimal = CChar(CampoDataTable(0).Caracter_Decimal)
                        EditarCampoForm.NuevoDesktopTextBox.Cantidad_Decimales = CampoDataTable(0).Cantidad_Decimales
                    End If

                    ' EditarCampoForm.AnteriorDesktopTextBox.Text = CStr(CType(TablaAsociadaDataGridView.DataSource, DataTable).Rows(nFila - 1)(nColumna))
                    If (Not CType(TablaAsociadaDataGridView.DataSource, DataTable).Rows(nFila - 1)(nColumna) Is DBNull.Value) Then
                        EditarCampoForm.AnteriorDesktopTextBox.Text = CStr(CType(TablaAsociadaDataGridView.DataSource, DataTable).Rows(nFila - 1)(nColumna))
                    End If

                Else
                    MessageBox.Show("No se puede modificar el expediente seleccionado ya que la OT a la cual pertenece se encuentra cerrada y/o proyecto no usa modificaciones de OT's cerradas.", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

            Catch ex As Exception
                Return
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try


            If (EditarCampoForm.ShowDialog() = DialogResult.OK) Then
                Try
                    dbmCore.Connection_Open(Program.Sesion.Usuario.id)
                    dbmImaging.Connection_Open(Program.Sesion.Usuario.id)

                    Dim DefinicionDataTable = dbmCore.SchemaConfig.TBL_Tabla_Asociada.DBFindByfk_Documentofk_CampoEliminado_Campo(nCampo.fk_Documento, nCampo.fk_Campo, False)
                    Dim fk_Campo_Tabla As Integer = DefinicionDataTable.Item(nColumna - 1).id_Campo_Tabla
                    Dim CampoDataType = New TBL_File_Data_AsociadaType()

                    If (CType(CampoDataTable(0).fk_Campo_Tipo, DesktopConfig.CampoTipo) = DesktopConfig.CampoTipo.Lista) Then
                        CampoDataType.Valor_File_Data = EditarCampoForm.NuevoComboBox.SelectedValue
                    Else
                        CampoDataType.Valor_File_Data = EditarCampoForm.NuevoDesktopTextBox.Text.ToString().Replace(",", "")
                    End If


                    Dim FileDataAsociadaDataTable = dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBFindByfk_Expedientefk_Folderfk_Filefk_Documentofk_Campofk_Campo_Tablaid_File_Data_Asociada(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, nCampo.fk_Documento, nCampo.fk_Campo, CType(fk_Campo_Tabla, Short), nFila)
                    If FileDataAsociadaDataTable.Count > 0 Then
                        dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBUpdate(CampoDataType, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, nCampo.fk_Documento, nCampo.fk_Campo, CShort(fk_Campo_Tabla), nFila)
                    Else
                        dbmCore.SchemaProcess.TBL_File_Data_Asociada.DBInsert(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, nCampo.fk_Documento, nCampo.fk_Campo, CShort(fk_Campo_Tabla), nFila, CampoDataType.Valor_File_Data, (CampoDataType.Valor_File_Data.ToString()).Length)
                    End If


                    If (CType(CampoDataTable(0).fk_Campo_Tipo, DesktopConfig.CampoTipo) = DesktopConfig.CampoTipo.Lista) Then
                        CType(TablaAsociadaDataGridView.DataSource, DataTable).Rows(nFila - 1)(nColumna) = EditarCampoForm.NuevoComboBox.Text
                    Else
                        CType(TablaAsociadaDataGridView.DataSource, DataTable).Rows(nFila - 1)(nColumna) = EditarCampoForm.NuevoDesktopTextBox.Text
                    End If



                    'Actualiza usuario que realiza modificacion
                    Dim TBL_File_Update As New DBImaging.SchemaProcess.TBL_FileType
                    TBL_File_Update.Usuario_Modifica_Correccion = Program.Sesion.Usuario.id
                    TBL_File_Update.Fecha_Modifica_Correccion = SlygNullable.SysDate
                    dbmImaging.SchemaProcess.TBL_File.DBUpdate(TBL_File_Update, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, CShort(1))

                    'Actualiza Valor correccion
                    Dim TBL_File_Data_MC_AsociadaDataTable = dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBGet(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, CShort(1), nCampo.fk_Documento, nCampo.fk_Campo, CShort(fk_Campo_Tabla), nFila)
                    Dim TBL_File_Data_MC_Asociada_Update As New DBImaging.SchemaProcess.TBL_File_Data_MC_AsociadaType
                    TBL_File_Data_MC_Asociada_Update.Valor_Correccion = CampoDataType.Valor_File_Data

                    If TBL_File_Data_MC_AsociadaDataTable.Count > 0 Then
                        dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBUpdate(TBL_File_Data_MC_Asociada_Update, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, CShort(1), nCampo.fk_Documento, nCampo.fk_Campo, CShort(fk_Campo_Tabla), nFila)
                    Else
                        dbmImaging.SchemaProcess.TBL_File_Data_MC_Asociada.DBInsert(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File, CShort(1), nCampo.fk_Documento, nCampo.fk_Campo, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, TBL_File_Data_MC_Asociada_Update.Valor_Correccion, Nothing)
                    End If

                    If Not (OTEstado) Then
                        'Event Finaliza Modificación F11
                        Me.Workspace.EventManager.FinalizarActualizarDatosBusqueda(nCampo, nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File)
                    Else
                        If (Program.ImagingGlobal.ProyectoImagingRow.Usa_Fecha_Proceso_Cerrada_F11) Then
                            'Reproceso
                            Me.Workspace.EventManager.Reprocesar(nCampo.fk_Expediente, nCampo.fk_Folder, nCampo.fk_File)
                        End If
                    End If

                Catch ex As Exception
                    Return
                Finally
                    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                End Try
            End If


        End Sub

        Private Sub Reproceso()
            Dim f = New FormReprocesoMotivo()
            f.LoadData(MotivosReproceso())
            If (f.ShowDialog() = DialogResult.OK) Then
                ReprocesoDocumento(f.Motivo)
            End If
        End Sub

        'Actualizar las imágenes en bd -- Oswaldo Ibarra -- 13/12/2019
        Private Sub btnGuardar_Click(sender As System.Object, e As System.EventArgs) Handles btnGuardar.Click, btnGuardarCambios.Click

            Dim manager As FileProviderManager = Nothing
            Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Try
                Dim RowFile = CType(CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row, CTA_Busqueda_FilesRow)
                Dim ListImages = New List(Of String)
                Dim tempPath = Program.AppPath & Program.TempPath

                dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmCore.Connection_Open(0)
                dbmImaging.Connection_Open(0)

                manager = New FileProviderManager(RowFile.fk_Expediente, RowFile.fk_Folder, dbmImaging, Program.Sesion.Usuario.id)
                manager.Connect()

                btnGuardarCambios.Enabled = False

                'Se obtienen los folios del file.
                Dim folios = manager.GetFolios(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version)
                If (folios > 0) Then
                    For folio = CShort(1) To folios
                        ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                        Dim Imagen As Byte() = FileToByteArray(ListImages(folio - 1).ToString())
                        Dim Thumbnail As Byte() = FileToByteArray(ListImages(folio - 1).ToString())
                        manager.UpdateFolio(RowFile.fk_Expediente, RowFile.fk_Folder, RowFile.fk_File, RowFile.id_Version, folio, CType(Imagen, Byte()), CType(Thumbnail, Byte()))
                    Next
                End If

                MessageBox.Show("La imagen ha sido actualizada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ListImages.Clear()
                btnGuardarCambios.Enabled = True
            Catch ex As Exception
                Return
            Finally
                If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try
        End Sub

#End Region

#Region " Funciones "

        Public Function ThumbnailCallback() As Boolean
            Return False
        End Function

        Public Function MotivosReproceso() As List(Of Item)
            Dim Motivos As New List(Of Item)

            Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing

            Try
                dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                dbmImaging.Connection_Open(1)

                Dim MotivoReprocesoDataTable = dbmImaging.SchemaProcess.TBL_Reproceso_Motivo.DBGet(Nothing)
                Dim MotivoReprocesoDataView As New DataView(MotivoReprocesoDataTable)
                MotivoReprocesoDataView.Sort = MotivoReprocesoDataTable.Id_Reproceso_MotivoColumn.ColumnName

                For Each MotivoItem As DataRowView In MotivoReprocesoDataView
                    Dim MotivoRow = CType(MotivoItem.Row, TBL_Reproceso_MotivoRow)
                    Motivos.Add(New Item(MotivoRow.Id_Reproceso_Motivo, MotivoRow.Nombre_Reproceso_Motivo))
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message, Program.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
            End Try

            Return Motivos
        End Function

        'Función para convertir ruta del archivo a Byte -- Oswaldo Ibarra -- 13/12/2019
        Private Function FileToByteArray(ByVal filename As String) As Byte()
            Dim fs As FileStream = New FileStream(filename, FileMode.Open, FileAccess.Read)
            Dim ImageData As Byte() = New Byte(CInt(fs.Length - 1)) {}
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length))
            fs.Close()
            Return ImageData
        End Function

#End Region
       
    End Class

End Namespace