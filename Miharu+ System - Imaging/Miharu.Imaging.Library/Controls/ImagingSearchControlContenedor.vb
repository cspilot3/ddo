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
Imports Miharu.Desktop.Library.MiharuDMZ

Namespace Controls

    Public Class ImagingSearchControlContenedor

#Region " Declaraciones "

        Private FileDataTable As New DBCore.SchemaImaging.CTA_Busqueda_Registros_ContenedorDataTable
        Private FileDataDataTable As New DBCore.SchemaImaging.CTA_Busqueda_Data_ContenedorDataTable

        'Private _centro As DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoSimpleType

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
            Me.SearchParameters = New ImagingSearchControlParametersContenedor()

            If (Not Me.DesignMode) Then
                SetSearchControl(Me.SearchParameters)
            End If
        End Sub

#End Region

#Region " Eventos "

        Private Sub ImagingSearchControl_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Enter
            SearchParameters.SetFocus()
        End Sub

        Private Sub ShowImageButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ShowImageButton.Click
            If TipologiasDataGridView.SelectedRows.Count > 0 Then
                Dim RowFile = CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row
                MostrarImagen(CInt(RowFile("fk_Cargue")), CShort(RowFile("fk_Cargue_Paquete")), CShort(RowFile("id_Cargue_Item")))
            End If
        End Sub

        Private Sub ResultadosDataGridView_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ResultadosDataGridView.SelectionChanged
            If ResultadosDataGridView.SelectedRows.Count > 0 Then
                Dim RowFolder = CType(ResultadosDataGridView.CurrentRow.DataBoundItem, DataRowView).Row
                ShowTipologias(CInt(RowFolder("fk_Cargue")), CShort(RowFolder("fk_Cargue_Paquete")))
                ShowAccesosBusqueda()
            End If
        End Sub

        Private Sub TipologiasDataGridView_SelectionChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TipologiasDataGridView.SelectionChanged
            If TipologiasDataGridView.SelectedRows.Count > 0 Then
                Dim RowFile = CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row
                ShowInformacion(CInt(RowFile("fk_Cargue")), CShort(RowFile("fk_Cargue_Paquete")), CShort(RowFile("id_Cargue_Item")))
                ShowAccesosBusqueda()
            End If
        End Sub

        Private Sub DisplayResult(ByVal nData As DataTable)
            FileDataTable.Clear()
            FileDataDataTable.Clear()
            ResultadosDataGridView.DataSource = Nothing

            If nData.Rows.Count > 0 Then
                ResultadosDataGridView.DataSource = nData
            End If

            If nData.Rows.Count = 0 Then
                MessageBox.Show("No se encontraron coincidencias", Program.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            ResultadosLabel.Text = "Resultados [" & Format(nData.Rows.Count, "#,##0") & "]"

            IndexerTablePanel.Visible = False
            TipologiasDataGridView.ClearSelection()
            If nData.Rows.Count > 0 Then
                ShowAccesosBusqueda()
            Else
                ShowAccesosBusqueda()
            End If
        End Sub

        Private Sub BeginSearch()
            Me.Cursor = Cursors.WaitCursor
        End Sub

        Private Sub EndSearch()
            Me.Cursor = Cursors.Default
        End Sub

        Private Sub Table_CloseButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Table_CloseButton.Click
            IndexerTablePanel.Visible = False
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

        Private Sub ShowTipologias(ByVal nCargue As Integer, ByVal nCarguePaquete As Short)
            Dim DataTableVista As New DataTable

            Try
                FileDataTable.Clear()

                Dim QueryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Imaging.CTA_Busqueda_Registros_Contenedor", New List(Of QueryParameter) From {
                   New QueryParameter With {.name = "fk_Cargue", .value = nCargue.ToString()},
                   New QueryParameter With {.name = "fk_Cargue_Paquete", .value = nCarguePaquete.ToString()}
                    }, QueryRequestType.Table, QueryResponseType.Table)

                FileDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaImaging.CTA_Busqueda_Registros_ContenedorDataTable(), QueryResponse.dataTable), DBCore.SchemaImaging.CTA_Busqueda_Registros_ContenedorDataTable)

                DataTableVista = FileDataTable


                TipologiasDataGridView.DataSource = DataTableVista

                TipologiasLabel.Text = "Tipologias [" & Format(DataTableVista.Rows.Count, "#,##0") & "]"
            Catch
                Throw
            End Try
        End Sub

        Private Sub ShowInformacion(ByVal nCargue As Long, ByVal nCarguePaquete As Short, ByVal nCarguePaqueteItem As Short)
            Try
                FileDataDataTable.Clear()

                Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Core].Imaging.PA_Busqueda_Data_Contenedor", New List(Of QueryParameter) From {
                   New QueryParameter With {.name = "fk_Cargue", .value = nCargue.ToString()},
                   New QueryParameter With {.name = "fk_Cargue_Paquete", .value = nCarguePaquete.ToString()}
                    }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                FileDataDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaImaging.CTA_Busqueda_Data_ContenedorDataTable(), queryResponse.dataTable), DBCore.SchemaImaging.CTA_Busqueda_Data_ContenedorDataTable)

                CamposDataGridView.DataSource = FileDataDataTable

                InformacionLabel.Text = "Información [" & Format(FileDataDataTable.Rows.Count, "#,##0") & "]"

            Catch
                Throw
            End Try
        End Sub

        Private Sub ShowAccesosBusqueda()
            If Not _Locked Then
                If TipologiasDataGridView.SelectedRows.Count > 0 Then
                    Dim RowFile = CType(TipologiasDataGridView.CurrentRow.DataBoundItem, DataRowView).Row
                    Try
                        ShowImageButton.Enabled = (Not RowFile("Nombre_Documento") Is Nothing)
                    Catch ex As Exception
                        ShowImageButton.Enabled = False
                    End Try

                Else

                    ShowImageButton.Enabled = False

                End If
            End If
        End Sub

        Private Sub MostrarImagen(ByVal nCargue As Integer, ByVal nCarguePaquete As Short, ByVal nCarguePaqueteItem As Short)
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

            'Dim manager As FileProviderManager = Nothing
            'Dim dbmCore As DBCore.DBCoreDataBaseManager = Nothing
            'Dim dbmImaging As DBImaging.DBImagingDataBaseManager = Nothing
            Dim Folios As Short = 0
            Dim ListImages = New List(Of String)
            Const Formato As ImageManager.EnumFormat = ImageManager.EnumFormat.Tiff
            Const Compresion As ImageManager.EnumCompression = ImageManager.EnumCompression.Lzw

            Try

                'dbmCore = New DBCore.DBCoreDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Core)
                'dbmImaging = New DBImaging.DBImagingDataBaseManager(Program.DesktopGlobal.ConnectionStrings.Imaging)

                'dbmCore.Connection_Open(0)
                'dbmImaging.Connection_Open(0)

                'Dim queryResponse As QueryResponse = ClientUtil.resolver("[DB_Miharu.Imaging_Core].Security.CTA_Centro_Procesamiento", New List(Of QueryParameter) From {
                '   New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                '   New QueryParameter With {.name = "fk_Sede", .value = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede.ToString()},
                '   New QueryParameter With {.name = "id_Centro_Procesamiento", .value = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento.ToString()}
                '    }, QueryRequestType.Table, QueryResponseType.Table)

                'Me._centro = CType(ClientUtil.mapToTypedTable(New DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoDataTable(), queryResponse.dataTable), DBImaging.SchemaSecurity.CTA_Centro_ProcesamientoDataTable)(0).ToCTA_Centro_ProcesamientoSimpleType()

                'manager = New FileProviderManager(nCargue, Me._centro, dbmImaging, 1)
                'manager.Connect()

                'Folios = manager.GetFolios(nCargue, nCarguePaquete, nCarguePaqueteItem)

                Dim queryResponseFoliosCargueItem As QueryResponse = ClientUtil.GetFoliosCargueItem("", New List(Of QueryParameter) From {
                                                                    New QueryParameter With {.name = "fk_Cargue", .value = nCargue.ToString()},
                                                                    New QueryParameter With {.name = "fk_Cargue_Paquete", .value = nCarguePaquete.ToString()},
                                                                    New QueryParameter With {.name = "fk_Cargue_Paquete_Item", .value = nCarguePaqueteItem.ToString()},
                                                                    New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                                    New QueryParameter With {.name = "fk_Sede", .value = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede.ToString()},
                                                                    New QueryParameter With {.name = "id_Centro_Procesamiento", .value = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento.ToString()}
                                                                }, QueryRequestType.Table, QueryResponseType.Scalar)
                Folios = CShort(queryResponseFoliosCargueItem.scalar)

                If Folios > 0 Then
                    For folio As Short = 1 To Folios
                        Dim Imagen As Byte() = Nothing
                        Dim Thumbnail As Byte() = Nothing
                        Dim filePath As String

                        'manager.GetFolio(nCargue, nCarguePaquete, nCarguePaqueteItem, folio, Imagen, Thumbnail)

                        Dim folioItemDataTable As DBStorage.SchemaImaging.TBL_Item_FolioDataTable = Nothing

                        Dim queryResponseFolioCargueItem As QueryResponse = ClientUtil.GetFolioCargueItem("", New List(Of QueryParameter) From {
                                                                    New QueryParameter With {.name = "fk_Cargue", .value = nCargue.ToString()},
                                                                    New QueryParameter With {.name = "fk_Cargue_Paquete", .value = nCarguePaquete.ToString()},
                                                                    New QueryParameter With {.name = "fk_Cargue_Paquete_Item", .value = nCarguePaqueteItem.ToString()},
                                                                    New QueryParameter With {.name = "folio", .value = folio.ToString()},
                                                                    New QueryParameter With {.name = "fk_Entidad", .value = Program.ImagingGlobal.Entidad.ToString()},
                                                                    New QueryParameter With {.name = "fk_Sede", .value = Program.DesktopGlobal.CentroProcesamientoRow.fk_Sede.ToString()},
                                                                    New QueryParameter With {.name = "id_Centro_Procesamiento", .value = Program.DesktopGlobal.CentroProcesamientoRow.id_Centro_Procesamiento.ToString()}
                                                                }, QueryRequestType.Table, QueryResponseType.Table)
                        folioItemDataTable = CType(ClientUtil.mapToTypedTable(New DBStorage.SchemaImaging.TBL_Item_FolioDataTable(), queryResponseFolioCargueItem.dataTable), DBStorage.SchemaImaging.TBL_Item_FolioDataTable)

                        If folioItemDataTable.Rows.Count > 0 Then
                            Imagen = CType(folioItemDataTable.Rows(0)("Image_Binary"), Byte())
                            Thumbnail = CType(folioItemDataTable.Rows(0)("Thumbnail_Binary"), Byte())
                        Else
                            Throw New Exception("No se encontró imagen asociado")
                        End If

                        Using ms = New MemoryStream(Imagen)
                            ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                            filePath = ListImages(folio - 1).ToString()
                            Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                            img.Save(filePath, System.Drawing.Imaging.ImageFormat.Tiff)
                            ms.Dispose()
                        End Using
                    Next
                Else
                    'If (manager IsNot Nothing) Then manager.Disconnect()

                    Dim FilesDataTable As DBCore.SchemaImaging.TBL_FileDataTable

                    Dim queryResponse = ClientUtil.resolver("[DB_Miharu.Core].Process.Pa_get_Expediente_Folder_File_x_CargueItem", New List(Of QueryParameter) From {
                                       New QueryParameter With {.name = "fk_Cargue", .value = nCargue.ToString()},
                                       New QueryParameter With {.name = "fk_Cargue_Paquete", .value = nCarguePaquete.ToString()},
                                       New QueryParameter With {.name = "fk_Cargue_Paquete_Item", .value = nCarguePaqueteItem.ToString()}
                                        }, QueryRequestType.StoredProcedure, QueryResponseType.Table)

                    FilesDataTable = CType(ClientUtil.mapToTypedTable(New DBCore.SchemaImaging.TBL_FileDataTable(), queryResponse.dataTable), DBCore.SchemaImaging.TBL_FileDataTable)

                    If (FilesDataTable.Count = 0) Then Throw New Exception("Imágenes no encontradas")

                    Dim FileRow = FilesDataTable(0)

                    'manager = New FileProviderManager(FileRow.fk_Expediente, FileRow.fk_Folder, dbmImaging, Program.Sesion.Usuario.id)
                    'manager.Connect()

                    'Obtiene el File a visualizar.
                    If (FileRow.Es_Anexo) Then
                        'Folios = manager.GetFolios(FileRow.fk_Anexo)

                        Dim queryResponseFoliosFile As QueryResponse = ClientUtil.GetFoliosFile("", New List(Of QueryParameter) From {
                                                                    New QueryParameter With {.name = "fk_Expediente", .value = FileRow.fk_Expediente.ToString()},
                                                                    New QueryParameter With {.name = "fk_Folder", .value = FileRow.fk_Folder.ToString()},
                                                                    New QueryParameter With {.name = "fk_File", .value = FileRow.fk_File.ToString()},
                                                                    New QueryParameter With {.name = "fk_Version", .value = FileRow.id_Version.ToString()},
                                                                    New QueryParameter With {.name = "es_Anexo", .value = FileRow.Es_Anexo.ToString()},
                                                                    New QueryParameter With {.name = "fk_Anexo", .value = FileRow.fk_Anexo.ToString()}
                                                                }, QueryRequestType.Table, QueryResponseType.Scalar)
                        Folios = CShort(queryResponseFoliosFile.scalar)

                        If (folios > 0) Then
                            For folio = CShort(1) To Folios
                                Dim Imagen() As Byte = Nothing
                                Dim Thumbnail() As Byte = Nothing
                                'manager.GetFolio(FilesDataTable(0).fk_Anexo, folio, Imagen, Thumbnail)

                                Dim folioFileDataTable As DBStorage.SchemaImaging.TBL_File_FolioDataTable = Nothing

                                Dim queryResponseFolioFile As QueryResponse = ClientUtil.GetFolioFile("", New List(Of QueryParameter) From {
                                                                     New QueryParameter With {.name = "fk_Expediente", .value = FileRow.fk_Expediente.ToString()},
                                                                    New QueryParameter With {.name = "fk_Folder", .value = FileRow.fk_Folder.ToString()},
                                                                    New QueryParameter With {.name = "fk_File", .value = FileRow.fk_File.ToString()},
                                                                    New QueryParameter With {.name = "fk_Version", .value = FileRow.id_Version.ToString()},
                                                                    New QueryParameter With {.name = "es_Anexo", .value = FileRow.Es_Anexo.ToString()},
                                                                    New QueryParameter With {.name = "fk_Anexo", .value = FileRow.fk_Anexo.ToString()},
                                                                    New QueryParameter With {.name = "folio", .value = folio.ToString()}
                                                                }, QueryRequestType.Table, QueryResponseType.Table)
                                folioFileDataTable = CType(ClientUtil.mapToTypedTable(New DBStorage.SchemaImaging.TBL_File_FolioDataTable(), queryResponseFolioFile.dataTable), DBStorage.SchemaImaging.TBL_File_FolioDataTable)

                                If foliofileDataTable.Rows.Count > 0 Then
                                    Imagen = CType(foliofileDataTable.Rows(0)("Image_Binary"), Byte())
                                    Thumbnail = CType(foliofileDataTable.Rows(0)("Thumbnail_Binary"), Byte())
                                Else
                                    Throw New Exception("No se encontró imagen asociado")
                                End If

                                ImageManager.Save(New FreeImageAPI.FreeImageBitmap(New MemoryStream(Imagen)), tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida, "", Formato, Compresion, False, tempPath)
                                ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                            Next
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("No existe una imagen asociada para visualizar.", "Imágen no encontrada", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        End If
                    Else
                        'Se obtienen los folios del file.
                        'Folios = manager.GetFolios(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version)
                        Dim fk_Anexo As Long = 0

                        Dim queryResponseFoliosFile As QueryResponse = ClientUtil.GetFoliosFile("", New List(Of QueryParameter) From {
                                                                    New QueryParameter With {.name = "fk_Expediente", .value = FileRow.fk_Expediente.ToString()},
                                                                    New QueryParameter With {.name = "fk_Folder", .value = FileRow.fk_Folder.ToString()},
                                                                    New QueryParameter With {.name = "fk_File", .value = FileRow.fk_File.ToString()},
                                                                    New QueryParameter With {.name = "fk_Version", .value = FileRow.id_Version.ToString()},
                                                                    New QueryParameter With {.name = "es_Anexo", .value = FileRow.Es_Anexo.ToString()},
                                                                    New QueryParameter With {.name = "fk_Anexo", .value = fk_Anexo.ToString()}
                                                                }, QueryRequestType.Table, QueryResponseType.Scalar)
                        Folios = CShort(queryResponseFoliosFile.scalar)

                        If (folios > 0) Then
                            For folio = CShort(1) To Folios
                                Dim Imagen() As Byte = Nothing
                                Dim Thumbnail() As Byte = Nothing
                                Dim filePath As String
                                'manager.GetFolio(FileRow.fk_Expediente, FileRow.fk_Folder, FileRow.fk_File, FileRow.id_Version, folio, Imagen, Thumbnail)

                                Dim folioFileDataTable As DBStorage.SchemaImaging.TBL_File_FolioDataTable = Nothing

                                Dim queryResponseFolioFile As QueryResponse = ClientUtil.GetFolioFile("", New List(Of QueryParameter) From {
                                                                     New QueryParameter With {.name = "fk_Expediente", .value = FileRow.fk_Expediente.ToString()},
                                                                    New QueryParameter With {.name = "fk_Folder", .value = FileRow.fk_Folder.ToString()},
                                                                    New QueryParameter With {.name = "fk_File", .value = FileRow.fk_File.ToString()},
                                                                    New QueryParameter With {.name = "fk_Version", .value = FileRow.id_Version.ToString()},
                                                                    New QueryParameter With {.name = "es_Anexo", .value = FileRow.Es_Anexo.ToString()},
                                                                    New QueryParameter With {.name = "fk_Anexo", .value = fk_Anexo.ToString()},
                                                                    New QueryParameter With {.name = "folio", .value = folio.ToString()}
                                                                }, QueryRequestType.Table, QueryResponseType.Table)
                                folioFileDataTable = CType(ClientUtil.mapToTypedTable(New DBStorage.SchemaImaging.TBL_File_FolioDataTable(), queryResponseFolioFile.dataTable), DBStorage.SchemaImaging.TBL_File_FolioDataTable)

                                If foliofileDataTable.Rows.Count > 0 Then
                                    Imagen = CType(foliofileDataTable.Rows(0)("Image_Binary"), Byte())
                                    Thumbnail = CType(foliofileDataTable.Rows(0)("Thumbnail_Binary"), Byte())
                                Else
                                    Throw New Exception("No se encontró imagen asociado")
                                End If

                                Using ms = New MemoryStream(Imagen)
                                    ListImages.Add(tempPath & folio.ToString() & Program.ImagingGlobal.ProyectoImagingRow.Extension_Formato_Imagen_Salida)
                                    filePath = ListImages(folio - 1).ToString()
                                    Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                                    img.Save(filePath, System.Drawing.Imaging.ImageFormat.Tiff)
                                    ms.Dispose()
                                End Using
                            Next
                        Else
                            DesktopMessageBoxControl.DesktopMessageShow("No existe una imagen asociada para visualizar.", "Imágen no encontrada", DesktopMessageBoxControl.IconEnum.AdvertencyIcon, True)
                        End If
                    End If
                End If

                CentralOffLineViewer.ImagePath = ListImages
                btnGuardarCambios.Enabled = True

            Catch ex As Exception
                DesktopMessageBoxControl.DesktopMessageShow("CargarImagen", ex)
                'Finally
                '    If (dbmCore IsNot Nothing) Then dbmCore.Connection_Close()
                '    If (dbmImaging IsNot Nothing) Then dbmImaging.Connection_Close()
                '    If (manager IsNot Nothing) Then manager.Disconnect()
            End Try
        End Sub

#End Region

#Region " Funciones "

        Public Function ThumbnailCallback() As Boolean
            Return False
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